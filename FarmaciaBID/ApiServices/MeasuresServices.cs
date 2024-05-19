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
    public class MeasuresServices
    {
        private readonly string apiUrl = ApiConfig.ApiConfig.Instance.BaseUrl; // Reemplaza con la URL real del servicio REST


        public async Task<List<Measures>> GetAllMeasures()
        {
            using (var client = new HttpClient())
            {
                // Desactivar la validación del certificado
                System.Net.ServicePointManager.ServerCertificateValidationCallback = delegate { return true; };
                client.BaseAddress = new Uri(apiUrl);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                HttpResponseMessage response = await client.GetAsync("/api/Medidas");

                if (response.IsSuccessStatusCode)
                {
                    string json = await response.Content.ReadAsStringAsync();
                    List<Measures> medidas = JsonConvert.DeserializeObject<List<Measures>>(json);
                    return medidas;
                }
                else
                {
                    // Manejar el caso en que la solicitud no sea exitosa
                    throw new Exception($"Error al obtener la lista de Medidas: {response.StatusCode}");
                }


            }
        }


        public async Task CreateMeasures(Measures newMeasures)
        {
            using (var client = new HttpClient())
            {
                System.Net.ServicePointManager.ServerCertificateValidationCallback = delegate { return true; };
                client.BaseAddress = new Uri(apiUrl);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                string json = JsonConvert.SerializeObject(newMeasures);
                HttpContent content = new StringContent(json, Encoding.UTF8, "application/json");

                // Realizar la solicitud POST
                HttpResponseMessage response = await client.PostAsync("/api/Medidas", content);

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
                    throw new Exception($"Error al crear la medida. Código de estado: {response.StatusCode}");
                }
            }
        }


        public async Task<Measures> GetMeasuresById(int idMeasures)
        {
            using (var client = new HttpClient())
            {
                System.Net.ServicePointManager.ServerCertificateValidationCallback = delegate { return true; };
                client.BaseAddress = new Uri(apiUrl);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                HttpResponseMessage response = await client.GetAsync($"/api/Medidas/{idMeasures}");


                if (response.IsSuccessStatusCode)
                {
                    string json = await response.Content.ReadAsStringAsync();
                    Measures getByIdMeasures = JsonConvert.DeserializeObject<Measures>(json);
                    return getByIdMeasures;
                }
                else if (response.StatusCode == HttpStatusCode.Conflict)
                {
                    // Manejar el caso de duplicado
                    throw new Exception("");
                }
                else
                {
                    // Manejar otros casos de error
                    throw new Exception($"Error al obtener la medida con ID {idMeasures}: {response.StatusCode}");
                }


            }
        }


        public async Task UpdateMeasures(Measures editMeasures, int idMeasures)
        {
            using (var client = new HttpClient())
            {
                System.Net.ServicePointManager.ServerCertificateValidationCallback = delegate { return true; };
                client.BaseAddress = new Uri(apiUrl);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                string json = JsonConvert.SerializeObject(editMeasures);
                HttpContent content = new StringContent(json, Encoding.UTF8, "application/json");

                // Realizar la solicitud POST
                HttpResponseMessage response = await client.PutAsync($"/api/Medidas/{idMeasures}", content);

                // Verificar si la respuesta indica éxito basado en el código de estado
                if (response.IsSuccessStatusCode)
                {
                    // Respuesta exitosa
                    return;
                }
                else if (response.StatusCode == HttpStatusCode.Conflict)
                {
                    // Manejar el caso de duplicado
                    throw new Exception("Error al crear la medida. Ya existe un registro con los mismos datos.");
                }
                else
                {
                    // Manejar otros casos de error
                    throw new Exception($"Error al actualizar la medida. Código de estado: {response.StatusCode}");
                }
            }
        }
    }
}