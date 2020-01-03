using Microsoft.EntityFrameworkCore.Migrations;

namespace AttendanceTracker.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "BeltGrade",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Description = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BeltGrade", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Instructor",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FirstName = table.Column<string>(nullable: true),
                    MiddleName = table.Column<string>(nullable: true),
                    LastName = table.Column<string>(nullable: true),
                    GradeId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Instructor", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Instructor_BeltGrade_GradeId",
                        column: x => x.GradeId,
                        principalTable: "BeltGrade",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "StudentClass",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Description = table.Column<string>(nullable: true),
                    InstructorId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StudentClass", x => x.Id);
                    table.ForeignKey(
                        name: "FK_StudentClass_Instructor_InstructorId",
                        column: x => x.InstructorId,
                        principalTable: "Instructor",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ClassSession",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StudentClassId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ClassSession", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ClassSession_StudentClass_StudentClassId",
                        column: x => x.StudentClassId,
                        principalTable: "StudentClass",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Student",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FirstName = table.Column<string>(nullable: true),
                    LastName = table.Column<string>(nullable: true),
                    GradeId = table.Column<int>(nullable: true),
                    StudentClassId = table.Column<int>(nullable: true),
                    ClassSessionId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Student", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Student_ClassSession_ClassSessionId",
                        column: x => x.ClassSessionId,
                        principalTable: "ClassSession",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Student_BeltGrade_GradeId",
                        column: x => x.GradeId,
                        principalTable: "BeltGrade",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Student_StudentClass_StudentClassId",
                        column: x => x.StudentClassId,
                        principalTable: "StudentClass",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.InsertData(
                table: "BeltGrade",
                columns: new[] { "Id", "Description" },
                values: new object[,]
                {
                    { 1, "10. geup" },
                    { 22, "7. dan" },
                    { 21, "6. dan" },
                    { 20, "5. dan" },
                    { 19, "4. dan" },
                    { 18, "3. dan" },
                    { 17, "2. dan" },
                    { 16, "1. dan" },
                    { 15, "1. geup" },
                    { 14, "2. geup" },
                    { 13, "3. geup" },
                    { 12, "4. geup" },
                    { 11, "4.1 geup" },
                    { 10, "5. geup" },
                    { 9, "5.1 geup" },
                    { 8, "6. geup" },
                    { 7, "6.1 geup" },
                    { 6, "6.1. geup" },
                    { 5, "7. geup" },
                    { 4, "7.1 geup" },
                    { 3, "8. geup" },
                    { 2, "8.1 geup" },
                    { 23, "8. dan" },
                    { 24, "9. dan" }
                });

            migrationBuilder.InsertData(
                table: "Instructor",
                columns: new[] { "Id", "FirstName", "GradeId", "LastName", "MiddleName" },
                values: new object[] { 1, "Jon", 15, "Karlsen", "Arild" });

            migrationBuilder.InsertData(
                table: "StudentClass",
                columns: new[] { "Id", "Description", "InstructorId" },
                values: new object[,]
                {
                    { 1, "Barn Nye", 1 },
                    { 2, "Barn Øvede", 1 },
                    { 3, "Ungdom", 1 },
                    { 4, "Voksen", 1 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_ClassSession_StudentClassId",
                table: "ClassSession",
                column: "StudentClassId");

            migrationBuilder.CreateIndex(
                name: "IX_Instructor_GradeId",
                table: "Instructor",
                column: "GradeId");

            migrationBuilder.CreateIndex(
                name: "IX_Student_ClassSessionId",
                table: "Student",
                column: "ClassSessionId");

            migrationBuilder.CreateIndex(
                name: "IX_Student_GradeId",
                table: "Student",
                column: "GradeId");

            migrationBuilder.CreateIndex(
                name: "IX_Student_StudentClassId",
                table: "Student",
                column: "StudentClassId");

            migrationBuilder.CreateIndex(
                name: "IX_StudentClass_InstructorId",
                table: "StudentClass",
                column: "InstructorId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Student");

            migrationBuilder.DropTable(
                name: "ClassSession");

            migrationBuilder.DropTable(
                name: "StudentClass");

            migrationBuilder.DropTable(
                name: "Instructor");

            migrationBuilder.DropTable(
                name: "BeltGrade");
        }
    }
}
