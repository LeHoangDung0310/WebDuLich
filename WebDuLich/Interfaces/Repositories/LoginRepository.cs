using Microsoft.EntityFrameworkCore;
using WebDuLich.Data;
using WebDuLich.Interfaces.dto;
using WebDuLich.Interfaces.IRepositories;

namespace WebDuLich.Interfaces.Repositories
{
	public class LoginRepository : ILoginRepository
	{
		private readonly MyDbContext _context;

		public LoginRepository(MyDbContext context)
		{
			_context = context;
		}

		public async Task<bool> CheckExistUsername(string username)
		{
			return await _context.TaiKhoans.AnyAsync(x => x.TenDangNhap == username);
		}

		public async Task<LoginResponse> Login(LoginDTO loginDTO)
		{
			var taiKhoan = await _context.TaiKhoans
				.Include(x => x.QuyenTaiKhoan)
				.FirstOrDefaultAsync(x => x.TenDangNhap == loginDTO.TenDangNhap);

			if (taiKhoan == null)
			{
				return new LoginResponse
				{
					Success = false,
					Message = "Tai Khoan hoac mật khẩu không đúng"
				};
			}

			if (taiKhoan.MatKhau != loginDTO.MatKhau) // Trong thực tế nên mã hóa mật khẩu
			{
				return new LoginResponse
				{
					Success = false,
					Message = "Tai Khoan hoac mật khẩu không đúng"
				};
			}

			return new LoginResponse
			{
				Success = true,
				Message = "Đăng nhập thành công",
				MaTK = taiKhoan.MaTK,
				TenDangNhap = taiKhoan.TenDangNhap,
				MaQuyen = taiKhoan.MaQuyen,
				TenQuyen = taiKhoan.QuyenTaiKhoan?.TenQuyen
			};
		}
	}
}
