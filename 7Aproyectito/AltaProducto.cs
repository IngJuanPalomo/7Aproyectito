using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.OleDb;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace _7Aproyectito
{
    public partial class AltaProducto : Form
    {
        public AltaProducto()
        {
            InitializeComponent();
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtNombre.Text) || string.IsNullOrEmpty(txtCantidad.Text) || string.IsNullOrEmpty(txtPrecio.Text) || string.IsNullOrEmpty(txtCosto.Text))
            {
                MessageBox.Show("Debe completar los datos requeridos.", "Faltan datos", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                try
                {
                    OleDbConnection conn = new OleDbConnection(@"Provider=sqloledb;Data Source=JUANCPC;Initial Catalog=7ABD;Integrated Security=SSPI");
                    OleDbCommand comando = new OleDbCommand();
                    comando.CommandText = "insert into producto (nombre, cantidad, precio, costo) values ('" + txtNombre.Text + "','" + txtCantidad.Text + "','" + txtPrecio.Text + "','" + txtCosto.Text + "')";
                    comando.Connection = conn;
                    conn.Open();
                    comando.ExecuteNonQuery();
                    conn.Close();

                    txtNombre.Clear();
                    txtCantidad.Clear();
                    txtPrecio.Clear();
                    txtCosto.Clear();
                    lblExito.Visible = true;
                    lblError.Visible = false;
                }
                catch
                {
                    lblExito.Visible = false;
                    lblError.Visible = true;
                }

            }

        }

        private void btnRegresar_Click(object sender, EventArgs e)
        {
            Form1 menuPrincipal = new Form1();
            this.Hide();
            menuPrincipal.Show();
        }

        private void txtCantidad_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void txtPrecio_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void txtCosto_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }
    }
}
