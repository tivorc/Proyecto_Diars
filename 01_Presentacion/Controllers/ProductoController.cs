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
    public class ProductoController : Controller
    {
        // GET: Producto
        public ActionResult Index()
        {
            return View();
        }

        // GET: PlatosDisponibles
        public ActionResult PlatosDisponibles()
        {
            int idSubTipoPlato = 0;
            List<entProducto> lista = appProducto.Instancia.ListaPlatosDisponibles(idSubTipoPlato);
            return View(lista);
        }

        // GET: PlatosDisponibles
        public ActionResult Platos()
        {
            int tipoProductoID = 1;
            List<entSubTipoProducto> lista = appSubTipoProducto.Instancia.ListaSubTipoProductos(tipoProductoID);
            ViewBag.Lista = lista;
            return View();
        }

        public ActionResult ListaPlatos(int subTipoProductoID)
        {
            List<entProducto> lista = appProducto.Instancia.ListaPlatos(subTipoProductoID);
            return PartialView(lista);
        }

        [HttpGet]
        public ActionResult NuevoPlato()
        {
            int tipoProductoID = 1;
            List<entSubTipoProducto> lista = appSubTipoProducto.Instancia.ListaSubTipoProductos(tipoProductoID);
            ViewBag.Lista = new SelectList(lista, "SubTipoProductoID", "DescripcionSubTipo");
            return View();
        }

        [HttpPost]
        public ActionResult NuevoPlato(entProducto p, HttpPostedFileBase archivo)
        {
            p.ImgProducto = Path.GetFileName(archivo.FileName);
            if (ModelState.IsValid)
            {
                bool inserto = appProducto.Instancia.InsertarProducto(p);
                if (inserto)
                {
                    if (archivo != null && archivo.ContentLength > 0)
                    {
                        var namearchivo = Path.GetFileName(archivo.FileName);
                        var ruta = Path.Combine(Server.MapPath("/Bootstrap/Extranet/images"), namearchivo);
                        archivo.SaveAs(ruta);
                    }
                    return RedirectToAction("Platos", "Producto");
                }
                else
                {
                    ViewBag.mensaje = "No se pudo editar";
                    return View();
                }
            }
            else
            {
                List<entSubTipoProducto> lista = appSubTipoProducto.Instancia.ListaSubTipoProductos(1);
                ViewBag.Lista = new SelectList(lista, "SubTipoProductoID", "DescripcionSubTipo");
                return View(p);
            }
        }

        [HttpGet]
        public ActionResult Eliminar(int id)
        {
            try
            {
                entProducto p = appProducto.Instancia.DevolverProducto(id);
                if (p == null)
                {
                    ViewBag.Message = "El cliente no existe";
                    return RedirectToAction("Index", "Error", new { mensaje = ViewBag.Message });
                }
                else
                {
                    return View(p);
                }
            }
            catch (Exception e)
            {
                return RedirectToAction("Index", "Error", new { mensaje = e.Message });
            }
        }

        [HttpPost, ActionName("Eliminar")]
        [ValidateAntiForgeryToken]
        public ActionResult ConfimaEliminacion(int id)
        {
            try
            {
                entProducto p = appProducto.Instancia.DevolverProducto(id);
                bool elimino = appProducto.Instancia.EliminarProducto(p.ProductoID);
                if (elimino)
                {
                    return RedirectToAction("Platos");
                }
                else
                {
                    ViewBag.mensaje = "No se pudo eliminar";
                    return View(p);
                }
            }
            catch (Exception e)
            {
                return RedirectToAction("Index", "Error", new { mensaje = e.Message });
            }
        }

    }
}