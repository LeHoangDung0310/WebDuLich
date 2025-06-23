using WebDuLich.Interfaces.dto;

namespace WebDuLich.Interfaces.IRepositories
{
    public interface IGioiThieuRepository
    {
        Task<IEnumerable<GioiThieuDTO>> GetAllAsync();
        Task<GioiThieuDTO> GetByIdAsync(int id);
        Task<GioiThieuDTO> CreateAsync(string idNguoiCapNhat, GioiThieuCreateDTO dto);
        Task<GioiThieuDTO> UpdateAsync(int id, string idNguoiCapNhat, GioiThieuUpdateDTO dto);
        Task<bool> DeleteAsync(int id);
    }
}