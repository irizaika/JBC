using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace JBC.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "JobCategories",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", maxLength: 100, nullable: false),
                    Hours = table.Column<decimal>(type: "TEXT", nullable: true),
                    IsCustom = table.Column<bool>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_JobCategories", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Partners",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    CompanyName = table.Column<string>(type: "TEXT", maxLength: 100, nullable: false),
                    ShortName = table.Column<string>(type: "TEXT", maxLength: 50, nullable: false),
                    Address = table.Column<string>(type: "TEXT", maxLength: 200, nullable: true),
                    Phone1 = table.Column<string>(type: "TEXT", maxLength: 30, nullable: true),
                    Phone2 = table.Column<string>(type: "TEXT", maxLength: 30, nullable: true),
                    Email = table.Column<string>(type: "TEXT", maxLength: 100, nullable: true),
                    Status = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Partners", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PartnersRates",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    PartnerId = table.Column<int>(type: "INTEGER", nullable: false),
                    JobTypeId = table.Column<int>(type: "INTEGER", nullable: false),
                    Pay = table.Column<decimal>(type: "decimal(10,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PartnersRates", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Roles",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    RoleName = table.Column<string>(type: "TEXT", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Roles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Vans",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    VanName = table.Column<string>(type: "TEXT", maxLength: 100, nullable: false),
                    Details = table.Column<string>(type: "TEXT", maxLength: 200, nullable: true),
                    Plate = table.Column<string>(type: "TEXT", maxLength: 20, nullable: true),
                    Status = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Vans", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "JobTypes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", maxLength: 100, nullable: false),
                    JobCategoryId = table.Column<int>(type: "INTEGER", nullable: true),
                    Description = table.Column<string>(type: "TEXT", maxLength: 200, nullable: true),
                    NumberOfPeople = table.Column<int>(type: "INTEGER", nullable: false),
                    NumberOfVans = table.Column<int>(type: "INTEGER", nullable: false),
                    PartnerId = table.Column<int>(type: "INTEGER", nullable: true),
                    PayRate = table.Column<decimal>(type: "decimal(10,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_JobTypes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_JobTypes_JobCategories_JobCategoryId",
                        column: x => x.JobCategoryId,
                        principalTable: "JobCategories",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_JobTypes_Partners_PartnerId",
                        column: x => x.PartnerId,
                        principalTable: "Partners",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Contractors",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", maxLength: 100, nullable: false),
                    ShortName = table.Column<string>(type: "TEXT", maxLength: 50, nullable: false),
                    Address = table.Column<string>(type: "TEXT", maxLength: 200, nullable: true),
                    Phone = table.Column<string>(type: "TEXT", maxLength: 30, nullable: true),
                    Email = table.Column<string>(type: "TEXT", maxLength: 100, nullable: true),
                    BankAccount = table.Column<string>(type: "TEXT", maxLength: 50, nullable: true),
                    Status = table.Column<int>(type: "INTEGER", nullable: false),
                    DayRate = table.Column<decimal>(type: "decimal(10,2)", nullable: false),
                    RoleId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Contractors", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Contractors_Roles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "Roles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "RoleRatesPerJobCategory",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    JobCategoryId = table.Column<int>(type: "INTEGER", nullable: false),
                    RoleId = table.Column<int>(type: "INTEGER", nullable: false),
                    Pay = table.Column<decimal>(type: "decimal(10,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RoleRatesPerJobCategory", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RoleRatesPerJobCategory_JobCategories_JobCategoryId",
                        column: x => x.JobCategoryId,
                        principalTable: "JobCategories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_RoleRatesPerJobCategory_Roles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "Roles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Jobs",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Date = table.Column<DateOnly>(type: "date", nullable: false),
                    PartnerId = table.Column<int>(type: "INTEGER", nullable: true),
                    CustomerName = table.Column<string>(type: "TEXT", maxLength: 150, nullable: true),
                    Details = table.Column<string>(type: "TEXT", maxLength: 500, nullable: true),
                    Count = table.Column<decimal>(type: "decimal(5,2)", nullable: false),
                    PayReceived = table.Column<decimal>(type: "decimal(10,2)", nullable: false),
                    JobTypeId = table.Column<int>(type: "INTEGER", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Jobs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Jobs_JobTypes_JobTypeId",
                        column: x => x.JobTypeId,
                        principalTable: "JobTypes",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Jobs_Partners_PartnerId",
                        column: x => x.PartnerId,
                        principalTable: "Partners",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "ContractorRates",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    ContractorId = table.Column<int>(type: "INTEGER", nullable: false),
                    JobTypeId = table.Column<int>(type: "INTEGER", nullable: false),
                    Pay = table.Column<decimal>(type: "decimal(10,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ContractorRates", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ContractorRates_Contractors_ContractorId",
                        column: x => x.ContractorId,
                        principalTable: "Contractors",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ContractorRates_JobTypes_JobTypeId",
                        column: x => x.JobTypeId,
                        principalTable: "JobTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ContractorRatesPErJobType",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    JobTypeId = table.Column<int>(type: "INTEGER", nullable: false),
                    ContractorId = table.Column<int>(type: "INTEGER", nullable: false),
                    Pay = table.Column<decimal>(type: "decimal(10,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ContractorRatesPErJobType", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ContractorRatesPErJobType_Contractors_ContractorId",
                        column: x => x.ContractorId,
                        principalTable: "Contractors",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ContractorRatesPErJobType_JobTypes_JobTypeId",
                        column: x => x.JobTypeId,
                        principalTable: "JobTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "JobContractors",
                columns: table => new
                {
                    JobId = table.Column<int>(type: "INTEGER", nullable: false),
                    ContractorId = table.Column<int>(type: "INTEGER", nullable: false),
                    Pay = table.Column<double>(type: "REAL", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_JobContractors", x => new { x.JobId, x.ContractorId });
                    table.ForeignKey(
                        name: "FK_JobContractors_Contractors_ContractorId",
                        column: x => x.ContractorId,
                        principalTable: "Contractors",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_JobContractors_Jobs_JobId",
                        column: x => x.JobId,
                        principalTable: "Jobs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "JobVans",
                columns: table => new
                {
                    JobId = table.Column<int>(type: "INTEGER", nullable: false),
                    VanId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_JobVans", x => new { x.JobId, x.VanId });
                    table.ForeignKey(
                        name: "FK_JobVans_Jobs_JobId",
                        column: x => x.JobId,
                        principalTable: "Jobs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_JobVans_Vans_VanId",
                        column: x => x.VanId,
                        principalTable: "Vans",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ContractorRates_ContractorId",
                table: "ContractorRates",
                column: "ContractorId");

            migrationBuilder.CreateIndex(
                name: "IX_ContractorRates_JobTypeId",
                table: "ContractorRates",
                column: "JobTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_ContractorRatesPErJobType_ContractorId",
                table: "ContractorRatesPErJobType",
                column: "ContractorId");

            migrationBuilder.CreateIndex(
                name: "IX_ContractorRatesPErJobType_JobTypeId",
                table: "ContractorRatesPErJobType",
                column: "JobTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Contractors_Name",
                table: "Contractors",
                column: "Name");

            migrationBuilder.CreateIndex(
                name: "IX_Contractors_RoleId",
                table: "Contractors",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "IX_Contractors_Status",
                table: "Contractors",
                column: "Status");

            migrationBuilder.CreateIndex(
                name: "IX_JobCategories_Name",
                table: "JobCategories",
                column: "Name");

            migrationBuilder.CreateIndex(
                name: "IX_JobContractors_ContractorId",
                table: "JobContractors",
                column: "ContractorId");

            migrationBuilder.CreateIndex(
                name: "IX_Jobs_Date",
                table: "Jobs",
                column: "Date");

            migrationBuilder.CreateIndex(
                name: "IX_Jobs_JobTypeId",
                table: "Jobs",
                column: "JobTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Jobs_PartnerId",
                table: "Jobs",
                column: "PartnerId");

            migrationBuilder.CreateIndex(
                name: "IX_JobTypes_JobCategoryId",
                table: "JobTypes",
                column: "JobCategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_JobTypes_Name",
                table: "JobTypes",
                column: "Name");

            migrationBuilder.CreateIndex(
                name: "IX_JobTypes_PartnerId",
                table: "JobTypes",
                column: "PartnerId");

            migrationBuilder.CreateIndex(
                name: "IX_JobVans_VanId",
                table: "JobVans",
                column: "VanId");

            migrationBuilder.CreateIndex(
                name: "IX_Partners_CompanyName",
                table: "Partners",
                column: "CompanyName");

            migrationBuilder.CreateIndex(
                name: "IX_Partners_Status",
                table: "Partners",
                column: "Status");

            migrationBuilder.CreateIndex(
                name: "IX_PartnersRates_JobTypeId",
                table: "PartnersRates",
                column: "JobTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_PartnersRates_PartnerId",
                table: "PartnersRates",
                column: "PartnerId");

            migrationBuilder.CreateIndex(
                name: "IX_RoleRatesPerJobCategory_JobCategoryId",
                table: "RoleRatesPerJobCategory",
                column: "JobCategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_RoleRatesPerJobCategory_RoleId",
                table: "RoleRatesPerJobCategory",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "IX_Roles_RoleName",
                table: "Roles",
                column: "RoleName");

            migrationBuilder.CreateIndex(
                name: "IX_Vans_Status",
                table: "Vans",
                column: "Status");

            migrationBuilder.CreateIndex(
                name: "IX_Vans_VanName",
                table: "Vans",
                column: "VanName");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ContractorRates");

            migrationBuilder.DropTable(
                name: "ContractorRatesPErJobType");

            migrationBuilder.DropTable(
                name: "JobContractors");

            migrationBuilder.DropTable(
                name: "JobVans");

            migrationBuilder.DropTable(
                name: "PartnersRates");

            migrationBuilder.DropTable(
                name: "RoleRatesPerJobCategory");

            migrationBuilder.DropTable(
                name: "Contractors");

            migrationBuilder.DropTable(
                name: "Jobs");

            migrationBuilder.DropTable(
                name: "Vans");

            migrationBuilder.DropTable(
                name: "Roles");

            migrationBuilder.DropTable(
                name: "JobTypes");

            migrationBuilder.DropTable(
                name: "JobCategories");

            migrationBuilder.DropTable(
                name: "Partners");
        }
    }
}
