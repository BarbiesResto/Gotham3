﻿using Gotham3.domain;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Gotham3.persistence
{
    public class Gotham3Context : DbContext
    {
        public Gotham3Context (DbContextOptions<Gotham3Context> options)
            : base(options)
        {
        }

        public DbSet<Signalement> Signalement { get; set; }
        public DbSet<Alerte> Alerte { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder
                .Entity<Alerte>()
                .Property(p => p.Published)
                .HasConversion(
                    v => v.ToString(),
                    v => (Status)Enum.Parse(typeof(Status), v));
        }
    }
}
