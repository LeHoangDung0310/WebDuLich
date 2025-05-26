using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace WebDuLich.Data
{
	[Table("HINHANHNHAHANG")]
	public class HinhAnhNhaHang
	{
		[Key]
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		[Column("Ma_anh")]
		public int MaAnh { get; set; }

		[Required]
		[Column("Ma_nha_hang")]
		[StringLength(10)]
		public string MaNhaHang { get; set; }

		[Required]
		[Column("duong_dan_anh")]
		public string DuongDanAnh { get; set; }

		// Navigation property
		[ForeignKey("MaNhaHang")]
		public NhaHang NhaHang { get; set; }
	}
}
