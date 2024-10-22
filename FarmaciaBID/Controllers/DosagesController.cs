using FarmaciaBID.ApiServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Threading.Tasks;
using FarmaciaBID.Models;
using FarmaciaBID.ApiServices.ApiConfig;
using System.Net.Http;

namespace FarmaciaBID.Controllers
{
    public class DosagesController : Controller
    {
        private readonly DosageService _dosageService;
        private readonly string apiUrl = ApiConfig.Instance.BaseUrl;
        public DosagesController()
        {
            _dosageService = new DosageService();
        }

        

        public async Task<ActionResult> ViewDosage()
        {
            var dosages = await _dosageService.GetAllDosagesAsync();
            return View(dosages);
        }


        public async Task<ActionResult> CreateDosages()
        {
            return View();
        }

    }
}
