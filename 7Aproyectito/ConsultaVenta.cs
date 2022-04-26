using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Data.OleDb;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace _7Aproyectito
{
    public partial class ConsultaVenta : Form
    {
        public ConsultaVenta()
        {
            InitializeComponent();
        }

        private void ConsultaVenta_Load(object sender, EventArgs e)
        {
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

            OleDbConnection connPago = new OleDbConnection(@"Provider=sqloledb;Data Source=JUANCPC;Initial Catalog=7ABD;Integrated Security=SSPI");
            string selectPago = " select * from Venta";
            connPago.Open();
            try
            {
                OleDbDataAdapter daPago = new OleDbDataAdapter(selectPago, connPago);
                DataSet dsPago = new DataSet();

                daPago.Fill(dsPago, "Venta");

                cbPago.ValueMember = "Pago";
                cbPago.DisplayMember = "Pago";
                cbPago.DataSource = dsPago.Tables["Venta"];

                cbPago.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
                cbPago.AutoCompleteSource = AutoCompleteSource.ListItems;
                cbPago.SelectedIndex = -1;
                //cbBusqueda.SelectedValue = 0;
            }
            catch (Exception ex)
            {
                //MessageBox.Show(ex.Message);
            }
            finally
            {
                connPago.Close();
            }
        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {
            if (dateTimePicker1.Value > DateTime.Now)
            {
                DialogResult result = MessageBox.Show("Las fechas seleccionadas no pueden ser posteriores a la fecha actual.", "Inconsistencia de fechas", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                if (result == DialogResult.OK)
                {
                    dateTimePicker1.Value = DateTime.Now;
                }
            }
        }

        private void dateTimePicker2_ValueChanged(object sender, EventArgs e)
        {
            if (dateTimePicker2.Value > DateTime.Now)
            {
                DialogResult result = MessageBox.Show("Las fechas seleccionadas no pueden ser posteriores a la fecha actual.", "Inconsistencia de fechas", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                if (result == DialogResult.OK)
                {
                    dateTimePicker2.Value = DateTime.Now;
                }
            }
        }

        private void btnRegresar_Click(object sender, EventArgs e)
        {
            Form1 menuPrincipal = new Form1();
            this.Hide();
            menuPrincipal.Show();
        }

        private void btnConsultar_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtFolio.Text))
            {
                try
                {
                    OleDbConnection conndt = new OleDbConnection(@"Provider=sqloledb;Data Source=JUANCPC;Initial Catalog=7ABD;Integrated Security=SSPI");
                    conndt.Open();
                    DataSet ds = new DataSet();
                    DataTable dt = new DataTable();
                    ds.Tables.Add(dt);
                    OleDbDataAdapter da = new OleDbDataAdapter();
                    da = new OleDbDataAdapter("select NombreCliente, Total, NumVenta, Fecha, Pago from venta where Cliente = " + cbBusqueda.SelectedValue.ToString() + " and Pago = " + cbPago.SelectedValue.ToString() + " and between " + this.dateTimePicker1.Text + " and " + this.dateTimePicker2.Text, conndt);
                    da.Fill(dt);
                    dt1.DataSource = dt.DefaultView;
                    conndt.Close();

                    //Headers Columns
                    dt1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.DisplayedCells;
                    dt1.Columns[0].HeaderText = "Cliente";
                    dt1.Columns[1].HeaderText = "Total";
                    dt1.Columns[2].HeaderText = "Folio";
                    dt1.Columns[3].HeaderText = "Fecha";
                    dt1.Columns[4].HeaderText = "Forma de pago";
                }
                catch
                {
                    MessageBox.Show("No hay registros que cumplan con los parametros de busqueda.", "No hay registros.", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            else
            {
                try
                {
                    OleDbConnection conndt = new OleDbConnection(@"Provider=sqloledb;Data Source=JUANCPC;Initial Catalog=7ABD;Integrated Security=SSPI");
                    conndt.Open();
                    DataSet ds = new DataSet();
                    DataTable dt = new DataTable();
                    ds.Tables.Add(dt);
                    OleDbDataAdapter da = new OleDbDataAdapter();
                    da = new OleDbDataAdapter("select NombreCliente, Total, NumVenta, Fecha, Pago from venta where NumVenta = " + txtFolio.Text, conndt);
                    da.Fill(dt);
                    dt1.DataSource = dt.DefaultView;
                    conndt.Close();

                    //Headers Columns
                    dt1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.DisplayedCells;
                    dt1.Columns[0].HeaderText = "Cliente";
                    dt1.Columns[1].HeaderText = "Total";
                    dt1.Columns[2].HeaderText = "Folio";
                    dt1.Columns[3].HeaderText = "Fecha";
                    dt1.Columns[4].HeaderText = "Forma de pago";
                }
                catch
                {
                    MessageBox.Show("No hay registros que cumplan con los parametros de busqueda.", "No hay registros.", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }

        }

        private void button5_Click(object sender, EventArgs e)
        {
            DataGridViewRow row = dt1.CurrentRow;
            if (row == null)
            {
                MessageBox.Show("No hay datos para exportar.", "Reporte", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                Exportar exp = new Exportar();
                exp.ExportarDataGridViewExcel(dt1);
            }
        }
    }
}
