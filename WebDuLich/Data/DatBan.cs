using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace WebDuLich.Data
{
	public enum TrangThaiDatBan
	{
		ChoXuLy = 0,
		DaXacNhan = 1,
		HoanTat = 2,
		DaHuy = 3
	}
	[Table("DATBAN")]
	public class DatBan
	{
		[Key]
		[Column("MaDatBan")]
		[StringLength(10)]
		public string MaDatBan { get; set; }

		[Required]
		[Column("MaNguoiDung")]
		[StringLength(10)]
		public string MaNguoiDung { get; set; }

		[Required]
		[Column("MaBan")]
		[StringLength(10)]
		public string MaBan { get; set; }

		[Required]
		[Column("SoNguoi")]
		[Range(1, int.MaxValue)]
		public int SoNguoi { get; set; }

		[Required]
		[Column("NgayGioDat")]
		public DateTime NgayGioDat { get; set; }

		[Column("YeuCauDacBiet")]
		public string YeuCauDacBiet { get; set; }

		[Required]
		[Column("TrangThai")]
		[StringLength(20)]
		public TrangThaiDatBan TrangThai { get; set; }  // chờ xử lý, xác nhận, hoàn tất, đã hủy

		// Navigation properties
		[ForeignKey("MaNguoiDung")]
		public TaiKhoan NguoiDung { get; set; }

		[ForeignKey("MaBan")]
		public BanNhaHang Ban { get; set; }
	}
}
