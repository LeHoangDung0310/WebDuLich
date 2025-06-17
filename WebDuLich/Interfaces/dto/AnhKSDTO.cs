using System.ComponentModel.DataAnnotations;

namespace WebDuLich.Interfaces.dto
{
    public class AnhKSDTO
    {
        public int MaAnh { get; set; }

        [Required(ErrorMessage = "Mã khách sạn không được để trống")]
        [StringLength(10, ErrorMessage = "Mã khách sạn không được vượt quá 10 ký tự")]
        public string MaKhachSan { get; set; }

        [Required(ErrorMessage = "Đường dẫn ảnh không được để trống")]
        public string DuongDanAnh { get; set; }
    }

    public class AnhKSUpdateDTO
    {
        [Required(ErrorMessage = "Mã khách sạn không được để trống")]
        [StringLength(10, ErrorMessage = "Mã khách sạn không được vượt quá 10 ký tự")]
        public string MaKhachSan { get; set; }
        [Required(ErrorMessage = "Đường dẫn ảnh không được để trống")]
        public string DuongDanAnh { get; set; }
    }
}