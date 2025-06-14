using Microsoft.EntityFrameworkCore;
using WebDuLich.Data;
using WebDuLich.Interfaces.dto;
using WebDuLich.Interfaces.IRepositories;

namespace WebDuLich.Interfaces.Repositories
{
    public class TinhRepository : ITinhRepository
    {
        private readonly MyDbContext _context;

        public TinhRepository(MyDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<TinhDTO>> GetAllTinhAsync()
        {
            return await _context.Tinhs
                .Select(t => new TinhDTO
                {
                    MaTinh = t.MaTinh,
                    TenTinh = t.TenTinh,
                    MaMien = t.MaMien
                })
                .ToListAsync();
        }

        public async Task<TinhDTO> GetTinhByIdAsync(int maTinh)
        {
            return await _context.Tinhs
                .Where(t => t.MaTinh == maTinh)
                .Select(t => new TinhDTO
                {
                    MaTinh = t.MaTinh,
                    TenTinh = t.TenTinh,
                    MaMien = t.MaMien
                })
                .FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<TinhDTO>> GetTinhByMienAsync(int maMien)
        {
            return await _context.Tinhs
                .Where(t => t.MaMien == maMien)
                .Select(t => new TinhDTO
                {
                    MaTinh = t.MaTinh,
                    TenTinh = t.TenTinh,
                    MaMien = t.MaMien
                })
                .ToListAsync();
        }

        public async Task<TinhDTO> CreateTinhAsync(TinhDTO tinhDTO)
        {
            var tinh = new Tinh
            {
                TenTinh = tinhDTO.TenTinh,
                MaMien = tinhDTO.MaMien
            };

            _context.Tinhs.Add(tinh);
            await _context.SaveChangesAsync();

            return new TinhDTO
            {
                MaTinh = tinh.MaTinh,
                TenTinh = tinh.TenTinh,
                MaMien = tinh.MaMien
            };
        }

        public async Task<TinhDTO> UpdateTinhAsync(int maTinh, TinhUpdateDTO tinhDTO)
        {
            var tinh = await _context.Tinhs.FindAsync(maTinh);
            if (tinh == null)
                return null;

            tinh.TenTinh = tinhDTO.TenTinh;
            tinh.MaMien = tinhDTO.MaMien;

            await _context.SaveChangesAsync();

            return new TinhDTO
            {
                MaTinh = tinh.MaTinh,
                TenTinh = tinh.TenTinh,
                MaMien = tinh.MaMien
            };
        }

        public async Task<bool> DeleteTinhAsync(int maTinh)
        {
            var tinh = await _context.Tinhs.FindAsync(maTinh);
            if (tinh == null)
                return false;

            _context.Tinhs.Remove(tinh);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> TinhExistsAsync(int maTinh)
        {
            return await _context.Tinhs.AnyAsync(t => t.MaTinh == maTinh);
        }
    }
}