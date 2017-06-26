using _02_Aplicacion;
using _03_Dominio;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace _01_Presentacion.Controllers
{
    public class ClienteController : Controller
    {
        // GET: Cliente
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult Nuevo()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Nuevo(entCliente cli)
        {
            if (ModelState.IsValid)
            {
                entUsuario u = new entUsuario();
                u.Rol = "Cliente";
                cli.Usuario = u;
                Boolean inserto = appCliente.Instancia.InsertarCliente(cli);
                if (inserto)
                {
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    ViewBag.mensaje = "No se pudo insertar";
                    return View();
                }
            }
            else
            {
                return View(cli);
            }

        }

        [HttpGet]
        public ActionResult Lista()
        {
            List<entCliente> lista = appCliente.Instancia.ListaCliente("");
            return View(lista);
        }

    }
}