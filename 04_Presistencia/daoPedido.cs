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
            int id = 0;
            try
            {
                SqlConnection cn = Conexion.Instancia.conectar();
                cmd = new SqlCommand("spInsertarPedido", cn);
                cmd.Parameters.AddWithValue("@clienteID", p.Cliente.ClienteID);
                cmd.Parameters.AddWithValue("@tipoPagoID", p.TipoPago.TipoPagoID);
                cmd.Parameters.AddWithValue("@tipoPedido", p.TipoPedido);
                cmd.Parameters.Add("@id", SqlDbType.Text, 20).Direction = ParameterDirection.Output;
                cmd.CommandType = CommandType.StoredProcedure;
                cn.Open();
                cmd.ExecuteNonQuery();
                id = Convert.ToInt32(cmd.Parameters["@id"].Value);
                return id;
            }
            catch (Exception e) { throw e; }
            finally { if (cmd != null) { cmd.Connection.Close(); } }
        }

        #endregion metodos
    }
}
