﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Odbc;
using System.Data;
using System.Windows.Forms;

namespace CapaDatos
{
    public class Commandos
    {
        Conexion nuevo = new Conexion();
        Conexion nuevo2 = new Conexion();
        Conexion nuevo3 = new Conexion();
        public void pubInsertData(string sParametro)
        {          
            OdbcCommand cmd = nuevo.ObtenerConexion().CreateCommand();
            cmd.CommandText = sParametro;
            cmd.ExecuteNonQuery();
        }

        public DataTable pubSeleccionarData(string sParametro)
        {
            OdbcCommand comando = new OdbcCommand(sParametro,nuevo2.ObtenerConexion());
            OdbcDataAdapter adaptador = new OdbcDataAdapter(comando);
            try
            {
                DataTable tabla = new DataTable();
                adaptador.Fill(tabla);

                return tabla;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error, tabla inexistentee" +ex);
                return null;
            }
        }

        public DataTable pubSeleccionarCampos(string sParametro)
        {
            OdbcCommand comando = new OdbcCommand(sParametro, nuevo3.ObtenerConexion());
            OdbcDataAdapter adaptador = new OdbcDataAdapter(comando);
            DataTable tabla = new DataTable();
            adaptador.Fill(tabla);
            return tabla;
        }

    }
}
//hola soy absa desde la compu de Abner
