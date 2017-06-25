using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _04_Presistencia
{
    public class Conexion
    {
        #region singleton
        private static readonly Conexion _instancia = new Conexion();
        public static Conexion Instancia
        {
            get { return Conexion._instancia; }
        }
        #endregion singleton

        #region metodos
        public SqlConnection conectar()
        {
            SqlConnection cn = new SqlConnection();
            //cn.ConnectionString = "Data source=proyectodiars2017.database.windows.net; Initial Catalog=ProyectoDiars; User ID=administrador; Password=Abcd1234";
            cn.ConnectionString = "Data source=.; Initial Catalog=ProyectoDiars; Integrated Security=true";
            return cn;
        }
        #endregion metodos
    }
}
