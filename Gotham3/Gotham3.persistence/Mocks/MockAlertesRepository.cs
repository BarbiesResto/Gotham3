using Gotham3.domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gotham3.persistence.Mocks
{
    public class MockAlertesRepository : IRepository<Alerte>
    {
        public readonly List<Alerte> _alertes;

        public MockAlertesRepository()
        {
            _alertes = new List<Alerte>()
            {
                new Alerte() { Id = 0, Event_Nature = "Rats!!", Sector = "Ste-Foy", Risk = "Moyen", Ressource = "Exterminateurs", Advice = "Enfuir", Status = Status.Attente},
                new Alerte() { Id = 1, Event_Nature = "Fuite d'eau", Sector = "Cap-rouge", Risk = "Eleve", Ressource = "Qualinet", Advice = "Pomper l'eau", Status = Status.Publiée}
            };
        }

        public Task Add(Alerte entity)
        {
            _alertes.Add(entity);
            return Task.CompletedTask;
        }

        public async Task Update(Alerte alerte)
        {
            var entityToModify = await GetById(alerte.Id);
            entityToModify.Event_Nature = alerte.Event_Nature;
            entityToModify.Ressource = alerte.Ressource;
            entityToModify.Advice = alerte.Advice;
            entityToModify.Risk = alerte.Risk;
            entityToModify.Status = alerte.Status;
        }

        public async Task Publish(int? id)
        {
            var entityToModify = await GetById(id);
            if(entityToModify.Status == Status.Attente)
            {
                entityToModify.Status = Status.Publiée;
            }
            else
            {
                entityToModify.Status = Status.Attente;
            }
        }

        public Task Delete(int? id)
        {
            var items = _alertes.AsQueryable();
            var itemToRemove = items.FirstOrDefault(x => x.Id == id);
            _alertes.Remove(itemToRemove);

            return Task.CompletedTask;
        }

        public async Task<IQueryable<Alerte>> GetAll()
        {
            var items = _alertes.AsQueryable();

            return await Task.Run(() => items);
        }

        public async Task<Alerte> GetById(int? id)
        {
            var items = _alertes.AsQueryable();
            var itemToGet = items.FirstOrDefault(x => x.Id == id);

            return await Task.Run(() => itemToGet);
        }
    }
}
