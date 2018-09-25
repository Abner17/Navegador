using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Odbc;
using System.Data;

namespace CapaDatos
{
    public class Commandos
    {
        Conexion nuevo = new Conexion();
        public void pubInsertData(string sParametro)
        {          
            OdbcCommand cmd = nuevo.ObtenerConexion().CreateCommand();
            cmd.CommandText = sParametro;
            cmd.ExecuteNonQuery();
        }
        /*
        public void pubBitacora()
        {
            OdbcCommand cmd = nuevo.ObtenerConexion().CreateCommand();
            cmd.CommandText = "'Insert into Bitacora values ()'";
            cmd.ExecuteNonQuery();
        }*/

    }
}
