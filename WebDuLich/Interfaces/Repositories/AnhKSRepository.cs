using Microsoft.EntityFrameworkCore;
using WebDuLich.Data;
using WebDuLich.Interfaces.dto;
using WebDuLich.Interfaces.IRepositories;

namespace WebDuLich.Interfaces.Repositories
{
    public class AnhKSRepository : IAnhKSRepository
    {
        private readonly MyDbContext _context;

        public AnhKSRepository(MyDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<AnhKSDTO>> GetAllAnhKSAsync()
        {
            return await _context.HinhAnhKhachSans
                .Select(a => new AnhKSDTO
                {
                    MaAnh = a.MaAnh,
                    MaKhachSan = a.MaKhachSan,
                    DuongDanAnh = a.DuongDanAnh
                })
                .ToListAsync();
        }

        public async Task<AnhKSDTO> GetAnhKSByIdAsync(int maAnh)
        {
            return await _context.HinhAnhKhachSans
                .Where(a => a.MaAnh == maAnh)
                .Select(a => new AnhKSDTO
                {
                    MaAnh = a.MaAnh,
                    MaKhachSan = a.MaKhachSan,
                    DuongDanAnh = a.DuongDanAnh
                })
                .FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<AnhKSDTO>> GetAnhKSByKhachSanAsync(string maKS)
        {
            return await _context.HinhAnhKhachSans
                .Where(a => a.MaKhachSan == maKS)
                .Select(a => new AnhKSDTO
                {
                    MaAnh = a.MaAnh,
                    MaKhachSan = a.MaKhachSan,
                    DuongDanAnh = a.DuongDanAnh
                })
                .ToListAsync();
        }

        public async Task<AnhKSDTO> CreateAnhKSAsync(AnhKSDTO anhKSDTO)
        {
            var anhKS = new HinhAnhKhachSan
            {
                MaKhachSan = anhKSDTO.MaKhachSan,
                DuongDanAnh = anhKSDTO.DuongDanAnh
            };

            _context.HinhAnhKhachSans.Add(anhKS);
            await _context.SaveChangesAsync();

            return new AnhKSDTO
            {
                MaAnh = anhKS.MaAnh,
                MaKhachSan = anhKS.MaKhachSan,
                DuongDanAnh = anhKS.DuongDanAnh
            };
        }

        public async Task<AnhKSDTO> UpdateAnhKSAsync(int maAnh, AnhKSUpdateDTO anhKSDTO)
        {
            var anhKS = await _context.HinhAnhKhachSans.FindAsync(maAnh);
            if (anhKS == null)
                return null;

            anhKS.DuongDanAnh = anhKSDTO.DuongDanAnh;
            await _context.SaveChangesAsync();

            return new AnhKSDTO
            {
                MaAnh = anhKS.MaAnh,
                MaKhachSan = anhKS.MaKhachSan,
                DuongDanAnh = anhKS.DuongDanAnh
            };
        }

        public async Task<bool> DeleteAnhKSAsync(int maAnh)
        {
            var anhKS = await _context.HinhAnhKhachSans.FindAsync(maAnh);
            if (anhKS == null)
                return false;

            _context.HinhAnhKhachSans.Remove(anhKS);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> AnhKSExistsAsync(int maAnh)
        {
            return await _context.HinhAnhKhachSans.AnyAsync(a => a.MaAnh == maAnh);
        }
    }
}