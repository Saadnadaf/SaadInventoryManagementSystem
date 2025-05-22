using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Configuration;
    
namespace SaadInventoryManagementSystem
{
    public partial class MainMDI : Form
    {
        SqlConnection con = new SqlConnection();
        SqlCommand cmd = new SqlCommand();
        SqlDataAdapter da = new SqlDataAdapter();
        DataTable dt = new DataTable();
        public MainMDI()
        {
            InitializeComponent();
            this.IsMdiContainer = true;
            this.WindowState = FormWindowState.Maximized;
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
             foreach(Form child in this.MdiChildren)
            {
                child.Close();
            }
        }

        private void categoryToolStripMenuItem_Click(object sender, EventArgs e)
        {
            foreach(Form f in this.MdiChildren)
            {
                if(f is CategoriesForm)
                {
                    f.Activate();
                    return;
                }

                f.Close();
            }
            CategoriesForm cf = new CategoriesForm();
            cf.MdiParent = this;
            cf.Show();
        }

        private void productsToolStripMenuItem_Click(object sender, EventArgs e)
         
        {
            foreach(Form f in this.MdiChildren)
            {
                if(f is ProductForm)
                {
                    f.Activate();
                    return;
                }

                f.Close();
            }
            ProductForm pf = new ProductForm();
            pf.MdiParent = this;
            pf.Show();
        }

        
    }
}
