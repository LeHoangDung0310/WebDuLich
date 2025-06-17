using Microsoft.EntityFrameworkCore;
using WebDuLich.Data;
using WebDuLich.Interfaces.dto;
using WebDuLich.Interfaces.IRepositories;

namespace WebDuLich.Interfaces.Repositories
{
    public class AnhNHRepository : IAnhNHRepository
    {
        private readonly MyDbContext _context;

        public AnhNHRepository(MyDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<AnhNHDTO>> GetAllAnhNHAsync()
        {
            return await _context.HinhAnhNhaHangs
                .Select(a => new AnhNHDTO
                {
                    MaAnh = a.MaAnh,
                    MaNhaHang = a.MaNhaHang,
                    DuongDanAnh = a.DuongDanAnh
                })
                .ToListAsync();
        }

        public async Task<AnhNHDTO> GetAnhNHByIdAsync(int maAnh)
        {
            return await _context.HinhAnhNhaHangs
                .Where(a => a.MaAnh == maAnh)
                .Select(a => new AnhNHDTO
                {
                    MaAnh = a.MaAnh,
                    MaNhaHang = a.MaNhaHang,
                    DuongDanAnh = a.DuongDanAnh
                })
                .FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<AnhNHDTO>> GetAnhNHByNhaHangAsync(string maNH)
        {
            return await _context.HinhAnhNhaHangs
                .Where(a => a.MaNhaHang == maNH)
                .Select(a => new AnhNHDTO
                {
                    MaAnh = a.MaAnh,
                    MaNhaHang = a.MaNhaHang,
                    DuongDanAnh = a.DuongDanAnh
                })
                .ToListAsync();
        }

        public async Task<AnhNHDTO> CreateAnhNHAsync(AnhNHDTO anhNHDTO)
        {
            var anhNH = new HinhAnhNhaHang
            {
                MaNhaHang = anhNHDTO.MaNhaHang,
                DuongDanAnh = anhNHDTO.DuongDanAnh
            };

            _context.HinhAnhNhaHangs.Add(anhNH);
            await _context.SaveChangesAsync();

            return new AnhNHDTO
            {
                MaAnh = anhNH.MaAnh,
                MaNhaHang = anhNH.MaNhaHang,
                DuongDanAnh = anhNH.DuongDanAnh
            };
        }

        public async Task<AnhNHDTO> UpdateAnhNHAsync(int maAnh, AnhNHUpdateDTO anhNHDTO)
        {
            var anhNH = await _context.HinhAnhNhaHangs.FindAsync(maAnh);
            if (anhNH == null)
                return null;

            anhNH.MaNhaHang = anhNHDTO.MaNhaHang;
            anhNH.DuongDanAnh = anhNHDTO.DuongDanAnh;
            await _context.SaveChangesAsync();

            return new AnhNHDTO
            {
                MaAnh = anhNH.MaAnh,
                MaNhaHang = anhNH.MaNhaHang,
                DuongDanAnh = anhNH.DuongDanAnh
            };
        }

        public async Task<bool> DeleteAnhNHAsync(int maAnh)
        {
            var anhNH = await _context.HinhAnhNhaHangs.FindAsync(maAnh);
            if (anhNH == null)
                return false;

            _context.HinhAnhNhaHangs.Remove(anhNH);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> AnhNHExistsAsync(int maAnh)
        {
            return await _context.HinhAnhNhaHangs.AnyAsync(a => a.MaAnh == maAnh);
        }
    }
}