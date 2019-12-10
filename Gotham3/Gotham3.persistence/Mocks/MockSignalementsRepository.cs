using Gotham3.domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Gotham3.persistence.Mocks
{
    public class MockSignalementsRepository : IRepository<Signalement>
    {
        public readonly List<Signalement> _signalements;

        public MockSignalementsRepository()
        {
            _signalements = new List<Signalement>()
            {
                new Signalement() { Id = 0, Event_Nature = "Rats!!", Sector = "Ste-Foy", Time = "12:53", Comment = "Beaucoup de rats!"},
                new Signalement() { Id = 1, Event_Nature = "Fuite d'eau", Sector = "Cap-rouge", Time = "11:11", Comment = "Qualinet, la solution"}
            };
        }

        public Task Delete(int? id)
        {
            var items = _signalements.AsQueryable();
            var itemToRemove = items.FirstOrDefault(x => x.Id == id);
            _signalements.Remove(itemToRemove);

            return Task.CompletedTask;
        }

        public async Task<IQueryable<Signalement>> GetAll()
        {
            var items = _signalements.AsQueryable();

            return await Task.Run(() => items);
        }

        public async Task<Signalement> GetById(int? id)
        {
            var items = _signalements.AsQueryable();
            var itemToGet = items.FirstOrDefault(x => x.Id == id);

            return await Task.Run(() => itemToGet);
        }

        public Task Add(Signalement entity)
        {
            throw new NotImplementedException();
        }

        public Task Update(Signalement entity)
        {
            throw new NotImplementedException();
        }


        public Task Publish(int id)
        {
            throw new NotImplementedException();
        }
    }
}