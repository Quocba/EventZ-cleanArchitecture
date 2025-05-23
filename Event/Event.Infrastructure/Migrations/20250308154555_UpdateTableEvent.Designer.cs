﻿// <auto-generated />
using System;
using Event.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Event.Infrastructure.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20250308154555_UpdateTableEvent")]
    partial class UpdateTableEvent
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.13")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("Event.Domain.Entities.EventBooking", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("id");

                    b.Property<string>("Additional_Info")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("additional_info");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2")
                        .HasColumnName("created_at");

                    b.Property<Guid>("EventSeatID")
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("event_seat_id");

                    b.Property<bool>("IsActive")
                        .HasColumnType("bit")
                        .HasColumnName("is_active");

                    b.Property<int>("NumSeat")
                        .HasColumnType("int")
                        .HasColumnName("num_seats");

                    b.Property<Guid>("PaymentID")
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("payment_id");

                    b.Property<int>("Status")
                        .HasColumnType("int")
                        .HasColumnName("status");

                    b.Property<DateTime>("UpdatedAt")
                        .HasColumnType("datetime2")
                        .HasColumnName("updated_at");

                    b.Property<Guid>("UserID")
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("user_id");

                    b.HasKey("Id");

                    b.HasIndex("EventSeatID");

                    b.ToTable("event_booking");
                });

            modelBuilder.Entity("Event.Domain.Entities.EventDocuments", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("id");

                    b.Property<string>("Content")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("content");

                    b.Property<int>("DocumentsType")
                        .HasColumnType("int")
                        .HasColumnName("documents_type");

                    b.Property<Guid>("EventID")
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("event_id");

                    b.Property<string>("LinkDocument")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("link_document");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)")
                        .HasColumnName("title");

                    b.HasKey("Id");

                    b.HasIndex("EventID");

                    b.ToTable("event_documents");
                });

            modelBuilder.Entity("Event.Domain.Entities.EventImages", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("id");

                    b.Property<Guid>("EventID")
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("event_id");

                    b.Property<int>("ImageType")
                        .HasMaxLength(255)
                        .HasColumnType("int")
                        .HasColumnName("image_type");

                    b.Property<string>("ImageUrl")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)")
                        .HasColumnName("image_url");

                    b.HasKey("Id");

                    b.HasIndex("EventID");

                    b.ToTable("event_images");
                });

            modelBuilder.Entity("Event.Domain.Entities.EventInvite", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("id");

                    b.Property<string>("Content")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("content");

                    b.Property<DateTime>("CreateAt")
                        .HasColumnType("datetime2")
                        .HasColumnName("create_at");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)")
                        .HasColumnName("email");

                    b.Property<Guid>("EventID")
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("event_id");

                    b.Property<bool>("IsChecked")
                        .HasColumnType("bit")
                        .HasColumnName("is_checked");

                    b.Property<bool>("IsConfirm")
                        .HasColumnType("bit")
                        .HasColumnName("is_confirm");

                    b.Property<string>("Phone")
                        .IsRequired()
                        .HasMaxLength(11)
                        .HasColumnType("nvarchar(11)")
                        .HasColumnName("phone");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)")
                        .HasColumnName("title");

                    b.Property<DateTime>("UpdateAt")
                        .HasColumnType("datetime2")
                        .HasColumnName("update_at");

                    b.HasKey("Id");

                    b.HasIndex("EventID");

                    b.ToTable("event_invite");
                });

            modelBuilder.Entity("Event.Domain.Entities.EventLayout", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("id");

                    b.Property<Guid>("EventID")
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("event_id");

                    b.Property<Guid>("LayoutID")
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("layout_id");

                    b.HasKey("Id");

                    b.HasIndex("EventID");

                    b.HasIndex("LayoutID");

                    b.ToTable("event_layout");
                });

            modelBuilder.Entity("Event.Domain.Entities.EventPackageRegistrations", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("id");

                    b.Property<DateTime>("CreateAt")
                        .HasColumnType("datetime2")
                        .HasColumnName("create_at");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)")
                        .HasColumnName("email");

                    b.Property<Guid>("EventPackageID")
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("event_package_id");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)")
                        .HasColumnName("name");

                    b.Property<string>("Phone")
                        .IsRequired()
                        .HasMaxLength(11)
                        .HasColumnType("nvarchar(11)")
                        .HasColumnName("phone");

                    b.Property<double>("Price")
                        .HasColumnType("float")
                        .HasColumnName("price");

                    b.Property<int>("Status")
                        .HasColumnType("int")
                        .HasColumnName("status");

                    b.Property<DateTime>("UpdateAt")
                        .HasColumnType("datetime2")
                        .HasColumnName("update_at");

                    b.HasKey("Id");

                    b.HasIndex("EventPackageID");

                    b.ToTable("event_package_registrations");
                });

            modelBuilder.Entity("Event.Domain.Entities.EventPackages", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("id");

                    b.Property<string>("Benefit")
                        .IsRequired()
                        .HasColumnType("NVARCHAR(MAX)")
                        .HasColumnName("benifit");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)")
                        .HasColumnName("name");

                    b.Property<double>("SalePrice")
                        .HasColumnType("float")
                        .HasColumnName("sale_price");

                    b.Property<double>("SellPrice")
                        .HasColumnType("float")
                        .HasColumnName("sell_price");

                    b.HasKey("Id");

                    b.ToTable("event_packages");
                });

            modelBuilder.Entity("Event.Domain.Entities.EventPayment", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("id");

                    b.Property<Guid>("EventID")
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("event_id");

                    b.Property<Guid>("PaymentHistoryID")
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("payment_history_id");

                    b.HasKey("Id");

                    b.HasIndex("EventID");

                    b.ToTable("event_payment");
                });

            modelBuilder.Entity("Event.Domain.Entities.EventRegistrationLink", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("id");

                    b.Property<string>("Code")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)")
                        .HasColumnName("code");

                    b.Property<DateTime>("EndDate")
                        .HasColumnType("datetime2")
                        .HasColumnName("end_date");

                    b.Property<Guid>("EventID")
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("event_id");

                    b.Property<DateTime>("StartDate")
                        .HasColumnType("datetime2")
                        .HasColumnName("start_date");

                    b.HasKey("Id");

                    b.HasIndex("EventID");

                    b.ToTable("event_registration_link");
                });

            modelBuilder.Entity("Event.Domain.Entities.EventSeats", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("id");

                    b.Property<int>("AvailableSeats")
                        .HasColumnType("int")
                        .HasColumnName("available_seats");

                    b.Property<int>("ColNumber")
                        .HasColumnType("int")
                        .HasColumnName("col_number");

                    b.Property<Guid>("EventLayoutID")
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("event_layout_id");

                    b.Property<bool>("IsFree")
                        .HasColumnType("bit")
                        .HasColumnName("is_free");

                    b.Property<bool>("NeedAccept")
                        .HasColumnType("bit")
                        .HasColumnName("need_accept");

                    b.Property<double>("Price")
                        .HasColumnType("float")
                        .HasColumnName("price");

                    b.Property<int>("RowNumber")
                        .HasColumnType("int")
                        .HasColumnName("row_number");

                    b.Property<string>("SeatLabel")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("nvarchar(20)")
                        .HasColumnName("seat_label");

                    b.Property<int>("SeatType")
                        .HasColumnType("int")
                        .HasColumnName("seat_type");

                    b.Property<string>("Status")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("status");

                    b.Property<int>("TotalSeats")
                        .HasColumnType("int")
                        .HasColumnName("total_seats");

                    b.HasKey("Id");

                    b.HasIndex("EventLayoutID");

                    b.ToTable("event_seats");
                });

            modelBuilder.Entity("Event.Domain.Entities.EventTimeLine", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("id");

                    b.Property<string>("Content")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)")
                        .HasColumnName("content");

                    b.Property<DateTime>("EndDate")
                        .HasColumnType("datetime2")
                        .HasColumnName("end_date");

                    b.Property<Guid>("EventID")
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("event_id");

                    b.Property<Guid>("ParentID")
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("parent_id");

                    b.Property<DateTime>("StartDate")
                        .HasColumnType("datetime2")
                        .HasColumnName("start_date");

                    b.Property<int>("Status")
                        .HasColumnType("int")
                        .HasColumnName("status");

                    b.Property<int>("TimeLineType")
                        .HasColumnType("int")
                        .HasColumnName("timeline_type");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)")
                        .HasColumnName("title");

                    b.HasKey("Id");

                    b.HasIndex("EventID");

                    b.ToTable("event_timeline");
                });

            modelBuilder.Entity("Event.Domain.Entities.EventTimeLineUser", b =>
                {
                    b.Property<Guid>("UserID")
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("user_id");

                    b.Property<Guid>("TimeLineID")
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("time_line_id");

                    b.HasKey("UserID", "TimeLineID");

                    b.HasIndex("TimeLineID");

                    b.ToTable("event_user_time_line");
                });

            modelBuilder.Entity("Event.Domain.Entities.EventType", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("id");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)")
                        .HasColumnName("description");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)")
                        .HasColumnName("name");

                    b.HasKey("Id");

                    b.ToTable("event_type");
                });

            modelBuilder.Entity("Event.Domain.Entities.Events", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("id");

                    b.Property<string>("AdditionalInfo")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)")
                        .HasColumnName("additional_info");

                    b.Property<string>("Address")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)")
                        .HasColumnName("address");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)")
                        .HasColumnName("description");

                    b.Property<DateTime>("EndTime")
                        .HasColumnType("datetime2")
                        .HasColumnName("end_time");

                    b.Property<Guid>("EventTypeID")
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("event_type_id");

                    b.Property<bool>("IsActive")
                        .HasColumnType("bit")
                        .HasColumnName("is_active");

                    b.Property<bool>("IsFree")
                        .HasColumnType("bit")
                        .HasColumnName("is_free");

                    b.Property<bool>("IsOpenLayout")
                        .HasColumnType("bit")
                        .HasColumnName("is_open_layout");

                    b.Property<int>("NumberOfGuest")
                        .HasColumnType("int")
                        .HasColumnName("number_of_guest");

                    b.Property<double>("Price")
                        .HasColumnType("float")
                        .HasColumnName("price");

                    b.Property<string>("Province")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)")
                        .HasColumnName("province");

                    b.Property<DateTime>("StartTime")
                        .HasColumnType("datetime2")
                        .HasColumnName("start_time");

                    b.Property<int>("Status")
                        .HasColumnType("int")
                        .HasColumnName("status");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)")
                        .HasColumnName("title");

                    b.Property<Guid>("UserID")
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("user_id");

                    b.HasKey("Id");

                    b.HasIndex("EventTypeID");

                    b.ToTable("events");
                });

            modelBuilder.Entity("Event.Domain.Entities.Layout", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("id");

                    b.Property<int>("Cols")
                        .HasColumnType("int")
                        .HasColumnName("cols");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2")
                        .HasColumnName("created_at");

                    b.Property<string>("CreatedBy")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)")
                        .HasColumnName("created_by");

                    b.Property<int>("LayoutFloorNumber")
                        .HasColumnType("int")
                        .HasColumnName("layout_floor_number");

                    b.Property<string>("LayoutName")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)")
                        .HasColumnName("layout_name");

                    b.Property<int>("LayoutType")
                        .HasColumnType("int")
                        .HasColumnName("layout_type");

                    b.Property<int>("Rows")
                        .HasColumnType("int")
                        .HasColumnName("rows");

                    b.HasKey("Id");

                    b.ToTable("layout");
                });

            modelBuilder.Entity("Event.Domain.Entities.EventBooking", b =>
                {
                    b.HasOne("Event.Domain.Entities.EventSeats", "EventSeats")
                        .WithMany()
                        .HasForeignKey("EventSeatID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("EventSeats");
                });

            modelBuilder.Entity("Event.Domain.Entities.EventDocuments", b =>
                {
                    b.HasOne("Event.Domain.Entities.Events", "Events")
                        .WithMany()
                        .HasForeignKey("EventID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Events");
                });

            modelBuilder.Entity("Event.Domain.Entities.EventImages", b =>
                {
                    b.HasOne("Event.Domain.Entities.Events", "Events")
                        .WithMany("EventImages")
                        .HasForeignKey("EventID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Events");
                });

            modelBuilder.Entity("Event.Domain.Entities.EventInvite", b =>
                {
                    b.HasOne("Event.Domain.Entities.Events", "Events")
                        .WithMany()
                        .HasForeignKey("EventID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Events");
                });

            modelBuilder.Entity("Event.Domain.Entities.EventLayout", b =>
                {
                    b.HasOne("Event.Domain.Entities.Events", "Events")
                        .WithMany()
                        .HasForeignKey("EventID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Event.Domain.Entities.Layout", "Layout")
                        .WithMany()
                        .HasForeignKey("LayoutID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Events");

                    b.Navigation("Layout");
                });

            modelBuilder.Entity("Event.Domain.Entities.EventPackageRegistrations", b =>
                {
                    b.HasOne("Event.Domain.Entities.EventPackages", "EventPackage")
                        .WithMany()
                        .HasForeignKey("EventPackageID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("EventPackage");
                });

            modelBuilder.Entity("Event.Domain.Entities.EventPayment", b =>
                {
                    b.HasOne("Event.Domain.Entities.Events", "Events")
                        .WithMany()
                        .HasForeignKey("EventID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Events");
                });

            modelBuilder.Entity("Event.Domain.Entities.EventRegistrationLink", b =>
                {
                    b.HasOne("Event.Domain.Entities.Events", "Events")
                        .WithMany("EventRegistrationLinks")
                        .HasForeignKey("EventID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Events");
                });

            modelBuilder.Entity("Event.Domain.Entities.EventSeats", b =>
                {
                    b.HasOne("Event.Domain.Entities.EventLayout", "EventLayout")
                        .WithMany()
                        .HasForeignKey("EventLayoutID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("EventLayout");
                });

            modelBuilder.Entity("Event.Domain.Entities.EventTimeLine", b =>
                {
                    b.HasOne("Event.Domain.Entities.Events", "Events")
                        .WithMany()
                        .HasForeignKey("EventID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Events");
                });

            modelBuilder.Entity("Event.Domain.Entities.EventTimeLineUser", b =>
                {
                    b.HasOne("Event.Domain.Entities.EventTimeLine", "EventTimeLine")
                        .WithMany()
                        .HasForeignKey("TimeLineID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("EventTimeLine");
                });

            modelBuilder.Entity("Event.Domain.Entities.Events", b =>
                {
                    b.HasOne("Event.Domain.Entities.EventType", "EventType")
                        .WithMany()
                        .HasForeignKey("EventTypeID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("EventType");
                });

            modelBuilder.Entity("Event.Domain.Entities.Events", b =>
                {
                    b.Navigation("EventImages");

                    b.Navigation("EventRegistrationLinks");
                });
#pragma warning restore 612, 618
        }
    }
}
