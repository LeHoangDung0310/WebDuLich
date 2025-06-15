using Microsoft.EntityFrameworkCore;
using WebDuLich.Data;
using WebDuLich.Interfaces.dto;
using WebDuLich.Interfaces.IRepositories;

namespace WebDuLich.Interfaces.Repositories
{
    public class NguoiDungRepository : INguoiDungRepository
    {
        private readonly MyDbContext _context;

        public NguoiDungRepository(MyDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<NguoiDungDTO>> GetAllNguoiDungAsync()
        {
            return await _context.NguoiDungs
                .Select(n => new NguoiDungDTO
                {
                    MaTV = n.MaTV,
                    HoVaTen = n.HoVaTen,
                    NgaySinh = n.NgaySinh,
                    Gioitinh = n.Gioitinh,
                    DiaChi = n.DiaChi,
                    SoDienThoai = n.SoDienThoai,
                    Email = n.Email
                })
                .ToListAsync();
        }

        public async Task<NguoiDungDTO> GetNguoiDungByIdAsync(string maTV)
        {
            return await _context.NguoiDungs
                .Where(n => n.MaTV == maTV)
                .Select(n => new NguoiDungDTO
                {
                    MaTV = n.MaTV,
                    HoVaTen = n.HoVaTen,
                    NgaySinh = n.NgaySinh,
                    Gioitinh = n.Gioitinh,
                    DiaChi = n.DiaChi,
                    SoDienThoai = n.SoDienThoai,
                    Email = n.Email
                })
                .FirstOrDefaultAsync();
        }

        public async Task<NguoiDungDTO> CreateNguoiDungAsync(NguoiDungDTO nguoiDungDTO)
        {
            var nguoiDung = new NguoiDung
            {
                MaTV = Guid.NewGuid().ToString().Substring(0, 10),
                HoVaTen = nguoiDungDTO.HoVaTen,
                NgaySinh = nguoiDungDTO.NgaySinh,
                Gioitinh = nguoiDungDTO.Gioitinh,
                DiaChi = nguoiDungDTO.DiaChi,
                SoDienThoai = nguoiDungDTO.SoDienThoai,
                Email = nguoiDungDTO.Email
            };

            _context.NguoiDungs.Add(nguoiDung);
            await _context.SaveChangesAsync();

            return new NguoiDungDTO
            {
                MaTV = nguoiDung.MaTV,
                HoVaTen = nguoiDung.HoVaTen,
                NgaySinh = nguoiDung.NgaySinh,
                Gioitinh = nguoiDung.Gioitinh,
                DiaChi = nguoiDung.DiaChi,
                SoDienThoai = nguoiDung.SoDienThoai,
                Email = nguoiDung.Email
            };
        }

        public async Task<NguoiDungDTO> UpdateNguoiDungAsync(string maTV, NguoiDungUpdateDTO nguoiDungDTO)
        {
            var nguoiDung = await _context.NguoiDungs.FindAsync(maTV);
            if (nguoiDung == null)
                return null;

            nguoiDung.HoVaTen = nguoiDungDTO.HoVaTen;
            nguoiDung.NgaySinh = nguoiDungDTO.NgaySinh;
            nguoiDung.Gioitinh = nguoiDungDTO.Gioitinh;
            nguoiDung.DiaChi = nguoiDungDTO.DiaChi;
            nguoiDung.SoDienThoai = nguoiDungDTO.SoDienThoai;
            nguoiDung.Email = nguoiDungDTO.Email;

            await _context.SaveChangesAsync();

            return new NguoiDungDTO
            {
                MaTV = nguoiDung.MaTV,
                HoVaTen = nguoiDung.HoVaTen,
                NgaySinh = nguoiDung.NgaySinh,
                Gioitinh = nguoiDung.Gioitinh,
                DiaChi = nguoiDung.DiaChi,
                SoDienThoai = nguoiDung.SoDienThoai,
                Email = nguoiDung.Email
            };
        }

        public async Task<bool> DeleteNguoiDungAsync(string maTV)
        {
            var nguoiDung = await _context.NguoiDungs.FindAsync(maTV);
            if (nguoiDung == null)
                return false;

            _context.NguoiDungs.Remove(nguoiDung);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> NguoiDungExistsAsync(string maTV)
        {
            return await _context.NguoiDungs.AnyAsync(n => n.MaTV == maTV);
        }
    }
}