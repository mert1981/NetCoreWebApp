using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.Web.CodeGeneration.EntityFrameworkCore;
using NetCoreWebApp.Models;

namespace NetCoreWebApp.Utility
{
    public class UygulamaDbContext : IdentityDbContext
    {
        public UygulamaDbContext(DbContextOptions<UygulamaDbContext> options) : base(options) { }

        public DbSet<KitapTuru> KitapTurleri { get; set; }
        public DbSet<Kitap> Kitaplar {  get; set; }
        public DbSet<Kiralama> Kiralama { get; set;}
       
        public DbSet<ApplicationUser> ApplicationUsers { get; set; }


    }
}
