using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using RedPill.Models;

#nullable disable

namespace RedPill.Data
{
    /// <summary>
    /// Scaffold-DbContext "Server=localhost;Database=RedpillDB;Trusted_Connection=True;" Microsoft.EntityFrameworkCore.SqlServer -OutputDir Models
    /// </summary>
    public partial class RedpillDBContext : DbContext
    {
        public DbSet<Usuario> Usuario { get; set; }
        public DbSet<Transacao> Transacao { get; set; }
        public RedpillDBContext()
        {
            
        }

        public RedpillDBContext(DbContextOptions<RedpillDBContext> options)
            : base(options)
        {

        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Server=localhost;Database=RedpillDB;Trusted_Connection=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "Latin1_General_CI_AS");

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
