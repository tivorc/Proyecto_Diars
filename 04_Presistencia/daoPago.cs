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
    public class daoPago
    {
        #region singleton
        private static readonly daoPago _instancia = new daoPago();
        public static daoPago Instancia
        {
            get { return daoPago._instancia; }
        }
        #endregion singleton

        public bool InsertarPago(entPago p)
        {
            SqlCommand cmd = null;
            bool inserto = false;
            try
            {
                SqlConnection cn = Conexion.Instancia.conectar();
                cmd = new SqlCommand("spInsertarPago", cn);
                cmd.Parameters.AddWithValue("@pedidoid", p.Pedido.PedidoID);
                cmd.Parameters.AddWithValue("@trabajadorid", p.Trabajador.TrabajadorID);
                cmd.Parameters.AddWithValue("@subtotalpago", p.SubtotalPago);
                cmd.Parameters.AddWithValue("@descuentopago", p.DescuentoPago);
                cmd.Parameters.AddWithValue("@totalpago", p.TotalPago);
                cmd.CommandType = CommandType.StoredProcedure;
                cn.Open();
                int i = cmd.ExecuteNonQuery();
                if (i > 0) { inserto = true; }
                return inserto;
            }
            catch (Exception e) { throw e; }
            finally { if (cmd != null) { cmd.Connection.Close(); } }
        }
    }
}
