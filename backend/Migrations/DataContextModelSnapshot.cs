﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using backend.Helpers;

#nullable disable

namespace backend.Migrations
{
    [DbContext(typeof(DataContext))]
    partial class DataContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.5")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            MySqlModelBuilderExtensions.AutoIncrementColumns(modelBuilder);

            modelBuilder.Entity("backend.Entities.Account", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)");

                    b.Property<Guid?>("AvatarId")
                        .HasColumnType("char(36)");

                    b.Property<DateTime>("Created")
                        .HasColumnType("datetime(6)");

                    b.Property<DateTime?>("DeletedAt")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("Email")
                        .HasColumnType("longtext");

                    b.Property<string>("FullName")
                        .HasColumnType("longtext");

                    b.Property<DateTime?>("LockedUntil")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("PasswordHash")
                        .HasColumnType("longtext");

                    b.Property<DateTime?>("PasswordReset")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("ResetToken")
                        .HasColumnType("varchar(80)");

                    b.Property<DateTime?>("ResetTokenExpires")
                        .HasColumnType("datetime(6)");

                    b.Property<Guid>("RoleId")
                        .HasColumnType("char(36)");

                    b.Property<int>("Status")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasDefaultValue(0);

                    b.Property<DateTime?>("Updated")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("UserName")
                        .HasColumnType("varchar(150)");

                    b.Property<string>("VerificationToken")
                        .HasColumnType("longtext");

                    b.Property<DateTime?>("Verified")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("WhatsappNumber")
                        .HasColumnType("longtext");

                    b.HasKey("Id");

                    b.HasIndex("AvatarId");

                    b.HasIndex("RoleId");

                    b.ToTable("Accounts");

