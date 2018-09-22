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
        Logica lo = new Logica();

        //Insertar lista = new Insertar();
        List<string> campos = new List<string>();

        //PEDIR NOMBRE DE LA FORMA---------------------------------------------
        Form forma;
        [Description("Nombre de la Forma")]
        [DisplayName("Form")]
        [Category("AreaDePrueba")]

        public Form Forma
        {
            get { return forma; }
            set { forma = value; }
        }
        //PEDIR NOMBRE DE LOS PROCEDIMIENTOS-------------------------------------
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
        private string[] list;
        public Navegador()
        {
            InitializeComponent();
          
        }
        public void nombreForm(Form fm)
        {
            forma = fm;
        }
        public void Conectar2()
        {
            
            try
            {
                lo.conectar();
                MessageBox.Show("Conectadito");
            }
            catch (Exception)
            {
                MessageBox.Show("Error :v");
                throw;
            }
            
        }
        private void button14_Click(object sender, EventArgs e)
        {

        }
       
        private void Btn_ingresar_Click(object sender, EventArgs e)
        {
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
                    }catch (Exception)
                    {
                        verificarIngreso = false;
                        MessageBox.Show("No se ha ingresado el Tag");
                    }
                }
            }
            
            if(verificarIngreso)
                MessageBox.Show("Ingreso exitoso");

            for (int i = 0; i < arrayCampos.Length; i++)
            {
                arrayCampos[i] = "";
            }
        }

        private void Btn_guardar_Click(object sender, EventArgs e)
        {
            try
            {
                //lo.InsertarDatos(tabla);
                //InsertarDatosProcedure();
                lo.InsertarDatosProcedure(nControl, procedimiento, list);
                MessageBox.Show("Guardado Exitosamente");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex);
                
            }
            
        }

        private void Btn_salir_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("¿Desea Salir?", "Salir", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1) == System.Windows.Forms.DialogResult.Yes)
            {
                this.close();
            }
        }

        private void close()
        {
            throw new NotImplementedException();
        }
    }
}
