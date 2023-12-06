using System;
using System.Collections.Generic;

namespace Ecommerce.Web.Models;

public partial class Localidad
{
    public int LocalidadId { get; set; }

    public string Descripcion { get; set; } = null!;

    public int ProvinciaId { get; set; }

    public virtual ICollection<Direccion> Direcciones { get; set; } = new List<Direccion>();

    public virtual Provincia Provincia { get; set; } = null!;
}
