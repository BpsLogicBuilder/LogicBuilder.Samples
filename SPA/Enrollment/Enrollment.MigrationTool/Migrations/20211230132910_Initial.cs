using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Enrollment.MigrationTool.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "LookUps",
                columns: table => new
                {
                    LookUpsID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Text = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ListName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Value = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NumericValue = table.Column<double>(type: "float", nullable: true),
                    BooleanValue = table.Column<bool>(type: "bit", nullable: true),
                    DateTimeValue = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CharValue = table.Column<string>(type: "nvarchar(1)", nullable: true),
                    GuidValue = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    TimeSpanValue = table.Column<TimeSpan>(type: "time", nullable: true),
                    Order = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LookUps", x => x.LookUpsID);
                });

            migrationBuilder.CreateTable(
                name: "User",
                columns: table => new
                {
                    UserId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserName = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_User", x => x.UserId);
                });

            migrationBuilder.CreateTable(
                name: "Academic",
                columns: table => new
                {
                    UserId = table.Column<int>(type: "int", nullable: false),
                    LastHighSchoolLocation = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false),
                    NcHighSchoolName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    HomeSchoolType = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    HomeSchoolAssociation = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    FromDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ToDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    GraduationStatus = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false),
                    ReceivedGed = table.Column<bool>(type: "bit", nullable: true),
                    GedLocation = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    GedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    EarnedCreditAtCmc = table.Column<bool>(type: "bit", nullable: false),
                    AttendedPriorColleges = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Academic", x => x.UserId);
                    table.ForeignKey(
                        name: "FK_Academic_User_UserId",
                        column: x => x.UserId,
                        principalTable: "User",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Admissions",
                columns: table => new
                {
                    UserId = table.Column<int>(type: "int", nullable: false),
                    EnteringStatus = table.Column<string>(type: "nvarchar(1)", maxLength: 1, nullable: false),
                    EnrollmentTerm = table.Column<string>(type: "nvarchar(6)", maxLength: 6, nullable: false),
                    EnrollmentYear = table.Column<string>(type: "nvarchar(4)", maxLength: 4, nullable: false),
                    ProgramType = table.Column<string>(type: "nvarchar(55)", maxLength: 55, nullable: false),
                    Program = table.Column<string>(type: "nvarchar(55)", maxLength: 55, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Admissions", x => x.UserId);
                    table.ForeignKey(
                        name: "FK_Admissions_User_UserId",
                        column: x => x.UserId,
                        principalTable: "User",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Certification",
                columns: table => new
                {
                    UserId = table.Column<int>(type: "int", nullable: false),
                    CertificateStatementChecked = table.Column<bool>(type: "bit", nullable: false),
                    DeclarationStatementChecked = table.Column<bool>(type: "bit", nullable: false),
                    PolicyStatementsChecked = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Certification", x => x.UserId);
                    table.ForeignKey(
                        name: "FK_Certification_User_UserId",
                        column: x => x.UserId,
                        principalTable: "User",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ContactInfo",
                columns: table => new
                {
                    UserId = table.Column<int>(type: "int", nullable: false),
                    HasFormerName = table.Column<bool>(type: "bit", nullable: false),
                    FormerFirstName = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: true),
                    FormerMiddleName = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: true),
                    FormerLastName = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: true),
                    DateOfBirth = table.Column<DateTime>(type: "datetime2", nullable: false),
                    SocialSecurityNumber = table.Column<string>(type: "nvarchar(11)", maxLength: 11, nullable: false),
                    Gender = table.Column<string>(type: "nvarchar(1)", maxLength: 1, nullable: false),
                    Race = table.Column<string>(type: "nvarchar(3)", maxLength: 3, nullable: false),
                    Ethnicity = table.Column<string>(type: "nvarchar(3)", maxLength: 3, nullable: false),
                    EnergencyContactFirstName = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    EnergencyContactLastName = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    EnergencyContactRelationship = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    EnergencyContactPhoneNumber = table.Column<string>(type: "nvarchar(12)", maxLength: 12, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ContactInfo", x => x.UserId);
                    table.ForeignKey(
                        name: "FK_ContactInfo_User_UserId",
                        column: x => x.UserId,
                        principalTable: "User",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "MoreInfo",
                columns: table => new
                {
                    UserId = table.Column<int>(type: "int", nullable: false),
                    ReasonForAttending = table.Column<string>(type: "nvarchar(4)", maxLength: 4, nullable: false),
                    OverallEducationalGoal = table.Column<string>(type: "nvarchar(4)", maxLength: 4, nullable: false),
                    IsVeteran = table.Column<bool>(type: "bit", nullable: false),
                    MilitaryStatus = table.Column<string>(type: "nvarchar(4)", maxLength: 4, nullable: true),
                    MilitaryBranch = table.Column<string>(type: "nvarchar(4)", maxLength: 4, nullable: true),
                    VeteranType = table.Column<string>(type: "nvarchar(4)", maxLength: 4, nullable: true),
                    GovernmentBenefits = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MoreInfo", x => x.UserId);
                    table.ForeignKey(
                        name: "FK_MoreInfo_User_UserId",
                        column: x => x.UserId,
                        principalTable: "User",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Personal",
                columns: table => new
                {
                    UserId = table.Column<int>(type: "int", nullable: false),
                    FirstName = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    MiddleName = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    LastName = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    Suffix = table.Column<string>(type: "nvarchar(6)", maxLength: 6, nullable: true),
                    Address1 = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    Address2 = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: true),
                    City = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    State = table.Column<string>(type: "nvarchar(25)", maxLength: 25, nullable: false),
                    County = table.Column<string>(type: "nvarchar(25)", maxLength: 25, nullable: true),
                    ZipCode = table.Column<string>(type: "nvarchar(5)", maxLength: 5, nullable: false),
                    CellPhone = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: true),
                    OtherPhone = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: true),
                    PrimaryEmail = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Personal", x => x.UserId);
                    table.ForeignKey(
                        name: "FK_Personal_User_UserId",
                        column: x => x.UserId,
                        principalTable: "User",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Residency",
                columns: table => new
                {
                    UserId = table.Column<int>(type: "int", nullable: false),
                    CitizenshipStatus = table.Column<string>(type: "nvarchar(6)", maxLength: 6, nullable: false),
                    ImmigrationStatus = table.Column<string>(type: "nvarchar(6)", maxLength: 6, nullable: true),
                    CountryOfCitizenship = table.Column<string>(type: "nvarchar(55)", maxLength: 55, nullable: true),
                    ResidentState = table.Column<string>(type: "nvarchar(55)", maxLength: 55, nullable: false),
                    HasValidDriversLicense = table.Column<bool>(type: "bit", nullable: false),
                    DriversLicenseState = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: true),
                    DriversLicenseNumber = table.Column<string>(type: "nvarchar(25)", maxLength: 25, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Residency", x => x.UserId);
                    table.ForeignKey(
                        name: "FK_Residency_User_UserId",
                        column: x => x.UserId,
                        principalTable: "User",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Institutions",
                columns: table => new
                {
                    InstitutionId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    InstitutionState = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false),
                    InstitutionName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    StartYear = table.Column<string>(type: "nvarchar(4)", maxLength: 4, nullable: false),
                    EndYear = table.Column<string>(type: "nvarchar(4)", maxLength: 4, nullable: false),
                    HighestDegreeEarned = table.Column<string>(type: "nvarchar(4)", maxLength: 4, nullable: false),
                    MonthYearGraduated = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UserId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Institutions", x => x.InstitutionId);
                    table.ForeignKey(
                        name: "FK_Institutions_Academic_UserId",
                        column: x => x.UserId,
                        principalTable: "Academic",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "StatesLivedIn",
                columns: table => new
                {
                    StateLivedInId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    State = table.Column<string>(type: "nvarchar(25)", maxLength: 25, nullable: false),
                    UserId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StatesLivedIn", x => x.StateLivedInId);
                    table.ForeignKey(
                        name: "FK_StatesLivedIn_Residency_UserId",
                        column: x => x.UserId,
                        principalTable: "Residency",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Institutions_UserId",
                table: "Institutions",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_StatesLivedIn_UserId",
                table: "StatesLivedIn",
                column: "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Admissions");

            migrationBuilder.DropTable(
                name: "Certification");

            migrationBuilder.DropTable(
                name: "ContactInfo");

            migrationBuilder.DropTable(
                name: "Institutions");

            migrationBuilder.DropTable(
                name: "LookUps");

            migrationBuilder.DropTable(
                name: "MoreInfo");

            migrationBuilder.DropTable(
                name: "Personal");

            migrationBuilder.DropTable(
                name: "StatesLivedIn");

            migrationBuilder.DropTable(
                name: "Academic");

            migrationBuilder.DropTable(
                name: "Residency");

            migrationBuilder.DropTable(
                name: "User");
        }
    }
}
