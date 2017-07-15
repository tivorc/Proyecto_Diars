using _03_Dominio;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _04_Presistencia
{
    public class daoDetallePedido
    {
        #region singleton
        private static readonly daoDetallePedido _instancia = new daoDetallePedido();
        public static daoDetallePedido Instancia
        {
            get { return daoDetallePedido._instancia; }
        }
        #endregion singleton

        #region metodos
   
        public List<entDetallePedido> DevolverProductosPedido(int pedidoID)
        {
            SqlCommand cmd = null;
            List<entDetallePedido> lista = new List<entDetallePedido>();
            try
            {
                SqlConnection cn = Conexion.Instancia.conectar();
                cmd = new SqlCommand("spDevolverProductosPedido", cn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@pedidoID", pedidoID);
                cn.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    entDetallePedido dt = new entDetallePedido();
                    dt.DetallePedidoID = Convert.ToInt32(dr["detallePedidoID"]);
                    dt.CantidadProducto = Convert.ToInt32(dr["cantidadProducto"]);
                    dt.PrecioProducto = Convert.ToDecimal(dr["precioProducto"]);

                    entProducto pro = new entProducto();
                    pro.ProductoID = Convert.ToInt32(dr["productoID"]);
                    pro.NombreProducto = dr["nombreProducto"].ToString();
                    pro.DescripcionProducto = dr["descripcionProducto"].ToString();
                    dt.Producto = pro;

                    lista.Add(dt);
                }
                return lista;
            }
            catch (Exception e) { throw e; }
            finally { if (cmd != null) { cmd.Connection.Close(); } }
        }

        #endregion metodos
    }
}
