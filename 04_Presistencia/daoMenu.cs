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
    public class daoMenu
    {
        #region singleton
        private static readonly daoMenu _instancia = new daoMenu();
        public static daoMenu Instancia
        {
            get { return daoMenu._instancia; }
        }
        #endregion singleton

        #region metodos

        public bool InsertarMenu(entMenu m)
        {
            SqlCommand cmd = null;
            bool inserto = false;
            try
            {
                SqlConnection cn = Conexion.Instancia.conectar();
                cmd = new SqlCommand("spInsertarMenu", cn);
                cmd.Parameters.AddWithValue("@pedidoID", m.Pedido.PedidoID);
                cmd.Parameters.AddWithValue("@cantidad", m.Cantidad);
                cmd.Parameters.AddWithValue("@entradaID", m.Entrada.ProductoID);
                cmd.Parameters.AddWithValue("@segundoID", m.Segundo.ProductoID);
                cmd.Parameters.AddWithValue("@postreID", m.Postre.ProductoID);
                cmd.Parameters.AddWithValue("@precio", m.Segundo.PrecioProducto);

                cmd.CommandType = CommandType.StoredProcedure;
                cn.Open();
                int i = cmd.ExecuteNonQuery();
                if (i > 0) { inserto = true; }
                return inserto;
            }
            catch (Exception e) { throw e; }
            finally { if (cmd != null) { cmd.Connection.Close(); } }
        }

        #endregion metodos
    }
}
