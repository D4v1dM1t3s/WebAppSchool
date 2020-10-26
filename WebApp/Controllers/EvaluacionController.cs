using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Web.Business;
using Web.Model;

namespace WebApp.Controllers
{
    public class EvaluacionController : Controller
    {
        // GET: Materias
        public ActionResult Evaluaciones()
        {
            var process = new Web.Business.Repository();
            var materias = process.GetAsignaturas();

            return View(materias);
        }

        [HttpPost]
        public ActionResult Search(string searchString)
        {
            var searchStr = Request.Form["SearchString"];

            if (searchString.Length > 0)
            {
                var process = new Web.Business.Repository();
                var materias = process.GetAsignaturas(searchString);

                return View("Evaluaciones", materias);
            }
            else
                return View("Evaluaciones");
        }

        //[HttpPost]
        //[RunidAuthorize(753)]
        //public ActionResult Create()
        //{
        //    var user = this.User;
        //    var request = new SaveVehiculoPrepRequest(user.UserID, user.SessionID, user, string.Empty);
        //    var process = new SaveVehiculoProcessPrep(request, this.Context);
        //    process.Run();

        //    return View("_SaveVehiculo", process.response);
        //}

        //[HttpPost]
        //[RunidAuthorize(753)]
        //public ActionResult Edit(string vehCodigo)
        //{
        //    var user = this.User;
        //    var request = new SaveVehiculoPrepRequest(user.UserID, user.SessionID, user, vehCodigo);
        //    var process = new SaveVehiculoProcessPrep(request, this.Context);
        //    process.Run();

        //    return View("_SaveVehiculo", process.response);
        //}

        //[HttpPost]
        //[RunidAuthorize(753)]
        //public ActionResult Save(SaveVehiculo vehiculo)
        //{
        //    var user = this.User;

        //    var process = new SaveVehiculoProcess(new SaveVehiculoRequest(user.UserID, user.SessionID, user, vehiculo), this.Context);
        //    process.Run();

        //    return Json(process.response, JsonRequestBehavior.AllowGet);
        //}


        //// GET: Evaluacion
        //public ActionResult Index()
        //{
        //    return View();
        //}
        //// GET: Evaluacion
        //public ActionResult Evaluaciones()
        //{
        //    return View();
        //}

        //// GET: Evaluacion/Details/5
        //public ActionResult Details(int id)
        //{
        //    return View();
        //}

        //// GET: Evaluacion/Create
        //public ActionResult Create()
        //{
        //    return View();
        //}

        // POST: Evaluacion/Create
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

        // GET: Evaluacion/Edit/5
        public ActionResult Edit(decimal? id)
        {
            if (id == null && id > 0)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var process = new Web.Business.Repository();
            var objToUpdate = process.GetAsignatura(id.Value);
            if (objToUpdate == null)
            {
                return HttpNotFound();
            }
            return View(objToUpdate);
        }

        // POST: Evaluacion/Edit/5

        [HttpPost, ActionName("Edit")]
        [ValidateAntiForgeryToken]
        public ActionResult Save(decimal? id, FormCollection collection)
        {
            try
            {
                if (id == null && id > 0)
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

                var tareas = Request.Form["NotaTareas"];
                var examen = Request.Form["NotaExamen"];
                double notaTarea = 0;
                double notaExamen = 0;

                double.TryParse(tareas, out notaTarea);
                double.TryParse(examen, out notaExamen);

                var asignatura = new Asignatura()
                {
                    ID = id.Value,
                    NotaExamen = notaExamen,
                    NotaTareas = notaTarea
                };

                var process = new Web.Business.Repository();
                var objUpdated = process.SaveAsignatura(asignatura);
                if (objUpdated)
                {
                    return RedirectToAction("Evaluaciones");
                }
                return RedirectToAction("Evaluaciones");
            }
            catch
            {     //Log the error (uncomment dex variable name and add a line here to write a log.
                ModelState.AddModelError("", "Unable to save changes. Try again, and if the problem persists, see your system administrator.");
                return RedirectToAction("Evaluaciones");
            }
        }

        // GET: Evaluacion/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Evaluacion/Delete/5
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