﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Notebook.Infrastructure.Data;

namespace Notebook.Infrastructure.Data.Migrations
{
    [DbContext(typeof(NotebookContext))]
    partial class NotebookContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.7")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Notebook.Calendar.Domain.CalendarEvents.CalendarEvent", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("DateStart")
                        .HasColumnType("datetime2");

                    b.Property<string>("Discriminator")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("Done")
                        .HasColumnType("bit");

                    b.Property<string>("Subject")
                        .IsRequired()
                        .HasMaxLength(120)
                        .HasColumnType("nvarchar(120)");

                    b.HasKey("Id");

                    b.ToTable("CalendarEvents");

                    b.HasDiscriminator<string>("Discriminator").HasValue("CalendarEvent");
                });

            modelBuilder.Entity("Notebook.Contacts.Domain.Contact", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("Birthday")
                        .HasColumnType("datetime2");

                    b.Property<string>("Company")
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("MiddleName")
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("Position")
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("Surname")
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("Id");

                    b.ToTable("Contacts");
                });

            modelBuilder.Entity("Notebook.Contacts.Domain.ContactInfo", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("ContactId")
                        .HasColumnType("int");

                    b.Property<int>("ContactType")
                        .HasColumnType("int");

                    b.Property<string>("Value")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("ContactId");

                    b.ToTable("ContactInfo");
                });

            modelBuilder.Entity("Notebook.Calendar.Domain.CalendarEvents.Meeting", b =>
                {
                    b.HasBaseType("Notebook.Calendar.Domain.CalendarEvents.CalendarEvent");

                    b.Property<DateTime>("DateEnd")
                        .HasColumnType("datetime2");

                    b.Property<string>("Place")
                        .IsRequired()
                        .HasMaxLength(120)
                        .HasColumnType("nvarchar(120)");

                    b.ToTable("CalendarEvents");

                    b.HasDiscriminator().HasValue("Meeting");
                });

            modelBuilder.Entity("Notebook.Calendar.Domain.CalendarEvents.Memo", b =>
                {
                    b.HasBaseType("Notebook.Calendar.Domain.CalendarEvents.CalendarEvent");

                    b.ToTable("CalendarEvents");

                    b.HasDiscriminator().HasValue("Memo");
                });

            modelBuilder.Entity("Notebook.Calendar.Domain.CalendarEvents.Work", b =>
                {
                    b.HasBaseType("Notebook.Calendar.Domain.CalendarEvents.CalendarEvent");

                    b.Property<DateTime>("DateEnd")
                        .HasColumnType("datetime2")
                        .HasColumnName("Work_DateEnd");

                    b.ToTable("CalendarEvents");

                    b.HasDiscriminator().HasValue("Work");
                });

            modelBuilder.Entity("Notebook.Contacts.Domain.ContactInfo", b =>
                {
                    b.HasOne("Notebook.Contacts.Domain.Contact", "Contact")
                        .WithMany("ContactInformation")
                        .HasForeignKey("ContactId");

                    b.Navigation("Contact");
                });

            modelBuilder.Entity("Notebook.Contacts.Domain.Contact", b =>
                {
                    b.Navigation("ContactInformation");
                });
#pragma warning restore 612, 618
        }
    }
}
