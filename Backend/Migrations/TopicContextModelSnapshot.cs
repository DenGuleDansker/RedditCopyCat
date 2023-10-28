﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Model;

#nullable disable

namespace Backend.Migrations
{
    [DbContext(typeof(TopicContext))]
    partial class TopicContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "7.0.12");

            modelBuilder.Entity("Model.Comment", b =>
                {
                    b.Property<long>("CommentID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<DateTime>("Date")
                        .HasColumnType("TEXT");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("TEXT");

<<<<<<< HEAD
                    b.Property<long?>("TopicID")
=======
                    b.Property<long>("TopicID")
>>>>>>> GG
                        .HasColumnType("INTEGER");

                    b.Property<string>("User")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<int>("Votes")
                        .HasColumnType("INTEGER");

                    b.HasKey("CommentID");

                    b.HasIndex("TopicID");

                    b.ToTable("Comment");
                });

            modelBuilder.Entity("Model.Topic", b =>
                {
                    b.Property<long>("TopicID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<DateTime>("Date")
                        .HasColumnType("TEXT");

                    b.Property<string>("Description")
<<<<<<< HEAD
=======
                        .IsRequired()
>>>>>>> GG
                        .HasColumnType("TEXT");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("User")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<int>("Votes")
                        .HasColumnType("INTEGER");

                    b.HasKey("TopicID");

                    b.ToTable("Topics");
                });

            modelBuilder.Entity("Model.Comment", b =>
                {
<<<<<<< HEAD
                    b.HasOne("Model.Topic", null)
                        .WithMany("Comment")
                        .HasForeignKey("TopicID");
=======
                    b.HasOne("Model.Topic", "Topic")
                        .WithMany("Comment")
                        .HasForeignKey("TopicID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Topic");
>>>>>>> GG
                });

            modelBuilder.Entity("Model.Topic", b =>
                {
                    b.Navigation("Comment");
                });
#pragma warning restore 612, 618
        }
    }
}
