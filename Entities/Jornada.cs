namespace WebApiKalum.Entities
{
    public class Jornada
    {
        public string Momento { get; set; }
        public string JornadaId { get; set; } 
        public string Descripción { get; set; }
        public virtual List<Aspirante> Aspirantes { get; set; }
        public virtual List<Inscripcion> Inscripciones { get; set; }
    }
}