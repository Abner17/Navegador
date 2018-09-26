using System;
using System.Collections.Generic;
using System.Data.Odbc;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace CapaDatos
{
    public class Conexion
    {
        public OdbcConnection ObtenerConexion()
        {
            OdbcConnection conectar = new OdbcConnection("Dsn=pruebas");
            conectar.Open();
            return conectar;
        }
        public void funcion()
        {
           
        }

    }
}
