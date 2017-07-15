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
        // GET: Pago
        public ActionResult Main()
        {

            return View();
        }

        public ActionResult DetalleMain(string tipoPedido, string nombre)
        {
            List<entPedido> lista = appPedido.Instancia.ListaPedidos(tipoPedido, "Registrado", nombre);
            return PartialView(lista);
        }

        public ActionResult IdentificarTipo(int pedidoID)
        {
            entPedido p = appPedido.Instancia.DevolverPedido(pedidoID);
            int id = p.PedidoID;
            if (p.TipoPago.DescripcionTipoPago.Equals("Paypal"))
            {
                return RedirectToAction("PagoPaypal", "Pago", new { idg : id});
            }
            else
            {
                if (p.TipoPago.DescripcionTipoPago.Equals("Tarjeta"))
                {

                }
                else
                {

                }
            }
            return View();
        }

        public ActionResult PagoPaypal(entPedido p)
        {
            return PartialView(p);
        }
    }
}