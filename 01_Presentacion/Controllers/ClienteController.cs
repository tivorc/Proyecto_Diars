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

        //[HttpPost]
        //public ActionResult Nuevo(entCliente cli, HttpPostedFileBase archivo)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        cli.Usuario.ImgUsuario = Path.GetFileName(archivo.FileName);
        //        Boolean inserto = appCliente.Instancia.InsertarCliente(cli);
        //        if (inserto)
        //        {
        //            if (archivo != null && archivo.ContentLength > 0)
        //            {
        //                var namearchivo = Path.GetFileName(archivo.FileName);
        //                var ruta = Path.Combine(Server.MapPath("/Bootstrap/Extranet/images"), namearchivo);
        //                archivo.SaveAs(ruta);
        //            }
        //            return RedirectToAction("Index", "Home");
        //        }
        //        else
        //        {
        //            ViewBag.mensaje = "No se pudo insertar";
        //            return View();
        //        }
        //    }
        //    else
        //    {
        //        return View(cli);
        //    }
        //}
    }
}