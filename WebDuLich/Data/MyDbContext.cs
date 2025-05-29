using Microsoft.EntityFrameworkCore;

namespace WebDuLich.Data
{
	public class MyDbContext : DbContext
	{
		public MyDbContext(DbContextOptions<MyDbContext> options) : base(options)
		{
		}

		// DbSet properties
		public DbSet<TaiKhoan> TaiKhoans { get; set; }
		public DbSet<QuyenTaiKhoan> QuyenTaiKhoans { get; set; }
		public DbSet<NguoiDung> NguoiDungs { get; set; }
		public DbSet<Mien> Miens { get; set; }
		public DbSet<Tinh> Tinhs { get; set; }
		public DbSet<KhachSan> KhachSans { get; set; }
		public DbSet<PhongKhachSan> PhongKhachSans { get; set; }
		public DbSet<DatPhong> DatPhongs { get; set; }
		public DbSet<NhaHang> NhaHangs { get; set; }
		public DbSet<BanNhaHang> BanNhaHangs { get; set; }
		public DbSet<DatBan> DatBans { get; set; }
		public DbSet<DiaDiemDuLich> DiaDiemDuLichs { get; set; }
		public DbSet<HinhAnhDiaDiem> HinhAnhDiaDiems { get; set; }
		public DbSet<HinhAnhKhachSan> HinhAnhKhachSans { get; set; }
		public DbSet<HinhAnhNhaHang> HinhAnhNhaHangs { get; set; }
		public DbSet<GioiThieuWebsite> GioiThieuWebsites { get; set; }
		public DbSet<RefreshToken> RefreshTokens { get; set; }

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			base.OnModelCreating(modelBuilder);

			modelBuilder.Entity<RefreshToken>(entity =>
			{
				entity.ToTable("REFRESHTOKEN");
				entity.HasKey(e => e.Id);
				
				entity.HasOne(d => d.TaiKhoan)
					.WithMany()
					.HasForeignKey(d => d.UserId)
					.OnDelete(DeleteBehavior.Cascade);
			});

			// TaiKhoan relationships
			modelBuilder.Entity<TaiKhoan>()
				.HasOne(t => t.QuyenTaiKhoan)
				.WithMany(q => q.TaiKhoans)
				.HasForeignKey(t => t.MaQuyen);

			modelBuilder.Entity<TaiKhoan>()
				.HasOne(t => t.NguoiDung)
				.WithOne(n => n.TaiKhoan)
				.HasForeignKey<NguoiDung>(n => n.MaTV);

			// Mien - Tinh relationships
			modelBuilder.Entity<Tinh>()
				.HasOne(t => t.Mien)
				.WithMany(m => m.Tinhs)
				.HasForeignKey(t => t.MaMien);

			// KhachSan relationships
			modelBuilder.Entity<KhachSan>()
				.HasOne(k => k.TaiKhoan)
				.WithOne()
				.HasForeignKey<KhachSan>(k => k.MaKs);

			modelBuilder.Entity<KhachSan>()
				.HasOne(k => k.Tinh)
				.WithMany(t => t.KhachSans)
				.HasForeignKey(k => k.MaTinh);
			// PhongKhachSan -> KhachSan

			// NhaHang relationships
			modelBuilder.Entity<NhaHang>()
				.HasOne(n => n.TaiKhoan)
				.WithOne()
				.HasForeignKey<NhaHang>(n => n.MaNhaHang);

			modelBuilder.Entity<NhaHang>()
				.HasOne(n => n.Tinh)
				.WithMany()
				.HasForeignKey(n => n.MaTinh);

			// DiaDiemDuLich relationships
			modelBuilder.Entity<DiaDiemDuLich>()
				.HasOne(d => d.TaiKhoan)
				.WithMany()
				.HasForeignKey(d => d.MaTaiKhoan);

			modelBuilder.Entity<DiaDiemDuLich>()
				.HasOne(d => d.Tinh)
				.WithMany()
				.HasForeignKey(d => d.MaTinh);

			// Cascade delete configurations
			modelBuilder.Entity<HinhAnhKhachSan>()
				.HasOne(h => h.KhachSan)
				.WithMany()
				.HasForeignKey(h => h.MaKhachSan)
				.OnDelete(DeleteBehavior.Cascade);

			modelBuilder.Entity<HinhAnhNhaHang>()
				.HasOne(h => h.NhaHang)
				.WithMany()
				.HasForeignKey(h => h.MaNhaHang)
				.OnDelete(DeleteBehavior.Cascade);

			modelBuilder.Entity<HinhAnhDiaDiem>()
				.HasOne(h => h.DiaDiemDuLich)
				.WithMany(d => d.HinhAnhs)
				.HasForeignKey(h => h.MaDiaDiem)
				.OnDelete(DeleteBehavior.Cascade);

			// DatPhong and DatBan configurations
			modelBuilder.Entity<DatPhong>()
				.HasOne(d => d.NguoiDung)
				.WithMany()
				.HasForeignKey(d => d.MaNguoiDung);
			// DatPhong -> PhongKhachSan
			modelBuilder.Entity<DatPhong>()
   				.HasOne(d => d.Phong)
				.WithMany(p => p.DatPhongs)
				.HasForeignKey(d => d.MaPhong)
				.OnDelete(DeleteBehavior.Restrict); // hoặc .NoAction
													// DatBan -> BanNhaHang
			modelBuilder.Entity<DatBan>()
				.HasOne(d => d.Ban)
				.WithMany()
				.HasForeignKey(d => d.MaBan)
				.OnDelete(DeleteBehavior.Restrict); // hoặc .NoAction
			modelBuilder.Entity<GioiThieuWebsite>()
				.HasOne(g => g.NguoiCapNhat)
				.WithMany()
				.HasForeignKey(g => g.IdNguoiCapNhat);
			modelBuilder.Entity<PhongKhachSan>()
				.HasOne(p => p.KhachSan)
				.WithMany(k => k.PhongKhachSans)
				.HasForeignKey(p => p.MaKhachSan);
			modelBuilder.Entity<NguoiDung>()
				.Property(nd => nd.Gioitinh)
				.HasConversion<string>();
			modelBuilder.Entity<PhongKhachSan>()
				.Property(p => p.Gia)
				.HasPrecision(18, 2); // 18 chữ số tổng cộng, 2 chữ số sau dấu phẩy
			modelBuilder.Entity<BanNhaHang>()
				.Property(p => p.Gia)
				.HasPrecision(18, 2); // 18 chữ số tổng cộng, 2 chữ số sau dấu phẩy
			modelBuilder.Entity<RefreshToken>(entity =>
			{
				entity.HasOne(rt => rt.TaiKhoan)
					  .WithMany()
					  .HasForeignKey(rt => rt.UserId)
					  .OnDelete(DeleteBehavior.Cascade);
			});
		}
	}
}
