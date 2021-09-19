using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace teste.Data.Migrations
{
    public partial class AddImages : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Reservas",
                keyColumn: "Id",
                keyValue: 1,
                column: "DataReserva",
                value: new DateTime(2021, 9, 19, 3, 35, 30, 744, DateTimeKind.Local).AddTicks(8397));

            migrationBuilder.UpdateData(
                table: "Reservas",
                keyColumn: "Id",
                keyValue: 2,
                column: "DataReserva",
                value: new DateTime(2021, 9, 19, 3, 35, 30, 749, DateTimeKind.Local).AddTicks(4317));

            migrationBuilder.UpdateData(
                table: "Reservas",
                keyColumn: "Id",
                keyValue: 3,
                column: "DataReserva",
                value: new DateTime(2021, 9, 19, 3, 35, 30, 749, DateTimeKind.Local).AddTicks(4360));

            migrationBuilder.UpdateData(
                table: "Reservas",
                keyColumn: "Id",
                keyValue: 4,
                column: "DataReserva",
                value: new DateTime(2021, 9, 19, 3, 35, 30, 749, DateTimeKind.Local).AddTicks(4365));

            migrationBuilder.UpdateData(
                table: "Reservas",
                keyColumn: "Id",
                keyValue: 5,
                column: "DataReserva",
                value: new DateTime(2021, 9, 19, 3, 35, 30, 749, DateTimeKind.Local).AddTicks(4369));

            migrationBuilder.UpdateData(
                table: "Reservas",
                keyColumn: "Id",
                keyValue: 6,
                column: "DataReserva",
                value: new DateTime(2021, 9, 19, 3, 35, 30, 749, DateTimeKind.Local).AddTicks(4373));

            migrationBuilder.UpdateData(
                table: "Reservas",
                keyColumn: "Id",
                keyValue: 7,
                column: "DataReserva",
                value: new DateTime(2021, 9, 19, 3, 35, 30, 749, DateTimeKind.Local).AddTicks(4377));

            migrationBuilder.UpdateData(
                table: "Reservas",
                keyColumn: "Id",
                keyValue: 8,
                column: "DataReserva",
                value: new DateTime(2021, 9, 19, 3, 35, 30, 749, DateTimeKind.Local).AddTicks(4380));

            migrationBuilder.UpdateData(
                table: "Reservas",
                keyColumn: "Id",
                keyValue: 11,
                column: "DataReserva",
                value: new DateTime(2021, 9, 19, 3, 35, 30, 749, DateTimeKind.Local).AddTicks(4384));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Reservas",
                keyColumn: "Id",
                keyValue: 1,
                column: "DataReserva",
                value: new DateTime(2021, 9, 19, 3, 25, 52, 491, DateTimeKind.Local).AddTicks(4437));

            migrationBuilder.UpdateData(
                table: "Reservas",
                keyColumn: "Id",
                keyValue: 2,
                column: "DataReserva",
                value: new DateTime(2021, 9, 19, 3, 25, 52, 494, DateTimeKind.Local).AddTicks(8436));

            migrationBuilder.UpdateData(
                table: "Reservas",
                keyColumn: "Id",
                keyValue: 3,
                column: "DataReserva",
                value: new DateTime(2021, 9, 19, 3, 25, 52, 494, DateTimeKind.Local).AddTicks(8473));

            migrationBuilder.UpdateData(
                table: "Reservas",
                keyColumn: "Id",
                keyValue: 4,
                column: "DataReserva",
                value: new DateTime(2021, 9, 19, 3, 25, 52, 494, DateTimeKind.Local).AddTicks(8478));

            migrationBuilder.UpdateData(
                table: "Reservas",
                keyColumn: "Id",
                keyValue: 5,
                column: "DataReserva",
                value: new DateTime(2021, 9, 19, 3, 25, 52, 494, DateTimeKind.Local).AddTicks(8482));

            migrationBuilder.UpdateData(
                table: "Reservas",
                keyColumn: "Id",
                keyValue: 6,
                column: "DataReserva",
                value: new DateTime(2021, 9, 19, 3, 25, 52, 494, DateTimeKind.Local).AddTicks(8486));

            migrationBuilder.UpdateData(
                table: "Reservas",
                keyColumn: "Id",
                keyValue: 7,
                column: "DataReserva",
                value: new DateTime(2021, 9, 19, 3, 25, 52, 494, DateTimeKind.Local).AddTicks(8490));

            migrationBuilder.UpdateData(
                table: "Reservas",
                keyColumn: "Id",
                keyValue: 8,
                column: "DataReserva",
                value: new DateTime(2021, 9, 19, 3, 25, 52, 494, DateTimeKind.Local).AddTicks(8494));

            migrationBuilder.UpdateData(
                table: "Reservas",
                keyColumn: "Id",
                keyValue: 11,
                column: "DataReserva",
                value: new DateTime(2021, 9, 19, 3, 25, 52, 494, DateTimeKind.Local).AddTicks(8498));
        }
    }
}
