using WebDuLich.Interfaces.dto;

namespace WebDuLich.Interfaces.IRepositories
{
    public interface IMienRepository
    {
        Task<IEnumerable<MienDTO>> GetAllMienAsync();
        Task<MienDTO> GetMienByIdAsync(int maMien);
        Task<MienDTO> CreateMienAsync(MienDTO mienDTO);
        Task<MienDTO> UpdateMienAsync(int maMien, MienUpdateDTO mienDTO);
        Task<bool> DeleteMienAsync(int maMien);
        Task<bool> MienExistsAsync(int maMien);
    }
}