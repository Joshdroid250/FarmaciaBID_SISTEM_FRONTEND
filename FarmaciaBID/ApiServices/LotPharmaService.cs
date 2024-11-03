
using FarmaciaBID.Models;

using System.Collections.Generic;

using System.Threading.Tasks;


namespace FarmaciaBID.ApiServices
{
    public class LotPharmaService : BaseService<LotPharma>
    {
        public LotPharmaService() : base(ApiConfig.ApiConfig.Instance.BaseUrl) { }

        public Task<List<LotPharma>> GetAllAsync()
        {
            return GetAllAsync("/api/LoteFarmacos");
        }

        public Task<List<LotPharmaView>> GetAllViewAsync()
        {
            return GetAllAsync<LotPharmaView>("/api/LoteFarmacos/LoteFarmacosView");
        }

        public Task<LotPharma> GetByIdAsync(int id)
        {
            return GetByIdAsync("/api/LoteFarmacos", id);
        }

        public Task CreateAsync(LotPharma lotefarma)
        {
            return CreateAsync("/api/LoteFarmacos", lotefarma);
        }

        public Task UpdateAsync(LotPharma lotefarma, int id)
        {
            return UpdateAsync("/api/LoteFarmacos", id, lotefarma);
        }

        public Task DeleteAsync(int id)
        {
            return DeleteAsync("/api/LoteFarmacos", id);
        }
    }
}