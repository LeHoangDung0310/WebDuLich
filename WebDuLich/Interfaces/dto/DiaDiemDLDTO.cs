using System.ComponentModel.DataAnnotations;

namespace WebDuLich.Interfaces.dto
{
    public class DiaDiemDLDTO
    {
        public string MaDiaDiem { get; set; }

        [Required(ErrorMessage = "Mã tài khoản không được để trống")]
        [StringLength(10, ErrorMessage = "Mã tài khoản không được vượt quá 10 ký tự")]
        public string MaTaiKhoan { get; set; }

        [Required(ErrorMessage = "Tiêu đề không được để trống")]
        [StringLength(200, ErrorMessage = "Tiêu đề không được vượt quá 200 ký tự")]
        public string TieuDe { get; set; }

        public string MoTa { get; set; }

        [Required(ErrorMessage = "Mã tỉnh không được để trống")]
        public int MaTinh { get; set; }

        public DateTime NgayTao { get; set; }
    }

    public class DiaDiemDLCreateDTO
    {
        [Required(ErrorMessage = "Tiêu đề không được để trống")]
        [StringLength(200, ErrorMessage = "Tiêu đề không được vượt quá 200 ký tự")]
        public string TieuDe { get; set; }

        public string MoTa { get; set; }

        [Required(ErrorMessage = "Mã tỉnh không được để trống")]
        public int MaTinh { get; set; }
    }

    public class DiaDiemDLUpdateDTO
    {
        [Required(ErrorMessage = "Tiêu đề không được để trống")]
        [StringLength(200, ErrorMessage = "Tiêu đề không được vượt quá 200 ký tự")]
        public string TieuDe { get; set; }

        public string MoTa { get; set; }

        [Required(ErrorMessage = "Mã tỉnh không được để trống")]
        public int MaTinh { get; set; }
    }
}