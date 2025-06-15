using WebDuLich.Interfaces.dto;

namespace WebDuLich.Interfaces.IRepositories
{
    public interface INguoiDungRepository
    {
        Task<IEnumerable<NguoiDungDTO>> GetAllNguoiDungAsync();
        Task<NguoiDungDTO> GetNguoiDungByIdAsync(string maTV);
        Task<NguoiDungDTO> CreateNguoiDungAsync(NguoiDungDTO nguoiDungDTO);
        Task<NguoiDungDTO> UpdateNguoiDungAsync(string maTV, NguoiDungUpdateDTO nguoiDungDTO);
        Task<bool> DeleteNguoiDungAsync(string maTV);
        Task<bool> NguoiDungExistsAsync(string maTV);
    }
}