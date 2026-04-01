using System;
using System.Data.Entity;

namespace MvcMusicStore.Models
{
    public class MusicStoreEntities : DbContext
    {
        // Set at startup from appsettings.json via Program.cs
        public static string ConnectionString { get; set; } = "name=MusicStoreEntities";

        public MusicStoreEntities() : base(ConnectionString)
        {
        }

        public DbSet<Album> Albums { get; set; }
        public DbSet<Genre> Genres { get; set; }
        public DbSet<Artist> Artists { get; set; }
        public DbSet<Cart> Carts { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderDetail> OrderDetails { get; set; }
        public DbSet<MusicStoreUser> Users { get; set; }
        public DbSet<MusicStoreRole> Roles { get; set; }
        public DbSet<MusicStoreUserRole> UserRoles { get; set; }
    }
}
