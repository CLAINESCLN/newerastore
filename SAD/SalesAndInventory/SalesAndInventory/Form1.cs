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
    public partial class frmFileMain : Form
    {
        public frmFileMain()
        {
            InitializeComponent();
        }

        private void btnCatMain_Click(object sender, EventArgs e)
        {
            frmCategoryMaintenance frmCatMain = new frmCategoryMaintenance();
            frmCatMain.ShowDialog();
        }

        private void frmFileMain_Load(object sender, EventArgs e)
        {

        }

        private void btnSubCatMain_Click(object sender, EventArgs e)
        {
            frmSubCategoryMaintenance frmSubCatMain = new frmSubCategoryMaintenance();
            frmSubCatMain.ShowDialog();

        }

        private void btnSupplierMain_Click(object sender, EventArgs e)
        {
            frmSupplierMaintenance frmSupMain = new frmSupplierMaintenance();
            frmSupMain.ShowDialog();


        }

        private void btnVatMain_Click(object sender, EventArgs e)
        {
            frmVatMaintenance frmVatMain = new frmVatMaintenance();
            frmVatMain.ShowDialog();
        }

        private void btnUnit_Click(object sender, EventArgs e)
        {
            frmUnit frmUnit = new frmUnit();
            frmUnit.ShowDialog();

        }

        private void btnProduct_Click(object sender, EventArgs e)
        {
            frmProduct frmProduct = new frmProduct();
            frmProduct.ShowDialog();

        }

        private void btnRefrigirated_Click(object sender, EventArgs e)
        {
            frmRefrigirated frmRef = new frmRefrigirated();
            frmRef.ShowDialog();
        }

        private void btnDiscount_Click(object sender, EventArgs e)
        {
            frmDiscount frmDiscount = new frmDiscount();
            frmDiscount.ShowDialog();
        }
    }
}
