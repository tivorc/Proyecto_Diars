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
    public class daoUsuario
    {
        #region singleton
        private static readonly daoUsuario _instancia = new daoUsuario();
        public static daoUsuario Instancia
        {
            get { return daoUsuario._instancia; }
        }
        #endregion singleton

        #region metodos
        public entUsuario VerificarAcceso(String Usuario, String Contrasena)
        {
            SqlCommand cmd = null;
            entUsuario u = null;
            try
            {
                SqlConnection cn = Conexion.Instancia.conectar();
                cmd = new SqlCommand("spVerificarAcceso", cn);
                cmd.Parameters.AddWithValue("@usuario", Usuario);
                cmd.Parameters.AddWithValue("@contrasena", Contrasena);
                cmd.CommandType = CommandType.StoredProcedure;
                cn.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.Read())
                {
                    u = new entUsuario();
                    u.UsuarioID = Convert.ToInt16(dr["usuarioID"]);
                    u.NombreUsuario = dr["nombreUsuario"].ToString();
                    u.Rol = dr["rol"].ToString();
                    u.Estado = Convert.ToBoolean(dr["estado"]);
                    u.ImgUsuario = dr["imgUsuario"].ToString();
                }
                return u;
            }
            catch (Exception e)
            {
                throw e;
            }
            finally { if (cmd != null) { cmd.Connection.Close(); } }
        }

        #endregion metodos
    }
}
