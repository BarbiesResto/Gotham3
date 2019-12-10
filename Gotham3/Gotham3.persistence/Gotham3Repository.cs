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

        public async Task Add(T entity)
        {
            using (var scope = scopeFactory.CreateScope())
            {
                var dbContext = scope.ServiceProvider.GetRequiredService<Gotham3Context>();
                var items = await dbContext.Set<T>().ToListAsync();
                items.Add(entity);
                dbContext.SaveChanges();
            }
        }

        public async Task Update(T entity)
        {
            using (var scope = scopeFactory.CreateScope())
            {
                var dbContext = scope.ServiceProvider.GetRequiredService<Gotham3Context>();
                dbContext.Update(entity);

                await dbContext.SaveChangesAsync();
            }
        }

        public async Task Publish(int id)
        {
            using (var scope = scopeFactory.CreateScope())
            {
                var dbContext = scope.ServiceProvider.GetRequiredService<Gotham3Context>();
                var items = await dbContext.Set<T>().ToListAsync();
                var itemToUpdate = items.FirstOrDefault(x => x.Id == id);

                if (itemToUpdate.Status == Status.Attente)
                    itemToUpdate.Status = Status.Publiée;
                else
                    itemToUpdate.Status = Status.Attente;

                dbContext.Update(itemToUpdate);
                await dbContext.SaveChangesAsync();
            }
        }
    }
}
