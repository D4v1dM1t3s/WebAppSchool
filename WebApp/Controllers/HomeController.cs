using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebApp.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Es una aplicación que le permita a un profesor registrar calificaciones en la asignatura que el dicta, tendrá un listado de estudiantes a calificar y podrá registrar las calificaciones en dos casilleros DEB(deberes) y EXA (examen).";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}