﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Enrollment.MigrationTool.Migrations
{
    [DbContext(typeof(MigrationContext))]
    [Migration("20211230132910_Initial")]
    partial class Initial
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.1")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("Enrollment.Data.Entities.Academic", b =>
                {
                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.Property<bool>("AttendedPriorColleges")
                        .HasColumnType("bit");

                    b.Property<bool>("EarnedCreditAtCmc")
                        .HasColumnType("bit");

                    b.Property<DateTime>("FromDate")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("GedDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("GedLocation")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("GraduationStatus")
                        .IsRequired()
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("HomeSchoolAssociation")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("HomeSchoolType")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("LastHighSchoolLocation")
                        .IsRequired()
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("NcHighSchoolName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<bool?>("ReceivedGed")
                        .HasColumnType("bit");

                    b.Property<DateTime>("ToDate")
                        .HasColumnType("datetime2");

                    b.HasKey("UserId");

                    b.ToTable("Academic");
                });

            modelBuilder.Entity("Enrollment.Data.Entities.Admissions", b =>
                {
                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.Property<string>("EnrollmentTerm")
                        .IsRequired()
                        .HasMaxLength(6)
                        .HasColumnType("nvarchar(6)");

                    b.Property<string>("EnrollmentYear")
                        .IsRequired()
                        .HasMaxLength(4)
                        .HasColumnType("nvarchar(4)");

                    b.Property<string>("EnteringStatus")
                        .IsRequired()
                        .HasMaxLength(1)
                        .HasColumnType("nvarchar(1)");

                    b.Property<string>("Program")
                        .IsRequired()
                        .HasMaxLength(55)
                        .HasColumnType("nvarchar(55)");

                    b.Property<string>("ProgramType")
                        .IsRequired()
                        .HasMaxLength(55)
                        .HasColumnType("nvarchar(55)");

                    b.HasKey("UserId");

                    b.ToTable("Admissions");
                });

            modelBuilder.Entity("Enrollment.Data.Entities.Certification", b =>
                {
                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.Property<bool>("CertificateStatementChecked")
                        .HasColumnType("bit");

                    b.Property<bool>("DeclarationStatementChecked")
                        .HasColumnType("bit");

                    b.Property<bool>("PolicyStatementsChecked")
                        .HasColumnType("bit");

                    b.HasKey("UserId");

                    b.ToTable("Certification");
                });

            modelBuilder.Entity("Enrollment.Data.Entities.ContactInfo", b =>
                {
                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.Property<DateTime>("DateOfBirth")
                        .HasColumnType("datetime2");

                    b.Property<string>("EnergencyContactFirstName")
                        .IsRequired()
                        .HasMaxLength(30)
                        .HasColumnType("nvarchar(30)");

                    b.Property<string>("EnergencyContactLastName")
                        .IsRequired()
                        .HasMaxLength(30)
                        .HasColumnType("nvarchar(30)");

                    b.Property<string>("EnergencyContactPhoneNumber")
                        .IsRequired()
                        .HasMaxLength(12)
                        .HasColumnType("nvarchar(12)");

                    b.Property<string>("EnergencyContactRelationship")
                        .IsRequired()
                        .HasMaxLength(30)
                        .HasColumnType("nvarchar(30)");

                    b.Property<string>("Ethnicity")
                        .IsRequired()
                        .HasMaxLength(3)
                        .HasColumnType("nvarchar(3)");

                    b.Property<string>("FormerFirstName")
                        .HasMaxLength(30)
                        .HasColumnType("nvarchar(30)");

                    b.Property<string>("FormerLastName")
                        .HasMaxLength(30)
                        .HasColumnType("nvarchar(30)");

                    b.Property<string>("FormerMiddleName")
                        .HasMaxLength(30)
                        .HasColumnType("nvarchar(30)");

                    b.Property<string>("Gender")
                        .IsRequired()
                        .HasMaxLength(1)
                        .HasColumnType("nvarchar(1)");

                    b.Property<bool>("HasFormerName")
                        .HasColumnType("bit");

                    b.Property<string>("Race")
                        .IsRequired()
                        .HasMaxLength(3)
                        .HasColumnType("nvarchar(3)");

                    b.Property<string>("SocialSecurityNumber")
                        .IsRequired()
                        .HasMaxLength(11)
                        .HasColumnType("nvarchar(11)");

                    b.HasKey("UserId");

                    b.ToTable("ContactInfo");
                });

            modelBuilder.Entity("Enrollment.Data.Entities.Institution", b =>
                {
                    b.Property<int>("InstitutionId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("InstitutionId"), 1L, 1);

                    b.Property<string>("EndYear")
                        .IsRequired()
                        .HasMaxLength(4)
                        .HasColumnType("nvarchar(4)");

                    b.Property<string>("HighestDegreeEarned")
                        .IsRequired()
                        .HasMaxLength(4)
                        .HasColumnType("nvarchar(4)");

                    b.Property<string>("InstitutionName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("InstitutionState")
                        .IsRequired()
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<DateTime?>("MonthYearGraduated")
                        .HasColumnType("datetime2");

                    b.Property<string>("StartYear")
                        .IsRequired()
                        .HasMaxLength(4)
                        .HasColumnType("nvarchar(4)");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("InstitutionId");

                    b.HasIndex("UserId");

                    b.ToTable("Institutions");
                });

            modelBuilder.Entity("Enrollment.Data.Entities.LookUps", b =>
                {
                    b.Property<int>("LookUpsID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("LookUpsID"), 1L, 1);

                    b.Property<bool?>("BooleanValue")
                        .HasColumnType("bit");

                    b.Property<string>("CharValue")
                        .HasColumnType("nvarchar(1)");

                    b.Property<DateTime?>("DateTimeValue")
                        .HasColumnType("datetime2");

                    b.Property<Guid?>("GuidValue")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("ListName")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<double?>("NumericValue")
                        .HasColumnType("float");

                    b.Property<int>("Order")
                        .HasColumnType("int");

                    b.Property<string>("Text")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<TimeSpan?>("TimeSpanValue")
                        .HasColumnType("time");

                    b.Property<string>("Value")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.HasKey("LookUpsID");

                    b.ToTable("LookUps");
                });

            modelBuilder.Entity("Enrollment.Data.Entities.MoreInfo", b =>
                {
                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.Property<string>("GovernmentBenefits")
                        .HasMaxLength(10)
                        .HasColumnType("nvarchar(10)");

                    b.Property<bool>("IsVeteran")
                        .HasColumnType("bit");

                    b.Property<string>("MilitaryBranch")
                        .HasMaxLength(4)
                        .HasColumnType("nvarchar(4)");

                    b.Property<string>("MilitaryStatus")
                        .HasMaxLength(4)
                        .HasColumnType("nvarchar(4)");

                    b.Property<string>("OverallEducationalGoal")
                        .IsRequired()
                        .HasMaxLength(4)
                        .HasColumnType("nvarchar(4)");

                    b.Property<string>("ReasonForAttending")
                        .IsRequired()
                        .HasMaxLength(4)
                        .HasColumnType("nvarchar(4)");

                    b.Property<string>("VeteranType")
                        .HasMaxLength(4)
                        .HasColumnType("nvarchar(4)");

                    b.HasKey("UserId");

                    b.ToTable("MoreInfo");
                });

            modelBuilder.Entity("Enrollment.Data.Entities.Personal", b =>
                {
                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.Property<string>("Address1")
                        .IsRequired()
                        .HasMaxLength(30)
                        .HasColumnType("nvarchar(30)");

                    b.Property<string>("Address2")
                        .HasMaxLength(30)
                        .HasColumnType("nvarchar(30)");

                    b.Property<string>("CellPhone")
                        .HasMaxLength(15)
                        .HasColumnType("nvarchar(15)");

                    b.Property<string>("City")
                        .IsRequired()
                        .HasMaxLength(30)
                        .HasColumnType("nvarchar(30)");

                    b.Property<string>("County")
                        .HasMaxLength(25)
                        .HasColumnType("nvarchar(25)");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasMaxLength(30)
                        .HasColumnType("nvarchar(30)");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasMaxLength(30)
                        .HasColumnType("nvarchar(30)");

                    b.Property<string>("MiddleName")
                        .HasMaxLength(20)
                        .HasColumnType("nvarchar(20)");

                    b.Property<string>("OtherPhone")
                        .HasMaxLength(15)
                        .HasColumnType("nvarchar(15)");

                    b.Property<string>("PrimaryEmail")
                        .HasMaxLength(40)
                        .HasColumnType("nvarchar(40)");

                    b.Property<string>("State")
                        .IsRequired()
                        .HasMaxLength(25)
                        .HasColumnType("nvarchar(25)");

                    b.Property<string>("Suffix")
                        .HasMaxLength(6)
                        .HasColumnType("nvarchar(6)");

                    b.Property<string>("ZipCode")
                        .IsRequired()
                        .HasMaxLength(5)
                        .HasColumnType("nvarchar(5)");

                    b.HasKey("UserId");

                    b.ToTable("Personal");
                });

            modelBuilder.Entity("Enrollment.Data.Entities.Residency", b =>
                {
                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.Property<string>("CitizenshipStatus")
                        .IsRequired()
                        .HasMaxLength(6)
                        .HasColumnType("nvarchar(6)");

                    b.Property<string>("CountryOfCitizenship")
                        .HasMaxLength(55)
                        .HasColumnType("nvarchar(55)");

                    b.Property<string>("DriversLicenseNumber")
                        .HasMaxLength(25)
                        .HasColumnType("nvarchar(25)");

                    b.Property<string>("DriversLicenseState")
                        .HasMaxLength(10)
                        .HasColumnType("nvarchar(10)");

                    b.Property<bool>("HasValidDriversLicense")
                        .HasColumnType("bit");

                    b.Property<string>("ImmigrationStatus")
                        .HasMaxLength(6)
                        .HasColumnType("nvarchar(6)");

                    b.Property<string>("ResidentState")
                        .IsRequired()
                        .HasMaxLength(55)
                        .HasColumnType("nvarchar(55)");

                    b.HasKey("UserId");

                    b.ToTable("Residency");
                });

            modelBuilder.Entity("Enrollment.Data.Entities.StateLivedIn", b =>
                {
                    b.Property<int>("StateLivedInId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("StateLivedInId"), 1L, 1);

                    b.Property<string>("State")
                        .IsRequired()
                        .HasMaxLength(25)
                        .HasColumnType("nvarchar(25)");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("StateLivedInId");

                    b.HasIndex("UserId");

                    b.ToTable("StatesLivedIn");
                });

            modelBuilder.Entity("Enrollment.Data.Entities.User", b =>
                {
                    b.Property<int>("UserId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("UserId"), 1L, 1);

                    b.Property<string>("UserName")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("UserId");

                    b.ToTable("User");
                });

            modelBuilder.Entity("Enrollment.Data.Entities.Academic", b =>
                {
                    b.HasOne("Enrollment.Data.Entities.User", "User")
                        .WithOne("Academic")
                        .HasForeignKey("Enrollment.Data.Entities.Academic", "UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("Enrollment.Data.Entities.Admissions", b =>
                {
                    b.HasOne("Enrollment.Data.Entities.User", "User")
                        .WithOne("Admissions")
                        .HasForeignKey("Enrollment.Data.Entities.Admissions", "UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("Enrollment.Data.Entities.Certification", b =>
                {
                    b.HasOne("Enrollment.Data.Entities.User", "User")
                        .WithOne("Certification")
                        .HasForeignKey("Enrollment.Data.Entities.Certification", "UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("Enrollment.Data.Entities.ContactInfo", b =>
                {
                    b.HasOne("Enrollment.Data.Entities.User", "User")
                        .WithOne("ContactInfo")
                        .HasForeignKey("Enrollment.Data.Entities.ContactInfo", "UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("Enrollment.Data.Entities.Institution", b =>
                {
                    b.HasOne("Enrollment.Data.Entities.Academic", "Academic")
                        .WithMany("Institutions")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Academic");
                });

            modelBuilder.Entity("Enrollment.Data.Entities.MoreInfo", b =>
                {
                    b.HasOne("Enrollment.Data.Entities.User", "User")
                        .WithOne("MoreInfo")
                        .HasForeignKey("Enrollment.Data.Entities.MoreInfo", "UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("Enrollment.Data.Entities.Personal", b =>
                {
                    b.HasOne("Enrollment.Data.Entities.User", "User")
                        .WithOne("Personal")
                        .HasForeignKey("Enrollment.Data.Entities.Personal", "UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("Enrollment.Data.Entities.Residency", b =>
                {
                    b.HasOne("Enrollment.Data.Entities.User", "User")
                        .WithOne("Residency")
                        .HasForeignKey("Enrollment.Data.Entities.Residency", "UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("Enrollment.Data.Entities.StateLivedIn", b =>
                {
                    b.HasOne("Enrollment.Data.Entities.Residency", "Residency")
                        .WithMany("StatesLivedIn")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Residency");
                });

            modelBuilder.Entity("Enrollment.Data.Entities.Academic", b =>
                {
                    b.Navigation("Institutions");
                });

            modelBuilder.Entity("Enrollment.Data.Entities.Residency", b =>
                {
                    b.Navigation("StatesLivedIn");
                });

            modelBuilder.Entity("Enrollment.Data.Entities.User", b =>
                {
                    b.Navigation("Academic");

                    b.Navigation("Admissions");

                    b.Navigation("Certification");

                    b.Navigation("ContactInfo");

                    b.Navigation("MoreInfo");

                    b.Navigation("Personal");

                    b.Navigation("Residency");
                });
#pragma warning restore 612, 618
        }
    }
}
