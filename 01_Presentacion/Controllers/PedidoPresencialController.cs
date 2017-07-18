using _02_Aplicacion;
using _03_Dominio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace _01_Presentacion.Controllers
{
    public class PedidoPresencialController : Controller
    {
        public ActionResult Main()
        {
            List<entMesa> listaMesa = appMesa.Instancia.ListarMesas();
            ViewBag.Mesa = listaMesa;
            return View();
        }

        public ActionResult DetalleMain(int mesa,string estado)
        {
            List<entPedido> lista = appPedido.Instancia.ListaPedidosPresencial(mesa, estado);
            return PartialView(lista);
        }

        public ActionResult NuevoPedido()
        {
            List<entMesa> listaMesa = appMesa.Instancia.ListarMesas();
            ViewBag.Mesa = listaMesa;
            List<entProducto> listaEntrada = appProducto.Instancia.ListaPlatosDisponibles(1);
            ViewBag.Entrada = listaEntrada;
            List<entProducto> listaSegundo = appProducto.Instancia.ListaPlatosDisponibles(2);
            ViewBag.Segundo = listaSegundo;
            List<entProducto> listaPostre = appProducto.Instancia.ListaPlatosDisponibles(3);
            ViewBag.Postre = listaPostre;
            return View();
        }

        public ActionResult SeleccionarProducto(string busquedaProducto)
        {
            List<entProducto> lista = appProducto.Instancia.ListaProductos(busquedaProducto);
            return PartialView(lista);
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

        public ActionResult GrabarPedido(int mesa)
        {
            if ((Session["listaMenu"] != null || Session["listaProducto"] != null) && mesa != 0)
            {
                entMesa m = new entMesa();
                m.MesaID = mesa;
                entPedido ped = new entPedido();
                ped.Mesa = m;
                ped.TipoPedido = "Presencial";

                List<entMenu> men = (List<entMenu>)Session["listaMenu"];

                List<entDetallePedido> pro = (List<entDetallePedido>)Session["listaProducto"];

                bool inserto = false;
                inserto = appPedido.Instancia.InsertarPedidoPresencial(ped, men, pro);

                Session["listaMenu"] = null;
                Session["listaProducto"] = null;
                return RedirectToAction("Main", "PedidoPresencial");
            }
            else
            {
                return View();
            }
        }

        public ActionResult Cancelar()
        {
            Session["listaMenu"] = null;
            Session["listaProducto"] = null;
            return RedirectToAction("Main", "PedidoPresencial");
        }
    }
}