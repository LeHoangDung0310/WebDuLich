using WebDuLich.Interfaces.dto;

namespace WebDuLich.Interfaces.IRepositories
{
    public interface IDatPhongRepository
    {
        Task<IEnumerable<DatPhongDTO>> GetAllDatPhongAsync();
        Task<DatPhongDTO> GetDatPhongByIdAsync(int maDatPhong);
        Task<IEnumerable<DatPhongDTO>> GetDatPhongByNguoiDungAsync(string maNguoiDung);
        Task<IEnumerable<DatPhongDTO>> GetDatPhongByPhongAsync(int maPhong);
        Task<DatPhongDTO> CreateDatPhongAsync(string maNguoiDung, DatPhongCreateDTO datPhongDTO);
        Task<DatPhongDTO> UpdateDatPhongAsync(int maDatPhong, DatPhongUpdateDTO datPhongDTO);
        Task<bool> DeleteDatPhongAsync(int maDatPhong);
        Task<bool> DatPhongExistsAsync(int maDatPhong);
        Task<bool> IsPhongAvailableAsync(int maPhong, DateTime ngayNhan, DateTime ngayTra);
    }
}