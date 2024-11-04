using Newtonsoft.Json;
using FarmaciaBID.Models;
using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace FarmaciaBID.ApiServices
{
    public class LoginService : BaseService<LoginModel>
    {
        // Elimina la declaración redundante de `client`
        public LoginService() : base(ApiConfig.ApiConfig.Instance.BaseUrl) { }

        public async Task<LoginResponse> LoginAsync(LoginModel login)
        {
            string json = JsonConvert.SerializeObject(login);
            HttpContent content = new StringContent(json, Encoding.UTF8, "application/json");
            HttpResponseMessage response = await client.PostAsync("/api/Auth/login", content);

            if (response.IsSuccessStatusCode)
            {
                string responseJson = await response.Content.ReadAsStringAsync();
                // Deserializa la respuesta en un objeto LoginResponse
                var result = JsonConvert.DeserializeObject<LoginResponse>(responseJson);
                return result; // Devuelve el objeto LoginResponse que incluye el token y el ID
            }
            else
            {
                throw new Exception($"Error al iniciar sesión: {response.StatusCode} - {response.ReasonPhrase}");
            }
        }
    }
}
