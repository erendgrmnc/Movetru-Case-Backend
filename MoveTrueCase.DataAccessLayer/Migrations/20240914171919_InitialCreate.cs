using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MovetruCase.DataAccessLayer.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "UserDatas",
                columns: table => new
                {
                    UserID = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    WeightAsKilogram = table.Column<int>(type: "int", nullable: false),
                    HeightAsCentimeter = table.Column<int>(type: "int", nullable: false),
                    Age = table.Column<int>(type: "int", nullable: false),
                    CreationDate = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    UpdateDate = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserDatas", x => x.UserID);
                });

            migrationBuilder.CreateTable(
                name: "DailyStepLogs",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    CreationDate = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    TotalSteps = table.Column<int>(type: "int", nullable: false),
                    UpdateDate = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DailyStepLogs", x => new { x.UserId, x.CreationDate });
                    table.ForeignKey(
                        name: "FK_DailyStepLogs_UserDatas_UserId",
                        column: x => x.UserId,
                        principalTable: "UserDatas",
                        principalColumn: "UserID",
                        onDelete: ReferentialAction.Cascade);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DailyStepLogs");

            migrationBuilder.DropTable(
                name: "UserDatas");
        }
    }
}
