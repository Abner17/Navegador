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

        //Ayuda de Douglas
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
            if (forma.CanSelect)
            {
                Help.ShowHelp(this, "C:\\Ayuda\\" + sNombrechm, sNombrehtml);
                //MessageBox.Show(sNombrechm);
            }
        }

        public void ingresarTabla_Campos(string table, params string[] camposIniciales)
        {
            cantidadCampos = camposIniciales.Length;
            tabla = table;
            camposTabla = camposIniciales;
        }
        private void Btn_ingresar_Click(object sender, EventArgs e)
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

        bool existeGrid = false;
        Panel panel = new Panel();
        int y = 40;
        int conteo = 0;
        int n;
        private void Btn_editar_Click(object sender, EventArgs e)
        {
            foreach (Control c in forma.Controls)
            {
                if (c is Panel)
                {
                    foreach (Control d in c.Controls)
                    {
                        if (d is DataGridView)
                        {
                            existeGrid = true;
                        }
                    }
                }


                if (c is DataGridView)
                {
                    existeGrid = true;

                }
            }

            //validacion para poder editar


            if (existeGrid == true)
            {
                string datoindice;
                try
                {
                    datoindice = dataGr.CurrentRow.Index.ToString();
                }
                catch (Exception)
                {
                    datoindice = "";
                }
                if (datoindice != "")
                {
                    panel.Visible = true;
                    conteo = 0;
                    panel.Controls.Clear();
                    y = 40;
                    n = dataGr.CurrentRow.Index;
                    int columnas = dataGr.ColumnCount;
                    string columnName = "";
                    string dato = "";
                    Label label = new Label();
                    label.Text = "Herramienta Editar";
                    panel.Controls.Add(label);
                    label.Location = new Point(5, 5);

                    for (int i = 1; i < columnas; i++)
                    {

                        columnName = dataGr.Columns[i].HeaderText;


                        dato = Convert.ToString(dataGr.Rows[n].Cells[i].Value);
                        crearTextBox(columnName, dato);
                    }

                    Button button = new Button();
                    button.Text = "ACTUALIZAR DATOS";
                    button.Name = "buton";
                    button.Height = 30;
                    button.Width = 150;
                    button.BackColor = Color.WhiteSmoke;
                    button.Location = new Point(120, 7);
                    panel.Controls.Add(button);
                    panel.Height = y;
                    forma.Controls.Add(panel);
                    panel.BringToFront();



                    button.Click += new EventHandler(actualizarABD);
                }
                else
                {
                    MessageBox.Show("debe seleccionar una fila para habilitar la edicion");

                }

            }
            else
            {
                MessageBox.Show("No existe una tabla de edicion, por lo tanto no es posible esta accion");
            }
        }


        private void actualizarABD(object sender, EventArgs e)
        {
            int columnas = dataGr.ColumnCount;
            string columnName = "";
            string dato = "";
            string primerdato = "";
            lo.actualizar(null, null);
           lo.actualizar(tabla, camposTabla);
            for (int i = 1; i < columnas; i++)
            {
                foreach (Control c in forma.Controls)
                {
                    if (c is Panel)
                    {
                        foreach (Control d in c.Controls)
                        {
                            if (d is TextBox)
                            {
                                if (d.Name == "txtEditar" + i)
                                {
                                   lo.modificarCampos(d.Text);
                                }
                            }
                        }
                    }
                    if (c is TextBox)
                    {
                        if (c.Name == "txtEditar" + i)
                        {
                         lo.modificarCampos(c.Text);
                        }
                    }
                }
            }
            dato = Convert.ToString(dataGr.Rows[n].Cells[0].Value);
            lo.terminarSentenciaModificar(dato);
            lo.limpiarsql();
            panel.Controls.Clear();
            panel.Visible = false;
            MessageBox.Show("Datos Actualizados");
        }

        private void crearTextBox(string encabezado, string dato)
        {
            Label label = new Label();
            label.Text = encabezado + ": ";
            label.BackColor = Color.Wheat;
            label.Size = new System.Drawing.Size(70, 20);
            label.Location = new Point(5, y);
            panel.Controls.Add(label);
            panel.Location = new Point(5, 70);
            panel.Width = 290;
            TextBox tem = new TextBox();
            tem.Height = 23;
            tem.Width = 200;
            tem.Location = new Point(85, y);
            y += 25;
            conteo++;
            tem.Name = "txtEditar" + conteo.ToString();
            tem.Text = dato;
            panel.Controls.Add(tem);
            panel.BackColor = Color.Gray;

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
            Btn_guardar.Enabled = false;

            DialogResult res = MessageBox.Show("¿Desea cancelar la accion?", "Cancelar", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Exclamation);
            if (res == DialogResult.Yes)
            {
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
    }
}
