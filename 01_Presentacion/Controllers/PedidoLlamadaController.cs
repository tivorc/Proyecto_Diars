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
            return RedirectToAction("Paso1", "PedidoLlamada");
        }

        public ActionResult Paso1()
        {
            return View();
        }

        public ActionResult BusquedaCliente(string Nombre)
        {
            List<entCliente> lista = appCliente.Instancia.ListaCliente(Nombre);
            return PartialView(lista);
        }

        public ActionResult AgregarCliente(int id)
        {
            entCliente cli = appCliente.Instancia.DevolverCliente(id);
            entPedido p = new entPedido();
            p.Cliente = cli;
            Session["pedido"] = p;
            return RedirectToAction("Paso2", "PedidoLlamada");
        }

        public ActionResult Paso2()
        {
            entPedido p = (entPedido)Session["pedido"];
            List<entTipoPago> lista = appTipoPago.Instancia.ListarTipoPago();
            ViewBag.Lista = lista;
            List<entProducto> listaEntrada = appProducto.Instancia.ListaPlatosDisponibles(1);
            ViewBag.Entrada = listaEntrada;
            List<entProducto> listaSegundo = appProducto.Instancia.ListaPlatosDisponibles(2);
            ViewBag.Segundo = listaSegundo;
            List<entProducto> listaPostre = appProducto.Instancia.ListaPlatosDisponibles(3);
            ViewBag.Postre = listaPostre;
            return View(p);
        }

        public ActionResult AgregarMenu(int entrada, int segundo, int postre)
        {

            entProducto ent = appProducto.Instancia.DevolverPlato(entrada);
            entProducto seg = appProducto.Instancia.DevolverPlato(segundo);
            entProducto pos = appProducto.Instancia.DevolverPlato(postre);
            entMenu menu = new entMenu();
            menu.Entrada = ent;
            menu.Segundo = seg;
            menu.Postre = pos;
            menu.Cantidad = 1;
            List < entMenu > listaMenu = (List<entMenu>)Session["listaMenu"];
            if (listaMenu.Count == 0)
            {
                menu.MenuID = 1;
            }
            else
            {
                menu.MenuID = listaMenu.Last().MenuID + 1;
            }
            listaMenu.Add(menu);
            Session["listaMenu"] = listaMenu;
            return PartialView(listaMenu);
        }
    }
}