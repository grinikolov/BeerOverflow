﻿// <auto-generated />
using System;
using Database;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Database.Migrations
{
    [DbContext(typeof(BOContext))]
    partial class BOContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.3")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("BeerOverflow.Models.Beer", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<float>("ABV")
                        .HasColumnType("real");

                    b.Property<int>("BreweryID")
                        .HasColumnType("int");

                    b.Property<int>("CountryID")
                        .HasColumnType("int");

                    b.Property<DateTime>("CreatedOn")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("DeletedOn")
                        .HasColumnType("datetime2");

                    b.Property<bool>("IsDeleted")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bit")
                        .HasDefaultValue(false);

                    b.Property<DateTime?>("ModifiedOn")
                        .HasColumnType("datetime2");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Rating")
                        .HasColumnType("int");

                    b.Property<int>("StyleID")
                        .HasColumnType("int");

                    b.HasKey("ID");

                    b.HasIndex("BreweryID");

                    b.HasIndex("CountryID");

                    b.HasIndex("StyleID");

                    b.ToTable("Beers");
                });

            modelBuilder.Entity("BeerOverflow.Models.BeerStyle", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("CreatedOn")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("DeletedOn")
                        .HasColumnType("datetime2");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("IsDeleted")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bit")
                        .HasDefaultValue(false);

                    b.Property<DateTime?>("ModifiedOn")
                        .HasColumnType("datetime2");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ID");

                    b.ToTable("BeerStyles");
                });

            modelBuilder.Entity("BeerOverflow.Models.BeerUserRating", b =>
                {
                    b.Property<int>("UserID")
                        .HasColumnType("int");

                    b.Property<int>("BeerID")
                        .HasColumnType("int");

                    b.Property<int>("Rating")
                        .HasColumnType("int");

                    b.HasKey("UserID", "BeerID");

                    b.HasIndex("BeerID");

                    b.ToTable("BeerUserRatings");
                });

            modelBuilder.Entity("BeerOverflow.Models.Brewery", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("CountryID")
                        .HasColumnType("int");

                    b.Property<DateTime>("CreatedOn")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("DeletedOn")
                        .HasColumnType("datetime2");

                    b.Property<bool>("IsDeleted")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bit")
                        .HasDefaultValue(false);

                    b.Property<DateTime?>("ModifiedOn")
                        .HasColumnType("datetime2");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ID");

                    b.HasIndex("CountryID");

                    b.ToTable("Breweries");
                });

            modelBuilder.Entity("BeerOverflow.Models.Comment", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("BeerID")
                        .HasColumnType("int");

                    b.Property<DateTime>("CreatedOn")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("DeletedOn")
                        .HasColumnType("datetime2");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("IsDeleted")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bit")
                        .HasDefaultValue(false);

                    b.Property<int>("LikesCount")
                        .HasColumnType("int");

                    b.Property<DateTime?>("ModifiedOn")
                        .HasColumnType("datetime2");

                    b.Property<int?>("ReviewID")
                        .HasColumnType("int");

                    b.Property<int>("UserID")
                        .HasColumnType("int");

                    b.HasKey("ID");

                    b.HasIndex("BeerID");

                    b.HasIndex("ReviewID");

                    b.HasIndex("UserID");

                    b.ToTable("Comments");
                });

            modelBuilder.Entity("BeerOverflow.Models.Country", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("CreatedOn")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("DeletedOn")
                        .HasColumnType("datetime2");

                    b.Property<bool>("IsDeleted")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bit")
                        .HasDefaultValue(false);

                    b.Property<DateTime?>("ModifiedOn")
                        .HasColumnType("datetime2");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("ID");

                    b.HasIndex("Name")
                        .IsUnique();

                    b.ToTable("Countries");
                });

            modelBuilder.Entity("BeerOverflow.Models.DrankList", b =>
                {
                    b.Property<int>("BeerID")
                        .HasColumnType("int");

                    b.Property<int>("UserID")
                        .HasColumnType("int");

                    b.HasKey("BeerID", "UserID");

                    b.HasIndex("UserID");

                    b.ToTable("DrankLists");
                });

            modelBuilder.Entity("BeerOverflow.Models.Flag", b =>
                {
                    b.Property<int>("UserID")
                        .HasColumnType("int");

                    b.Property<int>("ReviewID")
                        .HasColumnType("int");

                    b.HasKey("UserID", "ReviewID");

                    b.HasIndex("ReviewID");

                    b.ToTable("Flag");
                });

            modelBuilder.Entity("BeerOverflow.Models.Like", b =>
                {
                    b.Property<int>("UserID")
                        .HasColumnType("int");

                    b.Property<int>("CommentID")
                        .HasColumnType("int");

                    b.HasKey("UserID", "CommentID");

                    b.HasIndex("CommentID");

                    b.ToTable("Likes");
                });

            modelBuilder.Entity("BeerOverflow.Models.Review", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("BeerID")
                        .HasColumnType("int");

                    b.Property<DateTime>("CreatedOn")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("DeletedOn")
                        .HasColumnType("datetime2");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("IsDeleted")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bit")
                        .HasDefaultValue(false);

                    b.Property<bool>("IsFlagged")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bit")
                        .HasDefaultValue(false);

                    b.Property<int>("LikesCount")
                        .HasColumnType("int");

                    b.Property<DateTime?>("ModifiedOn")
                        .HasColumnType("datetime2");

                    b.Property<int>("Rating")
                        .HasColumnType("int");

                    b.Property<int>("UserID")
                        .HasColumnType("int");

                    b.HasKey("ID");

                    b.HasIndex("UserID");

                    b.HasIndex("BeerID", "UserID")
                        .IsUnique();

                    b.ToTable("Reviews");
                });

            modelBuilder.Entity("BeerOverflow.Models.User", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("CreatedOn")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("DeletedOn")
                        .HasColumnType("datetime2");

                    b.Property<bool>("IsDeleted")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bit")
                        .HasDefaultValue(false);

                    b.Property<DateTime?>("ModifiedOn")
                        .HasColumnType("datetime2");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ID");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("BeerOverflow.Models.WishList", b =>
                {
                    b.Property<int>("BeerID")
                        .HasColumnType("int");

                    b.Property<int>("UserID")
                        .HasColumnType("int");

                    b.HasKey("BeerID", "UserID");

                    b.HasIndex("UserID");

                    b.ToTable("WishLists");
                });

            modelBuilder.Entity("BeerOverflow.Models.Beer", b =>
                {
                    b.HasOne("BeerOverflow.Models.Brewery", "Brewery")
                        .WithMany("Beers")
                        .HasForeignKey("BreweryID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("BeerOverflow.Models.Country", "Country")
                        .WithMany()
                        .HasForeignKey("CountryID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("BeerOverflow.Models.BeerStyle", "Style")
                        .WithMany()
                        .HasForeignKey("StyleID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("BeerOverflow.Models.BeerUserRating", b =>
                {
                    b.HasOne("BeerOverflow.Models.Beer", "Beer")
                        .WithMany()
                        .HasForeignKey("BeerID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("BeerOverflow.Models.User", "User")
                        .WithMany()
                        .HasForeignKey("UserID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("BeerOverflow.Models.Brewery", b =>
                {
                    b.HasOne("BeerOverflow.Models.Country", "Country")
                        .WithMany("Breweries")
                        .HasForeignKey("CountryID")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();
                });

            modelBuilder.Entity("BeerOverflow.Models.Comment", b =>
                {
                    b.HasOne("BeerOverflow.Models.Beer", "Beer")
                        .WithMany()
                        .HasForeignKey("BeerID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("BeerOverflow.Models.Review", "Review")
                        .WithMany("Comments")
                        .HasForeignKey("ReviewID");

                    b.HasOne("BeerOverflow.Models.User", "User")
                        .WithMany("CommentList")
                        .HasForeignKey("UserID")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();
                });

            modelBuilder.Entity("BeerOverflow.Models.DrankList", b =>
                {
                    b.HasOne("BeerOverflow.Models.Beer", "Beer")
                        .WithMany("DrankLists")
                        .HasForeignKey("BeerID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("BeerOverflow.Models.User", "User")
                        .WithMany("DrankLists")
                        .HasForeignKey("UserID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("BeerOverflow.Models.Flag", b =>
                {
                    b.HasOne("BeerOverflow.Models.Review", "Review")
                        .WithMany()
                        .HasForeignKey("ReviewID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("BeerOverflow.Models.User", "User")
                        .WithMany("FlagList")
                        .HasForeignKey("UserID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("BeerOverflow.Models.Like", b =>
                {
                    b.HasOne("BeerOverflow.Models.Comment", "Comment")
                        .WithMany("Likes")
                        .HasForeignKey("CommentID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("BeerOverflow.Models.User", "User")
                        .WithMany("LikesList")
                        .HasForeignKey("UserID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("BeerOverflow.Models.Review", b =>
                {
                    b.HasOne("BeerOverflow.Models.Beer", "Beer")
                        .WithMany("Reviews")
                        .HasForeignKey("BeerID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("BeerOverflow.Models.User", "User")
                        .WithMany("ReviewList")
                        .HasForeignKey("UserID")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();
                });

            modelBuilder.Entity("BeerOverflow.Models.WishList", b =>
                {
                    b.HasOne("BeerOverflow.Models.Beer", "Beer")
                        .WithMany("WishLists")
                        .HasForeignKey("BeerID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("BeerOverflow.Models.User", "User")
                        .WithMany("WishLists")
                        .HasForeignKey("UserID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
