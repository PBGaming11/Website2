using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Website.Data.Migrations
{
    public partial class khoitao : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "danhmuc",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Namedanhmuc = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<DateTime>(type: "datetime2", nullable: false),
                    imgDanhMuc = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_danhmuc", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "sanpham",
                columns: table => new
                {
                    IdSanPham = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TenSanPham = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ImgDaiDien = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    GiaSanpham = table.Column<double>(type: "float", nullable: false),
                    GiamGia = table.Column<double>(type: "float", nullable: false),
                    MoTaSanPham = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DanhmucId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_sanpham", x => x.IdSanPham);
                    table.ForeignKey(
                        name: "FK_sanpham_danhmuc_DanhmucId",
                        column: x => x.DanhmucId,
                        principalTable: "danhmuc",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_sanpham_DanhmucId",
                table: "sanpham",
                column: "DanhmucId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "sanpham");

            migrationBuilder.DropTable(
                name: "danhmuc");
        }
    }
}
