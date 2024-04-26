using FarmaciaBID.ApiServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Threading.Tasks;
using FarmaciaBID.Models;

namespace FarmaciaBID.Controllers
{
    public class DosagesController : Controller
    {
        private readonly DosageService dosageService = new DosageService();
        public async Task<ActionResult> ViewDosage()
        {
            
            var employe = await dosageService.GetAllDosage();

            return View(employe);
        }


        public async Task<ActionResult> CreateDosages()
        {
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> CreateDosages(Dosage NewDosage)
        {
            try
            {
                // Verificar si el modelo es válido antes de intentar guardarlo en la base de datos
                if (ModelState.IsValid)
                {
                    // Lógica para guardar la nueva dosificación en la base de datos utilizando el servicio
                    await dosageService.CreateDosage(NewDosage);

                    // Redireccionar a una vista de éxito o a la lista de dosificaciones
                    return RedirectToAction("ViewDosage");
                }
                return View(NewDosage);
            }
            catch (Exception ex)
            {
                // Manejar cualquier excepción que pueda ocurrir al llamar al servicio
                ModelState.AddModelError("", "Error al intentar guardar la dosificación. Por favor, inténtelo de nuevo más tarde.");
                return RedirectToAction("Error");
            }
        }


        [HttpGet]
        public async Task<ActionResult> UpdateDosage(int id)
        {
            try
            {
                var dosage = await dosageService.GetDosageById(id);
                return View("UpdateDosage", dosage);
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", $"Error al obtener el usuario: {ex.Message}");
                return View("Error");
            }
        }

        [HttpPost]
        public async Task<ActionResult> UpdateDosage(Dosage updateDosage,  int id)
        {
            try
            {
                await dosageService.UpdateDosage(updateDosage, id);
                // Después de la actualización exitosa, redirigir a la vista ViewUser
                return RedirectToAction("ViewDosage");
            }
            catch (Exception ex)
            {
                if (ex.Message.Contains("ya EXISTE"))
                {
                    ModelState.AddModelError("", $"Error al crear el proveedor: {ex.Message}");
                    // Puedes agregar el mensaje de error específico a la vista si lo necesitas
                    ViewBag.DuplicateErrorMessage = ex.Message;

                    return View();
                }
                else
                {
                    ModelState.AddModelError("", $"Error al crear el proveedor: {ex.Message}");
                    return View();
                }
            }
        }



    }
}
