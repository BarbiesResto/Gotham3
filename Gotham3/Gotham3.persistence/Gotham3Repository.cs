using Gotham3.domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Gotham3.persistence
{
    public class Gotham3Repository<T> : IRepository<T> where T : Entity
    {
        private readonly IServiceScopeFactory scopeFactory;

        public Gotham3Repository(IServiceScopeFactory scopeFactory)
        {
            this.scopeFactory = scopeFactory;
        }

        public async Task<IQueryable<T>> GetAll()
        {
            using (var scope = scopeFactory.CreateScope())
            {
                var dbContext = scope.ServiceProvider.GetRequiredService<Gotham3Context>();
                var items = await dbContext.Set<T>().ToListAsync();

                return items.AsQueryable();
            }
        }

        public async Task<T> GetById(int? id)
        {
            using(var scope = scopeFactory.CreateScope())
            {
                var dbContext = scope.ServiceProvider.GetRequiredService<Gotham3Context>();
                var items = await dbContext.Set<T>().ToListAsync();
                return items.FirstOrDefault(x => x.Id == id);
            }
        }

        public async Task Delete(int? id)
        {
            using (var scope = scopeFactory.CreateScope())
            {
                var dbContext = scope.ServiceProvider.GetRequiredService<Gotham3Context>();
                var items = await dbContext.Set<T>().ToListAsync();
                var itemToRemove = items.FirstOrDefault(x => x.Id == id);
                dbContext.Remove(itemToRemove);
                dbContext.SaveChanges();
            }
        }
    }
}
