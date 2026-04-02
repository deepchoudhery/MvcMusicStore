using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace MvcMusicStore.Models
{
    public class MusicStoreEntities : IdentityDbContext<ApplicationUser>
    {
        public MusicStoreEntities(DbContextOptions<MusicStoreEntities> options)
            : base(options)
        {
        }

        public DbSet<Album> Albums => Set<Album>();
        public DbSet<Genre> Genres => Set<Genre>();
        public DbSet<Artist> Artists => Set<Artist>();
        public DbSet<Cart> Carts => Set<Cart>();
        public DbSet<Order> Orders => Set<Order>();
        public DbSet<OrderDetail> OrderDetails => Set<OrderDetail>();
    }
}
