using Bing.Wallpaper.Entities;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using System;
using System.Collections.Generic;
using System.Text;

namespace Bing.Wallpaper.Data.Configurations
{
    internal class AppLogConfiguration : IEntityTypeConfiguration<AppLog>
    {
        public void Configure(EntityTypeBuilder<AppLog> builder)
        {
            builder.ToTable("AppLogs");

            builder.HasComment("로깅 NLog");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id)
                .IsRequired()
                .HasMaxLength(36)
                .HasDefaultValueSql("newid()")
                .HasComment("식별자")
                ;

            builder.Property(x => x.MachineName)
                .HasMaxLength(50)
                .IsRequired()
                .HasComment("장치")
                ;
            
            builder.Property(x => x.Logged)
                .IsRequired()
                .HasDefaultValueSql("sysdatetimeoffset()") // sql server only
                .HasComment("작성시각")
                ;

            builder.Property(x => x.Level)
                .IsRequired()
                .HasMaxLength(50)
                .HasComment("로그 레벨")
                ;

            builder.Property(x => x.Message)
                .IsRequired()
                .HasComment("메시지")
                ;
            builder.Property(x => x.Logger)
                .IsRequired(false)
                .HasComment("로거")
                ;
            builder.Property(x => x.Callsite)
                .IsRequired(false)
                .HasComment("사이트")
                ;
            builder.Property(x => x.Exception)
                .IsRequired(false)
                .HasComment("예외")
                ;
        }
    }
}
