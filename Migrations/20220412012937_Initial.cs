using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Productos_Backend.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Productos",
                columns: table => new
                {
                    ProductoId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false),
                    Total = table.Column<int>(type: "int", nullable: false, defaultValue: 0)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Productos", x => x.ProductoId);
                });

            migrationBuilder.CreateTable(
                name: "SalidasInventarioEncabezados",
                columns: table => new
                {
                    salidaEncabezadoId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    fecha_Salida = table.Column<DateTime>(type: "Date", nullable: false, defaultValueSql: "GetDate()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SalidasInventarioEncabezados", x => x.salidaEncabezadoId);
                });

            migrationBuilder.CreateTable(
                name: "ProductoLotes",
                columns: table => new
                {
                    LoteId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProductoId = table.Column<int>(type: "int", nullable: false),
                    Fecha_Caducidad = table.Column<DateTime>(type: "Date", nullable: false),
                    Cantidad = table.Column<int>(type: "int", nullable: false, defaultValue: 0),
                    Costo = table.Column<int>(type: "int", nullable: false, defaultValue: 0)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductoLotes", x => x.LoteId);
                    table.ForeignKey(
                        name: "FK_ProductoLotes_Productos_ProductoId",
                        column: x => x.ProductoId,
                        principalTable: "Productos",
                        principalColumn: "ProductoId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SalidaInventarioDetalles",
                columns: table => new
                {
                    salidaDetalleId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    salidaEncabezadoId = table.Column<int>(type: "int", nullable: false),
                    loteId = table.Column<int>(type: "int", nullable: false),
                    ProductoId = table.Column<int>(type: "int", nullable: false),
                    Cantidad = table.Column<int>(type: "int", nullable: false, defaultValue: 0),
                    Costo = table.Column<int>(type: "int", nullable: false, defaultValue: 0),
                    Saldo_Actual = table.Column<int>(type: "int", nullable: false, defaultValue: 0),
                    ProductoLotesLoteId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SalidaInventarioDetalles", x => x.salidaDetalleId);
                    table.ForeignKey(
                        name: "FK_SalidaInventarioDetalles_ProductoLotes_ProductoLotesLoteId",
                        column: x => x.ProductoLotesLoteId,
                        principalTable: "ProductoLotes",
                        principalColumn: "LoteId");
                    table.ForeignKey(
                        name: "FK_SalidaInventarioDetalles_Productos_ProductoId",
                        column: x => x.ProductoId,
                        principalTable: "Productos",
                        principalColumn: "ProductoId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SalidaInventarioDetalles_SalidasInventarioEncabezados_salidaEncabezadoId",
                        column: x => x.salidaEncabezadoId,
                        principalTable: "SalidasInventarioEncabezados",
                        principalColumn: "salidaEncabezadoId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ProductoLotes_ProductoId",
                table: "ProductoLotes",
                column: "ProductoId");

            migrationBuilder.CreateIndex(
                name: "IX_SalidaInventarioDetalles_ProductoId",
                table: "SalidaInventarioDetalles",
                column: "ProductoId");

            migrationBuilder.CreateIndex(
                name: "IX_SalidaInventarioDetalles_ProductoLotesLoteId",
                table: "SalidaInventarioDetalles",
                column: "ProductoLotesLoteId");

            migrationBuilder.CreateIndex(
                name: "IX_SalidaInventarioDetalles_salidaEncabezadoId",
                table: "SalidaInventarioDetalles",
                column: "salidaEncabezadoId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SalidaInventarioDetalles");

            migrationBuilder.DropTable(
                name: "ProductoLotes");

            migrationBuilder.DropTable(
                name: "SalidasInventarioEncabezados");

            migrationBuilder.DropTable(
                name: "Productos");
        }
    }
}
