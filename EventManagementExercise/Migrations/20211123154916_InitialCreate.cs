using Microsoft.EntityFrameworkCore.Migrations;
using System;

namespace EventManagementExercise.Migrations
{
  public partial class InitialCreate : Migration
  {
    protected override void Up(MigrationBuilder migrationBuilder)
    {
      migrationBuilder.CreateTable(
          name: "Allergies",
          columns: table => new
          {
            Id = table.Column<int>(type: "int", nullable: false)
                  .Annotation("SqlServer:Identity", "1, 1"),
            Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
            NormalizedName = table.Column<string>(type: "nvarchar(450)", nullable: false)
          },
          constraints: table =>
          {
            table.PrimaryKey("PK_Allergies", x => x.Id);
          });

      migrationBuilder.CreateTable(
          name: "Events",
          columns: table => new
          {
            Id = table.Column<int>(type: "int", nullable: false)
                  .Annotation("SqlServer:Identity", "1, 1"),
            Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
            Date = table.Column<DateTime>(type: "datetime2", nullable: false)
          },
          constraints: table =>
          {
            table.PrimaryKey("PK_Events", x => x.Id);
          });

      migrationBuilder.CreateTable(
          name: "Guests",
          columns: table => new
          {
            Id = table.Column<int>(type: "int", nullable: false)
                  .Annotation("SqlServer:Identity", "1, 1"),
            FirstName = table.Column<string>(type: "nvarchar(max)", nullable: false),
            LastName = table.Column<string>(type: "nvarchar(max)", nullable: false),
            NormalizedName = table.Column<string>(type: "nvarchar(max)", nullable: false),
            Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
            NormalizedEmail = table.Column<string>(type: "nvarchar(450)", nullable: false),
            DOB = table.Column<DateTime>(type: "datetime2", nullable: true)
          },
          constraints: table =>
          {
            table.PrimaryKey("PK_Guests", x => x.Id);
          });

      migrationBuilder.CreateTable(
          name: "AllergyGuest",
          columns: table => new
          {
            AllergiesId = table.Column<int>(type: "int", nullable: false),
            GuestsId = table.Column<int>(type: "int", nullable: false)
          },
          constraints: table =>
          {
            table.PrimaryKey("PK_AllergyGuest", x => new { x.AllergiesId, x.GuestsId });
            table.ForeignKey(
                      name: "FK_AllergyGuest_Allergies_AllergiesId",
                      column: x => x.AllergiesId,
                      principalTable: "Allergies",
                      principalColumn: "Id",
                      onDelete: ReferentialAction.Cascade);
            table.ForeignKey(
                      name: "FK_AllergyGuest_Guests_GuestsId",
                      column: x => x.GuestsId,
                      principalTable: "Guests",
                      principalColumn: "Id",
                      onDelete: ReferentialAction.Cascade);
          });

      migrationBuilder.CreateTable(
          name: "EventGuest",
          columns: table => new
          {
            EventsId = table.Column<int>(type: "int", nullable: false),
            GuestsId = table.Column<int>(type: "int", nullable: false)
          },
          constraints: table =>
          {
            table.PrimaryKey("PK_EventGuest", x => new { x.EventsId, x.GuestsId });
            table.ForeignKey(
                      name: "FK_EventGuest_Events_EventsId",
                      column: x => x.EventsId,
                      principalTable: "Events",
                      principalColumn: "Id",
                      onDelete: ReferentialAction.Cascade);
            table.ForeignKey(
                      name: "FK_EventGuest_Guests_GuestsId",
                      column: x => x.GuestsId,
                      principalTable: "Guests",
                      principalColumn: "Id",
                      onDelete: ReferentialAction.Cascade);
          });

      migrationBuilder.CreateIndex(
          name: "IX_Allergies_NormalizedName",
          table: "Allergies",
          column: "NormalizedName",
          unique: true);

      migrationBuilder.CreateIndex(
          name: "IX_AllergyGuest_GuestsId",
          table: "AllergyGuest",
          column: "GuestsId");

      migrationBuilder.CreateIndex(
          name: "IX_EventGuest_GuestsId",
          table: "EventGuest",
          column: "GuestsId");

      migrationBuilder.CreateIndex(
          name: "IX_Guests_NormalizedEmail",
          table: "Guests",
          column: "NormalizedEmail",
          unique: true);
    }

    protected override void Down(MigrationBuilder migrationBuilder)
    {
      migrationBuilder.DropTable(
          name: "AllergyGuest");

      migrationBuilder.DropTable(
          name: "EventGuest");

      migrationBuilder.DropTable(
          name: "Allergies");

      migrationBuilder.DropTable(
          name: "Events");

      migrationBuilder.DropTable(
          name: "Guests");
    }
  }
}
