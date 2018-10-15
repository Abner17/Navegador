using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DLL__Reporteador;

namespace CapaDiseno
{
    public partial class Reporteador : Form
    {
        public Reporteador()
        {
            InitializeComponent();
        }

        private void reporte (String nombre)
        {
            nombre = nombre + ".rpt";
            DLL__Reporteador.Visualizador report = new Visualizador();
            report.AbrirReporte(nombre);
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            txtRerporter.Text = "";
        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnAcptar_Click(object sender, EventArgs e)
        {
            reporte(txtRerporter.Text);
        }
    }
}
