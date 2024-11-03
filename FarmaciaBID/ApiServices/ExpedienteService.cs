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


    public class ExpedienteService : BaseService<Expediente>
    {

        public ExpedienteService() : base(ApiConfig.ApiConfig.Instance.BaseUrl) { }

        public Task<List<ExpedienteView>> GetAllExpedientesAsync()
        {
            return GetAllAsync<ExpedienteView>("/api/Expedientes/ExpedienteView");
        }

        public Task<Expediente> GetExpedientesByIdAsync(int id)
        {
            return GetByIdAsync("/api/Expedientes", id);
        }

        public Task CreateExpedientesAsync(Expediente expediente)
        {
            return CreateAsync("/api/Expedientes", expediente);
        }

        public Task UpdateExpedientesAsync(Expediente expediente, int id)
        {
            return UpdateAsync("/api/Expedientes", id, expediente);
        }

        public Task DeleteExpedientesAsync(int id)
        {
            return DeleteAsync("/api/Expedientes", id);
        }


    }
}