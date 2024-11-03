
using FarmaciaBID.Models;

using System.Collections.Generic;

using System.Threading.Tasks;

namespace FarmaciaBID.ApiServices
{
    public class PresentacionesService : BaseService<Presentacion>
    {
        public PresentacionesService() : base(ApiConfig.ApiConfig.Instance.BaseUrl) { }

        public Task<List<Presentacion>> GetAllAsync()
        {
            return GetAllAsync("/api/Presentaciones");
        }

        public Task<List<LotPharmaView>> GetAllViewAsync()
        {
            return GetAllAsync<LotPharmaView>("/api/Presentaciones/PresentacionesView");
        }

        public Task<Presentacion> GetByIdAsync(int id)
        {
            return GetByIdAsync("/api/Presentaciones", id);
        }

        public Task CreateAsync(Presentacion presenta)
        {
            return CreateAsync("/api/Presentaciones", presenta);
        }

        public Task UpdateAsync(Presentacion presenta, int id)
        {
            return UpdateAsync("/api/Presentaciones", id, presenta);
        }

        public Task DeleteAsync(int id)
        {
            return DeleteAsync("/api/Presentaciones", id);
        }
    }
}