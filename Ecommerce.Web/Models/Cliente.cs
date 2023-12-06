using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Ecommerce.Web.Models;

public partial class Cliente
{
    public int ClienteId { get; set; }

    public string Apellido { get; set; } = null!;

    public string Nombre { get; set; } = null!;
    [DataType(DataType.Date)]
    public DateTime? FechaNacimiento { get; set; }

    public int TipoDocumentoId { get; set; }

    public int NumeroDocumento { get; set; }

    public string? RazonSocial { get; set; }

    public string Nacionalidad { get; set; } = null!;

    public string Celular { get; set; } = null!;

    public string? Telefono { get; set; }

    public string CorreoElectronico { get; set; } = null!;

    public DateTime? FechaInicio { get; set; }

    public DateTime? FechaModificacion { get; set; }

    public virtual ICollection<ClienteMetodosPago> ClienteMetodosPagos { get; set; } = new List<ClienteMetodosPago>();

    public virtual ICollection<Direccion> Direcciones { get; set; } = new List<Direccion>();

    public virtual ICollection<Orden> Ordenes { get; set; } = new List<Orden>();

    public virtual TipoDocumento TipoDocumento { get; set; } = null!;
}
