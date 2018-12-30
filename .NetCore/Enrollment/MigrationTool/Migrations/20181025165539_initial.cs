using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace MigrationTool.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "Automatic");

            migrationBuilder.EnsureSchema(
                name: "Rules");

            migrationBuilder.CreateTable(
                name: "LookUps",
                columns: table => new
                {
                    LookUpsID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    ListName = table.Column<string>(maxLength: 100, nullable: false),
                    Name = table.Column<string>(nullable: false),
                    Order = table.Column<int>(nullable: false),
                    Value = table.Column<string>(maxLength: 256, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LookUps", x => x.LookUpsID);
                });

            migrationBuilder.CreateTable(
                name: "User",
                columns: table => new
                {
                    UserId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    UserName = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_User", x => x.UserId);
                });

            migrationBuilder.CreateTable(
                name: "VariableMetaData",
                schema: "Automatic",
                columns: table => new
                {
                    VariableMetaDataId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Data = table.Column<string>(nullable: true),
                    LastUpdated = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VariableMetaData", x => x.VariableMetaDataId);
                });

            migrationBuilder.CreateTable(
                name: "RulesModule",
                schema: "Rules",
                columns: table => new
                {
                    RulesModuleId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Application = table.Column<string>(nullable: false),
                    LastUpdated = table.Column<DateTime>(nullable: false),
                    LoggedInUserId = table.Column<string>(nullable: true),
                    Name = table.Column<string>(nullable: false),
                    ResourceSetFile = table.Column<byte[]>(nullable: false),
                    RuleSetFile = table.Column<byte[]>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RulesModule", x => x.RulesModuleId);
                });

            migrationBuilder.CreateTable(
                name: "Academic",
                columns: table => new
                {
                    UserId = table.Column<int>(nullable: false),
                    AttendedPriorColleges = table.Column<bool>(nullable: false),
                    EarnedCreditAtCmc = table.Column<bool>(nullable: false),
                    FromDate = table.Column<DateTime>(nullable: false),
                    GedDate = table.Column<DateTime>(nullable: true),
                    GedLocation = table.Column<string>(nullable: true),
                    GraduationStatus = table.Column<string>(maxLength: 256, nullable: false),
                    HomeSchoolAssociation = table.Column<string>(maxLength: 256, nullable: true),
                    HomeSchoolType = table.Column<string>(maxLength: 256, nullable: true),
                    LastHighSchoolLocation = table.Column<string>(maxLength: 256, nullable: false),
                    NcHighSchoolName = table.Column<string>(maxLength: 256, nullable: true),
                    ReceivedGed = table.Column<bool>(nullable: true),
                    ToDate = table.Column<DateTime>(nullable: false)
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
                    UserId = table.Column<int>(nullable: false),
                    EnrollmentTerm = table.Column<string>(maxLength: 6, nullable: false),
                    EnrollmentYear = table.Column<string>(maxLength: 4, nullable: false),
                    EnteringStatus = table.Column<string>(maxLength: 1, nullable: false),
                    Program = table.Column<string>(maxLength: 55, nullable: false),
                    ProgramType = table.Column<string>(maxLength: 55, nullable: false)
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
                    UserId = table.Column<int>(nullable: false),
                    CertificateStatementChecked = table.Column<bool>(nullable: false),
                    DeclarationStatementChecked = table.Column<bool>(nullable: false),
                    PolicyStatementsChecked = table.Column<bool>(nullable: false)
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
                    UserId = table.Column<int>(nullable: false),
                    DateOfBirth = table.Column<DateTime>(nullable: false),
                    EnergencyContactFirstName = table.Column<string>(maxLength: 30, nullable: false),
                    EnergencyContactLastName = table.Column<string>(maxLength: 30, nullable: false),
                    EnergencyContactPhoneNumber = table.Column<string>(maxLength: 12, nullable: false),
                    EnergencyContactRelationship = table.Column<string>(maxLength: 30, nullable: false),
                    Ethnicity = table.Column<string>(maxLength: 3, nullable: false),
                    FormerFirstName = table.Column<string>(maxLength: 30, nullable: true),
                    FormerLastName = table.Column<string>(maxLength: 30, nullable: true),
                    FormerMiddleName = table.Column<string>(maxLength: 30, nullable: true),
                    Gender = table.Column<string>(maxLength: 1, nullable: false),
                    HasFormerName = table.Column<bool>(nullable: false),
                    Race = table.Column<string>(maxLength: 3, nullable: false),
                    SocialSecurityNumber = table.Column<string>(maxLength: 11, nullable: false)
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
                    UserId = table.Column<int>(nullable: false),
                    GovernmentBenefits = table.Column<string>(maxLength: 10, nullable: true),
                    IsVeteran = table.Column<bool>(nullable: false),
                    MilitaryBranch = table.Column<string>(maxLength: 4, nullable: true),
                    MilitaryStatus = table.Column<string>(maxLength: 4, nullable: true),
                    OverallEducationalGoal = table.Column<string>(maxLength: 4, nullable: false),
                    ReasonForAttending = table.Column<string>(maxLength: 4, nullable: false),
                    VeteranType = table.Column<string>(maxLength: 4, nullable: true)
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
                    UserId = table.Column<int>(nullable: false),
                    Address1 = table.Column<string>(maxLength: 30, nullable: false),
                    Address2 = table.Column<string>(maxLength: 30, nullable: true),
                    CellPhone = table.Column<string>(maxLength: 15, nullable: true),
                    City = table.Column<string>(maxLength: 30, nullable: false),
                    County = table.Column<string>(maxLength: 25, nullable: true),
                    FirstName = table.Column<string>(maxLength: 30, nullable: false),
                    LastName = table.Column<string>(maxLength: 30, nullable: false),
                    MiddleName = table.Column<string>(maxLength: 20, nullable: true),
                    OtherPhone = table.Column<string>(maxLength: 15, nullable: true),
                    PrimaryEmail = table.Column<string>(maxLength: 40, nullable: true),
                    State = table.Column<string>(maxLength: 25, nullable: false),
                    Suffix = table.Column<string>(maxLength: 6, nullable: true),
                    ZipCode = table.Column<string>(maxLength: 5, nullable: false)
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
                    UserId = table.Column<int>(nullable: false),
                    CitizenshipStatus = table.Column<string>(maxLength: 6, nullable: false),
                    CountryOfCitizenship = table.Column<string>(maxLength: 55, nullable: true),
                    DriversLicenseNumber = table.Column<string>(maxLength: 25, nullable: true),
                    DriversLicenseState = table.Column<string>(maxLength: 10, nullable: true),
                    HasValidDriversLicense = table.Column<bool>(nullable: false),
                    ImmigrationStatus = table.Column<string>(maxLength: 6, nullable: true),
                    ResidentState = table.Column<string>(maxLength: 55, nullable: false)
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
                    InstitutionId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    EndYear = table.Column<string>(maxLength: 4, nullable: false),
                    HighestDegreeEarned = table.Column<string>(maxLength: 4, nullable: false),
                    InstitutionName = table.Column<string>(nullable: false),
                    InstitutionState = table.Column<string>(maxLength: 256, nullable: false),
                    MonthYearGraduated = table.Column<DateTime>(nullable: true),
                    StartYear = table.Column<string>(maxLength: 4, nullable: false),
                    UserId = table.Column<int>(nullable: false)
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
                    StateLivedInId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    State = table.Column<string>(maxLength: 25, nullable: false),
                    UserId = table.Column<int>(nullable: false)
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

            migrationBuilder.CreateIndex(
                name: "uc_RulesModule",
                schema: "Rules",
                table: "RulesModule",
                columns: new[] { "Name", "Application" },
                unique: true);
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
                name: "VariableMetaData",
                schema: "Automatic");

            migrationBuilder.DropTable(
                name: "RulesModule",
                schema: "Rules");

            migrationBuilder.DropTable(
                name: "Academic");

            migrationBuilder.DropTable(
                name: "Residency");

            migrationBuilder.DropTable(
                name: "User");
        }
    }
}
