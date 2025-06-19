using System.ComponentModel.DataAnnotations;

namespace WebDuLich.Interfaces.dto
{
    public class AnhDDDTO
    {
        public int MaAnh { get; set; }

        [Required(ErrorMessage = "Mã địa điểm không được để trống")]
        [StringLength(10, ErrorMessage = "Mã địa điểm không được vượt quá 10 ký tự")]
        public string MaDiaDiem { get; set; }

        [Required(ErrorMessage = "Đường dẫn ảnh không được để trống")]
        public string DuongDanAnh { get; set; }
    }

    public class AnhDDUpdateDTO
    {
        [Required(ErrorMessage = "Tên ảnh không được để trống")]
        [StringLength(10, ErrorMessage = "Tên ảnh không được vượt quá 10 ký tự")]
        public string MaDiaDiem { get; set; }
        [Required(ErrorMessage = "Đường dẫn ảnh không được để trống")]
        public string DuongDanAnh { get; set; }
    }
}