                    b.HasData(
                        new
                        {
                            Id = new Guid("8f8884ac-e5ff-4ab2-92f6-5c328d2f6e97"),
                            Created = new DateTime(2024, 7, 10, 3, 23, 55, 791, DateTimeKind.Utc).AddTicks(1134),
                            Email = "superadmin@admin.com",
                            FullName = "Super Admin",
                            PasswordHash = "$2a$11$ExrErm.mgLQU28It8HOBmOpy5GdcjZJtdGW0yQrALskXRHynLTcD2",
                            RoleId = new Guid("9648fc11-a4bf-40bb-84a2-2925ebaa630d"),
                            Status = 0,
                            UserName = "superadmin",
                            Verified = new DateTime(2024, 7, 10, 3, 23, 55, 791, DateTimeKind.Utc).AddTicks(1128)
                        },
                        new
                        {
                            Id = new Guid("dc323466-0729-4f4c-82c4-43560ae1113c"),
                            Created = new DateTime(2024, 7, 10, 3, 23, 55, 985, DateTimeKind.Utc).AddTicks(6854),
                            Email = "admin@admin.com",
                            FullName = "Admin",
                            PasswordHash = "$2a$11$0uzzRxG6GYhke9qPgo6rh.MMDB7NlGqHNQEO2WlnQJBQelsSG68Uu",
                            RoleId = new Guid("3658f128-c3ac-4bc2-82d5-7b282183721d"),
                            Status = 0,
                            UserName = "admin",
                            Verified = new DateTime(2024, 7, 10, 3, 23, 55, 985, DateTimeKind.Utc).AddTicks(6844)
                        });
                });

            modelBuilder.Entity("backend.Entities.Berita", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)");

                    b.Property<Guid>("AuthorId")
                        .HasColumnType("char(36)");

                    b.Property<DateTime>("Created")
                        .HasColumnType("datetime(6)");

                    b.Property<DateTime?>("DeletedAt")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("Description")
                        .HasColumnType("longtext");

                    b.Property<Guid>("PathId")
                        .HasColumnType("char(36)");

                    b.Property<int>("Type")
                        .HasColumnType("int");

                    b.Property<DateTime?>("Updated")
                        .HasColumnType("datetime(6)");

                    b.HasKey("Id");

                    b.HasIndex("AuthorId");

                    b.HasIndex("PathId");

                    b.ToTable("Beritas");
                });

            modelBuilder.Entity("backend.Entities.Category", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)");

                    b.Property<Guid>("AuthorId")
                        .HasColumnType("char(36)");

                    b.Property<DateTime>("Created")
                        .HasColumnType("datetime(6)");

                    b.Property<DateTime?>("DeletedAt")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("Description")
                        .HasColumnType("longtext");

                    b.Property<string>("Name")
                        .HasColumnType("longtext");

                    b.Property<DateTime?>("Updated")
                        .HasColumnType("datetime(6)");

                    b.HasKey("Id");

                    b.HasIndex("AuthorId");

                    b.ToTable("Categories");
                });

            modelBuilder.Entity("backend.Entities.Media", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)");

                    b.Property<Guid>("AuthorId")
                        .HasColumnType("char(36)");

                    b.Property<DateTime>("Created")
                        .HasColumnType("datetime(6)");

                    b.Property<DateTime?>("DeletedAt")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("ImageBig")
                        .HasColumnType("longtext");

                    b.Property<string>("ImageDefault")
                        .HasColumnType("longtext");

                    b.Property<string>("ImageSmall")
                        .HasColumnType("longtext");

                    b.Property<string>("ImageType")
                        .HasColumnType("longtext");

                    b.Property<DateTime?>("Updated")
                        .HasColumnType("datetime(6)");

                    b.HasKey("Id");

                    b.HasIndex("AuthorId");

                    b.ToTable("Medias");
                });

            modelBuilder.Entity("backend.Entities.NavigationMenu", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)");

                    b.Property<string>("ControllerName")
                        .HasColumnType("varchar(64)");

                    b.Property<DateTime>("Created")
                        .HasColumnType("datetime(6)");

                    b.Property<DateTime?>("DeletedAt")
                        .HasColumnType("datetime(6)");

                    b.Property<Guid?>("GroupId")
                        .HasColumnType("char(36)");

                    b.Property<string>("Name")
                        .HasColumnType("varchar(64)");

                    b.Property<int>("Order")
                        .HasColumnType("int");

                    b.Property<DateTime?>("Updated")
                        .HasColumnType("datetime(6)");

                    b.HasKey("Id");

                    b.HasIndex("GroupId");

                    b.ToTable("NavigationMenus");
                });

            modelBuilder.Entity("backend.Entities.NavigationMenuGroup", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)");

                    b.Property<DateTime>("Created")
                        .HasColumnType("datetime(6)");

                    b.Property<DateTime?>("DeletedAt")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("Name")
                        .HasColumnType("longtext");

                    b.Property<DateTime?>("Updated")
                        .HasColumnType("datetime(6)");

                    b.HasKey("Id");

                    b.ToTable("NavigationMenuGroup");
                });

            modelBuilder.Entity("backend.Entities.Product", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)");

                    b.Property<Guid>("AuthorId")
                        .HasColumnType("char(36)");

                    b.Property<Guid>("CategoryId")
                        .HasColumnType("char(36)");

                    b.Property<DateTime>("Created")
                        .HasColumnType("datetime(6)");

                    b.Property<DateTime?>("DeletedAt")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("Description")
                        .HasColumnType("longtext");

                    b.Property<Guid?>("IconId")
                        .HasColumnType("char(36)");

                    b.Property<string>("Name")
                        .HasColumnType("longtext");

                    b.Property<int>("Status")
                        .HasColumnType("int");

                    b.Property<string>("SubName")
                        .HasColumnType("longtext");

                    b.Property<Guid?>("ThumbnailId")
                        .HasColumnType("char(36)");

                    b.Property<DateTime?>("Updated")
                        .HasColumnType("datetime(6)");

                    b.HasKey("Id");

                    b.HasIndex("AuthorId");

                    b.HasIndex("CategoryId");

                    b.HasIndex("IconId");

                    b.HasIndex("ThumbnailId");

                    b.ToTable("Products");
                });

            modelBuilder.Entity("backend.Entities.ProductItem", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)");

                    b.Property<DateTime>("Created")
                        .HasColumnType("datetime(6)");

                    b.Property<DateTime?>("DeletedAt")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("Name")
                        .HasColumnType("longtext");

                    b.Property<long>("Price")
                        .HasColumnType("bigint");

                    b.Property<Guid>("ProductId")
                        .HasColumnType("char(36)");

                    b.Property<string>("Provider")
                        .HasColumnType("longtext");

                    b.Property<long?>("SalePrice")
                        .HasColumnType("bigint");

                    b.Property<int>("Status")
                        .HasColumnType("int");

                    b.Property<DateTime?>("Updated")
                        .HasColumnType("datetime(6)");

                    b.HasKey("Id");

                    b.HasIndex("ProductId");

                    b.ToTable("ProductItems");
                });

            modelBuilder.Entity("backend.Entities.RefreshToken", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)");

                    b.Property<Guid?>("AccountId")
                        .HasColumnType("char(36)");

                    b.Property<DateTime>("Created")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("CreatedByIp")
                        .HasColumnType("longtext");

                    b.Property<DateTime>("Expires")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("ReplacedByToken")
                        .HasColumnType("longtext");

                    b.Property<DateTime?>("Revoked")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("RevokedByIp")
                        .HasColumnType("longtext");

                    b.Property<string>("Token")
                        .HasColumnType("longtext");

                    b.HasKey("Id");

                    b.HasIndex("AccountId");

                    b.ToTable("RefreshTokens");
                });

            modelBuilder.Entity("backend.Entities.Role", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)");

                    b.Property<DateTime>("Created")
                        .HasColumnType("datetime(6)");

                    b.Property<DateTime?>("DeletedAt")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("Description")
                        .HasColumnType("longtext");

                    b.Property<string>("RoleName")
                        .HasColumnType("varchar(64)");

                    b.Property<DateTime?>("Updated")
                        .HasColumnType("datetime(6)");

                    b.HasKey("Id");

                    b.ToTable("Role");

                    b.HasData(
                        new
                        {
                            Id = new Guid("9648fc11-a4bf-40bb-84a2-2925ebaa630d"),
                            Created = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            RoleName = "Super Admin"
                        },
                        new
                        {
                            Id = new Guid("3658f128-c3ac-4bc2-82d5-7b282183721d"),
                            Created = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            RoleName = "User"
                        });
                });

            modelBuilder.Entity("backend.Entities.RolePermission", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)");

                    b.Property<DateTime>("Created")
                        .HasColumnType("datetime(6)");

                    b.Property<DateTime?>("DeletedAt")
                        .HasColumnType("datetime(6)");

                    b.Property<Guid>("NavigationMenuId")
                        .HasColumnType("char(36)");

                    b.Property<bool>("Permitted")
                        .HasColumnType("tinyint(1)");

                    b.Property<Guid>("RoleId")
                        .HasColumnType("char(36)");

                    b.Property<DateTime?>("Updated")
                        .HasColumnType("datetime(6)");

                    b.HasKey("Id");

                    b.HasIndex("NavigationMenuId");

                    b.HasIndex("RoleId");

                    b.ToTable("RolePermissions");
                });

            modelBuilder.Entity("backend.Entities.Voucher", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)");

                    b.Property<Guid>("AuthorId")
                        .HasColumnType("char(36)");

                    b.Property<string>("Code")
                        .HasColumnType("varchar(150)");

                    b.Property<DateTime>("Created")
                        .HasColumnType("datetime(6)");

                    b.Property<DateTime?>("DeletedAt")
                        .HasColumnType("datetime(6)");

                    b.Property<int>("Percentage")
                        .HasColumnType("int");

                    b.Property<int>("Stock")
                        .HasColumnType("int");

                    b.Property<DateTime?>("Updated")
                        .HasColumnType("datetime(6)");

                    b.HasKey("Id");

                    b.HasIndex("AuthorId");

                    b.ToTable("Vouchers");
                });

            modelBuilder.Entity("backend.Entities.Account", b =>
                {
                    b.HasOne("backend.Entities.Media", "Avatar")
                        .WithMany()
                        .HasForeignKey("AvatarId");

                    b.HasOne("backend.Entities.Role", "Role")
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Avatar");

                    b.Navigation("Role");
                });

            modelBuilder.Entity("backend.Entities.Berita", b =>
                {
                    b.HasOne("backend.Entities.Account", "Author")
                        .WithMany()
                        .HasForeignKey("AuthorId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("backend.Entities.Media", "Path")
                        .WithMany()
                        .HasForeignKey("PathId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Author");

                    b.Navigation("Path");
                });

            modelBuilder.Entity("backend.Entities.Category", b =>
                {
                    b.HasOne("backend.Entities.Account", "Author")
                        .WithMany()
                        .HasForeignKey("AuthorId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Author");
                });

            modelBuilder.Entity("backend.Entities.Media", b =>
                {
                    b.HasOne("backend.Entities.Account", "Author")
                        .WithMany()
                        .HasForeignKey("AuthorId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Author");
                });

            modelBuilder.Entity("backend.Entities.NavigationMenu", b =>
                {
                    b.HasOne("backend.Entities.NavigationMenuGroup", "Group")
                        .WithMany()
                        .HasForeignKey("GroupId");

                    b.Navigation("Group");
                });

            modelBuilder.Entity("backend.Entities.Product", b =>
                {
                    b.HasOne("backend.Entities.Account", "Author")
                        .WithMany()
                        .HasForeignKey("AuthorId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("backend.Entities.Category", "Category")
                        .WithMany("Products")
                        .HasForeignKey("CategoryId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("backend.Entities.Media", "Icon")
                        .WithMany()
                        .HasForeignKey("IconId");

                    b.HasOne("backend.Entities.Media", "Thumbnail")
                        .WithMany()
                        .HasForeignKey("ThumbnailId");

                    b.Navigation("Author");

                    b.Navigation("Category");

                    b.Navigation("Icon");

                    b.Navigation("Thumbnail");
                });

            modelBuilder.Entity("backend.Entities.ProductItem", b =>
                {
                    b.HasOne("backend.Entities.Product", "Product")
                        .WithMany("ProductItems")
                        .HasForeignKey("ProductId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Product");
                });

            modelBuilder.Entity("backend.Entities.RefreshToken", b =>
                {
                    b.HasOne("backend.Entities.Account", "Account")
                        .WithMany("RefreshTokens")
                        .HasForeignKey("AccountId");

                    b.Navigation("Account");
                });

            modelBuilder.Entity("backend.Entities.RolePermission", b =>
                {
                    b.HasOne("backend.Entities.NavigationMenu", "NavigationMenu")
                        .WithMany()
                        .HasForeignKey("NavigationMenuId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("backend.Entities.Role", "Role")
                        .WithMany("Permission")
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("NavigationMenu");

                    b.Navigation("Role");
                });

            modelBuilder.Entity("backend.Entities.Voucher", b =>
                {
                    b.HasOne("backend.Entities.Account", "Author")
                        .WithMany()
                        .HasForeignKey("AuthorId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Author");
                });

            modelBuilder.Entity("backend.Entities.Account", b =>
                {
                    b.Navigation("RefreshTokens");
                });

            modelBuilder.Entity("backend.Entities.Category", b =>
                {
                    b.Navigation("Products");
                });

            modelBuilder.Entity("backend.Entities.Product", b =>
                {
                    b.Navigation("ProductItems");
                });

            modelBuilder.Entity("backend.Entities.Role", b =>
                {
                    b.Navigation("Permission");
                });
#pragma warning restore 612, 618
        }
    }
}
