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
        // GET: Home
        public ActionResult Index()
        {
            return View();
        }

        // GET: Menu
        public ActionResult Menu()
        {
            int idSubTipoPlato = 0;
            List<entProducto> lista = appProducto.Instancia.ListaPlatosDisponibles(idSubTipoPlato);
            return View(lista);
        }

        // GET: Login
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
                    //agregamos objeto usuario a la session
                    if (u.Rol == "Administrador")
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
            public string Valor { get; set; }
            public string Descripcion { get; set; }
        }
        // GET: Registro
        [HttpGet]
        public ActionResult Registro()
        {
            List<Sexo> sexos = new List<Sexo>();
            Sexo sexo1 = new Sexo();
            sexo1.Valor = "F";
            sexo1.Descripcion = "Femenino";
            Sexo sexo2 = new Sexo();
            sexo2.Valor = "M";
            sexo2.Descripcion = "Masculino";
            sexos.Add(sexo1);
            sexos.Add(sexo2);
            ViewBag.sexos = new SelectList(sexos, "Valor", "Descripcion");
            return View();
        }

        // GET: Registro
        [HttpPost]
        public ActionResult Registro(entCliente c, HttpPostedFileBase archivo)
        {
            List<Sexo> sexos = new List<Sexo>();
            Sexo sexo1 = new Sexo();
            sexo1.Valor = "F";
            sexo1.Descripcion = "Femenino";
            Sexo sexo2 = new Sexo();
            sexo2.Valor = "M";
            sexo2.Descripcion = "Masculino";
            sexos.Add(sexo1);
            sexos.Add(sexo2);
            ViewBag.sexos = new SelectList(sexos, "Valor", "Descripcion");
            if (ModelState.IsValid)
            {
                c.Usuario.Rol = "Cliente";
                c.Usuario.ImgUsuario = Path.GetFileName(archivo.FileName);
                bool inserto = appCliente.Instancia.InsertarCliente(c);
                if (inserto)
                {
                    if (archivo != null && archivo.ContentLength > 0)
                    {
                        var namearchivo = Path.GetFileName(archivo.FileName);
                        var ruta = Path.Combine(Server.MapPath("/Bootstrap/Intranet/build/images"), namearchivo);
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