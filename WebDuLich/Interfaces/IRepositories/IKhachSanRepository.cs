using WebDuLich.Interfaces.dto;

namespace WebDuLich.Interfaces.IRepositories
{
    public interface IKhachSanRepository
    {
        Task<IEnumerable<KhachSanDTO>> GetAllKhachSanAsync();
        Task<KhachSanDTO> GetKhachSanByIdAsync(string maKs);
        Task<IEnumerable<KhachSanDTO>> GetKhachSanByTinhAsync(int maTinh);
        Task<KhachSanDTO> CreateKhachSanAsync(KhachSanDTO khachSanDTO);
        Task<KhachSanDTO> UpdateKhachSanAsync(string maKs, KhachSanUpdateDTO khachSanDTO);
        Task<bool> DeleteKhachSanAsync(string maKs);
        Task<bool> KhachSanExistsAsync(string maKs);
    }
}