using Microsoft.EntityFrameworkCore;
using WebDuLich.Data;
using WebDuLich.Interfaces.dto;
using WebDuLich.Interfaces.IRepositories;

namespace WebDuLich.Interfaces.Repositories
{
    public class KhachSanRepository : IKhachSanRepository
    {
        private readonly MyDbContext _context;

        public KhachSanRepository(MyDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<KhachSanDTO>> GetAllKhachSanAsync()
        {
            return await _context.KhachSans
                .Select(k => new KhachSanDTO
                {
                    MaKs = k.MaKs,
                    Ten = k.Ten,
                    MoTa = k.MoTa,
                    DiaChi = k.DiaChi,
                    DienThoai = k.DienThoai,
                    Email = k.Email,
                    MaTinh = k.MaTinh
                })
                .ToListAsync();
        }

        public async Task<KhachSanDTO> GetKhachSanByIdAsync(string maKs)
        {
            return await _context.KhachSans
                .Where(k => k.MaKs == maKs)
                .Select(k => new KhachSanDTO
                {
                    MaKs = k.MaKs,
                    Ten = k.Ten,
                    MoTa = k.MoTa,
                    DiaChi = k.DiaChi,
                    DienThoai = k.DienThoai,
                    Email = k.Email,
                    MaTinh = k.MaTinh
                })
                .FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<KhachSanDTO>> GetKhachSanByTinhAsync(int maTinh)
        {
            return await _context.KhachSans
                .Where(k => k.MaTinh == maTinh)
                .Select(k => new KhachSanDTO
                {
                    MaKs = k.MaKs,
                    Ten = k.Ten,
                    MoTa = k.MoTa,
                    DiaChi = k.DiaChi,
                    DienThoai = k.DienThoai,
                    Email = k.Email,
                    MaTinh = k.MaTinh
                })
                .ToListAsync();
        }

        public async Task<KhachSanDTO> CreateKhachSanAsync(KhachSanDTO khachSanDTO)
        {
            var khachSan = new KhachSan
            {
                MaKs = Guid.NewGuid().ToString().Substring(0, 10),
                Ten = khachSanDTO.Ten,
                MoTa = khachSanDTO.MoTa,
                DiaChi = khachSanDTO.DiaChi,
                DienThoai = khachSanDTO.DienThoai,
                Email = khachSanDTO.Email,
                MaTinh = khachSanDTO.MaTinh
            };

            _context.KhachSans.Add(khachSan);
            await _context.SaveChangesAsync();

            return new KhachSanDTO
            {
                MaKs = khachSan.MaKs,
                Ten = khachSan.Ten,
                MoTa = khachSan.MoTa,
                DiaChi = khachSan.DiaChi,
                DienThoai = khachSan.DienThoai,
                Email = khachSan.Email,
                MaTinh = khachSan.MaTinh
            };
        }

        public async Task<KhachSanDTO> UpdateKhachSanAsync(string maKs, KhachSanUpdateDTO khachSanDTO)
        {
            var khachSan = await _context.KhachSans.FindAsync(maKs);
            if (khachSan == null)
                return null;

            khachSan.Ten = khachSanDTO.Ten;
            khachSan.MoTa = khachSanDTO.MoTa;
            khachSan.DiaChi = khachSanDTO.DiaChi;
            khachSan.DienThoai = khachSanDTO.DienThoai;
            khachSan.Email = khachSanDTO.Email;
            khachSan.MaTinh = khachSanDTO.MaTinh;

            await _context.SaveChangesAsync();

            return new KhachSanDTO
            {
                MaKs = khachSan.MaKs,
                Ten = khachSan.Ten,
                MoTa = khachSan.MoTa,
                DiaChi = khachSan.DiaChi,
                DienThoai = khachSan.DienThoai,
                Email = khachSan.Email,
                MaTinh = khachSan.MaTinh
            };
        }

        public async Task<bool> DeleteKhachSanAsync(string maKs)
        {
            var khachSan = await _context.KhachSans.FindAsync(maKs);
            if (khachSan == null)
                return false;

            _context.KhachSans.Remove(khachSan);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> KhachSanExistsAsync(string maKs)
        {
            return await _context.KhachSans.AnyAsync(k => k.MaKs == maKs);
        }
    }
}