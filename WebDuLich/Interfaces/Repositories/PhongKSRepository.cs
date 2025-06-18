using Microsoft.EntityFrameworkCore;
using WebDuLich.Data;
using WebDuLich.Interfaces.dto;
using WebDuLich.Interfaces.IRepositories;

namespace WebDuLich.Interfaces.Repositories
{
    public class PhongKSRepository : IPhongKSRepository
    {
        private readonly MyDbContext _context;

        public PhongKSRepository(MyDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<PhongKSDTO>> GetAllPhongKSAsync()
        {
            return await _context.PhongKhachSans
                .Select(p => new PhongKSDTO
                {
                    MaPhong = p.MaPhong,
                    MaKhachSan = p.MaKhachSan,
                    LoaiPhong = p.LoaiPhong,
                    Gia = p.Gia,
                    SucChua = p.SucChua,
                    TrangThai = p.TrangThai,
                    MoTa = p.MoTa,
                    DienTich = p.DienTich,
                    TienIch = p.TienIch
                })
                .ToListAsync();
        }

        public async Task<PhongKSDTO> GetPhongKSByIdAsync(int maPhong)
        {
            return await _context.PhongKhachSans
                .Where(p => p.MaPhong == maPhong)
                .Select(p => new PhongKSDTO
                {
                    MaPhong = p.MaPhong,
                    MaKhachSan = p.MaKhachSan,
                    LoaiPhong = p.LoaiPhong,
                    Gia = p.Gia,
                    SucChua = p.SucChua,
                    TrangThai = p.TrangThai,
                    MoTa = p.MoTa,
                    DienTich = p.DienTich,
                    TienIch = p.TienIch
                })
                .FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<PhongKSDTO>> GetPhongKSByKhachSanAsync(string maKS)
        {
            return await _context.PhongKhachSans
                .Where(p => p.MaKhachSan == maKS)
                .Select(p => new PhongKSDTO
                {
                    MaPhong = p.MaPhong,
                    MaKhachSan = p.MaKhachSan,
                    LoaiPhong = p.LoaiPhong,
                    Gia = p.Gia,
                    SucChua = p.SucChua,
                    TrangThai = p.TrangThai,
                    MoTa = p.MoTa,
                    DienTich = p.DienTich,
                    TienIch = p.TienIch
                })
                .ToListAsync();
        }

        public async Task<PhongKSDTO> CreatePhongKSAsync(PhongKSDTO phongKSDTO)
        {
            var phongKS = new PhongKhachSan
            {
                MaKhachSan = phongKSDTO.MaKhachSan,
                LoaiPhong = phongKSDTO.LoaiPhong,
                Gia = phongKSDTO.Gia,
                SucChua = phongKSDTO.SucChua,
                TrangThai = phongKSDTO.TrangThai,
                MoTa = phongKSDTO.MoTa,
                DienTich = phongKSDTO.DienTich,
                TienIch = phongKSDTO.TienIch
            };

            _context.PhongKhachSans.Add(phongKS);
            await _context.SaveChangesAsync();

            return new PhongKSDTO
            {
                MaPhong = phongKS.MaPhong,
                MaKhachSan = phongKS.MaKhachSan,
                LoaiPhong = phongKS.LoaiPhong,
                Gia = phongKS.Gia,
                SucChua = phongKS.SucChua,
                TrangThai = phongKS.TrangThai,
                MoTa = phongKS.MoTa,
                DienTich = phongKS.DienTich,
                TienIch = phongKS.TienIch
            };
        }

        public async Task<PhongKSDTO> UpdatePhongKSAsync(int maPhong, PhongKSUpdateDTO phongKSDTO)
        {
            var phongKS = await _context.PhongKhachSans.FindAsync(maPhong);
            if (phongKS == null)
                return null;

            phongKS.LoaiPhong = phongKSDTO.LoaiPhong;
            phongKS.Gia = phongKSDTO.Gia;
            phongKS.SucChua = phongKSDTO.SucChua;
            phongKS.TrangThai = phongKSDTO.TrangThai;
            phongKS.MoTa = phongKSDTO.MoTa;
            phongKS.DienTich = phongKSDTO.DienTich;
            phongKS.TienIch = phongKSDTO.TienIch;

            await _context.SaveChangesAsync();

            return new PhongKSDTO
            {
                MaPhong = phongKS.MaPhong,
                MaKhachSan = phongKS.MaKhachSan,
                LoaiPhong = phongKS.LoaiPhong,
                Gia = phongKS.Gia,
                SucChua = phongKS.SucChua,
                TrangThai = phongKS.TrangThai,
                MoTa = phongKS.MoTa,
                DienTich = phongKS.DienTich,
                TienIch = phongKS.TienIch
            };
        }

        public async Task<bool> DeletePhongKSAsync(int maPhong)
        {
            var phongKS = await _context.PhongKhachSans.FindAsync(maPhong);
            if (phongKS == null)
                return false;

            _context.PhongKhachSans.Remove(phongKS);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> PhongKSExistsAsync(int maPhong)
        {
            return await _context.PhongKhachSans.AnyAsync(p => p.MaPhong == maPhong);
        }
    }
}