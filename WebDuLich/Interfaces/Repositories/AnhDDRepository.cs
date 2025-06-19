using Microsoft.EntityFrameworkCore;
using WebDuLich.Data;
using WebDuLich.Interfaces.dto;
using WebDuLich.Interfaces.IRepositories;

namespace WebDuLich.Interfaces.Repositories
{
    public class AnhDDRepository : IAnhDDRepository
    {
        private readonly MyDbContext _context;

        public AnhDDRepository(MyDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<AnhDDDTO>> GetAllAnhDDAsync()
        {
            return await _context.HinhAnhDiaDiems
                .Select(a => new AnhDDDTO
                {
                    MaAnh = a.MaAnh,
                    MaDiaDiem = a.MaDiaDiem,
                    DuongDanAnh = a.DuongDanAnh
                })
                .ToListAsync();
        }

        public async Task<AnhDDDTO> GetAnhDDByIdAsync(int maAnh)
        {
            return await _context.HinhAnhDiaDiems
                .Where(a => a.MaAnh == maAnh)
                .Select(a => new AnhDDDTO
                {
                    MaAnh = a.MaAnh,
                    MaDiaDiem = a.MaDiaDiem,
                    DuongDanAnh = a.DuongDanAnh
                })
                .FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<AnhDDDTO>> GetAnhDDByDiaDiemAsync(string maDiaDiem)
        {
            return await _context.HinhAnhDiaDiems
                .Where(a => a.MaDiaDiem == maDiaDiem)
                .Select(a => new AnhDDDTO
                {
                    MaAnh = a.MaAnh,
                    MaDiaDiem = a.MaDiaDiem,
                    DuongDanAnh = a.DuongDanAnh
                })
                .ToListAsync();
        }

        public async Task<AnhDDDTO> CreateAnhDDAsync(AnhDDDTO anhDDDTO)
        {
            var anhDD = new HinhAnhDiaDiem
            {
                MaDiaDiem = anhDDDTO.MaDiaDiem,
                DuongDanAnh = anhDDDTO.DuongDanAnh
            };

            _context.HinhAnhDiaDiems.Add(anhDD);
            await _context.SaveChangesAsync();

            return new AnhDDDTO
            {
                MaAnh = anhDD.MaAnh,
                MaDiaDiem = anhDD.MaDiaDiem,
                DuongDanAnh = anhDD.DuongDanAnh
            };
        }

        public async Task<AnhDDDTO> UpdateAnhDDAsync(int maAnh, AnhDDUpdateDTO anhDDDTO)
        {
            var anhDD = await _context.HinhAnhDiaDiems.FindAsync(maAnh);
            if (anhDD == null)
                return null;

            anhDD.MaDiaDiem = anhDDDTO.MaDiaDiem;
            anhDD.DuongDanAnh = anhDDDTO.DuongDanAnh;
            await _context.SaveChangesAsync();

            return new AnhDDDTO
            {
                MaAnh = anhDD.MaAnh,
                MaDiaDiem = anhDD.MaDiaDiem,
                DuongDanAnh = anhDD.DuongDanAnh
            };
        }

        public async Task<bool> DeleteAnhDDAsync(int maAnh)
        {
            var anhDD = await _context.HinhAnhDiaDiems.FindAsync(maAnh);
            if (anhDD == null)
                return false;

            _context.HinhAnhDiaDiems.Remove(anhDD);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> AnhDDExistsAsync(int maAnh)
        {
            return await _context.HinhAnhDiaDiems.AnyAsync(a => a.MaAnh == maAnh);
        }
    }
}
