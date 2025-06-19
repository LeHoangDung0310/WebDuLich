using WebDuLich.Interfaces.dto;

namespace WebDuLich.Interfaces.IRepositories
{
    public interface IAnhDDRepository
    {
        Task<IEnumerable<AnhDDDTO>> GetAllAnhDDAsync();
        Task<AnhDDDTO> GetAnhDDByIdAsync(int maAnh);
        Task<IEnumerable<AnhDDDTO>> GetAnhDDByDiaDiemAsync(string maDiaDiem);
        Task<AnhDDDTO> CreateAnhDDAsync(AnhDDDTO anhDDDTO);
        Task<AnhDDDTO> UpdateAnhDDAsync(int maAnh, AnhDDUpdateDTO anhDDDTO);
        Task<bool> DeleteAnhDDAsync(int maAnh);
        Task<bool> AnhDDExistsAsync(int maAnh);
    }
}