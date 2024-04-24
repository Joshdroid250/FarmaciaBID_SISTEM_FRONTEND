using Newtonsoft.Json;
using FarmaciaBID.Models;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;


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
    }
}