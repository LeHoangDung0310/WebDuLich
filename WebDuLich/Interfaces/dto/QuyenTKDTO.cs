using System.ComponentModel.DataAnnotations;

namespace WebDuLich.Interfaces.dto
{
    public class QuyenTKDTO
    {
        public string MaQuyen { get; set; }

        [Required(ErrorMessage = "Tên quyền không được để trống")]
        [StringLength(50, ErrorMessage = "Tên quyền không được vượt quá 50 ký tự")]
        public string TenQuyen { get; set; }
    }

    public class QuyenTKUpdateDTO
    {
        [Required(ErrorMessage = "Tên quyền không được để trống")]
        [StringLength(50, ErrorMessage = "Tên quyền không được vượt quá 50 ký tự")]
        public string TenQuyen { get; set; }
    }
}