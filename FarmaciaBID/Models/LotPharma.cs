using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FarmaciaBID.Models
{
    public class LotPharma
    {
        public int idLoteFarmaco { get; set; }
        public string nombre { get; set; }
        public string descripcion { get; set; }
        public int concentracion { get; set; }
        public int cantidad { get; set; }
        public int idPresentacion { get; set; }
    }
}