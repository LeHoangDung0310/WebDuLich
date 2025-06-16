using Microsoft.EntityFrameworkCore;
using WebDuLich.Data;
using WebDuLich.Interfaces.dto;
using WebDuLich.Interfaces.IRepositories;

namespace WebDuLich.Interfaces.Repositories
{
    public class NhaHangRepository : INhaHangRepository
    {
        private readonly MyDbContext _context;

        public NhaHangRepository(MyDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<NhaHangDTO>> GetAllNhaHangAsync()
        {
            return await _context.NhaHangs
                .Select(n => new NhaHangDTO
                {
                    MaNhaHang = n.MaNhaHang,
                    Ten = n.Ten,
                    MoTa = n.MoTa,
                    DiaChi = n.DiaChi,
                    SoDienThoai = n.SoDienThoai,
                    Email = n.Email,
                    MaTinh = n.MaTinh,
                    LoaiAmThuc = n.LoaiAmThuc,
                    ThoiGianMoCua = n.ThoiGianMoCua,
                    ThoiGianDongCua = n.ThoiGianDongCua
                })
                .ToListAsync();
        }

        public async Task<NhaHangDTO> GetNhaHangByIdAsync(string maNhaHang)
        {
            return await _context.NhaHangs
                .Where(n => n.MaNhaHang == maNhaHang)
                .Select(n => new NhaHangDTO
                {
                    MaNhaHang = n.MaNhaHang,
                    Ten = n.Ten,
                    MoTa = n.MoTa,
                    DiaChi = n.DiaChi,
                    SoDienThoai = n.SoDienThoai,
                    Email = n.Email,
                    MaTinh = n.MaTinh,
                    LoaiAmThuc = n.LoaiAmThuc,
                    ThoiGianMoCua = n.ThoiGianMoCua,
                    ThoiGianDongCua = n.ThoiGianDongCua
                })
                .FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<NhaHangDTO>> GetNhaHangByTinhAsync(int maTinh)
        {
            return await _context.NhaHangs
                .Where(n => n.MaTinh == maTinh)
                .Select(n => new NhaHangDTO
                {
                    MaNhaHang = n.MaNhaHang,
                    Ten = n.Ten,
                    MoTa = n.MoTa,
                    DiaChi = n.DiaChi,
                    SoDienThoai = n.SoDienThoai,
                    Email = n.Email,
                    MaTinh = n.MaTinh,
                    LoaiAmThuc = n.LoaiAmThuc,
                    ThoiGianMoCua = n.ThoiGianMoCua,
                    ThoiGianDongCua = n.ThoiGianDongCua
                })
                .ToListAsync();
        }

        public async Task<NhaHangDTO> CreateNhaHangAsync(NhaHangDTO nhaHangDTO)
        {
            var nhaHang = new NhaHang
            {
                MaNhaHang = Guid.NewGuid().ToString().Substring(0, 10),
                Ten = nhaHangDTO.Ten,
                MoTa = nhaHangDTO.MoTa,
                DiaChi = nhaHangDTO.DiaChi,
                SoDienThoai = nhaHangDTO.SoDienThoai,
                Email = nhaHangDTO.Email,
                MaTinh = nhaHangDTO.MaTinh,
                LoaiAmThuc = nhaHangDTO.LoaiAmThuc,
                ThoiGianMoCua = nhaHangDTO.ThoiGianMoCua,
                ThoiGianDongCua = nhaHangDTO.ThoiGianDongCua
            };

            _context.NhaHangs.Add(nhaHang);
            await _context.SaveChangesAsync();

            return new NhaHangDTO
            {
                MaNhaHang = nhaHang.MaNhaHang,
                Ten = nhaHang.Ten,
                MoTa = nhaHang.MoTa,
                DiaChi = nhaHang.DiaChi,
                SoDienThoai = nhaHang.SoDienThoai,
                Email = nhaHang.Email,
                MaTinh = nhaHang.MaTinh,
                LoaiAmThuc = nhaHang.LoaiAmThuc,
                ThoiGianMoCua = nhaHang.ThoiGianMoCua,
                ThoiGianDongCua = nhaHang.ThoiGianDongCua
            };
        }

        public async Task<NhaHangDTO> UpdateNhaHangAsync(string maNhaHang, NhaHangUpdateDTO nhaHangDTO)
        {
            var nhaHang = await _context.NhaHangs.FindAsync(maNhaHang);
            if (nhaHang == null)
                return null;

            nhaHang.Ten = nhaHangDTO.Ten;
            nhaHang.MoTa = nhaHangDTO.MoTa;
            nhaHang.DiaChi = nhaHangDTO.DiaChi;
            nhaHang.SoDienThoai = nhaHangDTO.SoDienThoai;
            nhaHang.Email = nhaHangDTO.Email;
            nhaHang.MaTinh = nhaHangDTO.MaTinh;
            nhaHang.LoaiAmThuc = nhaHangDTO.LoaiAmThuc;
            nhaHang.ThoiGianMoCua = nhaHangDTO.ThoiGianMoCua;
            nhaHang.ThoiGianDongCua = nhaHangDTO.ThoiGianDongCua;

            await _context.SaveChangesAsync();

            return new NhaHangDTO
            {
                MaNhaHang = nhaHang.MaNhaHang,
                Ten = nhaHang.Ten,
                MoTa = nhaHang.MoTa,
                DiaChi = nhaHang.DiaChi,
                SoDienThoai = nhaHang.SoDienThoai,
                Email = nhaHang.Email,
                MaTinh = nhaHang.MaTinh,
                LoaiAmThuc = nhaHang.LoaiAmThuc,
                ThoiGianMoCua = nhaHang.ThoiGianMoCua,
                ThoiGianDongCua = nhaHang.ThoiGianDongCua
            };
        }

        public async Task<bool> DeleteNhaHangAsync(string maNhaHang)
        {
            var nhaHang = await _context.NhaHangs.FindAsync(maNhaHang);
            if (nhaHang == null)
                return false;

            _context.NhaHangs.Remove(nhaHang);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> NhaHangExistsAsync(string maNhaHang)
        {
            return await _context.NhaHangs.AnyAsync(n => n.MaNhaHang == maNhaHang);
        }
    }
}