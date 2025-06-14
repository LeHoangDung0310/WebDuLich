using Microsoft.EntityFrameworkCore;
using WebDuLich.Data;
using WebDuLich.Interfaces.dto;
using WebDuLich.Interfaces.IRepositories;

namespace WebDuLich.Interfaces.Repositories
{
    public class MienRepository : IMienRepository
    {
        private readonly MyDbContext _context;

        public MienRepository(MyDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<MienDTO>> GetAllMienAsync()
        {
            return await _context.Miens
                .Select(m => new MienDTO
                {
                    MaMien = m.MaMien,
                    TenMien = m.TenMien
                })
                .ToListAsync();
        }

        public async Task<MienDTO> GetMienByIdAsync(int maMien)
        {
            return await _context.Miens
                .Where(m => m.MaMien == maMien)
                .Select(m => new MienDTO
                {
                    MaMien = m.MaMien,
                    TenMien = m.TenMien
                })
                .FirstOrDefaultAsync();
        }

        public async Task<MienDTO> CreateMienAsync(MienDTO mienDTO)
        {
            var mien = new Mien
            {
                TenMien = mienDTO.TenMien
            };

            _context.Miens.Add(mien);
            await _context.SaveChangesAsync();

            return new MienDTO
            {
                MaMien = mien.MaMien,
                TenMien = mien.TenMien
            };
        }

        public async Task<MienDTO> UpdateMienAsync(int maMien, MienUpdateDTO mienDTO)
        {
            var mien = await _context.Miens.FindAsync(maMien);
            if (mien == null)
                return null;

            mien.TenMien = mienDTO.TenMien;
            await _context.SaveChangesAsync();

            return new MienDTO
            {
                MaMien = mien.MaMien,
                TenMien = mien.TenMien
            };
        }

        public async Task<bool> DeleteMienAsync(int maMien)
        {
            var mien = await _context.Miens.FindAsync(maMien);
            if (mien == null)
                return false;

            _context.Miens.Remove(mien);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> MienExistsAsync(int maMien)
        {
            return await _context.Miens.AnyAsync(m => m.MaMien == maMien);
        }
    }
}