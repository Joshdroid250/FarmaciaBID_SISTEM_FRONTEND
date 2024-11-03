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
    public class LotPharmaController : Controller
    {
        private readonly LotPharmaService _LotePharmaService;
        private readonly string apiUrl = ApiConfig.Instance.BaseUrl;
        private readonly PresentacionesService presentacionS = new PresentacionesService();
        // GET: LotPharma
        public LotPharmaController()
        {
            _LotePharmaService = new LotPharmaService();
        }

        private async Task<IEnumerable<SelectListItem>> ObtenerPresentacion()
        {
            var presenta = await presentacionS.GetAllAsync();

            // Verifica que oficinas tenga datos antes de asignarlo a ViewBag
            if (presenta != null && presenta.Any())
            {
                return new SelectList(presenta, "idPresentacion", "nombre");
            }
            else
            {
                // Si no hay oficinas, puedes manejarlo de alguna manera (por ejemplo, estableciendo un mensaje de error)
                ModelState.AddModelError("", "No se encontraron Presentaciones.");
                return Enumerable.Empty<SelectListItem>();
            }
        }

        public async Task<ActionResult> ViewLotPharma()
        {
            ViewBag.Presentaciones = await ObtenerPresentacion();
            var lotpharma = await _LotePharmaService.GetAllAsync();
            return View(lotpharma);
        }

        public async Task<ActionResult> CreateLotPharma()
        {
            ViewBag.Presentaciones = await ObtenerPresentacion();
            return View();
        }


        [HttpPost]
        public async Task<ActionResult> CreateLotPharma(LotPharma lotepharma)
        {
            await _LotePharmaService.CreateAsync(lotepharma);
            return RedirectToAction("ViewLotPharma");
        }


        [HttpGet]
        public async Task<ActionResult> UpdateLotPharma(int id)
        {
            try
            {
                ViewBag.Presentaciones = await ObtenerPresentacion();
                var lotepharma = await _LotePharmaService.GetByIdAsync(id);
                return View("UpdateLotPharma", lotepharma);
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", $"Error al obtener el lote de farmacos: {ex.Message}");
                return View("Error");
            }
        }


        [HttpPost]
        public async Task<ActionResult> UpdateLotPharma(LotPharma lotepharma, int id)
        {
            try
            {
                ViewBag.Presentaciones = await ObtenerPresentacion();
                await _LotePharmaService.UpdateAsync(lotepharma, id);
                // Después de la actualización exitosa, redirigir a la vista ViewUser
                return RedirectToAction("ViewLotPharma");
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
                    var deleteTask = client.DeleteAsync($"/api/LoteFarmacos/{id}");

                    deleteTask.Wait();

                    var result = deleteTask.Result;
                    if (result.IsSuccessStatusCode)
                    {
                        return RedirectToAction("ViewLotPharma"); // OK sin contenido
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
