using Microsoft.EntityFrameworkCore.Migrations;

namespace Newsletter.Migrations
{
    public partial class iniial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Clientes",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    nomeCompleto = table.Column<string>(type: "nvarchar(250)", nullable: false),
                    email = table.Column<string>(type: "nvarchar(250)", nullable: false),
                    ativo = table.Column<string>(type: "nvarchar(250)", nullable: false),
                    dataCadastro = table.Column<string>(type: "nvarchar(250)", nullable: false),
                    dataRevogação = table.Column<string>(type: "nvarchar(250)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Clientes", x => x.id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Clientes");
        }
    }
}
