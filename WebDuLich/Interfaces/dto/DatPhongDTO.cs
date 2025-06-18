using System.ComponentModel.DataAnnotations;
using WebDuLich.Data;

namespace WebDuLich.Interfaces.dto
{
    public class DatPhongDTO
    {
        public int MaDatPhong { get; set; }

        [Required(ErrorMessage = "Mã người dùng không được để trống")]
        [StringLength(10, ErrorMessage = "Mã người dùng không được vượt quá 10 ký tự")]
        public string MaNguoiDung { get; set; }

        [Required(ErrorMessage = "Mã phòng không được để trống")]
        public int MaPhong { get; set; }

        [Required(ErrorMessage = "Ngày nhận phòng không được để trống")]
        public DateTime NgayNhanPhong { get; set; }

        [Required(ErrorMessage = "Ngày trả phòng không được để trống")]
        public DateTime NgayTraPhong { get; set; }

        [Required(ErrorMessage = "Trạng thái không được để trống")]
        public TrangThaiDatPhong TrangThai { get; set; }

        public DateTime ThoiGianDat { get; set; }
    }

    public class DatPhongUpdateDTO
    {
        [Required(ErrorMessage = "Trạng thái không được để trống")]
        public TrangThaiDatPhong TrangThai { get; set; }

        [Required(ErrorMessage = "Ngày nhận phòng không được để trống")]
        public DateTime NgayNhanPhong { get; set; }

        [Required(ErrorMessage = "Ngày trả phòng không được để trống")]
        public DateTime NgayTraPhong { get; set; }
    }

    public class DatPhongCreateDTO
    {
        [Required(ErrorMessage = "Mã phòng không được để trống")]
        public int MaPhong { get; set; }

        [Required(ErrorMessage = "Ngày nhận phòng không được để trống")]
        public DateTime NgayNhanPhong { get; set; }

        [Required(ErrorMessage = "Ngày trả phòng không được để trống")]
        public DateTime NgayTraPhong { get; set; }
    }
}