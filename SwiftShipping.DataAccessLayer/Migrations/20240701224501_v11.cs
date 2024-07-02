using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace SwiftShipping.DataAccessLayer.Migrations
{
    /// <inheritdoc />
    public partial class v11 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "RolePermissions",
                keyColumns: new[] { "DepartmentId", "RoleName" },
                keyValues: new object[] { 1, "DliveryMan" });

            migrationBuilder.DeleteData(
                table: "RolePermissions",
                keyColumns: new[] { "DepartmentId", "RoleName" },
                keyValues: new object[] { 2, "DliveryMan" });

            migrationBuilder.DeleteData(
                table: "RolePermissions",
                keyColumns: new[] { "DepartmentId", "RoleName" },
                keyValues: new object[] { 3, "DliveryMan" });

            migrationBuilder.DeleteData(
                table: "RolePermissions",
                keyColumns: new[] { "DepartmentId", "RoleName" },
                keyValues: new object[] { 4, "DliveryMan" });

            migrationBuilder.DeleteData(
                table: "RolePermissions",
                keyColumns: new[] { "DepartmentId", "RoleName" },
                keyValues: new object[] { 5, "DliveryMan" });

            migrationBuilder.DeleteData(
                table: "RolePermissions",
                keyColumns: new[] { "DepartmentId", "RoleName" },
                keyValues: new object[] { 6, "DliveryMan" });

            migrationBuilder.DeleteData(
                table: "RolePermissions",
                keyColumns: new[] { "DepartmentId", "RoleName" },
                keyValues: new object[] { 7, "DliveryMan" });

            migrationBuilder.DeleteData(
                table: "RolePermissions",
                keyColumns: new[] { "DepartmentId", "RoleName" },
                keyValues: new object[] { 8, "DliveryMan" });

            migrationBuilder.DeleteData(
                table: "RolePermissions",
                keyColumns: new[] { "DepartmentId", "RoleName" },
                keyValues: new object[] { 9, "DliveryMan" });

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "DeliveryMans",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "AspNetUsers",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.InsertData(
                table: "RolePermissions",
                columns: new[] { "DepartmentId", "RoleName", "Add", "Delete", "Edit", "View" },
                values: new object[,]
                {
                    { 1, "DeliveryMan", false, false, false, false },
                    { 2, "DeliveryMan", false, false, false, false },
                    { 3, "DeliveryMan", false, false, false, false },
                    { 4, "DeliveryMan", false, false, false, false },
                    { 5, "DeliveryMan", false, false, false, false },
                    { 6, "DeliveryMan", false, false, false, false },
                    { 7, "DeliveryMan", false, false, false, false },
                    { 8, "DeliveryMan", false, false, false, false },
                    { 9, "DeliveryMan", false, false, false, false }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "RolePermissions",
                keyColumns: new[] { "DepartmentId", "RoleName" },
                keyValues: new object[] { 1, "DeliveryMan" });

            migrationBuilder.DeleteData(
                table: "RolePermissions",
                keyColumns: new[] { "DepartmentId", "RoleName" },
                keyValues: new object[] { 2, "DeliveryMan" });

            migrationBuilder.DeleteData(
                table: "RolePermissions",
                keyColumns: new[] { "DepartmentId", "RoleName" },
                keyValues: new object[] { 3, "DeliveryMan" });

            migrationBuilder.DeleteData(
                table: "RolePermissions",
                keyColumns: new[] { "DepartmentId", "RoleName" },
                keyValues: new object[] { 4, "DeliveryMan" });

            migrationBuilder.DeleteData(
                table: "RolePermissions",
                keyColumns: new[] { "DepartmentId", "RoleName" },
                keyValues: new object[] { 5, "DeliveryMan" });

            migrationBuilder.DeleteData(
                table: "RolePermissions",
                keyColumns: new[] { "DepartmentId", "RoleName" },
                keyValues: new object[] { 6, "DeliveryMan" });

            migrationBuilder.DeleteData(
                table: "RolePermissions",
                keyColumns: new[] { "DepartmentId", "RoleName" },
                keyValues: new object[] { 7, "DeliveryMan" });

            migrationBuilder.DeleteData(
                table: "RolePermissions",
                keyColumns: new[] { "DepartmentId", "RoleName" },
                keyValues: new object[] { 8, "DeliveryMan" });

            migrationBuilder.DeleteData(
                table: "RolePermissions",
                keyColumns: new[] { "DepartmentId", "RoleName" },
                keyValues: new object[] { 9, "DeliveryMan" });

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "DeliveryMans");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "AspNetUsers");

            migrationBuilder.InsertData(
                table: "RolePermissions",
                columns: new[] { "DepartmentId", "RoleName", "Add", "Delete", "Edit", "View" },
                values: new object[,]
                {
                    { 1, "DliveryMan", false, false, false, false },
                    { 2, "DliveryMan", false, false, false, false },
                    { 3, "DliveryMan", false, false, false, false },
                    { 4, "DliveryMan", false, false, false, false },
                    { 5, "DliveryMan", false, false, false, false },
                    { 6, "DliveryMan", false, false, false, false },
                    { 7, "DliveryMan", false, false, false, false },
                    { 8, "DliveryMan", false, false, false, false },
                    { 9, "DliveryMan", false, false, false, false }
                });
        }
    }
}
