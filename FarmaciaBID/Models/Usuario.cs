using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FarmaciaBID.Models
{
    public class Usuario
    {
        public int IdUsuario { get; set; }
        public string Username { get; set; }
        public string Pwd { get; set; }
        public string PasswordSalt { get; set; }
        public string Nombre { get; set; }
        public int Estado { get; set; }
    }
}