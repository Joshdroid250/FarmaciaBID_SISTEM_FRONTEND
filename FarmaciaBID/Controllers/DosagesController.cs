using FarmaciaBID.ApiServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Threading.Tasks;
using FarmaciaBID.Models;
using FarmaciaBID.ApiServices.ApiConfig;
using System.Net.Http;

namespace FarmaciaBID.Controllers
{
    public class DosagesController : Controller
    {
        private readonly DosageService _dosageService;
        private readonly string apiUrl = ApiConfig.Instance.BaseUrl;
        public DosagesController()
        {
            _dosageService = new DosageService();
        }

        

        public async Task<ActionResult> ViewDosage()
        {
            var dosages = await _dosageService.GetAllDosagesAsync();
            return View(dosages);
        }

        public async Task<ActionResult> CreateDosages()
        {
            return View();
        }


        [HttpPost]
        public async Task<ActionResult> CreateDosages(Dosage dosage)
        {
            await _dosageService.CreateDosageAsync(dosage);
            return RedirectToAction("ViewDosage");
        }


        [HttpGet]
        public async Task<ActionResult> UpdateDosage(int id)
        {
            try
            {
                
                var expe = await _dosageService.GetDosageByIdAsync(id);
                return View("UpdateDosage", expe);
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", $"Error al obtener la dosificacion: {ex.Message}");
                return View("Error");
            }
        }


        [HttpPost]
        public async Task<ActionResult> UpdateDosage(Dosage dosage, int id)
        {
            try
            {
                await _dosageService.UpdateDosageAsync(dosage, id);
                // Después de la actualización exitosa, redirigir a la vista ViewUser
                return RedirectToAction("ViewDosage");
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
                    var deleteTask = client.DeleteAsync($"/api/Dosificaciones/{id}");

                    deleteTask.Wait();

                    var result = deleteTask.Result;
                    if (result.IsSuccessStatusCode)
                    {
                        return RedirectToAction("ViewDosage"); // OK sin contenido
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
