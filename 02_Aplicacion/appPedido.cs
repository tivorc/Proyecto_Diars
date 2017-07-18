using _03_Dominio;
using _04_Presistencia;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace _02_Aplicacion
{
    public class appPedido
    {
        #region singleton
        private static readonly appPedido _instancia = new appPedido();
        public static appPedido Instancia
        {
            get { return appPedido._instancia; }
        }
        #endregion singleton

        #region metodos

        public bool InsertarPedidoLlamada(entPedido ped, List<entMenu> men, List<entDetallePedido> pro)
        {
            try
            {
                XmlDocument xmlDoc = new XmlDocument();
                XmlDeclaration xmldecl = xmlDoc.CreateXmlDeclaration("1.0", "ISO-8859-1", null);
                xmlDoc.InsertBefore(xmldecl, xmlDoc.DocumentElement);
                xmlDoc.AppendChild(xmlDoc.CreateElement("root"));

                XmlElement xmlPedido = default(XmlElement);
                xmlPedido = xmlDoc.CreateElement("Pedido");
                xmlPedido.SetAttribute("ClienteID", ped.Cliente.ClienteID.ToString());
                xmlPedido.SetAttribute("TipoPagoID", ped.TipoPago.TipoPagoID.ToString());
                xmlPedido.SetAttribute("TipoPedido", ped.TipoPedido.ToString());

                foreach (var m in men)
                {
                    XmlElement xmlMenuDetalle = default(XmlElement);
                    xmlMenuDetalle = xmlDoc.CreateElement("MenuDetalle");
                    xmlMenuDetalle.SetAttribute("CantidadMenu", m.Cantidad.ToString());
                    xmlMenuDetalle.SetAttribute("EntradaID", m.Entrada.ProductoID.ToString());
                    xmlMenuDetalle.SetAttribute("SegundoID", m.Segundo.ProductoID.ToString());
                    xmlMenuDetalle.SetAttribute("PostreID", m.Postre.ProductoID.ToString());
                    xmlMenuDetalle.SetAttribute("PrecioMenu", m.Precio.ToString());
                    xmlPedido.AppendChild(xmlMenuDetalle);
                }

                if(pro != null)
                {
                    foreach (var p in pro)
                    {
                        XmlElement xmlProductoDetalle = default(XmlElement);
                        xmlProductoDetalle = xmlDoc.CreateElement("ProductoDetalle");
                        xmlProductoDetalle.SetAttribute("ProductoID", p.Producto.ProductoID.ToString());
                        xmlProductoDetalle.SetAttribute("CantidadProducto", p.CantidadProducto.ToString());
                        xmlProductoDetalle.SetAttribute("PrecioProducto", p.Producto.PrecioProducto.ToString());
                        xmlPedido.AppendChild(xmlProductoDetalle);
                    }
                }

                xmlDoc.DocumentElement.AppendChild(xmlPedido);
                string xml = xmlDoc.OuterXml;

                bool i = daoPedido.Instancia.InsertarPedidoLlamada(xml);

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool InsertarPedidoPresencial(entPedido ped, List<entMenu> men, List<entDetallePedido> pro)
        {
            try
            {
                XmlDocument xmlDoc = new XmlDocument();
                XmlDeclaration xmldecl = xmlDoc.CreateXmlDeclaration("1.0", "ISO-8859-1", null);
                xmlDoc.InsertBefore(xmldecl, xmlDoc.DocumentElement);
                xmlDoc.AppendChild(xmlDoc.CreateElement("root"));

                XmlElement xmlPedido = default(XmlElement);
                xmlPedido = xmlDoc.CreateElement("Pedido");
                xmlPedido.SetAttribute("MesaID", ped.Mesa.MesaID.ToString());
                xmlPedido.SetAttribute("TipoPedido", ped.TipoPedido.ToString());

                foreach (var m in men)
                {
                    XmlElement xmlMenuDetalle = default(XmlElement);
                    xmlMenuDetalle = xmlDoc.CreateElement("MenuDetalle");
                    xmlMenuDetalle.SetAttribute("CantidadMenu", m.Cantidad.ToString());
                    xmlMenuDetalle.SetAttribute("EntradaID", m.Entrada.ProductoID.ToString());
                    xmlMenuDetalle.SetAttribute("SegundoID", m.Segundo.ProductoID.ToString());
                    xmlMenuDetalle.SetAttribute("PostreID", m.Postre.ProductoID.ToString());
                    xmlMenuDetalle.SetAttribute("PrecioMenu", m.Precio.ToString());
                    xmlPedido.AppendChild(xmlMenuDetalle);
                }

                if (pro != null)
                {
                    foreach (var p in pro)
                    {
                        XmlElement xmlProductoDetalle = default(XmlElement);
                        xmlProductoDetalle = xmlDoc.CreateElement("ProductoDetalle");
                        xmlProductoDetalle.SetAttribute("ProductoID", p.Producto.ProductoID.ToString());
                        xmlProductoDetalle.SetAttribute("CantidadProducto", p.CantidadProducto.ToString());
                        xmlProductoDetalle.SetAttribute("PrecioProducto", p.Producto.PrecioProducto.ToString());
                        xmlPedido.AppendChild(xmlProductoDetalle);
                    }
                }

                xmlDoc.DocumentElement.AppendChild(xmlPedido);
                string xml = xmlDoc.OuterXml;

                bool i = daoPedido.Instancia.InsertarPedidoPresencial(xml);

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public List<entPedido> ListaPedidosLlamada(string estado, string nombre)
        {
            try
            {
                return daoPedido.Instancia.ListaPedidosLlamada(estado, nombre);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public List<entPedido> ListaPedidosOnline(string estado, string nombre)
        {
            try
            {
                return daoPedido.Instancia.ListaPedidosOnline(estado, nombre);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public List<entPedido> ListaPedidosPresencial(int mesa, string estado)
        {
            try
            {
                return daoPedido.Instancia.ListaPedidosPresencial(mesa, estado);
            }
            catch (Exception e)
            {

                throw e;
            }
        }

        public entPedido DevolverPedido(int pedidoID)
        {
            try
            {
                return daoPedido.Instancia.DevolverPedido(pedidoID);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        #endregion metodos
    }
}
