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
                cmd.Parameters.AddWithValue("@telefono", cli.Persona.Telefono);
                cmd.Parameters.AddWithValue("@sexo", cli.Persona.Sexo);
                cmd.Parameters.AddWithValue("@direccion", cli.Persona.Direccion);
                cmd.Parameters.AddWithValue("@fechaNacimiento", cli.Persona.FechaNacimiento);
                cmd.Parameters.AddWithValue("@nombreUsuario", cli.Usuario.NombreUsuario);
                cmd.Parameters.AddWithValue("@contrasena", cli.Usuario.Contrasena);
                cmd.Parameters.AddWithValue("@rol", cli.Usuario.Rol);
                cmd.Parameters.AddWithValue("@imgUsuario", cli.Usuario.ImgUsuario);

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
