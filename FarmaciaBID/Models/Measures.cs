using System;

namespace FarmaciaBID.Models
{
    public class Measures
    {
        public int idMedidas { get; set; }
        public string nombre { get; set; }
        public int idUsuarioCreacion { get; set; }
        public DateTime fechaCreacion { get; set; }
        public int idUsuarioModificacion { get; set; }
        public DateTime fechaModificacion { get; set; }
    }
}