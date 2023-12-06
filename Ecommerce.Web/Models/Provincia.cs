using System;
using System.Collections.Generic;

namespace Ecommerce.Web.Models;

public partial class Provincia
{
    public int ProvinciaId { get; set; }

    public string Descripcion { get; set; } = null!;

    public virtual ICollection<Direccion> Direcciones { get; set; } = new List<Direccion>();

    public virtual ICollection<Localidad> Localidades { get; set; } = new List<Localidad>();
}
