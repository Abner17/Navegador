using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using CapaLogica;

namespace CapaDiseno
{
    
    public partial class Navegador : UserControl
    {
        
        private int sBanIngresar = 0;
        static int cantidadCampos;
        static string tabla;
        static string[] camposTabla;
        Logica lo = new Logica();
        Flechas fle = new Flechas();

        //Insertar lista = new Insertar();
        List<string> campos = new List<string>();
<<<<<<< HEAD
 
=======
       
>>>>>>> master

        //PEDIR NOMBRE DE LA FORMA------------------------------------------Prueba-de-Julio-
        Form forma;
        [Description("Nombre de la Forma")]
        [DisplayName("Form")]
        [Category("AreaDePrueba")]

        public Form Forma
        {
            get { return forma; }
            set { forma = value; }
        }

        //Hola soy Sindy

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
        DataGridView dataGr;
        [Description("Nombre del DataGridView")]
        [DisplayName("DataGridView")]
        [Category("AreaDePrueba")]
        public DataGridView DataGr
        {
            get { return dataGr; }
            set { dataGr = value; }
        }

        private int nControl;
        ///private string[] list;
        public Navegador()
        {
            InitializeComponent();

        }
        public void nombreForm(Form fm)
        {
            forma = fm;
        }




        private void button14_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Aiuda");
        }

        public void ingresarTabla_Campos(string table, params string[] camposIniciales)
        {
            cantidadCampos = camposIniciales.Length;
            tabla = table;
            camposTabla = camposIniciales;
        }
        private void Btn_ingresar_Click(object sender, EventArgs e)
        {
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
                    lo.insertarCampos(arrayCampos[i]);
                    arrayCampos[i] = "";
                }

                if (verificarIngreso)
                {
                    lo.terminarSentencia();
                    MessageBox.Show("Ingreso exitoso");
                }
            }
            else
            {
                MessageBox.Show("La cantidad de parametros no es igual a la cantidad de campos");
            }

        }
        private void priGuardar()
        {
            sBanIngresar = 0;
            try
            {
                lo.pubInsertarDatos();
                MessageBox.Show("Guardado Exitosamente");
                MessageBox.Show("1 002 pc_julio insert 23:40 20/agosto/2018 ");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex);
            }
        }

        private void Btn_guardar_Click(object sender, EventArgs e)
        {
            priGuardar();
        }

        private void Btn_editar_Click(object sender, EventArgs e)
        {
            lo.actualizar(tabla, camposTabla);
            lo.modificarCampos("karla");
            lo.modificarCampos("guatemala");
            lo.terminarSentenciaModificar("002");
            MessageBox.Show("Edicion Exitosa");
        }

        private void Btn_salir_Click(object sender, EventArgs e)
        {
            if (sBanIngresar == 1)
            {
                DialogResult res = MessageBox.Show("¿Desea guardar los datos?", "Salir", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Exclamation);
                if (res == DialogResult.Yes)
                {
                    priGuardar();
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
        }

        private void Btn_siguiente_Click(object sender, EventArgs e)
        {
            int flecha = 1;
            fle.movimiento(flecha, dataGr);
        }

        private void Btn_final_Click(object sender, EventArgs e)
        {
            int flecha = 3;
            fle.movimiento(flecha,dataGr);
        }

        private void Btn_inicio_Click(object sender, EventArgs e)
        {
            int flecha = 0;
            fle.movimiento(flecha,dataGr);
        }

        private void Btn_cancelar_Click(object sender, EventArgs e)
        {

        }

        private void Btn_imprimir_Click(object sender, EventArgs e)
        {

        }

<<<<<<< HEAD
        private void Btn_borrar_Click(object sender, EventArgs e)
        {
            int fila = DataGr.CurrentRow.Index;
            //lo.pubEliminar(tabla, fila.ToString(), camposTabla);
            Console.WriteLine("Fila:  " + fila);
=======
        private void Btn_refrescar_Click(object sender, EventArgs e)
        {
           DataTable table =  lo.refrescar(tabla, camposTabla);
            DataGr.DataSource = table;
>>>>>>> master
        }
    }
}
