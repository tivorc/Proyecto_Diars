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
        public ActionResult Main()
        {
            Session["pedido"] = null;
            Session["listaMenu"] = new List<entMenu>();
            return View();
        }

        public ActionResult DetalleMain(string estado)
        {
            List<entPedido> lista = appPedido.Instancia.ListaPedidos("Llamada", estado);
            return PartialView(lista);
        }

        public ActionResult NuevoPedido()
        {
            List<entTipoPago> lista = appTipoPago.Instancia.ListarTipoPago();
            ViewBag.Lista = lista;
            if ((List<entMenu>)Session["listaMenu"] != null)
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
                    menu.MenuID = i.MenuID;
                    listamenu.Add(menu);
                }
                Session["listaMenu"] = listamenu;
                return View(listamenu);
            }
            return View();
        }

        public ActionResult AgregarCliente()
        {
            return View();
        }

        public ActionResult DetalleListaCliente(string Nombre)
        {
            List<entCliente> lista = appCliente.Instancia.ListaCliente(Nombre);
            return PartialView(lista);
        }

        public ActionResult ClientePedido(int id)
        {
            entCliente cli = appCliente.Instancia.DevolverCliente(id);
            entPedido p = new entPedido();
            p.Cliente = cli;
            Session["pedido"] = p;
            return RedirectToAction("NuevoPedido", "Pedido");
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
                return RedirectToAction("NuevoPedido", "Pedido");
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



        public ActionResult QuitarMenu(int id)
        {
            List<entMenu> listaMenuPedido = (List<entMenu>)Session["listaMenu"];
            listaMenuPedido.Remove(listaMenuPedido.Find(x => x.MenuID == id));
            Session["listaMenu"] = listaMenuPedido;
            return RedirectToAction("NuevoPedido", "Pedido");
        }



        public ActionResult RegistrarPedido()
        {
            //List<entMenu> listaMenuPedido = (List<entMenu>)Session["listaMenu"];
            //entPedido p = (entPedido)Session["pedido"];
            //p.TipoPedido = "Llamada";
            //entTipoPago tp = new entTipoPago();
            //tp.TipoPagoID = 1;
            //p.TipoPago = tp;
            //int pedidoID = appPedido.Instancia.InsertarPedido(p);
            //entPedido id = new entPedido();
            //id.PedidoID = pedidoID;
            //foreach (var item in listaMenuPedido)
            //{
            //    item.Pedido = id;
            //    appMenu.Instancia.InsertarMenu(item);
            //}
            return RedirectToAction("Main", "Pedido");
        }

        public ActionResult Cancelar()
        {
            Session["pedido"] = null;
            Session["listaMenu"] = null;
            return RedirectToAction("Main", "Pedido");
        }

        public ActionResult MenuPedido(int pedidoID)
        {
            entPedido p = appPedido.Instancia.DevolverPedido(pedidoID);
            List<entMenu> lista = appMenu.Instancia.DevolverMenusPedido(pedidoID);
            ViewBag.lista = lista;
            return View(p);
        }
    }

}