using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Gotham3.domain;
using Gotham3.persistence;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace Gotham3.web.Controllers
{
    public class SignalementsController : Controller
    {
        private IRepository<Signalement> _signalementRepository;

        public SignalementsController(IRepository<Signalement> signalementRepo)
        {
            _signalementRepository = signalementRepo;
        }

        public async Task<IActionResult> Index()
        {
            return View(await _signalementRepository.GetAll());
        }

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

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            await _signalementRepository.Delete(id);

            return RedirectToAction(nameof(Index));
        }
    }
}
