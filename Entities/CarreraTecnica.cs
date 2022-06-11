using System.ComponentModel.DataAnnotations;

namespace WebApiKalum.Entities
{
    public class CarreraTecnica
    {
        public string CarreraId { get; set; }
        [Required (ErrorMessage = "El campo {0} es requerido")]
        [StringLength (128, MinimumLength = 5, ErrorMessage = "La cantidad maxima de caracteres es {1}, y minima de caracteres es {2}")]
        public string Nombre { get; set; }
        public virtual List<Aspirante> Aspirantes { get; set; }
        public virtual List<Incripcion> Inscripciones { get; set; }
    }
}