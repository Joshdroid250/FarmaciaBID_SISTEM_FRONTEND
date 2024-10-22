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

        public Task<List<Users>> GetAllUsersAsync()
        {
            return GetAllAsync("/api/Usuarios");
        }

        public Task<Users> GetUserByIdAsync(int id)
        {
            return GetByIdAsync("/api/Usuarios", id);
        }

        public Task CreateUserAsync(Users user)
        {
            return CreateAsync("/api/Usuarios", user);
        }

        public Task UpdateUserAsync(Users user, int id)
        {
            return UpdateAsync("/api/Usuarios", id, user);
        }

        public Task DeleteUserAsync(int id)
        {
            return DeleteAsync("/api/Usuarios", id);
        }
    }
}