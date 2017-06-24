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
    public class daoCliente
    {
        #region singleton
        private static readonly daoCliente _instancia = new daoCliente();
        public static daoCliente Instancia
        {
            get { return daoCliente._instancia; }
        }
        #endregion singleton

        #region metodos
        public Boolean InsertarCliente(entCliente cli)
        {
            SqlCommand cmd = null;
            Boolean inserto = false;
            try
            {
                SqlConnection cn = Conexion.Instancia.conectar();
                cmd = new SqlCommand("spInsertarCliente", cn);
                cmd.Parameters.AddWithValue("@nombre", cli.Persona.Nombre);
                cmd.Parameters.AddWithValue("@apellidos", cli.Persona.Apellidos);
                cmd.Parameters.AddWithValue("@dni ", cli.Persona.Dni);
                if (cli.Persona.Telefono == null)
                {
                    cmd.Parameters.AddWithValue("@telefono", DBNull.Value);
                }
                else
                {
                    cmd.Parameters.AddWithValue("@telefono", cli.Persona.Telefono);
                }
                if (cli.Persona.Sexo == null)
                {
                    cmd.Parameters.AddWithValue("@sexo", DBNull.Value);
                }
                else
                {
                    cmd.Parameters.AddWithValue("@sexo", cli.Persona.Sexo);
                }
                cmd.Parameters.AddWithValue("@direccion", cli.Persona.Direccion);
                if (cli.Persona.FechaNacimiento == null)
                {
                    cmd.Parameters.AddWithValue("@fechaNacimiento", DBNull.Value);
                }
                else
                {
                    cmd.Parameters.AddWithValue("@fechaNacimiento", cli.Persona.FechaNacimiento);
                }
                cmd.Parameters.AddWithValue("@nombreUsuario", cli.Usuario.NombreUsuario);
                cmd.Parameters.AddWithValue("@contrasena", cli.Usuario.Contrasena);
                cmd.Parameters.AddWithValue("@rol", cli.Usuario.Rol);
                if (cli.Usuario.ImgUsuario == null)
                {
                    cmd.Parameters.AddWithValue("@imgUsuario", DBNull.Value);
                }
                else
                {
                    cmd.Parameters.AddWithValue("@imgUsuario", cli.Usuario.ImgUsuario);
                }

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
