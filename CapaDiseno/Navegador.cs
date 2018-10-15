using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net;
using System.Net.NetworkInformation;
using CapaLogica;
using DLL__Reporteador;
using ConsultasInteligentes;

namespace CapaDiseno
{

    public partial class Navegador : UserControl
    {

        private int sBanIngresar = 0;
        private int nControl;
        static int cantidadCampos;
        static string tabla;
        static string estado;
        static string[] camposTabla;
        static List<string> camposTabla2 = new List<string>();
        Logica lo = new Logica();
        Flechas fle = new Flechas();

        //Ayuda nombre CHM
        string sNombrechm;
        [Description("Nombre de la Forma")]
        [DisplayName("Nombre CHM")]
        [Category("AreaDePrueba")]
        public string pubNombrechm
        {
            get { return sNombrechm; }
            set { sNombrechm = value; }
        }

        string sNombrehtml;
        [Description("Nombre de la Forma")]
        [DisplayName("Nombre HTML")]
        [Category("AreaDePrueba")]
        public string pubNombreHtml
        {
            get { return sNombrehtml; }
            set { sNombrehtml = value; }
        }

        //PEDIR NOMBRE DE LA FORMA
        Form forma;
        [Description("Nombre de la Forma")]
        [DisplayName("Form")]
        [Category("AreaDePrueba")]
        public Form Forma
        {
            get { return forma; }
            set { forma = value; }
        }

        //PEDIR NOMBRE DE LOS PROCEDIMIENTOS--------------------------------RAMAS-----
        // comentario....
        string procedimiento;
        [Description("Nombre del Procedimiento")]
        [DisplayName("Procedimiento")]
        [Category("AreaDePrueba")]
        public string Procedimiento
        {
            get { return procedimiento; }
            set { procedimiento = value; }
        }

        //DATAGRID----------------------------------------------------------------
        static DataGridView dataGr;
        [Description("Nombre del DataGridView")]
        [DisplayName("DataGridView")]
        [Category("AreaDePrueba")]
        public DataGridView DataGr
        {
            get { return dataGr; }
            set { dataGr = value; }
        }

        ///private string[] list;
        public Navegador()
        {
            InitializeComponent();
            Btn_guardar.Enabled = false;
            Btn_cancelar.Enabled = false;
        }
        public void nombreForm(Form fm)
        {
            forma = fm;
        }


  

        private void button14_Click(object sender, EventArgs e)
        {
            if (forma.CanSelect)
            {
                Help.ShowHelp(this, "C:\\Ayuda\\" + sNombrechm, sNombrehtml);
                //MessageBox.Show(sNombrechm);
            }
        }
        public void dgv_datos(DataGridView aux)
        {
            dataGr = aux;
        }
        public void ingresarTabla_Campos(string table)
        {
            
            tabla = table;
            DataTable table2 = lo.pubObtenerCampos(tabla);
            for (int i = 0; i < table2.Rows.Count; i++)
            {
                camposTabla2.Add(table2.Rows[i][0].ToString());
            }
            camposTabla = camposTabla2.ToArray();
            cantidadCampos = camposTabla.Length;
            for (int i = 0; i < camposTabla.Length; i++)
            {
                Console.WriteLine(camposTabla[i]);
            }
        }

        private void priInsertar()
        {
            List<string> campos = new List<string>();
            sBanIngresar = 1;
            lo.insertar(tabla, camposTabla);
            bool verificarIngreso = true;
            int j = 0;
            foreach (Control componente in forma.Controls)
            {
                if ((componente is TextBox) || (componente is ComboBox))
                {
                    campos.Add("");
                    j++;
                }
            }
            nControl = j;
            string[] arrayCampos = campos.ToArray();

            foreach (Control componente in forma.Controls)
            {
                if ((componente is TextBox) || (componente is ComboBox))
                {
                    try
                    {
                        string num = componente.Tag.ToString();
                        int numero = Convert.ToInt32(num) - 1;
                        arrayCampos[numero] = componente.Text;
                        componente.Text = "";
                    }
                    catch (Exception)
                    {
                        verificarIngreso = false;
                        MessageBox.Show("No se ha ingresado el Tag del elemento " + componente.Name);
                    }
                }
            }

            for (int i = 0; i < arrayCampos.Length; i++)
            {
                lo.insertarCampos(arrayCampos[i]);
                arrayCampos[i] = "";
            }

            if (verificarIngreso)
            {
                lo.terminarSentencia();
            }
        }

