﻿// <auto-generated />
using System;
using E_Agriculture.DAL.Data_Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace E_Agriculture.DAL.Migrations
{
    [DbContext(typeof(E_Agriculture_Context))]
    [Migration("20231214082548_ss4")]
    partial class ss4
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.13")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("E_Agriculture.DAL.Model.Answer", b =>
                {
                    b.Property<int>("Answer_Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Answer_Id"));

                    b.Property<string>("AnswerFor")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("Answer_Date")
                        .HasColumnType("datetime2");

                    b.Property<int>("Question_Id")
                        .HasColumnType("int");

                    b.Property<int>("User_Id")
                        .HasColumnType("int");

                    b.HasKey("Answer_Id");

                    b.ToTable("answer");
                });

            modelBuilder.Entity("E_Agriculture.DAL.Model.AvailabilityCrops", b =>
                {
                    b.Property<int>("ACrop_Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ACrop_Id"));

                    b.Property<string>("ACrop_Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ALocation")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("AQuantity")
                        .HasColumnType("int");

                    b.Property<DateTime>("Add_Date")
                        .HasColumnType("datetime2");

                    b.Property<string>("Image")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Price")
                        .HasColumnType("int");

                    b.Property<int>("User_Id")
                        .HasColumnType("int");

                    b.HasKey("ACrop_Id");

                    b.ToTable("availabilityCrops");
                });

            modelBuilder.Entity("E_Agriculture.DAL.Model.GovernmentPrograms", b =>
                {
                    b.Property<int>("Program_Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Program_Id"));

                    b.Property<DateTime>("ProgramEnd_Date")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("ProgramStart_Date")
                        .HasColumnType("datetime2");

                    b.Property<string>("Program_Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Program_Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Program_Id");

                    b.ToTable("governmentPrograms");
                });

            modelBuilder.Entity("E_Agriculture.DAL.Model.MarketDetails", b =>
                {
                    b.Property<int>("MCrop_Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("MCrop_Id"));

                    b.Property<DateTime>("MAdd_Date")
                        .HasColumnType("datetime2");

                    b.Property<string>("MCrop_Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("MLocation")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("MPrice")
                        .HasColumnType("int");

                    b.Property<int>("MQuantity")
                        .HasColumnType("int");

                    b.HasKey("MCrop_Id");

                    b.ToTable("marketDetails");
                });

            modelBuilder.Entity("E_Agriculture.DAL.Model.Queries", b =>
                {
                    b.Property<int>("Question_Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Question_Id"));

                    b.Property<string>("Question")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("Question_Date")
                        .HasColumnType("datetime2");

                    b.Property<int>("User_Id")
                        .HasColumnType("int");

                    b.HasKey("Question_Id");

                    b.ToTable("queries");
                });

            modelBuilder.Entity("E_Agriculture.DAL.Model.RequiredCrops", b =>
                {
                    b.Property<int>("RCrop_Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("RCrop_Id"));

                    b.Property<DateTime>("RAdd_Date")
                        .HasColumnType("datetime2");

                    b.Property<string>("RCrop_Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("RLocation")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("RQuantity")
                        .HasColumnType("int");

                    b.Property<int>("User_Id")
                        .HasColumnType("int");

                    b.HasKey("RCrop_Id");

                    b.ToTable("requiredCrops");
                });

            modelBuilder.Entity("E_Agriculture.DAL.Model.User", b =>
                {
                    b.Property<int>("User_Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("User_Id"));

                    b.Property<string>("Address")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Contact_No")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Hashpassword")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("Joining_Date")
                        .HasColumnType("datetime2");

                    b.Property<string>("OTP")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserAs")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("User_Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("User_Id");

                    b.ToTable("userdetails");
                });
#pragma warning restore 612, 618
        }
    }
}
