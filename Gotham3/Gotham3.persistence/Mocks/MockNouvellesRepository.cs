using Gotham3.domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Gotham3.persistence.Mocks
{
    public class MockNouvellesRepository : IRepository<Nouvelle>
    {
        public readonly List<Nouvelle> _nouvelles;

        public MockNouvellesRepository()
        {
            _nouvelles = new List<Nouvelle>()
            {
                new Nouvelle { Id = 0, Title = "Nouvelle plus ou moins interessante", Text_Desc = "ipsum dolor", Link_Media = " imagna", Status = Status.Attente},
                new Nouvelle { Id = 1, Title = "Nouvelle intéressante!", Text_Desc = "Lorem ipsum dolor Lorem ipsum dolor", Link_Media = " incididunt ut labore et dolore magna", Status = Status.Publiée}
            };
        }

        public Task Add(Nouvelle entity)
        {
            _nouvelles.Add(entity);
            return Task.CompletedTask;
        }

        public Task Delete(int? id)
        {
            var items = _nouvelles.AsQueryable();
            var itemToRemove = items.FirstOrDefault(x => x.Id == id);
            _nouvelles.Remove(itemToRemove);

            return Task.CompletedTask;
        }

        public async Task<IQueryable<Nouvelle>> GetAll()
        {
            var items = _nouvelles.AsQueryable();

            return await Task.Run(() => items);
        }

        public async Task<Nouvelle> GetById(int? id)
        {
            var items = _nouvelles.AsQueryable();
            var itemToGet = items.FirstOrDefault(x => x.Id == id);

            return await Task.Run(() => itemToGet);
        }

        public async Task Publish(int? id)
        {
            var itemToUpdate = await GetById(id);
            if (itemToUpdate.Status == Status.Attente)
            {
                itemToUpdate.Status = Status.Publiée;
            }
            else
            {
                itemToUpdate.Status = Status.Attente;
            }
        }

        public async Task Update(Nouvelle entity)
        {
            var entityToUpdate = await GetById(entity.Id);
            entityToUpdate.Link_Media = entity.Link_Media;
            entityToUpdate.Status = entity.Status;
            entityToUpdate.Text_Desc = entity.Text_Desc;
            entityToUpdate.Title = entity.Title;
        }
    }
}