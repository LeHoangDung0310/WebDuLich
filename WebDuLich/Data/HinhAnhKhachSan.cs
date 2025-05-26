using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace WebDuLich.Data
{
	[Table("HINHANHKHACHSAN")]
	public class HinhAnhKhachSan
	{
		[Key]
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		[Column("Ma_anh")]
		public int MaAnh { get; set; }

		[Required]
		[Column("MaKS")]
		[StringLength(10)]
		public string MaKhachSan { get; set; }

		[Required]
		[Column("duong_dan_anh")]
		public string DuongDanAnh { get; set; }

		// Navigation property
		[ForeignKey("MaKhachSan")]
		public KhachSan KhachSan { get; set; }
	}
}
