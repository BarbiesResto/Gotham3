using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Gotham3.domain;
using Gotham3.persistence;

namespace Gotham3.web
{
    public class NouvellesController : Controller
    {
        private IRepository<Nouvelle> _nouvelleRepository;

        public NouvellesController(IRepository<Nouvelle> nouvelleRepo)
        {
            _nouvelleRepository = nouvelleRepo;
        }

        public async Task<IActionResult> Index()
        {
            return View(await _nouvelleRepository.GetAll());
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var signalement = await _nouvelleRepository.GetById(id);
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

            await _nouvelleRepository.Delete(id);

            return RedirectToAction(nameof(Index));
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Title,Text_Desc,Link_Media,Status,Id")] Nouvelle nouvelle)
        {
            if (ModelState.IsValid)
            {
                await _nouvelleRepository.Add(nouvelle);
                return RedirectToAction(nameof(Index));
            }
            return View(nouvelle);
        }
        
        public async Task<IActionResult> Publish(int? id)
        {
            await _nouvelleRepository.Publish(id);

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var signalement = await _nouvelleRepository.GetById(id);
            if (signalement == null)
            {
                return NotFound();
            }

            return View(signalement);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Title,Text_Desc,Link_Media,Status,Id")] Nouvelle nouvelle)
        {
            if (id != nouvelle.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                await _nouvelleRepository.Update(nouvelle);
                return RedirectToAction(nameof(Index));
            }
            return View(nouvelle);
        }
    }
}
