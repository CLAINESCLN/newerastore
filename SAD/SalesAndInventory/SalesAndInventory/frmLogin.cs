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
    public partial class frmLogin : Form
    {
        public frmLogin()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtUserName.Text) || (string.IsNullOrEmpty(txtPassword.Text)))
            {
                MessageBox.Show("Field is empty.");
                return;
            }

            using (MySqlConnection con = Configuration.getConnection())
            {
                try
                {
                    con.Open();
                    string sql = "SELECT user_id,user_name,role,vat FROM users WHERE username = @username AND password = @password";
                    MySqlCommand command = new MySqlCommand(sql, con);
                    command.Parameters.AddWithValue("@username",txtUserName.Text);
                    command.Parameters.AddWithValue("@password",txtPassword.Text);
                    MySqlDataReader reader = command.ExecuteReader();
                    if (reader.Read())
                    {
                        frmHome.userId = int.Parse(reader["user_id"].ToString());
                        frmHome.name = reader["user_name"].ToString();
                        frmHome.role = reader["role"].ToString();
                        frmHome.vat = int.Parse(reader["vat"].ToString());
                        frmHome home = new frmHome();
                        this.Hide();
                        home.login = this;
                        home.Show();
                        
                        
                    }
                    else
                        MessageBox.Show("Invalid username or password.");
                }
                catch (MySqlException exception)
                {
                    MessageBox.Show(exception.Message);
                }
            }
        }

        public void clear()
        {
            txtUserName.Text = string.Empty;
            txtPassword.Text = string.Empty;
            frmHome.name = string.Empty;
            frmHome.role = string.Empty;
            frmHome.vat = 0;
            frmHome.userId = 0;
        }

        private void frmLogin_Load(object sender, EventArgs e)
        {

        }
    }
}
