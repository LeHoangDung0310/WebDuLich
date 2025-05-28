using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebDuLich.Migrations
{
    /// <inheritdoc />
    public partial class FixCascadeIssue : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "MIEN",
                columns: table => new
                {
                    Ma_mien = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ten_mien = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MIEN", x => x.Ma_mien);
                });

            migrationBuilder.CreateTable(
                name: "QUYENTAIKHOAN",
                columns: table => new
                {
                    MaQuyen = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    TenQuyen = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_QUYENTAIKHOAN", x => x.MaQuyen);
                });

            migrationBuilder.CreateTable(
                name: "TINH",
                columns: table => new
                {
                    Ma_tinh = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ten_tinh = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Ma_mien = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TINH", x => x.Ma_tinh);
                    table.ForeignKey(
                        name: "FK_TINH_MIEN_Ma_mien",
                        column: x => x.Ma_mien,
                        principalTable: "MIEN",
                        principalColumn: "Ma_mien",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TAIKHOAN",
                columns: table => new
                {
                    MaTK = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    TenDangNhap = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    MatKhau = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MaQuyen = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TAIKHOAN", x => x.MaTK);
                    table.ForeignKey(
                        name: "FK_TAIKHOAN_QUYENTAIKHOAN_MaQuyen",
                        column: x => x.MaQuyen,
                        principalTable: "QUYENTAIKHOAN",
                        principalColumn: "MaQuyen",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "DIADIEMDULICH",
                columns: table => new
                {
                    MaDiaDiem = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    MaTaiKhoan = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    TieuDe = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    MoTa = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MaTinh = table.Column<int>(type: "int", maxLength: 10, nullable: false),
                    NgayTao = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DIADIEMDULICH", x => x.MaDiaDiem);
                    table.ForeignKey(
                        name: "FK_DIADIEMDULICH_TAIKHOAN_MaTaiKhoan",
                        column: x => x.MaTaiKhoan,
                        principalTable: "TAIKHOAN",
                        principalColumn: "MaTK",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DIADIEMDULICH_TINH_MaTinh",
                        column: x => x.MaTinh,
                        principalTable: "TINH",
                        principalColumn: "Ma_tinh",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "GioiThieuWebsite",
                columns: table => new
                {
                    id_gioi_thieu = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    tieu_de = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    noi_dung = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ngay_cap_nhat = table.Column<DateTime>(type: "datetime2", nullable: false),
                    id_nguoi_cap_nhat = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GioiThieuWebsite", x => x.id_gioi_thieu);
                    table.ForeignKey(
                        name: "FK_GioiThieuWebsite_TAIKHOAN_id_nguoi_cap_nhat",
                        column: x => x.id_nguoi_cap_nhat,
                        principalTable: "TAIKHOAN",
                        principalColumn: "MaTK",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "KHACHSAN",
                columns: table => new
                {
                    MaKs = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    Ten = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Mota = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DiaChi = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DienThoai = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Ma_tinh = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_KHACHSAN", x => x.MaKs);
                    table.ForeignKey(
                        name: "FK_KHACHSAN_TAIKHOAN_MaKs",
                        column: x => x.MaKs,
                        principalTable: "TAIKHOAN",
                        principalColumn: "MaTK",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_KHACHSAN_TINH_Ma_tinh",
                        column: x => x.Ma_tinh,
                        principalTable: "TINH",
                        principalColumn: "Ma_tinh",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "NGUOIDUNG",
                columns: table => new
                {
                    MaND = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    HoVaTen = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    NgaySinh = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Gioitinh = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DiaChi = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SoDienThoai = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NGUOIDUNG", x => x.MaND);
                    table.ForeignKey(
                        name: "FK_NGUOIDUNG_TAIKHOAN_MaND",
                        column: x => x.MaND,
                        principalTable: "TAIKHOAN",
                        principalColumn: "MaTK",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "NhaHang",
                columns: table => new
                {
                    Ma_nha_hang = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    ten = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    mo_ta = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    dia_chi = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    so_dien_thoai = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    email = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    id_tinh = table.Column<int>(type: "int", maxLength: 10, nullable: false),
                    loai_am_thuc = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    thoi_gian_mo_cua = table.Column<TimeSpan>(type: "time", nullable: false),
                    thoi_gian_dong_cua = table.Column<TimeSpan>(type: "time", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NhaHang", x => x.Ma_nha_hang);
                    table.ForeignKey(
                        name: "FK_NhaHang_TAIKHOAN_Ma_nha_hang",
                        column: x => x.Ma_nha_hang,
                        principalTable: "TAIKHOAN",
                        principalColumn: "MaTK",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_NhaHang_TINH_id_tinh",
                        column: x => x.id_tinh,
                        principalTable: "TINH",
                        principalColumn: "Ma_tinh",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "HINHANHDIADIEM",
                columns: table => new
                {
                    Ma_anh = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MaDiaDiem = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    duong_dan_anh = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HINHANHDIADIEM", x => x.Ma_anh);
                    table.ForeignKey(
                        name: "FK_HINHANHDIADIEM_DIADIEMDULICH_MaDiaDiem",
                        column: x => x.MaDiaDiem,
                        principalTable: "DIADIEMDULICH",
                        principalColumn: "MaDiaDiem",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "HINHANHKHACHSAN",
                columns: table => new
                {
                    Ma_anh = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MaKS = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    duong_dan_anh = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HINHANHKHACHSAN", x => x.Ma_anh);
                    table.ForeignKey(
                        name: "FK_HINHANHKHACHSAN_KHACHSAN_MaKS",
                        column: x => x.MaKS,
                        principalTable: "KHACHSAN",
                        principalColumn: "MaKs",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PhongKhachSan",
                columns: table => new
                {
                    Ma_phong = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Ma_khach_san = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    loai_phong = table.Column<int>(type: "int", maxLength: 50, nullable: false),
                    Gia = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    suc_chua = table.Column<int>(type: "int", nullable: false),
                    trang_thai = table.Column<int>(type: "int", maxLength: 20, nullable: false),
                    mo_ta = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    dien_tich = table.Column<int>(type: "int", nullable: false),
                    tien_ich = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PhongKhachSan", x => x.Ma_phong);
                    table.ForeignKey(
                        name: "FK_PhongKhachSan_KHACHSAN_Ma_khach_san",
                        column: x => x.Ma_khach_san,
                        principalTable: "KHACHSAN",
                        principalColumn: "MaKs",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "BANNHAHANG",
                columns: table => new
                {
                    MaBan = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    MaNhaHang = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    SoNguoiToiDa = table.Column<int>(type: "int", nullable: false),
                    LoaiBan = table.Column<int>(type: "int", maxLength: 50, nullable: false),
                    MoTa = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Gia = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BANNHAHANG", x => x.MaBan);
                    table.ForeignKey(
                        name: "FK_BANNHAHANG_NhaHang_MaNhaHang",
                        column: x => x.MaNhaHang,
                        principalTable: "NhaHang",
                        principalColumn: "Ma_nha_hang",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "HINHANHNHAHANG",
                columns: table => new
                {
                    Ma_anh = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Ma_nha_hang = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    duong_dan_anh = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HINHANHNHAHANG", x => x.Ma_anh);
                    table.ForeignKey(
                        name: "FK_HINHANHNHAHANG_NhaHang_Ma_nha_hang",
                        column: x => x.Ma_nha_hang,
                        principalTable: "NhaHang",
                        principalColumn: "Ma_nha_hang",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "DatPhong",
                columns: table => new
                {
                    Ma_dat_phong = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Ma_nguoi_dung = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    Ma_phong = table.Column<int>(type: "int", nullable: false),
                    ngay_nhan_phong = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ngay_tra_phong = table.Column<DateTime>(type: "datetime2", nullable: false),
                    trang_thai = table.Column<int>(type: "int", maxLength: 20, nullable: false),
                    thoi_gian_dat = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DatPhong", x => x.Ma_dat_phong);
                    table.ForeignKey(
                        name: "FK_DatPhong_NGUOIDUNG_Ma_nguoi_dung",
                        column: x => x.Ma_nguoi_dung,
                        principalTable: "NGUOIDUNG",
                        principalColumn: "MaND",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DatPhong_PhongKhachSan_Ma_phong",
                        column: x => x.Ma_phong,
                        principalTable: "PhongKhachSan",
                        principalColumn: "Ma_phong",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "DATBAN",
                columns: table => new
                {
                    MaDatBan = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    MaNguoiDung = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    MaBan = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    SoNguoi = table.Column<int>(type: "int", nullable: false),
                    NgayGioDat = table.Column<DateTime>(type: "datetime2", nullable: false),
                    YeuCauDacBiet = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TrangThai = table.Column<int>(type: "int", maxLength: 20, nullable: false),
                    BanNhaHangMaBan = table.Column<string>(type: "nvarchar(10)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DATBAN", x => x.MaDatBan);
                    table.ForeignKey(
                        name: "FK_DATBAN_BANNHAHANG_BanNhaHangMaBan",
                        column: x => x.BanNhaHangMaBan,
                        principalTable: "BANNHAHANG",
                        principalColumn: "MaBan");
                    table.ForeignKey(
                        name: "FK_DATBAN_BANNHAHANG_MaBan",
                        column: x => x.MaBan,
                        principalTable: "BANNHAHANG",
                        principalColumn: "MaBan",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_DATBAN_TAIKHOAN_MaNguoiDung",
                        column: x => x.MaNguoiDung,
                        principalTable: "TAIKHOAN",
                        principalColumn: "MaTK",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_BANNHAHANG_MaNhaHang",
                table: "BANNHAHANG",
                column: "MaNhaHang");

            migrationBuilder.CreateIndex(
                name: "IX_DATBAN_BanNhaHangMaBan",
                table: "DATBAN",
                column: "BanNhaHangMaBan");

            migrationBuilder.CreateIndex(
                name: "IX_DATBAN_MaBan",
                table: "DATBAN",
                column: "MaBan");

            migrationBuilder.CreateIndex(
                name: "IX_DATBAN_MaNguoiDung",
                table: "DATBAN",
                column: "MaNguoiDung");

            migrationBuilder.CreateIndex(
                name: "IX_DatPhong_Ma_nguoi_dung",
                table: "DatPhong",
                column: "Ma_nguoi_dung");

            migrationBuilder.CreateIndex(
                name: "IX_DatPhong_Ma_phong",
                table: "DatPhong",
                column: "Ma_phong");

            migrationBuilder.CreateIndex(
                name: "IX_DIADIEMDULICH_MaTaiKhoan",
                table: "DIADIEMDULICH",
                column: "MaTaiKhoan");

            migrationBuilder.CreateIndex(
                name: "IX_DIADIEMDULICH_MaTinh",
                table: "DIADIEMDULICH",
                column: "MaTinh");

            migrationBuilder.CreateIndex(
                name: "IX_GioiThieuWebsite_id_nguoi_cap_nhat",
                table: "GioiThieuWebsite",
                column: "id_nguoi_cap_nhat");

            migrationBuilder.CreateIndex(
                name: "IX_HINHANHDIADIEM_MaDiaDiem",
                table: "HINHANHDIADIEM",
                column: "MaDiaDiem");

            migrationBuilder.CreateIndex(
                name: "IX_HINHANHKHACHSAN_MaKS",
                table: "HINHANHKHACHSAN",
                column: "MaKS");

            migrationBuilder.CreateIndex(
                name: "IX_HINHANHNHAHANG_Ma_nha_hang",
                table: "HINHANHNHAHANG",
                column: "Ma_nha_hang");

            migrationBuilder.CreateIndex(
                name: "IX_KHACHSAN_Ma_tinh",
                table: "KHACHSAN",
                column: "Ma_tinh");

            migrationBuilder.CreateIndex(
                name: "IX_NhaHang_id_tinh",
                table: "NhaHang",
                column: "id_tinh");

            migrationBuilder.CreateIndex(
                name: "IX_PhongKhachSan_Ma_khach_san",
                table: "PhongKhachSan",
                column: "Ma_khach_san");

            migrationBuilder.CreateIndex(
                name: "IX_TAIKHOAN_MaQuyen",
                table: "TAIKHOAN",
                column: "MaQuyen");

            migrationBuilder.CreateIndex(
                name: "IX_TINH_Ma_mien",
                table: "TINH",
                column: "Ma_mien");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DATBAN");

            migrationBuilder.DropTable(
                name: "DatPhong");

            migrationBuilder.DropTable(
                name: "GioiThieuWebsite");

            migrationBuilder.DropTable(
                name: "HINHANHDIADIEM");

            migrationBuilder.DropTable(
                name: "HINHANHKHACHSAN");

            migrationBuilder.DropTable(
                name: "HINHANHNHAHANG");

            migrationBuilder.DropTable(
                name: "BANNHAHANG");

            migrationBuilder.DropTable(
                name: "NGUOIDUNG");

            migrationBuilder.DropTable(
                name: "PhongKhachSan");

            migrationBuilder.DropTable(
                name: "DIADIEMDULICH");

            migrationBuilder.DropTable(
                name: "NhaHang");

            migrationBuilder.DropTable(
                name: "KHACHSAN");

            migrationBuilder.DropTable(
                name: "TAIKHOAN");

            migrationBuilder.DropTable(
                name: "TINH");

            migrationBuilder.DropTable(
                name: "QUYENTAIKHOAN");

            migrationBuilder.DropTable(
                name: "MIEN");
        }
    }
}
