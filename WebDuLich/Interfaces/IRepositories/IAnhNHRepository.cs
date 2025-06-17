using WebDuLich.Interfaces.dto;

namespace WebDuLich.Interfaces.IRepositories
{
    public interface IAnhNHRepository
    {
        Task<IEnumerable<AnhNHDTO>> GetAllAnhNHAsync();
        Task<AnhNHDTO> GetAnhNHByIdAsync(int maAnh);
        Task<IEnumerable<AnhNHDTO>> GetAnhNHByNhaHangAsync(string maNH);
        Task<AnhNHDTO> CreateAnhNHAsync(AnhNHDTO anhNHDTO);
        Task<AnhNHDTO> UpdateAnhNHAsync(int maAnh, AnhNHUpdateDTO anhNHDTO);
        Task<bool> DeleteAnhNHAsync(int maAnh);
        Task<bool> AnhNHExistsAsync(int maAnh);
    }
}