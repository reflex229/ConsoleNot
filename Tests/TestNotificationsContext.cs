using Lib;
using Microsoft.EntityFrameworkCore;

namespace Tests
{
    internal class TestNotificationsContext : DbContext
    {
        public DbSet<NotificationModel> Notifications { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseInMemoryDatabase("NotificationsTest");
        }
    }
}