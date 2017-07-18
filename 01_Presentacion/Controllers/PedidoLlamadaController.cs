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

        public ActionResult DetalleMain(string estado, string nombre)
        {
            List<entPedido> lista = appPedido.Instancia.ListaPedidosLlamada(estado, nombre);
            return PartialView(lista);
        }

        public ActionResult ListaClientes(string Nombre)
        {
            List<entCliente> lista = appCliente.Instancia.ListaCliente(Nombre);
            return PartialView(lista);
        }

        public JavaScriptResult AgregarCliente(int personaID)
        {
            entCliente cli = appCliente.Instancia.DevolverCliente(personaID);
            entPedido p = null;
            if (Session["pedido"] != null)
            {
                p = (entPedido)Session["pedido"];
            }
            else
            {
                p = new entPedido();
            }
            p.Cliente = cli;
            Session["pedido"] = p;
            return JavaScript("muestraClientePedido();");
        }

        public ActionResult NuevoPedido()
        {
            List<entTipoPago> lista = appTipoPago.Instancia.ListarTipoPago();
            lista.Remove(lista.Find(x => x.DescripcionTipoPago.Equals("Paypal")));
            ViewBag.Lista = lista;
            List<entProducto> listaEntrada = appProducto.Instancia.ListaPlatosDisponibles(1);
            ViewBag.Entrada = listaEntrada;
            List<entProducto> listaSegundo = appProducto.Instancia.ListaPlatosDisponibles(2);
            ViewBag.Segundo = listaSegundo;
            List<entProducto> listaPostre = appProducto.Instancia.ListaPlatosDisponibles(3);
            ViewBag.Postre = listaPostre;
            return View();
        }

        public JavaScriptResult AgregarMenu(int entrada, int segundo, int postre, int cantidad)
        {
            entProducto ent = appProducto.Instancia.DevolverProducto(entrada);
            entProducto seg = appProducto.Instancia.DevolverProducto(segundo);
            entProducto pos = appProducto.Instancia.DevolverProducto(postre);
            entMenu menu = new entMenu();
            menu.Entrada = ent;
            menu.Segundo = seg;
            menu.Postre = pos;
            menu.Precio = seg.PrecioProducto;
            menu.Cantidad = cantidad;

            List<entMenu> listaMenu = null;
            if (Session["listaMenu"] != null)
            {
                listaMenu = (List<entMenu>)Session["listaMenu"];
            }
            else
            {
                listaMenu = new List<entMenu>();
            }

            listaMenu.Add(menu);
            Session["listaMenu"] = listaMenu;
            return JavaScript("muestradetalle();");
        }

        public JavaScriptResult AgregarProducto(int productoID, int cantidadProducto)
        {
            entProducto pro = appProducto.Instancia.DevolverProducto(productoID);
            entDetallePedido dtPedido = new entDetallePedido();
            dtPedido.Producto = pro;
            dtPedido.CantidadProducto = cantidadProducto;
            List<entDetallePedido> listaProductos = null;
            if (Session["listaProducto"] != null)
            {
                listaProductos = (List<entDetallePedido>)Session["listaProducto"];
            }
            else
            {
                listaProductos = new List<entDetallePedido>();
            }
            listaProductos.Add(dtPedido);
            Session["listaProducto"] = listaProductos;
            return JavaScript("muestradetalle();");
        }

        public ActionResult DetalleMenu()
        {
            if (Session["listaMenu"] != null)
            {
                List<entMenu> lista = (List<entMenu>)Session["listaMenu"];
                return PartialView(lista);
            }
            else
            {
                List<entMenu> lista = new List<entMenu>();
                return PartialView(lista);
            }

        }

        public ActionResult DetalleProducto()
        {
            if (Session["listaProducto"] != null)
            {
                List<entDetallePedido> lista = (List<entDetallePedido>)Session["listaProducto"];
                return PartialView(lista);
            }
            else
            {
                List<entDetallePedido> lista = new List<entDetallePedido>();
                return PartialView(lista);
            }
        }

        public ActionResult SeleccionarProducto(string busquedaProducto)
        {
            List<entProducto> lista = appProducto.Instancia.ListaProductos(busquedaProducto);
            return PartialView(lista);
        }

        public ActionResult ClientePedido()
        {
            return PartialView();
        }

        public ActionResult Cancelar()
        {
            Session["pedido"] = null;
            Session["listaMenu"] = null;
            Session["listaProducto"] = null;
            return RedirectToAction("Main", "PedidoLlamada");
        }

        public ActionResult GrabarPedido(int tipoPago)
        {
            if (Session["pedido"] != null && (Session["listaMenu"] != null || Session["listaProducto"] != null))
            {
                entTipoPago tp = new entTipoPago();
                tp.TipoPagoID = tipoPago;
                entPedido ped = (entPedido)Session["pedido"];
                ped.TipoPago = tp;
                ped.TipoPedido = "Llamada";

                List<entMenu> men = (List<entMenu>)Session["listaMenu"];

                List<entDetallePedido> pro = (List<entDetallePedido>)Session["listaProducto"];

                bool inserto = false;
                inserto = appPedido.Instancia.InsertarPedidoLlamada(ped, men, pro);

                Session["pedido"] = null;
                Session["listaMenu"] = null;
                Session["listaProducto"] = null;
                return RedirectToAction("Main", "PedidoLlamada");
            }
            else
            {
                return View();
            }
        }


        public ActionResult DetallePedido(int pedidoID)
        {
            entPedido p = appPedido.Instancia.DevolverPedido(pedidoID);
            List<entMenu> listaMenu = appMenu.Instancia.DevolverMenusPedido(pedidoID);
            List<entDetallePedido> listaProducto = appDetallePedido.Instancia.DevolverProductosPedido(pedidoID);
            ViewBag.listaMenu = listaMenu;
            ViewBag.listaProducto = listaProducto;
            return View(p);
        }
    }

}