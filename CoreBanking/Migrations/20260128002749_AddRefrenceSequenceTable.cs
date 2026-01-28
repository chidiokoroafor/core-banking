using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CoreBanking.Migrations
{
    /// <inheritdoc />
    public partial class AddRefrenceSequenceTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropCheckConstraint(
                name: "CK_Transaction_Amount_Positive",
                table: "Transactions");

            migrationBuilder.AddColumn<string>(
                name: "Reference",
                table: "Transfers",
                type: "nvarchar(200)",
                maxLength: 200,
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateTable(
                name: "ReferenceSequence",
                columns: table => new
                {
                    Date = table.Column<DateTime>(type: "date", nullable: false),
                    LastNumber = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ReferenceSequence", x => x.Date);
                });

            migrationBuilder.AddCheckConstraint(
                name: "CK_Transaction_Amount_Positive",
                table: "Transactions",
                sql: "[Amount] <> 0");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ReferenceSequence");

            migrationBuilder.DropCheckConstraint(
                name: "CK_Transaction_Amount_Positive",
                table: "Transactions");

            migrationBuilder.DropColumn(
                name: "Reference",
                table: "Transfers");

            migrationBuilder.AddCheckConstraint(
                name: "CK_Transaction_Amount_Positive",
                table: "Transactions",
                sql: "[Amount] > 0");
        }
    }
}
