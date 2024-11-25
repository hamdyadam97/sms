using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SMS_LMS.Data.Migrations
{
    /// <inheritdoc />
    public partial class Creation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PleasePreview");

            migrationBuilder.CreateTable(
                name: "SMSConfigTbl",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    accesskey = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    user = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    pass = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    sid = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    sURL = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SMSConfigTbl", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SMSDataTbl",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MobileNo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Message = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SendDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SMSDataTbl", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SMSConfigTbl");

            migrationBuilder.DropTable(
                name: "SMSDataTbl");

            migrationBuilder.CreateTable(
                name: "PleasePreview",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PleasePreview", x => x.Id);
                });
        }
    }
}
