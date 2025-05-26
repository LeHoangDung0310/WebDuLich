using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace WebDuLich.Data
{
	public enum TrangThaiDatPhong
	{
		ChoXacNhan = 0,
		DaXacNhan = 1,
		DaHuy = 2,
		DaNhanPhong = 3,
		DaTraPhong = 4
	}
	[Table("DatPhong")]
	public class DatPhong
	{
		[Key]
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		[Column("Ma_dat_phong")]
		public int MaDatPhong { get; set; }

		[Required]
		[Column("Ma_nguoi_dung")]
		[StringLength(10)]
		public string MaNguoiDung { get; set; }

		[Required]
		[Column("Ma_phong")]
		public int MaPhong { get; set; }

		[Required]
		[Column("ngay_nhan_phong")]
		[DataType(DataType.Date)]
		public DateTime NgayNhanPhong { get; set; }

		[Required]
		[Column("ngay_tra_phong")]
		[DataType(DataType.Date)]
		public DateTime NgayTraPhong { get; set; }

		[Required]
		[Column("trang_thai")]
		[StringLength(20)]
		public TrangThaiDatPhong TrangThai { get; set; }

		[Column("thoi_gian_dat")]
		[DatabaseGenerated(DatabaseGeneratedOption.Computed)]
		public DateTime ThoiGianDat { get; set; }

		// Navigation properties
		[ForeignKey("MaNguoiDung")]
		public NguoiDung NguoiDung { get; set; }

		[ForeignKey("MaPhong")]
		public PhongKhachSan Phong { get; set; }
	}
}