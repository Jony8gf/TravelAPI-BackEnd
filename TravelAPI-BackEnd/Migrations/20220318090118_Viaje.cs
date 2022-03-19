using Microsoft.EntityFrameworkCore.Migrations;
using NetTopologySuite.Geometries;

namespace TravelAPI_BackEnd.Migrations
{
    public partial class Viaje : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_TipoActividad",
                table: "TipoActividad");

            migrationBuilder.RenameTable(
                name: "TipoActividad",
                newName: "TipoActividades");

            migrationBuilder.AddPrimaryKey(
                name: "PK_TipoActividades",
                table: "TipoActividades",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "Viajes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Pais = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    Lugar = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Descripcion = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true),
                    Foto = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Ubicacion = table.Column<Point>(type: "geography", nullable: true),
                    Precio = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Viajes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ViajesPromociones",
                columns: table => new
                {
                    PromocionId = table.Column<int>(type: "int", nullable: false),
                    ViajeId = table.Column<int>(type: "int", nullable: false),
                    PrecioConDescuento = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ViajesPromociones", x => new { x.PromocionId, x.ViajeId });
                    table.ForeignKey(
                        name: "FK_ViajesPromociones_Promociones_PromocionId",
                        column: x => x.PromocionId,
                        principalTable: "Promociones",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ViajesPromociones_Viajes_ViajeId",
                        column: x => x.ViajeId,
                        principalTable: "Viajes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ViajesTipoActividades",
                columns: table => new
                {
                    TipoActividadId = table.Column<int>(type: "int", nullable: false),
                    ViajeId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ViajesTipoActividades", x => new { x.ViajeId, x.TipoActividadId });
                    table.ForeignKey(
                        name: "FK_ViajesTipoActividades_TipoActividades_TipoActividadId",
                        column: x => x.TipoActividadId,
                        principalTable: "TipoActividades",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ViajesTipoActividades_Viajes_ViajeId",
                        column: x => x.ViajeId,
                        principalTable: "Viajes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ViajesPromociones_ViajeId",
                table: "ViajesPromociones",
                column: "ViajeId");

            migrationBuilder.CreateIndex(
                name: "IX_ViajesTipoActividades_TipoActividadId",
                table: "ViajesTipoActividades",
                column: "TipoActividadId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ViajesPromociones");

            migrationBuilder.DropTable(
                name: "ViajesTipoActividades");

            migrationBuilder.DropTable(
                name: "Viajes");

            migrationBuilder.DropPrimaryKey(
                name: "PK_TipoActividades",
                table: "TipoActividades");

            migrationBuilder.RenameTable(
                name: "TipoActividades",
                newName: "TipoActividad");

            migrationBuilder.AddPrimaryKey(
                name: "PK_TipoActividad",
                table: "TipoActividad",
                column: "Id");
        }
    }
}
