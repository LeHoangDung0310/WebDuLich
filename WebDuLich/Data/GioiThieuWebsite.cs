using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace WebDuLich.Data
{
	[Table("GioiThieuWebsite")]
	public class GioiThieuWebsite
	{
		[Key]
		[Column("id_gioi_thieu")]
		public int IdGioiThieu { get; set; }

		[Required]
		[Column("tieu_de")]
		public string TieuDe { get; set; }

		[Column("noi_dung")]
		public string NoiDung { get; set; }

		[Required]
		[Column("ngay_cap_nhat")]
		public DateTime NgayCapNhat { get; set; }

		[Required]
		[Column("id_nguoi_cap_nhat")]
		[StringLength(10)]
		public string IdNguoiCapNhat { get; set; }

		// Navigation property
		[ForeignKey("IdNguoiCapNhat")]
		public TaiKhoan NguoiCapNhat { get; set; }
	}
}
