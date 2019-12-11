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
    public class CapsuleInformativesController : Controller
    {
        private readonly IRepository<CapsuleInformative> _capsuleInformativeRepo; 

        public CapsuleInformativesController(IRepository<CapsuleInformative> capsuleInformativeRepo)
        {
            _capsuleInformativeRepo = capsuleInformativeRepo;
        }

        // GET: CapsuleInformatives
        public async Task<IActionResult> Index()
        {
            return View(await _capsuleInformativeRepo.GetAll());
        }

        // GET: CapsuleInformatives/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var capsuleInformative = await _capsuleInformativeRepo.GetById(id);
            if (capsuleInformative == null)
            {
                return NotFound();
            }

            return View(capsuleInformative);
        }

        // GET: CapsuleInformatives/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: CapsuleInformatives/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Title,Description,Link,Status,Id")] CapsuleInformative capsuleInformative)
        {
            if (ModelState.IsValid)
            {
                await _capsuleInformativeRepo.Add(capsuleInformative);
                return RedirectToAction(nameof(Index));
            }
            return View(capsuleInformative);
        }

        // GET: CapsuleInformatives/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var capsuleInformative = await _capsuleInformativeRepo.GetById(id);
            if (capsuleInformative == null)
            {
                return NotFound();
            }
            return View(capsuleInformative);
        }

        // POST: CapsuleInformatives/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Title,Description,Link,State,Id")] CapsuleInformative capsuleInformative)
        {
            if (id != capsuleInformative.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                   await _capsuleInformativeRepo.Update(capsuleInformative);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CapsuleInformativeExists(capsuleInformative.Id))
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
            return View(capsuleInformative);
        }

        // GET: CapsuleInformatives/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var capsuleInformative = await _capsuleInformativeRepo.GetById(id);
            if (capsuleInformative == null)
            {
                return NotFound();
            }

            return View(capsuleInformative);
        }

        // POST: CapsuleInformatives/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _capsuleInformativeRepo.Delete(id);
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Publish(int id)
        {
            await _capsuleInformativeRepo.Publish(id);
            return RedirectToAction(nameof(Index));
        }

        private bool CapsuleInformativeExists(int id)
        {
            return false;//_context.CapsuleInformative.Any(e => e.Id == id);
        }
    }
}
