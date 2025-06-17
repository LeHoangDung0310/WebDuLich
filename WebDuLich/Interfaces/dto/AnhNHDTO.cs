using System.ComponentModel.DataAnnotations;

namespace WebDuLich.Interfaces.dto
{
    public class AnhNHDTO
    {
        public int MaAnh { get; set; }

        [Required(ErrorMessage = "Mã nhà hàng không được để trống")]
        [StringLength(10, ErrorMessage = "Mã nhà hàng không được vượt quá 10 ký tự")]
        public string MaNhaHang { get; set; }

        [Required(ErrorMessage = "Đường dẫn ảnh không được để trống")]
        public string DuongDanAnh { get; set; }
    }

    public class AnhNHUpdateDTO
    {
        [Required(ErrorMessage = "Mã nhà hàng không được để trống")]
        [StringLength(10, ErrorMessage = "Mã nhà hàng không được vượt quá 10 ký tự")]
        public string MaNhaHang { get; set; }
        [Required(ErrorMessage = "Đường dẫn ảnh không được để trống")]
        public string DuongDanAnh { get; set; }
    }
}