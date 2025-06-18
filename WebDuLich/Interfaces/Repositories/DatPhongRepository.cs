using Microsoft.EntityFrameworkCore;
using WebDuLich.Data;
using WebDuLich.Interfaces.dto;
using WebDuLich.Interfaces.IRepositories;

namespace WebDuLich.Interfaces.Repositories
{
    public class DatPhongRepository : IDatPhongRepository
    {
        private readonly MyDbContext _context;

        public DatPhongRepository(MyDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<DatPhongDTO>> GetAllDatPhongAsync()
        {
            return await _context.DatPhongs
                .Select(d => new DatPhongDTO
                {
                    MaDatPhong = d.MaDatPhong,
                    MaNguoiDung = d.MaNguoiDung,
                    MaPhong = d.MaPhong,
                    NgayNhanPhong = d.NgayNhanPhong,
                    NgayTraPhong = d.NgayTraPhong,
                    TrangThai = d.TrangThai,
                    ThoiGianDat = d.ThoiGianDat
                })
                .ToListAsync();
        }

        public async Task<DatPhongDTO> GetDatPhongByIdAsync(int maDatPhong)
        {
            return await _context.DatPhongs
                .Where(d => d.MaDatPhong == maDatPhong)
                .Select(d => new DatPhongDTO
                {
                    MaDatPhong = d.MaDatPhong,
                    MaNguoiDung = d.MaNguoiDung,
                    MaPhong = d.MaPhong,
                    NgayNhanPhong = d.NgayNhanPhong,
                    NgayTraPhong = d.NgayTraPhong,
                    TrangThai = d.TrangThai,
                    ThoiGianDat = d.ThoiGianDat
                })
                .FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<DatPhongDTO>> GetDatPhongByNguoiDungAsync(string maNguoiDung)
        {
            return await _context.DatPhongs
                .Where(d => d.MaNguoiDung == maNguoiDung)
                .Select(d => new DatPhongDTO
                {
                    MaDatPhong = d.MaDatPhong,
                    MaNguoiDung = d.MaNguoiDung,
                    MaPhong = d.MaPhong,
                    NgayNhanPhong = d.NgayNhanPhong,
                    NgayTraPhong = d.NgayTraPhong,
                    TrangThai = d.TrangThai,
                    ThoiGianDat = d.ThoiGianDat
                })
                .ToListAsync();
        }

        public async Task<IEnumerable<DatPhongDTO>> GetDatPhongByPhongAsync(int maPhong)
        {
            return await _context.DatPhongs
                .Where(d => d.MaPhong == maPhong)
                .Select(d => new DatPhongDTO
                {
                    MaDatPhong = d.MaDatPhong,
                    MaNguoiDung = d.MaNguoiDung,
                    MaPhong = d.MaPhong,
                    NgayNhanPhong = d.NgayNhanPhong,
                    NgayTraPhong = d.NgayTraPhong,
                    TrangThai = d.TrangThai,
                    ThoiGianDat = d.ThoiGianDat
                })
                .ToListAsync();
        }

        public async Task<bool> IsPhongAvailableAsync(int maPhong, DateTime ngayNhan, DateTime ngayTra)
        {
            return !await _context.DatPhongs
                .AnyAsync(d => d.MaPhong == maPhong &&
                              d.TrangThai != TrangThaiDatPhong.DaHuy &&
                              ((ngayNhan >= d.NgayNhanPhong && ngayNhan < d.NgayTraPhong) ||
                               (ngayTra > d.NgayNhanPhong && ngayTra <= d.NgayTraPhong) ||
                               (ngayNhan <= d.NgayNhanPhong && ngayTra >= d.NgayTraPhong)));
        }

        public async Task<DatPhongDTO> CreateDatPhongAsync(string maNguoiDung, DatPhongCreateDTO datPhongDTO)
        {
            var datPhong = new DatPhong
            {
                MaNguoiDung = maNguoiDung,
                MaPhong = datPhongDTO.MaPhong,
                NgayNhanPhong = datPhongDTO.NgayNhanPhong,
                NgayTraPhong = datPhongDTO.NgayTraPhong,
                TrangThai = TrangThaiDatPhong.ChoXacNhan,
                ThoiGianDat = DateTime.Now
            };

            _context.DatPhongs.Add(datPhong);
            await _context.SaveChangesAsync();

            return new DatPhongDTO
            {
                MaDatPhong = datPhong.MaDatPhong,
                MaNguoiDung = datPhong.MaNguoiDung,
                MaPhong = datPhong.MaPhong,
                NgayNhanPhong = datPhong.NgayNhanPhong,
                NgayTraPhong = datPhong.NgayTraPhong,
                TrangThai = datPhong.TrangThai,
                ThoiGianDat = datPhong.ThoiGianDat
            };
        }

        public async Task<DatPhongDTO> UpdateDatPhongAsync(int maDatPhong, DatPhongUpdateDTO datPhongDTO)
        {
            var datPhong = await _context.DatPhongs.FindAsync(maDatPhong);
            if (datPhong == null)
                return null;

            datPhong.TrangThai = datPhongDTO.TrangThai;
            datPhong.NgayNhanPhong = datPhongDTO.NgayNhanPhong;
            datPhong.NgayTraPhong = datPhongDTO.NgayTraPhong;

            await _context.SaveChangesAsync();

            return new DatPhongDTO
            {
                MaDatPhong = datPhong.MaDatPhong,
                MaNguoiDung = datPhong.MaNguoiDung,
                MaPhong = datPhong.MaPhong,
                NgayNhanPhong = datPhong.NgayNhanPhong,
                NgayTraPhong = datPhong.NgayTraPhong,
                TrangThai = datPhong.TrangThai,
                ThoiGianDat = datPhong.ThoiGianDat
            };
        }

        public async Task<bool> DeleteDatPhongAsync(int maDatPhong)
        {
            var datPhong = await _context.DatPhongs.FindAsync(maDatPhong);
            if (datPhong == null)
                return false;

            _context.DatPhongs.Remove(datPhong);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DatPhongExistsAsync(int maDatPhong)
        {
            return await _context.DatPhongs.AnyAsync(d => d.MaDatPhong == maDatPhong);
        }
    }
}