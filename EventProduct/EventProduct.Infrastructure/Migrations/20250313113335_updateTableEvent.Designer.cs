﻿// <auto-generated />
using System;
using EventProduct.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace EventProduct.Infrastructure.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20250313113335_updateTableEvent")]
    partial class updateTableEvent
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.13")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("EventProduct.Domain.Entities.EventCategory", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("id");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("description");

                    b.Property<Guid>("EventID")
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("event_id");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)")
                        .HasColumnName("name");

                    b.HasKey("Id");

                    b.ToTable("event_category");
                });

            modelBuilder.Entity("EventProduct.Domain.Entities.EventOrder", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("id");

                    b.Property<string>("Address")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("address");

                    b.Property<DateTime>("CreateAt")
                        .HasColumnType("datetime2")
                        .HasColumnName("created_at");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("email");

                    b.Property<Guid>("EventID")
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("event_id");

                    b.Property<string>("FullName")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)")
                        .HasColumnName("fullname");

                    b.Property<Guid>("PaymentHistoryID")
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("payment_history_id");

                    b.Property<string>("Phone")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("phone");

                    b.Property<double>("TotalPrice")
                        .HasColumnType("float")
                        .HasColumnName("total_price");

                    b.Property<DateTime>("UpdateAt")
                        .HasColumnType("datetime2")
                        .HasColumnName("update_at");

                    b.Property<Guid>("UserID")
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("user_id");

                    b.HasKey("Id");

                    b.ToTable("event_order");
                });

            modelBuilder.Entity("EventProduct.Domain.Entities.EventOrderProduct", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("id");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2")
                        .HasColumnName("created_at");

                    b.Property<Guid>("EventOrderID")
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("event_order_id");

                    b.Property<double>("Price")
                        .HasColumnType("float")
                        .HasColumnName("price");

                    b.Property<Guid>("ProductID")
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("product_id");

                    b.Property<int>("Quantity")
                        .HasColumnType("int")
                        .HasColumnName("quantity");

                    b.Property<DateTime>("UpdatdAt")
                        .HasColumnType("datetime2")
                        .HasColumnName("update_at");

                    b.HasKey("Id");

                    b.HasIndex("EventOrderID");

                    b.HasIndex("ProductID");

                    b.ToTable("event_order_product");
                });

            modelBuilder.Entity("EventProduct.Domain.Entities.EventProduct", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("id");

                    b.Property<Guid>("CategoryID")
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("category_id");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("description");

                    b.Property<string>("ImageURL")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("image_url");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)")
                        .HasColumnName("name");

                    b.Property<double>("SalePrice")
                        .HasColumnType("float")
                        .HasColumnName("sale_price");

                    b.Property<double>("SellPrice")
                        .HasColumnType("float")
                        .HasColumnName("sell_price");

                    b.Property<int>("Stock")
                        .HasColumnType("int")
                        .HasColumnName("stock");

                    b.Property<string>("ThumbnaiURL")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("thumbnai_url");

                    b.HasKey("Id");

                    b.HasIndex("CategoryID");

                    b.ToTable("event_product");
                });

            modelBuilder.Entity("EventProduct.Domain.Entities.EventOrderProduct", b =>
                {
                    b.HasOne("EventProduct.Domain.Entities.EventOrder", "EventOrder")
                        .WithMany()
                        .HasForeignKey("EventOrderID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("EventProduct.Domain.Entities.EventProduct", "Product")
                        .WithMany()
                        .HasForeignKey("ProductID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("EventOrder");

                    b.Navigation("Product");
                });

            modelBuilder.Entity("EventProduct.Domain.Entities.EventProduct", b =>
                {
                    b.HasOne("EventProduct.Domain.Entities.EventCategory", "Category")
                        .WithMany()
                        .HasForeignKey("CategoryID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Category");
                });
#pragma warning restore 612, 618
        }
    }
}
