using System;
using Bing.Wallpaper.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Bing.Wallpaper.Data.Configurations
{
    public class LogConfiguration : IEntityTypeConfiguration<Log>
    {
        public void Configure(EntityTypeBuilder<Log> b)
        {
            b.ToTable(nameof(Log));

            b.HasKey(x => x.Id);

            b.Property(x => x.Id)
                .IsRequired()
                .ValueGeneratedOnAdd();

            b.Property(x => x.Level)
                .HasMaxLength(1000)
                ;

            b.Property(x => x.Message);
            b.Property(x => x.MessageTemplate);
            b.Property(x => x.Exception);
            b.Property(x => x.TimeStamp);
            b.Property(x => x.Properties);
            b.Property(x => x.LogEvent);

            b.Property(x => x.Payload);

            b.Property(x => x.UserIp).HasMaxLength(128).IsRequired(false);
            b.Property(x => x.UserRoles);
            b.Property(x => x.RequestUri);
            b.Property(x => x.HttpMethod).HasMaxLength(32).IsRequired(false);
            b.Property(x => x.QueryString);

            b.Property(x => x.IsResolved).IsRequired(false).HasDefaultValue(false);
            b.Property(x => x.ResolvedAt).IsRequired(false);
            b.Property(x => x.UserAgent);
            b.Property(x => x.Errors);
        }
    }
}

