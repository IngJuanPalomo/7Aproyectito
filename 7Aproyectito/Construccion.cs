using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace _7Aproyectito
{
    public partial class Construccion : Form
    {
        public Construccion()
        {
            InitializeComponent();
        }

        private void btnContinuar_Click(object sender, EventArgs e)
        {
            ConsultaVenta consultaventa = new ConsultaVenta();
            this.Hide();
            consultaventa.Show();
        }

        private void btnRegresar_Click(object sender, EventArgs e)
        {
            Form1 menuPrincipal = new Form1();
            this.Hide();
            menuPrincipal.Show();
        }
    }
}
