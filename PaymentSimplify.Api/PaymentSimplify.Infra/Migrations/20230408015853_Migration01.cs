using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PaymentSimplify.Infra.Migrations
{
    /// <inheritdoc />
    public partial class Migration01 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "SALT",
                table: "AUTH",
                type: "longtext",
                nullable: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SALT",
                table: "AUTH");
        }
    }
}
