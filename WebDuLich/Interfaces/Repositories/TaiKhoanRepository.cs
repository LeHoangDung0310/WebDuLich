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

        public async Task<IEnumerable<TaiKhoan>> GetAllTaiKhoanAsync()
        {
            return await _context.TaiKhoans
                .Include(t => t.QuyenTaiKhoan)
                .ToListAsync();
        }

        public async Task<TaiKhoan> GetTaiKhoanByIdAsync(string maTK)
        {
            return await _context.TaiKhoans
                .Include(t => t.QuyenTaiKhoan)
                .FirstOrDefaultAsync(t => t.MaTK == maTK);
        }

        public async Task<TaiKhoan> CreateTaiKhoanAsync(TaiKhoanDTO taiKhoanDTO)
        {
            var taiKhoan = new TaiKhoan
            {
                MaTK = Guid.NewGuid().ToString().Substring(0, 10),
                TenDangNhap = taiKhoanDTO.TenDangNhap,
                MatKhau = BCrypt.Net.BCrypt.HashPassword(taiKhoanDTO.MatKhau),
                MaQuyen = taiKhoanDTO.MaQuyen
            };

            _context.TaiKhoans.Add(taiKhoan);
            await _context.SaveChangesAsync();
            return taiKhoan;
        }

        public async Task<TaiKhoan> UpdateTaiKhoanAsync(string maTK, TaiKhoanUpdateDTO taiKhoanDTO)
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
            return taiKhoan;
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
