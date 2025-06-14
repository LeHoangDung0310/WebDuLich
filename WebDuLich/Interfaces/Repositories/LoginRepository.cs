using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using WebDuLich.Data;
using WebDuLich.Interfaces.dto;
using WebDuLich.Interfaces.IRepositories;
using Microsoft.EntityFrameworkCore;




namespace WebDuLich.Interfaces.Repositories
{
	public class LoginRepository : ILoginRepository
	{
		private readonly MyDbContext _context;
		private readonly IConfiguration _configuration;

		public LoginRepository(MyDbContext context, IConfiguration configuration)
		{
			_context = context;
			_configuration = configuration;
		}
		private async Task<TokenModel> GenerateTokens(TaiKhoan taiKhoan)
		{
			if (taiKhoan == null)
			{
				throw new ArgumentNullException(nameof(taiKhoan));
			}

			// Đảm bảo load QuyenTaiKhoan
			if (taiKhoan.QuyenTaiKhoan == null)
			{
				var loadedTaiKhoan = await _context.TaiKhoans
					.Include(t => t.QuyenTaiKhoan)
					.FirstOrDefaultAsync(t => t.MaTK == taiKhoan.MaTK);

				if (loadedTaiKhoan == null)
				{
					throw new InvalidOperationException($"Unable to load account with ID {taiKhoan.MaTK}");
				}
				taiKhoan = loadedTaiKhoan;
			}

			var jwtTokenHandler = new JwtSecurityTokenHandler();
			var secretKey = _configuration["AppSettings:SecretKey"] ?? throw new InvalidOperationException("JWT secret key not configured");
			var secretKeyBytes = Encoding.UTF8.GetBytes(secretKey);

			// Tạo token identifier
			var tokenId = Guid.NewGuid().ToString();

			var tokenDescription = new SecurityTokenDescriptor
			{
				Subject = new ClaimsIdentity(new[]
				{
					new Claim(ClaimTypes.Name, taiKhoan.TenDangNhap),
					new Claim(ClaimTypes.NameIdentifier, taiKhoan.MaTK),
					new Claim(ClaimTypes.Role, taiKhoan.MaQuyen ?? ""), // Ensure MaQuyen is not null
					new Claim("UserName", taiKhoan.TenDangNhap),
					new Claim(JwtRegisteredClaimNames.Jti, tokenId),
					new Claim("Role", taiKhoan.MaQuyen ?? ""), // Additional role claim for debugging
					new Claim("TenQuyen", taiKhoan.QuyenTaiKhoan?.TenQuyen ?? "") // Include role name for debugging
				}),
				Expires = DateTime.UtcNow.AddMinutes(20),
				SigningCredentials = new SigningCredentials(
					new SymmetricSecurityKey(secretKeyBytes),
					SecurityAlgorithms.HmacSha256) // Changed from HmacSha256Signature to match validation
			};

			var token = jwtTokenHandler.CreateToken(tokenDescription);
			var accessToken = jwtTokenHandler.WriteToken(token);

			using var transaction = await _context.Database.BeginTransactionAsync();
			try
			{
				// Remove old refresh tokens
				var oldTokens = await _context.RefreshTokens
					.Where(rt => rt.UserId == taiKhoan.MaTK)
					.ToListAsync();
				if (oldTokens.Any())
				{
					_context.RefreshTokens.RemoveRange(oldTokens);
					await _context.SaveChangesAsync();
				}

				// Create new refresh token
				var refreshToken = new RefreshToken
				{
					Id = Guid.NewGuid(),
					JwtId = tokenId, // Sử dụng tokenId đã tạo
					UserId = taiKhoan.MaTK,
					Token = Convert.ToBase64String(Guid.NewGuid().ToByteArray()),
					IsUsed = false,
					IsRevoked = false,
					IssuedAt = DateTime.UtcNow,
					ExpiredAt = DateTime.UtcNow.AddDays(7)
				};

				await _context.RefreshTokens.AddAsync(refreshToken);
				await _context.SaveChangesAsync();
				await transaction.CommitAsync();

				return new TokenModel
				{
					AccessToken = accessToken,
					RefreshToken = refreshToken.Token,
					AccessTokenExpires = DateTime.UtcNow.AddMinutes(20),
					RefreshTokenExpires = DateTime.UtcNow.AddDays(7),
					TokenType = "Bearer"
				};
			}
			catch (Exception ex)
			{
				await transaction.RollbackAsync();
				throw new Exception($"Error generating tokens: {ex.InnerException?.Message ?? ex.Message}", ex);
			}
		}
		public async Task<TokenModel?> RenewToken(RefreshTokenDTO model)
		{
			if (model?.AccessToken == null || model.RefreshToken == null)
			{
				System.Diagnostics.Debug.WriteLine("[Token Debug] Invalid refresh token request - missing tokens");
				return null;
			}

			var jwtTokenHandler = new JwtSecurityTokenHandler();
			var secretKey = _configuration["AppSettings:SecretKey"] ?? throw new InvalidOperationException("JWT secret key not configured");
			var secretKeyBytes = Encoding.UTF8.GetBytes(secretKey);
			var tokenValidateParam = new TokenValidationParameters
			{
				ValidateIssuer = false,
				ValidateAudience = false,
				ValidateIssuerSigningKey = true,
				IssuerSigningKey = new SymmetricSecurityKey(secretKeyBytes),
				ClockSkew = TimeSpan.Zero,
				ValidateLifetime = false // Không kiểm tra token hết hạn
			};

			try
			{
				System.Diagnostics.Debug.WriteLine($"[Token Debug] Validating token...");
				var tokenInVerification = jwtTokenHandler.ValidateToken(
					model.AccessToken,
					tokenValidateParam,
					out var validatedToken);

				// Kiểm tra thuật toán và log claims
				if (validatedToken is JwtSecurityToken jwtSecurityToken)
				{
					var claims = jwtSecurityToken.Claims.Select(c => $"{c.Type}: {c.Value}");
					System.Diagnostics.Debug.WriteLine($"[Token Debug] Token claims: {string.Join(", ", claims)}");
					System.Diagnostics.Debug.WriteLine($"[Token Debug] Token algorithm: {jwtSecurityToken.Header.Alg}");

					var result = jwtSecurityToken.Header.Alg.Equals(
						SecurityAlgorithms.HmacSha256,
						StringComparison.InvariantCultureIgnoreCase);
					if (!result)
					{
						System.Diagnostics.Debug.WriteLine("[Token Debug] Algorithm validation failed");
						return null;
					}
				}
				else
				{
					System.Diagnostics.Debug.WriteLine("[Token Debug] Invalid token format");
					return null;
				}

				// Kiểm tra refresh token trong db
				var storedToken = await _context.RefreshTokens
					.FirstOrDefaultAsync(x => x.Token == model.RefreshToken);

				if (storedToken == null)
				{
					System.Diagnostics.Debug.WriteLine("[Token Debug] Refresh token not found in database");
					return null;
				}

				// Kiểm tra refresh token đã sử dụng/thu hồi chưa
				if (storedToken.IsUsed || storedToken.IsRevoked)
				{
					System.Diagnostics.Debug.WriteLine("[Token Debug] Refresh token is used or revoked");
					return null;
				}

				// Kiểm tra refresh token còn hạn không
				if (storedToken.ExpiredAt < DateTime.UtcNow)
				{
					System.Diagnostics.Debug.WriteLine("[Token Debug] Refresh token has expired");
					return null;
				}

				// Lấy thông tin user
				var taiKhoan = await _context.TaiKhoans
					.Include(t => t.QuyenTaiKhoan)  // Make sure to include role information
					.FirstOrDefaultAsync(x => x.MaTK == storedToken.UserId);

				if (taiKhoan == null)
				{
					System.Diagnostics.Debug.WriteLine("[Token Debug] User not found");
					return null;
				}

				// Đánh dấu token cũ đã sử dụng
				storedToken.IsUsed = true;
				_context.RefreshTokens.Update(storedToken);
				await _context.SaveChangesAsync();

				// Tạo token mới
				System.Diagnostics.Debug.WriteLine($"[Token Debug] Generating new tokens for user {taiKhoan.TenDangNhap} with role {taiKhoan.MaQuyen}");
				return await GenerateTokens(taiKhoan);
			}
			catch (Exception ex)
			{
				System.Diagnostics.Debug.WriteLine($"[Token Debug] Error in token renewal: {ex.Message}");
				return null;
			}
		}
		public async Task<LoginResponse> Login(LoginDTO loginDTO)
		{
			System.Diagnostics.Debug.WriteLine($"[Login Debug] Attempting login for user: {loginDTO?.TenDangNhap}");

			if (loginDTO == null)
			{
				throw new ArgumentNullException(nameof(loginDTO));
			}

			var taiKhoan = await _context.TaiKhoans
				.Include(x => x.QuyenTaiKhoan)
				.FirstOrDefaultAsync(x => x.TenDangNhap == loginDTO.TenDangNhap);

			if (taiKhoan == null)
			{
				System.Diagnostics.Debug.WriteLine($"[Login Debug] User not found: {loginDTO.TenDangNhap}");
				return new LoginResponse
				{
					Success = false,
					Message = "Tên đăng nhập hoặc mật khẩu không đúng"
				};
			}

			if (taiKhoan.MatKhau != loginDTO.MatKhau)
			{
				System.Diagnostics.Debug.WriteLine($"[Login Debug] Invalid password for user: {loginDTO.TenDangNhap}");
				return new LoginResponse
				{
					Success = false,
					Message = "Tên đăng nhập hoặc mật khẩu không đúng"
				};
			}

			System.Diagnostics.Debug.WriteLine($"[Login Debug] User found: {taiKhoan.TenDangNhap}, Role: {taiKhoan.MaQuyen}, RoleName: {taiKhoan.QuyenTaiKhoan?.TenQuyen}");

			try
			{
				var tokens = await GenerateTokens(taiKhoan);
				if (tokens == null)
				{
					throw new InvalidOperationException("Failed to generate authentication tokens");
				}

				// Log token generation success
				System.Diagnostics.Debug.WriteLine($"Generated token for user {taiKhoan.TenDangNhap} with role {taiKhoan.MaQuyen}");

				return new LoginResponse
				{
					Success = true,
					Message = "Đăng nhập thành công",
					MaTK = taiKhoan.MaTK ?? "",
					TenDangNhap = taiKhoan.TenDangNhap ?? "",
					MaQuyen = taiKhoan.MaQuyen ?? "",
					TenQuyen = taiKhoan.QuyenTaiKhoan?.TenQuyen ?? "",
					Token = tokens.AccessToken,
					RefreshToken = tokens.RefreshToken
				};
			}
			catch (Exception ex)
			{
				return new LoginResponse
				{
					Success = false,
					Message = "Lỗi trong quá trình đăng nhập: " + ex.Message
				};
			}
		}

		public Task<bool> CheckExistUsername(string username)
		{
			throw new NotImplementedException();
		}

		// ...existing code...
	}
}