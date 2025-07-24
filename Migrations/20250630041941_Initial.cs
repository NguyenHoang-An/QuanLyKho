using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Client.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "DonViTinhs",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Ten_DonViTinh = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Ghi_Chu = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DonViTinhs", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Khos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Ten_Kho = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Ghi_Chu = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Khos", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "KhoUsers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Ma_Dang_Nhap = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Kho_ID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_KhoUsers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "LoaiSanPhams",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Ma_LSP = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Ten_LSP = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Ghi_Chu = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LoaiSanPhams", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "NhaCungCaps",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Ma_NCC = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Ten_NCC = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Ghi_Chu = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NhaCungCaps", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "tbl_XNK_Xuat_Kho",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    So_Phieu_Xuat_Kho = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Kho_ID = table.Column<int>(type: "int", nullable: false),
                    Ngay_Xuat_Kho = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Ghi_Chu = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbl_XNK_Xuat_Kho", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "XuatKho",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    So_Phieu_Xuat_Kho = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Kho_ID = table.Column<int>(type: "int", nullable: false),
                    Ngay_Xuat_Kho = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Ghi_Chu = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_XuatKho", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SanPhams",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Ma_SP = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Ten_SP = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DonViTinhId = table.Column<int>(type: "int", nullable: false),
                    LoaiSanPhamId = table.Column<int>(type: "int", nullable: false),
                    Ghi_Chu = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SanPhams", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SanPhams_DonViTinhs_DonViTinhId",
                        column: x => x.DonViTinhId,
                        principalTable: "DonViTinhs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SanPhams_LoaiSanPhams_LoaiSanPhamId",
                        column: x => x.LoaiSanPhamId,
                        principalTable: "LoaiSanPhams",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "HieuChinhThongTinPhieuNhaps",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    So_Phieu_Nhap_Kho = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Kho_ID = table.Column<int>(type: "int", nullable: true),
                    NCC_ID = table.Column<int>(type: "int", nullable: true),
                    Ngay_Nhap_Kho = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Ghi_Chu = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HieuChinhThongTinPhieuNhaps", x => x.Id);
                    table.ForeignKey(
                        name: "FK_HieuChinhThongTinPhieuNhaps_Khos_Kho_ID",
                        column: x => x.Kho_ID,
                        principalTable: "Khos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_HieuChinhThongTinPhieuNhaps_NhaCungCaps_NCC_ID",
                        column: x => x.NCC_ID,
                        principalTable: "NhaCungCaps",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "NhapKhos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    So_Phieu_Nhap_Kho = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Kho_ID = table.Column<int>(type: "int", nullable: false),
                    NCC_ID = table.Column<int>(type: "int", nullable: false),
                    Ngay_Nhap_Kho = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Ghi_Chu = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    KhoId = table.Column<int>(type: "int", nullable: true),
                    NhaCungCapId = table.Column<int>(type: "int", nullable: true),
                    Ten_Kho = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Ten_NCC = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NhapKhos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_NhapKhos_Khos_KhoId",
                        column: x => x.KhoId,
                        principalTable: "Khos",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_NhapKhos_NhaCungCaps_NhaCungCapId",
                        column: x => x.NhaCungCapId,
                        principalTable: "NhaCungCaps",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "XuatKhoRaw",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Xuat_Kho_ID = table.Column<int>(type: "int", nullable: false),
                    San_Pham_ID = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SL_Xuat = table.Column<int>(type: "int", nullable: false),
                    Don_Gia_Xuat = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_XuatKhoRaw", x => x.Id);
                    table.ForeignKey(
                        name: "FK_XuatKhoRaw_XuatKho_Xuat_Kho_ID",
                        column: x => x.Xuat_Kho_ID,
                        principalTable: "XuatKho",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "NhapKhoRawDatas",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nhap_Kho_ID = table.Column<int>(type: "int", nullable: false),
                    San_Pham_ID = table.Column<int>(type: "int", nullable: false),
                    SL_Nhap = table.Column<int>(type: "int", nullable: false),
                    Don_Gia_Nhap = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    Ghi_Chu = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NhapKhoRawDatas", x => x.Id);
                    table.ForeignKey(
                        name: "FK_NhapKhoRawDatas_NhapKhos_Nhap_Kho_ID",
                        column: x => x.Nhap_Kho_ID,
                        principalTable: "NhapKhos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_NhapKhoRawDatas_SanPhams_San_Pham_ID",
                        column: x => x.San_Pham_ID,
                        principalTable: "SanPhams",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_DonViTinhs_Ten_DonViTinh",
                table: "DonViTinhs",
                column: "Ten_DonViTinh",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_HieuChinhThongTinPhieuNhaps_Kho_ID",
                table: "HieuChinhThongTinPhieuNhaps",
                column: "Kho_ID");

            migrationBuilder.CreateIndex(
                name: "IX_HieuChinhThongTinPhieuNhaps_NCC_ID",
                table: "HieuChinhThongTinPhieuNhaps",
                column: "NCC_ID");

            migrationBuilder.CreateIndex(
                name: "IX_Khos_Ten_Kho",
                table: "Khos",
                column: "Ten_Kho",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_KhoUsers_Ma_Dang_Nhap_Kho_ID",
                table: "KhoUsers",
                columns: new[] { "Ma_Dang_Nhap", "Kho_ID" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_LoaiSanPhams_Ma_LSP",
                table: "LoaiSanPhams",
                column: "Ma_LSP",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_LoaiSanPhams_Ten_LSP",
                table: "LoaiSanPhams",
                column: "Ten_LSP",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_NhaCungCaps_Ten_NCC",
                table: "NhaCungCaps",
                column: "Ten_NCC",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_NhapKhoRawDatas_Nhap_Kho_ID",
                table: "NhapKhoRawDatas",
                column: "Nhap_Kho_ID");

            migrationBuilder.CreateIndex(
                name: "IX_NhapKhoRawDatas_San_Pham_ID",
                table: "NhapKhoRawDatas",
                column: "San_Pham_ID");

            migrationBuilder.CreateIndex(
                name: "IX_NhapKhos_KhoId",
                table: "NhapKhos",
                column: "KhoId");

            migrationBuilder.CreateIndex(
                name: "IX_NhapKhos_NhaCungCapId",
                table: "NhapKhos",
                column: "NhaCungCapId");

            migrationBuilder.CreateIndex(
                name: "IX_NhapKhos_So_Phieu_Nhap_Kho",
                table: "NhapKhos",
                column: "So_Phieu_Nhap_Kho",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_SanPhams_DonViTinhId",
                table: "SanPhams",
                column: "DonViTinhId");

            migrationBuilder.CreateIndex(
                name: "IX_SanPhams_LoaiSanPhamId",
                table: "SanPhams",
                column: "LoaiSanPhamId");

            migrationBuilder.CreateIndex(
                name: "IX_SanPhams_Ma_SP",
                table: "SanPhams",
                column: "Ma_SP",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_XuatKho_So_Phieu_Xuat_Kho",
                table: "XuatKho",
                column: "So_Phieu_Xuat_Kho",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_XuatKhoRaw_Xuat_Kho_ID",
                table: "XuatKhoRaw",
                column: "Xuat_Kho_ID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "HieuChinhThongTinPhieuNhaps");

            migrationBuilder.DropTable(
                name: "KhoUsers");

            migrationBuilder.DropTable(
                name: "NhapKhoRawDatas");

            migrationBuilder.DropTable(
                name: "tbl_XNK_Xuat_Kho");

            migrationBuilder.DropTable(
                name: "XuatKhoRaw");

            migrationBuilder.DropTable(
                name: "NhapKhos");

            migrationBuilder.DropTable(
                name: "SanPhams");

            migrationBuilder.DropTable(
                name: "XuatKho");

            migrationBuilder.DropTable(
                name: "Khos");

            migrationBuilder.DropTable(
                name: "NhaCungCaps");

            migrationBuilder.DropTable(
                name: "DonViTinhs");

            migrationBuilder.DropTable(
                name: "LoaiSanPhams");
        }
    }
}
