﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using VpnetworkAPI.DbContex;

#nullable disable

namespace VpnetworkAPI.Migrations
{
    [DbContext(typeof(UserDbContext))]
    [Migration("20240110092832_vpnetwork")]
    partial class vpnetwork
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("VpnetworkAPI.Models.Analysis", b =>
                {
                    b.Property<Guid>("AnalysisId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("DateTime")
                        .HasColumnType("datetime2");

                    b.Property<long>("MemoryUsage")
                        .HasColumnType("bigint");

                    b.Property<double>("NetworkUsage")
                        .HasColumnType("float");

                    b.Property<string>("ProgramName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("AnalysisId");

                    b.HasIndex("UserId");

                    b.ToTable("Analyses");
                });

            modelBuilder.Entity("VpnetworkAPI.Models.GlobalProgramData", b =>
                {
                    b.Property<string>("ProgramName")
                        .HasColumnType("nvarchar(450)");

                    b.Property<bool>("IsHarmful")
                        .HasColumnType("bit");

                    b.Property<double>("ProgramGLobalMemoryThreshold")
                        .HasColumnType("float");

                    b.Property<double>("ProgramGlobalNetworkThreshold")
                        .HasColumnType("float");

                    b.HasKey("ProgramName");

                    b.ToTable("GlobalProgramData");
                });

            modelBuilder.Entity("VpnetworkAPI.Models.LocalProgramData", b =>
                {
                    b.Property<Guid>("LocalProgramDataId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<bool?>("IsHarmful")
                        .HasColumnType("bit");

                    b.Property<double>("ProgramLocalMemoryThreshold")
                        .HasColumnType("float");

                    b.Property<double>("ProgramLocalNetworkThreshold")
                        .HasColumnType("float");

                    b.Property<string>("ProgramName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("LocalProgramDataId");

                    b.HasIndex("UserId");

                    b.ToTable("LocalProgramData");
                });

            modelBuilder.Entity("VpnetworkAPI.Models.ProgramData", b =>
                {
                    b.Property<Guid>("ProgramDataId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<long>("MemoryUsage")
                        .HasColumnType("bigint");

                    b.Property<double>("NetworkUsage")
                        .HasColumnType("float");

                    b.Property<int>("ProgramBadCount")
                        .HasColumnType("int");

                    b.Property<string>("ProgramName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("ProgramDataId");

                    b.HasIndex("UserId");

                    b.ToTable("ProgramData");
                });

            modelBuilder.Entity("VpnetworkAPI.Models.ThresholdSettings", b =>
                {
                    b.Property<Guid>("ThresholdSettingDataId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("ProgramName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ThresholdSetting")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("ThresholdSettingDataId");

                    b.HasIndex("UserId");

                    b.ToTable("ThresholdSettings");
                });

            modelBuilder.Entity("VpnetworkAPI.Models.User", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PassWord")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserImage")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("User_Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("UserId");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("VpnetworkAPI.Models.Analysis", b =>
                {
                    b.HasOne("VpnetworkAPI.Models.User", "User")
                        .WithMany("Analysis")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("VpnetworkAPI.Models.LocalProgramData", b =>
                {
                    b.HasOne("VpnetworkAPI.Models.User", "User")
                        .WithMany("LocalProgramData")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("VpnetworkAPI.Models.ProgramData", b =>
                {
                    b.HasOne("VpnetworkAPI.Models.User", "User")
                        .WithMany("ProgramData")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("VpnetworkAPI.Models.ThresholdSettings", b =>
                {
                    b.HasOne("VpnetworkAPI.Models.User", "User")
                        .WithMany("ThresholdSettings")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("VpnetworkAPI.Models.User", b =>
                {
                    b.Navigation("Analysis");

                    b.Navigation("LocalProgramData");

                    b.Navigation("ProgramData");

                    b.Navigation("ThresholdSettings");
                });
#pragma warning restore 612, 618
        }
    }
}
