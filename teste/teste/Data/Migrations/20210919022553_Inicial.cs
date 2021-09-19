using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace teste.Data.Migrations
{
    public partial class Inicial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Fotografia",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Nome",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "RegisterTime",
                table: "AspNetUsers",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.CreateTable(
                name: "Bebidas",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nome = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Descricao = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Preco = table.Column<float>(type: "real", nullable: false),
                    Imagem = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Stock = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Bebidas", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Clientes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nome = table.Column<string>(type: "nvarchar(60)", maxLength: 60, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Contacto = table.Column<int>(type: "int", maxLength: 11, nullable: false),
                    Datanasc = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Fotografia = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Username = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Clientes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Reservas",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DataReserva = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DataEntrega = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Estado = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Quantidade = table.Column<int>(type: "int", nullable: false),
                    BebidaFK = table.Column<int>(type: "int", nullable: false),
                    ClienteFK = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Reservas", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Reservas_Bebidas_BebidaFK",
                        column: x => x.BebidaFK,
                        principalTable: "Bebidas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Reservas_Clientes_ClienteFK",
                        column: x => x.ClienteFK,
                        principalTable: "Clientes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Bebidas",
                columns: new[] { "Id", "Descricao", "Imagem", "Nome", "Preco", "Stock" },
                values: new object[,]
                {
                    { 1, " MATEUS ROSÉ é um vinho leve, fresco, jovem e ligeiramente «pétillant»", null, "Vinho Rose Mateus", 20f, "Sim" },
                    { 2, "É vinificado pelo método tradicional do vinho do Porto.", null, "Vinho do Porto Ferreira", 30f, "Sim" },
                    { 3, "Vinho Moscatel de Setúbal", null, "Veritas Moscatel", 10f, "Sim" },
                    { 4, "Grant’s é um whisky extraordinário e um dos mais complexos produzidos na Escócia.", null, "Grants Whisky", 15f, "Sim" },
                    { 5, "Cîroc Vodka é uma marca de vodca eau-de-vie de luxo, fabricada com uvas da região Carântono-Marítimo, da França", null, "Ciroc Vodka", 30f, "Sim" },
                    { 6, "Nada bate um original, e Malibu não é apenas o original, é o mais vendido rum do Caribe com sabor natural de coco ", null, "Malibu Rum", 15f, "Sim" },
                    { 7, "Cachaça, o sabor e aroma perfeito da original caipirinha brasileira.", null, "Cachaça 51", 11f, "Sim" },
                    { 8, "Moet & Chandon, um champanhe de estilo único e elegante.", null, "Moet&Chandon", 50f, "Sim" },
                    { 9, "O sabor autêntico. Super Bock Original é a única cerveja portuguesa com 37 medalhas de ouro consecutivas", null, "Super Bock Pack15", 7f, "Sim" }
                });

            migrationBuilder.InsertData(
                table: "Clientes",
                columns: new[] { "Id", "Contacto", "Datanasc", "Email", "Fotografia", "Nome", "Username" },
                values: new object[,]
                {
                    { 1, 937492122, new DateTime(1995, 6, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), "pr@pr.com", "noUser.png", "Pedro Rafael", null },
                    { 2, 920562956, new DateTime(1994, 12, 9, 0, 0, 0, 0, DateTimeKind.Unspecified), "jv@jv.com", "noUser.png", "Jose Vieira", null },
                    { 3, 914659935, new DateTime(1999, 5, 21, 0, 0, 0, 0, DateTimeKind.Unspecified), "ms@ms.com", "noUser.png", "Maria Silva", null },
                    { 4, 936581003, new DateTime(1990, 8, 7, 0, 0, 0, 0, DateTimeKind.Unspecified), "fs@fs.com", "noUser.png", "Filipe Santos", null },
                    { 5, 962813384, new DateTime(1998, 2, 9, 0, 0, 0, 0, DateTimeKind.Unspecified), "as@as.com", "noUser.png", "Ana Sousa", null },
                    { 6, 961883421, new DateTime(1985, 10, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), "bp@bp.com", "noUser.png", "Beatriz Pinto", null },
                    { 7, 917745362, new DateTime(1978, 3, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), "tm@tm.com", "noUser.png", "Tiago Mendonça", null }
                });

            migrationBuilder.InsertData(
                table: "Reservas",
                columns: new[] { "Id", "BebidaFK", "ClienteFK", "DataEntrega", "DataReserva", "Estado", "Quantidade" },
                values: new object[,]
                {
                    { 1, 1, 1, new DateTime(2022, 6, 19, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2021, 9, 19, 3, 25, 52, 491, DateTimeKind.Local).AddTicks(4437), "Entregue", 2 },
                    { 2, 2, 2, new DateTime(2021, 12, 19, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2021, 9, 19, 3, 25, 52, 494, DateTimeKind.Local).AddTicks(8436), "Entregue", 1 },
                    { 11, 9, 2, new DateTime(2022, 3, 8, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2021, 9, 19, 3, 25, 52, 494, DateTimeKind.Local).AddTicks(8498), "Entregue", 4 },
                    { 3, 3, 3, new DateTime(2022, 5, 23, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2021, 9, 19, 3, 25, 52, 494, DateTimeKind.Local).AddTicks(8473), "Entregue", 1 },
                    { 8, 8, 3, new DateTime(2022, 6, 24, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2021, 9, 19, 3, 25, 52, 494, DateTimeKind.Local).AddTicks(8494), "Entregue", 1 },
                    { 4, 4, 4, new DateTime(2022, 8, 9, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2021, 9, 19, 3, 25, 52, 494, DateTimeKind.Local).AddTicks(8478), "Entregue", 1 },
                    { 5, 5, 5, new DateTime(2021, 10, 23, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2021, 9, 19, 3, 25, 52, 494, DateTimeKind.Local).AddTicks(8482), "Entregue", 5 },
                    { 6, 6, 6, new DateTime(2021, 12, 3, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2021, 9, 19, 3, 25, 52, 494, DateTimeKind.Local).AddTicks(8486), "Entregue", 2 },
                    { 7, 7, 7, new DateTime(2022, 2, 13, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2021, 9, 19, 3, 25, 52, 494, DateTimeKind.Local).AddTicks(8490), "Entregue", 3 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Reservas_BebidaFK",
                table: "Reservas",
                column: "BebidaFK");

            migrationBuilder.CreateIndex(
                name: "IX_Reservas_ClienteFK",
                table: "Reservas",
                column: "ClienteFK");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Reservas");

            migrationBuilder.DropTable(
                name: "Bebidas");

            migrationBuilder.DropTable(
                name: "Clientes");

            migrationBuilder.DropColumn(
                name: "Fotografia",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "Nome",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "RegisterTime",
                table: "AspNetUsers");
        }
    }
}
