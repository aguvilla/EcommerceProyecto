using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Ecommerce.Web.Data;
using Ecommerce.Web.Models;
using Ecommerce.Web.ViewModels;
using System.Drawing;

namespace Ecommerce.Web.Controllers
{
    public class ProductosController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ProductosController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Productos
        public async Task<IActionResult> Index(string producto, string sku, int? upc)
        {
            IQueryable<Producto> productos = _context.Productos
                   .Include(p => p.Categoria)
                   .Include(p => p.Descuento)
                   .Include(p => p.Inventario)
                   .Include(p => p.Marca);

            if (!String.IsNullOrEmpty(producto))
            {
                productos = productos.Where(p => p.Nombre.Contains(producto));
            }
            if (!String.IsNullOrEmpty(sku))
            {
                productos = productos.Where(p => p.SKU == sku);
            }
            if (upc != null)
            {
                productos = productos.Where(p => p.UPC == upc);
            }
            
            return View(await productos.ToListAsync());
        }

        // GET: Productos/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Productos == null)
            {
                return NotFound();
            }
            ProductoViewModel producto = new ProductoViewModel();
            var p = await _context.Productos
                .Include(p => p.Categoria)
                .Include(p => p.Descuento)
                .Include(p => p.Inventario)
                .Include(p => p.Marca)
                .FirstOrDefaultAsync(m => m.ProductoId == id);

            var i = await _context.Inventarios.FindAsync(p.InventarioId);

            producto.Inventario = i;
            producto.Producto = p;

            if (producto == null)
            {
                return NotFound();
            }

            return View(producto);
        }

        // GET: Productos/Create
        public IActionResult Create()
        {
            ViewData["CategoriaId"] = new SelectList(_context.Categorias, "CategoriaId", "Nombre");
            ViewData["DescuentoId"] = new SelectList(_context.Descuentos, "DescuentoId", "Nombre");
            ViewData["InventarioId"] = new SelectList(_context.Inventarios, "InventarioId", "Descripcion");
            ViewData["MarcaId"] = new SelectList(_context.Marcas, "MarcaId", "Nombre");
            return View();
        }

        // POST: Productos/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ProductoId,Nombre,Descripcion,Color,SKU,UPC,InventarioId,CategoriaId,MarcaId,Precio,DescuentoId,Creado,Modificado,Eliminado")] Producto producto)
        {
            var mensajeError = await ObtenerErrorProducto(producto);

            ViewData["CategoriaId"] = new SelectList(_context.Categorias, "CategoriaId", "Nombre", producto.CategoriaId);
            ViewData["DescuentoId"] = new SelectList(_context.Descuentos, "DescuentoId", "Nombre", producto.DescuentoId);
            ViewData["InventarioId"] = new SelectList(_context.Inventarios, "InventarioId", "Descripcion", producto.InventarioId);
            ViewData["MarcaId"] = new SelectList(_context.Marcas, "MarcaId", "Nombre", producto.MarcaId);

            if (mensajeError != "") 
            {
                ViewData["ErrorMessage"] = mensajeError;
                return View(producto);
            }
            
            if (ModelState.IsValid)
            {
                var inventarioId = await CrearInventario();
                producto.InventarioId = inventarioId.InventarioId;
                producto.Creado = DateTime.Now;
                _context.Add(producto);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            
            return View(producto);
        }

        // GET: Productos/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Productos == null)
            {
                return NotFound();
            }

            var producto = await _context.Productos.FindAsync(id);
            if (producto == null)
            {
                return NotFound();
            }
            ViewData["CategoriaId"] = new SelectList(_context.Categorias, "CategoriaId", "Nombre", producto.CategoriaId);
            ViewData["DescuentoId"] = new SelectList(_context.Descuentos, "DescuentoId", "Nombre", producto.DescuentoId);
            ViewData["InventarioId"] = new SelectList(_context.Inventarios, "InventarioId", "Descripcion", producto.InventarioId);
            ViewData["MarcaId"] = new SelectList(_context.Marcas, "MarcaId", "Nombre", producto.MarcaId);
            return View(producto);
        }

        // POST: Productos/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ProductoId,Nombre,Descripcion,SKU,UPC,InventarioId,CategoriaId,MarcaId,Precio,DescuentoId,Creado,Modificado,Eliminado")] Producto producto)
        {
            if (id != producto.ProductoId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    producto.Modificado = DateTime.Now;
                    _context.Update(producto);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProductoExists(producto.ProductoId))
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
            ViewData["CategoriaId"] = new SelectList(_context.Categorias, "CategoriaId", "Nombre", producto.CategoriaId);
            ViewData["DescuentoId"] = new SelectList(_context.Descuentos, "DescuentoId", "Nombre", producto.DescuentoId);
            ViewData["InventarioId"] = new SelectList(_context.Inventarios, "InventarioId", "Descripcion", producto.InventarioId);
            ViewData["MarcaId"] = new SelectList(_context.Marcas, "MarcaId", "Nombre", producto.MarcaId);
            return View(producto);
        }

        // GET: Productos/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Productos == null)
            {
                return NotFound();
            }

            var producto = await _context.Productos
                .Include(p => p.Categoria)
                .Include(p => p.Descuento)
                .Include(p => p.Inventario)
                .Include(p => p.Marca)
                .FirstOrDefaultAsync(m => m.ProductoId == id);
            if (producto == null)
            {
                return NotFound();
            }

            return View(producto);
        }

        // POST: Productos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Productos == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Productos'  is null.");
            }
            var producto = await _context.Productos.FindAsync(id);
            if (producto != null)
            {
                _context.Productos.Remove(producto);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ProductoExists(int id)
        {
          return (_context.Productos?.Any(e => e.ProductoId == id)).GetValueOrDefault();
        }

        private async Task<Inventario> CrearInventario()
        {
            Inventario inventario = new Inventario();
            inventario.Creado = DateTime.Now;
            _context.Add(inventario);
            await _context.SaveChangesAsync();
            var inventarioId = await _context.Inventarios.OrderByDescending(m => m.InventarioId).FirstAsync();


            return inventarioId;
        }

        private async Task<String> ObtenerErrorProducto(Producto producto)
        { 

            if (await _context.Productos.AnyAsync(a => a.Nombre == producto.Nombre))
            {
                return("Error este producto ya existe: verifique el Nombre ");
            }


            else if (await _context.Productos.AnyAsync(a => a.SKU == producto.SKU))
            {
                return ("Error este producto ya existe: verifique el SKU");

            }
            else if (await _context.Productos.AnyAsync(a => a.UPC == producto.UPC))
            {
                return ("Error este producto ya existe: verifique el UPC");
            }
            else return ("");
        }
    }
}



