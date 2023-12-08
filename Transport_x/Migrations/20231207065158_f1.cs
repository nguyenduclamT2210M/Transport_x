using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Transport_x.Migrations
{
    /// <inheritdoc />
    public partial class f1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Branches",
                columns: table => new
                {
                    IdBranches = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NameBranches = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AddressBranches = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TelephoneBranches = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Branches", x => x.IdBranches);
                });

            migrationBuilder.CreateTable(
                name: "ShippingTypes",
                columns: table => new
                {
                    IdShipType = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NameShip = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ChageRate = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ShippingTypes", x => x.IdShipType);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    IdUser = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FullNameUser = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    EmailUser = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TelephoneUser = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PassWordUser = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AddressUser = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.IdUser);
                });

            migrationBuilder.CreateTable(
                name: "Employees",
                columns: table => new
                {
                    IdEmployee = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IdBranches = table.Column<int>(type: "int", nullable: false),
                    FullName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    role = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Employees", x => x.IdEmployee);
                    table.ForeignKey(
                        name: "FK_Employees_Branches_IdBranches",
                        column: x => x.IdBranches,
                        principalTable: "Branches",
                        principalColumn: "IdBranches",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Bills",
                columns: table => new
                {
                    IdBill = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdUser = table.Column<int>(type: "int", nullable: false),
                    IdShippingType = table.Column<int>(type: "int", nullable: false),
                    ConsigneeName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ConsigneeAddress = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ConsigneeTel = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Change = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    PickUp = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Cod = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Payer = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Bills", x => x.IdBill);
                    table.ForeignKey(
                        name: "FK_Bills_ShippingTypes_IdShippingType",
                        column: x => x.IdShippingType,
                        principalTable: "ShippingTypes",
                        principalColumn: "IdShipType",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Bills_Users_IdUser",
                        column: x => x.IdUser,
                        principalTable: "Users",
                        principalColumn: "IdUser",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Goods",
                columns: table => new
                {
                    IdGoods = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IdBill = table.Column<int>(type: "int", nullable: false),
                    Quantity = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Weight = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Valuse = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Nature = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Goods", x => x.IdGoods);
                    table.ForeignKey(
                        name: "FK_Goods_Bills_IdBill",
                        column: x => x.IdBill,
                        principalTable: "Bills",
                        principalColumn: "IdBill",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Statuses",
                columns: table => new
                {
                    IdStatus = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TypeStatus = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IdEmployee = table.Column<int>(type: "int", nullable: false),
                    StatusTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IdBill = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Statuses", x => x.IdStatus);
                    table.ForeignKey(
                        name: "FK_Statuses_Bills_IdBill",
                        column: x => x.IdBill,
                        principalTable: "Bills",
                        principalColumn: "IdBill",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Statuses_Employees_IdEmployee",
                        column: x => x.IdEmployee,
                        principalTable: "Employees",
                        principalColumn: "IdEmployee",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Bills_IdShippingType",
                table: "Bills",
                column: "IdShippingType");

            migrationBuilder.CreateIndex(
                name: "IX_Bills_IdUser",
                table: "Bills",
                column: "IdUser");

            migrationBuilder.CreateIndex(
                name: "IX_Employees_IdBranches",
                table: "Employees",
                column: "IdBranches");

            migrationBuilder.CreateIndex(
                name: "IX_Goods_IdBill",
                table: "Goods",
                column: "IdBill");

            migrationBuilder.CreateIndex(
                name: "IX_Statuses_IdBill",
                table: "Statuses",
                column: "IdBill");

            migrationBuilder.CreateIndex(
                name: "IX_Statuses_IdEmployee",
                table: "Statuses",
                column: "IdEmployee");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Goods");

            migrationBuilder.DropTable(
                name: "Statuses");

            migrationBuilder.DropTable(
                name: "Bills");

            migrationBuilder.DropTable(
                name: "Employees");

            migrationBuilder.DropTable(
                name: "ShippingTypes");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "Branches");
        }
    }
}
