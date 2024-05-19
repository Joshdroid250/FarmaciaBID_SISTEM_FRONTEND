using System;

namespace FarmaciaBID.Models
{
    public class Expediente
    {
        public int idExpediente { get; set; }
        public DateTime fechaCreacion { get; set; }
        public string notas { get; set; }
        public int estado { get; set; }
        public int idPaciente { get; set; }
    }
}
