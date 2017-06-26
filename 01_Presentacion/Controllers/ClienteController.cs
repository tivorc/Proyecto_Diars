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
                    return RedirectToAction("Lista", "Cliente");
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

        public ActionResult Lista()
        {
            return View();
        }

        public ActionResult DetalleLista(string Nombre)
        {
            List<entCliente> lista = appCliente.Instancia.ListaCliente(Nombre);
            return PartialView(lista);
        }

        public ActionResult Editar(int id)
        {
            entCliente c = appCliente.Instancia.DevolverCliente(id);
            return View(c);
        }

        // PST: Editar
        [HttpPost]
        public ActionResult Editar(entCliente c)
        {

            if (ModelState.IsValid)
            {
                bool inserto = appCliente.Instancia.EditarClienteIntranet(c);
                if (inserto)
                {
                    return RedirectToAction("Lista");
                }
                else
                {
                    ViewBag.mensaje = "No se pudo insertar";
                    return View();
                }
            }
            else
            {
                return View(c);
            }
        }

    }
}