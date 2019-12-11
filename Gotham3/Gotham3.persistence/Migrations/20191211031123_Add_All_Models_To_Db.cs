using Microsoft.EntityFrameworkCore.Migrations;

namespace Gotham3.persistence.Migrations
{
    public partial class Add_All_Models_To_Db : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Alerte",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Status = table.Column<string>(nullable: false),
                    Event_Nature = table.Column<string>(nullable: true),
                    Sector = table.Column<string>(nullable: true),
                    Risk = table.Column<string>(nullable: true),
                    Ressource = table.Column<string>(nullable: true),
                    Advice = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Alerte", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CapsuleInformative",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Status = table.Column<string>(nullable: false),
                    Title = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    Link = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CapsuleInformative", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Nouvelle",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Status = table.Column<string>(nullable: false),
                    Title = table.Column<string>(nullable: true),
                    Text_Desc = table.Column<string>(nullable: true),
                    Link_Media = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Nouvelle", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Signalement",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Status = table.Column<int>(nullable: false),
                    Event_Nature = table.Column<string>(nullable: true),
                    Sector = table.Column<string>(nullable: true),
                    Time = table.Column<string>(nullable: true),
                    Comment = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Signalement", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Sinistre",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Status = table.Column<string>(nullable: false),
                    Title = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    Month = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sinistre", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Alerte");

            migrationBuilder.DropTable(
                name: "CapsuleInformative");

            migrationBuilder.DropTable(
                name: "Nouvelle");

            migrationBuilder.DropTable(
                name: "Signalement");

            migrationBuilder.DropTable(
                name: "Sinistre");
        }
    }
}
