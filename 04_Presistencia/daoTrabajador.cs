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
    public class daoTrabajador
    {
        #region singleton
        private static readonly daoTrabajador _instancia = new daoTrabajador();
        public static daoTrabajador Instancia
        {
            get { return daoTrabajador._instancia; }
        }
        #endregion singleton

        public entTrabajador DevolverTrabajadorLogin(int idUsuario)
        {
            SqlCommand cmd = null;
            entTrabajador tra = null;
            try
            {
                SqlConnection cn = Conexion.Instancia.conectar();
                cmd = new SqlCommand("spDevolverTrabajadorLogin", cn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@idUsuario", idUsuario);
                cn.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.Read())
                {
                    tra = new entTrabajador();
                    tra.TrabajadorID = Convert.ToInt16(dr["trabajadorID"]);

                    entPersona pe = new entPersona();
                    pe.PersonaID = Convert.ToInt16(dr["personaID"]);
                    pe.Nombre = dr["nombre"].ToString();
                    pe.Apellidos = dr["apellidos"].ToString();
                    pe.Dni = dr["dni"].ToString();
                    pe.Telefono = dr["telefono"].ToString();
                    pe.Sexo = dr["sexo"].ToString();
                    pe.Direccion = dr["direccion"].ToString();
                    string fecha = dr["fechaNacimiento"].ToString();
                    if (fecha != "")
                    {
                        pe.FechaNacimiento = Convert.ToDateTime(fecha);
                    }
                    tra.Persona = pe;

                    entUsuario u = new entUsuario();
                    u.UsuarioID = Convert.ToInt16(dr["usuarioID"]);
                    u.NombreUsuario = dr["nombreUsuario"].ToString();
                    u.Rol = dr["rol"].ToString();
                    u.Estado = Convert.ToBoolean(dr["estado"]);
                    u.ImgUsuario = dr["imgUsuario"].ToString();
                    tra.Usuario = u;
                }
                return tra;
            }
            catch (Exception e) { throw e; }
            finally { if (cmd != null) { cmd.Connection.Close(); } }
        }
    }
}
