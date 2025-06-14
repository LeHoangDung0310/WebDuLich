using WebDuLich.Interfaces.dto;

namespace WebDuLich.Interfaces.IRepositories
{
    public interface ITinhRepository
    {
        Task<IEnumerable<TinhDTO>> GetAllTinhAsync();
        Task<TinhDTO> GetTinhByIdAsync(int maTinh);
        Task<IEnumerable<TinhDTO>> GetTinhByMienAsync(int maMien);
        Task<TinhDTO> CreateTinhAsync(TinhDTO tinhDTO);
        Task<TinhDTO> UpdateTinhAsync(int maTinh, TinhUpdateDTO tinhDTO);
        Task<bool> DeleteTinhAsync(int maTinh);
        Task<bool> TinhExistsAsync(int maTinh);
    }
}