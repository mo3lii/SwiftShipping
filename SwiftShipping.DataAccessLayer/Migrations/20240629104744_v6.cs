using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SwiftShipping.DataAccessLayer.Migrations
{
    /// <inheritdoc />
    public partial class v6 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DeliveryManRegions_Branches_BranchId",
                table: "DeliveryManRegions");

            migrationBuilder.DropIndex(
                name: "IX_DeliveryManRegions_BranchId",
                table: "DeliveryManRegions");

            migrationBuilder.DropColumn(
                name: "BranchId",
                table: "DeliveryManRegions");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "BranchId",
                table: "DeliveryManRegions",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_DeliveryManRegions_BranchId",
                table: "DeliveryManRegions",
                column: "BranchId");

            migrationBuilder.AddForeignKey(
                name: "FK_DeliveryManRegions_Branches_BranchId",
                table: "DeliveryManRegions",
                column: "BranchId",
                principalTable: "Branches",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
