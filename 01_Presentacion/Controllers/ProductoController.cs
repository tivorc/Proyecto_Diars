using _02_Aplicacion;
using _03_Dominio;
using System;
using System.Collections.Generic;
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
        public ActionResult StockMenu()
        {
            int tipoProductoID = 1;
            List<entSubTipoProducto> lista = appSubTipoProducto.Instancia.ListaSubTipoProductos(tipoProductoID);
            ViewBag.Lista = lista;
            return View();
        }

        public ActionResult DetalleStock(int subTipoProductoID)
        {
            List<entProducto> lista = appProducto.Instancia.ListaPlatosDisponibles(subTipoProductoID);
            return PartialView(lista);
        }
    }
}