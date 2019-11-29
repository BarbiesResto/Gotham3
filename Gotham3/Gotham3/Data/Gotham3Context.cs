using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Gotham3.Models;

namespace Gotham3.Data
{
    public class Gotham3Context : DbContext
    {
        public Gotham3Context (DbContextOptions<Gotham3Context> options)
            : base(options)
        {
        }

        public DbSet<Gotham3.Models.Signalement> Signalement { get; set; }
    }
}
