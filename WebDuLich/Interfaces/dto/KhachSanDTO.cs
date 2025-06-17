using System.ComponentModel.DataAnnotations;

namespace WebDuLich.Interfaces.dto
{
    public class KhachSanDTO
    {
        public string MaKs { get; set; }

        [Required(ErrorMessage = "Tên khách sạn không được để trống")]
        public string Ten { get; set; }

        public string MoTa { get; set; }

        [Required(ErrorMessage = "Địa chỉ không được để trống")]
        public string DiaChi { get; set; }

        [Required(ErrorMessage = "Số điện thoại không được để trống")]
        [StringLength(10, ErrorMessage = "Số điện thoại không được vượt quá 10 ký tự")]
        [RegularExpression(@"^[0-9]{10}$", ErrorMessage = "Số điện thoại không hợp lệ")]
        public string DienThoai { get; set; }

        [Required(ErrorMessage = "Email không được để trống")]
        [EmailAddress(ErrorMessage = "Email không hợp lệ")]
        [StringLength(50, ErrorMessage = "Email không được vượt quá 50 ký tự")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Mã tỉnh không được để trống")]
        public int MaTinh { get; set; }
    }

    public class KhachSanUpdateDTO
    {
        [Required(ErrorMessage = "Tên khách sạn không được để trống")]
        public string Ten { get; set; }

        public string MoTa { get; set; }

        [Required(ErrorMessage = "Địa chỉ không được để trống")]
        public string DiaChi { get; set; }

        [Required(ErrorMessage = "Số điện thoại không được để trống")]
        [StringLength(10, ErrorMessage = "Số điện thoại không được vượt quá 10 ký tự")]
        [RegularExpression(@"^[0-9]{10}$", ErrorMessage = "Số điện thoại không hợp lệ")]
        public string DienThoai { get; set; }

        [Required(ErrorMessage = "Email không được để trống")]
        [EmailAddress(ErrorMessage = "Email không hợp lệ")]
        [StringLength(50, ErrorMessage = "Email không được vượt quá 50 ký tự")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Mã tỉnh không được để trống")]
        public int MaTinh { get; set; }
    }
}