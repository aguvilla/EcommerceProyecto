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
    public class ClientesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ClientesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Clientes
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Clientes.Include(c => c.TipoDocumento);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Clientes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Clientes == null)
            {
                return NotFound();
            }

            var cliente = await _context.Clientes
                .Include(c => c.TipoDocumento)
                .FirstOrDefaultAsync(m => m.ClienteId == id);
            if (cliente == null)
            {
                return NotFound();
            }

            return View(cliente);
        }

        // GET: Clientes/Create
        public IActionResult Create()
        {
            ViewData["TipoDocumentoId"] = new SelectList(_context.TiposDocumentos, "TipoDocumentoId", "Descripcion");
            ViewData["SelectProvincia"] = new SelectList(_context.Provincias, "ProvinciaId", "Descripcion");
            ViewData["SelectLocalidad"] = new SelectList(_context.Localidades, "LocalidadId", "Descripcion");
            return View();
        }

        // POST: Clientes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(NewClienteViewModel newcliente)
        {   

            Direccion direccion = new Direccion();
            Cliente cliente = new Cliente();

            cliente = newcliente.Cliente;
            direccion = newcliente.Direccion; 
            cliente.FechaInicio = DateTime.Now;

            if (ModelState.IsValid)
            {
        
                _context.Add(cliente);
                await _context.SaveChangesAsync();
                
                var clienteid = await _context.Clientes
                        .FirstOrDefaultAsync(c=> c.NumeroDocumento == cliente.NumeroDocumento);
                
                direccion.ClienteId = clienteid.ClienteId;    
                
                _context.Add(direccion);
                
                await _context.SaveChangesAsync();
                
                return RedirectToAction(nameof(Index));
            }

            //ViewData["TipoDocumentoId"] = new SelectList(_context.TiposDocumentos, "TipoDocumentoId", "TipoDocumentoId", cliente.TipoDocumentoId);
            return View(newcliente);
        }

        // GET: Clientes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Clientes == null)
            {
                return NotFound();
            }

            var cliente = await _context.Clientes.FindAsync(id);
            if (cliente == null)
            {
                return NotFound();
            }
            ViewData["TipoDocumentoId"] = new SelectList(_context.TiposDocumentos, "TipoDocumentoId", "TipoDocumentoId", cliente.TipoDocumentoId);
            return View(cliente);
        }

        // POST: Clientes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ClienteId,Apellido,Nombre,FechaNacimiento,TipoDocumentoId,NumeroDocumento,RazonSocial,Nacionalidad,Celular,Telefono,CorreoElectronico,FechaInicio,FechaModificacion")] Cliente cliente)
        {
            if (id != cliente.ClienteId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(cliente);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ClienteExists(cliente.ClienteId))
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
            ViewData["TipoDocumentoId"] = new SelectList(_context.TiposDocumentos, "TipoDocumentoId", "TipoDocumentoId", cliente.TipoDocumentoId);
            return View(cliente);
        }

        // GET: Clientes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Clientes == null)
            {
                return NotFound();
            }

            var cliente = await _context.Clientes
                .Include(c => c.TipoDocumento)
                .FirstOrDefaultAsync(m => m.ClienteId == id);
            if (cliente == null)
            {
                return NotFound();
            }

            return View(cliente);
        }

        // POST: Clientes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Clientes == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Clientes'  is null.");
            }
            var cliente = await _context.Clientes.FindAsync(id);
            if (cliente != null)
            {
                _context.Clientes.Remove(cliente);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ClienteExists(int id)
        {
          return (_context.Clientes?.Any(e => e.ClienteId == id)).GetValueOrDefault();
        }

        public async Task<IActionResult> ListaLocalidades(int provinciaId)
        {
            var localidades = await _context.Localidades.Where(l => l.ProvinciaId == provinciaId)
                    .Select(l => new { l.LocalidadId, l.Descripcion})
                    .ToListAsync();
            return Json(localidades);
        }
    }
}
