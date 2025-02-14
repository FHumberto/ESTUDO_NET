﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using T_Tier.DAL.Context;

#nullable disable

namespace T_Tier.DAL.Migrations
{
    [DbContext(typeof(AppDbContext))]
    [Migration("20250214092542_AddRolesMigration")]
    partial class AddRolesMigration
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "9.0.2")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRole", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("NormalizedName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasDatabaseName("RoleNameIndex")
                        .HasFilter("[NormalizedName] IS NOT NULL");

                    b.ToTable("AspNetRoles", "Identity");

                    b.HasData(
                        new
                        {
                            Id = "1",
                            Name = "Admin",
                            NormalizedName = "ADMIN"
                        },
                        new
                        {
                            Id = "2",
                            Name = "Default",
                            NormalizedName = "DEFAULT"
                        });
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("RoleId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims", "Identity");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims", "Identity");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.Property<string>("LoginProvider")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ProviderKey")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ProviderDisplayName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins", "Identity");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("RoleId")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetUserRoles", "Identity");

                    b.HasData(
                        new
                        {
                            UserId = "e3d46b61-39c2-4fd3-b36b-2a30c2c2c3e9",
                            RoleId = "1"
                        },
                        new
                        {
                            UserId = "c7d96d38-45b1-4a3a-8a4d-746e4c929f64",
                            RoleId = "2"
                        },
                        new
                        {
                            UserId = "b2fcd97a-91b4-43a0-b55f-d1d5e7a8b7b5",
                            RoleId = "2"
                        });
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("LoginProvider")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Value")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens", "Identity");
                });

            modelBuilder.Entity("PostTag", b =>
                {
                    b.Property<int>("PostId")
                        .HasColumnType("int");

                    b.Property<int>("TagId")
                        .HasColumnType("int");

                    b.HasKey("PostId", "TagId");

                    b.HasIndex("TagId");

                    b.ToTable("PostTag", "blog");

                    b.HasData(
                        new
                        {
                            PostId = 1,
                            TagId = 1
                        },
                        new
                        {
                            PostId = 1,
                            TagId = 2
                        },
                        new
                        {
                            PostId = 2,
                            TagId = 2
                        },
                        new
                        {
                            PostId = 2,
                            TagId = 3
                        },
                        new
                        {
                            PostId = 3,
                            TagId = 1
                        },
                        new
                        {
                            PostId = 3,
                            TagId = 3
                        });
                });

            modelBuilder.Entity("T_Tier.DAL.Entities.Comment", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Body")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<DateTime?>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("CreatedBy")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("IsDeleted")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bit")
                        .HasDefaultValue(false);

                    b.Property<int>("PostId")
                        .HasColumnType("int");

                    b.Property<DateTime?>("UpdatedAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("UpdatedBy")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("PostId");

                    b.HasIndex("UserId");

                    b.ToTable("Comments", "blog");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Body = "Este é o primeiro comentário.",
                            IsDeleted = false,
                            PostId = 1,
                            UserId = "e3d46b61-39c2-4fd3-b36b-2a30c2c2c3e9"
                        },
                        new
                        {
                            Id = 2,
                            Body = "Este é o segundo comentário.",
                            IsDeleted = false,
                            PostId = 2,
                            UserId = "c7d96d38-45b1-4a3a-8a4d-746e4c929f64"
                        },
                        new
                        {
                            Id = 3,
                            Body = "Este é o terceiro comentário.",
                            IsDeleted = false,
                            PostId = 3,
                            UserId = "b2fcd97a-91b4-43a0-b55f-d1d5e7a8b7b5"
                        });
                });

            modelBuilder.Entity("T_Tier.DAL.Entities.Post", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Body")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<DateTime?>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("CreatedBy")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("IsDeleted")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bit")
                        .HasDefaultValue(false);

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<DateTime?>("UpdatedAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("UpdatedBy")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("Posts", "blog");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Body = "Este é o corpo do primeiro post.",
                            IsDeleted = false,
                            Title = "Primeiro Post",
                            UserId = "e3d46b61-39c2-4fd3-b36b-2a30c2c2c3e9"
                        },
                        new
                        {
                            Id = 2,
                            Body = "Este é o corpo do segundo post.",
                            IsDeleted = false,
                            Title = "Segundo Post",
                            UserId = "c7d96d38-45b1-4a3a-8a4d-746e4c929f64"
                        },
                        new
                        {
                            Id = 3,
                            Body = "Este é o corpo do terceiro post.",
                            IsDeleted = false,
                            Title = "Terceiro Post",
                            UserId = "b2fcd97a-91b4-43a0-b55f-d1d5e7a8b7b5"
                        });
                });

            modelBuilder.Entity("T_Tier.DAL.Entities.Tag", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateTime?>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("CreatedBy")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<DateTime?>("UpdatedAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("UpdatedBy")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("Name")
                        .IsUnique();

                    b.ToTable("Tags", "blog");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Name = "Tecnologia"
                        },
                        new
                        {
                            Id = 2,
                            Name = "Educação"
                        },
                        new
                        {
                            Id = 3,
                            Name = "Saúde"
                        });
                });

            modelBuilder.Entity("T_Tier.DAL.Entities.User", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("AccessFailedCount")
                        .HasColumnType("int");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Email")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<bool>("EmailConfirmed")
                        .HasColumnType("bit");

                    b.Property<string>("FirstName")
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<string>("LastName")
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<bool>("LockoutEnabled")
                        .HasColumnType("bit");

                    b.Property<DateTimeOffset?>("LockoutEnd")
                        .HasColumnType("datetimeoffset");

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("PasswordHash")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("PhoneNumberConfirmed")
                        .HasColumnType("bit");

                    b.Property<string>("SecurityStamp")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("TwoFactorEnabled")
                        .HasColumnType("bit");

                    b.Property<string>("UserName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.HasKey("Id");

                    b.HasIndex("Email")
                        .IsUnique()
                        .HasFilter("[Email] IS NOT NULL");

                    b.HasIndex("NormalizedEmail")
                        .HasDatabaseName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasDatabaseName("UserNameIndex")
                        .HasFilter("[NormalizedUserName] IS NOT NULL");

                    b.ToTable("AspNetUsers", "Identity");

                    b.HasData(
                        new
                        {
                            Id = "e3d46b61-39c2-4fd3-b36b-2a30c2c2c3e9",
                            AccessFailedCount = 0,
                            ConcurrencyStamp = "456c8374-f1fd-4c50-847a-f53164c90dd1",
                            Email = "joao.silva@example.com",
                            EmailConfirmed = false,
                            FirstName = "João",
                            IsDeleted = false,
                            LastName = "Silva",
                            LockoutEnabled = false,
                            NormalizedEmail = "JOAO.SILVA@EXAMPLE.COM",
                            NormalizedUserName = "JOAO.SILVA",
                            PasswordHash = "AQAAAAIAAYagAAAAEPQUluQb4q+fxuuFSGlMfZEitwtzfGh5lPE1iEE+lD9iFrZeVax6KZcCypY47z4JVg==",
                            PhoneNumberConfirmed = false,
                            SecurityStamp = "37d0bf1a-62e1-4819-b524-25ad0dec7ccd",
                            TwoFactorEnabled = false,
                            UserName = "joao.silva"
                        },
                        new
                        {
                            Id = "c7d96d38-45b1-4a3a-8a4d-746e4c929f64",
                            AccessFailedCount = 0,
                            ConcurrencyStamp = "b8d057df-8c19-4a31-ae22-d7e53f49b701",
                            Email = "maria.oliveira@example.com",
                            EmailConfirmed = false,
                            FirstName = "Maria",
                            IsDeleted = false,
                            LastName = "Oliveira",
                            LockoutEnabled = false,
                            NormalizedEmail = "MARIA.OLIVEIRA@EXAMPLE.COM",
                            NormalizedUserName = "MARIA.OLIVEIRA",
                            PasswordHash = "AQAAAAIAAYagAAAAEIeVFwJi0cMmkw5L+QX9HfNlzwI5Lzx/YXy6Ab3E6kMAVqgs5il7dquu/CMMOwpXZw==",
                            PhoneNumberConfirmed = false,
                            SecurityStamp = "7ad99721-76e2-44a4-b9ae-97211b27e487",
                            TwoFactorEnabled = false,
                            UserName = "maria.oliveira"
                        },
                        new
                        {
                            Id = "b2fcd97a-91b4-43a0-b55f-d1d5e7a8b7b5",
                            AccessFailedCount = 0,
                            ConcurrencyStamp = "b5f8d05f-0757-4938-89b9-cd13501dd7db",
                            Email = "carlos.santos@example.com",
                            EmailConfirmed = false,
                            FirstName = "Carlos",
                            IsDeleted = false,
                            LastName = "Santos",
                            LockoutEnabled = false,
                            NormalizedEmail = "CARLOS.SANTOS@EXAMPLE.COM",
                            NormalizedUserName = "CARLOS.SANTOS",
                            PasswordHash = "AQAAAAIAAYagAAAAEMyOLbQyPBGVp4W6JWAXddxUKdFILkPHy31Gc7f1ap0fmN1jeBRFRqh+HZQUVAi+GQ==",
                            PhoneNumberConfirmed = false,
                            SecurityStamp = "b65f20f0-78a0-4c3a-a81a-e88d09de1b27",
                            TwoFactorEnabled = false,
                            UserName = "carlos.santos"
                        });
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.HasOne("T_Tier.DAL.Entities.User", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.HasOne("T_Tier.DAL.Entities.User", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("T_Tier.DAL.Entities.User", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.HasOne("T_Tier.DAL.Entities.User", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("PostTag", b =>
                {
                    b.HasOne("T_Tier.DAL.Entities.Post", null)
                        .WithMany()
                        .HasForeignKey("PostId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("T_Tier.DAL.Entities.Tag", null)
                        .WithMany()
                        .HasForeignKey("TagId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("T_Tier.DAL.Entities.Comment", b =>
                {
                    b.HasOne("T_Tier.DAL.Entities.Post", "Post")
                        .WithMany("Comments")
                        .HasForeignKey("PostId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("T_Tier.DAL.Entities.User", "User")
                        .WithMany("Comments")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.Navigation("Post");

                    b.Navigation("User");
                });

            modelBuilder.Entity("T_Tier.DAL.Entities.Post", b =>
                {
                    b.HasOne("T_Tier.DAL.Entities.User", "User")
                        .WithMany("Posts")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.Navigation("User");
                });

            modelBuilder.Entity("T_Tier.DAL.Entities.Post", b =>
                {
                    b.Navigation("Comments");
                });

            modelBuilder.Entity("T_Tier.DAL.Entities.User", b =>
                {
                    b.Navigation("Comments");

                    b.Navigation("Posts");
                });
#pragma warning restore 612, 618
        }
    }
}
