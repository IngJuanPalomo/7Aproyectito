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
    public partial class ConsultaProducto : Form
    {
        public ConsultaProducto()
        {
            InitializeComponent();
        }

        private void ConsultaProducto_Load(object sender, EventArgs e)
        {
            OleDbConnection conn = new OleDbConnection(@"Provider=sqloledb;Data Source=JUANCPC;Initial Catalog=7ABD;Integrated Security=SSPI");
            string select = " select * from Producto";
            OleDbDataAdapter da = new OleDbDataAdapter(select, conn);
            DataSet ds = new DataSet();

            da.Fill(ds, "Producto");

            cbBusqueda.ValueMember = "Id";
            cbBusqueda.DisplayMember = "Nombre";
            cbBusqueda.DataSource = ds.Tables["Producto"];

            cbBusqueda.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            cbBusqueda.AutoCompleteSource = AutoCompleteSource.ListItems;
            cbBusqueda.SelectedIndex = -1;
        }

        private void cbBusqueda_SelectedIndexChanged(object sender, EventArgs e)
        {
            OleDbConnection conn = new OleDbConnection(@"Provider=sqloledb;Data Source=JUANCPC;Initial Catalog=7ABD;Integrated Security=SSPI");

            //string  select = " select * from Cliente where Id = " + txtId.Text;
            string select = " select * from Producto where Id = " + cbBusqueda.SelectedValue.ToString();
            OleDbDataAdapter da = new OleDbDataAdapter(select, conn);
            DataTable dt = new DataTable();

            da.Fill(dt);
            if (dt.Rows.Count != 0)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    txtNombre.Text = dr["Nombre"].ToString();
                    txtCantidad.Text = dr["Cantidad"].ToString();
                    txtPrecio.Text = dr["Precio"].ToString();
                    txtCosto.Text = dr["Costo"].ToString();
                }
            }
            conn.Close();
        }

        private void btnRegresar_Click(object sender, EventArgs e)
        {
            Form1 menuPrincipal = new Form1();
            this.Hide();
            menuPrincipal.Show();
        }

        private void btnEditar_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtNombre.Text) || string.IsNullOrEmpty(txtCantidad.Text) || string.IsNullOrEmpty(txtPrecio.Text) || string.IsNullOrEmpty(txtCosto.Text))
            {
                MessageBox.Show("Debe completar los datos requeridos.", "Faltan datos", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                OleDbConnection conn = new OleDbConnection(@"Provider=sqloledb;Data Source=JUANCPC;Initial Catalog=7ABD;Integrated Security=SSPI");
                OleDbCommand comando = new OleDbCommand();
                comando.CommandText = "update Producto set Nombre = '" + txtNombre.Text + "', Cantidad = '" + txtCantidad.Text + "', Precio = '" + txtPrecio.Text + "', Costo = '" + txtCosto.Text + "' where id = " + cbBusqueda.SelectedValue.ToString();

                try
                {
                    comando.Connection = conn;
                    conn.Open();
                    comando.ExecuteNonQuery();
                    //MessageBox.Show("Se ha actualizado corretamente el producto");
                    txtNombre.Clear();
                    txtCantidad.Clear();
                    txtPrecio.Clear();
                    txtCosto.Clear();
                    lblExitoEditar.Visible = true;
                    lblErrorEditar.Visible = false;
                    lblExitoEliminar.Visible = false;
                    lblErrorEliminar.Visible = false;
                }
                catch (Exception ex)
                {
                    //MessageBox.Show("Ha ocurrido un error");
                    lblExitoEditar.Visible = false;
                    lblErrorEditar.Visible = true;
                    lblExitoEliminar.Visible = false;
                    lblErrorEliminar.Visible = false;
                }
                conn.Close();
            }

        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            OleDbConnection conn = new OleDbConnection(@"Provider=sqloledb;Data Source=JUANCPC;Initial Catalog=7ABD;Integrated Security=SSPI");
            OleDbCommand comando = new OleDbCommand();
            comando.CommandText = "delete from Producto where id = " + cbBusqueda.SelectedValue.ToString();

            try
            {
                comando.Connection = conn;
                conn.Open();
                comando.ExecuteNonQuery();
                //MessageBox.Show("Se ha eliminado corretamente el producto");
                txtNombre.Clear();
                txtCantidad.Clear();
                txtPrecio.Clear();
                txtCosto.Clear();
                lblExitoEditar.Visible = false;
                lblErrorEditar.Visible = false;
                lblExitoEliminar.Visible = true;
                lblErrorEliminar.Visible = false;

            }
            catch (Exception ex)
            {
                //MessageBox.Show("Ha ocurrido un error");
                lblExitoEditar.Visible = false;
                lblErrorEditar.Visible = false;
                lblExitoEliminar.Visible = false;
                lblErrorEliminar.Visible = true;
            }
            conn.Close();
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
