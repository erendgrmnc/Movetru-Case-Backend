using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MovetruCase.DataAccessLayer.Migrations
{
    /// <inheritdoc />
    public partial class v01 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsUserNewlyRegistered",
                table: "UserDatas",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "UserDatas",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsUserNewlyRegistered",
                table: "UserDatas");

            migrationBuilder.DropColumn(
                name: "Name",
                table: "UserDatas");
        }
    }
}
