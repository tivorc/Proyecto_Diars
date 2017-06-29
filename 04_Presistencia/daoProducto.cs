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
    public class daoProducto
    {
        #region singleton
        private static readonly daoProducto _instancia = new daoProducto();
        public static daoProducto Instancia
        {
            get { return daoProducto._instancia; }
        }
        #endregion singleton

        #region metodos

        public List<entProducto> ListaPlatos(int idSubTipoPlato)
        {
            SqlCommand cmd = null;
            List<entProducto> lista = new List<entProducto>();
            try
            {
                SqlConnection cn = Conexion.Instancia.conectar();
                cmd = new SqlCommand("spListarPlatosDisponibles", cn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@idSubTipoPlato", idSubTipoPlato);
                cn.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    entProducto p = new entProducto();
                    p.ProductoID = Convert.ToInt32(dr["productoID"]);
                    p.NombreProducto = dr["nombreProducto"].ToString();
                    p.DescripcionProducto = dr["descripcionProducto"].ToString();
                    p.PrecioProducto = Convert.ToDecimal(dr["precioProducto"]);
                    p.Stock = Convert.ToInt32(dr["stock"]);
                    p.ImgProducto = dr["imgProducto"].ToString();
                    entTipoProducto tp = new entTipoProducto();
                    tp.TipoProductoID = Convert.ToInt32(dr["tipoProductoID"]);
                    tp.DescripcionTipoProducto = dr["descripcionTipoProducto"].ToString();
                    entSubTipoProducto stp = new entSubTipoProducto();
                    stp.SubTipoProductoID = Convert.ToInt32(dr["subTipoProductoID"]);
                    stp.DescripcionSubTipo = dr["descripcionSubTipo"].ToString();
                    stp.TipoProducto = tp;
                    p.SubTipoProducto = stp;
                    lista.Add(p);
                }
                return lista;
            }
            catch (Exception e) { throw e; }
            finally { if (cmd != null) { cmd.Connection.Close(); } }
        }

        public List<entProducto> ListaPlatosDisponibles(int idSubTipoPlato)
        {
            SqlCommand cmd = null;
            List<entProducto> lista = new List<entProducto>();
            try
            {
                SqlConnection cn = Conexion.Instancia.conectar();
                cmd = new SqlCommand("spListarPlatosDisponibles", cn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@idSubTipoPlato", idSubTipoPlato);
                cn.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    entProducto p = new entProducto();
                    p.ProductoID = Convert.ToInt32(dr["productoID"]);
                    p.NombreProducto = dr["nombreProducto"].ToString();
                    p.DescripcionProducto = dr["descripcionProducto"].ToString();
                    p.PrecioProducto = Convert.ToDecimal(dr["precioProducto"]);
                    p.Stock = Convert.ToInt32(dr["stock"]);
                    p.ImgProducto = dr["imgProducto"].ToString();
                    entTipoProducto tp = new entTipoProducto();
                    tp.TipoProductoID = Convert.ToInt32(dr["tipoProductoID"]);
                    tp.DescripcionTipoProducto = dr["descripcionTipoProducto"].ToString();
                    entSubTipoProducto stp = new entSubTipoProducto();
                    stp.SubTipoProductoID = Convert.ToInt32(dr["subTipoProductoID"]);
                    stp.DescripcionSubTipo = dr["descripcionSubTipo"].ToString();
                    stp.TipoProducto = tp;
                    p.SubTipoProducto = stp;
                    lista.Add(p);
                }
                return lista;
            }
            catch (Exception e) { throw e; }
            finally { if (cmd != null) { cmd.Connection.Close(); } }
        }

        public entProducto DevolverPlato(int platoID)
        {
            SqlCommand cmd = null;
            entProducto p = null;
            try
            {
                SqlConnection cn = Conexion.Instancia.conectar();
                cmd = new SqlCommand("spDevolverPlato", cn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@productoID", platoID);
                cn.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.Read())
                {
                    p = new entProducto();
                    p.ProductoID = Convert.ToInt32(dr["productoID"]);
                    p.NombreProducto = dr["nombreProducto"].ToString();
                    p.DescripcionProducto = dr["descripcionProducto"].ToString();
                    p.PrecioProducto = Convert.ToDecimal(dr["precioProducto"]);
                    p.Stock = Convert.ToInt32(dr["stock"]);
                    p.ImgProducto = dr["imgProducto"].ToString();
                    entTipoProducto tp = new entTipoProducto();
                    tp.TipoProductoID = Convert.ToInt32(dr["tipoProductoID"]);
                    tp.DescripcionTipoProducto = dr["descripcionTipoProducto"].ToString();
                    entSubTipoProducto stp = new entSubTipoProducto();
                    stp.SubTipoProductoID = Convert.ToInt32(dr["subTipoProductoID"]);
                    stp.DescripcionSubTipo = dr["descripcionSubTipo"].ToString();
                    stp.TipoProducto = tp;
                    p.SubTipoProducto = stp;
                }
                return p;
            }
            catch (Exception e) { throw e; }
            finally { if (cmd != null) { cmd.Connection.Close(); } }
        }

        public bool EditarProducto(entProducto p)
        {
            SqlCommand cmd = null;
            bool inserto = false;
            try
            {
                SqlConnection cn = Conexion.Instancia.conectar();
                cmd = new SqlCommand("spEditarProducto", cn);
                cmd.Parameters.AddWithValue("@productoID", p.ProductoID);
                cmd.Parameters.AddWithValue("@nombreProducto", p.NombreProducto);
                cmd.Parameters.AddWithValue("@descripcionProducto", p.DescripcionProducto);
                cmd.Parameters.AddWithValue("@subtipoProductoID", p.SubTipoProducto.SubTipoProductoID);
                cmd.Parameters.AddWithValue("@precioProducto", p.PrecioProducto);
                cmd.Parameters.AddWithValue("@stock", p.Stock);
                cmd.Parameters.AddWithValue("@imgProducto", p.ImgProducto);
                cmd.CommandType = CommandType.StoredProcedure;
                cn.Open();
                int i = cmd.ExecuteNonQuery();
                if (i > 0) { inserto = true; }
                return inserto;
            }
            catch (Exception e) { throw e; }
            finally { if (cmd != null) { cmd.Connection.Close(); } }
        }

        public bool InsertarProducto(entProducto p)
        {
            SqlCommand cmd = null;
            bool inserto = false;
            try
            {
                SqlConnection cn = Conexion.Instancia.conectar();
                cmd = new SqlCommand("spInsertarProducto", cn);
                cmd.Parameters.AddWithValue("@nombreProducto", p.NombreProducto);
                cmd.Parameters.AddWithValue("@descripcionProducto", p.DescripcionProducto);
                cmd.Parameters.AddWithValue("@subtipoProductoID", p.SubTipoProducto.SubTipoProductoID);
                cmd.Parameters.AddWithValue("@precioProducto", p.PrecioProducto);
                cmd.Parameters.AddWithValue("@stock", p.Stock);
                cmd.Parameters.AddWithValue("@imgProducto", p.ImgProducto);
                cmd.CommandType = CommandType.StoredProcedure;
                cn.Open();
                int i = cmd.ExecuteNonQuery();
                if (i > 0) { inserto = true; }
                return inserto;
            }
            catch (Exception e) { throw e; }
            finally { if (cmd != null) { cmd.Connection.Close(); } }
        }

        public bool EliminarProducto(int productoID)
        {
            SqlCommand cmd = null;
            bool elimino = false;
            try
            {
                SqlConnection cn = Conexion.Instancia.conectar();
                cmd = new SqlCommand("spEliminarProducto", cn);
                cmd.Parameters.AddWithValue("@productoID", productoID);
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
