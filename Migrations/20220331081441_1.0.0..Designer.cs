﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using OpenExchange.Data;

#nullable disable

namespace OpenExchange.Migrations
{
    [DbContext(typeof(DbInteractor))]
    [Migration("20220331081441_1.0.0.")]
    partial class _100
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.3")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("OpenExchange.Models.EUR", b =>
                {
                    b.Property<string>("EurId")
                        .HasMaxLength(36)
                        .HasColumnType("nvarchar(36)");

                    b.Property<double>("Average")
                        .HasColumnType("float");

                    b.Property<double>("Close")
                        .HasColumnType("float");

                    b.Property<double>("High")
                        .HasColumnType("float");

                    b.Property<double>("Low")
                        .HasColumnType("float");

                    b.Property<double>("Open")
                        .HasColumnType("float");

                    b.HasKey("EurId");

                    b.ToTable("EURs");
                });

            modelBuilder.Entity("OpenExchange.Models.GBP", b =>
                {
                    b.Property<string>("GbpId")
                        .HasMaxLength(36)
                        .HasColumnType("nvarchar(36)");

                    b.Property<double>("Average")
                        .HasColumnType("float");

                    b.Property<double>("Close")
                        .HasColumnType("float");

                    b.Property<double>("High")
                        .HasColumnType("float");

                    b.Property<double>("Low")
                        .HasColumnType("float");

                    b.Property<double>("Open")
                        .HasColumnType("float");

                    b.HasKey("GbpId");

                    b.ToTable("GBPs");
                });

            modelBuilder.Entity("OpenExchange.Models.Rates", b =>
                {
                    b.Property<string>("RatestId")
                        .HasMaxLength(36)
                        .HasColumnType("nvarchar(36)");

                    b.Property<string>("EurId")
                        .HasColumnType("nvarchar(36)");

                    b.Property<string>("GbpId")
                        .HasColumnType("nvarchar(36)");

                    b.HasKey("RatestId");

                    b.HasIndex("EurId");

                    b.HasIndex("GbpId");

                    b.ToTable("Rates");
                });

            modelBuilder.Entity("OpenExchange.Models.RatesEx", b =>
                {
                    b.Property<string>("RatestId")
                        .HasMaxLength(36)
                        .HasColumnType("nvarchar(36)");

                    b.Property<DateTime>("DateRate")
                        .HasColumnType("datetime2");

                    b.Property<double>("Eur")
                        .HasColumnType("float");

                    b.Property<double>("Gbp")
                        .HasColumnType("float");

                    b.Property<double>("Rsd")
                        .HasColumnType("float");

                    b.HasKey("RatestId");

                    b.ToTable("RatesExs");
                });

            modelBuilder.Entity("OpenExchange.Models.Root", b =>
                {
                    b.Property<string>("RootId")
                        .HasMaxLength(36)
                        .HasColumnType("nvarchar(36)");

                    b.Property<string>("Disclaimer")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("End_time")
                        .HasColumnType("datetime2");

                    b.Property<string>("License")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("RatestId")
                        .HasColumnType("nvarchar(36)");

                    b.Property<DateTime>("Start_time")
                        .HasColumnType("datetime2");

                    b.Property<string>("base")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("RootId");

                    b.HasIndex("RatestId");

                    b.ToTable("Roots");
                });

            modelBuilder.Entity("OpenExchange.Models.RootEx", b =>
                {
                    b.Property<string>("RootExId")
                        .HasMaxLength(36)
                        .HasColumnType("nvarchar(36)");

                    b.Property<string>("Disclaimer")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("License")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("RatestId")
                        .HasColumnType("nvarchar(36)");

                    b.Property<int>("Timestamp")
                        .HasColumnType("int");

                    b.Property<string>("base")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("RootExId");

                    b.HasIndex("RatestId");

                    b.ToTable("RootsExs");
                });

            modelBuilder.Entity("OpenExchange.Models.Rates", b =>
                {
                    b.HasOne("OpenExchange.Models.EUR", "EUR")
                        .WithMany()
                        .HasForeignKey("EurId");

                    b.HasOne("OpenExchange.Models.GBP", "GBP")
                        .WithMany()
                        .HasForeignKey("GbpId");

                    b.Navigation("EUR");

                    b.Navigation("GBP");
                });

            modelBuilder.Entity("OpenExchange.Models.Root", b =>
                {
                    b.HasOne("OpenExchange.Models.Rates", "Rates")
                        .WithMany()
                        .HasForeignKey("RatestId");

                    b.Navigation("Rates");
                });

            modelBuilder.Entity("OpenExchange.Models.RootEx", b =>
                {
                    b.HasOne("OpenExchange.Models.RatesEx", "Rates")
                        .WithMany()
                        .HasForeignKey("RatestId");

                    b.Navigation("Rates");
                });
#pragma warning restore 612, 618
        }
    }
}
