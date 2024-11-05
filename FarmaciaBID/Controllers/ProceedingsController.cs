using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Threading.Tasks;
using FarmaciaBID.Models;
using FarmaciaBID.ApiServices.ApiConfig;
using System.Net.Http;
using FarmaciaBID.ApiServices;
using System;
using System.Collections.Generic;

namespace FarmaciaBID.Controllers
{
    public class ProceedingsController : Controller
    {
        private readonly ExpedienteService _expedienteService;
        private readonly string apiUrl = ApiConfig.Instance.BaseUrl;
        private readonly MeasuresServices MeasuresService = new MeasuresServices();
        private readonly PatientsService patientS = new PatientsService();

        public ProceedingsController()
        {
            _expedienteService = new ExpedienteService();
        }

        private async Task<IEnumerable<SelectListItem>> ObtenerPaciente()
        {
            var paciente = await patientS.GetAllAsync();

            // Verifica que oficinas tenga datos antes de asignarlo a ViewBag
            if (paciente != null && paciente.Any())
            {
                return new SelectList(paciente, "idPaciente", "nombres", "apellidos");
            }
            else
            {
                // Si no hay oficinas, puedes manejarlo de alguna manera (por ejemplo, estableciendo un mensaje de error)
                ModelState.AddModelError("", "No se encontraron pacientes disponibles.");
                return Enumerable.Empty<SelectListItem>();
            }
        }


        public async Task<ActionResult> ViewProceedings()
        {
            var expe = await _expedienteService.GetAllAsync();
            return View(expe);
        }


        public async Task<ActionResult> CreateProceedings()
        {
            ViewBag.Paciente = await ObtenerPaciente();
            return View();
        }


        [HttpPost]
        public async Task<ActionResult> CreateProceedings(Expediente expe)
        {
            await _expedienteService.CreateExpedientesAsync(expe);
            return RedirectToAction("ViewProceedings");
        }


        [HttpGet]
        public async Task<ActionResult> UpdateProceedings(int id)
        {
            try
            {
                ViewBag.Paciente = await ObtenerPaciente();
                var expe = await _expedienteService.GetExpedientesByIdAsync(id);
                return View("UpdateProceedings", expe);
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", $"Error al obtener el Expediente: {ex.Message}");
                return View("Error");
            }
        }

        [HttpPost]
        public async Task<ActionResult> UpdateProceedings(Expediente updateExpediente, int id)
        {
            try
            {
                await _expedienteService.UpdateExpedientesAsync(updateExpediente, id);
                // Después de la actualización exitosa, redirigir a la vista ViewUser
                return RedirectToAction("ViewProceedings");
            }
            catch (Exception ex)
            {
                if (ex.Message.Contains("ya EXISTE"))
                {
                    ModelState.AddModelError("", $"Error al actualizar el Expediente: {ex.Message}");
                    // Puedes agregar el mensaje de error específico a la vista si lo necesitas
                    ViewBag.DuplicateErrorMessage = ex.Message;

                    return View();
                }
                else
                {
                    ModelState.AddModelError("", $"Error al actualizar el Expediente: {ex.Message}");
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
                    var deleteTask = client.DeleteAsync($"/api/Expedientes/{id}");

                    deleteTask.Wait();

                    var result = deleteTask.Result;
                    if (result.IsSuccessStatusCode)
                    {
                        return RedirectToAction("ViewProceedings"); // OK sin contenido
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
