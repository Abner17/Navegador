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
        Sentencia sen = new Sentencia();

        Commandos comando = new Commandos();
        //string sSentencia = "INSERT INTO prueba VALUES('Julios', 'Lutin', '43')";

        public void pubInsertarDatos()
        {
            comando.pubInsertData(sen.obtenerSentencia());
        }
        public void insertar(string sTabla, string[] sCampos)
        {
            sen.insertar(sTabla, sCampos);
        }
        //Boton Ingresar--------------------------------------
        public void insertarCampos(string sCampos)
        {
            sen.insertarCampos(sCampos);
        }
        public void terminarSentencia()
        {
            sen.terminarSentencia();
        }

        //Boton Editar-----------------------------------------

        public void actualizar(string sTa, string[] sCampos)
        {
            sen.actualizar(sTa, sCampos);
        }
        public void modificarCampos(string sCampos)
        {
            sen.modificarCampos(sCampos);
        }
        public void terminarSentenciaModificar(string sKey)
        {
            sen.terminarSentenciaModificar(sKey);
        }
        public void pubEliminar(string tabla,string id, params string[] campos)
        {
            sen.pubDelete(tabla, id, campos);
            comando.pubInsertData(sen.obtenerSentencia());
        }

    }
}
