using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Inital : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "video_games");

            migrationBuilder.CreateTable(
                name: "genre",
                schema: "video_games",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    genre_name = table.Column<string>(type: "varchar(50)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_genre", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "platform",
                schema: "video_games",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    platform_name = table.Column<string>(type: "varchar(50)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_platform", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "publisher",
                schema: "video_games",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    publisher_name = table.Column<string>(type: "varchar(100)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_publisher", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "region",
                schema: "video_games",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    region_name = table.Column<string>(type: "varchar(50)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_region", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "game",
                schema: "video_games",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    genre_id = table.Column<int>(type: "integer", nullable: false),
                    game_name = table.Column<string>(type: "varchar(200)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_game", x => x.id);
                    table.ForeignKey(
                        name: "FK_game_genre_genre_id",
                        column: x => x.genre_id,
                        principalSchema: "video_games",
                        principalTable: "genre",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "game_publisher",
                schema: "video_games",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    game_id = table.Column<int>(type: "integer", nullable: false),
                    publisher_id = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_game_publisher", x => x.id);
                    table.ForeignKey(
                        name: "FK_game_publisher_game_game_id",
                        column: x => x.game_id,
                        principalSchema: "video_games",
                        principalTable: "game",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_game_publisher_publisher_publisher_id",
                        column: x => x.publisher_id,
                        principalSchema: "video_games",
                        principalTable: "publisher",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "game_platform",
                schema: "video_games",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    game_publisher_id = table.Column<int>(type: "integer", nullable: false),
                    platform_id = table.Column<int>(type: "integer", nullable: false),
                    release_year = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_game_platform", x => x.id);
                    table.ForeignKey(
                        name: "FK_game_platform_game_publisher_game_publisher_id",
                        column: x => x.game_publisher_id,
                        principalSchema: "video_games",
                        principalTable: "game_publisher",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_game_platform_platform_platform_id",
                        column: x => x.platform_id,
                        principalSchema: "video_games",
                        principalTable: "platform",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "region_sales",
                schema: "video_games",
                columns: table => new
                {
                    region_id = table.Column<int>(type: "integer", nullable: false),
                    game_platform_id = table.Column<int>(type: "integer", nullable: false),
                    num_sales = table.Column<decimal>(type: "numeric(5,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_region_sales", x => new { x.region_id, x.game_platform_id });
                    table.ForeignKey(
                        name: "FK_region_sales_game_platform_game_platform_id",
                        column: x => x.game_platform_id,
                        principalSchema: "video_games",
                        principalTable: "game_platform",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_region_sales_region_region_id",
                        column: x => x.region_id,
                        principalSchema: "video_games",
                        principalTable: "region",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_game_genre_id",
                schema: "video_games",
                table: "game",
                column: "genre_id");

            migrationBuilder.CreateIndex(
                name: "IX_game_platform_game_publisher_id",
                schema: "video_games",
                table: "game_platform",
                column: "game_publisher_id");

            migrationBuilder.CreateIndex(
                name: "IX_game_platform_platform_id",
                schema: "video_games",
                table: "game_platform",
                column: "platform_id");

            migrationBuilder.CreateIndex(
                name: "IX_game_publisher_game_id",
                schema: "video_games",
                table: "game_publisher",
                column: "game_id");

            migrationBuilder.CreateIndex(
                name: "IX_game_publisher_publisher_id",
                schema: "video_games",
                table: "game_publisher",
                column: "publisher_id");

            migrationBuilder.CreateIndex(
                name: "IX_region_sales_game_platform_id",
                schema: "video_games",
                table: "region_sales",
                column: "game_platform_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "region_sales",
                schema: "video_games");

            migrationBuilder.DropTable(
                name: "game_platform",
                schema: "video_games");

            migrationBuilder.DropTable(
                name: "region",
                schema: "video_games");

            migrationBuilder.DropTable(
                name: "game_publisher",
                schema: "video_games");

            migrationBuilder.DropTable(
                name: "platform",
                schema: "video_games");

            migrationBuilder.DropTable(
                name: "game",
                schema: "video_games");

            migrationBuilder.DropTable(
                name: "publisher",
                schema: "video_games");

            migrationBuilder.DropTable(
                name: "genre",
                schema: "video_games");
        }
    }
}
