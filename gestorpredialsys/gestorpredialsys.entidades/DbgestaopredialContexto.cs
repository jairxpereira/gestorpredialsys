using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore; // DbContext, DbContextOptionsBuilder
using static System.Console;

namespace gestorpredialsys.entidades
{


    // isto gerencia a conexão com o banco de dados
    public class DbgestaopredialContexto : DbContext
    {
        public DbSet<Morador>? Moradores { get; set; }
        public DbSet<Familia>? Familias { get; set; }
        public DbSet<Condominio>? Condominios { get; set; }

        public DbgestaopredialContexto()
        {
        }

        public DbgestaopredialContexto(DbContextOptions<DbgestaopredialContexto> options)
            : base(options)
        {
        }

        protected override void OnConfiguring(
        DbContextOptionsBuilder optionsBuilder)
        {
            if (ConstantesProjeto.DatabaseProvider == "SqlServer")
            {
                string connection = "Data Source=.;" +
                "Initial Catalog=dbgestaopredial;" +
                "Integrated Security=true;Encrypt=false;" +
                "MultipleActiveResultSets=true;";
                optionsBuilder.UseSqlServer(connection);
                
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Condominio>(entity =>
            {
                entity.Property(e => e.Id).ValueGeneratedNever();
            });

            modelBuilder.Entity<Familia>(entity =>
            {
                entity.Property(e => e.Id).ValueGeneratedNever();
            });

            modelBuilder.Entity<Morador>(entity =>
            {
                entity.Property(e => e.Id).ValueGeneratedNever();
            });

            // OnModelCreatingPartial(modelBuilder);
        }
    }
}

