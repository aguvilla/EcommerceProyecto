using System;
using System.Collections.Generic;

namespace Ecommerce.Web.Models;

public partial class Descuento
{
    public int DescuentoId { get; set; }

    public string Nombre { get; set; } = null!;

    public string? Descripcion { get; set; }

    public bool? Activo { get; set; }

    public DateTime? Creado { get; set; }

    public DateTime? Modificado { get; set; }

    public DateTime? Eliminado { get; set; }

    public virtual ICollection<Producto> Productos { get; set; } = new List<Producto>();
}
