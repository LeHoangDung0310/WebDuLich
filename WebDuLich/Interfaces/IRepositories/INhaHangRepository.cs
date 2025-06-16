using WebDuLich.Interfaces.dto;

namespace WebDuLich.Interfaces.IRepositories
{
    public interface INhaHangRepository
    {
        Task<IEnumerable<NhaHangDTO>> GetAllNhaHangAsync();
        Task<NhaHangDTO> GetNhaHangByIdAsync(string maNhaHang);
        Task<IEnumerable<NhaHangDTO>> GetNhaHangByTinhAsync(int maTinh);
        Task<NhaHangDTO> CreateNhaHangAsync(NhaHangDTO nhaHangDTO);
        Task<NhaHangDTO> UpdateNhaHangAsync(string maNhaHang, NhaHangUpdateDTO nhaHangDTO);
        Task<bool> DeleteNhaHangAsync(string maNhaHang);
        Task<bool> NhaHangExistsAsync(string maNhaHang);
    }
}