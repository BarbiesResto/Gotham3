using Gotham3.domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Gotham3.persistence.Mocks
{
    public class MockSinistresRepository : IRepository<Sinistre>
    {
        public readonly List<Sinistre> _sinistres;

        public MockSinistresRepository()
        {
            _sinistres = new List<Sinistre>()
            {
                new Sinistre() { Id = 0, Description = "Il y a un gros sinistre sur la rue principale.", Month = "Juin", Status = Status.Attente, Title = "Bulldozer"},
                new Sinistre() { Id = 1, Description = "Il y a un gros sinistre sur la rue secondaire.", Month = "Avril", Status = Status.Publiée, Title = "McDonalds"}
            };
        }

        public Task Add(Sinistre entity)
        {
            _sinistres.Add(entity);

            return Task.CompletedTask;
        }

        public Task Delete(int? id)
        {
            var items = _sinistres.AsQueryable();
            var itemToRemove = items.FirstOrDefault(x => x.Id == id);
            _sinistres.Remove(itemToRemove);

            return Task.CompletedTask;
        }

        public async Task<IQueryable<Sinistre>> GetAll()
        {
            var items = _sinistres.AsQueryable();

            return await Task.Run(() => items);
        }

        public async Task<Sinistre> GetById(int? id)
        {
            var items = _sinistres.AsQueryable();
            var itemToGet = items.FirstOrDefault(x => x.Id == id);

            return await Task.Run(() => itemToGet);
        }

        public async Task Update(Sinistre entity)
        {
            var itemToUpdate = await GetById(entity.Id);
            itemToUpdate.Description = entity.Description;
            itemToUpdate.Month = entity.Month;
            itemToUpdate.Status = entity.Status;
            itemToUpdate.Title = entity.Title;
        }

        public async Task Publish(int id) {
            var itemToUpdate = await GetById(id);

            if (itemToUpdate.Status == Status.Attente)
                itemToUpdate.Status = Status.Publiée;
            else
                itemToUpdate.Status = Status.Attente;
        }
    }
}