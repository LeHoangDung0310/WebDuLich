using System.ComponentModel.DataAnnotations;

namespace WebDuLich.Interfaces.dto
{
    public class GioiThieuDTO
    {
        public int IdGioiThieu { get; set; }

        [Required]
        [StringLength(200)]
        public string TieuDe { get; set; }

        public string NoiDung { get; set; }

        public DateTime NgayCapNhat { get; set; }

        [Required]
        [StringLength(10)]
        public string IdNguoiCapNhat { get; set; }
    }

    public class GioiThieuCreateDTO
    {
        [Required]
        [StringLength(200)]
        public string TieuDe { get; set; }

        public string NoiDung { get; set; }
    }

    public class GioiThieuUpdateDTO
    {
        [Required]
        [StringLength(200)]
        public string TieuDe { get; set; }

        public string NoiDung { get; set; }
    }
}