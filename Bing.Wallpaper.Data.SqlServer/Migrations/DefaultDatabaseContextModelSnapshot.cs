﻿// <auto-generated />
using System;
using Bing.Wallpaper.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Bing.Wallpaper.Data.SqlServer.Migrations
{
    [DbContext(typeof(DefaultDatabaseContext))]
    partial class DefaultDatabaseContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.8")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Bing.Wallpaper.Entities.AppLog", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("nvarchar(36)")
                        .HasComment("식별자")
                        .HasMaxLength(36);

                    b.Property<string>("Callsite")
                        .HasColumnType("nvarchar(max)")
                        .HasComment("사이트");

                    b.Property<string>("Exception")
                        .HasColumnType("nvarchar(max)")
                        .HasComment("예외");

                    b.Property<string>("Level")
                        .IsRequired()
                        .HasColumnType("nvarchar(50)")
                        .HasComment("로그 레벨")
                        .HasMaxLength(50);

                    b.Property<DateTimeOffset>("Logged")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetimeoffset")
                        .HasComment("작성시각")
                        .HasDefaultValue(new DateTimeOffset(new DateTime(2020, 9, 21, 4, 5, 23, 934, DateTimeKind.Unspecified).AddTicks(4295), new TimeSpan(0, 0, 0, 0, 0)));

                    b.Property<string>("Logger")
                        .HasColumnType("nvarchar(max)")
                        .HasComment("로거");

                    b.Property<string>("MachineName")
                        .IsRequired()
                        .HasColumnType("nvarchar(50)")
                        .HasComment("장치")
                        .HasMaxLength(50);

                    b.Property<string>("Message")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)")
                        .HasComment("메시지");

                    b.HasKey("Id");

                    b.ToTable("Logs");

                    b.HasComment("로깅 NLog");
                });

            modelBuilder.Entity("Bing.Wallpaper.Entities.ImageInfo", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("BaseUrl")
                        .IsRequired()
                        .HasColumnType("nvarchar(1000)")
                        .HasMaxLength(1000);

                    b.Property<string>("ContentType")
                        .IsRequired()
                        .HasColumnType("nvarchar(30)")
                        .HasMaxLength(30);

                    b.Property<DateTimeOffset>("CreatedAt")
                        .HasColumnType("datetimeoffset");

                    b.Property<string>("Directory")
                        .IsRequired()
                        .HasColumnType("nvarchar(1000)")
                        .HasMaxLength(1000);

                    b.Property<string>("FileName")
                        .IsRequired()
                        .HasColumnType("nvarchar(1000)")
                        .HasMaxLength(1000);

                    b.Property<string>("FilePath")
                        .IsRequired()
                        .HasColumnType("nvarchar(1000)")
                        .HasMaxLength(1000);

                    b.Property<long>("FileSize")
                        .HasColumnType("bigint");

                    b.Property<string>("Hash")
                        .IsRequired()
                        .HasColumnType("nvarchar(500)")
                        .HasMaxLength(500);

                    b.Property<string>("Url")
                        .IsRequired()
                        .HasColumnType("nvarchar(1000)")
                        .HasMaxLength(1000);

                    b.HasKey("Id");

                    b.ToTable("Images");
                });
#pragma warning restore 612, 618
        }
    }
}
