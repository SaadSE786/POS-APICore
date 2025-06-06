using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace POS_API.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "tblCompanies",
                columns: table => new
                {
                    intCompanyId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    varCompanyName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    varCompanyAddress = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    varContactEmail = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    varContactNo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    varCompanyLogo = table.Column<byte[]>(type: "varbinary(max)", nullable: true),
                    dtCreationDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    dtUpdationDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblCompanies", x => x.intCompanyId);
                });

            migrationBuilder.CreateTable(
                name: "tblLevel1",
                columns: table => new
                {
                    intLevel1Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    varLevel1Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    dtCreationDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    dtUpdationDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    intCreatedBy = table.Column<int>(type: "int", nullable: true),
                    intUpdatedBy = table.Column<int>(type: "int", nullable: true),
                    intCompanyId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblLevel1", x => x.intLevel1Id);
                });

            migrationBuilder.CreateTable(
                name: "tblModules",
                columns: table => new
                {
                    intModuleId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    varModuleName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    dtCreationDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    dtUpdationDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblModules", x => x.intModuleId);
                });

            migrationBuilder.CreateTable(
                name: "tblItems",
                columns: table => new
                {
                    intItemId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    intCompanyId = table.Column<int>(type: "int", nullable: true),
                    varItemName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    dcOpenStock = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    dcMinLevel = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    dcMaxLevel = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    dcOrderLevel = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    dtOpenDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    isActive = table.Column<bool>(type: "bit", nullable: true),
                    dcSellRate = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    dcPurRate = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    dcRetailSaleRate = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    dcDistributorSaleRate = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    dcDiscount = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    isTaxable = table.Column<bool>(type: "bit", nullable: true),
                    isExpirable = table.Column<bool>(type: "bit", nullable: true),
                    varUom = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    dtExpiryDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    dtCreationDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    dtUpdationDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    intCreatedBy = table.Column<int>(type: "int", nullable: true),
                    intUpdatedBy = table.Column<int>(type: "int", nullable: true),
                    tblCompanyintCompanyId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblItems", x => x.intItemId);
                    table.ForeignKey(
                        name: "FK_tblItems_tblCompanies_tblCompanyintCompanyId",
                        column: x => x.tblCompanyintCompanyId,
                        principalTable: "tblCompanies",
                        principalColumn: "intCompanyId");
                });

            migrationBuilder.CreateTable(
                name: "tblRoles",
                columns: table => new
                {
                    intRoleId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    intCompanyId = table.Column<int>(type: "int", nullable: true),
                    varName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    dtCreationDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    dtUpdationDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    intCreatedBy = table.Column<int>(type: "int", nullable: true),
                    intUpdatedBy = table.Column<int>(type: "int", nullable: true),
                    tblCompanyintCompanyId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblRoles", x => x.intRoleId);
                    table.ForeignKey(
                        name: "FK_tblRoles_tblCompanies_tblCompanyintCompanyId",
                        column: x => x.tblCompanyintCompanyId,
                        principalTable: "tblCompanies",
                        principalColumn: "intCompanyId");
                });

            migrationBuilder.CreateTable(
                name: "tblTransporters",
                columns: table => new
                {
                    intTransporterId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    varTransporterName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    varContactNo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    varAddress = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    varEmail = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    dtCreationDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    dtUpdationDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    intCreatedBy = table.Column<int>(type: "int", nullable: true),
                    intUpdatedBy = table.Column<int>(type: "int", nullable: true),
                    intCompanyId = table.Column<int>(type: "int", nullable: true),
                    tblCompanyintCompanyId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblTransporters", x => x.intTransporterId);
                    table.ForeignKey(
                        name: "FK_tblTransporters_tblCompanies_tblCompanyintCompanyId",
                        column: x => x.tblCompanyintCompanyId,
                        principalTable: "tblCompanies",
                        principalColumn: "intCompanyId");
                });

            migrationBuilder.CreateTable(
                name: "tblUsers",
                columns: table => new
                {
                    intUserId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    intCompanyId = table.Column<int>(type: "int", nullable: true),
                    varName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    varEmail = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    varPassword = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    varAddress = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    varCnic = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    varContactNo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    isAdmin = table.Column<bool>(type: "bit", nullable: true),
                    varPhoto = table.Column<byte[]>(type: "varbinary(max)", nullable: true),
                    dtCreationDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    dtUpdationDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    tblCompanyintCompanyId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblUsers", x => x.intUserId);
                    table.ForeignKey(
                        name: "FK_tblUsers_tblCompanies_tblCompanyintCompanyId",
                        column: x => x.tblCompanyintCompanyId,
                        principalTable: "tblCompanies",
                        principalColumn: "intCompanyId");
                });

            migrationBuilder.CreateTable(
                name: "tblWarehouses",
                columns: table => new
                {
                    intWarehouseId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    varWarehouseName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    dtCreationDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    dtUpdationDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    intCreatedBy = table.Column<int>(type: "int", nullable: true),
                    intUpdatedBy = table.Column<int>(type: "int", nullable: true),
                    intCompanyId = table.Column<int>(type: "int", nullable: true),
                    tblCompanyintCompanyId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblWarehouses", x => x.intWarehouseId);
                    table.ForeignKey(
                        name: "FK_tblWarehouses_tblCompanies_tblCompanyintCompanyId",
                        column: x => x.tblCompanyintCompanyId,
                        principalTable: "tblCompanies",
                        principalColumn: "intCompanyId");
                });

            migrationBuilder.CreateTable(
                name: "tblLevel2",
                columns: table => new
                {
                    intLevel2Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    intLevel1Id = table.Column<int>(type: "int", nullable: true),
                    varLevel2Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    dtCreationDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    dtUpdationDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    intCreatedBy = table.Column<int>(type: "int", nullable: true),
                    intUpdatedBy = table.Column<int>(type: "int", nullable: true),
                    tblLevel1intLevel1Id = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblLevel2", x => x.intLevel2Id);
                    table.ForeignKey(
                        name: "FK_tblLevel2_tblLevel1_tblLevel1intLevel1Id",
                        column: x => x.tblLevel1intLevel1Id,
                        principalTable: "tblLevel1",
                        principalColumn: "intLevel1Id");
                });

            migrationBuilder.CreateTable(
                name: "tblComponents",
                columns: table => new
                {
                    intComponentId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    intModuleId = table.Column<int>(type: "int", nullable: true),
                    varComponentName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    dtCreationDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    dtUpdationDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    tblModuleintModuleId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblComponents", x => x.intComponentId);
                    table.ForeignKey(
                        name: "FK_tblComponents_tblModules_tblModuleintModuleId",
                        column: x => x.tblModuleintModuleId,
                        principalTable: "tblModules",
                        principalColumn: "intModuleId");
                });

            migrationBuilder.CreateTable(
                name: "tblRoletblUser",
                columns: table => new
                {
                    tblRolesintRoleId = table.Column<int>(type: "int", nullable: false),
                    tblUsersintUserId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblRoletblUser", x => new { x.tblRolesintRoleId, x.tblUsersintUserId });
                    table.ForeignKey(
                        name: "FK_tblRoletblUser_tblRoles_tblRolesintRoleId",
                        column: x => x.tblRolesintRoleId,
                        principalTable: "tblRoles",
                        principalColumn: "intRoleId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_tblRoletblUser_tblUsers_tblUsersintUserId",
                        column: x => x.tblUsersintUserId,
                        principalTable: "tblUsers",
                        principalColumn: "intUserId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "tblLevel3",
                columns: table => new
                {
                    intLevel3Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    intLevel2Id = table.Column<int>(type: "int", nullable: true),
                    varLevel3Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    dtCreationDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    dtUpdationDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    intCreatedBy = table.Column<int>(type: "int", nullable: true),
                    intUpdatedBy = table.Column<int>(type: "int", nullable: true),
                    tblLevel2intLevel2Id = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblLevel3", x => x.intLevel3Id);
                    table.ForeignKey(
                        name: "FK_tblLevel3_tblLevel2_tblLevel2intLevel2Id",
                        column: x => x.tblLevel2intLevel2Id,
                        principalTable: "tblLevel2",
                        principalColumn: "intLevel2Id");
                });

            migrationBuilder.CreateTable(
                name: "tblFields",
                columns: table => new
                {
                    intFieldId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    intComponentId = table.Column<int>(type: "int", nullable: true),
                    varFieldName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    dtCreationDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    dtUpdationDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    tblComponentintComponentId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblFields", x => x.intFieldId);
                    table.ForeignKey(
                        name: "FK_tblFields_tblComponents_tblComponentintComponentId",
                        column: x => x.tblComponentintComponentId,
                        principalTable: "tblComponents",
                        principalColumn: "intComponentId");
                });

            migrationBuilder.CreateTable(
                name: "tblParties",
                columns: table => new
                {
                    intPartyId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    intCompanyId = table.Column<int>(type: "int", nullable: true),
                    varPartyName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    varPartyType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    varAddress = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    varContactNo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    varCity = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    varCountry = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    isActive = table.Column<bool>(type: "bit", nullable: true),
                    intLevel1Id = table.Column<int>(type: "int", nullable: true),
                    intLevel2Id = table.Column<int>(type: "int", nullable: true),
                    intLevel3Id = table.Column<int>(type: "int", nullable: true),
                    dtCreationDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    dtUpdationDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    tblCompanyintCompanyId = table.Column<int>(type: "int", nullable: true),
                    tblLevel1intLevel1Id = table.Column<int>(type: "int", nullable: true),
                    tblLevel2intLevel2Id = table.Column<int>(type: "int", nullable: true),
                    tblLevel3intLevel3Id = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblParties", x => x.intPartyId);
                    table.ForeignKey(
                        name: "FK_tblParties_tblCompanies_tblCompanyintCompanyId",
                        column: x => x.tblCompanyintCompanyId,
                        principalTable: "tblCompanies",
                        principalColumn: "intCompanyId");
                    table.ForeignKey(
                        name: "FK_tblParties_tblLevel1_tblLevel1intLevel1Id",
                        column: x => x.tblLevel1intLevel1Id,
                        principalTable: "tblLevel1",
                        principalColumn: "intLevel1Id");
                    table.ForeignKey(
                        name: "FK_tblParties_tblLevel2_tblLevel2intLevel2Id",
                        column: x => x.tblLevel2intLevel2Id,
                        principalTable: "tblLevel2",
                        principalColumn: "intLevel2Id");
                    table.ForeignKey(
                        name: "FK_tblParties_tblLevel3_tblLevel3intLevel3Id",
                        column: x => x.tblLevel3intLevel3Id,
                        principalTable: "tblLevel3",
                        principalColumn: "intLevel3Id");
                });

            migrationBuilder.CreateTable(
                name: "tblPermissions",
                columns: table => new
                {
                    intPermissionId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    intRoleId = table.Column<int>(type: "int", nullable: true),
                    intModuleId = table.Column<int>(type: "int", nullable: true),
                    intComponentId = table.Column<int>(type: "int", nullable: true),
                    intFieldId = table.Column<int>(type: "int", nullable: true),
                    isActive = table.Column<bool>(type: "bit", nullable: true),
                    dtCreationDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    dtUpdationDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    intCreatedBy = table.Column<int>(type: "int", nullable: true),
                    intUpdatedBy = table.Column<int>(type: "int", nullable: true),
                    tblComponentintComponentId = table.Column<int>(type: "int", nullable: true),
                    tblFieldintFieldId = table.Column<int>(type: "int", nullable: true),
                    tblModuleintModuleId = table.Column<int>(type: "int", nullable: true),
                    tblRoleintRoleId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblPermissions", x => x.intPermissionId);
                    table.ForeignKey(
                        name: "FK_tblPermissions_tblComponents_tblComponentintComponentId",
                        column: x => x.tblComponentintComponentId,
                        principalTable: "tblComponents",
                        principalColumn: "intComponentId");
                    table.ForeignKey(
                        name: "FK_tblPermissions_tblFields_tblFieldintFieldId",
                        column: x => x.tblFieldintFieldId,
                        principalTable: "tblFields",
                        principalColumn: "intFieldId");
                    table.ForeignKey(
                        name: "FK_tblPermissions_tblModules_tblModuleintModuleId",
                        column: x => x.tblModuleintModuleId,
                        principalTable: "tblModules",
                        principalColumn: "intModuleId");
                    table.ForeignKey(
                        name: "FK_tblPermissions_tblRoles_tblRoleintRoleId",
                        column: x => x.tblRoleintRoleId,
                        principalTable: "tblRoles",
                        principalColumn: "intRoleId");
                });

            migrationBuilder.CreateTable(
                name: "tblPledgers",
                columns: table => new
                {
                    intPlid = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    intCompanyId = table.Column<int>(type: "int", nullable: true),
                    intPartyId = table.Column<int>(type: "int", nullable: true),
                    intDcno = table.Column<int>(type: "int", nullable: true),
                    dtVrDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    varDescription = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    varVrType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    dcDebit = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    dcCredit = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    dtCreationDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    dtUpdationDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    intCreatedBy = table.Column<int>(type: "int", nullable: true),
                    intUpdatedBy = table.Column<int>(type: "int", nullable: true),
                    tblCompanyintCompanyId = table.Column<int>(type: "int", nullable: true),
                    tblPartyintPartyId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblPledgers", x => x.intPlid);
                    table.ForeignKey(
                        name: "FK_tblPledgers_tblCompanies_tblCompanyintCompanyId",
                        column: x => x.tblCompanyintCompanyId,
                        principalTable: "tblCompanies",
                        principalColumn: "intCompanyId");
                    table.ForeignKey(
                        name: "FK_tblPledgers_tblParties_tblPartyintPartyId",
                        column: x => x.tblPartyintPartyId,
                        principalTable: "tblParties",
                        principalColumn: "intPartyId");
                });

            migrationBuilder.CreateTable(
                name: "tblStockMains",
                columns: table => new
                {
                    intStid = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    intCompanyId = table.Column<int>(type: "int", nullable: true),
                    intPartyId = table.Column<int>(type: "int", nullable: true),
                    intVrno = table.Column<int>(type: "int", nullable: true),
                    intVrnoA = table.Column<int>(type: "int", nullable: true),
                    dtVrDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    varRemarks = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    intTransporterId = table.Column<int>(type: "int", nullable: true),
                    varVrType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    dcDiscount = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    dcDiscountAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    dcExpense = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    dcAdditionalCharges = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    dcNetAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    dtCreationDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    dtUpdationDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    intCreatedBy = table.Column<int>(type: "int", nullable: true),
                    intUpdatedBy = table.Column<int>(type: "int", nullable: true),
                    dcTotalAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    tblCompanyintCompanyId = table.Column<int>(type: "int", nullable: true),
                    tblPartyintPartyId = table.Column<int>(type: "int", nullable: true),
                    tblTransporterintTransporterId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblStockMains", x => x.intStid);
                    table.ForeignKey(
                        name: "FK_tblStockMains_tblCompanies_tblCompanyintCompanyId",
                        column: x => x.tblCompanyintCompanyId,
                        principalTable: "tblCompanies",
                        principalColumn: "intCompanyId");
                    table.ForeignKey(
                        name: "FK_tblStockMains_tblParties_tblPartyintPartyId",
                        column: x => x.tblPartyintPartyId,
                        principalTable: "tblParties",
                        principalColumn: "intPartyId");
                    table.ForeignKey(
                        name: "FK_tblStockMains_tblTransporters_tblTransporterintTransporterId",
                        column: x => x.tblTransporterintTransporterId,
                        principalTable: "tblTransporters",
                        principalColumn: "intTransporterId");
                });

            migrationBuilder.CreateTable(
                name: "tblStockDetails",
                columns: table => new
                {
                    intStockDetailId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    intStid = table.Column<int>(type: "int", nullable: true),
                    intItemId = table.Column<int>(type: "int", nullable: true),
                    intWarehouseId = table.Column<int>(type: "int", nullable: true),
                    intQuantity = table.Column<int>(type: "int", nullable: true),
                    dcRate = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    dcAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    dcDisc = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    dcDiscAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    dcExclTaxAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    dcTax = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    dcTaxAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    dcInclTaxAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    varType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    dcPurRate = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    dtCreationDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    dtUpdationDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    intCreatedBy = table.Column<int>(type: "int", nullable: true),
                    intUpdatedBy = table.Column<int>(type: "int", nullable: true),
                    tblItemintItemId = table.Column<int>(type: "int", nullable: true),
                    tblStockMainintStid = table.Column<int>(type: "int", nullable: true),
                    tblWarehouseintWarehouseId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblStockDetails", x => x.intStockDetailId);
                    table.ForeignKey(
                        name: "FK_tblStockDetails_tblItems_tblItemintItemId",
                        column: x => x.tblItemintItemId,
                        principalTable: "tblItems",
                        principalColumn: "intItemId");
                    table.ForeignKey(
                        name: "FK_tblStockDetails_tblStockMains_tblStockMainintStid",
                        column: x => x.tblStockMainintStid,
                        principalTable: "tblStockMains",
                        principalColumn: "intStid");
                    table.ForeignKey(
                        name: "FK_tblStockDetails_tblWarehouses_tblWarehouseintWarehouseId",
                        column: x => x.tblWarehouseintWarehouseId,
                        principalTable: "tblWarehouses",
                        principalColumn: "intWarehouseId");
                });

            migrationBuilder.CreateIndex(
                name: "IX_tblComponents_tblModuleintModuleId",
                table: "tblComponents",
                column: "tblModuleintModuleId");

            migrationBuilder.CreateIndex(
                name: "IX_tblFields_tblComponentintComponentId",
                table: "tblFields",
                column: "tblComponentintComponentId");

            migrationBuilder.CreateIndex(
                name: "IX_tblItems_tblCompanyintCompanyId",
                table: "tblItems",
                column: "tblCompanyintCompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_tblLevel2_tblLevel1intLevel1Id",
                table: "tblLevel2",
                column: "tblLevel1intLevel1Id");

            migrationBuilder.CreateIndex(
                name: "IX_tblLevel3_tblLevel2intLevel2Id",
                table: "tblLevel3",
                column: "tblLevel2intLevel2Id");

            migrationBuilder.CreateIndex(
                name: "IX_tblParties_tblCompanyintCompanyId",
                table: "tblParties",
                column: "tblCompanyintCompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_tblParties_tblLevel1intLevel1Id",
                table: "tblParties",
                column: "tblLevel1intLevel1Id");

            migrationBuilder.CreateIndex(
                name: "IX_tblParties_tblLevel2intLevel2Id",
                table: "tblParties",
                column: "tblLevel2intLevel2Id");

            migrationBuilder.CreateIndex(
                name: "IX_tblParties_tblLevel3intLevel3Id",
                table: "tblParties",
                column: "tblLevel3intLevel3Id");

            migrationBuilder.CreateIndex(
                name: "IX_tblPermissions_tblComponentintComponentId",
                table: "tblPermissions",
                column: "tblComponentintComponentId");

            migrationBuilder.CreateIndex(
                name: "IX_tblPermissions_tblFieldintFieldId",
                table: "tblPermissions",
                column: "tblFieldintFieldId");

            migrationBuilder.CreateIndex(
                name: "IX_tblPermissions_tblModuleintModuleId",
                table: "tblPermissions",
                column: "tblModuleintModuleId");

            migrationBuilder.CreateIndex(
                name: "IX_tblPermissions_tblRoleintRoleId",
                table: "tblPermissions",
                column: "tblRoleintRoleId");

            migrationBuilder.CreateIndex(
                name: "IX_tblPledgers_tblCompanyintCompanyId",
                table: "tblPledgers",
                column: "tblCompanyintCompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_tblPledgers_tblPartyintPartyId",
                table: "tblPledgers",
                column: "tblPartyintPartyId");

            migrationBuilder.CreateIndex(
                name: "IX_tblRoles_tblCompanyintCompanyId",
                table: "tblRoles",
                column: "tblCompanyintCompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_tblRoletblUser_tblUsersintUserId",
                table: "tblRoletblUser",
                column: "tblUsersintUserId");

            migrationBuilder.CreateIndex(
                name: "IX_tblStockDetails_tblItemintItemId",
                table: "tblStockDetails",
                column: "tblItemintItemId");

            migrationBuilder.CreateIndex(
                name: "IX_tblStockDetails_tblStockMainintStid",
                table: "tblStockDetails",
                column: "tblStockMainintStid");

            migrationBuilder.CreateIndex(
                name: "IX_tblStockDetails_tblWarehouseintWarehouseId",
                table: "tblStockDetails",
                column: "tblWarehouseintWarehouseId");

            migrationBuilder.CreateIndex(
                name: "IX_tblStockMains_tblCompanyintCompanyId",
                table: "tblStockMains",
                column: "tblCompanyintCompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_tblStockMains_tblPartyintPartyId",
                table: "tblStockMains",
                column: "tblPartyintPartyId");

            migrationBuilder.CreateIndex(
                name: "IX_tblStockMains_tblTransporterintTransporterId",
                table: "tblStockMains",
                column: "tblTransporterintTransporterId");

            migrationBuilder.CreateIndex(
                name: "IX_tblTransporters_tblCompanyintCompanyId",
                table: "tblTransporters",
                column: "tblCompanyintCompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_tblUsers_tblCompanyintCompanyId",
                table: "tblUsers",
                column: "tblCompanyintCompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_tblWarehouses_tblCompanyintCompanyId",
                table: "tblWarehouses",
                column: "tblCompanyintCompanyId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "tblPermissions");

            migrationBuilder.DropTable(
                name: "tblPledgers");

            migrationBuilder.DropTable(
                name: "tblRoletblUser");

            migrationBuilder.DropTable(
                name: "tblStockDetails");

            migrationBuilder.DropTable(
                name: "tblFields");

            migrationBuilder.DropTable(
                name: "tblRoles");

            migrationBuilder.DropTable(
                name: "tblUsers");

            migrationBuilder.DropTable(
                name: "tblItems");

            migrationBuilder.DropTable(
                name: "tblStockMains");

            migrationBuilder.DropTable(
                name: "tblWarehouses");

            migrationBuilder.DropTable(
                name: "tblComponents");

            migrationBuilder.DropTable(
                name: "tblParties");

            migrationBuilder.DropTable(
                name: "tblTransporters");

            migrationBuilder.DropTable(
                name: "tblModules");

            migrationBuilder.DropTable(
                name: "tblLevel3");

            migrationBuilder.DropTable(
                name: "tblCompanies");

            migrationBuilder.DropTable(
                name: "tblLevel2");

            migrationBuilder.DropTable(
                name: "tblLevel1");
        }
    }
}
