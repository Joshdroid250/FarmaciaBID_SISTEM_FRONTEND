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
        private readonly ProceedingsServices ProceedingsS = new ProceedingsServices();
        private readonly MeasuresServices MeasuresService = new MeasuresServices();
        private readonly PatientsService patientS = new PatientsService();
        private readonly string apiUrl = ApiConfig.Instance.BaseUrl;

        private async Task<IEnumerable<SelectListItem>> ObtenerPaciente()
        {
            var paciente = await patientS.GetAllPacientes();

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
            ViewBag.Paciente = await ObtenerPaciente();
            var employe = await ProceedingsS.GetAllProceedings();

            return View(employe);
        }


        public async Task<ActionResult> CreateProceedings()
        {
            ViewBag.Paciente = await ObtenerPaciente();
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> CreateProceedings(Expediente newExpediente)
        {
            try
            {
                // Verificar si el modelo es válido antes de intentar guardarlo en la base de datos
                if (ModelState.IsValid)
                {
                    // Lógica para guardar la nueva dosificación en la base de datos utilizando el servicio
                    await ProceedingsS.CreateProceedings(newExpediente);

                    // Redireccionar a una vista de éxito o a la lista de dosificaciones
                    return RedirectToAction("ViewProceedings");
                }
                return View(newExpediente);
            }
            catch (Exception ex)
            {
                // Manejar cualquier excepción que pueda ocurrir al llamar al servicio
                ModelState.AddModelError("", "Error al intentar guardar la dosificación. Por favor, inténtelo de nuevo más tarde.");
                return RedirectToAction("Error");
            }
        }


        [HttpGet]
        public async Task<ActionResult> UpdateMeasures(int id)
        {
            try
            {
                var medida = await MeasuresService.GetMeasuresById(id);
                return View("UpdateMeasures", medida);
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", $"Error al obtener el Measures: {ex.Message}");
                return View("Error");
            }
        }

        [HttpPost]
        public async Task<ActionResult> UpdateMeasures(Measures updateMeasures, int id)
        {
            try
            {
                await MeasuresService.UpdateMeasures(updateMeasures, id);
                // Después de la actualización exitosa, redirigir a la vista ViewUser
                return RedirectToAction("ViewMeasures");
            }
            catch (Exception ex)
            {
                if (ex.Message.Contains("ya EXISTE"))
                {
                    ModelState.AddModelError("", $"Error al actualizar el Measures: {ex.Message}");
                    // Puedes agregar el mensaje de error específico a la vista si lo necesitas
                    ViewBag.DuplicateErrorMessage = ex.Message;

                    return View();
                }
                else
                {
                    ModelState.AddModelError("", $"Error al actualizar el Measures: {ex.Message}");
                    return View();
                }
            }
        }


        public ActionResult DeleteMeasures(int id)
        {
            try
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(apiUrl);

                    // HTTP DELETE sin cuerpo del mensaje
                    var deleteTask = client.DeleteAsync($"/api/Medidas/{id}");

                    deleteTask.Wait();

                    var result = deleteTask.Result;
                    if (result.IsSuccessStatusCode)
                    {
                        return RedirectToAction("ViewMeasures"); // OK sin contenido
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
