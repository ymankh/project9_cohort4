using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace project9_cohort4.Server.Migrations
{
    /// <inheritdoc />
    public partial class init : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Animals",
                columns: table => new
                {
                    AnimalId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Species = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Breed = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Age = table.Column<int>(type: "int", nullable: true),
                    Size = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    Temperament = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    SpecialNeeds = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AdoptionStatus = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true, defaultValue: "Available"),
                    PhotoUrl = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "(getdate())"),
                    UpdatedAt = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "(getdate())")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Animals__A21A7307727FCE24", x => x.AnimalId);
                });

            migrationBuilder.CreateTable(
                name: "Shelters",
                columns: table => new
                {
                    ShelterId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ShelterName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ContactEmail = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    Phone = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    Address = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "(getdate())"),
                    UpdatedAt = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "(getdate())")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Shelters__E2CDF5546EC5EA4D", x => x.ShelterId);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    UserId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Username = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    PasswordHash = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    FullName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "(getdate())"),
                    UpdatedAt = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "(getdate())"),
                    IsAdmin = table.Column<bool>(type: "bit", nullable: true, defaultValue: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Users__1788CC4C19EDAB47", x => x.UserId);
                });

            migrationBuilder.CreateTable(
                name: "ShelterAnimals",
                columns: table => new
                {
                    ShelterAnimalId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ShelterId = table.Column<int>(type: "int", nullable: false),
                    AnimalId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__ShelterA__28908D34344C5F85", x => x.ShelterAnimalId);
                    table.ForeignKey(
                        name: "FK__ShelterAn__Anima__52593CB8",
                        column: x => x.AnimalId,
                        principalTable: "Animals",
                        principalColumn: "AnimalId");
                    table.ForeignKey(
                        name: "FK__ShelterAn__Shelt__5165187F",
                        column: x => x.ShelterId,
                        principalTable: "Shelters",
                        principalColumn: "ShelterId");
                });

            migrationBuilder.CreateTable(
                name: "AdoptionApplications",
                columns: table => new
                {
                    ApplicationId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    AnimalId = table.Column<int>(type: "int", nullable: false),
                    ApplicationDate = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "(getdate())"),
                    Status = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true, defaultValue: "Pending")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Adoption__C93A4C99FFEC99D8", x => x.ApplicationId);
                    table.ForeignKey(
                        name: "FK__AdoptionA__Anima__45F365D3",
                        column: x => x.AnimalId,
                        principalTable: "Animals",
                        principalColumn: "AnimalId");
                    table.ForeignKey(
                        name: "FK__AdoptionA__UserI__44FF419A",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "UserId");
                });

            migrationBuilder.CreateTable(
                name: "SuccessStories",
                columns: table => new
                {
                    StoryId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    AnimalId = table.Column<int>(type: "int", nullable: false),
                    StoryText = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    StoryDate = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "(getdate())"),
                    PhotoUrl = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__SuccessS__3E82C048F6DBE715", x => x.StoryId);
                    table.ForeignKey(
                        name: "FK__SuccessSt__Anima__4AB81AF0",
                        column: x => x.AnimalId,
                        principalTable: "Animals",
                        principalColumn: "AnimalId");
                    table.ForeignKey(
                        name: "FK__SuccessSt__UserI__49C3F6B7",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "UserId");
                });

            migrationBuilder.CreateIndex(
                name: "IX_AdoptionApplications_AnimalId",
                table: "AdoptionApplications",
                column: "AnimalId");

            migrationBuilder.CreateIndex(
                name: "IX_AdoptionApplications_UserId",
                table: "AdoptionApplications",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_ShelterAnimals_AnimalId",
                table: "ShelterAnimals",
                column: "AnimalId");

            migrationBuilder.CreateIndex(
                name: "IX_ShelterAnimals_ShelterId",
                table: "ShelterAnimals",
                column: "ShelterId");

            migrationBuilder.CreateIndex(
                name: "IX_SuccessStories_AnimalId",
                table: "SuccessStories",
                column: "AnimalId");

            migrationBuilder.CreateIndex(
                name: "IX_SuccessStories_UserId",
                table: "SuccessStories",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "UQ__Users__536C85E4585A1460",
                table: "Users",
                column: "Username",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "UQ__Users__A9D1053403862BC9",
                table: "Users",
                column: "Email",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AdoptionApplications");

            migrationBuilder.DropTable(
                name: "ShelterAnimals");

            migrationBuilder.DropTable(
                name: "SuccessStories");

            migrationBuilder.DropTable(
                name: "Shelters");

            migrationBuilder.DropTable(
                name: "Animals");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
