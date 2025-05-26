using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace WebDuLich.Data
{
	[Table("QUYENTAIKHOAN")]
	public class QuyenTaiKhoan
	{
		[Key]
		[Column("MaQuyen")]
		[StringLength(10)]
		public string MaQuyen { get; set; }

		[Column("TenQuyen")]
		[StringLength(50)]
		public string TenQuyen { get; set; }

		// Navigation property
		public ICollection<TaiKhoan> TaiKhoans { get; set; }
	}
}
