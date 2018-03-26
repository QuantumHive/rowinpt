using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace RowinPt.DataAccess.Migrations
{
    public partial class RecentEntryInSubscriptionsAndAbsenceNotes : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "RecentEntry",
                table: "Subscriptions",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "AbsenceNotes",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Active = table.Column<bool>(nullable: false),
                    CreatedBy = table.Column<string>(nullable: false),
                    CreatedOn = table.Column<DateTime>(nullable: false),
                    CustomerId = table.Column<Guid>(nullable: false),
                    EditedBy = table.Column<string>(nullable: false),
                    EditedOn = table.Column<DateTime>(nullable: false),
                    Notes = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AbsenceNotes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AbsenceNotes_Users_CustomerId",
                        column: x => x.CustomerId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AbsenceNotes_CustomerId",
                table: "AbsenceNotes",
                column: "CustomerId",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AbsenceNotes");

            migrationBuilder.DropColumn(
                name: "RecentEntry",
                table: "Subscriptions");
        }
    }
}
