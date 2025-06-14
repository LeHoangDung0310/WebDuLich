using System.ComponentModel.DataAnnotations;

namespace WebDuLich.Interfaces.dto
{
    public class MienDTO
    {
        public int MaMien { get; set; }

        [Required(ErrorMessage = "Tên miền không được để trống")]
        [StringLength(50, ErrorMessage = "Tên miền không được vượt quá 50 ký tự")]
        public string TenMien { get; set; }
    }

    public class MienUpdateDTO 
    {
        [Required(ErrorMessage = "Tên miền không được để trống")]
        [StringLength(50, ErrorMessage = "Tên miền không được vượt quá 50 ký tự")]
        public string TenMien { get; set; }
    }
}