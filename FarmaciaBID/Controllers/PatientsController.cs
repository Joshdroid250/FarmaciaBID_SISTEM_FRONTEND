using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Threading.Tasks;
using FarmaciaBID.Models;
using FarmaciaBID.ApiServices.ApiConfig;
using System.Net.Http;
using FarmaciaBID.ApiServices;

namespace FarmaciaBID.Controllers
{
    public class PatientsController : Controller
    {
        private readonly PatientsService patientsService = new PatientsService();
        public async Task<ActionResult> ViewPatients()
        {

            var employe = await patientsService.GetAllPacientes();

            return View(employe);
        }
    }
}
