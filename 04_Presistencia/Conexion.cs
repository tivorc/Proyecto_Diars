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
            cn.ConnectionString = "Data source=; Initial Catalog=BDLaValentina; Integrated Security=true";
            //cn.ConnectionString = "Server = tcp:proyectodiars2017.database.windows.net,1433; Initial Catalog = ProyectoDiars; Persist Security Info = False; User ID = administrador; Password = Abcd1234; MultipleActiveResultSets = False; Encrypt = True; TrustServerCertificate = False; Connection Timeout = 30;";
            return cn;
        }
        #endregion metodos
    }
}
