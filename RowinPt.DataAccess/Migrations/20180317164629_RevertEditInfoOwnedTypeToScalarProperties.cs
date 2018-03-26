using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace RowinPt.DataAccess.Migrations
{
    public partial class RevertEditInfoOwnedTypeToScalarProperties : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "EditInfo_EditedOn",
                table: "Users",
                newName: "EditedOn");

            migrationBuilder.RenameColumn(
                name: "EditInfo_EditedBy",
                table: "Users",
                newName: "EditedBy");

            migrationBuilder.RenameColumn(
                name: "EditInfo_CreatedOn",
                table: "Users",
                newName: "CreatedOn");

            migrationBuilder.RenameColumn(
                name: "EditInfo_CreatedBy",
                table: "Users",
                newName: "CreatedBy");

            migrationBuilder.RenameColumn(
                name: "EditInfo_EditedOn",
                table: "Subscriptions",
                newName: "EditedOn");

            migrationBuilder.RenameColumn(
                name: "EditInfo_EditedBy",
                table: "Subscriptions",
                newName: "EditedBy");

            migrationBuilder.RenameColumn(
                name: "EditInfo_CreatedOn",
                table: "Subscriptions",
                newName: "CreatedOn");

            migrationBuilder.RenameColumn(
                name: "EditInfo_CreatedBy",
                table: "Subscriptions",
                newName: "CreatedBy");

            migrationBuilder.RenameColumn(
                name: "EditInfo_EditedOn",
                table: "ScheduleItems",
                newName: "EditedOn");

            migrationBuilder.RenameColumn(
                name: "EditInfo_EditedBy",
                table: "ScheduleItems",
                newName: "EditedBy");

            migrationBuilder.RenameColumn(
                name: "EditInfo_CreatedOn",
                table: "ScheduleItems",
                newName: "CreatedOn");

            migrationBuilder.RenameColumn(
                name: "EditInfo_CreatedBy",
                table: "ScheduleItems",
                newName: "CreatedBy");

            migrationBuilder.RenameColumn(
                name: "EditInfo_EditedOn",
                table: "Schedule",
                newName: "EditedOn");

            migrationBuilder.RenameColumn(
                name: "EditInfo_EditedBy",
                table: "Schedule",
                newName: "EditedBy");

            migrationBuilder.RenameColumn(
                name: "EditInfo_CreatedOn",
                table: "Schedule",
                newName: "CreatedOn");

            migrationBuilder.RenameColumn(
                name: "EditInfo_CreatedBy",
                table: "Schedule",
                newName: "CreatedBy");

            migrationBuilder.RenameColumn(
                name: "EditInfo_EditedOn",
                table: "Measurements",
                newName: "EditedOn");

            migrationBuilder.RenameColumn(
                name: "EditInfo_EditedBy",
                table: "Measurements",
                newName: "EditedBy");

            migrationBuilder.RenameColumn(
                name: "EditInfo_CreatedOn",
                table: "Measurements",
                newName: "CreatedOn");

            migrationBuilder.RenameColumn(
                name: "EditInfo_CreatedBy",
                table: "Measurements",
                newName: "CreatedBy");

            migrationBuilder.RenameColumn(
                name: "EditInfo_EditedOn",
                table: "Locations",
                newName: "EditedOn");

            migrationBuilder.RenameColumn(
                name: "EditInfo_EditedBy",
                table: "Locations",
                newName: "EditedBy");

            migrationBuilder.RenameColumn(
                name: "EditInfo_CreatedOn",
                table: "Locations",
                newName: "CreatedOn");

            migrationBuilder.RenameColumn(
                name: "EditInfo_CreatedBy",
                table: "Locations",
                newName: "CreatedBy");

            migrationBuilder.RenameColumn(
                name: "EditInfo_EditedOn",
                table: "CourseTypes",
                newName: "EditedOn");

            migrationBuilder.RenameColumn(
                name: "EditInfo_EditedBy",
                table: "CourseTypes",
                newName: "EditedBy");

            migrationBuilder.RenameColumn(
                name: "EditInfo_CreatedOn",
                table: "CourseTypes",
                newName: "CreatedOn");

            migrationBuilder.RenameColumn(
                name: "EditInfo_CreatedBy",
                table: "CourseTypes",
                newName: "CreatedBy");

            migrationBuilder.RenameColumn(
                name: "EditInfo_EditedOn",
                table: "Courses",
                newName: "EditedOn");

            migrationBuilder.RenameColumn(
                name: "EditInfo_EditedBy",
                table: "Courses",
                newName: "EditedBy");

            migrationBuilder.RenameColumn(
                name: "EditInfo_CreatedOn",
                table: "Courses",
                newName: "CreatedOn");

            migrationBuilder.RenameColumn(
                name: "EditInfo_CreatedBy",
                table: "Courses",
                newName: "CreatedBy");

            migrationBuilder.RenameColumn(
                name: "EditInfo_EditedOn",
                table: "Agenda",
                newName: "EditedOn");

            migrationBuilder.RenameColumn(
                name: "EditInfo_EditedBy",
                table: "Agenda",
                newName: "EditedBy");

            migrationBuilder.RenameColumn(
                name: "EditInfo_CreatedOn",
                table: "Agenda",
                newName: "CreatedOn");

            migrationBuilder.RenameColumn(
                name: "EditInfo_CreatedBy",
                table: "Agenda",
                newName: "CreatedBy");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "EditedOn",
                table: "Users",
                newName: "EditInfo_EditedOn");

            migrationBuilder.RenameColumn(
                name: "EditedBy",
                table: "Users",
                newName: "EditInfo_EditedBy");

            migrationBuilder.RenameColumn(
                name: "CreatedOn",
                table: "Users",
                newName: "EditInfo_CreatedOn");

            migrationBuilder.RenameColumn(
                name: "CreatedBy",
                table: "Users",
                newName: "EditInfo_CreatedBy");

            migrationBuilder.RenameColumn(
                name: "EditedOn",
                table: "Subscriptions",
                newName: "EditInfo_EditedOn");

            migrationBuilder.RenameColumn(
                name: "EditedBy",
                table: "Subscriptions",
                newName: "EditInfo_EditedBy");

            migrationBuilder.RenameColumn(
                name: "CreatedOn",
                table: "Subscriptions",
                newName: "EditInfo_CreatedOn");

            migrationBuilder.RenameColumn(
                name: "CreatedBy",
                table: "Subscriptions",
                newName: "EditInfo_CreatedBy");

            migrationBuilder.RenameColumn(
                name: "EditedOn",
                table: "ScheduleItems",
                newName: "EditInfo_EditedOn");

            migrationBuilder.RenameColumn(
                name: "EditedBy",
                table: "ScheduleItems",
                newName: "EditInfo_EditedBy");

            migrationBuilder.RenameColumn(
                name: "CreatedOn",
                table: "ScheduleItems",
                newName: "EditInfo_CreatedOn");

            migrationBuilder.RenameColumn(
                name: "CreatedBy",
                table: "ScheduleItems",
                newName: "EditInfo_CreatedBy");

            migrationBuilder.RenameColumn(
                name: "EditedOn",
                table: "Schedule",
                newName: "EditInfo_EditedOn");

            migrationBuilder.RenameColumn(
                name: "EditedBy",
                table: "Schedule",
                newName: "EditInfo_EditedBy");

            migrationBuilder.RenameColumn(
                name: "CreatedOn",
                table: "Schedule",
                newName: "EditInfo_CreatedOn");

            migrationBuilder.RenameColumn(
                name: "CreatedBy",
                table: "Schedule",
                newName: "EditInfo_CreatedBy");

            migrationBuilder.RenameColumn(
                name: "EditedOn",
                table: "Measurements",
                newName: "EditInfo_EditedOn");

            migrationBuilder.RenameColumn(
                name: "EditedBy",
                table: "Measurements",
                newName: "EditInfo_EditedBy");

            migrationBuilder.RenameColumn(
                name: "CreatedOn",
                table: "Measurements",
                newName: "EditInfo_CreatedOn");

            migrationBuilder.RenameColumn(
                name: "CreatedBy",
                table: "Measurements",
                newName: "EditInfo_CreatedBy");

            migrationBuilder.RenameColumn(
                name: "EditedOn",
                table: "Locations",
                newName: "EditInfo_EditedOn");

            migrationBuilder.RenameColumn(
                name: "EditedBy",
                table: "Locations",
                newName: "EditInfo_EditedBy");

            migrationBuilder.RenameColumn(
                name: "CreatedOn",
                table: "Locations",
                newName: "EditInfo_CreatedOn");

            migrationBuilder.RenameColumn(
                name: "CreatedBy",
                table: "Locations",
                newName: "EditInfo_CreatedBy");

            migrationBuilder.RenameColumn(
                name: "EditedOn",
                table: "CourseTypes",
                newName: "EditInfo_EditedOn");

            migrationBuilder.RenameColumn(
                name: "EditedBy",
                table: "CourseTypes",
                newName: "EditInfo_EditedBy");

            migrationBuilder.RenameColumn(
                name: "CreatedOn",
                table: "CourseTypes",
                newName: "EditInfo_CreatedOn");

            migrationBuilder.RenameColumn(
                name: "CreatedBy",
                table: "CourseTypes",
                newName: "EditInfo_CreatedBy");

            migrationBuilder.RenameColumn(
                name: "EditedOn",
                table: "Courses",
                newName: "EditInfo_EditedOn");

            migrationBuilder.RenameColumn(
                name: "EditedBy",
                table: "Courses",
                newName: "EditInfo_EditedBy");

            migrationBuilder.RenameColumn(
                name: "CreatedOn",
                table: "Courses",
                newName: "EditInfo_CreatedOn");

            migrationBuilder.RenameColumn(
                name: "CreatedBy",
                table: "Courses",
                newName: "EditInfo_CreatedBy");

            migrationBuilder.RenameColumn(
                name: "EditedOn",
                table: "Agenda",
                newName: "EditInfo_EditedOn");

            migrationBuilder.RenameColumn(
                name: "EditedBy",
                table: "Agenda",
                newName: "EditInfo_EditedBy");

            migrationBuilder.RenameColumn(
                name: "CreatedOn",
                table: "Agenda",
                newName: "EditInfo_CreatedOn");

            migrationBuilder.RenameColumn(
                name: "CreatedBy",
                table: "Agenda",
                newName: "EditInfo_CreatedBy");
        }
    }
}
