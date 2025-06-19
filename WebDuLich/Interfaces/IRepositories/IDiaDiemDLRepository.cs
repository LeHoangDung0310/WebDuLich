using WebDuLich.Interfaces.dto;

namespace WebDuLich.Interfaces.IRepositories
{
    public interface IDiaDiemDLRepository
    {
        Task<IEnumerable<DiaDiemDLDTO>> GetAllDiaDiemDLAsync();
        Task<DiaDiemDLDTO> GetDiaDiemDLByIdAsync(string maDiaDiem);
        Task<IEnumerable<DiaDiemDLDTO>> GetDiaDiemDLByTinhAsync(int maTinh);

        Task<DiaDiemDLDTO> CreateDiaDiemDLAsync(string maTaiKhoan, DiaDiemDLCreateDTO diaDiemDLDTO);
        Task<DiaDiemDLDTO> UpdateDiaDiemDLAsync(string maDiaDiem, DiaDiemDLUpdateDTO diaDiemDLDTO);
        Task<bool> DeleteDiaDiemDLAsync(string maDiaDiem);
        Task<bool> DiaDiemDLExistsAsync(string maDiaDiem);
    }
}