using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace teste.Data.Migrations
{
    public partial class SeedBebida : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Reservas",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Reservas",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Reservas",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Reservas",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Reservas",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Reservas",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Reservas",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "Reservas",
                keyColumn: "Id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "Reservas",
                keyColumn: "Id",
                keyValue: 11);

            migrationBuilder.DeleteData(
                table: "Clientes",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Clientes",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Clientes",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Clientes",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Clientes",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Clientes",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Clientes",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DropColumn(
                name: "Estado",
                table: "Reservas");

            migrationBuilder.UpdateData(
                table: "Bebidas",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Imagem", "Preco" },
                values: new object[] { "Vinho-Mateus-Rose.jpg", 20.55f });

            migrationBuilder.UpdateData(
                table: "Bebidas",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "Imagem", "Preco" },
                values: new object[] { "ferreira_Porto.jpg", 30.25f });

            migrationBuilder.UpdateData(
                table: "Bebidas",
                keyColumn: "Id",
                keyValue: 3,
                column: "Imagem",
                value: "veritas_moscatel.jpg");

            migrationBuilder.UpdateData(
                table: "Bebidas",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "Imagem", "Preco" },
                values: new object[] { "grants_whisky.jpg", 15.1f });

            migrationBuilder.UpdateData(
                table: "Bebidas",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "Imagem", "Preco" },
                values: new object[] { "ciroc_vodka.jpg", 30.5f });

            migrationBuilder.UpdateData(
                table: "Bebidas",
                keyColumn: "Id",
                keyValue: 6,
                column: "Imagem",
                value: "malibu_rum.jpg");

            migrationBuilder.UpdateData(
                table: "Bebidas",
                keyColumn: "Id",
                keyValue: 7,
                column: "Imagem",
                value: "51_cachaça.jpg");

            migrationBuilder.UpdateData(
                table: "Bebidas",
                keyColumn: "Id",
                keyValue: 8,
                column: "Imagem",
                value: "moet_champanhe.jpg");

            migrationBuilder.UpdateData(
                table: "Bebidas",
                keyColumn: "Id",
                keyValue: 9,
                columns: new[] { "Imagem", "Preco" },
                values: new object[] { "superBock.jpg", 7.99f });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Estado",
                table: "Reservas",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "Bebidas",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Imagem", "Preco" },
                values: new object[] { null, 20f });

            migrationBuilder.UpdateData(
                table: "Bebidas",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "Imagem", "Preco" },
                values: new object[] { null, 30f });

            migrationBuilder.UpdateData(
                table: "Bebidas",
                keyColumn: "Id",
                keyValue: 3,
                column: "Imagem",
                value: null);

            migrationBuilder.UpdateData(
                table: "Bebidas",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "Imagem", "Preco" },
                values: new object[] { null, 15f });

            migrationBuilder.UpdateData(
                table: "Bebidas",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "Imagem", "Preco" },
                values: new object[] { null, 30f });

            migrationBuilder.UpdateData(
                table: "Bebidas",
                keyColumn: "Id",
                keyValue: 6,
                column: "Imagem",
                value: null);

            migrationBuilder.UpdateData(
                table: "Bebidas",
                keyColumn: "Id",
                keyValue: 7,
                column: "Imagem",
                value: null);

            migrationBuilder.UpdateData(
                table: "Bebidas",
                keyColumn: "Id",
                keyValue: 8,
                column: "Imagem",
                value: null);

            migrationBuilder.UpdateData(
                table: "Bebidas",
                keyColumn: "Id",
                keyValue: 9,
                columns: new[] { "Imagem", "Preco" },
                values: new object[] { null, 7f });

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
                    { 1, 1, 1, new DateTime(2022, 6, 19, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2021, 9, 19, 3, 35, 30, 744, DateTimeKind.Local).AddTicks(8397), "Entregue", 2 },
                    { 2, 2, 2, new DateTime(2021, 12, 19, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2021, 9, 19, 3, 35, 30, 749, DateTimeKind.Local).AddTicks(4317), "Entregue", 1 },
                    { 11, 9, 2, new DateTime(2022, 3, 8, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2021, 9, 19, 3, 35, 30, 749, DateTimeKind.Local).AddTicks(4384), "Entregue", 4 },
                    { 3, 3, 3, new DateTime(2022, 5, 23, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2021, 9, 19, 3, 35, 30, 749, DateTimeKind.Local).AddTicks(4360), "Entregue", 1 },
                    { 8, 8, 3, new DateTime(2022, 6, 24, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2021, 9, 19, 3, 35, 30, 749, DateTimeKind.Local).AddTicks(4380), "Entregue", 1 },
                    { 4, 4, 4, new DateTime(2022, 8, 9, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2021, 9, 19, 3, 35, 30, 749, DateTimeKind.Local).AddTicks(4365), "Entregue", 1 },
                    { 5, 5, 5, new DateTime(2021, 10, 23, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2021, 9, 19, 3, 35, 30, 749, DateTimeKind.Local).AddTicks(4369), "Entregue", 5 },
                    { 6, 6, 6, new DateTime(2021, 12, 3, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2021, 9, 19, 3, 35, 30, 749, DateTimeKind.Local).AddTicks(4373), "Entregue", 2 },
                    { 7, 7, 7, new DateTime(2022, 2, 13, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2021, 9, 19, 3, 35, 30, 749, DateTimeKind.Local).AddTicks(4377), "Entregue", 3 }
                });
        }
    }
}
