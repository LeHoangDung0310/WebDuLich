using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace WebDuLich.Data
{
	[Table("DIADIEMDULICH")]
	public class DiaDiemDuLich
	{
		[Key]
		[Column("MaDiaDiem")]
		[StringLength(10)]
		public string MaDiaDiem { get; set; }

		[Required]
		[Column("MaTaiKhoan")]
		[StringLength(10)]
		public string MaTaiKhoan { get; set; }

		[Required]
		[Column("TieuDe")]
		[StringLength(200)]
		public string TieuDe { get; set; }

		[Column("MoTa")]
		public string MoTa { get; set; }

		[Column("MaTinh")]
		[StringLength(10)]
		public string MaTinh { get; set; }

		[Column("NgayTao")]
		[DatabaseGenerated(DatabaseGeneratedOption.Computed)]
		public DateTime NgayTao { get; set; }

		// Navigation properties
		[ForeignKey("MaTaiKhoan")]
		public virtual TaiKhoan TaiKhoan { get; set; }

		[ForeignKey("MaTinh")]
		public Tinh Tinh { get; set; }

		public ICollection<HinhAnhDiaDiem> HinhAnhs { get; set; }
	}
}
