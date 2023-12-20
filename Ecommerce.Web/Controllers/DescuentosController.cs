using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Ecommerce.Web.Data;
using Ecommerce.Web.Models;

namespace Ecommerce.Web.Controllers
{
    public class DescuentosController : Controller
    {
        private readonly ApplicationDbContext _context;

        public DescuentosController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Descuentos
        public async Task<IActionResult> Index()
        {
              return _context.Descuentos != null ? 
                          View(await _context.Descuentos.ToListAsync()) :
                          Problem("Entity set 'ApplicationDbContext.Descuentos'  is null.");
        }

        // GET: Descuentos/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Descuentos == null)
            {
                return NotFound();
            }

            var descuento = await _context.Descuentos
                .FirstOrDefaultAsync(m => m.DescuentoId == id);
            if (descuento == null)
            {
                return NotFound();
            }

            return View(descuento);
        }

        // GET: Descuentos/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Descuentos/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("DescuentoId,Nombre,Descripcion,Porcentaje,Activo,Creado,Modificado,Eliminado")] Descuento descuento)
        {
            if (ModelState.IsValid)
            {
                descuento.Creado = DateTime.Now;
                _context.Add(descuento);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(descuento);
        }

        // GET: Descuentos/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Descuentos == null)
            {
                return NotFound();
            }

            var descuento = await _context.Descuentos.FindAsync(id);
            if (descuento == null)
            {
                return NotFound();
            }
            return View(descuento);
        }

        // POST: Descuentos/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("DescuentoId,Nombre,Descripcion,Porcentaje,Activo,Creado,Modificado,Eliminado")] Descuento descuento)
        {
            if (id != descuento.DescuentoId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    descuento.Modificado = DateTime.Now;
                    _context.Update(descuento);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DescuentoExists(descuento.DescuentoId))
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
            return View(descuento);
        }

        // GET: Descuentos/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Descuentos == null)
            {
                return NotFound();
            }

            var descuento = await _context.Descuentos
                .FirstOrDefaultAsync(m => m.DescuentoId == id);
            if (descuento == null)
            {
                return NotFound();
            }

            return View(descuento);
        }

        // POST: Descuentos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Descuentos == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Descuentos'  is null.");
            }
            var descuento = await _context.Descuentos.FindAsync(id);
            if (descuento != null)
            {
                _context.Descuentos.Remove(descuento);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool DescuentoExists(int id)
        {
          return (_context.Descuentos?.Any(e => e.DescuentoId == id)).GetValueOrDefault();
        }
    }
}
