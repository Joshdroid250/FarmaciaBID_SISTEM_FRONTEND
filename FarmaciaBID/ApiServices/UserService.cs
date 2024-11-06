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
    public class UserService : BaseService<Users>
    {
        public UserService() : base(ApiConfig.ApiConfig.Instance.BaseUrl) { }

        public Task<List<Users>> GetAllAsync()
        {
            return GetAllAsync("/api/Usuarios");
        }

        public Task<Users> GetByIdAsync(int id)
        {
            return GetByIdAsync("/api/Usuarios", id);
        }

        public Task CreateAsync(Users user)
        {
            return CreateAsync("/api/Usuarios", user);
        }

        public Task UpdateAsync(Users user, int id)
        {
            return UpdateAsync("/api/Usuarios", id, user);
        }

        public Task DeleteAsync(int id)
        {
            return DeleteAsync("/api/Usuarios", id);
        }
    }
}