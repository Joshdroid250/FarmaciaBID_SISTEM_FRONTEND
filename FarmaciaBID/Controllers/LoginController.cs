using FarmaciaBID.ApiServices;
using FarmaciaBID.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace FarmaciaBID.Controllers
{
    public class LoginController : Controller
    {

        private readonly LoginService _LoginService;

        public LoginController()
        {
            _LoginService = new LoginService();
        }
        public ActionResult Index()
        {
            return View();
        }


        public async Task<ActionResult> Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> Login(LoginModel login)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    string token = await _LoginService.LoginAsync(login);
                    Session["AuthToken"] = token; // Guarda el token en la sesión
                    return RedirectToAction("Index", "Home");
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", $"Error al iniciar sesión: {ex.Message}");
                }
            }
            return View("Login");
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