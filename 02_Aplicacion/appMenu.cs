using _03_Dominio;
using _04_Presistencia;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _02_Aplicacion
{
    public class appMenu
    {
        #region singleton
        private static readonly appMenu _instancia = new appMenu();
        public static appMenu Instancia
        {
            get { return appMenu._instancia; }
        }
        #endregion singleton

        #region metodos
        public bool InsertarMenu(entMenu m)
        {
            try
            {
                return daoMenu.Instancia.InsertarMenu(m);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public List<entMenu> DevolverMenusPedido(int pedidoID)
        {
            try
            {
                return daoMenu.Instancia.DevolverMenusPedido(pedidoID);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        #endregion metodos
    }
}
