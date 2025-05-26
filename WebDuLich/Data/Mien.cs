using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace WebDuLich.Data
{
	[Table("MIEN")]
	public class Mien
	{
		[Key]
		[Column("Ma_mien")]
		public int MaMien { get; set; }

		[Required]
		[Column("ten_mien")]
		[StringLength(50)]
		public string TenMien { get; set; }

		// Navigation property
		public ICollection<Tinh> Tinhs { get; set; }
	}
}
