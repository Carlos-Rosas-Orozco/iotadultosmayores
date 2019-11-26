using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using IoT_PI.Models;

/*
Controlador que realiza todos los servicios del backend para las vistas y el modelo de la clase Anciano
 */

namespace IoT_PI.Controllers
{
    public class AdultoMayorController : Controller
    {
        private readonly usersContext _context;

        public AdultoMayorController(usersContext context)
        {
            _context = context;
        }

        // GET: AdultoMayor
        public async Task<IActionResult> Index()
        {
            var usersContext = _context.AdultoMayor.Include(a => a.IdusuarioNavigation);
            return View(await usersContext.ToListAsync());
        }

        // GET: AdultoMayor/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var adultoMayor = await _context.AdultoMayor
                .Include(a => a.IdusuarioNavigation)
                .FirstOrDefaultAsync(m => m.IdAdulto == id);
            if (adultoMayor == null)
            {
                return NotFound();
            }

            return View(adultoMayor);
        }

        // GET: AdultoMayor/Create
        public IActionResult Create()
        {
            ViewData["Idusuario"] = new SelectList(_context.Usuarios, "Idusuario", "Correo");
            return View();
        }

        // POST: AdultoMayor/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdAdulto,Idusuario,Nombre,Edad,DescripcionGeneral,Estado")] AdultoMayor adultoMayor)
        {
            if (ModelState.IsValid)
            {
                _context.Add(adultoMayor);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["Idusuario"] = new SelectList(_context.Usuarios, "Idusuario", "Correo", adultoMayor.Idusuario);
            return View(adultoMayor);
        }

        // GET: AdultoMayor/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var adultoMayor = await _context.AdultoMayor.FindAsync(id);
            if (adultoMayor == null)
            {
                return NotFound();
            }
            ViewData["Idusuario"] = new SelectList(_context.Usuarios, "Idusuario", "Correo", adultoMayor.Idusuario);
            return View(adultoMayor);
        }

        // POST: AdultoMayor/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdAdulto,Idusuario,Nombre,Edad,DescripcionGeneral,Estado")] AdultoMayor adultoMayor)
        {
            if (id != adultoMayor.IdAdulto)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(adultoMayor);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AdultoMayorExists(adultoMayor.IdAdulto))
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
            ViewData["Idusuario"] = new SelectList(_context.Usuarios, "Idusuario", "Correo", adultoMayor.Idusuario);
            return View(adultoMayor);
        }

        // GET: AdultoMayor/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var adultoMayor = await _context.AdultoMayor
                .Include(a => a.IdusuarioNavigation)
                .FirstOrDefaultAsync(m => m.IdAdulto == id);
            if (adultoMayor == null)
            {
                return NotFound();
            }

            return View(adultoMayor);
        }

        // POST: AdultoMayor/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var adultoMayor = await _context.AdultoMayor.FindAsync(id);
            _context.AdultoMayor.Remove(adultoMayor);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AdultoMayorExists(int id)
        {
            return _context.AdultoMayor.Any(e => e.IdAdulto == id);
        }
    }
}
