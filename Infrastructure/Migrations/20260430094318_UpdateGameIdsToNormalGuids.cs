using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class UpdateGameIdsToNormalGuids : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Games",
                keyColumn: "Id",
                keyValue: new Guid("11111111-1111-1111-1111-111111111111"));

            migrationBuilder.DeleteData(
                table: "Games",
                keyColumn: "Id",
                keyValue: new Guid("22222222-2222-2222-2222-222222222222"));

            migrationBuilder.InsertData(
                table: "Games",
                columns: new[] { "Id", "Description", "KategoriId", "Name" },
                values: new object[,]
                {
                    { new Guid("a1b2c3d4-e5f6-4789-a012-3456789abcde"), "Hop over forhindringerne og scor point!", null, "Appelsin Hop" },
                    { new Guid("b2c3d4e5-f6a7-4890-b123-456789abcdef"), "Find den skjulte appelsin så hurtigt som muligt. Ingen hjælp. Ingen hints. Den er der et sted", null, "Hvor filan er appelsinen?" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Games",
                keyColumn: "Id",
                keyValue: new Guid("a1b2c3d4-e5f6-4789-a012-3456789abcde"));

            migrationBuilder.DeleteData(
                table: "Games",
                keyColumn: "Id",
                keyValue: new Guid("b2c3d4e5-f6a7-4890-b123-456789abcdef"));

            migrationBuilder.InsertData(
                table: "Games",
                columns: new[] { "Id", "Description", "KategoriId", "Name" },
                values: new object[,]
                {
                    { new Guid("11111111-1111-1111-1111-111111111111"), "Hop over forhindringerne og scor point!", null, "Appelsin Hop" },
                    { new Guid("22222222-2222-2222-2222-222222222222"), "Find den skjulte appelsin så hurtigt som muligt. Ingen hjælp. Ingen hints. Den er der et sted", null, "Hvor filan er appelsinen?" }
                });
        }
    }
}
