using System;
using System.Collections.Generic;

namespace Ecommerce.Web.Models;

public partial class Producto
{
    public int ProductoId { get; set; }

    public string? Nombre { get; set; }

    public string? Descripcion { get; set; }
    public string? Color {  get; set; }
    public string? SKU { get; set; }

    public int? UPC { get; set; }

    public int? InventarioId { get; set; }

    public int CategoriaId { get; set; }

    public int MarcaId { get; set; }

    public double Precio { get; set; }

    public int DescuentoId { get; set; }

    public DateTime? Creado { get; set; }

    public DateTime? Modificado { get; set; }

    public DateTime? Eliminado { get; set; }

    public virtual Categoria? Categoria { get; set; } = null!;

    public virtual Descuento? Descuento { get; set; } = null!;

    public virtual Inventario? Inventario { get; set; } = null!;

    public virtual Marca? Marca { get; set; } = null!;

    public virtual ICollection<OrdenCarrito> OrdenesCarritos { get; set; } = new List<OrdenCarrito>();
}
