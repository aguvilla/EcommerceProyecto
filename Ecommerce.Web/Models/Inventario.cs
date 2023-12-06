using System;
using System.Collections.Generic;

namespace Ecommerce.Web.Models;

public partial class Inventario
{
    public int InventarioId { get; set; }

    public int Cantidad { get; set; }

    public string? Descripcion { get; set; }

    public DateTime? Creado { get; set; }

    public DateTime? Modificado { get; set; }

    public DateTime? Eliminado { get; set; }

    public virtual ICollection<Producto> Productos { get; set; } = new List<Producto>();
}
