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
    public class daoMesa
    {
        #region singleton
        private static readonly daoMesa _instancia = new daoMesa();
        public static daoMesa Instancia
        {
            get { return daoMesa._instancia; }
        }
        #endregion singleton

        public List<entMesa> ListarMesas()
        {
            SqlCommand cmd = null;
            List<entMesa> lista = new List<entMesa>();
            try
            {
                SqlConnection cn = Conexion.Instancia.conectar();
                cmd = new SqlCommand("spListarMesa", cn);
                cmd.CommandType = CommandType.StoredProcedure;
                cn.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    entMesa m = new entMesa();
                    m.MesaID = Convert.ToInt32(dr["mesaID"]);
                    m.NumeroMesa = Convert.ToInt32(dr["numeroMesa"]);
                    m.CapacidadMesa = Convert.ToInt32(dr["capacidadMesa"]);
                    lista.Add(m);
                }
                return lista;
            }
            catch (Exception e) { throw e; }
            finally { if (cmd != null) { cmd.Connection.Close(); } }
        }
    }
}
