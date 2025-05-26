using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace WebDuLich.Data
{
	public enum GioiTinh
	{
		Nam,
		Nu,
		Khac
	}

	[Table("NGUOIDUNG")]
	public class NguoiDung
	{
		[Key]
		[Column("MaND")]
		[StringLength(10)]
		public string MaTV { get; set; }

		[Column("HoVaTen")]
		[StringLength(100)]
		public string HoVaTen { get; set; }

		[Column("NgaySinh")]
		public DateTime NgaySinh { get; set; }

		[Column("Gioitinh")]
		public GioiTinh Gioitinh { get; set; } = GioiTinh.Nam;


		[Column("DiaChi")]
		public string DiaChi { get; set; }

		[Column("SoDienThoai")]
		[StringLength(10)]
		public string SoDienThoai { get; set; }

		[Column("Email")]
		[StringLength(50)]
		[EmailAddress]
		public string Email { get; set; }

		// Navigation property
		[ForeignKey("MaTV")]
		public TaiKhoan TaiKhoan { get; set; }
	}
}
