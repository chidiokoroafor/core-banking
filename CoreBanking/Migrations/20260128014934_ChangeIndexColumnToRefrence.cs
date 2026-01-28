using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CoreBanking.Migrations
{
    /// <inheritdoc />
    public partial class ChangeIndexColumnToRefrence : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Transfers_IdempotencyKey",
                table: "Transfers");

            migrationBuilder.DropColumn(
                name: "IdempotencyKey",
                table: "Transfers");

            migrationBuilder.CreateIndex(
                name: "IX_Transfers_Reference",
                table: "Transfers",
                column: "Reference",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Transfers_Reference",
                table: "Transfers");

            migrationBuilder.AddColumn<string>(
                name: "IdempotencyKey",
                table: "Transfers",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_Transfers_IdempotencyKey",
                table: "Transfers",
                column: "IdempotencyKey",
                unique: true);
        }
    }
}
