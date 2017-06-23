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
    public class daoSubTipoProducto
    {
        #region singleton
        private static readonly daoSubTipoProducto _instancia = new daoSubTipoProducto();
        public static daoSubTipoProducto Instancia
        {
            get { return daoSubTipoProducto._instancia; }
        }
        #endregion singleton

        public List<entSubTipoProducto> ListaSubTipoProductos(int tipoPlatoID)
        {
            SqlCommand cmd = null;
            List<entSubTipoProducto> lista = new List<entSubTipoProducto>();
            try
            {
                SqlConnection cn = Conexion.Instancia.conectar();
                cmd = new SqlCommand("spListarSubTipoProducto", cn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@tipoProductoID", tipoPlatoID);
                cn.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    entSubTipoProducto stp = new entSubTipoProducto();
                    stp.SubTipoProductoID = Convert.ToInt32(dr["subTipoProductoID"]);
                    stp.DescripcionSubTipo = dr["descripcionSubTipo"].ToString();
                    lista.Add(stp);
                }
                return lista;
            }
            catch (Exception e) { throw e; }
            finally { if (cmd != null) { cmd.Connection.Close(); } }
        }
    }
}
