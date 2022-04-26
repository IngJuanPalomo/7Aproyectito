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
    public partial class ConsultaCliente : Form
    {
        public ConsultaCliente()
        {
            InitializeComponent();
        }

        private void btnConsultar_Click(object sender, EventArgs e)
        {
            //OleDbConnection conn = new OleDbConnection(@"Provider=sqloledb;Data Source=JUANCPC;Initial Catalog=7ABD;Integrated Security=SSPI");

            ////string  select = " select * from Cliente where Id = " + txtId.Text;
            //string select = " select * from Cliente where Id = " + cbBusqueda.SelectedValue.ToString();
            //OleDbDataAdapter da = new OleDbDataAdapter(select, conn);
            //DataTable dt = new DataTable();

            //da.Fill(dt);
            //if (dt.Rows.Count != 0)
            //{
            //    foreach (DataRow dr in dt.Rows)
            //    {
            //        txtNombre.Text = dr["Nombre"].ToString();
            //        txtRazonSocial.Text = dr["RazonSocial"].ToString();
            //        txtRFC.Text = dr["RFC"].ToString();
            //        txtTelefono.Text = dr["Telefono"].ToString();
            //    }
            //}
            //conn.Close();
        }

        private void ConsultaCliente_Load(object sender, EventArgs e)
        {
            OleDbConnection conn = new OleDbConnection(@"Provider=sqloledb;Data Source=JUANCPC;Initial Catalog=7ABD;Integrated Security=SSPI");
            string select = " select * from Cliente";
            OleDbDataAdapter da = new OleDbDataAdapter(select, conn);
            DataSet ds = new DataSet();

            da.Fill(ds, "Cliente");

            cbBusqueda.ValueMember = "Id";
            cbBusqueda.DisplayMember = "RazonSocial";
            cbBusqueda.DataSource = ds.Tables["Cliente"];

            cbBusqueda.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            cbBusqueda.AutoCompleteSource = AutoCompleteSource.ListItems;
            cbBusqueda.SelectedIndex = -1;

            txtNombre.Text = string.Empty;
        }

        private void cbBusqueda_SelectedIndexChanged(object sender, EventArgs e)
        {
            OleDbConnection conn = new OleDbConnection(@"Provider=sqloledb;Data Source=JUANCPC;Initial Catalog=7ABD;Integrated Security=SSPI");

            //string  select = " select * from Cliente where Id = " + txtId.Text;
            string select = " select * from Cliente where Id = " + cbBusqueda.SelectedValue.ToString();
            OleDbDataAdapter da = new OleDbDataAdapter(select, conn);
            DataTable dt = new DataTable();

            da.Fill(dt);
            if (dt.Rows.Count != 0)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    txtNombre.Text = dr["Nombre"].ToString();
                    txtRazonSocial.Text = dr["RazonSocial"].ToString();
                    txtRFC.Text = dr["RFC"].ToString();
                    txtTelefono.Text = dr["Telefono"].ToString();
                    txtDireccion.Text = dr["Direccion"].ToString();
                }
            }
            conn.Close();

            //OleDbConnection conn = new OleDbConnection(@"Provider=sqloledb;Data Source=JUANCPC;Initial Catalog=7ABD;Integrated Security=SSPI");
            //conn.Open();
            ////string  select = " select * from Cliente where Id = " + txtId.Text;
            //string select = " select * from Cliente where Id = " + cbBusqueda.SelectedValue.ToString();
            //OleDbCommand cmd = new OleDbCommand(select, conn);
            //OleDbDataReader dr = cmd.ExecuteReader();

            //if (cbBusqueda.SelectedValue.ToString() == null)
            //{
            //    txtNombre.Text = "";
            //    txtRazonSocial.Text = "";
            //    txtRFC.Text = "";
            //    txtTelefono.Text = "";
            //    txtDireccion.Text = "";
            //}
            //else
            //{
            //    if (dr.Read() && cbBusqueda.SelectedValue.ToString() != null)
            //    {
            //        txtNombre.Text = dr["Nombre"].ToString();
            //        txtRazonSocial.Text = dr["RazonSocial"].ToString();
            //        txtRFC.Text = dr["RFC"].ToString();
            //        txtTelefono.Text = dr["Telefono"].ToString();
            //        txtDireccion.Text = dr["Direccion"].ToString();
            //    }

            //}
            //conn.Close();
        }

        private void btnRegresar_Click(object sender, EventArgs e)
        {
            Form1 menuPrincipal = new Form1();
            this.Hide();
            menuPrincipal.Show();
        }

        private void btnEditar_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtNombre.Text))
            {
                MessageBox.Show("El campo nombre es requerido.", "Faltan datos", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                OleDbConnection conn = new OleDbConnection(@"Provider=sqloledb;Data Source=JUANCPC;Initial Catalog=7ABD;Integrated Security=SSPI");
                OleDbCommand comando = new OleDbCommand();
                comando.CommandText = "update Cliente set Nombre = '" + txtNombre.Text + "',RazonSocial = '" + txtRazonSocial.Text + "', RFC = '" + txtRFC.Text + "', Telefono = '" + txtTelefono.Text + "', Direccion = '" + txtDireccion.Text + "'  where id = " + cbBusqueda.SelectedValue.ToString();

                try
                {
                    comando.Connection = conn;
                    conn.Open();
                    comando.ExecuteNonQuery();

                    //MessageBox.Show("Se ha actualizado corretamente el cliente");
                    txtNombre.Clear();
                    txtRazonSocial.Clear();
                    txtRFC.Clear();
                    txtTelefono.Clear();
                    txtDireccion.Clear();
                    //cbBusqueda.SelectedIndex = -1;
                    lblExitoEditar.Visible = true;
                    lblErrorEditar.Visible = false;
                    lblExitoEliminar.Visible = false;
                    lblErrorEliminar.Visible = false;

                }
                catch 
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
            comando.CommandText = "delete from Cliente where id = " + cbBusqueda.SelectedValue.ToString();

            try
            {
                comando.Connection = conn;
                conn.Open();
                comando.ExecuteNonQuery();
                //cbBusqueda.Refresh();

                //MessageBox.Show("Se ha eliminado corretamente el cliente");
                txtNombre.Clear();
                txtRazonSocial.Clear();
                txtRFC.Clear();
                txtTelefono.Clear();
                txtDireccion.Clear();
                lblExitoEditar.Visible = false;
                lblErrorEditar.Visible = false;
                lblExitoEliminar.Visible = true;
                lblErrorEliminar.Visible = false;
                //cbBusqueda.SelectedIndex = -1;
            }
            catch (Exception ex)
            {
                //MessageBox.Show("Ha ocurrido un error");
                lblExitoEditar.Visible = false;
                lblErrorEditar.Visible = false;
                lblExitoEliminar.Visible = false;
                lblErrorEliminar.Visible = true;
            }
            //cbBusqueda.Refresh();
            //cbBusqueda.SelectedIndex = -1;
            conn.Close();
        }

        private void lblId_Click(object sender, EventArgs e)
        {

        }

        private void txtNombre_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsLetter(e.KeyChar) && !char.IsWhiteSpace(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void txtTelefono_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }
    }
}
