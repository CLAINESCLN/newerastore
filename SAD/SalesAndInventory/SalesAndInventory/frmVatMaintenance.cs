using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SalesAndInventory
{
    public partial class frmVatMaintenance : Form
    {
        public static int VAT = 0;

        public static void initializeVat()
        {
            
        }

        public frmVatMaintenance()
        {
            InitializeComponent();
        }

        private void frmVatMaintenance_Load(object sender, EventArgs e)
        {
            txtValueAddedTax.Text = frmHome.vat.ToString();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtValueAddedTax.Text))
            {
                MessageBox.Show("Field is empty.");
                return;
            }

            using (MySql.Data.MySqlClient.MySqlConnection connection = SalesAndInventory.Classes.Configuration.getConnection())
            {
                try
                {
                    connection.Open();
                    string sql = "UPDATE users SET vat = @vat WHERE user_id = @userId";
                    MySql.Data.MySqlClient.MySqlCommand command = new MySql.Data.MySqlClient.MySqlCommand(sql, connection);
                    command.Parameters.AddWithValue("@userId",frmHome.userId);
                    command.Parameters.AddWithValue("@vat",txtValueAddedTax.Text);
                    command.ExecuteNonQuery();
                    frmHome.vat = int.Parse(txtValueAddedTax.Text);
                    MessageBox.Show("Successfully Updated.");
                    this.Close();
                }
                catch (MySql.Data.MySqlClient.MySqlException exception)
                {
                    MessageBox.Show(exception.Message);
                }
            }
        }
    }
}
