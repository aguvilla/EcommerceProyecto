using Ecommerce.Web.Models;

namespace Ecommerce.Web.ViewModels
{
    public class ProductoViewModel
    {
        public Producto? Producto { get; set; }
        public Marca? Marca { get; set; }
        public Categoria?Categoria { get; set; }
        public Inventario? Inventario { get; set; }
        public Descuento? Descuento { get; set; }
    }
}
