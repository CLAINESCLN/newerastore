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
    public partial class frmCategoryMaintenance : Form
    {
        private int selectedRow;
        private int selectedCategoryId;

        public frmCategoryMaintenance()
        {
            InitializeComponent();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void btnEdit_Click(object sender, EventArgs e)
        {

        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtProductCategory.Text))
            {
                MessageBox.Show("Field is empty.");
            }
            else
            {
                using (MySqlConnection con = Configuration.getConnection())
                {
                    con.Open();
                    string sql = "INSERT INTO categories(category_name) VALUE (@categoryName)";
                    MySqlCommand command = new MySqlCommand(sql, con);
                    command.Parameters.AddWithValue("@categoryName", txtProductCategory.Text);
                    command.ExecuteNonQuery();
                    MessageBox.Show("Successfully Added.");
                    loadCategories();
                    try
                    {

                    }
                    catch (MySqlException exception)
                    {
                        MessageBox.Show(exception.Message);
                    }
                }
            }
        }

        public DataTable getCategories()
        {
            using (MySqlConnection con = Configuration.getConnection())
            {
                try
                {
                    con.Open();
                    DataTable table = new DataTable();
                    string sql = "SELECT * FROM categories";
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

        private void loadCategories()
        {
            dgvCategories.DataSource = getCategories();
            dgvCategories.Columns[0].Visible = false;
            clear();
        }

        private void frmCategoryMaintenance_Load(object sender, EventArgs e)
        {
            loadCategories();
            dgvCategories.ClearSelection();
        }

        private void dgvCategories_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dgvCategories.RowCount > 0)
            {
                selectedRow = dgvCategories.CurrentCell.RowIndex;
                selectedCategoryId = int.Parse(dgvCategories.Rows[selectedRow].Cells[0].Value.ToString());
                txtProductCategory.Text = dgvCategories.Rows[selectedRow].Cells[1].Value.ToString();
                editMode(true);
            }
        }

        private void editMode(Boolean mode)
        {
            btnAdd.Enabled = !mode;
            btnUpdate.Enabled = mode;
            btnDelete.Enabled = mode;
            btnCancel.Enabled = mode;
            if (!mode)
                clear();
        }

        private void clear()
        {
            txtProductCategory.Text = string.Empty;
            dgvCategories.ClearSelection();
            selectedCategoryId = -1;
            selectedRow = -1;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            editMode(false);
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if (selectedRow < 0)
                return;
            using (MySqlConnection con = Configuration.getConnection())
            {
                try
                {
                    con.Open();
                    string sql = "UPDATE categories SET category_name = @categoryName WHERE category_id = @categoryId";
                    MySqlCommand command = new MySqlCommand(sql, con);
                    command.Parameters.AddWithValue("@categoryName",txtProductCategory.Text);
                    command.Parameters.AddWithValue("@categoryId",selectedCategoryId);
                    command.ExecuteNonQuery();
                    MessageBox.Show("Successfully Updated.");
                    loadCategories();

                }
                catch (MySqlException exception)
                {
                    MessageBox.Show(exception.Message);
                }
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (selectedRow < 0)
                return;
            DialogResult result = MessageBox.Show(null, "Are you sure you want to delete this item?", "Delete category", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                using (MySqlConnection con = Configuration.getConnection())
                {
                    try
                    {
                        con.Open();
                        string sql = "DELETE FROM categories WHERE category_id = @categoryId";
                        MySqlCommand command = new MySqlCommand(sql, con);
                        command.Parameters.AddWithValue("@categoryId", selectedCategoryId);
                        command.ExecuteNonQuery();
                        MessageBox.Show("Successfully Deleted.");
                        loadCategories();

                    }
                    catch (MySqlException exception)
                    {
                        MessageBox.Show("This category can't be deleted because it contains a subcategory");
                    }
                }

            }
        }

    }
}
