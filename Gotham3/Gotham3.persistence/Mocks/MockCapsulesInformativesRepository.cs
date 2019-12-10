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
                new CapsuleInformative() { Id = 0, Description = "Description1", Link = "Lien 1", State = "Publiée", Title = "Capsule1"},
                new CapsuleInformative() { Id = 1, Description = "Description2", Link = "Lien 2", State = "En attente", Title = "Capsule2"}
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
    }
}
