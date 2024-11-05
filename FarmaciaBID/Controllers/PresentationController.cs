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
    public class PresentationController : Controller
    {
        private readonly PresentacionesService presentacionService = new PresentacionesService();
        private readonly string apiUrl = ApiConfig.Instance.BaseUrl;
        private readonly MeasuresServices MeasuresService = new MeasuresServices();
        private readonly DosageService dosageS = new DosageService();


        private async Task<IEnumerable<SelectListItem>> ObtenerDosificacion()
        {
            var dosage = await dosageS.GetAllDosagesAsync();

            // Verifica que oficinas tenga datos antes de asignarlo a ViewBag
            if (dosage != null && dosage.Any())
            {
                return new SelectList(dosage, "idDosificacion", "nombre");
            }
            else
            {
                // Si no hay oficinas, puedes manejarlo de alguna manera (por ejemplo, estableciendo un mensaje de error)
                ModelState.AddModelError("", "No se encontraron Dosificaciones.");
                return Enumerable.Empty<SelectListItem>();
            }
        }

        private async Task<IEnumerable<SelectListItem>> ObtenerMedidas()
        {
            var medida = await MeasuresService.GetAllMeasures();

            // Verifica que oficinas tenga datos antes de asignarlo a ViewBag
            if (medida != null && medida.Any())
            {
                return new SelectList(medida, "idMedidas", "nombre");
            }
            else
            {
                // Si no hay oficinas, puedes manejarlo de alguna manera (por ejemplo, estableciendo un mensaje de error)
                ModelState.AddModelError("", "No se encontraron Dosificaciones.");
                return Enumerable.Empty<SelectListItem>();
            }
        }



        public PresentationController()
        {
            presentacionService = new PresentacionesService();
        }
        public async Task<ActionResult> ViewPresentation()
        {
            ViewBag.Dosificacion = await ObtenerDosificacion();
            ViewBag.Medida = await ObtenerMedidas();
            var presentacion = await presentacionService.GetAllAsync();

            return View(presentacion);
        }

        public async Task<ActionResult> CreatePresentation()
        {
            ViewBag.Dosificacion = await ObtenerDosificacion();
            ViewBag.Medida = await ObtenerMedidas();
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> CreatePresentation(Presentacion presentacion)
        {
            await presentacionService.CreateAsync(presentacion);
            return RedirectToAction("ViewPresentation");
        }



        [HttpGet]
        public async Task<ActionResult> UpdatePresentation(int id)
        {
            try
            {
                ViewBag.Dosificacion = await ObtenerDosificacion();
                ViewBag.Medida = await ObtenerMedidas();
                var presentacion = await presentacionService.GetByIdAsync(id);
                return View("UpdatePresentation", presentacion);
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", $"Error al obtener el presentacion: {ex.Message}");
                return View("Error");
            }
        }


        [HttpPost]
        public async Task<ActionResult> UpdatePresentation(Presentacion presentacion, int id)
        {
            try
            {
                await presentacionService.UpdateAsync(presentacion, id);
                // Después de la actualización exitosa, redirigir a la vista ViewUser
                return RedirectToAction("ViewPresentation");
            }
            catch (Exception ex)
            {
                if (ex.Message.Contains("ya EXISTE"))
                {
                    ModelState.AddModelError("", $"Error al actualizar el presentacion: {ex.Message}");
                    // Puedes agregar el mensaje de error específico a la vista si lo necesitas
                    ViewBag.DuplicateErrorMessage = ex.Message;

                    return View();
                }
                else
                {
                    ModelState.AddModelError("", $"Error al actualizar el presentacion: {ex.Message}");
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
                    var deleteTask = client.DeleteAsync($"/api/Presentaciones/{id}");

                    deleteTask.Wait();

                    var result = deleteTask.Result;
                    if (result.IsSuccessStatusCode)
                    {
                        return RedirectToAction("ViewPresentation"); // OK sin contenido
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