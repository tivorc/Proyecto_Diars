using _02_Aplicacion;
using _03_Dominio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace _01_Presentacion.Controllers
{
    public class IntranetController : Controller
    {
        
        // GET: Main
        public ActionResult Main()
        {
            return View();
        }

        public ActionResult Logout()
        {
            Session["usuario"] = null;
            return RedirectToAction("Login", "Home");
        }

    }
}