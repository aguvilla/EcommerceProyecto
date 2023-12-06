using System;
using System.Collections.Generic;

namespace Ecommerce.Web.Models;

public partial class ClienteMetodosPago
{
    public int MetodoPagoId { get; set; }

    public int ClienteId { get; set; }

    public string Metodo { get; set; } = null!;

    public string? Proveedor { get; set; }

    public virtual Cliente Cliente { get; set; } = null!;
}
