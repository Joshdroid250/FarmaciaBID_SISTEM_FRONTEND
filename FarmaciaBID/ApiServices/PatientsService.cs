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
    public class PatientsService : BaseService<Patients>
    {
        
        public PatientsService() : base(ApiConfig.ApiConfig.Instance.BaseUrl) { }
        public Task<List<Patients>> GetAllAsync()
        {
            return GetAllAsync("/api/Pacientes");
        }

        public Task<Patients> GetByIdAsync(int id)
        {
            return GetByIdAsync("/api/Pacientes", id);
        }

        public Task CreateAsync(Patients paciente)
        {
            return CreateAsync("/api/Pacientes", paciente);
        }

        public Task UpdateAsync(Patients paciente, int id)
        {
            return UpdateAsync("/api/Pacientes", id, paciente);
        }

        public Task DeleteAsync(int id)
        {
            return DeleteAsync("/api/Pacientes", id);
        }

    }
}