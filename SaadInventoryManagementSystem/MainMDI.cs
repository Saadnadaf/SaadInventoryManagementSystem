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
        public MainMDI()
        {
            InitializeComponent();
            this.IsMdiContainer = true;
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void categoryToolStripMenuItem_Click(object sender, EventArgs e)
        {
            foreach(Form f in this.MdiChildren)
            {
                if(f is CategoryForm)
                {
                    f.Activate();
                    return;
                }

                f.Close();
            }
            CategoryForm cf = new CategoryForm();
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
