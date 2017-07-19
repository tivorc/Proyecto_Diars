using _02_Aplicacion;
using _03_Dominio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace _01_Presentacion.Controllers
{
    public class PagoController : Controller
    {
        public ActionResult Main()
        {

            return View();
        }

        public ActionResult DetalleMain(string tipoPedido, string nombre)
        {
            if (tipoPedido.Equals("Presencial"))
            {
                return RedirectToAction("DetallePresencial", "Pago");
            }
            else
            {
                if (tipoPedido.Equals("Llamada"))
                {
                    List<entPedido> lista = appPedido.Instancia.ListaPedidosLlamada("Registrado", nombre);
                    ViewBag.titulo = "Pedidos Llamada";
                    return PartialView(lista);
                }
                else
                {
                    List<entPedido> lista = appPedido.Instancia.ListaPedidosOnline("Registrado", nombre);
                    List<entPedido> listita = new List<entPedido>();
                    foreach (var item in lista)
                    {
                        if (!item.TipoPago.DescripcionTipoPago.Equals("Paypal"))
                        {
                            listita.Add(item);
                        }
                    }
                    ViewBag.titulo = "Pedidos Online";
                    return PartialView(listita);
                }
            }
        }

        public ActionResult DetallePresencial()
        {
            List<entPedido> lista = appPedido.Instancia.ListaPedidosPresencial(0, "Registrado");
            return PartialView(lista);
        }

        public ActionResult DetalleLlamada(string cliente)
        {
            List<entPedido> lista = appPedido.Instancia.ListaPedidosLlamada("Registrado", cliente);
            return PartialView(lista);
        }

        public ActionResult IdentificarTipo(int pedidoID)
        {
            entPedido p = appPedido.Instancia.DevolverPedido(pedidoID);
            int id = p.PedidoID;
            if (p.TipoPago.DescripcionTipoPago.Equals("Tarjeta"))
            {
                return RedirectToAction("PagoTarjeta", "Pago", new { @id = p.PedidoID });
            }
            else
            {
                return RedirectToAction("PagoEfectivo", "Pago", new { @id = p.PedidoID });
            }
        }

        public ActionResult PagoTarjeta(int id)
        {
            entPedido p = appPedido.Instancia.DevolverPedido(id);
            return View(p);
        }

        public ActionResult PagoEfectivo(int id)
        {
            entPago p = appPago.Instancia.CalcularTotal(id, ((entUsuario)Session["usuario"]).UsuarioID);
            Session["pago"] = p;
            return View(p);
        }

        public ActionResult Pagar()
        {
            entPago p = (entPago)Session["pago"];
            bool inserto = appPago.Instancia.InsertarPago(p);
            return RedirectToAction("Main", "Pago");
        }
    }
}