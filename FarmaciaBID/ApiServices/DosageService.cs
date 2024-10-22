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
    public class DosageService : BaseService<Dosage>
    {
        public DosageService() : base(ApiConfig.ApiConfig.Instance.BaseUrl) { }

        public Task<List<Dosage>> GetAllDosagesAsync()
        {
            return GetAllAsync("/api/Dosificaciones");
        }

        public Task<Dosage> GetDosageByIdAsync(int id)
        {
            return GetByIdAsync("/api/Dosificaciones", id);
        }

        public Task CreateDosageAsync(Dosage dosage)
        {
            return CreateAsync("/api/Dosificaciones", dosage);
        }

        public Task UpdateDosageAsync(Dosage dosage, int id)
        {
            return UpdateAsync("/api/Dosificaciones", id, dosage);
        }

        public Task DeleteDosageAsync(int id)
        {
            return DeleteAsync("/api/Dosificaciones", id);
        }
    }
}