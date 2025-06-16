using System.ComponentModel.DataAnnotations;

namespace WebDuLich.Interfaces.dto
{
    public class NhaHangDTO
    {
        public string MaNhaHang { get; set; }

        [Required(ErrorMessage = "Tên nhà hàng không được để trống")]
        [StringLength(100, ErrorMessage = "Tên nhà hàng không được vượt quá 100 ký tự")]
        public string Ten { get; set; }

        public string MoTa { get; set; }

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

        [Required(ErrorMessage = "Mã tỉnh không được để trống")]
        public int MaTinh { get; set; }

        [StringLength(100, ErrorMessage = "Loại ẩm thực không được vượt quá 100 ký tự")]
        public string LoaiAmThuc { get; set; }

        [Required(ErrorMessage = "Thời gian mở cửa không được để trống")]
        public TimeSpan ThoiGianMoCua { get; set; }

        [Required(ErrorMessage = "Thời gian đóng cửa không được để trống")]
        public TimeSpan ThoiGianDongCua { get; set; }
    }

    public class NhaHangUpdateDTO
    {
        [Required(ErrorMessage = "Tên nhà hàng không được để trống")]
        [StringLength(100, ErrorMessage = "Tên nhà hàng không được vượt quá 100 ký tự")]
        public string Ten { get; set; }

        public string MoTa { get; set; }

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

        [Required(ErrorMessage = "Mã tỉnh không được để trống")]
        public int MaTinh { get; set; }

        [StringLength(100, ErrorMessage = "Loại ẩm thực không được vượt quá 100 ký tự")]
        public string LoaiAmThuc { get; set; }

        [Required(ErrorMessage = "Thời gian mở cửa không được để trống")]
        public TimeSpan ThoiGianMoCua { get; set; }

        [Required(ErrorMessage = "Thời gian đóng cửa không được để trống")]
        public TimeSpan ThoiGianDongCua { get; set; }
    }
}