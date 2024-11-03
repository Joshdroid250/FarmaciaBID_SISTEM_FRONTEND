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

        public async Task<string> LoginAsync(LoginModel login)
        {
            // Usa el `client` de la clase base
            string json = JsonConvert.SerializeObject(login);
            HttpContent content = new StringContent(json, Encoding.UTF8, "application/json");
            HttpResponseMessage response = await client.PostAsync("/api/Auth/login", content);

            if (response.IsSuccessStatusCode)
            {
                string responseJson = await response.Content.ReadAsStringAsync();
                var result = JsonConvert.DeserializeObject<dynamic>(responseJson);
                return result.token;
            }
            else
            {
                throw new Exception($"Error al iniciar sesión: {response.StatusCode} - {response.ReasonPhrase}");
            }
        }
    }
}
