using Microsoft.EntityFrameworkCore;
using WebDuLich.Data;
using WebDuLich.Interfaces.dto;
using WebDuLich.Interfaces.IRepositories;

namespace WebDuLich.Interfaces.Repositories
{
    public class TaiKhoanRepository : ITaiKhoanRepository
    {
        private readonly MyDbContext _context;

        public TaiKhoanRepository(MyDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<TaiKhoanDTO>> GetAllTaiKhoanAsync()
        {
            return await _context.TaiKhoans
                .Select(t => new TaiKhoanDTO
                {
                    MaTK = t.MaTK,
                    TenDangNhap = t.TenDangNhap,
                    MatKhau = t.MatKhau,
                    MaQuyen = t.MaQuyen
                })
                .ToListAsync();
        }

        public async Task<TaiKhoanDTO> GetTaiKhoanByIdAsync(string maTK)
        {
            return await _context.TaiKhoans
                .Where(t => t.MaTK == maTK)
                .Select(t => new TaiKhoanDTO
                {
                    MaTK = t.MaTK,
                    TenDangNhap = t.TenDangNhap,
                    MatKhau = t.MatKhau,
                    MaQuyen = t.MaQuyen
                })
                .FirstOrDefaultAsync();
        }

        public async Task<TaiKhoanDTO> CreateTaiKhoanAsync(TaiKhoanDTO taiKhoanDTO)
        {
            var taiKhoan = new TaiKhoan
            {
                MaTK = taiKhoanDTO.MaTK,
                TenDangNhap = taiKhoanDTO.TenDangNhap,
                MatKhau = BCrypt.Net.BCrypt.HashPassword(taiKhoanDTO.MatKhau),
                MaQuyen = taiKhoanDTO.MaQuyen
            };

            _context.TaiKhoans.Add(taiKhoan);
            await _context.SaveChangesAsync();

            return new TaiKhoanDTO
            {
                MaTK = taiKhoan.MaTK,
                TenDangNhap = taiKhoan.TenDangNhap,
                MatKhau = taiKhoan.MatKhau,
                MaQuyen = taiKhoan.MaQuyen
            };
        }

        public async Task<TaiKhoanDTO> UpdateTaiKhoanAsync(string maTK, TaiKhoanUpdateDTO taiKhoanDTO)
        {
            var taiKhoan = await _context.TaiKhoans.FindAsync(maTK);
            if (taiKhoan == null)
                return null;

            taiKhoan.TenDangNhap = taiKhoanDTO.TenDangNhap;
            if (!string.IsNullOrEmpty(taiKhoanDTO.MatKhau))
            {
                taiKhoan.MatKhau = BCrypt.Net.BCrypt.HashPassword(taiKhoanDTO.MatKhau);
            }
            taiKhoan.MaQuyen = taiKhoanDTO.MaQuyen;

            await _context.SaveChangesAsync();

            return new TaiKhoanDTO
            {
                MaTK = taiKhoan.MaTK,
                TenDangNhap = taiKhoan.TenDangNhap,
                MatKhau = taiKhoan.MatKhau,
                MaQuyen = taiKhoan.MaQuyen
            };
        }

        public async Task<bool> DeleteTaiKhoanAsync(string maTK)
        {
            var taiKhoan = await _context.TaiKhoans.FindAsync(maTK);
            if (taiKhoan == null)
                return false;

            _context.TaiKhoans.Remove(taiKhoan);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> TaiKhoanExistsAsync(string maTK)
        {
            return await _context.TaiKhoans.AnyAsync(t => t.MaTK == maTK);
        }
    }
}
