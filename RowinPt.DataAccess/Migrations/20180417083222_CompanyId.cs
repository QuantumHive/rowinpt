using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace RowinPt.DataAccess.Migrations
{
    public partial class CompanyId : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "CompanyId",
                table: "Users",
                nullable: false);

            migrationBuilder.AddColumn<Guid>(
                name: "CompanyId",
                table: "Subscriptions",
                nullable: false);

            migrationBuilder.AddColumn<Guid>(
                name: "CompanyId",
                table: "ScheduleItems",
                nullable: false);

            migrationBuilder.AddColumn<Guid>(
                name: "CompanyId",
                table: "Schedule",
                nullable: false);

            migrationBuilder.AddColumn<Guid>(
                name: "CompanyId",
                table: "Measurements",
                nullable: false);

            migrationBuilder.AddColumn<Guid>(
                name: "CompanyId",
                table: "Locations",
                nullable: false);

            migrationBuilder.AddColumn<Guid>(
                name: "CompanyId",
                table: "CourseTypes",
                nullable: false);

            migrationBuilder.AddColumn<Guid>(
                name: "CompanyId",
                table: "Courses",
                nullable: false);

            migrationBuilder.AddColumn<Guid>(
                name: "CompanyId",
                table: "Agenda",
                nullable: false);

            migrationBuilder.AddColumn<Guid>(
                name: "CompanyId",
                table: "AbsenceNotes",
                nullable: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CompanyId",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "CompanyId",
                table: "Subscriptions");

            migrationBuilder.DropColumn(
                name: "CompanyId",
                table: "ScheduleItems");

            migrationBuilder.DropColumn(
                name: "CompanyId",
                table: "Schedule");

            migrationBuilder.DropColumn(
                name: "CompanyId",
                table: "Measurements");

            migrationBuilder.DropColumn(
                name: "CompanyId",
                table: "Locations");

            migrationBuilder.DropColumn(
                name: "CompanyId",
                table: "CourseTypes");

            migrationBuilder.DropColumn(
                name: "CompanyId",
                table: "Courses");

            migrationBuilder.DropColumn(
                name: "CompanyId",
                table: "Agenda");

            migrationBuilder.DropColumn(
                name: "CompanyId",
                table: "AbsenceNotes");
        }
    }
}
