using _03_Dominio;
using _04_Presistencia;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _02_Aplicacion
{
    public class appMesa
    {
        #region singleton
        private static readonly appMesa _instancia = new appMesa();
        public static appMesa Instancia
        {
            get { return appMesa._instancia; }
        }
        #endregion singleton

        #region metodos

        public List<entMesa> ListarMesas()
        {
            try
            {
                return daoMesa.Instancia.ListarMesas();
            }
            catch (Exception e)
            {
                throw e;
            }
        }


        #endregion metodos
    }
}
