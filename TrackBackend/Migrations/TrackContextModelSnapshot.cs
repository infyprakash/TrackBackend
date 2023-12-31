﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using TrackBackend.Models;

#nullable disable

namespace TrackBackend.Migrations
{
    [DbContext(typeof(TrackContext))]
    partial class TrackContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "7.0.10");

            modelBuilder.Entity("TrackBackend.Models.Manufacturer", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("CompanyName")
                        .HasColumnType("TEXT");

                    b.Property<DateTime?>("CreatedAt")
                        .HasColumnType("TEXT");

                    b.Property<string>("Email")
                        .HasColumnType("TEXT");

                    b.Property<bool?>("IsActive")
                        .HasColumnType("INTEGER");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("TEXT");

                    b.Property<string>("UserName")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Manufacturers");
                });

            modelBuilder.Entity("TrackBackend.Models.OrderManufacturer", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<DateTime?>("CreatedAt")
                        .HasColumnType("TEXT");

                    b.Property<bool?>("IsActive")
                        .HasColumnType("INTEGER");

                    b.Property<int?>("ManufacturerId")
                        .HasColumnType("INTEGER");

                    b.Property<int?>("OrderSheetId")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.HasIndex("ManufacturerId");

                    b.HasIndex("OrderSheetId");

                    b.ToTable("orderManufacturers");
                });

            modelBuilder.Entity("TrackBackend.Models.OrderPriority", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<DateTime?>("CreatedAt")
                        .HasColumnType("TEXT");

                    b.Property<double?>("DaysRequiredForFinalTriming")
                        .HasColumnType("REAL");

                    b.Property<double?>("DaysRequiredForFinishing")
                        .HasColumnType("REAL");

                    b.Property<double?>("DaysRequiredForPackaging")
                        .HasColumnType("REAL");

                    b.Property<double?>("DaysRequiredForProcessing")
                        .HasColumnType("REAL");

                    b.Property<double?>("DaysRequiredForShipping")
                        .HasColumnType("REAL");

                    b.Property<double?>("DaysRequiredForTrimming")
                        .HasColumnType("REAL");

                    b.Property<double?>("DaysRequiredForWashing")
                        .HasColumnType("REAL");

                    b.Property<bool?>("IsActive")
                        .HasColumnType("INTEGER");

                    b.Property<double?>("LinesPerDay")
                        .HasColumnType("REAL");

                    b.Property<int?>("ManufacturerId")
                        .HasColumnType("INTEGER");

                    b.Property<string>("PriorityName")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("ManufacturerId");

                    b.ToTable("orderPriorities");
                });

            modelBuilder.Entity("TrackBackend.Models.OrderSheet", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("TEXT");

                    b.Property<string>("CtfUrl")
                        .HasColumnType("TEXT");

                    b.Property<string>("DesignName")
                        .HasColumnType("TEXT");

                    b.Property<string>("ImageUrl")
                        .HasColumnType("TEXT");

                    b.Property<bool?>("IsActive")
                        .HasColumnType("INTEGER");

                    b.Property<bool?>("IsScheduled")
                        .HasColumnType("INTEGER");

                    b.Property<string>("OrderId")
                        .HasColumnType("TEXT");

                    b.Property<string>("PdfUrl")
                        .HasColumnType("TEXT");

                    b.Property<string>("PhysicalHeight")
                        .HasColumnType("TEXT");

                    b.Property<string>("PhysicalWidth")
                        .HasColumnType("TEXT");

                    b.Property<string>("PixelHeight")
                        .HasColumnType("TEXT");

                    b.Property<string>("PixelWidth")
                        .HasColumnType("TEXT");

                    b.Property<string>("Unit")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("orderSheets");
                });

            modelBuilder.Entity("TrackBackend.Models.OrderStage", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<DateTime?>("CreatedAt")
                        .HasColumnType("TEXT");

                    b.Property<string>("FilePath")
                        .HasColumnType("TEXT");

                    b.Property<bool?>("IsActive")
                        .HasColumnType("INTEGER");

                    b.Property<bool?>("IsCompleted")
                        .HasColumnType("INTEGER");

                    b.Property<double?>("LengthCompleted")
                        .HasColumnType("REAL");

                    b.Property<int?>("LengthUnit")
                        .HasColumnType("INTEGER");

                    b.Property<int?>("OrderSheetId")
                        .HasColumnType("INTEGER");

                    b.Property<int>("stage")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.HasIndex("OrderSheetId");

                    b.ToTable("orderStages");
                });

            modelBuilder.Entity("TrackBackend.Models.OrderStatus", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<DateTime?>("CreatedAt")
                        .HasColumnType("TEXT");

                    b.Property<DateTime?>("DeliveryDate")
                        .HasColumnType("TEXT");

                    b.Property<bool?>("IsActive")
                        .HasColumnType("INTEGER");

                    b.Property<int?>("OrderPriorityId")
                        .HasColumnType("INTEGER");

                    b.Property<int?>("OrderSheetId")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.HasIndex("OrderPriorityId");

                    b.HasIndex("OrderSheetId");

                    b.ToTable("orderStatuses");
                });

            modelBuilder.Entity("TrackBackend.Models.OrderManufacturer", b =>
                {
                    b.HasOne("TrackBackend.Models.Manufacturer", "Manufacturer")
                        .WithMany()
                        .HasForeignKey("ManufacturerId");

                    b.HasOne("TrackBackend.Models.OrderSheet", "OrderSheet")
                        .WithMany()
                        .HasForeignKey("OrderSheetId");

                    b.Navigation("Manufacturer");

                    b.Navigation("OrderSheet");
                });

            modelBuilder.Entity("TrackBackend.Models.OrderPriority", b =>
                {
                    b.HasOne("TrackBackend.Models.Manufacturer", "Manufacturer")
                        .WithMany()
                        .HasForeignKey("ManufacturerId");

                    b.Navigation("Manufacturer");
                });

            modelBuilder.Entity("TrackBackend.Models.OrderStage", b =>
                {
                    b.HasOne("TrackBackend.Models.OrderSheet", "OrderSheet")
                        .WithMany()
                        .HasForeignKey("OrderSheetId");

                    b.Navigation("OrderSheet");
                });

            modelBuilder.Entity("TrackBackend.Models.OrderStatus", b =>
                {
                    b.HasOne("TrackBackend.Models.OrderPriority", "OrderPriority")
                        .WithMany()
                        .HasForeignKey("OrderPriorityId");

                    b.HasOne("TrackBackend.Models.OrderSheet", "OrderSheet")
                        .WithMany()
                        .HasForeignKey("OrderSheetId");

                    b.Navigation("OrderPriority");

                    b.Navigation("OrderSheet");
                });
#pragma warning restore 612, 618
        }
    }
}
