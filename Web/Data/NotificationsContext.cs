using System;
using System.IO;
using System.Reflection;
using System.Runtime.InteropServices;
using Lib;
using Microsoft.EntityFrameworkCore;

namespace Web.Data
{
    internal class NotificationsContext : DbContext
    {
        public DbSet<NotificationModel> Notifications { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            {
                optionsBuilder.UseSqlite($"Data Source={Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location)}" +
                                          @"\notifications.db");
            }
            else if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
            {
                optionsBuilder.UseSqlite($"Data Source={Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location)}" +
                                            "/notifications.db");
            }
            else
            {
                throw new Exception("Your system is not supported :(");
            }
        }
    }
}