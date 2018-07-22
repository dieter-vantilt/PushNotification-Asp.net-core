using Microsoft.EntityFrameworkCore;

namespace PushNotificationShop.Entities.Context
{
    public class PushNotificationShopDbContext
        : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Server=(localdb)\mssqllocaldb;Database=PushNotificationShopDb;Trusted_Connection=True;");
        }

        public virtual DbSet<Item> Items { get; set; }
        public virtual DbSet<PushSubscription> PushSubscriptions { get; set; }
    }
}