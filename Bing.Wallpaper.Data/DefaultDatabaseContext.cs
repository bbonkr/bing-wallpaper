using Bing.Wallpaper.Data.Configurations;
using Bing.Wallpaper.Entities;
using Microsoft.EntityFrameworkCore;
using System;

namespace Bing.Wallpaper.Data
{
    public class DefaultDatabaseContext : DbContext
    {
        public DefaultDatabaseContext(DbContextOptions<DefaultDatabaseContext> options) : base(options)
        {

        }

        public DbSet<ImageInfo> Images { get; set; }

        public DbSet<AppLog> Logs { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfiguration(new ImageInfoConfiguration());
            modelBuilder.ApplyConfiguration(new AppLogConfiguration());
        }
    }
}
