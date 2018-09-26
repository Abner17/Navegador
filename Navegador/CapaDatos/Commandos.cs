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
        Conexion nuevo2 = new Conexion();
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
            DataTable tabla = new DataTable();
            adaptador.Fill(tabla);

            return tabla;
        }
 
    }
}
