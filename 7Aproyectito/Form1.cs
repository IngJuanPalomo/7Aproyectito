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
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            AltaCliente altaCliente = new AltaCliente();
            this.Hide();
            altaCliente.Show();
        }

        private void btnConsulta_Click(object sender, EventArgs e)
        {
            ConsultaCliente consultaCliente = new ConsultaCliente();
            this.Hide();
            consultaCliente.Show();
        }

        private void btnAltaProducto_Click(object sender, EventArgs e)
        {
            AltaProducto altaProducto = new AltaProducto();
            this.Hide();
            altaProducto.Show();
        }

        private void btnConsultaProducto_Click(object sender, EventArgs e)
        {
            ConsultaProducto consultaProducto = new ConsultaProducto();
            this.Hide();
            consultaProducto.Show();
        }

        private void btnCotizacion_Click(object sender, EventArgs e)
        {
            Cotizacion cotizacion = new Cotizacion();
            this.Hide();
            cotizacion.Show();
        }

        private void btnVenta_Click(object sender, EventArgs e)
        {
            Venta venta = new Venta();
            this.Hide();
            venta.Show();
        }

        private void btnConsultaVenta_Click(object sender, EventArgs e)
        {
            ConsultaVenta consultaventa = new ConsultaVenta();
            this.Hide();
            consultaventa.Show();
        }
    }
}
