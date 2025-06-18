using System.ComponentModel.DataAnnotations;
using WebDuLich.Data;

namespace WebDuLich.Interfaces.dto
{
    public class PhongKSDTO
    {
        public int MaPhong { get; set; }

        [Required(ErrorMessage = "Mã khách sạn không được để trống")]
        [StringLength(10, ErrorMessage = "Mã khách sạn không được vượt quá 10 ký tự")]
        public string MaKhachSan { get; set; }

        [Required(ErrorMessage = "Loại phòng không được để trống")]
        public LoaiPhongKhachSan LoaiPhong { get; set; }

        [Required(ErrorMessage = "Giá phòng không được để trống")]
        [Range(0, double.MaxValue, ErrorMessage = "Giá phòng phải lớn hơn 0")]
        public decimal Gia { get; set; }

        [Required(ErrorMessage = "Sức chứa không được để trống")]
        [Range(1, int.MaxValue, ErrorMessage = "Sức chứa phải lớn hơn 0")]
        public int SucChua { get; set; }

        [Required(ErrorMessage = "Trạng thái không được để trống")]
        public TrangThaiPhong TrangThai { get; set; }

        public string MoTa { get; set; }

        [Required(ErrorMessage = "Diện tích không được để trống")]
        [Range(1, int.MaxValue, ErrorMessage = "Diện tích phải lớn hơn 0")]
        public int DienTich { get; set; }

        public string TienIch { get; set; }
    }

    public class PhongKSUpdateDTO
    {
        [Required(ErrorMessage = "Loại phòng không được để trống")]
        public LoaiPhongKhachSan LoaiPhong { get; set; }

        [Required(ErrorMessage = "Giá phòng không được để trống")]
        [Range(0, double.MaxValue, ErrorMessage = "Giá phòng phải lớn hơn 0")]
        public decimal Gia { get; set; }

        [Required(ErrorMessage = "Sức chứa không được để trống")]
        [Range(1, int.MaxValue, ErrorMessage = "Sức chứa phải lớn hơn 0")]
        public int SucChua { get; set; }

        [Required(ErrorMessage = "Trạng thái không được để trống")]
        public TrangThaiPhong TrangThai { get; set; }

        public string MoTa { get; set; }

        [Required(ErrorMessage = "Diện tích không được để trống")]
        [Range(1, int.MaxValue, ErrorMessage = "Diện tích phải lớn hơn 0")]
        public int DienTich { get; set; }

        public string TienIch { get; set; }
    }
}