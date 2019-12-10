using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Gotham3.domain;
using Gotham3.persistence;

namespace Gotham3.web.Controllers
{
    public class AlertesController : Controller
    {
        private IRepository<Alerte> _alertesRepository;

        public AlertesController(IRepository<Alerte> alertesRepository)
        {
            _alertesRepository = alertesRepository;
        }

        public async Task<IActionResult> Index()
        {
            return View(await _alertesRepository.GetAll());
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Event_Nature,Sector,Risk,Ressource,Advice,Published")] Alerte alerte)
        {
            if (ModelState.IsValid)
            {
                await _alertesRepository.Add(alerte);
                return RedirectToAction(nameof(Index));
            }
            return View(alerte);
        }

        public IActionResult Edit()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Event_Nature,Sector,Risk,Ressource,Advice,Published")] Alerte alerte)
        {
            if (id != alerte.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    await _alertesRepository.Update(alerte);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AlerteExists(alerte.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(alerte);
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var signalement = await _alertesRepository.GetById(id);
            if (signalement == null)
            {
                return NotFound();
            }

            return View(signalement);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            await _alertesRepository.Delete(id);

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Publish(int id, [Bind("Event_Nature,Sector,Risk,Ressource,Advice,Published")] Alerte alerte)
        {
            await _alertesRepository.Publish(id);

            return RedirectToAction(nameof(Index));
        }

        private bool AlerteExists(int id)
        {
            return false;
        }
    }
}