        private void Btn_ingresar_Click(object sender, EventArgs e)
        {
            int j = 0;
            sBanIngresar = 1;
            Btn_ingresar.Enabled = false;
            foreach (Control componente in forma.Controls)
            { 
                if ((componente is TextBox) || (componente is ComboBox))
                {
                    componente.Text = "";
                    j++;
                }
            }
            nControl = j;
            if (j == cantidadCampos)
            {
                estado = "insertar";
                Btn_guardar.Enabled = true;
                Btn_cancelar.Enabled = true;

                Btn_editar.Enabled = false;
                Btn_borrar.Enabled = false;
                Btn_consultar.Enabled = false;
                Btn_imprimir.Enabled = false;
                Btn_refrescar.Enabled = false;
                Btn_inicio.Enabled = false;
                Btn_anterior.Enabled = false;
                Btn_siguiente.Enabled = false;
                Btn_final.Enabled = false;
            }
            else
            {
                MessageBox.Show("La cantidad de parametros no es igual a la cantidad de campos");
            }
        }

        private void priGuardar()
        {
            IPHostEntry host;
            string localIP = "";
            host = Dns.GetHostEntry(Dns.GetHostName());
            foreach (IPAddress ip in host.AddressList)
            {
                if (ip.AddressFamily.ToString() == "InterNetwork")
                {
                    localIP = ip.ToString();
                }
            }
            sBanIngresar = 0;
            try
            {
                lo.pubInsertarDatos();
                MessageBox.Show("Guardado Exitosamente");
                MessageBox.Show("Ip: " + localIP + "\n Fecha y hora: " + DateTime.Now.ToString("G"));
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex);
            }
        }

        private void Btn_guardar_Click(object sender, EventArgs e)
        {
            if (estado.Equals("insertar"))
            {
                priInsertar();
                priGuardar();
                Btn_guardar.Enabled = false;
                Btn_cancelar.Enabled = false;
                Btn_ingresar.Enabled = true;
                Btn_editar.Enabled = true;
                Btn_borrar.Enabled = true;
                Btn_consultar.Enabled = true;
                Btn_imprimir.Enabled = true;
                Btn_refrescar.Enabled = true;
                Btn_inicio.Enabled = true;
                Btn_anterior.Enabled = true;
                Btn_siguiente.Enabled = true;
                Btn_final.Enabled = true;
            }else if (estado.Equals("editar")){
                priEditar();
                priGuardar();
                Btn_guardar.Enabled = false;
                Btn_cancelar.Enabled = false;
                Btn_ingresar.Enabled = true;
                
                Btn_borrar.Enabled = true;
                Btn_consultar.Enabled = true;
                Btn_imprimir.Enabled = true;
                Btn_refrescar.Enabled = true;
                Btn_inicio.Enabled = true;
                Btn_anterior.Enabled = true;
                Btn_siguiente.Enabled = true;
                Btn_final.Enabled = true;
            }
        }

