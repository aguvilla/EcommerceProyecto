using Ecommerce.Web.Models;

namespace Ecommerce.Web.ViewModels
{
    public class NewClienteViewModel
    {
        public Cliente Cliente { get; set; }
        public Direccion Direccion { get; set; }

        public List<Direccion>? Direcciones { get; set; }
    }
}
