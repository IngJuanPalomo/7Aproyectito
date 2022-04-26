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
    public partial class AltaCliente : Form
    {
        public AltaCliente()
        {
            InitializeComponent();
        }

        private void btnGuardar_Click_1(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtNombre.Text))
            {
                MessageBox.Show("El campo nombre es requerido.", "Faltan datos", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                try
                {
                    OleDbConnection conn = new OleDbConnection(@"Provider=sqloledb;Data Source=JUANCPC;Initial Catalog=7ABD;Integrated Security=SSPI");
                    OleDbCommand comando = new OleDbCommand();
                    comando.CommandText = "insert into cliente (nombre, razonsocial, rfc, telefono, direccion) values ('" + txtNombre.Text + "','" + txtRazonSocial.Text + "','" + txtRFC.Text + "','" + txtTelefono.Text + "','" + txtDireccion.Text + "')";
                    comando.Connection = conn;
                    conn.Open();
                    comando.ExecuteNonQuery();
                    conn.Close();

                    txtNombre.Clear();
                    txtRazonSocial.Clear();
                    txtRFC.Clear();
                    txtTelefono.Clear();
                    txtDireccion.Clear();
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

        private void btnRegresar_Click_1(object sender, EventArgs e)
        {
            Form1 menuPrincipal = new Form1();
            this.Hide();
            menuPrincipal.Show();
        }

        private void txtTelefono_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void txtNombre_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsLetter(e.KeyChar) && !char.IsWhiteSpace(e.KeyChar))
            {
                e.Handled = true;
            }
        }
    }
}
