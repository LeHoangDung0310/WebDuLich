using WebDuLich.Interfaces.dto;

namespace WebDuLich.Interfaces.IRepositories
{
    public interface IPhongKSRepository
    {
        Task<IEnumerable<PhongKSDTO>> GetAllPhongKSAsync();
        Task<PhongKSDTO> GetPhongKSByIdAsync(int maPhong);
        Task<IEnumerable<PhongKSDTO>> GetPhongKSByKhachSanAsync(string maKS);
        Task<PhongKSDTO> CreatePhongKSAsync(PhongKSDTO phongKSDTO);
        Task<PhongKSDTO> UpdatePhongKSAsync(int maPhong, PhongKSUpdateDTO phongKSDTO);
        Task<bool> DeletePhongKSAsync(int maPhong);
        Task<bool> PhongKSExistsAsync(int maPhong);
    }
}