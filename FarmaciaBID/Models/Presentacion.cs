using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FarmaciaBID.Models
{
    public class Presentacion
    {
        public int idPresentacion { get; set; }
        public string nombre { get; set; }
        public string descripcion { get; set; }
        public int idMedidas { get; set; }
        public int idDosificacion { get; set; }
        public int idUsuarioCreacion { get; set; }
        public DateTime fechaCreacion { get; set; }
        public int idUsuarioModificacion { get; set; }
        public DateTime fechaModificacion { get; set; }
    }
}