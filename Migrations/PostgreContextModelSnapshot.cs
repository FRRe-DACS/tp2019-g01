﻿// <auto-generated />
using System;
using GestiónDeMedicamentos.Database;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace GestiónDeMedicamentos.Migrations
{
    [DbContext(typeof(PostgreContext))]
    partial class PostgreContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn)
                .HasAnnotation("ProductVersion", "2.1.11-servicing-32099")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            modelBuilder.Entity("GestiónDeMedicamentos.Models.Drug", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Name");

                    b.HasKey("Id");

                    b.ToTable("Drugs");
                });

            modelBuilder.Entity("GestiónDeMedicamentos.Models.Medicine", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<long?>("DrugId");

                    b.Property<int?>("DrugId1");

                    b.Property<string>("Laboratory");

                    b.Property<string>("Name");

                    b.Property<int>("Presentation");

                    b.Property<decimal>("Proportion");

                    b.Property<long>("Stock");

                    b.HasKey("Id");

                    b.HasIndex("DrugId1");

                    b.ToTable("Medicines");
                });

            modelBuilder.Entity("GestiónDeMedicamentos.Models.MedicinePrescription", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int?>("DrugId");

                    b.Property<int>("MedicineId");

                    b.Property<int>("PrescriptionId");

                    b.Property<long>("Quantity");

                    b.HasKey("Id");

                    b.HasIndex("DrugId");

                    b.HasIndex("MedicineId");

                    b.HasIndex("PrescriptionId");

                    b.ToTable("MedicinePrescriptions");
                });

            modelBuilder.Entity("GestiónDeMedicamentos.Models.MedicinePurchaseOrder", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("MedicineId");

                    b.Property<int>("PurchaseOrderId");

                    b.Property<long>("Quantity");

                    b.HasKey("Id");

                    b.HasIndex("MedicineId");

                    b.HasIndex("PurchaseOrderId");

                    b.ToTable("MedicinePurchaseOrders");
                });

            modelBuilder.Entity("GestiónDeMedicamentos.Models.MedicineStockOrder", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("MedicineId");

                    b.Property<int>("Quantity");

                    b.Property<int>("StockOrderId");

                    b.HasKey("Id");

                    b.HasIndex("MedicineId");

                    b.HasIndex("StockOrderId");

                    b.ToTable("MedicineStockOrders");
                });

            modelBuilder.Entity("GestiónDeMedicamentos.Models.Prescription", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("Date");

                    b.HasKey("Id");

                    b.ToTable("Prescriptions");
                });

            modelBuilder.Entity("GestiónDeMedicamentos.Models.PurchaseOrder", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("Date");

                    b.HasKey("Id");

                    b.ToTable("PurchaseOrders");
                });

            modelBuilder.Entity("GestiónDeMedicamentos.Models.StockOrder", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("Date");

                    b.HasKey("Id");

                    b.ToTable("StockOrders");
                });

            modelBuilder.Entity("GestiónDeMedicamentos.Models.Medicine", b =>
                {
                    b.HasOne("GestiónDeMedicamentos.Models.Drug", "Drug")
                        .WithMany()
                        .HasForeignKey("DrugId1");
                });

            modelBuilder.Entity("GestiónDeMedicamentos.Models.MedicinePrescription", b =>
                {
                    b.HasOne("GestiónDeMedicamentos.Models.Medicine")
                        .WithMany("MedicinePrescriptions")
                        .HasForeignKey("DrugId");

                    b.HasOne("GestiónDeMedicamentos.Models.Medicine", "Medicine")
                        .WithMany()
                        .HasForeignKey("MedicineId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("GestiónDeMedicamentos.Models.Prescription", "Prescription")
                        .WithMany("MedicinePrescriptions")
                        .HasForeignKey("PrescriptionId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("GestiónDeMedicamentos.Models.MedicinePurchaseOrder", b =>
                {
                    b.HasOne("GestiónDeMedicamentos.Models.Medicine", "Medicine")
                        .WithMany("MedicinePurchaseOrders")
                        .HasForeignKey("MedicineId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("GestiónDeMedicamentos.Models.PurchaseOrder", "PurchaseOrder")
                        .WithMany("MedicinePurchaseOrders")
                        .HasForeignKey("PurchaseOrderId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("GestiónDeMedicamentos.Models.MedicineStockOrder", b =>
                {
                    b.HasOne("GestiónDeMedicamentos.Models.Medicine", "Medicine")
                        .WithMany("MedicineStockOrders")
                        .HasForeignKey("MedicineId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("GestiónDeMedicamentos.Models.StockOrder", "StockOrder")
                        .WithMany("MedicineStockOrders")
                        .HasForeignKey("StockOrderId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}
