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

        // GET: Producto
        public ActionResult PlatosDisponibles()
        {
            int idSubTipoPlato = 0;
            List<entProducto> lista = appProducto.Instancia.ListaPlatosDisponibles(idSubTipoPlato);
            return View(lista);
        }
    }
}