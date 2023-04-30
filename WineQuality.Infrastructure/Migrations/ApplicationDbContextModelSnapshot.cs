﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using WineQuality.Infrastructure.Persistence;

#nullable disable

namespace WineQuality.Infrastructure.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    partial class ApplicationDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.16")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("RoleId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.Property<string>("LoginProvider")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ProviderKey")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ProviderDisplayName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("LoginProvider")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Value")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens", (string)null);
                });

            modelBuilder.Entity("WineQuality.Domain.Entities.Culture", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("CultureCode")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("CultureName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("UpdatedAt")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.ToTable("Cultures");
                });

            modelBuilder.Entity("WineQuality.Domain.Entities.FileReference", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("UpdatedAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("Uri")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("FileReferences");
                });

            modelBuilder.Entity("WineQuality.Domain.Entities.GrapeSort", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<DateTime?>("UpdatedAt")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.HasIndex("Name")
                        .IsUnique();

                    b.ToTable("GrapeSorts");
                });

            modelBuilder.Entity("WineQuality.Domain.Entities.GrapeSortPhase", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<int>("Duration")
                        .HasColumnType("int");

                    b.Property<string>("GrapeSortId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("Order")
                        .HasColumnType("int");

                    b.Property<string>("PhaseId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("PreviousPhaseId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<DateTime?>("UpdatedAt")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.HasIndex("GrapeSortId");

                    b.HasIndex("Order")
                        .IsUnique();

                    b.HasIndex("PhaseId");

                    b.HasIndex("PreviousPhaseId")
                        .IsUnique()
                        .HasFilter("[PreviousPhaseId] IS NOT NULL");

                    b.ToTable("GrapeSortPhases");
                });

            modelBuilder.Entity("WineQuality.Domain.Entities.GrapeSortPhaseDataset", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("DatasetFileReferenceId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("GrapeSortPhaseId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("UpdatedAt")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.HasIndex("DatasetFileReferenceId");

                    b.HasIndex("GrapeSortPhaseId", "DatasetFileReferenceId")
                        .IsUnique();

                    b.ToTable("GrapeSortPhaseDatasets");
                });

            modelBuilder.Entity("WineQuality.Domain.Entities.GrapeSortPhaseForecastModel", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<decimal?>("Accuracy")
                        .HasColumnType("decimal(18,2)");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("ForecastingModelFileReferenceId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("GrapeSortPhaseId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<DateTime?>("UpdatedAt")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.HasIndex("ForecastingModelFileReferenceId");

                    b.HasIndex("GrapeSortPhaseId", "ForecastingModelFileReferenceId")
                        .IsUnique()
                        .HasFilter("[ForecastingModelFileReferenceId] IS NOT NULL");

                    b.ToTable("GrapeSortPhaseForecastModels");
                });

            modelBuilder.Entity("WineQuality.Domain.Entities.GrapeSortProcessPhaseParameterStandard", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("GrapeSortPhaseId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<double?>("LowerBound")
                        .HasColumnType("float");

                    b.Property<string>("PhaseParameterId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<DateTime?>("UpdatedAt")
                        .HasColumnType("datetime2");

                    b.Property<double?>("UpperBound")
                        .HasColumnType("float");

                    b.HasKey("Id");

                    b.HasIndex("GrapeSortPhaseId");

                    b.HasIndex("PhaseParameterId");

                    b.ToTable("GrapeSortProcessPhaseParameterStandards");
                });

            modelBuilder.Entity("WineQuality.Domain.Entities.Localization", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("CultureId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("LocalName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ProcessParameterId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ProcessPhaseId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<DateTime?>("UpdatedAt")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.HasIndex("CultureId");

                    b.HasIndex("ProcessParameterId");

                    b.HasIndex("ProcessPhaseId");

                    b.ToTable("Localizations");
                });

            modelBuilder.Entity("WineQuality.Domain.Entities.ProcessParameter", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("UpdatedAt")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.ToTable("ProcessParameters");
                });

            modelBuilder.Entity("WineQuality.Domain.Entities.ProcessPhase", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("UpdatedAt")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.ToTable("ProcessPhases");
                });

            modelBuilder.Entity("WineQuality.Domain.Entities.ProcessPhaseParameter", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("ParameterId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("PhaseId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<DateTime?>("UpdatedAt")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.HasIndex("PhaseId");

                    b.HasIndex("ParameterId", "PhaseId")
                        .IsUnique();

                    b.ToTable("ProcessPhaseParameters");
                });

            modelBuilder.Entity("WineQuality.Domain.Entities.ProcessPhaseParameterSensor", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("DeviceKey")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("DeviceName")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<bool>("IsActive")
                        .HasColumnType("bit");

                    b.Property<string>("PhaseParameterId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<DateTime?>("UpdatedAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("WineMaterialBatchGrapeSortPhaseParameterId")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("DeviceKey")
                        .IsUnique();

                    b.HasIndex("DeviceName")
                        .IsUnique();

                    b.HasIndex("PhaseParameterId");

                    b.HasIndex("WineMaterialBatchGrapeSortPhaseParameterId");

                    b.ToTable("ProcessPhaseParameterSensors");
                });

            modelBuilder.Entity("WineQuality.Domain.Entities.WineMaterialBatch", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("GrapeSortId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<DateTime>("HarvestDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("HarvestLocation")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("UpdatedAt")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.HasIndex("GrapeSortId");

                    b.ToTable("WineMaterialBatches");
                });

            modelBuilder.Entity("WineQuality.Domain.Entities.WineMaterialBatchGrapeSortPhase", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("EndDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("GrapeSortPhaseId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<bool>("IsActive")
                        .HasColumnType("bit");

                    b.Property<DateTime>("StartDate")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("UpdatedAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("WineMaterialBatchId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("GrapeSortPhaseId");

                    b.HasIndex("WineMaterialBatchId", "GrapeSortPhaseId")
                        .IsUnique();

                    b.ToTable("WineMaterialBatchGrapeSortPhases");
                });

            modelBuilder.Entity("WineQuality.Domain.Entities.WineMaterialBatchGrapeSortPhaseParameter", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("PhaseParameterId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<DateTime?>("UpdatedAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("WineMaterialBatchGrapeSortPhaseId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("PhaseParameterId");

                    b.HasIndex("WineMaterialBatchGrapeSortPhaseId");

                    b.ToTable("WineMaterialBatchGrapeSortPhaseParameters");
                });

            modelBuilder.Entity("WineQuality.Domain.Entities.WineMaterialBatchGrapeSortPhaseParameterValue", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("PhaseParameterId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<DateTime?>("UpdatedAt")
                        .HasColumnType("datetime2");

                    b.Property<double>("Value")
                        .HasColumnType("float");

                    b.HasKey("Id");

                    b.HasIndex("PhaseParameterId");

                    b.ToTable("WineMaterialBatchGrapeSortPhaseParameterValues");
                });

            modelBuilder.Entity("WineQuality.Infrastructure.Identity.AppRole", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("Name")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("NormalizedName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<DateTime?>("UpdatedAt")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasDatabaseName("RoleNameIndex")
                        .HasFilter("[NormalizedName] IS NOT NULL");

                    b.ToTable("AspNetRoles", (string)null);
                });

            modelBuilder.Entity("WineQuality.Infrastructure.Identity.AppUser", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("AccessFailedCount")
                        .HasColumnType("int");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("Email")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<bool>("EmailConfirmed")
                        .HasColumnType("bit");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("LockoutEnabled")
                        .HasColumnType("bit");

                    b.Property<DateTimeOffset?>("LockoutEnd")
                        .HasColumnType("datetimeoffset");

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("PasswordHash")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("PhoneNumberConfirmed")
                        .HasColumnType("bit");

                    b.Property<string>("SecurityStamp")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("TwoFactorEnabled")
                        .HasColumnType("bit");

                    b.Property<DateTime?>("UpdatedAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("UserName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedEmail")
                        .HasDatabaseName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasDatabaseName("UserNameIndex")
                        .HasFilter("[NormalizedUserName] IS NOT NULL");

                    b.ToTable("AspNetUsers", (string)null);
                });

            modelBuilder.Entity("WineQuality.Infrastructure.Identity.AppUserRole", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("RoleId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("UpdatedAt")
                        .HasColumnType("datetime2");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetUserRoles", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.HasOne("WineQuality.Infrastructure.Identity.AppRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.HasOne("WineQuality.Infrastructure.Identity.AppUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.HasOne("WineQuality.Infrastructure.Identity.AppUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.HasOne("WineQuality.Infrastructure.Identity.AppUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("WineQuality.Domain.Entities.GrapeSortPhase", b =>
                {
                    b.HasOne("WineQuality.Domain.Entities.GrapeSort", "GrapeSort")
                        .WithMany("Phases")
                        .HasForeignKey("GrapeSortId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("WineQuality.Domain.Entities.ProcessPhase", "Phase")
                        .WithMany("GrapeSorts")
                        .HasForeignKey("PhaseId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("WineQuality.Domain.Entities.GrapeSortPhase", "PreviousPhase")
                        .WithOne("NextPhase")
                        .HasForeignKey("WineQuality.Domain.Entities.GrapeSortPhase", "PreviousPhaseId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.Navigation("GrapeSort");

                    b.Navigation("Phase");

                    b.Navigation("PreviousPhase");
                });

            modelBuilder.Entity("WineQuality.Domain.Entities.GrapeSortPhaseDataset", b =>
                {
                    b.HasOne("WineQuality.Domain.Entities.FileReference", "DatasetFileReference")
                        .WithMany("GrapeSortPhaseDatasets")
                        .HasForeignKey("DatasetFileReferenceId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("WineQuality.Domain.Entities.GrapeSortPhase", "GrapeSortPhase")
                        .WithMany("Datasets")
                        .HasForeignKey("GrapeSortPhaseId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("DatasetFileReference");

                    b.Navigation("GrapeSortPhase");
                });

            modelBuilder.Entity("WineQuality.Domain.Entities.GrapeSortPhaseForecastModel", b =>
                {
                    b.HasOne("WineQuality.Domain.Entities.FileReference", "ForecastingModelFileReference")
                        .WithMany("GrapeSortPhaseForecastModels")
                        .HasForeignKey("ForecastingModelFileReferenceId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("WineQuality.Domain.Entities.GrapeSortPhase", "GrapeSortPhase")
                        .WithMany("GrapeSortPhaseForecastModels")
                        .HasForeignKey("GrapeSortPhaseId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("ForecastingModelFileReference");

                    b.Navigation("GrapeSortPhase");
                });

            modelBuilder.Entity("WineQuality.Domain.Entities.GrapeSortProcessPhaseParameterStandard", b =>
                {
                    b.HasOne("WineQuality.Domain.Entities.GrapeSortPhase", "GrapeSortPhase")
                        .WithMany("GrapeSortProcessPhaseParameterStandards")
                        .HasForeignKey("GrapeSortPhaseId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("WineQuality.Domain.Entities.ProcessPhaseParameter", "PhaseParameter")
                        .WithMany("GrapeSortProcessPhaseParameterStandards")
                        .HasForeignKey("PhaseParameterId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("GrapeSortPhase");

                    b.Navigation("PhaseParameter");
                });

            modelBuilder.Entity("WineQuality.Domain.Entities.Localization", b =>
                {
                    b.HasOne("WineQuality.Domain.Entities.Culture", "Culture")
                        .WithMany("Localizations")
                        .HasForeignKey("CultureId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("WineQuality.Domain.Entities.ProcessParameter", null)
                        .WithMany("Localizations")
                        .HasForeignKey("ProcessParameterId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("WineQuality.Domain.Entities.ProcessPhase", null)
                        .WithMany("Localizations")
                        .HasForeignKey("ProcessPhaseId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.Navigation("Culture");
                });

            modelBuilder.Entity("WineQuality.Domain.Entities.ProcessPhaseParameter", b =>
                {
                    b.HasOne("WineQuality.Domain.Entities.ProcessParameter", "Parameter")
                        .WithMany("Phases")
                        .HasForeignKey("ParameterId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("WineQuality.Domain.Entities.ProcessPhase", "Phase")
                        .WithMany("Parameters")
                        .HasForeignKey("PhaseId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Parameter");

                    b.Navigation("Phase");
                });

            modelBuilder.Entity("WineQuality.Domain.Entities.ProcessPhaseParameterSensor", b =>
                {
                    b.HasOne("WineQuality.Domain.Entities.ProcessPhaseParameter", "PhaseParameter")
                        .WithMany("Sensors")
                        .HasForeignKey("PhaseParameterId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("WineQuality.Domain.Entities.WineMaterialBatchGrapeSortPhaseParameter", "WineMaterialBatchGrapeSortPhaseParameter")
                        .WithMany("Sensors")
                        .HasForeignKey("WineMaterialBatchGrapeSortPhaseParameterId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.Navigation("PhaseParameter");

                    b.Navigation("WineMaterialBatchGrapeSortPhaseParameter");
                });

            modelBuilder.Entity("WineQuality.Domain.Entities.WineMaterialBatch", b =>
                {
                    b.HasOne("WineQuality.Domain.Entities.GrapeSort", "GrapeSort")
                        .WithMany("WineMaterialBatches")
                        .HasForeignKey("GrapeSortId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("GrapeSort");
                });

            modelBuilder.Entity("WineQuality.Domain.Entities.WineMaterialBatchGrapeSortPhase", b =>
                {
                    b.HasOne("WineQuality.Domain.Entities.GrapeSortPhase", "GrapeSortPhase")
                        .WithMany()
                        .HasForeignKey("GrapeSortPhaseId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("WineQuality.Domain.Entities.WineMaterialBatch", "WineMaterialBatch")
                        .WithMany("Phases")
                        .HasForeignKey("WineMaterialBatchId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("GrapeSortPhase");

                    b.Navigation("WineMaterialBatch");
                });

            modelBuilder.Entity("WineQuality.Domain.Entities.WineMaterialBatchGrapeSortPhaseParameter", b =>
                {
                    b.HasOne("WineQuality.Domain.Entities.ProcessPhaseParameter", "PhaseParameter")
                        .WithMany()
                        .HasForeignKey("PhaseParameterId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("WineQuality.Domain.Entities.WineMaterialBatchGrapeSortPhase", "WineMaterialBatchGrapeSortPhase")
                        .WithMany("Parameters")
                        .HasForeignKey("WineMaterialBatchGrapeSortPhaseId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("PhaseParameter");

                    b.Navigation("WineMaterialBatchGrapeSortPhase");
                });

            modelBuilder.Entity("WineQuality.Domain.Entities.WineMaterialBatchGrapeSortPhaseParameterValue", b =>
                {
                    b.HasOne("WineQuality.Domain.Entities.WineMaterialBatchGrapeSortPhaseParameter", "PhaseParameter")
                        .WithMany("Values")
                        .HasForeignKey("PhaseParameterId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("PhaseParameter");
                });

            modelBuilder.Entity("WineQuality.Infrastructure.Identity.AppUserRole", b =>
                {
                    b.HasOne("WineQuality.Infrastructure.Identity.AppRole", "Role")
                        .WithMany("UserRoles")
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("WineQuality.Infrastructure.Identity.AppUser", "User")
                        .WithMany("UserRoles")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Role");

                    b.Navigation("User");
                });

            modelBuilder.Entity("WineQuality.Domain.Entities.Culture", b =>
                {
                    b.Navigation("Localizations");
                });

            modelBuilder.Entity("WineQuality.Domain.Entities.FileReference", b =>
                {
                    b.Navigation("GrapeSortPhaseDatasets");

                    b.Navigation("GrapeSortPhaseForecastModels");
                });

            modelBuilder.Entity("WineQuality.Domain.Entities.GrapeSort", b =>
                {
                    b.Navigation("Phases");

                    b.Navigation("WineMaterialBatches");
                });

            modelBuilder.Entity("WineQuality.Domain.Entities.GrapeSortPhase", b =>
                {
                    b.Navigation("Datasets");

                    b.Navigation("GrapeSortPhaseForecastModels");

                    b.Navigation("GrapeSortProcessPhaseParameterStandards");

                    b.Navigation("NextPhase");
                });

            modelBuilder.Entity("WineQuality.Domain.Entities.ProcessParameter", b =>
                {
                    b.Navigation("Localizations");

                    b.Navigation("Phases");
                });

            modelBuilder.Entity("WineQuality.Domain.Entities.ProcessPhase", b =>
                {
                    b.Navigation("GrapeSorts");

                    b.Navigation("Localizations");

                    b.Navigation("Parameters");
                });

            modelBuilder.Entity("WineQuality.Domain.Entities.ProcessPhaseParameter", b =>
                {
                    b.Navigation("GrapeSortProcessPhaseParameterStandards");

                    b.Navigation("Sensors");
                });

            modelBuilder.Entity("WineQuality.Domain.Entities.WineMaterialBatch", b =>
                {
                    b.Navigation("Phases");
                });

            modelBuilder.Entity("WineQuality.Domain.Entities.WineMaterialBatchGrapeSortPhase", b =>
                {
                    b.Navigation("Parameters");
                });

            modelBuilder.Entity("WineQuality.Domain.Entities.WineMaterialBatchGrapeSortPhaseParameter", b =>
                {
                    b.Navigation("Sensors");

                    b.Navigation("Values");
                });

            modelBuilder.Entity("WineQuality.Infrastructure.Identity.AppRole", b =>
                {
                    b.Navigation("UserRoles");
                });

            modelBuilder.Entity("WineQuality.Infrastructure.Identity.AppUser", b =>
                {
                    b.Navigation("UserRoles");
                });
#pragma warning restore 612, 618
        }
    }
}
