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
        public DbSet<Nouvelle> Nouvelle { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder
                .Entity<Nouvelle>()
                .Property(p => p.Status)
                .HasConversion(
                    v => v.ToString(),
                    v => (Status)Enum.Parse(typeof(Status), v));
        }
    }
}