        private void priEditar()
        {
            string dato = "";
            List<string> campos = new List<string>();
            lo.actualizar(null, null);
            lo.actualizar(tabla, camposTabla);
            bool verificarIngreso = true;
            int j = 0;
            foreach (Control componente in forma.Controls)
            {
                if ((componente is TextBox) || (componente is ComboBox))
                {
                    campos.Add("");
                    j++;
                }
            }
            nControl = j;
            if (j == cantidadCampos)
            {
                string[] arrayCampos = campos.ToArray();

                foreach (Control componente in forma.Controls)
                {
                    if ((componente is TextBox) || (componente is ComboBox))
                    {
                        try
                        {
                            string num = componente.Tag.ToString();
                            int numero = Convert.ToInt32(num) - 1;
                            arrayCampos[numero] = componente.Text;
                            componente.Text = "";
                        }
                        catch (Exception)
                        {
                            verificarIngreso = false;
                            MessageBox.Show("No se ha ingresado el Tag del elemento " + componente.Name);
                        }
                    }
                }

                for (int i = 0; i < arrayCampos.Length; i++)
                {
                    if (i == 0)
                    {
                        dato = arrayCampos[i];
                        arrayCampos[i] = "";
                    }
                    else
                    {
                        lo.modificarCampos(arrayCampos[i]);
                        arrayCampos[i] = "";
                    }
                }

                if (verificarIngreso)
                {
                    lo.terminarSentenciaModificar(dato);
                    MessageBox.Show("Edicion terminada");
                }
            }
            else
            {
                MessageBox.Show("La cantidad de parametros no es igual a la cantidad de campos");
            }
        }
        
        private void Btn_editar_Click(object sender, EventArgs e)
        {
            int j = 0;
            sBanIngresar = 1;
            Btn_editar.Enabled = false;
            foreach (Control componente in forma.Controls)
            {
                if ((componente is TextBox) || (componente is ComboBox))
                {
                    j++;
                }
            }
            nControl = j;
            if (j == cantidadCampos)
            {
                estado = "editar";
                Btn_guardar.Enabled = true;
                Btn_cancelar.Enabled = true;
                
                Btn_ingresar.Enabled = false;
                Btn_borrar.Enabled = false;
                Btn_consultar.Enabled = false;
                Btn_imprimir.Enabled = false;
                Btn_refrescar.Enabled = false;
                Btn_inicio.Enabled = false;
                Btn_anterior.Enabled = false;
                Btn_siguiente.Enabled = false;
                Btn_final.Enabled = false;
            }
            else
            {
                MessageBox.Show("La cantidad de parametros no es igual a la cantidad de campos");
            }
        }



        private void Btn_salir_Click(object sender, EventArgs e)
        {
            if (sBanIngresar == 1)
            {
                DialogResult res = MessageBox.Show("¿Desea guardar los datos?", "Salir", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Exclamation);
                if (res == DialogResult.Yes)
                {
                    if (estado.Equals("insertar"))
                    {
                        priInsertar();
                        priGuardar();
                        Btn_guardar.Enabled = false;
                        Btn_cancelar.Enabled = false;

                        Btn_editar.Enabled = true;
                        Btn_borrar.Enabled = true;
                        Btn_consultar.Enabled = true;
                        Btn_imprimir.Enabled = true;
                        Btn_refrescar.Enabled = true;
                        Btn_inicio.Enabled = true;
                        Btn_anterior.Enabled = true;
                        Btn_siguiente.Enabled = true;
                        Btn_final.Enabled = true;
                    }
                    else if (estado.Equals("editar"))
                    {
                        priEditar();
                        priGuardar();
                        Btn_guardar.Enabled = false;
                        Btn_cancelar.Enabled = false;

                        Btn_ingresar.Enabled = true;
                        Btn_borrar.Enabled = true;
                        Btn_consultar.Enabled = true;
                        Btn_imprimir.Enabled = true;
                        Btn_refrescar.Enabled = true;
                        Btn_inicio.Enabled = true;
                        Btn_anterior.Enabled = true;
                        Btn_siguiente.Enabled = true;
                        Btn_final.Enabled = true;
                    }
                    forma.Close();
                }
                else if (res == DialogResult.No)
                {
                    sBanIngresar = 0;
                    forma.Close();                 
                }               
            }else
            {
                forma.Close();
            }
        }

