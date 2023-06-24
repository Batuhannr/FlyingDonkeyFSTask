﻿// <auto-generated />
using FylingDonkeyFSTask.DataAccess;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace FylingDonkeyFSTask.DataAccess.Migrations
{
    [DbContext(typeof(FylingDonkeyFSTaskContext))]
    [Migration("20230622180119_first_mig")]
    partial class first_mig
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.3")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("FylingDonkeyFSTask.Entities.Models.Tag", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<bool>("Deleted")
                        .HasColumnType("boolean");

                    b.Property<string>("Name")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Tag");
                });

            modelBuilder.Entity("FylingDonkeyFSTask.Entities.Models.Todos", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<bool>("Deleted")
                        .HasColumnType("boolean");

                    b.Property<string>("Explanation")
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Todos");
                });

            modelBuilder.Entity("FylingDonkeyFSTask.Entities.Models.TodosTags", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<bool>("Deleted")
                        .HasColumnType("boolean");

                    b.Property<int>("TagId")
                        .HasColumnType("integer");

                    b.Property<int>("TodoId")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("TagId");

                    b.HasIndex("TodoId");

                    b.ToTable("TodoTags");
                });

            modelBuilder.Entity("FylingDonkeyFSTask.Entities.Models.TodosTags", b =>
                {
                    b.HasOne("FylingDonkeyFSTask.Entities.Models.Tag", "Tag")
                        .WithMany("TodosTags")
                        .HasForeignKey("TagId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("FylingDonkeyFSTask.Entities.Models.Todos", "Todo")
                        .WithMany("TodosTags")
                        .HasForeignKey("TodoId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Tag");

                    b.Navigation("Todo");
                });

            modelBuilder.Entity("FylingDonkeyFSTask.Entities.Models.Tag", b =>
                {
                    b.Navigation("TodosTags");
                });

            modelBuilder.Entity("FylingDonkeyFSTask.Entities.Models.Todos", b =>
                {
                    b.Navigation("TodosTags");
                });
#pragma warning restore 612, 618
        }
    }
}
