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
    public partial class frmSubCategoryMaintenance : Form
    {
        frmCategoryMaintenance categories;
        public int categorySelectedId;
        private int subcategorySelectedId;

        public frmSubCategoryMaintenance()
        {
            InitializeComponent();
            categories = new frmCategoryMaintenance();
        }

        private void frmSubCategoryMaintenance_Load(object sender, EventArgs e)
        {
            loadCategories();
        }

        private void loadCategories()
        {
            dgvCategories.DataSource = categories.getCategories();
            dgvCategories.Columns[0].Visible = false;
            dgvCategories.ClearSelection();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void dgvCategories_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dgvCategories.RowCount > 0)
            {
                int rowSelected = dgvCategories.CurrentCell.RowIndex;
                categorySelectedId = int.Parse(dgvCategories.Rows[rowSelected].Cells[0].Value.ToString());
                loadSubCategories();
                btnAdd.Enabled = true;
                clearButtons();

            }
        }

        private void loadSubCategories()
        {
            dgvSubCategories.DataSource = getSubCategories();
            dgvSubCategories.Columns[0].Visible = false;
            dgvSubCategories.ClearSelection();
        }


        public DataTable getSubCategories()
        {
            using (MySqlConnection con = Configuration.getConnection())
            {
                try
                {
                    DataTable table = new DataTable();
                    con.Open();
                    string sql = "SELECT subcategories.subcategory_id , subcategories.subcategory_name AS SubCategory, subcategories.markup FROM subcategories,categories_subcategories WHERE categories_subcategories.category_id = @categoryId AND subcategories.subcategory_id = categories_subcategories.subcategory_id";
                    MySqlCommand command = new MySqlCommand(sql, con);
                    command.Parameters.AddWithValue("@categoryId",categorySelectedId);
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
            txtSubCategories.Text = string.Empty;
            txtMarkUp.Text = string.Empty;
            dgvCategories.ClearSelection();
            dgvSubCategories.DataSource = null;
            btnAdd.Enabled = false;
            categorySelectedId = -1;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            editMode(false);
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            Boolean success = false;
            if (string.IsNullOrEmpty(txtSubCategories.Text) || (string.IsNullOrEmpty(txtMarkUp.Text)))
            {
                MessageBox.Show("Field is empty.");
            }
            else
            {
                using (MySqlConnection con = Configuration.getConnection())
                {
                    try
                    {
                        con.Open();
                        string sql = "INSERT INTO subcategories(subcategory_name,markup) VALUES (@subcategoryName,@markup)";
                        MySqlCommand command = new MySqlCommand(sql, con);
                        command.Parameters.AddWithValue("@subcategoryName",txtSubCategories.Text);
                        command.Parameters.AddWithValue("@markup",txtMarkUp.Text);
                        command.ExecuteNonQuery();
                        success = true;
                    }
                    catch (MySqlException exception)
                    {
                        MessageBox.Show(exception.Message);
                        success = false;
                    }
                }

                if (success)
                {
                    int subCategoryId  = lastInsertedId();
                    using (MySqlConnection con = Configuration.getConnection())
                    {
                        try
                        {
                            con.Open();
                            string sql = "INSERT INTO categories_subcategories(ID,category_id,subcategory_id) VALUES(null,@categoryId,@subCategoryId)";
                            MySqlCommand command = new MySqlCommand(sql, con);
                            command.Parameters.AddWithValue("@categoryId", categorySelectedId);
                            command.Parameters.AddWithValue("@subCategoryId", subCategoryId);
                            command.ExecuteNonQuery();
                            MessageBox.Show("Succesfully Added.");
                            loadSubCategories();
                            txtSubCategories.Text = string.Empty;
                            txtMarkUp.Text = string.Empty;
                        }
                        catch (MySqlException exception)
                        {
                            MessageBox.Show(exception.Message);
                        }
                    }


                }
            }
        }

        private int lastInsertedId()
        {
            using (MySqlConnection con = Configuration.getConnection())
            {
                try
                {
                    con.Open();
                    string sql = "SELECT LAST_INSERT_ID()";
                    MySqlCommand command = new MySqlCommand(sql, con);
                    return Convert.ToInt32(command.ExecuteScalar());
                }
                catch (MySqlException exception)
                {
                    MessageBox.Show(exception.Message);
                    return 0;
                }
            }
        }

        private void dgvSubCategories_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dgvSubCategories.RowCount > 0)
            {
                int rowIndex = dgvSubCategories.CurrentCell.RowIndex;
                subcategorySelectedId = int.Parse(dgvSubCategories.Rows[rowIndex].Cells[0].Value.ToString());
                txtSubCategories.Text = dgvSubCategories.Rows[rowIndex].Cells[1].Value.ToString();
                txtMarkUp.Text = dgvSubCategories.Rows[rowIndex].Cells[2].Value.ToString();
                editMode(true);

                

            }
        }

        private void clearButtons()
        {

            btnUpdate.Enabled = false;
            btnDelete.Enabled = false;
            btnCancel.Enabled = false;
            txtSubCategories.Text = string.Empty;
            txtMarkUp.Text = string.Empty;
        }


        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtSubCategories.Text) || (string.IsNullOrEmpty(txtMarkUp.Text)))
            {
                MessageBox.Show("Field is empty.");
            }
            else
            {
                using (MySqlConnection con = Configuration.getConnection())
                {
                    try
                    {
                        con.Open();
                        string sql = "UPDATE subcategories SET subcategory_name = @subCategoryName, markup = @markup WHERE subcategory_id = @subCategoryId";
                        MySqlCommand command = new MySqlCommand(sql,con);
                        command.Parameters.AddWithValue("@subCategoryId",subcategorySelectedId);
                        command.Parameters.AddWithValue("@subCategoryName", txtSubCategories.Text);
                        command.Parameters.AddWithValue("@markup",txtMarkUp.Text);
                        command.ExecuteNonQuery();
                        MessageBox.Show("Successfully Updated.");
                        loadSubCategories();
                        clearButtons();
                    }
                    catch (MySqlException exception)
                    {
                        MessageBox.Show(exception.Message);
                    }
                }
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {

            DialogResult result = MessageBox.Show(null, "Are you sure you want to delete this item?", "Delete sub-category", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {


                Boolean success = false;
                using (MySqlConnection con = Configuration.getConnection())
                {
                    try
                    {

                        con.Open();
                        string sql = "DELETE FROM categories_subcategories WHERE category_id = @categoryId AND subcategory_id = @subCategoryId";
                        MySqlCommand command = new MySqlCommand(sql, con);
                        command.Parameters.AddWithValue("@categoryId", categorySelectedId);
                        command.Parameters.AddWithValue("@subCategoryId", subcategorySelectedId);
                        command.ExecuteNonQuery();
                        MessageBox.Show("Successfully Deleted.");
                        success = true;
                    }
                    catch (MySqlException exception)
                    {
                        MessageBox.Show(exception.Message);
                        success = false;
                    }
                }


                if (success)
                {
                    using (MySqlConnection con = Configuration.getConnection())
                    {
                        try
                        {

                            con.Open();
                            string sql = "DELETE FROM subcategories WHERE subcategory_id = @subCategoryId";
                            MySqlCommand command = new MySqlCommand(sql, con);
                            command.Parameters.AddWithValue("@subCategoryId", subcategorySelectedId);
                            command.ExecuteNonQuery();
                            loadSubCategories();
                            clearButtons();

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
}
