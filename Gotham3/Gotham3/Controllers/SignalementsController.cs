using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Gotham3.Data;
using Gotham3.Models;

namespace Gotham3.Controllers
{
    public class SignalementsController : Controller
    {
        private IRepository<Signalement> _signalementRepository;

        public SignalementsController(IRepository<Signalement> signalementRepo)
        {
            _signalementRepository = signalementRepo;
        }

        // GET: Signalements
        public async Task<IActionResult> Index()
        {
            return View(await _signalementRepository.GetAll());
        }

        // GET: Signalements/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var signalement = await _signalementRepository.GetById(id);
            if (signalement == null)
            {
                return NotFound();
            }

            return View(signalement);
        }
        /*
        // GET: Signalements/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Signalements/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Event_Nature,Sector,Time,Comment,Id")] Signalement signalement)
        {
            if (ModelState.IsValid)
            {
                _context.Add(signalement);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(signalement);
        }

        // GET: Signalements/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var signalement = await _context.Signalement.FindAsync(id);
            if (signalement == null)
            {
                return NotFound();
            }
            return View(signalement);
        }

        // POST: Signalements/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Event_Nature,Sector,Time,Comment,Id")] Signalement signalement)
        {
            if (id != signalement.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(signalement);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SignalementExists(signalement.Id))
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
            return View(signalement);
        }
        */
        // GET: Signalements/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            await _signalementRepository.Delete(id);

            return RedirectToAction(nameof(Index));
        }
        /*
        // POST: Signalements/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var signalement = await _context.Signalement.FindAsync(id);
            _context.Signalement.Remove(signalement);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SignalementExists(int id)
        {
            return _context.Signalement.Any(e => e.Id == id);
        }
        */
    }
}
