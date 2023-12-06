using System;
using System.Collections.Generic;

namespace Ecommerce.Web.Models;

public partial class Orden
{
    public int OrdenId { get; set; }

    public int ClienteId { get; set; }

    public double Monto { get; set; }

    public int OrdenPagoId { get; set; }

    public DateTime? Creacion { get; set; }

    public DateTime? Modificacion { get; set; }

    public int OrdenProductosId { get; set; }

    public string Estado { get; set; } = null!;

    public virtual Cliente Cliente { get; set; } = null!;

    public virtual ICollection<OrdenCarrito> OrdenesCarritos { get; set; } = new List<OrdenCarrito>();

    public virtual ICollection<OrdenPago> OrdenesPagos { get; set; } = new List<OrdenPago>();
}
