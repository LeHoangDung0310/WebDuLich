using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace WebDuLich.Data
{
	[Table("KHACHSAN")]
	public class KhachSan
	{
		[Key]
		[Column("MaKs")]
		[StringLength(10)]
		public string MaKs { get; set; }

		[Required]
		[Column("Ten")]
		public string Ten { get; set; }

		[Column("Mota")]
		public string MoTa { get; set; }

		[Required]
		[Column("DiaChi")]
		public string DiaChi { get; set; }

		[Required]
		[Column("DienThoai")]
		[StringLength(10)]
		public string DienThoai { get; set; }

		[Required]
		[Column("Email")]
		[StringLength(50)]
		[EmailAddress]
		public string Email { get; set; }

		[Required]
		[Column("Ma_tinh")]
		public int MaTinh { get; set; }

		// Navigation properties
		[ForeignKey("MaKs")]
		public TaiKhoan TaiKhoan { get; set; }

		[ForeignKey("MaTinh")]
		public  Tinh Tinh { get; set; }
		public ICollection<PhongKhachSan> PhongKhachSans { get; set; }
	}
}
