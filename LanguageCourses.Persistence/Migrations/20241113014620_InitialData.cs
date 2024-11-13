using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace LanguageCourses.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class InitialData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "JobTitles",
                columns: table => new
                {
                    JobTitleID = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "(newid())"),
                    Name = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: false),
                    Salary = table.Column<decimal>(type: "decimal(10,2)", nullable: false),
                    Responsibilities = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: true),
                    Requirements = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__JobTitle__35382FC9A56AE929", x => x.JobTitleID);
                });

            migrationBuilder.CreateTable(
                name: "Students",
                columns: table => new
                {
                    StudentID = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "(newid())"),
                    Surname = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: false),
                    Name = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: false),
                    MidName = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: true),
                    BirthDate = table.Column<DateOnly>(type: "date", nullable: false),
                    Address = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: false),
                    Phone = table.Column<string>(type: "varchar(20)", unicode: false, maxLength: 20, nullable: false),
                    PassportNumber = table.Column<string>(type: "varchar(9)", unicode: false, maxLength: 9, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Students__32C52A7968AC35E8", x => x.StudentID);
                });

            migrationBuilder.CreateTable(
                name: "Employees",
                columns: table => new
                {
                    EmployeeID = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "(newid())"),
                    JobTitleID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Surname = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: false),
                    Name = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: false),
                    Midname = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: true),
                    BirthDate = table.Column<DateOnly>(type: "date", nullable: false),
                    Address = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: true),
                    Phone = table.Column<string>(type: "varchar(20)", unicode: false, maxLength: 20, nullable: true),
                    PassportNumber = table.Column<string>(type: "varchar(9)", unicode: false, maxLength: 9, nullable: false),
                    Education = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Employee__7AD04FF14A0267E9", x => x.EmployeeID);
                    table.ForeignKey(
                        name: "FK__Employees__JobTi__3B4BBA2E",
                        column: x => x.JobTitleID,
                        principalTable: "JobTitles",
                        principalColumn: "JobTitleID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Payments",
                columns: table => new
                {
                    PaymentID = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "(newid())"),
                    Date = table.Column<DateOnly>(type: "date", nullable: false),
                    Purpose = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: false),
                    Amount = table.Column<decimal>(type: "decimal(10,2)", nullable: false),
                    StudentID = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Payments__9B556A5874FA56DF", x => x.PaymentID);
                    table.ForeignKey(
                        name: "FK__Payments__Studen__4F52B2DB",
                        column: x => x.StudentID,
                        principalTable: "Students",
                        principalColumn: "StudentID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Courses",
                columns: table => new
                {
                    CourseID = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "(newid())"),
                    EmployeeID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: false),
                    TrainingProgram = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: false),
                    Description = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: true),
                    Intensity = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: false),
                    GroupSize = table.Column<int>(type: "int", nullable: false),
                    AvailableSeats = table.Column<int>(type: "int", nullable: false),
                    Hours = table.Column<int>(type: "int", nullable: false),
                    TuitionFee = table.Column<decimal>(type: "decimal(10,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Courses__C92D71876B88FF34", x => x.CourseID);
                    table.ForeignKey(
                        name: "FK__Courses__Employe__3F1C4B12",
                        column: x => x.EmployeeID,
                        principalTable: "Employees",
                        principalColumn: "EmployeeID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CourseStudents",
                columns: table => new
                {
                    CourseID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    StudentID = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__CourseSt__4A0123205C2964B0", x => new { x.CourseID, x.StudentID });
                    table.ForeignKey(
                        name: "FK__CourseStu__Cours__4A8DFDBE",
                        column: x => x.CourseID,
                        principalTable: "Courses",
                        principalColumn: "CourseID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK__CourseStu__Stude__4B8221F7",
                        column: x => x.StudentID,
                        principalTable: "Students",
                        principalColumn: "StudentID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "JobTitles",
                columns: new[] { "JobTitleID", "Name", "Requirements", "Responsibilities", "Salary" },
                values: new object[,]
                {
                    { new Guid("66666666-6666-6666-6666-666666666666"), "Преподаватель", "Высшее образование.", "Преподавание курсов.", 60000m },
                    { new Guid("88888888-8888-8888-8888-888888888888"), "Администратор", "Опыт работы в образовании.", "Организация учебного процесса.", 40000m }
                });

            migrationBuilder.InsertData(
                table: "Students",
                columns: new[] { "StudentID", "Address", "BirthDate", "MidName", "Name", "PassportNumber", "Phone", "Surname" },
                values: new object[,]
                {
                    { new Guid("99999999-9999-9999-9999-999999999993"), "Москва, ул. 3", new DateOnly(2000, 5, 15), "Иванович", "Алексей", "HB6543122", "+79011112233", "Смирнов" },
                    { new Guid("99999999-9999-9999-9999-999999999995"), "Санкт-Петербург, ул. 4", new DateOnly(1999, 7, 20), null, "Сергей", "HB6543121", "+79044445556", "Попов" }
                });

            migrationBuilder.InsertData(
                table: "Employees",
                columns: new[] { "EmployeeID", "Address", "BirthDate", "Education", "JobTitleID", "Midname", "Name", "PassportNumber", "Phone", "Surname" },
                values: new object[,]
                {
                    { new Guid("22222222-2222-2222-2222-222222222222"), "Москва, ул. 1", new DateOnly(1990, 1, 1), "Высшее", new Guid("66666666-6666-6666-6666-666666666666"), "Иванович", "Иван", "HB6543129", "+79012345678", "Иванов" },
                    { new Guid("44444444-4444-4444-4444-444444444444"), "Санкт-Петербург, ул. 2", new DateOnly(1985, 2, 2), "Высшее", new Guid("88888888-8888-8888-8888-888888888888"), "Петрович", "Петр", "HB6543111", "+79087654321", "Петров" }
                });

            migrationBuilder.InsertData(
                table: "Payments",
                columns: new[] { "PaymentID", "Amount", "Date", "Purpose", "StudentID" },
                values: new object[,]
                {
                    { new Guid("99999999-9999-9999-9999-999999999992"), 15000m, new DateOnly(2024, 11, 13), "Оплата курса Основы программирования", new Guid("99999999-9999-9999-9999-999999999993") },
                    { new Guid("99999999-9999-9999-9999-999999999994"), 20000m, new DateOnly(2024, 10, 13), "Оплата курса Разработка веб-приложений", new Guid("99999999-9999-9999-9999-999999999995") }
                });

            migrationBuilder.InsertData(
                table: "Courses",
                columns: new[] { "CourseID", "AvailableSeats", "Description", "EmployeeID", "GroupSize", "Hours", "Intensity", "Name", "TrainingProgram", "TuitionFee" },
                values: new object[,]
                {
                    { new Guid("11111111-1111-1111-1111-111111111111"), 10, "Курс для начинающих программистов", new Guid("22222222-2222-2222-2222-222222222222"), 15, 40, "Средняя", "Основы программирования", "Изучение основ языков программирования", 15000m },
                    { new Guid("33333333-3333-3333-3333-333333333333"), 8, "Курс для веб-разработчиков", new Guid("44444444-4444-4444-4444-444444444444"), 12, 60, "Высокая", "Разработка веб-приложений", "Создание интерактивных веб-приложений с использованием JavaScript и других технологий", 20000m }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Courses_EmployeeID",
                table: "Courses",
                column: "EmployeeID");

            migrationBuilder.CreateIndex(
                name: "IX_CourseStudents_StudentID",
                table: "CourseStudents",
                column: "StudentID");

            migrationBuilder.CreateIndex(
                name: "IX_Employees_JobTitleID",
                table: "Employees",
                column: "JobTitleID");

            migrationBuilder.CreateIndex(
                name: "UQ__Employee__45809E71543558D3",
                table: "Employees",
                column: "PassportNumber",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "UQ__Employee__5C7E359EB6DB6BF9",
                table: "Employees",
                column: "Phone",
                unique: true,
                filter: "[Phone] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Payments_StudentID",
                table: "Payments",
                column: "StudentID");

            migrationBuilder.CreateIndex(
                name: "UQ__Students__45809E717E8906CA",
                table: "Students",
                column: "PassportNumber",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "UQ__Students__5C7E359EED8438DE",
                table: "Students",
                column: "Phone",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CourseStudents");

            migrationBuilder.DropTable(
                name: "Payments");

            migrationBuilder.DropTable(
                name: "Courses");

            migrationBuilder.DropTable(
                name: "Students");

            migrationBuilder.DropTable(
                name: "Employees");

            migrationBuilder.DropTable(
                name: "JobTitles");
        }
    }
}
