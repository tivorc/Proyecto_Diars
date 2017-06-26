﻿using _03_Dominio;
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


        public entCliente DevolverCliente(Int16 idCliente)
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
                    pe.Direccion = dr["direccion"].ToString();
                    pe.FechaNacimiento = Convert.ToDateTime(dr["fechaNacimiento"]);
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


        public List<entCliente> ListaCliente(string idNombre)
        {
            SqlCommand cmd = null;
            List<entCliente> lista = new List<entCliente>();
            try
            {
                SqlConnection cn = Conexion.Instancia.conectar();
                cmd = new SqlCommand("spListarCliente", cn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@nombre", idNombre);
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
                    pe.Direccion = dr["direccion"].ToString();
                    pe.FechaNacimiento = Convert.ToDateTime(dr["fechaNacimiento"]);
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
        #endregion metodos 
    }
}
