using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CapaDatos;


namespace CapaLogica
{
   public class Logica
    {

        Conexion con = new Conexion();
        Commandos comando = new Commandos();
        public void conectar()
        {
           
            
            try
            {
                con.ObtenerConexion();
            }
            catch (Exception)
            {
                throw;
            } 
        }

        public void InsertarDatosProcedure(int num, string callPro, string[] lista)
        {
            string signo = "?", coma = ",";
            string temp=null, aux;

           /* string[] lista = new string[3];
            lista[0] = "Julio";
            lista[1] = "Sicaja";
            lista[2] = "12";*/

            //For de concatenar todos las variables del procedimiento almacenado
            for (int i = 1; i <= num; i++)
            {
                if (i == num)
                {
                    temp = temp + signo;
                }
                else
                {
                    temp = temp + signo;
                    temp = temp + coma;
                }
            }
            aux = "{CALL " + callPro + "(" + temp + ")}";
            comando.InsertProcedure(aux, lista);   
        }
    }
}
