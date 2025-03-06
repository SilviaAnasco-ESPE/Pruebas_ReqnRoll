using System.ComponentModel.DataAnnotations;

namespace TDDTestingMVC2.Data
{
    public class Pedidos
    {
        [Required]
        public int PedidoID { get; set; }

        [Required(ErrorMessage = "El campo ClienteID está vacío.")]
        [RegularExpression(@"^[0-9]+$", ErrorMessage = "Solo se admiten números en el campo ClienteID.")]
        public int ClienteID { get; set; }

        [Required(ErrorMessage = "El campo Fecha de pedido está vacío.")]
        public DateTime FechaPedido { get; set; }

        [Required(ErrorMessage = "El campo Monto está vacío.")]
        [RegularExpression(@"^\d+(\,\d{1,2})?$", ErrorMessage = "El monto debe ser un número decimal con hasta dos decimales.")]
        public decimal Monto { get; set; }

        [Required(ErrorMessage = "El campo Estado está vacío.")]
        public String Estado { get; set; }

    }
}
