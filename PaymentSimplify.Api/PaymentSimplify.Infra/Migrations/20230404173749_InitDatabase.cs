using System;
using Microsoft.EntityFrameworkCore.Migrations;
using MySql.EntityFrameworkCore.Metadata;

#nullable disable

namespace PaymentSimplify.Infra.Migrations
{
    /// <inheritdoc />
    public partial class InitDatabase : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterDatabase()
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "ACCOUNT_BANK",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "char(36)", nullable: false),
                    CURRENCY = table.Column<string>(type: "longtext", nullable: false),
                    AMOUNT = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    CREATED = table.Column<DateTime>(type: "datetime(6)", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    CREATED_BY = table.Column<string>(type: "longtext", nullable: true),
                    LAST_MODIFIED = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    LAST_MODIFIED_BE = table.Column<string>(type: "longtext", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ACCOUNT_BANK", x => x.ID);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "CUSTOMER",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "char(36)", nullable: false),
                    ACCOUNT_BANK_ID = table.Column<Guid>(type: "char(36)", nullable: false),
                    DOCUMENT = table.Column<string>(type: "varchar(20)", maxLength: 20, nullable: false),
                    TYPE_DOCUMENT = table.Column<int>(type: "int", nullable: false),
                    FIRST_NAME = table.Column<string>(type: "varchar(250)", maxLength: 250, nullable: false),
                    LAST_NAME = table.Column<string>(type: "varchar(250)", maxLength: 250, nullable: false),
                    CREATED = table.Column<DateTime>(type: "datetime(6)", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    CREATED_BY = table.Column<string>(type: "longtext", nullable: true),
                    LAST_MODIFIED = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    LAST_MODIFIED_BE = table.Column<string>(type: "longtext", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CUSTOMER", x => x.ID);
                    table.ForeignKey(
                        name: "FK_CUSTOMER_ACCOUNT_BANK_ACCOUNT_BANK_ID",
                        column: x => x.ACCOUNT_BANK_ID,
                        principalTable: "ACCOUNT_BANK",
                        principalColumn: "ID");
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "AUTH",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "char(36)", nullable: false),
                    COSTUMER_ID = table.Column<Guid>(type: "char(36)", nullable: false),
                    EMAIL = table.Column<string>(type: "varchar(80)", maxLength: 80, nullable: false),
                    PASSWORD = table.Column<string>(type: "longtext", nullable: false),
                    CREATED = table.Column<DateTime>(type: "datetime(6)", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    CREATED_BY = table.Column<string>(type: "longtext", nullable: true),
                    LAST_MODIFIED = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    LAST_MODIFIED_BE = table.Column<string>(type: "longtext", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AUTH", x => x.ID);
                    table.ForeignKey(
                        name: "FK_AUTH_CUSTOMER_COSTUMER_ID",
                        column: x => x.COSTUMER_ID,
                        principalTable: "CUSTOMER",
                        principalColumn: "ID");
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "TRANSACTIONS",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "char(36)", nullable: false),
                    ID_COSTUMER_PAYEE = table.Column<Guid>(type: "char(36)", nullable: false),
                    ID_ACCOUNT_BANK = table.Column<Guid>(type: "char(36)", nullable: false),
                    CREATED = table.Column<DateTime>(type: "datetime(6)", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    CREATED_BY = table.Column<string>(type: "longtext", nullable: true),
                    LAST_MODIFIED = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    LAST_MODIFIED_BE = table.Column<string>(type: "longtext", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TRANSACTIONS", x => x.ID);
                    table.ForeignKey(
                        name: "FK_TRANSACTIONS_ACCOUNT_BANK_ID_ACCOUNT_BANK",
                        column: x => x.ID_ACCOUNT_BANK,
                        principalTable: "ACCOUNT_BANK",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TRANSACTIONS_CUSTOMER_ID_COSTUMER_PAYEE",
                        column: x => x.ID_COSTUMER_PAYEE,
                        principalTable: "CUSTOMER",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_AUTH_COSTUMER_ID",
                table: "AUTH",
                column: "COSTUMER_ID",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_AUTH_EMAIL",
                table: "AUTH",
                column: "EMAIL",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_CUSTOMER_ACCOUNT_BANK_ID",
                table: "CUSTOMER",
                column: "ACCOUNT_BANK_ID");

            migrationBuilder.CreateIndex(
                name: "IX_CUSTOMER_DOCUMENT",
                table: "CUSTOMER",
                column: "DOCUMENT",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_TRANSACTIONS_ID_ACCOUNT_BANK",
                table: "TRANSACTIONS",
                column: "ID_ACCOUNT_BANK");

            migrationBuilder.CreateIndex(
                name: "IX_TRANSACTIONS_ID_COSTUMER_PAYEE",
                table: "TRANSACTIONS",
                column: "ID_COSTUMER_PAYEE");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AUTH");

            migrationBuilder.DropTable(
                name: "TRANSACTIONS");

            migrationBuilder.DropTable(
                name: "CUSTOMER");

            migrationBuilder.DropTable(
                name: "ACCOUNT_BANK");
        }
    }
}
