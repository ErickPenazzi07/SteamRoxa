using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace SteamRoxa.Data

{
    public class SteamRoxaContext : IdentityDbContext
    {
        //Metodo construtor
        public SteamRoxaContext(DbContextOptions<SteamRoxaContext> options) : base(options)
        { }

        //sobreescrever o metodo OnModelCreating
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }

    }
}
