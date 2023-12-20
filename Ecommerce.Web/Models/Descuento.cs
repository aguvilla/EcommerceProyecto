using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Ecommerce.Web.Models;

public partial class Descuento
{
    public int DescuentoId { get; set; }

    public string Nombre { get; set; } = null!;

    public string? Descripcion { get; set; }

    public bool? Activo { get; set; }
    
    [Range(0,100),Required(ErrorMessage ="Se requiere un numero entre 0-100"),DisplayName("Porcentaje")]
    public int Porcentaje { get; set; }

    public DateTime? Creado { get; set; }

    public DateTime? Modificado { get; set; }

    public DateTime? Eliminado { get; set; }

    public virtual ICollection<Producto> Productos { get; set; } = new List<Producto>();
}
