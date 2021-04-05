﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using buckstore.auth.service.infrastructure.Data.Context;

namespace buckstore.auth.service.infrastructure.Data.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20210405142003_FixAddress")]
    partial class FixAddress
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn)
                .HasAnnotation("ProductVersion", "3.1.5")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            modelBuilder.Entity("buckstore.auth.service.domain.Aggregates.UserAggregate.User", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("Cpf")
                        .HasColumnName("cpf")
                        .HasColumnType("text");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnName("email")
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnName("name")
                        .HasColumnType("text");

                    b.Property<string>("Surname")
                        .IsRequired()
                        .HasColumnName("surname")
                        .HasColumnType("text");

                    b.Property<int>("UserType")
                        .HasColumnName("userType")
                        .HasColumnType("integer");

                    b.Property<string>("_credCard")
                        .HasColumnName("credCard")
                        .HasColumnType("text");

                    b.Property<string>("_password")
                        .HasColumnName("password")
                        .HasColumnType("text");

                    b.Property<byte[]>("_passwordSalt")
                        .HasColumnName("passwordSalt")
                        .HasColumnType("bytea");

                    b.HasKey("Id");

                    b.ToTable("User");
                });

            modelBuilder.Entity("buckstore.auth.service.domain.Aggregates.UserAggregate.User", b =>
                {
                    b.OwnsOne("buckstore.auth.service.domain.Aggregates.UserAggregate.Address", "Address", b1 =>
                        {
                            b1.Property<Guid>("UserId")
                                .HasColumnType("uuid");

                            b1.Property<string>("_city")
                                .HasColumnType("text");

                            b1.Property<string>("_district")
                                .HasColumnType("text");

                            b1.Property<string>("_state")
                                .HasColumnType("text");

                            b1.Property<string>("_street")
                                .HasColumnType("text");

                            b1.Property<string>("_zipCode")
                                .HasColumnType("text");

                            b1.HasKey("UserId");

                            b1.ToTable("User");

                            b1.WithOwner()
                                .HasForeignKey("UserId");
                        });
                });
#pragma warning restore 612, 618
        }
    }
}
