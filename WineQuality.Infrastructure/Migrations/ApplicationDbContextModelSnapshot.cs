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

            modelBuilder.Entity("WineQuality.Domain.Entities.FileReference", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ContainerName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

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

            modelBuilder.Entity("WineQuality.Domain.Entities.GrapeSortPhaseForecastModel", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("ForecastingModelFileReferenceId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("GrapeSortId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("PhaseTypeId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<DateTime?>("UpdatedAt")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.HasIndex("ForecastingModelFileReferenceId");

                    b.HasIndex("GrapeSortId");

                    b.HasIndex("PhaseTypeId");

                    b.ToTable("GrapeSortPhaseForecastModels");
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

            modelBuilder.Entity("WineQuality.Domain.Entities.ProcessPhaseParameter", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("ParameterId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("PhaseTypeId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<DateTime?>("UpdatedAt")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.HasIndex("ParameterId");

                    b.HasIndex("PhaseTypeId");

                    b.ToTable("ProcessPhaseParameters");
                });

            modelBuilder.Entity("WineQuality.Domain.Entities.ProcessPhaseType", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PreviousPhaseId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<DateTime?>("UpdatedAt")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.HasIndex("PreviousPhaseId")
                        .IsUnique()
                        .HasFilter("[PreviousPhaseId] IS NOT NULL");

                    b.ToTable("ProcessPhaseTypes");
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

            modelBuilder.Entity("WineQuality.Domain.Entities.WineMaterialBatchProcessParameterValue", b =>
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

                    b.ToTable("WineMaterialBatchProcessParameterValues");
                });

            modelBuilder.Entity("WineQuality.Domain.Entities.WineMaterialBatchProcessPhase", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("EndDate")
                        .HasColumnType("datetime2");

                    b.Property<bool>("IsActive")
                        .HasColumnType("bit");

                    b.Property<string>("PhaseTypeId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<DateTime>("StartDate")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("UpdatedAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("WineMaterialBatchId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("PhaseTypeId");

                    b.HasIndex("WineMaterialBatchId");

                    b.ToTable("WineMaterialBatchProcessPhases");
                });

            modelBuilder.Entity("WineQuality.Domain.Entities.WineMaterialBatchProcessPhaseParameter", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<double?>("LowerBound")
                        .HasColumnType("float");

                    b.Property<string>("PhaseParameterId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<DateTime?>("UpdatedAt")
                        .HasColumnType("datetime2");

                    b.Property<double?>("UpperBound")
                        .HasColumnType("float");

                    b.Property<string>("WineMaterialBatchId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("PhaseParameterId");

                    b.HasIndex("WineMaterialBatchId");

                    b.ToTable("WineMaterialBatchProcessPhaseParameters");
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

            modelBuilder.Entity("WineQuality.Domain.Entities.GrapeSortPhaseForecastModel", b =>
                {
                    b.HasOne("WineQuality.Domain.Entities.FileReference", "ForecastingModelFileReference")
                        .WithMany("GrapeSortPhaseForecastModels")
                        .HasForeignKey("ForecastingModelFileReferenceId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("WineQuality.Domain.Entities.GrapeSort", "GrapeSort")
                        .WithMany("GrapeSortPhaseForecastModels")
                        .HasForeignKey("GrapeSortId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("WineQuality.Domain.Entities.ProcessPhaseType", "PhaseType")
                        .WithMany("GrapeSortPhaseForecastModels")
                        .HasForeignKey("PhaseTypeId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("ForecastingModelFileReference");

                    b.Navigation("GrapeSort");

                    b.Navigation("PhaseType");
                });

            modelBuilder.Entity("WineQuality.Domain.Entities.ProcessPhaseParameter", b =>
                {
                    b.HasOne("WineQuality.Domain.Entities.ProcessParameter", "Parameter")
                        .WithMany("Phases")
                        .HasForeignKey("ParameterId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("WineQuality.Domain.Entities.ProcessPhaseType", "PhaseType")
                        .WithMany("Parameters")
                        .HasForeignKey("PhaseTypeId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Parameter");

                    b.Navigation("PhaseType");
                });

            modelBuilder.Entity("WineQuality.Domain.Entities.ProcessPhaseType", b =>
                {
                    b.HasOne("WineQuality.Domain.Entities.ProcessPhaseType", "PreviousPhase")
                        .WithOne("NextPhase")
                        .HasForeignKey("WineQuality.Domain.Entities.ProcessPhaseType", "PreviousPhaseId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.Navigation("PreviousPhase");
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

            modelBuilder.Entity("WineQuality.Domain.Entities.WineMaterialBatchProcessParameterValue", b =>
                {
                    b.HasOne("WineQuality.Domain.Entities.WineMaterialBatchProcessPhaseParameter", "PhaseParameter")
                        .WithMany("Values")
                        .HasForeignKey("PhaseParameterId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("PhaseParameter");
                });

            modelBuilder.Entity("WineQuality.Domain.Entities.WineMaterialBatchProcessPhase", b =>
                {
                    b.HasOne("WineQuality.Domain.Entities.ProcessPhaseType", "PhaseType")
                        .WithMany()
                        .HasForeignKey("PhaseTypeId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("WineQuality.Domain.Entities.WineMaterialBatch", "WineMaterialBatch")
                        .WithMany("Phases")
                        .HasForeignKey("WineMaterialBatchId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("PhaseType");

                    b.Navigation("WineMaterialBatch");
                });

            modelBuilder.Entity("WineQuality.Domain.Entities.WineMaterialBatchProcessPhaseParameter", b =>
                {
                    b.HasOne("WineQuality.Domain.Entities.ProcessPhaseParameter", "PhaseParameter")
                        .WithMany()
                        .HasForeignKey("PhaseParameterId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("WineQuality.Domain.Entities.WineMaterialBatchProcessPhase", "WineMaterialBatch")
                        .WithMany("Parameters")
                        .HasForeignKey("WineMaterialBatchId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("PhaseParameter");

                    b.Navigation("WineMaterialBatch");
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

            modelBuilder.Entity("WineQuality.Domain.Entities.FileReference", b =>
                {
                    b.Navigation("GrapeSortPhaseForecastModels");
                });

            modelBuilder.Entity("WineQuality.Domain.Entities.GrapeSort", b =>
                {
                    b.Navigation("GrapeSortPhaseForecastModels");

                    b.Navigation("WineMaterialBatches");
                });

            modelBuilder.Entity("WineQuality.Domain.Entities.ProcessParameter", b =>
                {
                    b.Navigation("Phases");
                });

            modelBuilder.Entity("WineQuality.Domain.Entities.ProcessPhaseType", b =>
                {
                    b.Navigation("GrapeSortPhaseForecastModels");

                    b.Navigation("NextPhase");

                    b.Navigation("Parameters");
                });

            modelBuilder.Entity("WineQuality.Domain.Entities.WineMaterialBatch", b =>
                {
                    b.Navigation("Phases");
                });

            modelBuilder.Entity("WineQuality.Domain.Entities.WineMaterialBatchProcessPhase", b =>
                {
                    b.Navigation("Parameters");
                });

            modelBuilder.Entity("WineQuality.Domain.Entities.WineMaterialBatchProcessPhaseParameter", b =>
                {
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
