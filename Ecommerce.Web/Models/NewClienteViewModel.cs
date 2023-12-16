namespace Ecommerce.Web.Models
{
    public class NewClienteViewModel
    {
        public Cliente Cliente { get; set; }
        public Direccion Direccion { get; set; }

        public List<Direccion>? Direcciones {get; set;}
    }
}
