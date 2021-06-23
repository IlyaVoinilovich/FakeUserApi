using Microsoft.EntityFrameworkCore.Migrations;

namespace FakeUserApi.Migrations
{
    public partial class HashPassMigrations : Migration
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="migrationBuilder"></param>
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Email",
                table: "FakeUsers",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "HashPass",
                table: "FakeUsers",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Login",
                table: "FakeUsers",
                type: "nvarchar(max)",
                nullable: true);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="migrationBuilder"></param>
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Email",
                table: "FakeUsers");

            migrationBuilder.DropColumn(
                name: "HashPass",
                table: "FakeUsers");

            migrationBuilder.DropColumn(
                name: "Login",
                table: "FakeUsers");
        }
    }
}
