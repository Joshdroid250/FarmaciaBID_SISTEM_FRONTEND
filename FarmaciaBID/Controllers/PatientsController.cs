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
    public class PatientsController : Controller
    {
        private readonly PatientsService patientsService = new PatientsService();
        private readonly string apiUrl = ApiConfig.Instance.BaseUrl;

        public PatientsController()
        {
            patientsService = new PatientsService();
        }
        public async Task<ActionResult> ViewPatients()
        {

            var paciente = await patientsService.GetAllAsync();

            return View(paciente);
        }

        public async Task<ActionResult> CreatePatients()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> CreatePatients(Patients paciente)
        {
            await patientsService.CreateAsync(paciente);
            return RedirectToAction("ViewPatients");
        }



        [HttpGet]
        public async Task<ActionResult> UpdatePatients(int id)
        {
            try
            {

                var paciente = await patientsService.GetByIdAsync(id);
                return View("UpdatePatients", paciente);
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", $"Error al obtener el paciente: {ex.Message}");
                return View("Error");
            }
        }


        [HttpPost]
        public async Task<ActionResult> UpdatePatients(Patients paciente, int id)
        {
            try
            {
                await patientsService.UpdateAsync(paciente, id);
                // Después de la actualización exitosa, redirigir a la vista ViewUser
                return RedirectToAction("ViewPatients");
            }
            catch (Exception ex)
            {
                if (ex.Message.Contains("ya EXISTE"))
                {
                    ModelState.AddModelError("", $"Error al actualizar el paciente: {ex.Message}");
                    // Puedes agregar el mensaje de error específico a la vista si lo necesitas
                    ViewBag.DuplicateErrorMessage = ex.Message;

                    return View();
                }
                else
                {
                    ModelState.AddModelError("", $"Error al actualizar el paciente: {ex.Message}");
                    return View();
                }
            }
        }

        public ActionResult Delete(int id)
        {
            try
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(apiUrl);

                    // HTTP DELETE sin cuerpo del mensaje
                    var deleteTask = client.DeleteAsync($"/api/Pacientes/{id}");

                    deleteTask.Wait();

                    var result = deleteTask.Result;
                    if (result.IsSuccessStatusCode)
                    {
                        return RedirectToAction("ViewPatients"); // OK sin contenido
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
    }
}
