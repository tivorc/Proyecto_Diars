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

        public ActionResult AgregarCliente()
        {
            return View();
        }

        public ActionResult ClientePedido(int id)
        {
            Session["pedido"] = null;
            Session["listaMenu"] = new List<entMenu>();
            entCliente cli = appCliente.Instancia.DevolverCliente(id);
            entPedido p = new entPedido();
            p.Cliente = cli;
            Session["pedido"] = p;
            return RedirectToAction("Main", "Pedido");
        }

        [HttpGet]
        public ActionResult AgregarMenu()
        {
            List<entProducto> listaEntrada = appProducto.Instancia.ListaPlatosDisponibles(1);
            ViewBag.Lista1 = new SelectList(listaEntrada, "productoID", "nombreProducto");
            List<entProducto> listaSegundo = appProducto.Instancia.ListaPlatosDisponibles(2);
            ViewBag.Lista2 = new SelectList(listaSegundo, "productoID", "nombreProducto");
            List<entProducto> listaPostre = appProducto.Instancia.ListaPlatosDisponibles(3);
            ViewBag.Lista3 = new SelectList(listaPostre, "productoID", "nombreProducto");
            return View();
        }

        [HttpPost]
        public ActionResult AgregarMenu(entMenu menu)
        {
            if (ModelState.IsValid)
            {
                entPedido p = (entPedido)Session["pedido"];
                menu.Pedido = p;
                List<entMenu> listaMenu = (List<entMenu>)Session["listaMenu"];
                listaMenu.Add(menu);
                Session["listaMenu"] = listaMenu;
                return RedirectToAction("MainPedido", "Pedido");
            }
            else
            {
                List<entProducto> listaEntrada = appProducto.Instancia.ListaPlatosDisponibles(1);
                ViewBag.Lista1 = new SelectList(listaEntrada, "productoID", "nombreProducto");
                List<entProducto> listaSegundo = appProducto.Instancia.ListaPlatosDisponibles(2);
                ViewBag.Lista2 = new SelectList(listaSegundo, "productoID", "nombreProducto");
                List<entProducto> listaPostre = appProducto.Instancia.ListaPlatosDisponibles(3);
                ViewBag.Lista3 = new SelectList(listaPostre, "productoID", "nombreProducto");
                return View(menu);
            }
        }

        public ActionResult DetalleListaCliente(string Nombre)
        {
            List<entCliente> lista = appCliente.Instancia.ListaCliente(Nombre);
            return PartialView(lista);
        }

        public ActionResult Main()
        {
            return View();
        }

        public ActionResult QuitarMenu(entMenu m)
        {
            List<entMenu> listaMenuPedido = (List<entMenu>)Session["listaMenu"];
            listaMenuPedido.Remove(m);
            Session["listaMenu"] = listaMenuPedido;
            return RedirectToAction("MainPedido", "Pedido");
        }

        public ActionResult MainPedido()
        {
            List<entMenu> listaMenuPedido = (List<entMenu>)Session["listaMenu"];
            List<entMenu> listamenu = new List<entMenu>();
            foreach (var i in listaMenuPedido)
            {
                entMenu menu = new entMenu();
                entProducto entrada = appProducto.Instancia.DevolverPlato(i.Entrada.ProductoID);
                entProducto segundo = appProducto.Instancia.DevolverPlato(i.Segundo.ProductoID);
                entProducto postre = appProducto.Instancia.DevolverPlato(i.Postre.ProductoID);
                menu.Entrada = entrada;
                menu.Segundo = segundo;
                menu.Postre = postre;
                menu.Precio = segundo.PrecioProducto;
                menu.Cantidad = i.Cantidad;
                listamenu.Add(menu);
            }
            return View(listamenu);
        }


        public ActionResult RegistrarPedido()
        {
            List<entMenu> listaMenuPedido = (List<entMenu>)Session["listaMenu"];
            entPedido p = (entPedido)Session["pedido"];
            p.TipoPedido = "Llamada";
            entTipoPago tp = new entTipoPago();
            tp.TipoPagoID = 1;
            p.TipoPago = tp;
            int pedidoID = appPedido.Instancia.InsertarPedido(p);
            return RedirectToAction("Main", "Intranet");
        }
    }

}