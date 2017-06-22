using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace _01_Presentacion.Controllers
{
    public class ErrorController : Controller
    {
        // GET: Error
        public ActionResult Index(String mensaje)
        {
            ViewBag.mensajito = mensaje;
            return View();
        }
    }
}