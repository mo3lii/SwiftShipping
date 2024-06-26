using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SwiftShipping.DataAccessLayer.Migrations
{
    /// <inheritdoc />
    public partial class v2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_WeightSettings",
                table: "WeightSettings");

            migrationBuilder.DropColumn(
                name: "Key",
                table: "WeightSettings");

            migrationBuilder.RenameColumn(
                name: "Value",
                table: "WeightSettings",
                newName: "KGPrice");

            migrationBuilder.RenameColumn(
                name: "SoreName",
                table: "Sellers",
                newName: "StoreName");

            migrationBuilder.RenameColumn(
                name: "Price",
                table: "Regions",
                newName: "PickupPrice");

            migrationBuilder.AddColumn<float>(
                name: "DefaultWeight",
                table: "WeightSettings",
                type: "real",
                nullable: false,
                defaultValue: 0f);

            migrationBuilder.AddColumn<decimal>(
                name: "NormalPrice",
                table: "Regions",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "Employees",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "DeliveryMans",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Address",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "Admins",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddPrimaryKey(
                name: "PK_WeightSettings",
                table: "WeightSettings",
                column: "DefaultWeight");

            migrationBuilder.InsertData(
                table: "WeightSettings",
                columns: new[] { "DefaultWeight", "KGPrice" },
                values: new object[] { 5f, 10m });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_WeightSettings",
                table: "WeightSettings");

            migrationBuilder.DeleteData(
                table: "WeightSettings",
                keyColumn: "DefaultWeight",
                keyColumnType: "real",
                keyValue: 5f);

            migrationBuilder.DropColumn(
                name: "DefaultWeight",
                table: "WeightSettings");

            migrationBuilder.DropColumn(
                name: "NormalPrice",
                table: "Regions");

            migrationBuilder.DropColumn(
                name: "Name",
                table: "Employees");

            migrationBuilder.DropColumn(
                name: "Name",
                table: "DeliveryMans");

            migrationBuilder.DropColumn(
                name: "Address",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "Name",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "Name",
                table: "Admins");

            migrationBuilder.RenameColumn(
                name: "KGPrice",
                table: "WeightSettings",
                newName: "Value");

            migrationBuilder.RenameColumn(
                name: "StoreName",
                table: "Sellers",
                newName: "SoreName");

            migrationBuilder.RenameColumn(
                name: "PickupPrice",
                table: "Regions",
                newName: "Price");

            migrationBuilder.AddColumn<string>(
                name: "Key",
                table: "WeightSettings",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddPrimaryKey(
                name: "PK_WeightSettings",
                table: "WeightSettings",
                column: "Key");
        }
    }
}
