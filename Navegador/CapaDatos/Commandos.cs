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
        public void InsertData(string parametro)
        {
            
            OdbcCommand cmd = nuevo.ObtenerConexion().CreateCommand();
            cmd.CommandText = parametro;
            cmd.ExecuteNonQuery();
        }
        //saber el número de ? 
        public void InsertProcedure(string llamarPro,string[] lista)
        {
            int band = 0;
            OdbcCommand comando = new OdbcCommand(llamarPro, nuevo.ObtenerConexion());
            comando.CommandType = CommandType.StoredProcedure;

            //Hacer un ciclo para ingresar los datos que hay en la lista uno por uno
            foreach (var el in lista)
            {
               band++;
               comando.Parameters.AddWithValue(band.ToString(), el);      
            }
            comando.ExecuteNonQuery();
        } 
    }
}
