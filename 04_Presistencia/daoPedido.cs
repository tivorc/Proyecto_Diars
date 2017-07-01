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

        public int InsertarPedido(entPedido p)
        {
            SqlCommand cmd = null;
            try
            {
                SqlConnection cn = Conexion.Instancia.conectar();
                cmd = new SqlCommand("spInsertarPedido", cn);
                cmd.Parameters.AddWithValue("@clienteID", p.Cliente.ClienteID);
                cmd.Parameters.AddWithValue("@tipoPagoID", p.TipoPago.TipoPagoID);
                cmd.Parameters.AddWithValue("@tipoPedido", p.TipoPedido);

                cmd.CommandType = CommandType.StoredProcedure;

                cn.Open();

                int id = (int)cmd.ExecuteScalar();
                return id;
            }
            catch (Exception e) { throw e; }
            finally { if (cmd != null) { cmd.Connection.Close(); } }
        }

        public List<entPedido> ListaPedidos(string tipoPedido, string estado)
        {
            SqlCommand cmd = null;
            List<entPedido> lista = new List<entPedido>();
            try
            {
                SqlConnection cn = Conexion.Instancia.conectar();
                cmd = new SqlCommand("spListarPedidos", cn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@tipoPedido", tipoPedido);
                cmd.Parameters.AddWithValue("@estado", estado);
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