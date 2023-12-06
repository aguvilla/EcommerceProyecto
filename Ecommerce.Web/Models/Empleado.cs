using System;
using System.Collections.Generic;

namespace Ecommerce.Web.Models;

public partial class Empleado
{
    public int EmpleadoId { get; set; }

    public string Cargo { get; set; } = null!;

    public string Apellido { get; set; } = null!;

    public string Nombre { get; set; } = null!;

    public DateTime? FechaNacimiento { get; set; }

    public int TipoDocumentoId { get; set; }

    public int NumeroDocumento { get; set; }

    public int DireccionId { get; set; }

    public string? RazonSocial { get; set; }

    public string Nacionalidad { get; set; } = null!;

    public string Celular { get; set; } = null!;

    public string? Telefono { get; set; }

    public string CorreoElectronico { get; set; } = null!;

    public DateTime? FechaInicio { get; set; }

    public DateTime? FechaModificacion { get; set; }

    public virtual Direccion Direccion { get; set; } = null!;

    public virtual TipoDocumento TipoDocumento { get; set; } = null!;
}
