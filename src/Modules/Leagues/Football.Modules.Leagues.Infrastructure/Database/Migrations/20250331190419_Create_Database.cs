using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Football.Modules.Leagues.Infrastructure.Database.Migrations
{
    /// <inheritdoc />
    public partial class Create_Database : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "leagues");

            migrationBuilder.CreateTable(
                name: "Manager",
                schema: "leagues",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "character varying(150)", maxLength: 150, nullable: false),
                    YellowCard = table.Column<int>(type: "integer", nullable: false, defaultValue: 0),
                    RedCard = table.Column<int>(type: "integer", nullable: false, defaultValue: 0)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Manager", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Player",
                schema: "leagues",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "character varying(150)", maxLength: 150, nullable: false),
                    YellowCard = table.Column<int>(type: "integer", nullable: false, defaultValue: 0),
                    RedCard = table.Column<int>(type: "integer", nullable: false, defaultValue: 0),
                    MinutesPlayed = table.Column<int>(type: "integer", nullable: false, defaultValue: 0)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Player", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Referee",
                schema: "leagues",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "character varying(150)", maxLength: 150, nullable: false),
                    MinutesPlayed = table.Column<int>(type: "integer", nullable: false, defaultValue: 0)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Referee", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Match",
                schema: "leagues",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    HouseManagerId = table.Column<int>(type: "integer", nullable: false),
                    AwayManagerId = table.Column<int>(type: "integer", nullable: false),
                    RefereeId = table.Column<int>(type: "integer", nullable: false),
                    StartsAtUtc = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Match", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Match_Manager_AwayManagerId",
                        column: x => x.AwayManagerId,
                        principalSchema: "leagues",
                        principalTable: "Manager",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                    table.ForeignKey(
                        name: "FK_Match_Manager_HouseManagerId",
                        column: x => x.HouseManagerId,
                        principalSchema: "leagues",
                        principalTable: "Manager",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                    table.ForeignKey(
                        name: "FK_Match_Referee_RefereeId",
                        column: x => x.RefereeId,
                        principalSchema: "leagues",
                        principalTable: "Referee",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                });

            migrationBuilder.CreateTable(
                name: "MatchPlayer",
                schema: "leagues",
                columns: table => new
                {
                    MatchId = table.Column<int>(type: "integer", nullable: false),
                    PlayerId = table.Column<int>(type: "integer", nullable: false),
                    PlayerType = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MatchPlayer", x => new { x.MatchId, x.PlayerId });
                    table.ForeignKey(
                        name: "FK_MatchPlayer_Match_MatchId",
                        column: x => x.MatchId,
                        principalSchema: "leagues",
                        principalTable: "Match",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MatchPlayer_Player_PlayerId",
                        column: x => x.PlayerId,
                        principalSchema: "leagues",
                        principalTable: "Player",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Match_AwayManagerId",
                schema: "leagues",
                table: "Match",
                column: "AwayManagerId");

            migrationBuilder.CreateIndex(
                name: "IX_Match_HouseManagerId",
                schema: "leagues",
                table: "Match",
                column: "HouseManagerId");

            migrationBuilder.CreateIndex(
                name: "IX_Match_RefereeId",
                schema: "leagues",
                table: "Match",
                column: "RefereeId");

            migrationBuilder.CreateIndex(
                name: "IX_MatchPlayer_MatchId",
                schema: "leagues",
                table: "MatchPlayer",
                column: "MatchId");

            migrationBuilder.CreateIndex(
                name: "IX_MatchPlayer_PlayerId",
                schema: "leagues",
                table: "MatchPlayer",
                column: "PlayerId");

            migrationBuilder.CreateIndex(
                name: "IX_MatchPlayer_PlayerType",
                schema: "leagues",
                table: "MatchPlayer",
                column: "PlayerType");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MatchPlayer",
                schema: "leagues");

            migrationBuilder.DropTable(
                name: "Match",
                schema: "leagues");

            migrationBuilder.DropTable(
                name: "Player",
                schema: "leagues");

            migrationBuilder.DropTable(
                name: "Manager",
                schema: "leagues");

            migrationBuilder.DropTable(
                name: "Referee",
                schema: "leagues");
        }
    }
}
