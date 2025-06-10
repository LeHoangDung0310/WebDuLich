using WebDuLich.Data;
using WebDuLich.Interfaces.dto;

namespace WebDuLich.Interfaces.IRepositories
{
    public interface ITaiKhoanRepository
    {
        Task<IEnumerable<TaiKhoan>> GetAllTaiKhoanAsync();
        Task<TaiKhoan> GetTaiKhoanByIdAsync(string maTK);
        Task<TaiKhoan> CreateTaiKhoanAsync(TaiKhoanDTO taiKhoanDTO);
        Task<TaiKhoan> UpdateTaiKhoanAsync(string maTK, TaiKhoanUpdateDTO taiKhoanDTO);
        Task<bool> DeleteTaiKhoanAsync(string maTK);
        Task<bool> TaiKhoanExistsAsync(string maTK);
    }
}