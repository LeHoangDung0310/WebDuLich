using Microsoft.EntityFrameworkCore;
using WebDuLich.Data;
using WebDuLich.Interfaces.dto;
using WebDuLich.Interfaces.IRepositories;

namespace WebDuLich.Interfaces.Repositories
{
    public class QuyenTKRepository : IQuyenTKRepository
    {
        private readonly MyDbContext _context;

        public QuyenTKRepository(MyDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<QuyenTKDTO>> GetAllQuyenTKAsync()
        {
            return await _context.QuyenTaiKhoans
                .Select(q => new QuyenTKDTO
                {
                    MaQuyen = q.MaQuyen,
                    TenQuyen = q.TenQuyen
                })
                .ToListAsync();
        }

        public async Task<QuyenTKDTO> GetQuyenTKByIdAsync(string maQuyen)
        {
            return await _context.QuyenTaiKhoans
                .Where(q => q.MaQuyen == maQuyen)
                .Select(q => new QuyenTKDTO
                {
                    MaQuyen = q.MaQuyen,
                    TenQuyen = q.TenQuyen
                })
                .FirstOrDefaultAsync();
        }

        public async Task<QuyenTKDTO> CreateQuyenTKAsync(QuyenTKDTO quyenTKDTO)
        {
            var quyenTK = new QuyenTaiKhoan
            {
                MaQuyen = Guid.NewGuid().ToString().Substring(0, 10),
                TenQuyen = quyenTKDTO.TenQuyen
            };

            _context.QuyenTaiKhoans.Add(quyenTK);
            await _context.SaveChangesAsync();

            return new QuyenTKDTO
            {
                MaQuyen = quyenTK.MaQuyen,
                TenQuyen = quyenTK.TenQuyen
            };
        }

        public async Task<QuyenTKDTO> UpdateQuyenTKAsync(string maQuyen, QuyenTKUpdateDTO quyenTKDTO)
        {
            var quyenTK = await _context.QuyenTaiKhoans.FindAsync(maQuyen);
            if (quyenTK == null)
                return null;

            quyenTK.TenQuyen = quyenTKDTO.TenQuyen;
            await _context.SaveChangesAsync();

            return new QuyenTKDTO
            {
                MaQuyen = quyenTK.MaQuyen,
                TenQuyen = quyenTK.TenQuyen
            };
        }

        public async Task<bool> DeleteQuyenTKAsync(string maQuyen)
        {
            var quyenTK = await _context.QuyenTaiKhoans.FindAsync(maQuyen);
            if (quyenTK == null)
                return false;

            _context.QuyenTaiKhoans.Remove(quyenTK);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> QuyenTKExistsAsync(string maQuyen)
        {
            return await _context.QuyenTaiKhoans.AnyAsync(q => q.MaQuyen == maQuyen);
        }
    }
}