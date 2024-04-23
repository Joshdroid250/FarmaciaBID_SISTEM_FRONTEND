using FarmaciaBID.ApiServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Threading.Tasks;


namespace FarmaciaBID.Controllers
{
    public class DosagesController : Controller
    {
        private readonly DosageService dosageService = new DosageService();
        public async Task<ActionResult> ViewDosage()
        {
            
            var employe = await dosageService.GetAllDosage();

            return View(employe);
        }
    }
}