        private void Btn_anterior_Click(object sender, EventArgs e)
        {
            //Manda el número de flecha al que pertenece este botón para saber hacia donde moverse.
            int flecha = 2;
            fle.movimiento(flecha,dataGr);
            int fila = DataGr.CurrentRow.Index;
            foreach (Control componente in forma.Controls)
            {
                if ((componente is TextBox) || (componente is ComboBox))
                {
                    string num = componente.Tag.ToString();
                    int numero = Convert.ToInt32(num) - 1;
                    componente.Text = dataGr.Rows[fila].Cells[numero].Value.ToString();
                }
            }
        }

        private void Btn_siguiente_Click(object sender, EventArgs e)
        {
            int flecha = 1;
            fle.movimiento(flecha, dataGr);
            int fila = DataGr.CurrentRow.Index;
            foreach (Control componente in forma.Controls)
            {
                if ((componente is TextBox) || (componente is ComboBox))
                {
                    string num = componente.Tag.ToString();
                    int numero = Convert.ToInt32(num) - 1;
                    componente.Text = dataGr.Rows[fila].Cells[numero].Value.ToString();
                }
            }
        }

        private void Btn_final_Click(object sender, EventArgs e)
        {
            int flecha = 3;
            fle.movimiento(flecha,dataGr);
            int fila = DataGr.CurrentRow.Index;
            foreach (Control componente in forma.Controls)
            {
                if ((componente is TextBox) || (componente is ComboBox))
                {
                    string num = componente.Tag.ToString();
                    int numero = Convert.ToInt32(num) - 1;
                    componente.Text = dataGr.Rows[fila].Cells[numero].Value.ToString();
                }
            }
        }

        private void Btn_inicio_Click(object sender, EventArgs e)
        {
            int flecha = 0;
            fle.movimiento(flecha,dataGr);
            int fila = DataGr.CurrentRow.Index;
            foreach (Control componente in forma.Controls)
            {
                if ((componente is TextBox) || (componente is ComboBox))
                {
                    string num = componente.Tag.ToString();
                    int numero = Convert.ToInt32(num) - 1;
                    componente.Text = dataGr.Rows[fila].Cells[numero].Value.ToString();
                }
            }
        }

        private void Btn_cancelar_Click(object sender, EventArgs e)
        {
            Btn_guardar.Enabled = false;
       
            
            DialogResult res = MessageBox.Show("¿Desea cancelar la accion?", "Cancelar", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation);
            if (res == DialogResult.Yes)
            {
                Btn_cancelar.Enabled = false;
                Btn_ingresar.Enabled = true;
                Btn_editar.Enabled = true;
                Btn_borrar.Enabled = true;
                Btn_consultar.Enabled = true;
                Btn_imprimir.Enabled = true;
                Btn_refrescar.Enabled = true;
                Btn_inicio.Enabled = true;
                Btn_anterior.Enabled = true;
                Btn_siguiente.Enabled = true;
                Btn_final.Enabled = true;
                sBanIngresar = 0;
                foreach (Control componente in forma.Controls)
                {

                    if ((componente is TextBox) || (componente is ComboBox))
                    {
                        componente.Text = "";
                    }
                }
                MessageBox.Show("Accion Cancelada");
            }
            else if (res == DialogResult.No)
            {

            }
        }

        private void Btn_imprimir_Click(object sender, EventArgs e)
        {
            Reporteador frmMSG = new Reporteador();
            frmMSG.Show();
        }

        private void Btn_borrar_Click(object sender, EventArgs e)
        {
            int fila = DataGr.CurrentRow.Index;
            string id = dataGr.Rows[fila].Cells[0].Value.ToString();
            lo.pubEliminar(tabla, id, camposTabla);
            Console.WriteLine("FilasSS:  " + fila);
            Console.WriteLine("iD:  " + id);

            MessageBox.Show("Borrado Exitosamente");
        }
        private void Btn_refrescar_Click(object sender, EventArgs e)
        {
           DataTable table =  lo.refrescar(tabla, camposTabla);
            DataGr.DataSource = table;
        }

        private void Btn_consultar_Click(object sender, EventArgs e)
        {
            Consulta cn = new Consulta("josueponciano", "fabricante");
            cn.abrir();
        }
    }
}
