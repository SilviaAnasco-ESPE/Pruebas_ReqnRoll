using System.ComponentModel.DataAnnotations;

namespace TDDTestingMVC2.Data
{
    public class Cliente
    {
        [Required]
        public int Codigo { get; set; }

        [Required(ErrorMessage = "El campo Cédula está vacío.")]
        [RegularExpression(@"^[0-9]+$", ErrorMessage = "Solo se admiten números en el campo Cédula.")]
        [StringLength(10, MinimumLength = 10, ErrorMessage = "La cédula debe tener exactamente 10 dígitos.")]
        public String Cedula { get; set; }

        [Required(ErrorMessage = "El campo Apellidos está vacío.")]
        [RegularExpression(@"^[A-Za-zÁÉÍÓÚáéíóúÑñ\s]+$", ErrorMessage = "Solo se admiten letras y espacios en el campo Apellidos.")]
        public String Apellidos { get; set; }

        [Required(ErrorMessage = "El campo Nombres está vacío.")]
        [RegularExpression(@"^[A-Za-zÁÉÍÓÚáéíóúÑñ\s]+$", ErrorMessage = "Solo se admiten letras y espacios en el campo Nombres.")]
        public String Nombres { get; set; }

        [Required(ErrorMessage = "El campo Fecha de nacimiento está vacío.")]
        public DateTime FechaNacimiento { get; set; }

        [Required(ErrorMessage = "El campo Mail está vacío.")]
        [RegularExpression(@"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,5}$", ErrorMessage = "Por favor, ingresar un correo en el formato válido.")]
        public String Mail { get; set; }

        [Required(ErrorMessage = "El campo Teléfono está vacío.")]
        [RegularExpression(@"^[0-9]+$", ErrorMessage = "Solo se admiten números en el campo Teléfono.")]
        [StringLength(10, MinimumLength = 10, ErrorMessage = "La teléfono debe tener exactamente 10 dígitos")]
        public String Telefono { get; set; }

        [Required(ErrorMessage = "El campo Dirección está vacío.")]
        public String Direccion { get; set; }
        
        [Required]
        public Boolean Estado { get; set; }
    }
}
