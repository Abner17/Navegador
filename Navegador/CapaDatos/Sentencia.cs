using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaLogica
{
    public class Sentencia
    {
        public static string sql;
        string[] campos;
        int posicion = 1;

        public void cancelarSentencia()
        {
            sql = "";
        }

        //Creacion de sentencia para Insertar
        public void insertar(string tabla, params string[] campos)
        {
            sql = "";
            sql = "INSERT INTO " + tabla + " (";
            for (int i = 0; i < campos.Length; i++)
            {
                sql = sql + campos[i] + ", ";
            }
            char[] quitar = { ',', ' ' };
            sql = sql.TrimEnd(quitar);
            sql = sql + ") VALUES (";
        }

        public void insertarCampos(string campo)
        {
            sql = sql + "'" + campo + "',";
        }
        public void terminarSentencia()
        {
            sql = sql.TrimEnd(',');
            sql = sql + ");";
            Console.WriteLine(sql);
        }
        public string obtenerSentencia()
        {
            return sql;
        }

        //Creacion de sentencia para Modificar
        public void actualizar(string tabla, params string[] campos)
        {
            sql = "";
            this.campos = campos;
            sql = "UPDATE " + tabla + " SET ";
        }
        public void modificarCampos(string campo)
        {
            if (posicion < campos.Length)
            {
                sql = sql + campos[posicion] + " = '" + campo + "', ";
                posicion++;
            }
            else
            {
                Console.WriteLine("Error indice incorrecto");
            }
        }
        public void terminarSentenciaModificar(string llavePrimaria)
        {
            char[] quitar = { ',', ' ' };
            sql = sql.TrimEnd(quitar);
            sql = sql + " WHERE " + campos[0] + " = '" + llavePrimaria + "';";
            Console.WriteLine(sql);
        }
        
        //Creacion de la sentencia para Seleccionar
        public void seleccionar(string tabla, params string[] campos)
        {
            sql = "";
            sql = "SELECT ";
            for (int i = 0; i < campos.Length; i++)
            {
                sql = sql + campos[i] + ", ";
            }
            char[] quitar = { ',', ' ' };
            sql = sql.TrimEnd(quitar);
            sql = sql + " FROM " + tabla + " WHERE status = '0'";
           
        }
    }
}
