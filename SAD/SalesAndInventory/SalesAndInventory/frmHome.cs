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
    public partial class frmHome : Form
    {

        public static string name;
        public static string role;
        public static int vat;
        public static int userId;

        public frmLogin login { set; get; }

        public frmHome()
        {
            InitializeComponent();
        }

        private void frmHome_Load(object sender, EventArgs e)
        {
            lblName.Text = name;
            lblUserType.Text = role;
        }

        private void button7_Click(object sender, EventArgs e)
        {
            this.Close();
            login.clear();
            login.Show();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            frmFileMain fileMain = new frmFileMain();
            fileMain.Home = this;
            this.Hide();
            fileMain.Show();
            
        }
    }
}
