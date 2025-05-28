using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace WebDuLich.Data
{
	[Table("TAIKHOAN")]
	public class TaiKhoan
	{
		[Key]
		[Column("MaTK")]
		[StringLength(10)]
		public string MaTK { get; set; }

		[Column("TenDangNhap")]
		[StringLength(50)]
		public string TenDangNhap { get; set; }

		[Required(ErrorMessage = "Mật khẩu không được để trống")]
		[MinLength(6, ErrorMessage = "Mật khẩu phải có ít nhất 6 ký tự")]
		[Column("MatKhau")]
		public string MatKhau { get; set; }

		[Column("MaQuyen")]
		[StringLength(10)]
		public string MaQuyen { get; set; }

		// Navigation properties
		[ForeignKey("MaQuyen")]
		public QuyenTaiKhoan QuyenTaiKhoan { get; set; }

		public NguoiDung NguoiDung { get; set; }
	}
}
