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
			var jwtTokenHandler = new JwtSecurityTokenHandler();
			var secretKeyBytes = Encoding.UTF8.GetBytes(_configuration["AppSettings:SecretKey"]);

			// Tạo token identifier
			var tokenId = Guid.NewGuid().ToString();

			var tokenDescription = new SecurityTokenDescriptor
			{
				Subject = new ClaimsIdentity(new[]
				{
					new Claim(ClaimTypes.Name, taiKhoan.TenDangNhap),
					new Claim(ClaimTypes.NameIdentifier, taiKhoan.MaTK),
					new Claim(ClaimTypes.Role, taiKhoan.MaQuyen),
					new Claim("UserName", taiKhoan.TenDangNhap),
					new Claim(JwtRegisteredClaimNames.Jti, tokenId), // Thêm JWT ID claim
				}),
				Expires = DateTime.UtcNow.AddMinutes(20),
				SigningCredentials = new SigningCredentials(
					new SymmetricSecurityKey(secretKeyBytes),
					SecurityAlgorithms.HmacSha256Signature)
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

		public async Task<TokenModel> RenewToken(RefreshTokenDTO model)
		{
			var jwtTokenHandler = new JwtSecurityTokenHandler();
			var secretKeyBytes = Encoding.UTF8.GetBytes(_configuration["AppSettings:SecretKey"]);
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
				// Xác thực JWT cũ
				var tokenInVerification = jwtTokenHandler.ValidateToken(
					model.AccessToken,
					tokenValidateParam,
					out var validatedToken);

				// Kiểm tra thuật toán
				if (validatedToken is JwtSecurityToken jwtSecurityToken)
				{
					var result = jwtSecurityToken.Header.Alg.Equals(
						SecurityAlgorithms.HmacSha256,
						StringComparison.InvariantCultureIgnoreCase);
					if (!result)
					{
						return null;
					}
				}

				// Kiểm tra refresh token trong db
				var storedToken = await _context.RefreshTokens
					.FirstOrDefaultAsync(x => x.Token == model.RefreshToken);

				if (storedToken == null)
					return null;

				// Kiểm tra refresh token đã sử dụng/thu hồi chưa
				if (storedToken.IsUsed || storedToken.IsRevoked)
					return null;

				// Kiểm tra refresh token còn hạn không
				if (storedToken.ExpiredAt < DateTime.UtcNow)
					return null;

				// Lấy thông tin user
				var taiKhoan = await _context.TaiKhoans
					.FirstOrDefaultAsync(x => x.MaTK == storedToken.UserId);

				if (taiKhoan == null)
					return null;

				// Đánh dấu token cũ đã sử dụng
				storedToken.IsUsed = true;
				_context.RefreshTokens.Update(storedToken);
				await _context.SaveChangesAsync();

				// Tạo token mới
				return await GenerateTokens(taiKhoan);
			}
			catch
			{
				return null;
			}
		}

		public async Task<LoginResponse> Login(LoginDTO loginDTO)
		{
			var taiKhoan = await _context.TaiKhoans
				.Include(x => x.QuyenTaiKhoan)
				.FirstOrDefaultAsync(x => x.TenDangNhap == loginDTO.TenDangNhap);

			if (taiKhoan == null || taiKhoan.MatKhau != loginDTO.MatKhau)
			{
				return new LoginResponse
				{
					Success = false,
					Message = "Tài khoản hoặc mật khẩu không đúng"
				};
			}

			var tokens = await GenerateTokens(taiKhoan);

			return new LoginResponse
			{
				Success = true,
				Message = "Đăng nhập thành công",
				MaTK = taiKhoan.MaTK,
				TenDangNhap = taiKhoan.TenDangNhap,
				MaQuyen = taiKhoan.MaQuyen,
				TenQuyen = taiKhoan.QuyenTaiKhoan?.TenQuyen,
				Token = tokens.AccessToken,
				RefreshToken = tokens.RefreshToken
			};
		}

		public Task<bool> CheckExistUsername(string username)
		{
			throw new NotImplementedException();
		}

		// ...existing code...
	}
}