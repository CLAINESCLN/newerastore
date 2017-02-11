using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
using SalesAndInventory.Classes;


namespace SalesAndInventory
{
    public partial class frmSupplierMaintenance : Form
    {

        private string selectedCode;

        public frmSupplierMaintenance()
        {
            InitializeComponent();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            editMode(false);
            dgvSuppliers.ClearSelection();
            clear();
        }

        private void clear()
        {
            txtSupplierCode.Text = string.Empty;
            txtSupplierName.Text = string.Empty;
            txtSupplierAddress.Text = string.Empty;
            txtContactNumber.Text = string.Empty;
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtSupplierCode.Text) || (string.IsNullOrEmpty(txtSupplierName.Text)) || string.IsNullOrEmpty(txtSupplierAddress.Text) || (string.IsNullOrEmpty(txtContactNumber.Text)))
                MessageBox.Show("Field is empty.");
            else
            {

                using (MySqlConnection con = Configuration.getConnection())
                {
                    try
                    {
                        con.Open(); 
                        string sql = "INSERT INTO suppliers VALUES (@supplierId,@supplierName,@supplierAddress,@contactPerson)";
                        MySqlCommand command = new MySqlCommand(sql, con);
                        command.Parameters.AddWithValue("@supplierId", txtSupplierCode.Text);
                        command.Parameters.AddWithValue("@supplierName", txtSupplierName.Text);
                        command.Parameters.AddWithValue("@supplierAddress", txtSupplierAddress.Text);
                        command.Parameters.AddWithValue("@contactPerson", txtContactNumber.Text);
                        command.ExecuteNonQuery();
                        MessageBox.Show("Successfully Added.");
                        loadSuppliers();
                        clear();
                    }
                    catch (MySqlException exception)
                    {
                        MessageBox.Show(exception.Message);
                    }
                }
            }
        }

        private void editMode(Boolean mode)
        {
            btnAdd.Enabled = !mode;
            btnUpdate.Enabled = mode;
            btnDelete.Enabled = mode;
            btnCancel.Enabled = mode;
        }

        private void loadSuppliers()
        {
            dgvSuppliers.DataSource = getSuppliers();
            dgvSuppliers.ClearSelection();
        }

        public DataTable getSuppliers()
        {

            using (MySqlConnection con = Configuration.getConnection())
            {
                try
                {
                    DataTable table = new DataTable();
                    string sql = "SELECT supplier_id AS Code,supplier_name AS Name, supplier_address AS Address, contact_person AS ContactNumber FROM suppliers";
                    MySqlCommand command = new MySqlCommand(sql, con);
                    MySqlDataAdapter adapter = new MySqlDataAdapter(command);
                    adapter.Fill(table);
                    return table;
                }
                catch (MySqlException exception)
                {
                    MessageBox.Show(exception.Message);
                    return null;
                }
            }
        }


        private void frmSupplierMaintenance_Load(object sender, EventArgs e)
        {
            loadSuppliers();
        }

        private void dgvSuppliers_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dgvSuppliers.RowCount > 0)
            {
                int rowIndex = dgvSuppliers.CurrentCell.RowIndex;
                selectedCode = dgvSuppliers.Rows[rowIndex].Cells[0].Value.ToString();
                txtSupplierCode.Text = selectedCode;
                txtSupplierName.Text = dgvSuppliers.Rows[rowIndex].Cells[1].Value.ToString();
                txtSupplierAddress.Text = dgvSuppliers.Rows[rowIndex].Cells[2].Value.ToString();
                txtContactNumber.Text = dgvSuppliers.Rows[rowIndex].Cells[3].Value.ToString();
                editMode(true);
            }
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            using (MySqlConnection con = Configuration.getConnection())
            {
                try
                {
                    con.Open();
                    string sql = "UPDATE suppliers SET supplier_id = @supplierCode, supplier_name = @supplierName, supplier_address = @supplierAddress, contact_person = @contactNumber WHERE supplier_id = @supplierId";
                    MySqlCommand command = new MySqlCommand(sql, con);
                    command.Parameters.AddWithValue("@supplierId", selectedCode);
                    command.Parameters.AddWithValue("@supplierCode", txtSupplierCode.Text);
                    command.Parameters.AddWithValue("@supplierName", txtSupplierName.Text);
                    command.Parameters.AddWithValue("@supplierAddress", txtSupplierAddress.Text);
                    command.Parameters.AddWithValue("@contactNumber", txtContactNumber.Text);

                    command.ExecuteNonQuery();
                    MessageBox.Show("Successfully Updated.");
                    loadSuppliers();
                    clear();

                }
                catch (MySqlException exception)
                {
                    MessageBox.Show(exception.Message);
                }
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
             DialogResult result = MessageBox.Show(null, "Are you sure you want to delete this item?", "Delete supplier", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

             if (result == DialogResult.Yes)
             {

                 using (MySqlConnection con = Configuration.getConnection())
                 {
                     try
                     {
                         con.Open();
                         string sql = "DELETE FROM suppliers WHERE supplier_id = @supplierId";
                         MySqlCommand command = new MySqlCommand(sql, con);
                         command.Parameters.AddWithValue("@supplierId", selectedCode);

                         command.ExecuteNonQuery();
                         MessageBox.Show("Successfully Deleted.");
                         loadSuppliers();
                         clear();

                     }
                     catch (MySqlException exception)
                     {
                         MessageBox.Show(exception.Message);
                     }
                 }
             }
        }

    }
}
