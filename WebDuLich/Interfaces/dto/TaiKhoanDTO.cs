using System.ComponentModel.DataAnnotations;

namespace WebDuLich.Interfaces.dto
{
    public class TaiKhoanDTO
    {
        public string MaTK { get; set; }

        [Required(ErrorMessage = "Tên đăng nhập không được để trống")]
        [StringLength(50, ErrorMessage = "Tên đăng nhập không được vượt quá 50 ký tự")]
        public string TenDangNhap { get; set; }

        [Required(ErrorMessage = "Mật khẩu không được để trống")]
        [MinLength(6, ErrorMessage = "Mật khẩu phải có ít nhất 6 ký tự")]
        public string MatKhau { get; set; }

        [Required(ErrorMessage = "Mã quyền không được để trống")]
        [StringLength(10, ErrorMessage = "Mã quyền không được vượt quá 10 ký tự")]
        public string MaQuyen { get; set; }
    }

    public class TaiKhoanUpdateDTO
    {
        [Required(ErrorMessage = "Tên đăng nhập không được để trống")]
        [StringLength(50, ErrorMessage = "Tên đăng nhập không được vượt quá 50 ký tự")]
        public string TenDangNhap { get; set; }

        [MinLength(6, ErrorMessage = "Mật khẩu phải có ít nhất 6 ký tự")]
        public string? MatKhau { get; set; }  // Optional khi update

        [Required(ErrorMessage = "Mã quyền không được để trống")]
        [StringLength(10, ErrorMessage = "Mã quyền không được vượt quá 10 ký tự")]
        public string MaQuyen { get; set; }
    }
}
