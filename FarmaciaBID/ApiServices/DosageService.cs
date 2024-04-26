using Newtonsoft.Json;
using FarmaciaBID.Models;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Net;

namespace FarmaciaBID.ApiServices
{
    public class DosageService
    {
        private readonly string apiUrl = ApiConfig.ApiConfig.Instance.BaseUrl; // Reemplaza con la URL real del servicio REST


        public async Task<List<Dosage>> GetAllDosage()
        {
            using (var client = new HttpClient())
            {
                // Desactivar la validación del certificado
                System.Net.ServicePointManager.ServerCertificateValidationCallback = delegate { return true; };
                client.BaseAddress = new Uri(apiUrl);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                HttpResponseMessage response = await client.GetAsync("/api/Dosificaciones");

                if (response.IsSuccessStatusCode)
                {
                    string json = await response.Content.ReadAsStringAsync();
                    List<Dosage> dosages = JsonConvert.DeserializeObject<List<Dosage>>(json);
                    return dosages;
                }
                else
                {
                    // Manejar el caso en que la solicitud no sea exitosa
                    throw new Exception($"Error al obtener la lista de Dosificacion: {response.StatusCode}");
                }

                
            }
        }


        public async Task CreateDosage(Dosage newDosage)
        {
            using (var client = new HttpClient())
            {
                System.Net.ServicePointManager.ServerCertificateValidationCallback = delegate { return true; };
                client.BaseAddress = new Uri(apiUrl);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                string json = JsonConvert.SerializeObject(newDosage);
                HttpContent content = new StringContent(json, Encoding.UTF8, "application/json");

                // Realizar la solicitud POST
                HttpResponseMessage response = await client.PostAsync("/api/Dosificaciones", content);

                // Verificar si la respuesta indica éxito basado en el código de estado
                if (response.IsSuccessStatusCode)
                {
                    // Respuesta exitosa
                    return;
                }
                else if (response.StatusCode == HttpStatusCode.Conflict)
                {
                    // Manejar el caso de duplicado
                    throw new Exception("Error al crear el donante. Ya existe un registro con los mismos datos.");
                }
                else
                {
                    // Manejar otros casos de error
                    throw new Exception($"Error al crear el donante. Código de estado: {response.StatusCode}");
                }
            }
        }
    }
}