using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using EmpresaExportaciones.Context;
using EmpresaExportaciones.Models;

namespace EmpresaExportaciones.Controllers
{
    public class PedidoController : Controller
    {
        private readonly EmpresaDatabaseContext _context;

        public PedidoController(EmpresaDatabaseContext context)
        {
            _context = context;
        }

        // GET: Pedido
        public async Task<IActionResult> Index()
        {
            var empresaDatabaseContext = _context.Pedidos.Include(p => p.Cliente).Include(p => p.Producto);
            return View(await empresaDatabaseContext.ToListAsync());
        }

        // GET: Pedido/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Pedidos == null)
            {
                return NotFound();
            }

            var pedido = await _context.Pedidos
                .Include(p => p.Cliente)
                .Include(p => p.Producto)
                .FirstOrDefaultAsync(m => m.id == id);
            if (pedido == null)
            {
                return NotFound();
            }

            return View(pedido);
        }

        // GET: Pedido/Create
        public IActionResult Create()
        {
            ViewData["ClienteId"] = new SelectList(_context.Clientes, "id", "apellidoCliente");
            ViewData["ProductoId"] = new SelectList(_context.Productos, "id", "descripcion");
            return View();
        }

        // POST: Pedido/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("id,ClienteId,ProductoId,cantProducto,precio,fecha,estado")] Pedido pedido)
        {
            var productoActual = await _context.Productos.FindAsync(pedido.ProductoId);
            if (productoActual != null)
            {
                if (productoActual.stock >= pedido.cantProducto)
                {
                    productoActual.stock -= pedido.cantProducto;
                    _context.Add(pedido);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                    ViewData["ClienteId"] = new SelectList(_context.Clientes, "id", "apellidoCliente", pedido.ClienteId);
                    ViewData["ProductoId"] = new SelectList(_context.Productos, "id", "descripcion", pedido.ProductoId);
                    return View(pedido);
                }
                else
                {
                    ModelState.AddModelError("","No hay suficiente stock disponible para este producto.");
                    ViewData["ClienteId"] = new SelectList(_context.Clientes, "id", "apellidoCliente", pedido.ClienteId);
                    ViewData["ProductoId"] = new SelectList(_context.Productos, "id", "descripcion", pedido.ProductoId);
                    return View(pedido);
                }
            }
            return NotFound();
        }

        // GET: Pedido/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Pedidos == null)
            {
                return NotFound();
            }

            var pedido = await _context.Pedidos.FindAsync(id);
            if (pedido == null)
            {
                return NotFound();
            }
            ViewData["ClienteId"] = new SelectList(_context.Clientes, "id", "apellidoCliente", pedido.ClienteId);
            ViewData["ProductoId"] = new SelectList(_context.Productos, "id", "descripcion", pedido.ProductoId);
            return View(pedido);
        }

        // POST: Pedido/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("id,ClienteId,ProductoId,cantProducto,precio,fecha,estado")] Pedido pedido)
        {
            var pedidoActual = await _context.Pedidos.FindAsync(id);

            if (id != pedido.id && pedidoActual != null)
            {
                return NotFound();
            }

            try
            {
                if (pedidoActual.estado == Estado.PENDIENTE)
                {
                    pedidoActual.ClienteId = pedido.ClienteId;
                    pedidoActual.ProductoId = pedido.ProductoId;
                    pedidoActual.cantProducto = pedido.cantProducto;
                    pedidoActual.precio = pedido.precio;
                    pedidoActual.fecha = pedido.fecha;
                    pedidoActual.estado = pedido.estado;

                    _context.Update(pedidoActual);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    ModelState.AddModelError("", "No es posible cambiar el estado del pedido.");
                }
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PedidoExists(pedido.id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            return RedirectToAction(nameof(Index));

            ViewData["ClienteId"] = new SelectList(_context.Clientes, "id", "apellidoCliente", pedido.ClienteId);
            ViewData["ProductoId"] = new SelectList(_context.Productos, "id", "descripcion", pedido.ProductoId);
            return View(pedido);
        }

        // GET: Pedido/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Pedidos == null)
            {
                return NotFound();
            }

            var pedido = await _context.Pedidos
                .Include(p => p.Cliente)
                .Include(p => p.Producto)
                .FirstOrDefaultAsync(m => m.id == id);
            if (pedido == null)
            {
                return NotFound();
            }

            return View(pedido);
        }

        // POST: Pedido/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Pedidos == null)
            {
                return Problem("Entity set 'EmpresaDatabaseContext.Pedidos'  is null.");
            }
            var pedido = await _context.Pedidos.FindAsync(id);
            if (pedido != null)
            {
                _context.Pedidos.Remove(pedido);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PedidoExists(int id)
        {
          return (_context.Pedidos?.Any(e => e.id == id)).GetValueOrDefault();
        }
    }
}
