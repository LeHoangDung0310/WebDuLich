using WebDuLich.Interfaces.dto;

namespace WebDuLich.Interfaces.IRepositories
{
    public interface IQuyenTKRepository
    {
        Task<IEnumerable<QuyenTKDTO>> GetAllQuyenTKAsync();
        Task<QuyenTKDTO> GetQuyenTKByIdAsync(string maQuyen);
        Task<QuyenTKDTO> CreateQuyenTKAsync(QuyenTKDTO quyenTKDTO);
        Task<QuyenTKDTO> UpdateQuyenTKAsync(string maQuyen, QuyenTKUpdateDTO quyenTKDTO);
        Task<bool> DeleteQuyenTKAsync(string maQuyen);
        Task<bool> QuyenTKExistsAsync(string maQuyen);
    }
}