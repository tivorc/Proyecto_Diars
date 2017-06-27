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
    public class daoPedido
    {
        #region singleton
        private static readonly daoPedido _instancia = new daoPedido();
        public static daoPedido Instancia
        {
            get { return daoPedido._instancia; }
        }
        #endregion singleton

        #region metodos

        public int InsertarPedido(entPedido p)
        {
            SqlCommand cmd = null;
            try
            {
                SqlConnection cn = Conexion.Instancia.conectar();
                cmd = new SqlCommand("spInsertarPedido", cn);
                cmd.Parameters.AddWithValue("@clienteID", p.Cliente.ClienteID);
                cmd.Parameters.AddWithValue("@tipoPagoID", p.TipoPago.TipoPagoID);
                cmd.Parameters.AddWithValue("@tipoPedido", p.TipoPedido);

                cmd.CommandType = CommandType.StoredProcedure;

                cn.Open();

                SqlDataReader dr = cmd.ExecuteReader();
                dr.Read();
                int id = Convert.ToInt32(dr["@a"]);
                return id;
            }
            catch (Exception e) { throw e; }
            finally { if (cmd != null) { cmd.Connection.Close(); } }
        }

        #endregion metodos
    }
}