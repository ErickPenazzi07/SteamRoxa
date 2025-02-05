using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using SteamRoxa.Models;

namespace SteamRoxa.Data

{
    public class SteamRoxaContext : IdentityDbContext
    {
        //Metodo construtor
        public SteamRoxaContext(DbContextOptions<SteamRoxaContext> options) : base(options)
        { }

        //Definir as tabelas do banco de dados

        public DbSet<Jogo> Jogos { get; set; }
        public DbSet<Categoria> Categorias { get; set; }

        //sobreescrever o metodo OnModelCreating
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Jogo>().ToTable("Jogos");
            modelBuilder.Entity<Categoria>().ToTable("Categorias");
        }

    }
}
