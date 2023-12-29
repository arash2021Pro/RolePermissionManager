﻿// <auto-generated />
using System;
using CoreStorage.AppContext;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace CoreStorage.Migrations
{
    [DbContext(typeof(ApplicationContext))]
    [Migration("20231211174732_Token")]
    partial class Token
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.14")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("CoreBussiness.BussinessEntity.Permissions.Permission", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("ActionName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ControllerName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTimeOffset?>("CreationTimeOffset")
                        .HasColumnType("datetimeoffset");

                    b.Property<bool>("IsActive")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bit")
                        .HasDefaultValue(true);

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<DateTimeOffset?>("ModificationTime")
                        .HasColumnType("datetimeoffset");

                    b.Property<string>("PermissionName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("RoleId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("Permissions");
                });

            modelBuilder.Entity("CoreBussiness.BussinessEntity.Roles.Role", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateTimeOffset?>("CreationTimeOffset")
                        .HasColumnType("datetimeoffset");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<DateTimeOffset?>("ModificationTime")
                        .HasColumnType("datetimeoffset");

                    b.Property<string>("RoleName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("RoleStatus")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("Roles");
                });

            modelBuilder.Entity("CoreBussiness.BussinessEntity.Users.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateTimeOffset?>("CreationTimeOffset")
                        .HasColumnType("datetimeoffset");

                    b.Property<int?>("CreatorId")
                        .HasColumnType("int");

                    b.Property<string>("Email")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FullName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<bool>("IsPin")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bit")
                        .HasDefaultValue(false);

                    b.Property<DateTimeOffset?>("ModificationTime")
                        .HasColumnType("datetimeoffset");

                    b.Property<string>("NationalCode")
                        .HasMaxLength(10)
                        .HasColumnType("nvarchar(10)");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PhoneNumber")
                        .HasMaxLength(11)
                        .HasColumnType("nvarchar(11)");

                    b.Property<int>("RoleId")
                        .HasColumnType("int");

                    b.Property<string>("Serial")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Token")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("UserStatus")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("CreatorId");

                    b.HasIndex("RoleId");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("CoreBussiness.BussinessEntity.Permissions.Permission", b =>
                {
                    b.HasOne("CoreBussiness.BussinessEntity.Roles.Role", "Role")
                        .WithMany("Permissions")
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Role");
                });

            modelBuilder.Entity("CoreBussiness.BussinessEntity.Users.User", b =>
                {
                    b.HasOne("CoreBussiness.BussinessEntity.Users.User", "Creator")
                        .WithMany("Users")
                        .HasForeignKey("CreatorId");

                    b.HasOne("CoreBussiness.BussinessEntity.Roles.Role", "Role")
                        .WithMany("Users")
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Creator");

                    b.Navigation("Role");
                });

            modelBuilder.Entity("CoreBussiness.BussinessEntity.Roles.Role", b =>
                {
                    b.Navigation("Permissions");

                    b.Navigation("Users");
                });

            modelBuilder.Entity("CoreBussiness.BussinessEntity.Users.User", b =>
                {
                    b.Navigation("Users");
                });
#pragma warning restore 612, 618
        }
    }
}
