using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Forum.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Roles",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleName = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Roles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FirstName = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: false),
                    Username = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsUserBlocked = table.Column<bool>(type: "bit", nullable: false),
                    RoleId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Users_Roles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "Roles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Posts",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(64)", maxLength: 64, nullable: false),
                    Content = table.Column<string>(type: "nvarchar(max)", maxLength: 8192, nullable: false),
                    Likes = table.Column<int>(type: "int", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UserId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Posts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Posts_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Comments",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Content = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PostId = table.Column<int>(type: "int", nullable: false),
                    UserId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Comments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Comments_Posts_PostId",
                        column: x => x.PostId,
                        principalTable: "Posts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Comments_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id");
                });

            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "Id", "RoleName" },
                values: new object[] { 1, "admin" });

            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "Id", "RoleName" },
                values: new object[] { 2, "user" });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Email", "FirstName", "IsUserBlocked", "LastName", "Password", "PhoneNumber", "RoleId", "Username" },
                values: new object[] { 1, "vpatova@abv.bg", "Valentina", false, "Patova", "123", "555-555-5555", 1, "valia" });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Email", "FirstName", "IsUserBlocked", "LastName", "Password", "PhoneNumber", "RoleId", "Username" },
                values: new object[] { 3, "vi@abv.bg", "Valentina", false, "Iordanova", "123", "777-777-7777", 2, "valia2" });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Email", "FirstName", "IsUserBlocked", "LastName", "Password", "PhoneNumber", "RoleId", "Username" },
                values: new object[] { 2, "mivanov@abv.bg", "Miroslav", false, "Ivanov", "123", "666-666-6666", 2, "miro" });

            migrationBuilder.InsertData(
                table: "Posts",
                columns: new[] { "Id", "Content", "CreatedOn", "Likes", "Title", "UserId" },
                values: new object[,]
                {
                    { 2, "Great Time at the beach !", new DateTime(2022, 5, 21, 19, 23, 57, 784, DateTimeKind.Local).AddTicks(2743), 1, "We went to the Golden Sands", 1 },
                    { 6, "Conted Of Sixth Post", new DateTime(2022, 5, 21, 19, 23, 57, 784, DateTimeKind.Local).AddTicks(2779), 1, "Sixth Post", 1 },
                    { 7, "Conted Of Seventh Post", new DateTime(2022, 5, 21, 19, 23, 57, 784, DateTimeKind.Local).AddTicks(2782), 1, "Seventh Post", 1 },
                    { 11, "Conted Of Eleventh Post", new DateTime(2022, 5, 21, 19, 23, 57, 784, DateTimeKind.Local).AddTicks(2793), 1, "Eleventh Post", 1 },
                    { 3, "Went skiing to Lake Tahoe last week ,we had a great time", new DateTime(2022, 5, 21, 19, 23, 57, 784, DateTimeKind.Local).AddTicks(2767), 1, "Lake Tahoe visit", 3 },
                    { 5, "Conted Of Fifth Post", new DateTime(2022, 5, 21, 19, 23, 57, 784, DateTimeKind.Local).AddTicks(2775), 1, "Fifth Post", 3 },
                    { 9, "Conted Of Ninth Post", new DateTime(2022, 5, 21, 19, 23, 57, 784, DateTimeKind.Local).AddTicks(2787), 1, "Ninth Post", 3 },
                    { 1, "Our Vacation was amazing .. ", new DateTime(2022, 5, 21, 19, 23, 57, 782, DateTimeKind.Local).AddTicks(5922), 3, "Vacation in Sunny beach", 2 },
                    { 4, "Super Nice place to stay", new DateTime(2022, 5, 21, 19, 23, 57, 784, DateTimeKind.Local).AddTicks(2772), 1, "Grand Pyramids Hotel", 2 },
                    { 8, "Conted Of Eight Post", new DateTime(2022, 5, 21, 19, 23, 57, 784, DateTimeKind.Local).AddTicks(2785), 1, "Eight Post", 2 },
                    { 10, "Conted Of Tenth Post", new DateTime(2022, 5, 21, 19, 23, 57, 784, DateTimeKind.Local).AddTicks(2791), 1, "Tenth Post", 2 }
                });

            migrationBuilder.InsertData(
                table: "Comments",
                columns: new[] { "Id", "Content", "PostId", "UserId" },
                values: new object[,]
                {
                    { 4, "4 Comment", 2, 2 },
                    { 5, "5 Comment", 2, 1 },
                    { 6, "6 Comment", 2, 3 },
                    { 7, "7 Comment", 3, 2 },
                    { 8, "8 Comment", 3, 3 },
                    { 10, "10 Comment", 5, 3 },
                    { 11, "11 Comment", 5, 3 },
                    { 12, "12 Comment", 5, 2 },
                    { 13, "13 Comment", 5, 2 },
                    { 14, "14 Comment", 5, 1 },
                    { 1, "1 Comment", 1, 2 },
                    { 2, "2 Comment", 1, 1 },
                    { 3, "3 Comment", 1, 3 },
                    { 9, "9 Comment", 4, 3 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Comments_PostId",
                table: "Comments",
                column: "PostId");

            migrationBuilder.CreateIndex(
                name: "IX_Comments_UserId",
                table: "Comments",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Posts_UserId",
                table: "Posts",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Users_RoleId",
                table: "Users",
                column: "RoleId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Comments");

            migrationBuilder.DropTable(
                name: "Posts");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "Roles");
        }
    }
}
