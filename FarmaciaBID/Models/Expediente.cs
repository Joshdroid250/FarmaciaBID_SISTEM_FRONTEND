using System;

namespace FarmaciaBID.Models
{
    public class Expediente
    {
        public int idExpediente { get; set; }
        public string notas { get; set; }
        public int estado { get; set; }
        public int idPaciente { get; set; }
        public int idUsuarioCreacion { get; set; }
        public DateTime fechaCreacion { get; set; }
        public int idUsuarioModificacion { get; set; }
        public DateTime fechaModificacion { get; set; }
    }
}
