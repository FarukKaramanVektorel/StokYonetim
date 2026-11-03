using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace StokYonetim.Inf.Persistance.Migrations
{
    /// <inheritdoc />
    public partial class DomainModeli : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateSequence(
                name: "KategoriIdSirasi",
                incrementBy: 10);

            migrationBuilder.CreateTable(
                name: "Ayarlar",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false),
                    KilitTarihi = table.Column<DateOnly>(type: "date", nullable: true),
                    RowVersion = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Ayarlar", x => x.Id);
                    table.CheckConstraint("CK_Ayarlar_TekSatir", "[Id] = 1");
                });

            migrationBuilder.CreateTable(
                name: "Depolar",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Ad = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Aktif = table.Column<bool>(type: "bit", nullable: false),
                    RowVersion = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Depolar", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Kategoriler",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false),
                    Ad = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    UstKategoriId = table.Column<int>(type: "int", nullable: true),
                    Yol = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: false),
                    Seviye = table.Column<int>(type: "int", nullable: false),
                    RowVersion = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Kategoriler", x => x.Id);
                    table.CheckConstraint("CK_Kategoriler_Seviye", "[Seviye] BETWEEN 1 AND 5");
                    table.ForeignKey(
                        name: "FK_Kategoriler_Kategoriler_UstKategoriId",
                        column: x => x.UstKategoriId,
                        principalTable: "Kategoriler",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Tedarikciler",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Ad = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    Telefon = table.Column<string>(type: "varchar(20)", unicode: false, maxLength: 20, nullable: true),
                    Eposta = table.Column<string>(type: "varchar(150)", unicode: false, maxLength: 150, nullable: true),
                    Aktif = table.Column<bool>(type: "bit", nullable: false),
                    RowVersion = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tedarikciler", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Urunler",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Ad = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    Barkod = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true),
                    Birim = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    SatisFiyati = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    VarsayilanKritikSeviye = table.Column<decimal>(type: "decimal(18,3)", precision: 18, scale: 3, nullable: false),
                    Aktif = table.Column<bool>(type: "bit", nullable: false),
                    KategoriId = table.Column<int>(type: "int", nullable: false),
                    TedarikciId = table.Column<int>(type: "int", nullable: true),
                    RowVersion = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Urunler", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Urunler_Kategoriler_KategoriId",
                        column: x => x.KategoriId,
                        principalTable: "Kategoriler",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Urunler_Tedarikciler_TedarikciId",
                        column: x => x.TedarikciId,
                        principalTable: "Tedarikciler",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "DepoStoklar",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DepoId = table.Column<int>(type: "int", nullable: false),
                    UrunId = table.Column<int>(type: "int", nullable: false),
                    Miktar = table.Column<decimal>(type: "decimal(18,3)", precision: 18, scale: 3, nullable: false),
                    KritikSeviye = table.Column<decimal>(type: "decimal(18,3)", precision: 18, scale: 3, nullable: false),
                    RowVersion = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DepoStoklar", x => x.Id);
                    table.CheckConstraint("CK_DepoStoklar_KritikSeviye", "[KritikSeviye] >= 0");
                    table.CheckConstraint("CK_DepoStoklar_Miktar", "[Miktar] >= 0");
                    table.ForeignKey(
                        name: "FK_DepoStoklar_Depolar_DepoId",
                        column: x => x.DepoId,
                        principalTable: "Depolar",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DepoStoklar_Urunler_UrunId",
                        column: x => x.UrunId,
                        principalTable: "Urunler",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "StokHareketleri",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Tip = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    UrunId = table.Column<int>(type: "int", nullable: false),
                    DepoId = table.Column<int>(type: "int", nullable: false),
                    Miktar = table.Column<decimal>(type: "decimal(18,3)", precision: 18, scale: 3, nullable: false),
                    BirimFiyat = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: true),
                    TederikciId = table.Column<int>(type: "int", nullable: true),
                    TransferId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    IslemTarihi = table.Column<DateOnly>(type: "date", nullable: false),
                    OlusturmaTarihi = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false, defaultValueSql: "SYSDATETIMEOFFSET()"),
                    Aciklama = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    RowVersion = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StokHareketleri", x => x.Id);
                    table.CheckConstraint("CK_StokHareketleri_Miktar", "[Miktar] > 0");
                    table.ForeignKey(
                        name: "FK_StokHareketleri_Depolar_DepoId",
                        column: x => x.DepoId,
                        principalTable: "Depolar",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_StokHareketleri_Tedarikciler_TederikciId",
                        column: x => x.TederikciId,
                        principalTable: "Tedarikciler",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_StokHareketleri_Urunler_UrunId",
                        column: x => x.UrunId,
                        principalTable: "Urunler",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Ayarlar",
                columns: new[] { "Id", "KilitTarihi" },
                values: new object[] { 1, null });

            migrationBuilder.CreateIndex(
                name: "IX_Depolar_Ad",
                table: "Depolar",
                column: "Ad",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_DepoStoklar_DepoId_Miktar",
                table: "DepoStoklar",
                columns: new[] { "DepoId", "Miktar" })
                .Annotation("SqlServer:Include", new[] { "KritikSeviye" });

            migrationBuilder.CreateIndex(
                name: "IX_DepoStoklar_DepoId_UrunId",
                table: "DepoStoklar",
                columns: new[] { "DepoId", "UrunId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_DepoStoklar_UrunId",
                table: "DepoStoklar",
                column: "UrunId");

            migrationBuilder.CreateIndex(
                name: "IX_Kategoriler_UstKategoriId_Ad",
                table: "Kategoriler",
                columns: new[] { "UstKategoriId", "Ad" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Kategoriler_Yol",
                table: "Kategoriler",
                column: "Yol",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_StokHareketleri_DepoId",
                table: "StokHareketleri",
                column: "DepoId");

            migrationBuilder.CreateIndex(
                name: "IX_StokHareketleri_TederikciId",
                table: "StokHareketleri",
                column: "TederikciId");

            migrationBuilder.CreateIndex(
                name: "IX_StokHareketleri_TransferId",
                table: "StokHareketleri",
                column: "TransferId",
                filter: "[TransferId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_StokHareketleri_UrunId_DepoId_IslemTarihi",
                table: "StokHareketleri",
                columns: new[] { "UrunId", "DepoId", "IslemTarihi" });

            migrationBuilder.CreateIndex(
                name: "IX_Tedarikciler_Ad",
                table: "Tedarikciler",
                column: "Ad",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Urunler_Ad",
                table: "Urunler",
                column: "Ad");

            migrationBuilder.CreateIndex(
                name: "IX_Urunler_Barkod",
                table: "Urunler",
                column: "Barkod",
                unique: true,
                filter: "[Barkod] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Urunler_KategoriId",
                table: "Urunler",
                column: "KategoriId");

            migrationBuilder.CreateIndex(
                name: "IX_Urunler_TedarikciId",
                table: "Urunler",
                column: "TedarikciId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Ayarlar");

            migrationBuilder.DropTable(
                name: "DepoStoklar");

            migrationBuilder.DropTable(
                name: "StokHareketleri");

            migrationBuilder.DropTable(
                name: "Depolar");

            migrationBuilder.DropTable(
                name: "Urunler");

            migrationBuilder.DropTable(
                name: "Kategoriler");

            migrationBuilder.DropTable(
                name: "Tedarikciler");

            migrationBuilder.DropSequence(
                name: "KategoriIdSirasi");
        }
    }
}
