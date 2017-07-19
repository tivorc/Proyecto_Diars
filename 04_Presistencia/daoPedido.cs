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
    public class daoPedido
    {
        #region singleton
        private static readonly daoPedido _instancia = new daoPedido();
        public static daoPedido Instancia
        {
            get { return daoPedido._instancia; }
        }
        #endregion singleton

        #region metodos

        public bool InsertarPedidoLlamada(string xml)
        {
            SqlCommand cmd = null;
            bool inserto = false;
            try
            {
                SqlConnection cn = Conexion.Instancia.conectar();
                cmd = new SqlCommand("spInsertarPedidoLlamada", cn);
                cmd.Parameters.AddWithValue("@prmstrXML", xml);

                cmd.CommandType = CommandType.StoredProcedure;

                cn.Open();

                int i = cmd.ExecuteNonQuery();
                if (i > 0) { inserto = true; }
                return inserto;
            }
            catch (Exception e) { throw e; }
            finally { if (cmd != null) { cmd.Connection.Close(); } }
        }

        public bool InsertarPedidoOnline(string xml)
        {
            SqlCommand cmd = null;
            bool inserto = false;
            try
            {
                SqlConnection cn = Conexion.Instancia.conectar();
                cmd = new SqlCommand("spInsertarPedidoOnline", cn);
                cmd.Parameters.AddWithValue("@prmstrXML", xml);

                cmd.CommandType = CommandType.StoredProcedure;

                cn.Open();

                int i = cmd.ExecuteNonQuery();
                if (i > 0) { inserto = true; }
                return inserto;
            }
            catch (Exception e) { throw e; }
            finally { if (cmd != null) { cmd.Connection.Close(); } }
        }

        public bool InsertarPedidoPresencial(string xml)
        {
            SqlCommand cmd = null;
            bool inserto = false;
            try
            {
                SqlConnection cn = Conexion.Instancia.conectar();
                cmd = new SqlCommand("spInsertarPedidoPresencial", cn);
                cmd.Parameters.AddWithValue("@prmstrXML", xml);

                cmd.CommandType = CommandType.StoredProcedure;

                cn.Open();

                int i = cmd.ExecuteNonQuery();
                if (i > 0) { inserto = true; }
                return inserto;
            }
            catch (Exception e) { throw e; }
            finally { if (cmd != null) { cmd.Connection.Close(); } }
        }

        public List<entPedido> ListaPedidosLlamada(string estado, string nombre)
        {
            SqlCommand cmd = null;
            List<entPedido> lista = new List<entPedido>();
            try
            {
                SqlConnection cn = Conexion.Instancia.conectar();
                cmd = new SqlCommand("spListarPedidosLlamada", cn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@estado", estado);
                cmd.Parameters.AddWithValue("@cliente", nombre);
                cn.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    entPedido ped = new entPedido();
                    ped.PedidoID = Convert.ToInt32(dr["pedidoID"]);
                    ped.EstadoPedido = dr["estadoPedido"].ToString();
                    ped.Fecha = Convert.ToDateTime(dr["fecha"]);
                    entPersona per = new entPersona();
                    per.PersonaID = Convert.ToInt32(dr["personaID"]);
                    per.Nombre = dr["nombre"].ToString();
                    per.Apellidos = dr["apellidos"].ToString();
                    per.Direccion = dr["direccion"].ToString();
                    entCliente cli = new entCliente();
                    cli.ClienteID = Convert.ToInt32(dr["clienteID"]);
                    entTipoPago tp = new entTipoPago();
                    tp.TipoPagoID = Convert.ToInt32(dr["tipoPagoID"]);
                    tp.DescripcionTipoPago = dr["descripcionTipoPago"].ToString();

                    cli.Persona = per;
                    ped.Cliente = cli;
                    ped.TipoPago = tp;

                    lista.Add(ped);
                }
                return lista;
            }
            catch (Exception e) { throw e; }
            finally { if (cmd != null) { cmd.Connection.Close(); } }
        }

        public List<entPedido> ListaPedidosOnline(string estado, string nombre)
        {
            SqlCommand cmd = null;
            List<entPedido> lista = new List<entPedido>();
            try
            {
                SqlConnection cn = Conexion.Instancia.conectar();
                cmd = new SqlCommand("spListarPedidosOnline", cn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@estado", estado);
                cmd.Parameters.AddWithValue("@cliente", nombre);
                cn.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    entPedido ped = new entPedido();
                    ped.PedidoID = Convert.ToInt32(dr["pedidoID"]);
                    ped.EstadoPedido = dr["estadoPedido"].ToString();
                    ped.Fecha = Convert.ToDateTime(dr["fecha"]);
                    entPersona per = new entPersona();
                    per.PersonaID = Convert.ToInt32(dr["personaID"]);
                    per.Nombre = dr["nombre"].ToString();
                    per.Apellidos = dr["apellidos"].ToString();
                    per.Direccion = dr["direccion"].ToString();
                    entCliente cli = new entCliente();
                    cli.ClienteID = Convert.ToInt32(dr["clienteID"]);
                    entTipoPago tp = new entTipoPago();
                    tp.TipoPagoID = Convert.ToInt32(dr["tipoPagoID"]);
                    tp.DescripcionTipoPago = dr["descripcionTipoPago"].ToString();

                    cli.Persona = per;
                    ped.Cliente = cli;
                    ped.TipoPago = tp;

                    lista.Add(ped);
                }
                return lista;
            }
            catch (Exception e) { throw e; }
            finally { if (cmd != null) { cmd.Connection.Close(); } }
        }

        public List<entPedido> ListaPedidosPresencial(int mesa, string estado)
        {
            SqlCommand cmd = null;
            List<entPedido> lista = new List<entPedido>();
            try
            {
                SqlConnection cn = Conexion.Instancia.conectar();
                cmd = new SqlCommand("spListarPedidosPresencial", cn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@mesa", mesa);
                cmd.Parameters.AddWithValue("@estado", estado);
                cn.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    entPedido ped = new entPedido();
                    ped.PedidoID = Convert.ToInt32(dr["pedidoID"]);
                    ped.EstadoPedido = dr["estadoPedido"].ToString();
                    ped.Fecha = Convert.ToDateTime(dr["fecha"]);

                    entMesa m = new entMesa();
                    m.MesaID = Convert.ToInt32(dr["mesaID"]);
                    m.NumeroMesa = Convert.ToInt32(dr["numeroMesa"]);

                    ped.Mesa = m;

                    lista.Add(ped);
                }
                return lista;
            }
            catch (Exception e) { throw e; }
            finally { if (cmd != null) { cmd.Connection.Close(); } }
        }

        public entPedido DevolverPedido(int pedidoID)
        {
            SqlCommand cmd = null;
            entPedido ped = null;
            try
            {
                SqlConnection cn = Conexion.Instancia.conectar();
                cmd = new SqlCommand("spDevolverPedido", cn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@pedidoID", pedidoID);
                cn.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.Read())
                {
                    ped = new entPedido();
                    ped.PedidoID = Convert.ToInt32(dr["pedidoID"]);
                    ped.TipoPedido = dr["tipoPedido"].ToString();
                    ped.EstadoPedido = dr["estadoPedido"].ToString();
                    ped.Fecha = Convert.ToDateTime(dr["fecha"]);

                    entCliente cli = new entCliente();
                    cli.ClienteID = Convert.ToInt32(dr["clienteID"]);
                    ped.Cliente = cli;

                    entPersona per = new entPersona();
                    per.PersonaID = Convert.ToInt32(dr["personaID"]);
                    per.Nombre = dr["nombre"].ToString();
                    per.Apellidos = dr["apellidos"].ToString();
                    per.Dni = dr["dni"].ToString();
                    per.Telefono = dr["telefono"].ToString();
                    per.Direccion = dr["direccion"].ToString();
                    cli.Persona = per;

                    entTipoPago tp = new entTipoPago();
                    tp.TipoPagoID = Convert.ToInt32(dr["tipoPagoID"]);
                    tp.DescripcionTipoPago = dr["descripcionTipoPago"].ToString();

                    entMesa m = new entMesa();
                    string mesaID = dr["mesaID"].ToString();
                    if (mesaID != "")
                    {
                        m.MesaID = Convert.ToInt32(dr["mesaID"]);
                        m.NumeroMesa = Convert.ToInt32(dr["numeroMesa"]);
                        ped.Mesa = m;
                    }
                    

                    ped.TipoPago = tp;
                }
                return ped;
            }
            catch (Exception e) { throw e; }
            finally { if (cmd != null) { cmd.Connection.Close(); } }
        }

        #endregion metodos
    }
}