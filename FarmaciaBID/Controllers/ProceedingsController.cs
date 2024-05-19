using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FarmaciaBID.Controllers
{
    public class ProceedingsController : Controller
    {
        // GET: Proceedings
        public ActionResult Index()
        {
            return View();
        }

        // GET: Proceedings/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Proceedings/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Proceedings/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Proceedings/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Proceedings/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Proceedings/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Proceedings/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
