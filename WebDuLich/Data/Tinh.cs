using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace WebDuLich.Data
{
	[Table("TINH")]
	public class Tinh
	{
		[Key]
		[Column("Ma_tinh")]
		public int MaTinh { get; set; }

		[Required]
		[Column("ten_tinh")]
		[StringLength(100)]
		public string TenTinh { get; set; }

		[Required]
		[Column("Ma_mien")]
		public int MaMien { get; set; }

		// Navigation properties
		[ForeignKey("MaMien")]
		public virtual Mien Mien { get; set; }
		public virtual ICollection<KhachSan> KhachSans { get; set; }
	}
}
