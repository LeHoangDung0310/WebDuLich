using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace WebDuLich.Data
{
	public enum LoaiPhongKhachSan
	{
		Thuong = 0,
		CaoCap = 1,
		GiaDinh = 2
	}

	public enum TrangThaiPhong
	{
		ConTrong = 0,
		DaDat = 1,
		DangBaoTri = 2,
		KhongSuDung = 3
	}

	[Table("PhongKhachSan")]
	public class PhongKhachSan
	{
		[Key]
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		[Column("Ma_phong")]
		public int MaPhong { get; set; }

		[Required]
		[Column("Ma_khach_san")]
		[StringLength(10)]
		public string MaKhachSan { get; set; }

		[Required]
		[Column("loai_phong")]
		[StringLength(50)]
		public LoaiPhongKhachSan LoaiPhong { get; set; }

		[Column("gia")]
		[DataType(DataType.Currency)]
		public decimal Gia { get; set; }

		[Column("suc_chua")]
		public int SucChua { get; set; }

		[Column("trang_thai")]
		[StringLength(20)]
		public TrangThaiPhong TrangThai { get; set; }

		[Column("mo_ta")]
		public string MoTa { get; set; }

		[Column("dien_tich")]
		public int DienTich { get; set; }

		[Column("tien_ich")]
		public string TienIch { get; set; }

		// Navigation properties
		[ForeignKey("MaKhachSan")]
		public KhachSan KhachSan { get; set; }
		public ICollection<DatPhong> DatPhongs { get; set; }
	}
}
