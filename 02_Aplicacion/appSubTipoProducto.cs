using _03_Dominio;
using _04_Presistencia;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _02_Aplicacion
{
    public class appSubTipoProducto
    {
        #region singleton
        private static readonly appSubTipoProducto _instancia = new appSubTipoProducto();
        public static appSubTipoProducto Instancia
        {
            get { return appSubTipoProducto._instancia; }
        }
        #endregion singleton

        #region metodos

        public List<entSubTipoProducto> ListaSubTipoProductos(int tipoPlatoID)
        {
            try
            {
                return daoSubTipoProducto.Instancia.ListaSubTipoProductos(tipoPlatoID);
            }
            catch (Exception e)
            {
                throw e;
            }
        }


        #endregion metodos
    }
}
