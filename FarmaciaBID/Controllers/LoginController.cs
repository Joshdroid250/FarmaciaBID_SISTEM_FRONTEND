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
                    // Obtiene el objeto LoginResponse que incluye el token y el ID
                    var loginResponse = await _LoginService.LoginAsync(login);

                    // Guarda el token y el ID en la sesión
                    Session["AuthToken"] = loginResponse.token; // Guarda el token en la sesión
                    Session["UserId"] = loginResponse.id; // Guarda el ID en la sesión

                    return RedirectToAction("Index", "Home");
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", $"Error al iniciar sesión: {ex.Message}");
                }
            }
            return View("Login");
        }

        public ActionResult Logout()
        {
            // Limpiar la sesión
            Session.Clear();
            Session.Abandon();

            // Redirigir al inicio de sesión
            return RedirectToAction("Login", "Login");
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