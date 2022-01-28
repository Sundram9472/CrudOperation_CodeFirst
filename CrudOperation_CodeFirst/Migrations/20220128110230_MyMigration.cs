using Microsoft.EntityFrameworkCore.Migrations;

namespace CrudOperation_CodeFirst.Migrations
{
    public partial class MyMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Department_sk",
                columns: table => new
                {
                    DepartmentId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DepartmentName = table.Column<string>(type: "nvarchar(80)", maxLength: 80, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Department_sk", x => x.DepartmentId);
                });

            migrationBuilder.CreateTable(
                name: "Employee_sk",
                columns: table => new
                {
                    EmployeeId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DepartmentId = table.Column<int>(type: "int", nullable: false),
                    EmployeeName = table.Column<string>(type: "nvarchar(80)", maxLength: 80, nullable: false),
                    EmployeeGmail = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    EmployeeContact = table.Column<string>(type: "nvarchar(80)", maxLength: 80, nullable: false),
                    EmployeeAdress = table.Column<string>(type: "nvarchar(80)", maxLength: 80, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Employee_sk", x => x.EmployeeId);
                    table.ForeignKey(
                        name: "FK_Employee_sk_Department_sk_DepartmentId",
                        column: x => x.DepartmentId,
                        principalTable: "Department_sk",
                        principalColumn: "DepartmentId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Employee_sk_DepartmentId",
                table: "Employee_sk",
                column: "DepartmentId",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Employee_sk");

            migrationBuilder.DropTable(
                name: "Department_sk");
        }
    }
}
