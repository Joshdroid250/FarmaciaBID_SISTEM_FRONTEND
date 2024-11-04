using FarmaciaBID.ApiServices;
using FarmaciaBID.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using FarmaciaBID.Filters;

namespace FarmaciaBID.Controllers
{
    [AuthFilter]
    public class HomeController : Controller
    {

        private readonly LoginService _HomeService;



        public HomeController()
        {
            _HomeService = new LoginService();
        }
        public ActionResult Index()
        {
            return View();
        }

        public async Task<ActionResult> Login()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}