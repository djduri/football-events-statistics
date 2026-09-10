using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FootballEvents.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Init : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "Teams");

            migrationBuilder.CreateTable(
                name: "Teams",
                schema: "Teams",
                columns: table => new
                {
                    Id = table.Column<long>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", maxLength: 255, nullable: false),
                    NormalizedName = table.Column<string>(type: "TEXT", maxLength: 255, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Teams", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "MatchRecords",
                schema: "Teams",
                columns: table => new
                {
                    Id = table.Column<long>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    HomeTeamId = table.Column<long>(type: "INTEGER", nullable: false),
                    AwayTeamId = table.Column<long>(type: "INTEGER", nullable: false),
                    HomeScore = table.Column<int>(type: "INTEGER", nullable: false),
                    AwayScore = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MatchRecords", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MatchRecords_Teams_AwayTeamId",
                        column: x => x.AwayTeamId,
                        principalSchema: "Teams",
                        principalTable: "Teams",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_MatchRecords_Teams_HomeTeamId",
                        column: x => x.HomeTeamId,
                        principalSchema: "Teams",
                        principalTable: "Teams",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "TeamStatistics",
                schema: "Teams",
                columns: table => new
                {
                    TeamId = table.Column<long>(type: "INTEGER", nullable: false),
                    MatchesPlayed = table.Column<int>(type: "INTEGER", nullable: false),
                    Points = table.Column<int>(type: "INTEGER", nullable: false),
                    GoalScored = table.Column<int>(type: "INTEGER", nullable: false),
                    GoalConceded = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TeamStatistics", x => x.TeamId);
                    table.ForeignKey(
                        name: "FK_TeamStatistics_Teams_TeamId",
                        column: x => x.TeamId,
                        principalSchema: "Teams",
                        principalTable: "Teams",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_MatchRecords_AwayTeamId",
                schema: "Teams",
                table: "MatchRecords",
                column: "AwayTeamId");

            migrationBuilder.CreateIndex(
                name: "IX_MatchRecords_HomeTeamId",
                schema: "Teams",
                table: "MatchRecords",
                column: "HomeTeamId");

            migrationBuilder.CreateIndex(
                name: "IX_Teams_Name",
                schema: "Teams",
                table: "Teams",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Teams_NormalizedName",
                schema: "Teams",
                table: "Teams",
                column: "NormalizedName",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MatchRecords",
                schema: "Teams");

            migrationBuilder.DropTable(
                name: "TeamStatistics",
                schema: "Teams");

            migrationBuilder.DropTable(
                name: "Teams",
                schema: "Teams");
        }
    }
}
