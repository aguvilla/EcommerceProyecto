using System;
using System.Collections.Generic;

namespace Ecommerce.Web.Models;

public partial class TipoDocumento
{
    public int TipoDocumentoId { get; set; }

    public string? Descripcion { get; set; }

    public virtual ICollection<Cliente> Clientes { get; set; } = new List<Cliente>();

    public virtual ICollection<Empleado> Empleados { get; set; } = new List<Empleado>();
}
