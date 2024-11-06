using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Threading.Tasks;
using FarmaciaBID.Models;
using FarmaciaBID.ApiServices.ApiConfig;
using System.Net.Http;
using FarmaciaBID.ApiServices;
using System;



namespace FarmaciaBID.Controllers
{
    public class UsersController : Controller
    {
        private readonly UserService _userService;
        private readonly string apiUrl = ApiConfig.Instance.BaseUrl;

        public UsersController()
        {
            _userService = new UserService();
        }

        // GET: Users
        public async Task<ActionResult> ViewUser()
        {

            var users = await _userService.GetAllAsync();
            return View(users);
        }
        // GET: Users/Create
        public async Task<ActionResult> CreateUser()
        {
            return View();
        }

        // POST: Users/Create
        [HttpPost]
        public async Task<ActionResult> CreateUser(Users user)
        {
            await _userService.CreateAsync(user);
            return RedirectToAction("ViewUser");
        }

        // GET: Users/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Users/Edit/5
        [HttpGet]
        public async Task<ActionResult> UpdateUser(int id)
        {
            try
            {

                var user = await _userService.GetByIdAsync(id);
                return View("UpdateUser", user);
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", $"Error al obtener el usuario: {ex.Message}");
                return View("Error");
            }
        }

        // GET: Users/Delete/5
        public ActionResult Delete(int id)
        {
            try
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(apiUrl);

                    // HTTP DELETE sin cuerpo del mensaje
                    var deleteTask = client.DeleteAsync($"/api/Usuarios/{id}");

                    deleteTask.Wait();

                    var result = deleteTask.Result;
                    if (result.IsSuccessStatusCode)
                    {
                        return RedirectToAction("ViewUser"); // OK sin contenido
                    }
                }

                return new HttpStatusCodeResult(500); // Error interno del servidor
            }
            catch (Exception ex)
            {
                // Manejar otras excepciones según sea necesario
                return new HttpStatusCodeResult(500); // Error interno del servidor
            }
        }

        // POST: Users/Delete/5
        [HttpPost]
        public async Task<ActionResult> UpdateUser(Users user, int id)
        {
            try
            {
                await _userService.UpdateAsync(user, id);
                // Después de la actualización exitosa, redirigir a la vista ViewUser
                return RedirectToAction("ViewUser");
            }
            catch (Exception ex)
            {
                if (ex.Message.Contains("ya EXISTE"))
                {
                    ModelState.AddModelError("", $"Error al actualizar el usuario: {ex.Message}");
                    // Puedes agregar el mensaje de error específico a la vista si lo necesitas
                    ViewBag.DuplicateErrorMessage = ex.Message;

                    return View();
                }
                else
                {
                    ModelState.AddModelError("", $"Error al actualizar el usuario: {ex.Message}");
                    return View();
                }
            }                                                               
        }
    }
}