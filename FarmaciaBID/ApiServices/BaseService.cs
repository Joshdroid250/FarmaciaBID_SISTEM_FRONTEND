using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace FarmaciaBID.ApiServices
{
    public class BaseService<T>
    {
        private readonly string apiUrl;
        protected readonly HttpClient client;

        public BaseService(string baseUrl)
        {
            apiUrl = baseUrl;
            client = new HttpClient
            {
                BaseAddress = new Uri(apiUrl)
            };
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            ServicePointManager.ServerCertificateValidationCallback = delegate { return true; };
        }

        protected async Task<T> GetByIdAsync(string endpoint, int id)
        {
            HttpResponseMessage response = await client.GetAsync($"{endpoint}/{id}");
            return await HandleResponse<T>(response);
        }

        protected async Task<List<T>> GetAllAsync(string endpoint)
        {
            HttpResponseMessage response = await client.GetAsync(endpoint);
            return await HandleResponse<List<T>>(response);
        }

        protected async Task CreateAsync(string endpoint, T item)
        {
            string json = JsonConvert.SerializeObject(item);
            HttpContent content = new StringContent(json, Encoding.UTF8, "application/json");
            HttpResponseMessage response = await client.PostAsync(endpoint, content);

            if (!response.IsSuccessStatusCode)
            {
                throw new Exception($"Error al crear el registro: {response.StatusCode}");
            }
        }

        protected async Task UpdateAsync(string endpoint, int id, T item)
        {
            string json = JsonConvert.SerializeObject(item);
            HttpContent content = new StringContent(json, Encoding.UTF8, "application/json");
            HttpResponseMessage response = await client.PutAsync($"{endpoint}/{id}", content);

            if (!response.IsSuccessStatusCode)
            {
                throw new Exception($"Error al actualizar el registro: {response.StatusCode}");
            }
        }

        protected async Task DeleteAsync(string endpoint, int id)
        {
            HttpResponseMessage response = await client.DeleteAsync($"{endpoint}/{id}");

            if (!response.IsSuccessStatusCode)
            {
                throw new Exception($"Error al eliminar el registro: {response.StatusCode}");
            }
        }

        private async Task<TResponse> HandleResponse<TResponse>(HttpResponseMessage response)
        {
            if (response.IsSuccessStatusCode)
            {
                string json = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<TResponse>(json);
            }
            else
            {
                throw new Exception($"Error en la solicitud: {response.StatusCode} - {response.ReasonPhrase}");
            }
        }


        public async Task<T> GetAsync<T>(string endpoint, int id)
        {
            var response = await client.GetAsync($"{endpoint}/{id}");
            response.EnsureSuccessStatusCode();

            var content = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<T>(content);
        }


        public async Task<List<T>> GetAllAsync<T>(string endpoint)
        {
            var response = await client.GetAsync(endpoint);
            response.EnsureSuccessStatusCode();

            var content = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<List<T>>(content);
        }

    }
}