using Microsoft.EntityFrameworkCore;
using WebDuLich.Data;
using WebDuLich.Interfaces.dto;
using WebDuLich.Interfaces.IRepositories;

namespace WebDuLich.Interfaces.Repositories
{
    public class BanNHRepository : IBanNHRepository
    {
        private readonly MyDbContext _context;

        public BanNHRepository(MyDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<BanNHDTO>> GetAllBanNHAsync()
        {
            return await _context.BanNhaHangs
                .Select(b => new BanNHDTO
                {
                    MaBan = b.MaBan,
                    MaNhaHang = b.MaNhaHang,
                    SoNguoiToiDa = b.SoNguoiToiDa,
                    LoaiBan = b.LoaiBan,
                    MoTa = b.MoTa,
                    Gia = b.Gia
                })
                .ToListAsync();
        }

        public async Task<BanNHDTO> GetBanNHByIdAsync(string maBan)
        {
            return await _context.BanNhaHangs
                .Where(b => b.MaBan == maBan)
                .Select(b => new BanNHDTO
                {
                    MaBan = b.MaBan,
                    MaNhaHang = b.MaNhaHang,
                    SoNguoiToiDa = b.SoNguoiToiDa,
                    LoaiBan = b.LoaiBan,
                    MoTa = b.MoTa,
                    Gia = b.Gia
                })
                .FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<BanNHDTO>> GetBanNHByNhaHangAsync(string maNhaHang)
        {
            return await _context.BanNhaHangs
                .Where(b => b.MaNhaHang == maNhaHang)
                .Select(b => new BanNHDTO
                {
                    MaBan = b.MaBan,
                    MaNhaHang = b.MaNhaHang,
                    SoNguoiToiDa = b.SoNguoiToiDa,
                    LoaiBan = b.LoaiBan,
                    MoTa = b.MoTa,
                    Gia = b.Gia
                })
                .ToListAsync();
        }

        public async Task<BanNHDTO> CreateBanNHAsync(BanNHCreateDTO banNHDTO)
        {
            var banNH = new BanNhaHang
            {
                MaBan = Guid.NewGuid().ToString().Substring(0, 10),
                MaNhaHang = banNHDTO.MaNhaHang,
                SoNguoiToiDa = banNHDTO.SoNguoiToiDa,
                LoaiBan = banNHDTO.LoaiBan,
                MoTa = banNHDTO.MoTa,
                Gia = banNHDTO.Gia
            };

            _context.BanNhaHangs.Add(banNH);
            await _context.SaveChangesAsync();

            return new BanNHDTO
            {
                MaBan = banNH.MaBan,
                MaNhaHang = banNH.MaNhaHang,
                SoNguoiToiDa = banNH.SoNguoiToiDa,
                LoaiBan = banNH.LoaiBan,
                MoTa = banNH.MoTa,
                Gia = banNH.Gia
            };
        }

        public async Task<BanNHDTO> UpdateBanNHAsync(string maBan, BanNHUpdateDTO banNHDTO)
        {
            var banNH = await _context.BanNhaHangs.FindAsync(maBan);
            if (banNH == null)
                return null;

            banNH.SoNguoiToiDa = banNHDTO.SoNguoiToiDa;
            banNH.LoaiBan = banNHDTO.LoaiBan;
            banNH.MoTa = banNHDTO.MoTa;
            banNH.Gia = banNHDTO.Gia;

            await _context.SaveChangesAsync();

            return new BanNHDTO
            {
                MaBan = banNH.MaBan,
                MaNhaHang = banNH.MaNhaHang,
                SoNguoiToiDa = banNH.SoNguoiToiDa,
                LoaiBan = banNH.LoaiBan,
                MoTa = banNH.MoTa,
                Gia = banNH.Gia
            };
        }

        public async Task<bool> DeleteBanNHAsync(string maBan)
        {
            var banNH = await _context.BanNhaHangs.FindAsync(maBan);
            if (banNH == null)
                return false;

            _context.BanNhaHangs.Remove(banNH);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> BanNHExistsAsync(string maBan)
        {
            return await _context.BanNhaHangs.AnyAsync(b => b.MaBan == maBan);
        }
    }
}