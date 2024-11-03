using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FarmaciaBID.Models
{
    public class LotPharmaView
    {
        public int idLoteFarmaco { get; set; }
        public string nombreLoteFarmaco { get; set; }
        public string descripcion { get; set; }
        public int concentracion { get; set; }
        public int cantidad { get; set; }
        public string descripcionLoteFarmaco { get; set; }
        public int idPresentacion { get; set; }
        public string nombrePresentacion { get; set; }
        public string descripcionPresentacion { get; set; }
        public string idMedidas { get; set; }
        public string nombreMedidas { get; set; }
        public string idDosificacion { get; set; }
        public string nombreDosificacion { get; set; }
    }
}