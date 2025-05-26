using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace WebDuLich.Data
{
	[Table("HINHANHDIADIEM")]
	public class HinhAnhDiaDiem
	{
		[Key]
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		[Column("Ma_anh")]
		public int MaAnh { get; set; }

		[Required]
		[Column("MaDiaDiem")]
		[StringLength(10)]
		public string MaDiaDiem { get; set; }

		[Required]
		[Column("duong_dan_anh")]
		public string DuongDanAnh { get; set; }

		// Navigation property
		[ForeignKey("MaDiaDiem")]
		public DiaDiemDuLich DiaDiemDuLich { get; set; }
	}
}