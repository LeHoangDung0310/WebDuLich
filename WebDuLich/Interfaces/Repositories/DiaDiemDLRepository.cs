using Microsoft.EntityFrameworkCore;
using WebDuLich.Data;
using WebDuLich.Interfaces.dto;
using WebDuLich.Interfaces.IRepositories;

namespace WebDuLich.Interfaces.Repositories
{
    public class DiaDiemDLRepository : IDiaDiemDLRepository
    {
        private readonly MyDbContext _context;

        public DiaDiemDLRepository(MyDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<DiaDiemDLDTO>> GetAllDiaDiemDLAsync()
        {
            return await _context.DiaDiemDuLichs
                .Select(d => new DiaDiemDLDTO
                {
                    MaDiaDiem = d.MaDiaDiem,
                    MaTaiKhoan = d.MaTaiKhoan,
                    TieuDe = d.TieuDe,
                    MoTa = d.MoTa,
                    MaTinh = d.MaTinh,
                    NgayTao = d.NgayTao
                })
                .ToListAsync();
        }

        public async Task<DiaDiemDLDTO> GetDiaDiemDLByIdAsync(string maDiaDiem)
        {
            return await _context.DiaDiemDuLichs
                .Where(d => d.MaDiaDiem == maDiaDiem)
                .Select(d => new DiaDiemDLDTO
                {
                    MaDiaDiem = d.MaDiaDiem,
                    MaTaiKhoan = d.MaTaiKhoan,
                    TieuDe = d.TieuDe,
                    MoTa = d.MoTa,
                    MaTinh = d.MaTinh,
                    NgayTao = d.NgayTao
                })
                .FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<DiaDiemDLDTO>> GetDiaDiemDLByTinhAsync(int maTinh)
        {
            return await _context.DiaDiemDuLichs
                .Where(d => d.MaTinh == maTinh)
                .Select(d => new DiaDiemDLDTO
                {
                    MaDiaDiem = d.MaDiaDiem,
                    MaTaiKhoan = d.MaTaiKhoan,
                    TieuDe = d.TieuDe,
                    MoTa = d.MoTa,
                    MaTinh = d.MaTinh,
                    NgayTao = d.NgayTao
                })
                .ToListAsync();
        }

        public async Task<IEnumerable<DiaDiemDLDTO>> GetDiaDiemDLByTaiKhoanAsync(string maTaiKhoan)
        {
            return await _context.DiaDiemDuLichs
                .Where(d => d.MaTaiKhoan == maTaiKhoan)
                .Select(d => new DiaDiemDLDTO
                {
                    MaDiaDiem = d.MaDiaDiem,
                    MaTaiKhoan = d.MaTaiKhoan,
                    TieuDe = d.TieuDe,
                    MoTa = d.MoTa,
                    MaTinh = d.MaTinh,
                    NgayTao = d.NgayTao
                })
                .ToListAsync();
        }

        public async Task<DiaDiemDLDTO> CreateDiaDiemDLAsync(string maTaiKhoan, DiaDiemDLCreateDTO diaDiemDLDTO)
        {
            var diaDiemDL = new DiaDiemDuLich
            {
                MaDiaDiem = Guid.NewGuid().ToString().Substring(0, 10),
                MaTaiKhoan = maTaiKhoan,
                TieuDe = diaDiemDLDTO.TieuDe,
                MoTa = diaDiemDLDTO.MoTa,
                MaTinh = diaDiemDLDTO.MaTinh,
                NgayTao = DateTime.Now
            };

            _context.DiaDiemDuLichs.Add(diaDiemDL);
            await _context.SaveChangesAsync();

            return new DiaDiemDLDTO
            {
                MaDiaDiem = diaDiemDL.MaDiaDiem,
                MaTaiKhoan = diaDiemDL.MaTaiKhoan,
                TieuDe = diaDiemDL.TieuDe,
                MoTa = diaDiemDL.MoTa,
                MaTinh = diaDiemDL.MaTinh,
                NgayTao = diaDiemDL.NgayTao
            };
        }

        public async Task<DiaDiemDLDTO> UpdateDiaDiemDLAsync(string maDiaDiem, DiaDiemDLUpdateDTO diaDiemDLDTO)
        {
            var diaDiemDL = await _context.DiaDiemDuLichs.FindAsync(maDiaDiem);
            if (diaDiemDL == null)
                return null;

            diaDiemDL.TieuDe = diaDiemDLDTO.TieuDe;
            diaDiemDL.MoTa = diaDiemDLDTO.MoTa;
            diaDiemDL.MaTinh = diaDiemDLDTO.MaTinh;

            await _context.SaveChangesAsync();

            return new DiaDiemDLDTO
            {
                MaDiaDiem = diaDiemDL.MaDiaDiem,
                MaTaiKhoan = diaDiemDL.MaTaiKhoan,
                TieuDe = diaDiemDL.TieuDe,
                MoTa = diaDiemDL.MoTa,
                MaTinh = diaDiemDL.MaTinh,
                NgayTao = diaDiemDL.NgayTao
            };
        }

        public async Task<bool> DeleteDiaDiemDLAsync(string maDiaDiem)
        {
            var diaDiemDL = await _context.DiaDiemDuLichs.FindAsync(maDiaDiem);
            if (diaDiemDL == null)
                return false;

            _context.DiaDiemDuLichs.Remove(diaDiemDL);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DiaDiemDLExistsAsync(string maDiaDiem)
        {
            return await _context.DiaDiemDuLichs.AnyAsync(d => d.MaDiaDiem == maDiaDiem);
        }
    }
}