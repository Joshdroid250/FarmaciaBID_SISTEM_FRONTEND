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
    public class PatientsService
    {
        private readonly string apiUrl = ApiConfig.ApiConfig.Instance.BaseUrl; // Reemplaza con la URL real del servicio REST


        public async Task<List<Patients>> GetAllPacientes()
        {
            using (var client = new HttpClient())
            {
                // Desactivar la validación del certificado
                System.Net.ServicePointManager.ServerCertificateValidationCallback = delegate { return true; };
                client.BaseAddress = new Uri(apiUrl);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                HttpResponseMessage response = await client.GetAsync("/api/Pacientes");

                if (response.IsSuccessStatusCode)
                {
                    string json = await response.Content.ReadAsStringAsync();
                    List<Patients> patients = JsonConvert.DeserializeObject<List<Patients>>(json);
                    return patients;
                }
                else
                {
                    // Manejar el caso en que la solicitud no sea exitosa
                    throw new Exception($"Error al obtener la lista de pacientes: {response.StatusCode}");
                }


            }
        }
    }
}