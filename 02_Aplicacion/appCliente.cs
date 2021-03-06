﻿using _03_Dominio;
using _04_Presistencia;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _02_Aplicacion
{
    public class appCliente
    {
        #region singleton
        private static readonly appCliente _instancia = new appCliente();
        public static appCliente Instancia
        {
            get { return appCliente._instancia; }
        }
        #endregion singleton

        #region metodos
        public bool InsertarCliente(entCliente cli)
        {
            try
            {
                return daoCliente.Instancia.InsertarCliente(cli);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public List<entCliente> ListaCliente(string idNombre)
        {
            try
            {
                return daoCliente.Instancia.ListaCliente(idNombre);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public entCliente DevolverCliente(int idCliente)
        {
            try
            {
                return daoCliente.Instancia.DevolverCliente(idCliente);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public entCliente DevolverClienteLogin(int idUsuario)
        {
            try
            {
                return daoCliente.Instancia.DevolverClienteLogin(idUsuario);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public bool EditarClienteIntranet(entCliente c)
        {
            try
            {
                return daoCliente.Instancia.EditarClienteIntranet(c);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public bool EliminarCliente(int clienteID)
        {
            try
            {
                return daoCliente.Instancia.EliminarCliente(clienteID);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        #endregion metodos
    }
}
