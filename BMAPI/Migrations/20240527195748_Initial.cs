using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace BMAPI.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Jobs",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    MinSalary = table.Column<double>(type: "float", nullable: false),
                    MinHours = table.Column<int>(type: "int", nullable: false),
                    IsRemoteAvailable = table.Column<bool>(type: "bit", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    JobCreationDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Jobs", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "People",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FName = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    LName = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    Sex = table.Column<string>(type: "nvarchar(16)", maxLength: 16, nullable: true),
                    Age = table.Column<int>(type: "int", nullable: false),
                    Salary = table.Column<double>(type: "float", nullable: false),
                    BirthDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    JobId = table.Column<int>(type: "int", nullable: false),
                    WorkId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_People", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Workplaces",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    City = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    Holder = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    DateCreated = table.Column<DateTime>(type: "datetime2", nullable: false),
                    RatingWithStars = table.Column<double>(type: "float", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Workplaces", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "WorkToJobs",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    JobId = table.Column<int>(type: "int", nullable: false),
                    WorkId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WorkToJobs", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "Jobs",
                columns: new[] { "Id", "Description", "IsRemoteAvailable", "JobCreationDate", "MinHours", "MinSalary", "Title" },
                values: new object[,]
                {
                    { 1, "You write code...", true, new DateTime(2024, 5, 27, 22, 57, 48, 199, DateTimeKind.Local).AddTicks(9286), 4, 1050.5, "Programmer" },
                    { 2, "Cleaning offices and halls", false, new DateTime(2024, 5, 28, 22, 57, 48, 199, DateTimeKind.Local).AddTicks(9319), 8, 360.49000000000001, "Janitor" },
                    { 3, "You do office work for the boss", true, new DateTime(2024, 5, 31, 22, 57, 48, 199, DateTimeKind.Local).AddTicks(9333), 6, 639.69000000000005, "Secretary" },
                    { 4, "You guard all the computers in the office", false, new DateTime(2024, 5, 22, 22, 57, 48, 199, DateTimeKind.Local).AddTicks(9341), 8, 1255.55, "Guard" },
                    { 5, "You are the boss", true, new DateTime(2023, 11, 9, 22, 57, 48, 199, DateTimeKind.Local).AddTicks(9350), 4, 4200.6899999999996, "Boss" },
                    { 6, "You write code... But in the front end", true, new DateTime(2024, 5, 27, 22, 57, 48, 199, DateTimeKind.Local).AddTicks(9361), 4, 1050.5, "UI Designer" },
                    { 7, "You maintain the database", true, new DateTime(2024, 5, 12, 22, 57, 48, 199, DateTimeKind.Local).AddTicks(9369), 8, 1050.5, "Database Maintainer" }
                });

            migrationBuilder.InsertData(
                table: "People",
                columns: new[] { "Id", "Age", "BirthDate", "Email", "FName", "JobId", "LName", "Salary", "Sex", "WorkId" },
                values: new object[,]
                {
                    { 1, 43, new DateTime(1981, 10, 26, 0, 0, 0, 0, DateTimeKind.Unspecified), "toncho@gmail.com", "Toncho", 5, "Nikolov", 1200.0, "Female", 1 },
                    { 2, 42, new DateTime(1982, 5, 4, 0, 0, 0, 0, DateTimeKind.Unspecified), "cvety@gmail.com", "Cvetelina", 5, "Stoilova", 1200.0, "Male", 3 },
                    { 3, 37, new DateTime(1987, 7, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "cvety@gmail.com", "Cvetelina", 7, "Stoilova", 1200.0, "Female", 2 },
                    { 4, 27, new DateTime(1997, 6, 27, 0, 0, 0, 0, DateTimeKind.Unspecified), "elena@gmail.com", "Elena", 3, "Elenova", 1200.0, "Male", 3 },
                    { 5, 23, new DateTime(2001, 8, 28, 0, 0, 0, 0, DateTimeKind.Unspecified), "stefan@gmail.com", "Stefan", 7, "Stefanov", 1200.0, "Female", 3 },
                    { 6, 24, new DateTime(2000, 3, 16, 0, 0, 0, 0, DateTimeKind.Unspecified), "viktoria@gmail.com", "Viktoria", 4, "Viktorova", 1200.0, "Male", 1 },
                    { 7, 25, new DateTime(1999, 11, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), "elena@gmail.com", "Elena", 1, "Elenova", 1200.0, "Female", 2 },
                    { 8, 38, new DateTime(1986, 5, 28, 0, 0, 0, 0, DateTimeKind.Unspecified), "georgi@gmail.com", "Georgi", 4, "Georgiev", 1200.0, "Male", 3 },
                    { 9, 41, new DateTime(1983, 2, 9, 0, 0, 0, 0, DateTimeKind.Unspecified), "toncho@gmail.com", "Toncho", 6, "Nikolov", 1200.0, "Female", 1 },
                    { 10, 41, new DateTime(1983, 11, 22, 0, 0, 0, 0, DateTimeKind.Unspecified), "elena@gmail.com", "Elena", 4, "Elenova", 1200.0, "Male", 3 }
                });

            migrationBuilder.InsertData(
                table: "WorkToJobs",
                columns: new[] { "Id", "JobId", "WorkId" },
                values: new object[,]
                {
                    { 1, 1, 1 },
                    { 2, 1, 2 },
                    { 3, 1, 3 },
                    { 4, 2, 1 },
                    { 5, 2, 2 },
                    { 6, 2, 3 },
                    { 7, 3, 1 },
                    { 8, 3, 2 },
                    { 9, 3, 3 },
                    { 10, 4, 1 },
                    { 11, 4, 2 },
                    { 12, 4, 3 },
                    { 13, 5, 1 },
                    { 14, 5, 2 },
                    { 15, 5, 3 },
                    { 16, 6, 1 },
                    { 17, 6, 2 },
                    { 18, 6, 3 },
                    { 19, 7, 1 },
                    { 20, 7, 2 },
                    { 21, 7, 3 }
                });

            migrationBuilder.InsertData(
                table: "Workplaces",
                columns: new[] { "Id", "City", "DateCreated", "Holder", "Name", "RatingWithStars" },
                values: new object[,]
                {
                    { 1, "Plovdiv", new DateTime(2024, 5, 27, 22, 57, 48, 199, DateTimeKind.Local).AddTicks(9382), "Rhody Dervishev", "Sibiz", 4.5999999999999996 },
                    { 2, "Plovdiv", new DateTime(2024, 5, 27, 22, 57, 48, 199, DateTimeKind.Local).AddTicks(9393), "Ivan Draganov", "Prime Holding", 4.0999999999999996 },
                    { 3, "Plovdiv", new DateTime(2024, 5, 27, 22, 57, 48, 199, DateTimeKind.Local).AddTicks(9402), "Petur Arnaudov", "Broadcom", 3.7999999999999998 }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Jobs");

            migrationBuilder.DropTable(
                name: "People");

            migrationBuilder.DropTable(
                name: "Workplaces");

            migrationBuilder.DropTable(
                name: "WorkToJobs");
        }
    }
}
