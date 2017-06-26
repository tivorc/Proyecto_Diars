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

            entCliente cli = appCliente.Instancia.DevolverCliente(id);
            entPedido p = new entPedido();
            p.Cliente = cli;
            Session["pedido"] = p;
            return RedirectToAction("Main", "Pedido");
        }

        [HttpGet]
        public ActionResult AgregarMenu()
        {
            List<entProducto> lista1 = appProducto.Instancia.ListaTamanoPizza();
            ViewBag.Lista = new SelectList(lista, "tamanoPizzaID", "descripcionTamanoPizza");
            List<entProducto> listaEntrada = appProducto.Instancia.ListaPlatosDisponibles(1);
            ViewBag.lista1 = listaEntrada;
            List<entProducto> listaSegundo = appProducto.Instancia.ListaPlatosDisponibles(2);
            ViewBag.lista2 = listaSegundo;
            List<entProducto> listaPostre = appProducto.Instancia.ListaPlatosDisponibles(3);
            ViewBag.lista3 = listaPostre;
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
                return RedirectToAction("Main", "Pedido");
            }
            else
            {
                List<entProducto> listaEntrada = appProducto.Instancia.ListaPlatosDisponibles(1);
                ViewBag.lista1 = listaEntrada;
                List<entProducto> listaSegundo = appProducto.Instancia.ListaPlatosDisponibles(2);
                ViewBag.lista2 = listaSegundo;
                List<entProducto> listaPostre = appProducto.Instancia.ListaPlatosDisponibles(3);
                ViewBag.lista3 = listaPostre;
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
            Session["pedido"] = null;
            Session["listaMenu"] = new List<entMenu>();
            return View();
        }
    }
}