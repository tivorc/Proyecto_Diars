using _02_Aplicacion;
using _03_Dominio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace _01_Presentacion.Controllers
{
    public class PedidoLlamadaController : Controller
    {
        public ActionResult Main()
        {
            return View();
        }

        public ActionResult DetalleMain(string estado)
        {
            List<entPedido> lista = appPedido.Instancia.ListaPedidos("Llamada", estado);
            return PartialView(lista);
        }

        public ActionResult InicializarPedido()
        {
            Session["pedido"] = null;
            Session["listaMenu"] = null;
            return RedirectToAction("Nuevo", "PedidoLlamada");
        }

        public ActionResult Nuevo()
        {
            return View();
        }

        public ActionResult BusquedaCliente(string Nombre)
        {
            List<entCliente> lista = appCliente.Instancia.ListaCliente(Nombre);
            return PartialView(lista);
        }
    }
}