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
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Menu()
        {
            List<entProducto> lista = appProducto.Instancia.ListaPlatosDisponibles(0);
            return View(lista);
        }

        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(FormCollection frm)
        {
            try
            {
                String Usuario = frm["Usuario"].ToString();
                String Contrasena = frm["Contrasena"].ToString();
                String mensaje;
                entUsuario u = appUsuario.Instancia.VerificarAcceso(Usuario, Contrasena, out mensaje);
                if (mensaje.Equals(""))
                {
                    Session["usuario"] = u;
                    if (u.Rol.Equals("Administrador") || u.Rol.Equals("Trabajador"))
                    {
                        return RedirectToAction("Main", "Intranet");
                    }
                    else if(u.Rol == "Cliente")
                    {
                        return RedirectToAction("Index");
                    }
                    else
                    {
                        return RedirectToAction("Index");
                    }
                }
                else
                {
                    ViewBag.mensaje = mensaje;
                    return View();
                }
            }
            catch (Exception e)
            {
                return RedirectToAction("Index", "Error", new { mensaje = e.Message });
            }

        }
        public class Sexo
        {
            public string SexoID { get; set; }
            public string Descripcion { get; set; }

            public Sexo(string sexoID, string descripcion)
            {
                this.SexoID = sexoID;
                this.Descripcion = descripcion;
            }
        }

        [HttpGet]
        public ActionResult Registro()
        {
            List<Sexo> lista = new List<Sexo>();
            Sexo sexo = new Sexo("F", "Femenino");
            lista.Add(sexo);
            sexo = new Sexo("M", "Masculino");
            lista.Add(sexo);
            ViewBag.sexos = new SelectList(lista, "SexoID", "Descripcion");
            return View();
        }

        [HttpPost]
        public ActionResult Registro(entCliente c, HttpPostedFileBase archivo)
        {
            List<Sexo> lista = new List<Sexo>();
            Sexo sexo = new Sexo("F", "Femenino");
            lista.Add(sexo);
            sexo = new Sexo("M", "Masculino");
            lista.Add(sexo);
            ViewBag.sexos = new SelectList(lista, "SexoID", "Descripcion");
            if (ModelState.IsValid)
            {
                c.Usuario.Rol = "Cliente";
                if (archivo != null && archivo.ContentLength > 0)
                {
                    c.Usuario.ImgUsuario = Path.GetFileName(archivo.FileName);
                }
                bool inserto = appCliente.Instancia.InsertarCliente(c);
                if (inserto)
                {
                    if (archivo != null && archivo.ContentLength > 0)
                    {
                        var namearchivo = Path.GetFileName(archivo.FileName);
                        var ruta = Path.Combine(Server.MapPath("/Bootstrap/Extranet/images"), namearchivo);
                        archivo.SaveAs(ruta);
                    }
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
                return View(c);
            }
        }

        public ActionResult Logout()
        {
            Session["usuario"] = null;
            return RedirectToAction("Login", "Home");
        }
    }
}