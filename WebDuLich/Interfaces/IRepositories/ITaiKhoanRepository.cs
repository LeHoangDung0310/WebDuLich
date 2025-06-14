using WebDuLich.Data;
using WebDuLich.Interfaces.dto;

namespace WebDuLich.Interfaces.IRepositories
{
    public interface ITaiKhoanRepository
    {
        Task<IEnumerable<TaiKhoanDTO>> GetAllTaiKhoanAsync();
        Task<TaiKhoanDTO> GetTaiKhoanByIdAsync(string maTK);
        Task<TaiKhoanDTO> CreateTaiKhoanAsync(TaiKhoanDTO taiKhoanDTO);
        Task<TaiKhoanDTO> UpdateTaiKhoanAsync(string maTK, TaiKhoanUpdateDTO taiKhoanDTO);
        Task<bool> DeleteTaiKhoanAsync(string maTK);
        Task<bool> TaiKhoanExistsAsync(string maTK);
    }
}