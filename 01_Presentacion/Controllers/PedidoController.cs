using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using _03_Dominio;
using _02_Aplicacion;
namespace _01_Presentacion.Controllers
{
    public class PedidoController : Controller
    {


        public ActionResult buscarCliente()
        {
            return View();
        }


        public ActionResult DetalleListaCliente(string idNombre)
        {
            List<entCliente> lista = appCliente.Instancia.ListaCliente(idNombre);
            return PartialView(lista);
        }
        public ActionResult ListaInfoCliente(Int16 id)
        {
            entCliente cli = appCliente.Instancia.DevolverCliente(id);
            return View(cli);
        }
    }
}