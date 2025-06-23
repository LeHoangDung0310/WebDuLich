using System.ComponentModel.DataAnnotations;
using WebDuLich.Data;

namespace WebDuLich.Interfaces.dto
{
    public class BanNHDTO
    {
        public string MaBan { get; set; }

        [Required(ErrorMessage = "Mã nhà hàng không được để trống")]
        [StringLength(10)]
        public string MaNhaHang { get; set; }

        [Required(ErrorMessage = "Số người tối đa không được để trống")]
        [Range(1, int.MaxValue, ErrorMessage = "Số người tối đa phải lớn hơn 0")]
        public int SoNguoiToiDa { get; set; }

        [Required(ErrorMessage = "Loại bàn không được để trống")]
        public LoaiBanEnum LoaiBan { get; set; }

        public string MoTa { get; set; }

        [Required(ErrorMessage = "Giá không được để trống")]
        [Range(0, double.MaxValue, ErrorMessage = "Giá phải lớn hơn hoặc bằng 0")]
        public decimal Gia { get; set; }
    }

    public class BanNHCreateDTO
    {
        [Required(ErrorMessage = "Mã nhà hàng không được để trống")]
        [StringLength(10)]
        public string MaNhaHang { get; set; }

        [Required(ErrorMessage = "Số người tối đa không được để trống")]
        [Range(1, int.MaxValue, ErrorMessage = "Số người tối đa phải lớn hơn 0")]
        public int SoNguoiToiDa { get; set; }

        [Required(ErrorMessage = "Loại bàn không được để trống")]
        public LoaiBanEnum LoaiBan { get; set; }

        public string MoTa { get; set; }

        [Required(ErrorMessage = "Giá không được để trống")]
        [Range(0, double.MaxValue, ErrorMessage = "Giá phải lớn hơn hoặc bằng 0")]
        public decimal Gia { get; set; }
    }

    public class BanNHUpdateDTO
    {
        [Required(ErrorMessage = "Số người tối đa không được để trống")]
        [Range(1, int.MaxValue, ErrorMessage = "Số người tối đa phải lớn hơn 0")]
        public int SoNguoiToiDa { get; set; }

        [Required(ErrorMessage = "Loại bàn không được để trống")]
        public LoaiBanEnum LoaiBan { get; set; }

        public string MoTa { get; set; }

        [Required(ErrorMessage = "Giá không được để trống")]
        [Range(0, double.MaxValue, ErrorMessage = "Giá phải lớn hơn hoặc bằng 0")]
        public decimal Gia { get; set; }
    }
}