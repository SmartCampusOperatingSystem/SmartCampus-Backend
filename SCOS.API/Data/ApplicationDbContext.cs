using Microsoft.EntityFrameworkCore;
using SCOS.API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using System.IO;

namespace SCOS.API.Data
{
    public class ApplicationDbContext : DbContext
    {
        static IConfigurationRoot config = new ConfigurationBuilder()
                        .SetBasePath(Directory.GetCurrentDirectory())
                        .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                        .Build();
        static DbContextOptionsBuilder optionsBuilder = new DbContextOptionsBuilder<ApplicationDbContext>().UseSqlServer(config.GetConnectionString("ApplicationDbContext"));
        
        public ApplicationDbContext()
            : base(optionsBuilder.Options)
        {
        }
        public ApplicationDbContext(DbContextOptions options) :
            base(options) { }

        public DbSet<Produto> Produtos { get; set; }
        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<DadosSensor> DadosSensor { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Produto>()
                .HasKey(p => p.CodigoBarras);
            modelBuilder.Entity<Usuario>()
                .HasKey(p => p.ID);
            modelBuilder.Entity<DadosSensor>()
                .HasKey(p => p.IdDadosSensor);
                
        }
    }
}