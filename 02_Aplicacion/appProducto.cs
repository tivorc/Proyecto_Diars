using _03_Dominio;
using _04_Presistencia;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _02_Aplicacion
{
    public class appProducto
    {
        #region singleton
        private static readonly appProducto _instancia = new appProducto();
        public static appProducto Instancia
        {
            get { return appProducto._instancia; }
        }
        #endregion singleton

        #region metodos

        public List<entProducto> ListaPlatos(int idSubTipoPlato)
        {
            try
            {
                return daoProducto.Instancia.ListaPlatos(idSubTipoPlato);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public List<entProducto> ListaPlatosDisponibles(int idSubTipoPlato)
        {
            try
            {
                return daoProducto.Instancia.ListaPlatosDisponibles(idSubTipoPlato);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public entProducto DevolverPlato(int platoID)
        {
            try
            {
                return daoProducto.Instancia.DevolverPlato(platoID);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public bool EditarProducto(entProducto p)
        {
            try
            {
                return daoProducto.Instancia.EditarProducto(p);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public bool InsertarProducto(entProducto p)
        {
            try
            {
                return daoProducto.Instancia.InsertarProducto(p);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        #endregion metodos
    }
}
