namespace Minions.Data.Migrations
{
    using Microsoft.EntityFrameworkCore.Metadata;
    using Microsoft.EntityFrameworkCore.Migrations;

    public partial class InitialTables : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Countries",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Countries", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "EvilnessFactors",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EvilnessFactors", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Towns",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(maxLength: 50, nullable: false),
                    CountryId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Towns", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Towns_Countries_CountryId",
                        column: x => x.CountryId,
                        principalTable: "Countries",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Villains",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(maxLength: 50, nullable: false),
                    EvilnessFactorId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Villains", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Villains_EvilnessFactors_EvilnessFactorId",
                        column: x => x.EvilnessFactorId,
                        principalTable: "EvilnessFactors",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Minions",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(maxLength: 50, nullable: false),
                    Age = table.Column<int>(nullable: false),
                    TownId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Minions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Minions_Towns_TownId",
                        column: x => x.TownId,
                        principalTable: "Towns",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "MinionsVillains",
                columns: table => new
                {
                    MinionId = table.Column<int>(nullable: false),
                    VillainId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MinionsVillains", x => new { x.MinionId, x.VillainId });
                    table.ForeignKey(
                        name: "FK_MinionsVillains_Minions_MinionId",
                        column: x => x.MinionId,
                        principalTable: "Minions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MinionsVillains_Villains_VillainId",
                        column: x => x.VillainId,
                        principalTable: "Villains",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Minions_TownId",
                table: "Minions",
                column: "TownId");

            migrationBuilder.CreateIndex(
                name: "IX_MinionsVillains_VillainId",
                table: "MinionsVillains",
                column: "VillainId");

            migrationBuilder.CreateIndex(
                name: "IX_Towns_CountryId",
                table: "Towns",
                column: "CountryId");

            migrationBuilder.CreateIndex(
                name: "IX_Villains_EvilnessFactorId",
                table: "Villains",
                column: "EvilnessFactorId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MinionsVillains");

            migrationBuilder.DropTable(
                name: "Minions");

            migrationBuilder.DropTable(
                name: "Villains");

            migrationBuilder.DropTable(
                name: "Towns");

            migrationBuilder.DropTable(
                name: "EvilnessFactors");

            migrationBuilder.DropTable(
                name: "Countries");
        }
    }
}