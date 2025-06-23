using Microsoft.EntityFrameworkCore;
using WebDuLich.Data;
using WebDuLich.Interfaces.dto;
using WebDuLich.Interfaces.IRepositories;

namespace WebDuLich.Interfaces.Repositories
{
    public class GioiThieuRepository : IGioiThieuRepository
    {
        private readonly MyDbContext _context;

        public GioiThieuRepository(MyDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<GioiThieuDTO>> GetAllAsync()
        {
            return await _context.GioiThieuWebsites
                .Select(g => new GioiThieuDTO
                {
                    IdGioiThieu = g.IdGioiThieu,
                    TieuDe = g.TieuDe,
                    NoiDung = g.NoiDung,
                    NgayCapNhat = g.NgayCapNhat,
                    IdNguoiCapNhat = g.IdNguoiCapNhat
                }).ToListAsync();
        }

        public async Task<GioiThieuDTO> GetByIdAsync(int id)
        {
            return await _context.GioiThieuWebsites
                .Where(g => g.IdGioiThieu == id)
                .Select(g => new GioiThieuDTO
                {
                    IdGioiThieu = g.IdGioiThieu,
                    TieuDe = g.TieuDe,
                    NoiDung = g.NoiDung,
                    NgayCapNhat = g.NgayCapNhat,
                    IdNguoiCapNhat = g.IdNguoiCapNhat
                }).FirstOrDefaultAsync();
        }

        public async Task<GioiThieuDTO> CreateAsync(string idNguoiCapNhat, GioiThieuCreateDTO dto)
        {
            var entity = new GioiThieuWebsite
            {
                TieuDe = dto.TieuDe,
                NoiDung = dto.NoiDung,
                NgayCapNhat = DateTime.Now,
                IdNguoiCapNhat = idNguoiCapNhat
            };
            _context.GioiThieuWebsites.Add(entity);
            await _context.SaveChangesAsync();

            return new GioiThieuDTO
            {
                IdGioiThieu = entity.IdGioiThieu,
                TieuDe = entity.TieuDe,
                NoiDung = entity.NoiDung,
                NgayCapNhat = entity.NgayCapNhat,
                IdNguoiCapNhat = entity.IdNguoiCapNhat
            };
        }

        public async Task<GioiThieuDTO> UpdateAsync(int id, string idNguoiCapNhat, GioiThieuUpdateDTO dto)
        {
            var entity = await _context.GioiThieuWebsites.FindAsync(id);
            if (entity == null) return null;

            entity.TieuDe = dto.TieuDe;
            entity.NoiDung = dto.NoiDung;
            entity.NgayCapNhat = DateTime.Now;
            entity.IdNguoiCapNhat = idNguoiCapNhat;

            await _context.SaveChangesAsync();

            return new GioiThieuDTO
            {
                IdGioiThieu = entity.IdGioiThieu,
                TieuDe = entity.TieuDe,
                NoiDung = entity.NoiDung,
                NgayCapNhat = entity.NgayCapNhat,
                IdNguoiCapNhat = entity.IdNguoiCapNhat
            };
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var entity = await _context.GioiThieuWebsites.FindAsync(id);
            if (entity == null) return false;
            _context.GioiThieuWebsites.Remove(entity);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}