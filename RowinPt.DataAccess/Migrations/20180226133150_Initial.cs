using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace RowinPt.DataAccess.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CourseTypes",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Active = table.Column<bool>(nullable: false),
                    Capacity = table.Column<int>(nullable: false),
                    Name = table.Column<string>(nullable: false),
                    EditInfo_CreatedBy = table.Column<string>(nullable: false),
                    EditInfo_CreatedOn = table.Column<DateTime>(nullable: false),
                    EditInfo_EditedBy = table.Column<string>(nullable: false),
                    EditInfo_EditedOn = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CourseTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Locations",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Active = table.Column<bool>(nullable: false),
                    Address = table.Column<string>(nullable: false),
                    Name = table.Column<string>(nullable: false),
                    EditInfo_CreatedBy = table.Column<string>(nullable: false),
                    EditInfo_CreatedOn = table.Column<DateTime>(nullable: false),
                    EditInfo_EditedBy = table.Column<string>(nullable: false),
                    EditInfo_EditedOn = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Locations", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    BirthDate = table.Column<DateTime>(nullable: true),
                    Length = table.Column<int>(nullable: true),
                    Number = table.Column<string>(nullable: true),
                    Admin = table.Column<bool>(nullable: true),
                    Id = table.Column<Guid>(nullable: false),
                    Active = table.Column<bool>(nullable: false),
                    Discriminator = table.Column<string>(nullable: false),
                    Email = table.Column<string>(nullable: false),
                    EmailConfirmed = table.Column<bool>(nullable: false),
                    Name = table.Column<string>(nullable: false),
                    NormalizedEmail = table.Column<string>(nullable: false),
                    PasswordHash = table.Column<string>(nullable: true),
                    PhoneNumber = table.Column<string>(nullable: false),
                    SecurityStamp = table.Column<Guid>(nullable: false),
                    Sex = table.Column<byte>(nullable: false),
                    EditInfo_CreatedBy = table.Column<string>(nullable: false),
                    EditInfo_CreatedOn = table.Column<DateTime>(nullable: false),
                    EditInfo_EditedBy = table.Column<string>(nullable: false),
                    EditInfo_EditedOn = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Courses",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Active = table.Column<bool>(nullable: false),
                    Capacity = table.Column<int>(nullable: false),
                    CourseTypeId = table.Column<Guid>(nullable: false),
                    Name = table.Column<string>(nullable: false),
                    EditInfo_CreatedBy = table.Column<string>(nullable: false),
                    EditInfo_CreatedOn = table.Column<DateTime>(nullable: false),
                    EditInfo_EditedBy = table.Column<string>(nullable: false),
                    EditInfo_EditedOn = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Courses", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Courses_CourseTypes_CourseTypeId",
                        column: x => x.CourseTypeId,
                        principalTable: "CourseTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Schedule",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Active = table.Column<bool>(nullable: false),
                    LocationId = table.Column<Guid>(nullable: false),
                    Name = table.Column<string>(nullable: false),
                    EditInfo_CreatedBy = table.Column<string>(nullable: false),
                    EditInfo_CreatedOn = table.Column<DateTime>(nullable: false),
                    EditInfo_EditedBy = table.Column<string>(nullable: false),
                    EditInfo_EditedOn = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Schedule", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Schedule_Locations_LocationId",
                        column: x => x.LocationId,
                        principalTable: "Locations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Measurements",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Active = table.Column<bool>(nullable: false),
                    ArmSize = table.Column<float>(nullable: true),
                    BellySize = table.Column<float>(nullable: true),
                    CustomerId = table.Column<Guid>(nullable: false),
                    Date = table.Column<DateTime>(nullable: false),
                    FatPercentage = table.Column<float>(nullable: true),
                    ShoulderSize = table.Column<float>(nullable: true),
                    UpperLegSize = table.Column<float>(nullable: true),
                    WaistSize = table.Column<float>(nullable: true),
                    Weight = table.Column<float>(nullable: true),
                    EditInfo_CreatedBy = table.Column<string>(nullable: false),
                    EditInfo_CreatedOn = table.Column<DateTime>(nullable: false),
                    EditInfo_EditedBy = table.Column<string>(nullable: false),
                    EditInfo_EditedOn = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Measurements", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Measurements_Users_CustomerId",
                        column: x => x.CustomerId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Subscriptions",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Active = table.Column<bool>(nullable: false),
                    CourseTypeId = table.Column<Guid>(nullable: false),
                    CustomerId = table.Column<Guid>(nullable: false),
                    Notes = table.Column<string>(nullable: true),
                    StartDate = table.Column<DateTime>(nullable: false),
                    WeeklyCredits = table.Column<int>(nullable: false),
                    EditInfo_CreatedBy = table.Column<string>(nullable: false),
                    EditInfo_CreatedOn = table.Column<DateTime>(nullable: false),
                    EditInfo_EditedBy = table.Column<string>(nullable: false),
                    EditInfo_EditedOn = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Subscriptions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Subscriptions_CourseTypes_CourseTypeId",
                        column: x => x.CourseTypeId,
                        principalTable: "CourseTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Subscriptions_Users_CustomerId",
                        column: x => x.CustomerId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ScheduleItems",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Active = table.Column<bool>(nullable: false),
                    CourseId = table.Column<Guid>(nullable: false),
                    Date = table.Column<DateTime>(nullable: false),
                    EndTime = table.Column<TimeSpan>(nullable: false),
                    PersonalTrainerId = table.Column<Guid>(nullable: true),
                    ScheduleId = table.Column<Guid>(nullable: false),
                    StartTime = table.Column<TimeSpan>(nullable: false),
                    EditInfo_CreatedBy = table.Column<string>(nullable: false),
                    EditInfo_CreatedOn = table.Column<DateTime>(nullable: false),
                    EditInfo_EditedBy = table.Column<string>(nullable: false),
                    EditInfo_EditedOn = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ScheduleItems", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ScheduleItems_Courses_CourseId",
                        column: x => x.CourseId,
                        principalTable: "Courses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ScheduleItems_Users_PersonalTrainerId",
                        column: x => x.PersonalTrainerId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ScheduleItems_Schedule_ScheduleId",
                        column: x => x.ScheduleId,
                        principalTable: "Schedule",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Agenda",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Active = table.Column<bool>(nullable: false),
                    CustomerId = table.Column<Guid>(nullable: false),
                    ScheduleItemId = table.Column<Guid>(nullable: false),
                    EditInfo_CreatedBy = table.Column<string>(nullable: false),
                    EditInfo_CreatedOn = table.Column<DateTime>(nullable: false),
                    EditInfo_EditedBy = table.Column<string>(nullable: false),
                    EditInfo_EditedOn = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Agenda", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Agenda_Users_CustomerId",
                        column: x => x.CustomerId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Agenda_ScheduleItems_ScheduleItemId",
                        column: x => x.ScheduleItemId,
                        principalTable: "ScheduleItems",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Agenda_CustomerId",
                table: "Agenda",
                column: "CustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_Agenda_ScheduleItemId",
                table: "Agenda",
                column: "ScheduleItemId");

            migrationBuilder.CreateIndex(
                name: "IX_Courses_CourseTypeId",
                table: "Courses",
                column: "CourseTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Measurements_CustomerId",
                table: "Measurements",
                column: "CustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_Schedule_LocationId",
                table: "Schedule",
                column: "LocationId");

            migrationBuilder.CreateIndex(
                name: "IX_ScheduleItems_CourseId",
                table: "ScheduleItems",
                column: "CourseId");

            migrationBuilder.CreateIndex(
                name: "IX_ScheduleItems_PersonalTrainerId",
                table: "ScheduleItems",
                column: "PersonalTrainerId");

            migrationBuilder.CreateIndex(
                name: "IX_ScheduleItems_ScheduleId",
                table: "ScheduleItems",
                column: "ScheduleId");

            migrationBuilder.CreateIndex(
                name: "IX_Subscriptions_CourseTypeId",
                table: "Subscriptions",
                column: "CourseTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Subscriptions_CustomerId",
                table: "Subscriptions",
                column: "CustomerId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Agenda");

            migrationBuilder.DropTable(
                name: "Measurements");

            migrationBuilder.DropTable(
                name: "Subscriptions");

            migrationBuilder.DropTable(
                name: "ScheduleItems");

            migrationBuilder.DropTable(
                name: "Courses");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "Schedule");

            migrationBuilder.DropTable(
                name: "CourseTypes");

            migrationBuilder.DropTable(
                name: "Locations");
        }
    }
}
