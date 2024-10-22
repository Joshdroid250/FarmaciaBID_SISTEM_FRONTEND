using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FarmaciaBID.Models
{
    public class Dosage
    {
        public int IdDosificacion { get; set; }
        public string Nombre { get; set; }
        public int Estado { get; set; }
        public int IdUsuarioCreacion { get; set; }
        public Usuario UsuarioCreacion { get; set; }
        public DateTime FechaCreacion { get; set; }
        public int IdUsuarioModificacion { get; set; }
        public Usuario UsuarioModificacion { get; set; }
        public DateTime FechaModificacion { get; set; }
    }
}