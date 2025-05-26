using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace WebDuLich.Data
{
	public enum LoaiBanEnum
	{
		Thuong = 0,
		VIP = 1,
		NgoaiTroi = 2,
		PhongRieng = 3
	}
	[Table("BANNHAHANG")]
	public class BanNhaHang
	{
		[Key]
		[Column("MaBan")]
		[StringLength(10)]
		public string MaBan { get; set; }

		[Required]
		[Column("MaNhaHang")]
		[StringLength(10)]
		public string MaNhaHang { get; set; }

		[Required]
		[Column("SoNguoiToiDa")]
		[Range(1, int.MaxValue)]
		public int SoNguoiToiDa { get; set; }

		[Required]
		[Column("LoaiBan")]
		[StringLength(50)]
		public LoaiBanEnum LoaiBan { get; set; }  // VIP, Thường, Ngoài trời, Phòng riêng

		[Column("MoTa")]
		public string MoTa { get; set; }

		// Navigation properties
		[ForeignKey("MaNhaHang")]
		public NhaHang NhaHang { get; set; }
		public ICollection<DatBan> DatBans { get; set; }
	}
}
