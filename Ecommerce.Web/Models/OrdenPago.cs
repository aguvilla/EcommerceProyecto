using System;
using System.Collections.Generic;

namespace Ecommerce.Web.Models;

public partial class OrdenPago
{
    public int OrdenPagoId { get; set; }

    public int OrdenId { get; set; }

    public double Monto { get; set; }

    public string? Descripcion { get; set; }

    public string? Metodo { get; set; }

    public string? Estado { get; set; }

    public DateTime? Creado { get; set; }

    public DateTime? Modificacion { get; set; }

    public virtual Orden Orden { get; set; } = null!;
}
