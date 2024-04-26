using Microsoft.EntityFrameworkCore;
using NetCoreWebApp.Models;

namespace NetCoreWebApp.Utility
{
    public class UygulamaDbContext : DbContext
    {
        public UygulamaDbContext(DbContextOptions<UygulamaDbContext> options) : base(options) { }

        public DbSet<KitapTuru> KitapTurleri { get; set; }
        public DbSet<Kitap> Kitaplar {  get; set; }

    }
}
