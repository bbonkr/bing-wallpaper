using Bing.Wallpaper.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Bing.Wallpaper.Data.Configurations
{
    public class ImageInfoConfiguration : IEntityTypeConfiguration<ImageInfo>
    {
        public void Configure(EntityTypeBuilder<ImageInfo> builder)
        {
            builder.ToTable("Images");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id)
                .IsRequired()
                .ValueGeneratedOnAdd();

            builder.Property(x => x.BaseUrl)
                .IsRequired()
                .HasMaxLength(1000)
                ;
            builder.Property(x => x.ContentType)
                .IsRequired()
                .HasMaxLength(30)
                ;
            builder.Property(x => x.CreatedAt)
                .IsRequired();
            builder.Property(x => x.Directory)
                .IsRequired()
                .HasMaxLength(1000);
            builder.Property(x => x.FileName)
                .IsRequired()
                .HasMaxLength(1000)
                ;
            builder.Property(x => x.FilePath)
                .IsRequired()
                .HasMaxLength(1000)
                ;
            builder.Property(x => x.FileSize)
                .IsRequired()
                ;
            builder.Property(x => x.Hash)
                .IsRequired()
                .HasMaxLength(500)
                ;

            builder.Property(x => x.Url)
                .IsRequired()
                .HasMaxLength(1000)
                ;
        }
    }
}
