using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace SwiftShipping.DataAccessLayer.Migrations
{
    /// <inheritdoc />
    public partial class v10 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "RolePermissions",
                columns: new[] { "DepartmentId", "RoleName", "Add", "Delete", "Edit", "View" },
                values: new object[,]
                {
                    { 1, "Admin", false, false, false, false },
                    { 2, "Admin", false, false, false, false },
                    { 3, "Admin", false, false, false, false },
                    { 4, "Admin", false, false, false, false },
                    { 5, "Admin", false, false, false, false },
                    { 6, "Admin", false, false, false, false },
                    { 7, "Admin", false, false, false, false },
                    { 8, "Admin", false, false, false, false },
                    { 9, "Admin", false, false, false, false },
                    { 1, "DliveryMan", false, false, false, false },
                    { 2, "DliveryMan", false, false, false, false },
                    { 3, "DliveryMan", false, false, false, false },
                    { 4, "DliveryMan", false, false, false, false },
                    { 5, "DliveryMan", false, false, false, false },
                    { 6, "DliveryMan", false, false, false, false },
                    { 7, "DliveryMan", false, false, false, false },
                    { 8, "DliveryMan", false, false, false, false },
                    { 9, "DliveryMan", false, false, false, false },
                    { 1, "Employee", false, false, false, false },
                    { 2, "Employee", false, false, false, false },
                    { 3, "Employee", false, false, false, false },
                    { 4, "Employee", false, false, false, false },
                    { 5, "Employee", false, false, false, false },
                    { 6, "Employee", false, false, false, false },
                    { 7, "Employee", false, false, false, false },
                    { 8, "Employee", false, false, false, false },
                    { 9, "Employee", false, false, false, false },
                    { 1, "Seller", false, false, false, false },
                    { 2, "Seller", false, false, false, false },
                    { 3, "Seller", false, false, false, false },
                    { 4, "Seller", false, false, false, false },
                    { 5, "Seller", false, false, false, false },
                    { 6, "Seller", false, false, false, false },
                    { 7, "Seller", false, false, false, false },
                    { 8, "Seller", false, false, false, false },
                    { 9, "Seller", false, false, false, false }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "RolePermissions",
                keyColumns: new[] { "DepartmentId", "RoleName" },
                keyValues: new object[] { 1, "Admin" });

            migrationBuilder.DeleteData(
                table: "RolePermissions",
                keyColumns: new[] { "DepartmentId", "RoleName" },
                keyValues: new object[] { 2, "Admin" });

            migrationBuilder.DeleteData(
                table: "RolePermissions",
                keyColumns: new[] { "DepartmentId", "RoleName" },
                keyValues: new object[] { 3, "Admin" });

            migrationBuilder.DeleteData(
                table: "RolePermissions",
                keyColumns: new[] { "DepartmentId", "RoleName" },
                keyValues: new object[] { 4, "Admin" });

            migrationBuilder.DeleteData(
                table: "RolePermissions",
                keyColumns: new[] { "DepartmentId", "RoleName" },
                keyValues: new object[] { 5, "Admin" });

            migrationBuilder.DeleteData(
                table: "RolePermissions",
                keyColumns: new[] { "DepartmentId", "RoleName" },
                keyValues: new object[] { 6, "Admin" });

            migrationBuilder.DeleteData(
                table: "RolePermissions",
                keyColumns: new[] { "DepartmentId", "RoleName" },
                keyValues: new object[] { 7, "Admin" });

            migrationBuilder.DeleteData(
                table: "RolePermissions",
                keyColumns: new[] { "DepartmentId", "RoleName" },
                keyValues: new object[] { 8, "Admin" });

            migrationBuilder.DeleteData(
                table: "RolePermissions",
                keyColumns: new[] { "DepartmentId", "RoleName" },
                keyValues: new object[] { 9, "Admin" });

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

            migrationBuilder.DeleteData(
                table: "RolePermissions",
                keyColumns: new[] { "DepartmentId", "RoleName" },
                keyValues: new object[] { 1, "Employee" });

            migrationBuilder.DeleteData(
                table: "RolePermissions",
                keyColumns: new[] { "DepartmentId", "RoleName" },
                keyValues: new object[] { 2, "Employee" });

            migrationBuilder.DeleteData(
                table: "RolePermissions",
                keyColumns: new[] { "DepartmentId", "RoleName" },
                keyValues: new object[] { 3, "Employee" });

            migrationBuilder.DeleteData(
                table: "RolePermissions",
                keyColumns: new[] { "DepartmentId", "RoleName" },
                keyValues: new object[] { 4, "Employee" });

            migrationBuilder.DeleteData(
                table: "RolePermissions",
                keyColumns: new[] { "DepartmentId", "RoleName" },
                keyValues: new object[] { 5, "Employee" });

            migrationBuilder.DeleteData(
                table: "RolePermissions",
                keyColumns: new[] { "DepartmentId", "RoleName" },
                keyValues: new object[] { 6, "Employee" });

            migrationBuilder.DeleteData(
                table: "RolePermissions",
                keyColumns: new[] { "DepartmentId", "RoleName" },
                keyValues: new object[] { 7, "Employee" });

            migrationBuilder.DeleteData(
                table: "RolePermissions",
                keyColumns: new[] { "DepartmentId", "RoleName" },
                keyValues: new object[] { 8, "Employee" });

            migrationBuilder.DeleteData(
                table: "RolePermissions",
                keyColumns: new[] { "DepartmentId", "RoleName" },
                keyValues: new object[] { 9, "Employee" });

            migrationBuilder.DeleteData(
                table: "RolePermissions",
                keyColumns: new[] { "DepartmentId", "RoleName" },
                keyValues: new object[] { 1, "Seller" });

            migrationBuilder.DeleteData(
                table: "RolePermissions",
                keyColumns: new[] { "DepartmentId", "RoleName" },
                keyValues: new object[] { 2, "Seller" });

            migrationBuilder.DeleteData(
                table: "RolePermissions",
                keyColumns: new[] { "DepartmentId", "RoleName" },
                keyValues: new object[] { 3, "Seller" });

            migrationBuilder.DeleteData(
                table: "RolePermissions",
                keyColumns: new[] { "DepartmentId", "RoleName" },
                keyValues: new object[] { 4, "Seller" });

            migrationBuilder.DeleteData(
                table: "RolePermissions",
                keyColumns: new[] { "DepartmentId", "RoleName" },
                keyValues: new object[] { 5, "Seller" });

            migrationBuilder.DeleteData(
                table: "RolePermissions",
                keyColumns: new[] { "DepartmentId", "RoleName" },
                keyValues: new object[] { 6, "Seller" });

            migrationBuilder.DeleteData(
                table: "RolePermissions",
                keyColumns: new[] { "DepartmentId", "RoleName" },
                keyValues: new object[] { 7, "Seller" });

            migrationBuilder.DeleteData(
                table: "RolePermissions",
                keyColumns: new[] { "DepartmentId", "RoleName" },
                keyValues: new object[] { 8, "Seller" });

            migrationBuilder.DeleteData(
                table: "RolePermissions",
                keyColumns: new[] { "DepartmentId", "RoleName" },
                keyValues: new object[] { 9, "Seller" });
        }
    }
}
