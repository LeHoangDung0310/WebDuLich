using System.ComponentModel.DataAnnotations;

namespace WebDuLich.Interfaces.dto
{
    public class TinhDTO
    {
        public int MaTinh { get; set; }

        [Required(ErrorMessage = "Tên tỉnh không được để trống")]
        [StringLength(100, ErrorMessage = "Tên tỉnh không được vượt quá 100 ký tự")]
        public string TenTinh { get; set; }

        [Required(ErrorMessage = "Mã miền không được để trống")]
        public int MaMien { get; set; }
    }

    public class TinhUpdateDTO
    {
        [Required(ErrorMessage = "Tên tỉnh không được để trống")]
        [StringLength(100, ErrorMessage = "Tên tỉnh không được vượt quá 100 ký tự")]
        public string TenTinh { get; set; }

        [Required(ErrorMessage = "Mã miền không được để trống")]
        public int MaMien { get; set; }
    }
}