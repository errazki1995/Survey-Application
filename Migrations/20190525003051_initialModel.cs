using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace EsisaSurvey.Migrations
{
    public partial class initialModel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Client",
                columns: table => new
                {
                    clientid = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    name = table.Column<string>(nullable: true),
                    email = table.Column<string>(nullable: true),
                    adress = table.Column<string>(nullable: true),
                    login = table.Column<string>(nullable: true),
                    password = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Client", x => x.clientid);
                });

            migrationBuilder.CreateTable(
                name: "Commercant",
                columns: table => new
                {
                    commercantId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    commercantName = table.Column<string>(nullable: true),
                    Entreprise = table.Column<string>(nullable: true),
                    login = table.Column<string>(nullable: true),
                    password = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Commercant", x => x.commercantId);
                });

            migrationBuilder.CreateTable(
                name: "SurveyResulLog",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    postId = table.Column<string>(nullable: true),
                    surveyanswerJson = table.Column<string>(nullable: true),
                    clientId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SurveyResulLog", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "SurveyResult",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    postId = table.Column<string>(nullable: true),
                    surveyResult = table.Column<string>(nullable: true),
                    clientid = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SurveyResult", x => x.id);
                    table.ForeignKey(
                        name: "FK_SurveyResult_Client_clientid",
                        column: x => x.clientid,
                        principalTable: "Client",
                        principalColumn: "clientid",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Survey",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    Json = table.Column<string>(nullable: true),
                    Text = table.Column<string>(nullable: true),
                    commercantId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Survey", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Survey_Commercant_commercantId",
                        column: x => x.commercantId,
                        principalTable: "Commercant",
                        principalColumn: "commercantId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SurveyStore",
                columns: table => new
                {
                    surveyIdBE = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Id = table.Column<string>(nullable: true),
                    Json = table.Column<string>(nullable: true),
                    Text = table.Column<string>(nullable: true),
                    commercantId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SurveyStore", x => x.surveyIdBE);
                    table.ForeignKey(
                        name: "FK_SurveyStore_Commercant_commercantId",
                        column: x => x.commercantId,
                        principalTable: "Commercant",
                        principalColumn: "commercantId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Survey_commercantId",
                table: "Survey",
                column: "commercantId");

            migrationBuilder.CreateIndex(
                name: "IX_SurveyResult_clientid",
                table: "SurveyResult",
                column: "clientid");

            migrationBuilder.CreateIndex(
                name: "IX_SurveyStore_commercantId",
                table: "SurveyStore",
                column: "commercantId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Survey");

            migrationBuilder.DropTable(
                name: "SurveyResulLog");

            migrationBuilder.DropTable(
                name: "SurveyResult");

            migrationBuilder.DropTable(
                name: "SurveyStore");

            migrationBuilder.DropTable(
                name: "Client");

            migrationBuilder.DropTable(
                name: "Commercant");
        }
    }
}
