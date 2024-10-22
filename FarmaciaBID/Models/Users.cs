﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FarmaciaBID.Models
{
    public class Users
    {
        public int idUsuario { get; set; }
        public string username { get; set; }
        public string pwd { get; set; }
        public string passwordSalt { get; set; }
        public string nombre { get; set; }
        public int estado { get; set; }
    }
}