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
    public class daoTipoPago
    {
        #region singleton
        private static readonly daoTipoPago _instancia = new daoTipoPago();
        public static daoTipoPago Instancia
        {
            get { return daoTipoPago._instancia; }
        }
        #endregion singleton

        public List<entTipoPago> ListarTipoPago()
        {
            SqlCommand cmd = null;
            List<entTipoPago> lista = new List<entTipoPago>();
            try
            {
                SqlConnection cn = Conexion.Instancia.conectar();
                cmd = new SqlCommand("spListarTipoPago", cn);
                cmd.CommandType = CommandType.StoredProcedure;
                cn.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    entTipoPago tp = new entTipoPago();
                    tp.TipoPagoID = Convert.ToInt32(dr["tipoPagoID"]);
                    tp.DescripcionTipoPago = dr["descripcionTipoPago"].ToString();
                    lista.Add(tp);
                }
                return lista;
            }
            catch (Exception e) { throw e; }
            finally { if (cmd != null) { cmd.Connection.Close(); } }
        }
    }
}
