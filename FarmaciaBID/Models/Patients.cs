using System;

namespace FarmaciaBID.Models
{
    public class Patients
    {
        public int idPaciente { get; set; }
        public string nombres { get; set; }
        public string apellidos { get; set; }
        public DateTime fechaNacimiento { get; set; }
        public string sexo { get; set; }
        public string direccion { get; set; }
        public string telefono { get; set; }
        public string correo { get; set; }
        public int estado { get; set; }
    }
}