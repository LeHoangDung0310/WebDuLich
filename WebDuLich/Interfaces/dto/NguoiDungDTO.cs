using System.ComponentModel.DataAnnotations;
using WebDuLich.Data;

namespace WebDuLich.Interfaces.dto
{
    public class NguoiDungDTO
    {
        public string MaTV { get; set; }

        [Required(ErrorMessage = "Họ và tên không được để trống")]
        [StringLength(100, ErrorMessage = "Họ và tên không được vượt quá 100 ký tự")]
        public string HoVaTen { get; set; }

        [Required(ErrorMessage = "Ngày sinh không được để trống")]
        public DateTime NgaySinh { get; set; }

        [Required(ErrorMessage = "Giới tính không được để trống")]
        public GioiTinh Gioitinh { get; set; }

        [Required(ErrorMessage = "Địa chỉ không được để trống")]
        public string DiaChi { get; set; }

        [Required(ErrorMessage = "Số điện thoại không được để trống")]
        [StringLength(10, ErrorMessage = "Số điện thoại không được vượt quá 10 ký tự")]
        [RegularExpression(@"^[0-9]{10}$", ErrorMessage = "Số điện thoại không hợp lệ")]
        public string SoDienThoai { get; set; }

        [Required(ErrorMessage = "Email không được để trống")]
        [EmailAddress(ErrorMessage = "Email không hợp lệ")]
        [StringLength(50, ErrorMessage = "Email không được vượt quá 50 ký tự")]
        public string Email { get; set; }
    }

    public class NguoiDungUpdateDTO
    {
        [Required(ErrorMessage = "Họ và tên không được để trống")]
        [StringLength(100, ErrorMessage = "Họ và tên không được vượt quá 100 ký tự")]
        public string HoVaTen { get; set; }

        [Required(ErrorMessage = "Ngày sinh không được để trống")]
        public DateTime NgaySinh { get; set; }

        [Required(ErrorMessage = "Giới tính không được để trống")]
        public GioiTinh Gioitinh { get; set; }

        [Required(ErrorMessage = "Địa chỉ không được để trống")]
        public string DiaChi { get; set; }

        [Required(ErrorMessage = "Số điện thoại không được để trống")]
        [StringLength(10, ErrorMessage = "Số điện thoại không được vượt quá 10 ký tự")]
        [RegularExpression(@"^[0-9]{10}$", ErrorMessage = "Số điện thoại không hợp lệ")]
        public string SoDienThoai { get; set; }

        [Required(ErrorMessage = "Email không được để trống")]
        [EmailAddress(ErrorMessage = "Email không hợp lệ")]
        [StringLength(50, ErrorMessage = "Email không được vượt quá 50 ký tự")]
        public string Email { get; set; }
    }
}