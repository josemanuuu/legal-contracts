using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace legal_contracts_back.Migrations
{
    /// <inheritdoc />
    public partial class AddContracttable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Contracts",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Author = table.Column<string>(type: "text", nullable: false),
                    EntityName = table.Column<string>(type: "text", nullable: false),
                    Description = table.Column<string>(type: "text", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Contracts", x => x.Id);
                });

            Random random = new Random();

            for (int i = 1; i <= 30; i++)
            {
                // Fecha aleatoria hasta hoy
                int daysAgo = random.Next(0, 365);
                DateTime createdAt = DateTime.UtcNow.AddDays(-daysAgo);

                DateTime? updatedAt = (i % 2 == 0) ? createdAt.AddHours(random.Next(1, 24)) : null;

                // Descripción en el 50% de los casos
                string description = (random.NextDouble() < 0.5) ? $"Placeholder description {i}" : null;

                migrationBuilder.InsertData(
                    table: "Contracts",
                    columns: new[] { "Author", "EntityName", "Description", "CreatedAt", "UpdatedAt" },
                    values: new object[] { $"Author {i}", $"Entity {i}", description, createdAt, updatedAt }
                );
            }
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Contracts");
        }
    }
}
