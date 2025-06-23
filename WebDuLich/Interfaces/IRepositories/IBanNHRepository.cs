using WebDuLich.Interfaces.dto;

namespace WebDuLich.Interfaces.IRepositories
{
    public interface IBanNHRepository
    {
        Task<IEnumerable<BanNHDTO>> GetAllBanNHAsync();
        Task<BanNHDTO> GetBanNHByIdAsync(string maBan);
        Task<IEnumerable<BanNHDTO>> GetBanNHByNhaHangAsync(string maNhaHang);
        Task<BanNHDTO> CreateBanNHAsync(BanNHCreateDTO banNHDTO);
        Task<BanNHDTO> UpdateBanNHAsync(string maBan, BanNHUpdateDTO banNHDTO);
        Task<bool> DeleteBanNHAsync(string maBan);
        Task<bool> BanNHExistsAsync(string maBan);
    }
}