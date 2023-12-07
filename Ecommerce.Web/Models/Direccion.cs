using System;
using System.Collections.Generic;

namespace Ecommerce.Web.Models;

public partial class Direccion
{
    public int DireccionId { get; set; }

    public string Calle { get; set; } = null!;

    public int Altura { get; set; }

    public string Barrio { get; set; } = null!;

    public string? Partido { get; set; }

    public int LocalidadId { get; set; }

    public int ProvinciaId { get; set; }

    public int CodigoPostal { get; set; }

    public int ClienteId { get; set; }

    public virtual Cliente? Cliente { get; set; } = null!;

    public virtual ICollection<Empleado> Empleados { get; set; } = new List<Empleado>();

    public virtual Localidad? Localidad { get; set; } = null!;

    public virtual Provincia? Provincia { get; set; } = null!;
}
