﻿using System;
using System.Configuration;

namespace FarmaciaBID.ApiServices.ApiConfig
{
    public class ApiConfig
    {
        private static readonly Lazy<ApiConfig> lazy = new Lazy<ApiConfig>(() => new ApiConfig());

        public static ApiConfig Instance { get { return lazy.Value; } }

        public string BaseUrl { get; private set; }

        private ApiConfig()
        {
            // Constructor privado para evitar la creación de instancias fuera de la clase
            // Configura la URL base según el entorno (desarrollo o producción)
            string environment = ConfigurationManager.AppSettings["Environment"];

            switch (environment?.ToLower())
            {
                case "development":
                    BaseUrl = "https://farmaciaapisjm.azurewebsites.net";
                    break;
                case "deployment":
                    BaseUrl = "https://farmaciaapisjm.azurewebsites.net";
                    break;
                default:
                    throw new InvalidOperationException("Entorno no válido especificado en el archivo web.config");
            }
        }
    }
}