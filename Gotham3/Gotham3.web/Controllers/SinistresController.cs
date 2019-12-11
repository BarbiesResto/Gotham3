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
    public class SinistresController : Controller
    {
        private readonly IRepository<Sinistre> _sinistreRepository;

        public SinistresController(IRepository<Sinistre> sinistreRepository)
        {
            _sinistreRepository = sinistreRepository;
        }

        // GET: Sinistres
        public async Task<IActionResult> Index()
        {
            return View(await _sinistreRepository.GetAll());
        }

        // GET: Sinistres/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var sinistre = await _sinistreRepository.GetById(id);
            if (sinistre == null)
            {
                return NotFound();
            }

            return View(sinistre);
        }

        // GET: Sinistres/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Sinistres/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Title,Description,Month,Status,Id")] Sinistre sinistre)
        {
            if (ModelState.IsValid)
            {
                await _sinistreRepository.Add(sinistre);
                return RedirectToAction(nameof(Index));
            }
            return View(sinistre);
        }

        // GET: Sinistres/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var sinistre = await _sinistreRepository.GetById(id);
            if (sinistre == null)
            {
                return NotFound();
            }
            return View(sinistre);
        }

        // POST: Sinistres/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Title,Description,Month,Status,Id")] Sinistre sinistre)
        {
            if (id != sinistre.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    await _sinistreRepository.Update(sinistre);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SinistreExists(sinistre.Id))
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
            return View(sinistre);
        }

        // GET: Sinistres/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var sinistre = await _sinistreRepository.GetById(id);
            if (sinistre == null)
            {
                return NotFound();
            }

            return View(sinistre);
        }

        // POST: Sinistres/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _sinistreRepository.Delete(id);
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Publish(int id)
        {
            await _sinistreRepository.Publish(id);
            return RedirectToAction(nameof(Index));
        }

        private bool SinistreExists(int id)
        {
            if(_sinistreRepository.GetById(id) == null)
                return false;

            return true;
        }
    }
}
