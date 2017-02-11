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
    public partial class frmProduct : Form
    
    {
        private int categoryId;
        private int subCategoryId;

        frmCategoryMaintenance categories;
        frmSubCategoryMaintenance subCategories;
        frmSupplierMaintenance suppliers;

        public frmProduct()
        {
            InitializeComponent();
            categories = new frmCategoryMaintenance();
            subCategories = new frmSubCategoryMaintenance();
            suppliers = new frmSupplierMaintenance();
        }

        private void loadTables()
        {
            dgvCategories.DataSource = categories.getCategories();
            dgvCategories.Columns[0].Visible = false;

            dgvSuppliers.DataSource = suppliers.getSuppliers();
            dgvSuppliers.Columns[2].Visible = false;
            dgvSuppliers.Columns[3].Visible = false;
            dgvSuppliers.ClearSelection();
        }





        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void frmProduct_Load(object sender, EventArgs e)
        {
            loadTables();
            loadProducts();
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void btnNew_Click(object sender, EventArgs e)
        {

        }

        private void btnDelete_Click(object sender, EventArgs e)
        {

        }

        private void btnEdit_Click(object sender, EventArgs e)
        {

        }

        private void btnSave_Click(object sender, EventArgs e)
        {

        }

        private void btnClose_Click(object sender, EventArgs e)
        {

        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void loadProducts()
        {
            dgvProducts.DataSource = getProducts();
            dgvProducts.Columns[0].Visible = false;
        }




        public DataTable getProducts()
        {
            using (MySqlConnection connection = Configuration.getConnection())
            {
                try
                {
                    DataTable table = new DataTable();
                    connection.Open();
                    string sql = "SELECT product_id,product_code AS Code,product_description AS Description,cost AS Cost,selling_price AS Price,quantity,supplier_name AS Supplier FROM products,suppliers WHERE products.supplier_id = suppliers.supplier_id";
                    MySqlCommand command = new MySqlCommand(sql,connection);
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
        




        private void dgvCategories_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dgvCategories.RowCount > 0)
            {
                int rowIndex = dgvCategories.CurrentCell.RowIndex;
                categoryId = int.Parse(dgvCategories.Rows[rowIndex].Cells[0].Value.ToString());
                subCategories.categorySelectedId = categoryId;
                dgvSubCategories.DataSource = subCategories.getSubCategories();
                dgvSubCategories.Columns[0].Visible = false;
                dgvSubCategories.ClearSelection();
            }
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            Boolean success = false;
            try
            {
                if (dgvCategories.RowCount > 0)
                    if (dgvSubCategories.RowCount > 0)
                        if (dgvSuppliers.RowCount > 0)
                        {
                            int indexCat = dgvCategories.CurrentCell.RowIndex;
                            int indexSub = dgvSubCategories.CurrentCell.RowIndex;
                            int indexSup = dgvSuppliers.CurrentCell.RowIndex;


                            categoryId = int.Parse(dgvCategories.Rows[indexCat].Cells[0].Value.ToString());
                            subCategoryId = int.Parse(dgvSubCategories.Rows[indexSub].Cells[0].Value.ToString());
                            int supplierId = int.Parse(dgvSuppliers.Rows[indexSup].Cells[0].Value.ToString());

                            using (MySqlConnection connection = Configuration.getConnection())
                            {
                                try
                                {
                                    connection.Open();
                                    string sql = "INSERT INTO products VALUES (null,@productCode,@productDescription,@cost,@sellingPrice,@quantity,@supplierId)";
                                    MySqlCommand command = new MySqlCommand(sql, connection);
                                    command.Parameters.AddWithValue("@productCode",txtProdCod.Text);
                                    command.Parameters.AddWithValue("@productDescription",txtDescription.Text);
                                    command.Parameters.AddWithValue("@cost", txtUnitCost.Text);
                                    command.Parameters.AddWithValue("@sellingPrice", txtSellingPrice.Text);
                                    command.Parameters.AddWithValue("@quantity", txtQty.Text);
                                    command.Parameters.AddWithValue("@supplierId", supplierId);
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
                                int catSub = getCatSubId();
                                int id = lastInsertedId();
                                using (MySqlConnection connection = Configuration.getConnection())
                                {
                                    try
                                    {
                                        connection.Open();
                                        string sql = "INSERT INTO product_catsub (product_id,ID) VALUES (@productId,@catSubId)";
                                        MySqlCommand command = new MySqlCommand(sql, connection);
                                        command.Parameters.AddWithValue("@productId",id);
                                        command.Parameters.AddWithValue("@catSubId",catSub);
                                        command.ExecuteNonQuery();
                                        MessageBox.Show("Successfully Added.");
                                        loadProducts();
                                    }
                                    catch (MySqlException exception)
                                    {
                                        MessageBox.Show(exception.Message);
                                    }
                                }

                            }
                        }

            }
            catch (Exception) {
                MessageBox.Show("Select a category, sub-category, and suppliers");
            }
            
        }


        private int lastInsertedId()
        {
            using (MySqlConnection connection = Configuration.getConnection())
            {
                try
                {
                    connection.Open();
                    string sql = "SELECT LAST_INSERT_ID()";
                    MySqlCommand command = new MySqlCommand(sql, connection);
                    return Convert.ToInt32(command.ExecuteScalar());
                }
                catch (MySqlException exception)
                {
                    MessageBox.Show(exception.Message);
                    return 0;
                }
            }
        }


        

        private int getCatSubId()
        {
            using (MySqlConnection connection = Configuration.getConnection())
            {
                try
                {
                    connection.Open();
                    string sql = "SELECT ID FROM categories_subcategories WHERE category_id = @categoryId AND subcategory_id = @subCategoryId";
                    MySqlCommand command = new MySqlCommand(sql, connection);
                    command.Parameters.AddWithValue("@categoryId",categoryId);
                    command.Parameters.AddWithValue("@subCategoryId",subCategoryId);
                    return Convert.ToInt32(command.ExecuteScalar());
                }
                catch (MySqlException exception)
                {
                    MessageBox.Show(exception.Message);
                    return 0;
                }
            }
        }

        private void txtMarkup_Leave(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtUnitCost.Text))
            {
                txtSellingPrice.Text = Formula.getSellingPrice(double.Parse(txtUnitCost.Text), double.Parse(txtMarkup.Text)).ToString();
            }
        }

        private void txtSellingPrice_Leave(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtUnitCost.Text))
            {
                txtMarkup.Text = Formula.getMarkup(double.Parse(txtUnitCost.Text), double.Parse(txtSellingPrice.Text)).ToString();
            }
        }

        private void dgvSubCategories_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dgvSubCategories.RowCount > 0)
            {
                int index = dgvSubCategories.CurrentCell.RowIndex;
                int defaultMarkUp = int.Parse(dgvSubCategories.Rows[index].Cells[2].Value.ToString());
                txtMarkup.Text = defaultMarkUp.ToString();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtUnitCost.Text))
            {
                txtSellingPrice.Text = Formula.getSellingPrice(double.Parse(txtUnitCost.Text), double.Parse(txtMarkup.Text)).ToString();
            }
        }

      

    }
}
