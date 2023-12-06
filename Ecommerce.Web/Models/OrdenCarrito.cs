using System;
using System.Collections.Generic;

namespace Ecommerce.Web.Models;

public partial class OrdenCarrito
{
    public int OrdenCarritoId { get; set; }

    public int OrdenId { get; set; }

    public int ProductoId { get; set; }

    public int Cantidad { get; set; }

    public DateTime? Creado { get; set; }

    public DateTime? Modificado { get; set; }

    public virtual Orden Orden { get; set; } = null!;

    public virtual Producto Producto { get; set; } = null!;
}
