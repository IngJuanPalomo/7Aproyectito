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
    public partial class Venta : Form
    {
        int numventa = 0;
        int numcotizacion = 0;
        DateTime dateAndTime = DateTime.Now;
        String fecha = DateTime.Now.ToString("dd-MM-yyyy");
        String hora = DateTime.Now.ToString("HH:mm:ss");
        public Venta()
        {
            InitializeComponent();
        }

        private void Venta_Load(object sender, EventArgs e)
        {
            OleDbConnection connVenta = new OleDbConnection(@"Provider=sqloledb;Data Source=JUANCPC;Initial Catalog=7ABD;Integrated Security=SSPI");
            string selectVenta = "select  isnull(max(NumVenta),0)+1 maximo from venta";
            connVenta.Open();
            try
            {

                OleDbCommand command = new OleDbCommand(selectVenta, connVenta);
                OleDbDataReader dr = command.ExecuteReader();

                while (dr.Read())
                {
                    numventa = dr.GetInt32(0);
                    break;
                }
                if (numventa == 0)
                {
                    return;
                }
                lblNumVenta.Text = numventa.ToString();
            }
            catch (Exception ex)
            {
                //MessageBox.Show(ex.Message);
            }
            finally
            {
                connVenta.Close();
            }

            OleDbConnection conn = new OleDbConnection(@"Provider=sqloledb;Data Source=JUANCPC;Initial Catalog=7ABD;Integrated Security=SSPI");
            string select = " select * from Cliente";
            conn.Open();
            try
            {
                OleDbDataAdapter da = new OleDbDataAdapter(select, conn);
                DataSet ds = new DataSet();

                da.Fill(ds, "Cliente");

                cbBusqueda.ValueMember = "Id";
                cbBusqueda.DisplayMember = "RazonSocial";
                cbBusqueda.DataSource = ds.Tables["Cliente"];

                cbBusqueda.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
                cbBusqueda.AutoCompleteSource = AutoCompleteSource.ListItems;
                cbBusqueda.SelectedIndex = -1;
                //cbBusqueda.SelectedValue = 0;
            }
            catch (Exception ex)
            {
                //MessageBox.Show(ex.Message);
            }
            finally
            {
                conn.Close();
            }

            OleDbConnection connProducto = new OleDbConnection(@"Provider=sqloledb;Data Source=JUANCPC;Initial Catalog=7ABD;Integrated Security=SSPI");
            string selectProducto = " select * from Producto";
            connProducto.Open();
            try
            {
                OleDbDataAdapter daProducto = new OleDbDataAdapter(selectProducto, conn);
                DataSet dsProducto = new DataSet();

                daProducto.Fill(dsProducto, "Producto");

                cbProducto.ValueMember = "Id";
                cbProducto.DisplayMember = "Nombre";
                cbProducto.DataSource = dsProducto.Tables["Producto"];

                cbProducto.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
                cbProducto.AutoCompleteSource = AutoCompleteSource.ListItems;
                cbProducto.SelectedIndex = -1;
            }
            catch (Exception ex)
            {
                //MessageBox.Show(ex.Message);
            }
            finally
            {
                connProducto.Close();
            }

            this.cbBusqueda.SelectedIndexChanged += new System.EventHandler(this.cbBusqueda_SelectedIndexChanged);
            txtNombreProducto.Text = string.Empty;
            txtPrecio.Text = string.Empty;
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
                    lblNombre.Text = dr["Nombre"].ToString();
                    lblRazonSocial.Text = dr["RazonSocial"].ToString();
                    lblRFC.Text = dr["RFC"].ToString();
                    lblTelefono.Text = dr["Telefono"].ToString();
                    lblDireccion.Text = dr["Direccion"].ToString();
                }
            }
            conn.Close();
            cbBusqueda.Enabled = false;

            OleDbConnection connCotizacion = new OleDbConnection(@"Provider=sqloledb;Data Source=JUANCPC;Initial Catalog=7ABD;Integrated Security=SSPI");
            string selectCotizacion = "select  isnull(max(NumCotizacion),0) maximo from cotizacion where cliente = " + cbBusqueda.SelectedValue.ToString();
            connCotizacion.Open();
            try
            {

                OleDbCommand command = new OleDbCommand(selectCotizacion, connCotizacion);
                OleDbDataReader dr = command.ExecuteReader();

                while (dr.Read())
                {
                    numcotizacion = dr.GetInt32(0);
                    break;
                }
                if (numcotizacion == 0)
                {
                    if (MessageBox.Show("El cliente seleccionado no cuenta con cotizaciones. \n ¿Desea realizar una cotizacion antes?", "Confirmar", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        Cotizacion cotizacion = new Cotizacion();
                        this.Hide();
                        cotizacion.Show();
                    }
                }
                lblNumCotizacion.Text = numcotizacion.ToString();
            }
            catch (Exception ex)
            {
                //MessageBox.Show(ex.Message);
            }
            finally
            {
                connCotizacion.Close();
            }          
        }

        private void cbProducto_SelectedIndexChanged(object sender, EventArgs e)
        {
            OleDbConnection conn = new OleDbConnection(@"Provider=sqloledb;Data Source=JUANCPC;Initial Catalog=7ABD;Integrated Security=SSPI");

            //string  select = " select * from Cliente where Id = " + txtId.Text;
            string select = " select * from Producto where Id = " + cbProducto.SelectedValue.ToString();
            OleDbDataAdapter da = new OleDbDataAdapter(select, conn);
            DataTable dt = new DataTable();

            da.Fill(dt);
            if (dt.Rows.Count != 0)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    txtNombreProducto.Text = dr["Nombre"].ToString();
                    txtPrecio.Text = dr["Precio"].ToString();
                }
            }
            conn.Close();
        }

        private void LaCuenta()
        {

            double cuenta = 0;

            OleDbConnection connCuenta = new OleDbConnection(@"Provider=sqloledb;Data Source=JUANCPC;Initial Catalog=7ABD;Integrated Security=SSPI");
            string selectCuenta = "select  isnull(sum(Importe),0) x from cotizacion where NumCotizacion = " + numcotizacion;
            connCuenta.Open();
            try
            {

                OleDbCommand command = new OleDbCommand(selectCuenta, connCuenta);
                OleDbDataReader dr = command.ExecuteReader();

                while (dr.Read())
                {
                    cuenta = dr.GetInt32(0);
                    break;
                }
                if (cuenta == 0)
                {
                    lblSubtotal.Text = 0.ToString();
                    lblIVA.Text = 0.ToString();
                    lblTotal.Text = 0.ToString();
                }

                if (chbIVA.Checked)
                {
                    double subtotal = cuenta * .84;
                    double iva = cuenta * .16;
                    lblSubtotal.Text = subtotal.ToString();
                    lblIVA.Text = iva.ToString();
                    lblTotal.Text = cuenta.ToString();
                }
                else
                {
                    lblSubtotal.Text = cuenta.ToString();
                    lblIVA.Text = 0.ToString();
                    lblTotal.Text = cuenta.ToString();
                }

            }
            catch (Exception ex)
            {
                //MessageBox.Show(ex.Message);
            }
            finally
            {
                connCuenta.Close();
            }
        }

        private void btnAnadir_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtCantidad.Text) || string.IsNullOrEmpty(txtPrecio.Text) || string.IsNullOrEmpty(cbBusqueda.Text))
            {
                MessageBox.Show("Se debe seleccionar cliente y producto, y agregar una cantidad.", "Faltan datos", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {

                int cantidad = Convert.ToInt32(txtCantidad.Text);
                int precio = Convert.ToInt32(txtPrecio.Text);
                int importe = cantidad * precio;
                int stock = 0;

                OleDbConnection connStock = new OleDbConnection(@"Provider=sqloledb;Data Source=JUANCPC;Initial Catalog=7ABD;Integrated Security=SSPI");
                string selectStock = "select cantidad from Producto where Id = " + cbProducto.SelectedValue.ToString();
                connStock.Open();
                try
                {

                    OleDbCommand command = new OleDbCommand(selectStock, connStock);
                    OleDbDataReader dr = command.ExecuteReader();

                    while (dr.Read())
                    {
                        stock = dr.GetInt32(0);
                        break;
                    }
                    if (stock == 0)
                    {
                        return;
                    }
                }
                catch (Exception ex)
                {
                    //MessageBox.Show(ex.Message);
                }
                finally
                {
                    connStock.Close();
                }

                if (stock < cantidad)
                {
                    MessageBox.Show("Solo hay " + stock + " unidades de " + txtNombreProducto.Text);
                }
                else
                {
                    try
                    {

                        OleDbConnection conn = new OleDbConnection(@"Provider=sqloledb;Data Source=JUANCPC;Initial Catalog=7ABD;Integrated Security=SSPI");
                        OleDbCommand comando = new OleDbCommand();
                        comando.CommandText = "insert into cotizacion (numcotizacion, fecha, hora, cliente, nombrecliente, razonsocial, rfc, codigo, nombreproducto, cantidad, precio, importe) values ('" + lblNumCotizacion.Text + "','" + fecha + "','" + hora + "','" + cbBusqueda.SelectedValue.ToString() + "','" + lblNombre.Text + "','" + lblRazonSocial.Text + "','" + lblRFC.Text + "','" + lblNumCotizacion.Text + cbProducto.SelectedValue.ToString() + "','" + txtNombreProducto.Text + "','" + txtCantidad.Text + "','" + txtPrecio.Text + "','" + importe + "')";
                        comando.Connection = conn;
                        conn.Open();
                        comando.ExecuteNonQuery();
                        conn.Close();

                    }
                    catch (Exception ex)
                    {
                        //MessageBox.Show(ex.Message);
                    }

                    try
                    {
                        OleDbConnection conndt = new OleDbConnection(@"Provider=sqloledb;Data Source=JUANCPC;Initial Catalog=7ABD;Integrated Security=SSPI");
                        conndt.Open();
                        DataSet ds = new DataSet();
                        DataTable dt = new DataTable();
                        ds.Tables.Add(dt);
                        OleDbDataAdapter da = new OleDbDataAdapter();
                        da = new OleDbDataAdapter("select Codigo, NombreProducto, Cantidad, Precio, Importe from cotizacion where NumCotizacion = " + numcotizacion, conndt);
                        da.Fill(dt);
                        dt1.DataSource = dt.DefaultView;
                        conndt.Close();

                        //Headers Columns
                        dt1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.DisplayedCells;
                        dt1.Columns[0].HeaderText = "Codigo";
                        dt1.Columns[1].HeaderText = "Nombre";
                        dt1.Columns[2].HeaderText = "Cantidad";
                        dt1.Columns[3].HeaderText = "Precio";
                        dt1.Columns[4].HeaderText = "Importe";

                        txtCantidad.Text = "";
                        txtNombreProducto.Text = "";
                        txtPrecio.Text = "";
                        cbProducto.SelectedIndex = -1;
                    }
                    catch (Exception ex)
                    {
                        //MessageBox.Show(ex.Message);
                    }
                    LaCuenta();

                }
            }
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            DataGridViewRow row = dt1.CurrentRow;
            if (row == null)
            {
                MessageBox.Show("Debe selccionar algún producto de la tabla para eliminarlo de la cotización.", "Eliminar", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            else
            {
                if (MessageBox.Show("Se eliminaran los productos: " + Convert.ToString(row.Cells[0].Value) + " " + Convert.ToString(row.Cells[1].Value) + "\n ¿Esta de acuerdo?", "Confirmar", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    OleDbConnection connEliminar = new OleDbConnection(@"Provider=sqloledb;Data Source=JUANCPC;Initial Catalog=7ABD;Integrated Security=SSPI");
                    string deleteEliminar = "Delete FROM Cotizacion WHERE Codigo = " + dt1.CurrentRow.Cells[0].Value.ToString();
                    connEliminar.Open();
                    try
                    {
                        OleDbCommand command = new OleDbCommand(deleteEliminar, connEliminar);
                        command.ExecuteNonQuery();
                    }
                    catch (Exception ex)
                    {

                    }
                    finally
                    {
                        connEliminar.Close();
                    }
                    try
                    {
                        OleDbConnection conndt = new OleDbConnection(@"Provider=sqloledb;Data Source=JUANCPC;Initial Catalog=7ABD;Integrated Security=SSPI");
                        conndt.Open();
                        DataSet ds = new DataSet();
                        DataTable dt = new DataTable();
                        ds.Tables.Add(dt);
                        OleDbDataAdapter da = new OleDbDataAdapter();
                        da = new OleDbDataAdapter("select Codigo, NombreProducto, Cantidad, Precio, Importe from cotizacion where NumCotizacion = " + numcotizacion, conndt);
                        da.Fill(dt);
                        dt1.DataSource = dt.DefaultView;
                        conndt.Close();
                    }
                    catch (Exception ex)
                    {

                    }
                    LaCuenta();
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

        private void btnVer_Click(object sender, EventArgs e)
        {
            try
            {
                OleDbConnection conndt = new OleDbConnection(@"Provider=sqloledb;Data Source=JUANCPC;Initial Catalog=7ABD;Integrated Security=SSPI");
                conndt.Open();
                DataSet ds = new DataSet();
                DataTable dt = new DataTable();
                ds.Tables.Add(dt);
                OleDbDataAdapter da = new OleDbDataAdapter();
                da = new OleDbDataAdapter("select Codigo, NombreProducto, Cantidad, Precio, Importe from cotizacion where NumCotizacion = " + numcotizacion, conndt);
                da.Fill(dt);
                dt1.DataSource = dt.DefaultView;
                conndt.Close();

                //Headers Columns
                dt1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.DisplayedCells;
                dt1.Columns[0].HeaderText = "Codigo";
                dt1.Columns[1].HeaderText = "Nombre";
                dt1.Columns[2].HeaderText = "Cantidad";
                dt1.Columns[3].HeaderText = "Precio";
                dt1.Columns[4].HeaderText = "Importe";

                txtCantidad.Text = "";
                txtNombreProducto.Text = "";
                txtPrecio.Text = "";
                cbProducto.SelectedIndex = -1;
            }
            catch (Exception ex)
            {
                DataGridViewRow row = dt1.CurrentRow;
                if (row == null)
                {
                    //MessageBox.Show(ex.Message);
                    if (MessageBox.Show("El cliente seleccionado no cuenta con cotizaciones. \n ¿Desea realizar una cotizacion antes?", "Confirmar", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        Cotizacion cotizacion = new Cotizacion();
                        this.Hide();
                        cotizacion.Show();
                    }
                }
            }
            LaCuenta();
        }

        private void btnVenta_Click(object sender, EventArgs e)
        {
            DataGridViewRow row = dt1.CurrentRow;
            if (row == null)
            {
                MessageBox.Show("No hay articulos en la venta.", "Error.", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                if (!chbEfectivo.Checked && !chbTransferencia.Checked && !chbTarjeta.Checked)
                {
                    MessageBox.Show("Escoge al menos una forma de pago.", "Error.", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
                else
                {
                    string pago = "";
                    if (chbEfectivo.Checked && chbTransferencia.Checked && chbTarjeta.Checked)
                    {
                        pago = "Efectivo, Transferecnia, y Tarjeta";
                    }
                    else
                    {
                        if (chbEfectivo.Checked && chbTransferencia.Checked)
                        {
                            pago = "Efectivo, y Transferencia";
                        }
                        else
                        {
                            if (chbEfectivo.Checked && chbTarjeta.Checked)
                            {
                                pago = "Efectivo, y Tarjeta";
                            }
                            else
                            {
                                if (chbTransferencia.Checked && chbTarjeta.Checked)
                                {
                                    pago = "Tansferencia, y Tarjeta";
                                }
                                else
                                {
                                    if (chbEfectivo.Checked)
                                    {
                                        pago = "Efectivo";
                                    }
                                    else
                                    {
                                        if (chbTransferencia.Checked)
                                        {
                                            pago = "Transferencia";
                                        }
                                        else
                                        {
                                            if (chbTarjeta.Checked)
                                            {
                                                pago = "Tarjeta";
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                    try
                    {

                        OleDbConnection conn = new OleDbConnection(@"Provider=sqloledb;Data Source=JUANCPC;Initial Catalog=7ABD;Integrated Security=SSPI");
                        OleDbCommand comando = new OleDbCommand();
                        comando.CommandText = "insert into venta (numventa, fecha, hora, cliente, nombrecliente, razonsocial, rfc, subtotal, iva, total, pago, numcotizacion) values ('" + lblNumVenta.Text + "','" + fecha + "','" + hora + "','" + cbBusqueda.SelectedValue.ToString() + "','" + lblNombre.Text + "','" + lblRazonSocial.Text + "','" + lblRFC.Text + "','" + lblSubtotal.Text + "','" + lblIVA.Text + "','" + lblTotal.Text + "','" + pago + "','" + lblNumCotizacion.Text + "')";
                        comando.Connection = conn;
                        conn.Open();
                        comando.ExecuteNonQuery();
                        conn.Close();

                        MessageBox.Show("Gracias por su compra.", "Venta.", MessageBoxButtons.OK, MessageBoxIcon.None);

                        Form1 menuPrincipal = new Form1();
                        this.Hide();
                        menuPrincipal.Show();

                    }
                    catch (Exception ex)
                    {
                        //MessageBox.Show(ex.Message);
                    }
                }

            }
        }

        private void chbIVA_CheckedChanged(object sender, EventArgs e)
        {
            LaCuenta();
        }
    }
}
