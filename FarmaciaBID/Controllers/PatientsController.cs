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
        public async Task<ActionResult> ViewPatients()
        {

            var employe = await patientsService.GetAllPacientes();

            return View(employe);
        }


        public async Task<ActionResult> CreatePatients()
        {
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> CreatePatients(Patients newPatient)
        {
            try
            {
                // Verificar si el modelo es válido antes de intentar guardarlo en la base de datos
                if (ModelState.IsValid)
                {
                    // Lógica para guardar la nueva dosificación en la base de datos utilizando el servicio
                    await patientsService.CreatePacientes(newPatient);

                    // Redireccionar a una vista de éxito o a la lista de dosificaciones
                    return RedirectToAction("ViewPatients");
                }
                return View(newPatient);
            }
            catch (Exception ex)
            {
                // Manejar cualquier excepción que pueda ocurrir al llamar al servicio
                ModelState.AddModelError("", "Error al intentar guardar la dosificación. Por favor, inténtelo de nuevo más tarde.");
                return RedirectToAction("Error");
            }
        }


        [HttpGet]
        public async Task<ActionResult> UpdatePatients(int id)
        {
            try
            {
                var paciente = await patientsService.GetPatientsById(id);
                return View("UpdatePatients", paciente);
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", $"Error al obtener el Paciente: {ex.Message}");
                return View("Error");
            }
        }

        [HttpPost]
        public async Task<ActionResult> UpdatePatients(Patients updatePatient, int id)
        {
            try
            {
                await patientsService.UpdatePatients(updatePatient, id);
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


        public ActionResult DeletePatient(int id)
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
