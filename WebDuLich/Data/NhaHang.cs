using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace WebDuLich.Data
{
	[Table("NhaHang")]
	public class NhaHang
	{
		[Key]
		[Column("Ma_nha_hang")]
		[StringLength(10)]
		public string MaNhaHang { get; set; }

		[Required]
		[Column("ten")]
		[StringLength(100)]
		public string Ten { get; set; }

		[Column("mo_ta")]
		public string MoTa { get; set; }

		[Required]
		[Column("dia_chi")]
		public string DiaChi { get; set; }

		[Required]
		[Column("so_dien_thoai")]
		[StringLength(10)]
		public string SoDienThoai { get; set; }

		[Required]
		[Column("email")]
		[StringLength(50)]
		[EmailAddress]
		public string Email { get; set; }

		[Required]
		[Column("id_tinh")]
		[StringLength(10)]
		public int MaTinh { get; set; }

		[Column("loai_am_thuc")]
		[StringLength(100)]
		public string LoaiAmThuc { get; set; }

		[Column("thoi_gian_mo_cua")]
		public TimeSpan ThoiGianMoCua { get; set; }

		[Column("thoi_gian_dong_cua")]
		public TimeSpan ThoiGianDongCua { get; set; }

		// Navigation properties
		[ForeignKey("MaNhaHang")]
		public TaiKhoan TaiKhoan { get; set; }

		[ForeignKey("MaTinh")]
		public Tinh Tinh { get; set; }
	}
}
