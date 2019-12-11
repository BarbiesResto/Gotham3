using Gotham3.domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gotham3.persistence.Mocks
{
    public class MockCapsulesInformativesRepository : IRepository<CapsuleInformative>
    {
        public readonly List<CapsuleInformative> _capsuleInformatives;

        public MockCapsulesInformativesRepository()
        {
            _capsuleInformatives = new List<CapsuleInformative>()
            {
                new CapsuleInformative() { Id = 0, Description = "Description1", Link = "Lien 1", Status = Status.Attente, Title = "Capsule1"},
                new CapsuleInformative() { Id = 1, Description = "Description2", Link = "Lien 2", Status = Status.Publiée, Title = "Capsule2"}
            };
        }
        public Task Delete(int? id)
        {
            var items = _capsuleInformatives.AsQueryable();
            var itemToRemove = items.FirstOrDefault(x => x.Id == id);
            _capsuleInformatives.Remove(itemToRemove);

            return Task.CompletedTask;
        }

        public async Task<IQueryable<CapsuleInformative>> GetAll()
        {
            var items = _capsuleInformatives.AsQueryable();

            return await Task.Run(() => items);
        }

        public async Task<CapsuleInformative> GetById(int? id)
        {
            var items = _capsuleInformatives.AsQueryable();
            var itemToGet = items.FirstOrDefault(x => x.Id == id);

            return await Task.Run(() => itemToGet);
        }

        public Task Add(CapsuleInformative entity)
        { 
            _capsuleInformatives.Add(entity);
            return Task.CompletedTask;            
        }

        public async Task Update(CapsuleInformative entity)
        {
            var entityToModify = await GetById(entity.Id);
            entityToModify.Description = entity.Description;
            entityToModify.Link = entity.Link;
            entityToModify.Status = entity.Status;
            entityToModify.Title = entity.Title;
        }

        public async Task Publish(int? id)
        {
            var itemToUpdate = await GetById(id);
            if (itemToUpdate.Status == Status.Attente)
                itemToUpdate.Status = Status.Publiée;
            else
                itemToUpdate.Status = Status.Attente;
        }
    }
}
