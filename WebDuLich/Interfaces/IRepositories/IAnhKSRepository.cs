using WebDuLich.Interfaces.dto;

namespace WebDuLich.Interfaces.IRepositories
{
    public interface IAnhKSRepository
    {
        Task<IEnumerable<AnhKSDTO>> GetAllAnhKSAsync();
        Task<AnhKSDTO> GetAnhKSByIdAsync(int maAnh);
        Task<IEnumerable<AnhKSDTO>> GetAnhKSByKhachSanAsync(string maKS);
        Task<AnhKSDTO> CreateAnhKSAsync(AnhKSDTO anhKSDTO);
        Task<AnhKSDTO> UpdateAnhKSAsync(int maAnh, AnhKSUpdateDTO anhKSDTO);
        Task<bool> DeleteAnhKSAsync(int maAnh);
        Task<bool> AnhKSExistsAsync(int maAnh);
    }
}