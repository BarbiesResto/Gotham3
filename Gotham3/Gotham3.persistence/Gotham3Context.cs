using Gotham3.domain;
using Microsoft.EntityFrameworkCore;
using System;

namespace Gotham3.persistence
{
    public class Gotham3Context : DbContext
    {
        public Gotham3Context (DbContextOptions<Gotham3Context> options)
            : base(options)
        {
        }

        public DbSet<Signalement> Signalement { get; set; }
        public DbSet<Sinistre> Sinistre { get; set; }
        public DbSet<CapsuleInformative> CapsuleInformative { get; set; }
        public DbSet<Nouvelle> Nouvelle { get; set; }
        
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder
                .Entity<Nouvelle>()
                .Property(p => p.Status)
                .HasConversion(
                    v => v.ToString(),
                    v => (Status)Enum.Parse(typeof(Status), v));
            modelBuilder
                .Entity<CapsuleInformative>()
                .Property(p => p.Status)
                .HasConversion(
                    v => v.ToString(),
                    v => (Status)Enum.Parse(typeof(Status), v));
            modelBuilder
                .Entity<Sinistre>()
                .Property(p => p.Status)
                .HasConversion(
                v => v.ToString(),
                v => (Status)Enum.Parse(typeof(Status), v));
         }
    }
}
