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
        public bool InsertarCliente(entCliente cli)
        {
            SqlCommand cmd = null;
            bool inserto = false;
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
                if (cli.Usuario.NombreUsuario == null)
                {
                    cmd.Parameters.AddWithValue("@nombreUsuario", DBNull.Value);
                }
                else
                {
                    cmd.Parameters.AddWithValue("@nombreUsuario", cli.Usuario.NombreUsuario);
                }
                if (cli.Usuario.Contrasena == null)
                {
                    cmd.Parameters.AddWithValue("@contrasena", DBNull.Value);
                }
                else
                {
                    cmd.Parameters.AddWithValue("@contrasena", cli.Usuario.Contrasena);
                }
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
        
        public entCliente DevolverCliente(int idCliente)
        {
            SqlCommand cmd = null;
            entCliente cli = null;
            try
            {
                SqlConnection cn = Conexion.Instancia.conectar();
                cmd = new SqlCommand("spDevolverCliente", cn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@idCliente", idCliente);
                cn.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.Read())
                {
                    cli = new entCliente();
                    cli.ClienteID = Convert.ToInt16(dr["clienteID"]);
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
                    //pe.FechaNacimiento = Convert.ToDateTime(dr["fechaNacimiento"]);
                    cli.Persona = pe;
                    entUsuario u = new entUsuario();
                    u.UsuarioID = Convert.ToInt16(dr["usuarioID"]);
                    u.NombreUsuario = dr["nombreUsuario"].ToString();
                    u.Contrasena = dr["contrasena"].ToString();
                    u.Rol = dr["rol"].ToString();
                    u.Estado = Convert.ToBoolean(dr["estado"]);
                    u.ImgUsuario = dr["imgUsuario"].ToString();
                    cli.Usuario = u;
                }
                return cli;
            }
            catch (Exception e) { throw e; }
            finally { if (cmd != null) { cmd.Connection.Close(); } }
        }
        
        public List<entCliente> ListaCliente(string nombre)
        {
            SqlCommand cmd = null;
            List<entCliente> lista = new List<entCliente>();
            try
            {
                SqlConnection cn = Conexion.Instancia.conectar();
                cmd = new SqlCommand("spListarClientes", cn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@nombre", nombre);
                cn.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    entCliente cli = new entCliente();
                    cli.ClienteID = Convert.ToInt16(dr["clienteID"]);
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
                    //pe.FechaNacimiento = Convert.ToDateTime(dr["fechaNacimiento"]);
                    cli.Persona = pe;
                    entUsuario u = new entUsuario();
                    u.UsuarioID = Convert.ToInt16(dr["usuarioID"]);
                    u.NombreUsuario = dr["nombreUsuario"].ToString();
                    u.Contrasena = dr["contrasena"].ToString();
                    u.Rol = dr["rol"].ToString();
                    u.Estado = Convert.ToBoolean(dr["estado"]);
                    u.ImgUsuario = dr["imgUsuario"].ToString();
                    cli.Usuario = u;
                    lista.Add(cli);
                }
                return lista;
            }
            catch (Exception e) { throw e; }
            finally { if (cmd != null) { cmd.Connection.Close(); } }
        }

        public bool EditarClienteExtranet(entCliente c)
        {
            SqlCommand cmd = null;
            bool inserto = false;
            try
            {
                SqlConnection cn = Conexion.Instancia.conectar();
                cmd = new SqlCommand("spEditarCliente", cn);
                cmd.Parameters.AddWithValue("@clienteID", c.ClienteID);
                cmd.Parameters.AddWithValue("@personaID", c.Persona.PersonaID);
                cmd.Parameters.AddWithValue("@nombre", c.Persona.Nombre);
                cmd.Parameters.AddWithValue("@apellidos", c.Persona.Apellidos);
                cmd.Parameters.AddWithValue("@dni", c.Persona.Dni);
                if (c.Persona.Telefono == null)
                {
                    cmd.Parameters.AddWithValue("@telefono", DBNull.Value);
                }
                else
                {
                    cmd.Parameters.AddWithValue("@telefono", c.Persona.Telefono);
                }
                if (c.Persona.Sexo == null)
                {
                    cmd.Parameters.AddWithValue("@sexo", DBNull.Value);
                }
                else
                {
                    cmd.Parameters.AddWithValue("@sexo", c.Persona.Sexo);
                }
                cmd.Parameters.AddWithValue("@direccion", c.Persona.Direccion);
                if (c.Persona.FechaNacimiento == null)
                {
                    cmd.Parameters.AddWithValue("@fechaNacimiento", DBNull.Value);
                }
                else
                {
                    cmd.Parameters.AddWithValue("@fechaNacimiento", c.Persona.FechaNacimiento);
                }
                cmd.Parameters.AddWithValue("@usuarioID", c.Usuario.UsuarioID);
                if (c.Usuario.NombreUsuario == null)
                {
                    cmd.Parameters.AddWithValue("@nombreUsuario", DBNull.Value);
                }
                else
                {
                    cmd.Parameters.AddWithValue("@nombreUsuario", c.Usuario.NombreUsuario);
                }
                if (c.Usuario.Contrasena == null)
                {
                    cmd.Parameters.AddWithValue("@contrasena", DBNull.Value);
                }
                else
                {
                    cmd.Parameters.AddWithValue("@contrasena", c.Usuario.Contrasena);
                }
                if (c.Usuario.Rol == null)
                {
                    cmd.Parameters.AddWithValue("@rol", DBNull.Value);
                }
                else
                {
                    cmd.Parameters.AddWithValue("@rol", c.Usuario.Rol);
                }
                cmd.Parameters.AddWithValue("@estado", c.Usuario.Estado);
                if (c.Usuario.ImgUsuario == null)
                {
                    cmd.Parameters.AddWithValue("@estado", DBNull.Value);
                }
                else
                {
                    cmd.Parameters.AddWithValue("@estado", c.Usuario.ImgUsuario);
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

        public bool EditarClienteIntranet(entCliente c)
        {
            SqlCommand cmd = null;
            bool inserto = false;
            try
            {
                SqlConnection cn = Conexion.Instancia.conectar();
                cmd = new SqlCommand("spEditarClienteIntranet", cn);
                cmd.Parameters.AddWithValue("@clienteID", c.ClienteID);
                cmd.Parameters.AddWithValue("@personaID", c.Persona.PersonaID);
                cmd.Parameters.AddWithValue("@nombre", c.Persona.Nombre);
                cmd.Parameters.AddWithValue("@apellidos", c.Persona.Apellidos);
                cmd.Parameters.AddWithValue("@dni", c.Persona.Dni);
                if (c.Persona.Telefono == null)
                {
                    cmd.Parameters.AddWithValue("@telefono", DBNull.Value);
                }
                else
                {
                    cmd.Parameters.AddWithValue("@telefono", c.Persona.Telefono);
                }
                if (c.Persona.Sexo == null)
                {
                    cmd.Parameters.AddWithValue("@sexo", DBNull.Value);
                }
                else
                {
                    cmd.Parameters.AddWithValue("@sexo", c.Persona.Sexo);
                }
                cmd.Parameters.AddWithValue("@direccion", c.Persona.Direccion);
                if (c.Persona.FechaNacimiento == null)
                {
                    cmd.Parameters.AddWithValue("@fechaNacimiento", DBNull.Value);
                }
                else
                {
                    cmd.Parameters.AddWithValue("@fechaNacimiento", c.Persona.FechaNacimiento);
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

        public bool EliminarCliente(int clienteID)
        {
            SqlCommand cmd = null;
            bool elimino = false;
            try
            {
                SqlConnection cn = Conexion.Instancia.conectar();
                cmd = new SqlCommand("spEliminarCliente", cn);
                cmd.Parameters.AddWithValue("@clienteID", clienteID);
                cmd.CommandType = CommandType.StoredProcedure;
                cn.Open();
                int i = cmd.ExecuteNonQuery();
                if (i > 0) { elimino = true; }
                return elimino;
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
