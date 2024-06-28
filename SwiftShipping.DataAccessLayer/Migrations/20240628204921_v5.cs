using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SwiftShipping.DataAccessLayer.Migrations
{
    /// <inheritdoc />
    public partial class v5 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_WeightSettings",
                table: "WeightSettings");

            migrationBuilder.DeleteData(
                table: "WeightSettings",
                keyColumn: "DefaultWeight",
                keyValue: 5f);

            migrationBuilder.RenameColumn(
                name: "RegionId",
                table: "Employees",
                newName: "BranchId");

            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "WeightSettings",
                type: "int",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddPrimaryKey(
                name: "PK_WeightSettings",
                table: "WeightSettings",
                column: "Id");

            migrationBuilder.InsertData(
                table: "WeightSettings",
                columns: new[] { "Id", "DefaultWeight", "KGPrice" },
                values: new object[] { 1, 5f, 10m });

            migrationBuilder.CreateIndex(
                name: "IX_Sellers_BranchId",
                table: "Sellers",
                column: "BranchId");

            migrationBuilder.CreateIndex(
                name: "IX_Employees_BranchId",
                table: "Employees",
                column: "BranchId");

            migrationBuilder.AddForeignKey(
                name: "FK_Employees_Branches_BranchId",
                table: "Employees",
                column: "BranchId",
                principalTable: "Branches",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Sellers_Branches_BranchId",
                table: "Sellers",
                column: "BranchId",
                principalTable: "Branches",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Employees_Branches_BranchId",
                table: "Employees");

            migrationBuilder.DropForeignKey(
                name: "FK_Sellers_Branches_BranchId",
                table: "Sellers");

            migrationBuilder.DropPrimaryKey(
                name: "PK_WeightSettings",
                table: "WeightSettings");

            migrationBuilder.DropIndex(
                name: "IX_Sellers_BranchId",
                table: "Sellers");

            migrationBuilder.DropIndex(
                name: "IX_Employees_BranchId",
                table: "Employees");

            migrationBuilder.DeleteData(
                table: "WeightSettings",
                keyColumn: "Id",
                keyColumnType: "int",
                keyValue: 1);

            migrationBuilder.DropColumn(
                name: "Id",
                table: "WeightSettings");

            migrationBuilder.RenameColumn(
                name: "BranchId",
                table: "Employees",
                newName: "RegionId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_WeightSettings",
                table: "WeightSettings",
                column: "DefaultWeight");

            migrationBuilder.InsertData(
                table: "WeightSettings",
                columns: new[] { "DefaultWeight", "KGPrice" },
                values: new object[] { 5f, 10m });
        }
    }
}
