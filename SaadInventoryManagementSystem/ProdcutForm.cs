using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Data_Access_Layer;
using Business_Layer;
using Property_Layer;


namespace SaadInventoryManagementSystem
{
    public partial class ProductForm : Form
    {
        ProductBLL bll = new ProductBLL();
        
        public ProductForm()
        {
            InitializeComponent();
        }

        private void Savebutton_Click(object sender, EventArgs e)
        {
            if (!validatecategoryform1())
            {
                return;
            }

            ProductFormProperties pf = new ProductFormProperties
            {
                CategoryId = Convert.ToInt32(comboBoxCategory.SelectedValue),
                ProductName = ProductNametextBox1.Text.Trim(),
                Quantity =Convert.ToInt32( QuantiytextBox2.Text.Trim()),
                Price =Convert.ToDecimal(PricetextBox3.Text.Trim())
            };
            int result = bll.InsertProductBLL(pf);
            if (result > 0)
            {
                MessageBox.Show("Product saved successfully");
                loadcategories();
                clearform();
            }
            else if (result == -2)
            {
                MessageBox.Show("Product already exists");
            }
            else
            {
                MessageBox.Show("Error saving Product");
            }
          
        }

        private void Updatebutton_Click(object sender, EventArgs e)
        {
            if (!validatecategoryform1())
            {
                return;
            }
            ProductFormProperties pf = new ProductFormProperties
            {
                CategoryId = Convert.ToInt32(comboBoxCategory.SelectedValue),
                ProductName = ProductNametextBox1.Text.Trim(),
                Quantity = Convert.ToInt32(QuantiytextBox2.Text.Trim()),
                Price = Convert.ToDecimal(PricetextBox3.Text.Trim()),
                

        };
            int result = bll.UpdateProductBLL(pf);
            if (result > 0)
            {
                MessageBox.Show("Product updated successfully");
                loadcategories();
                clearform();
            }
            else
            {
                MessageBox.Show("Error updating Product");
            }
            ProductNametextBox1.Focus();
        }

       

        private void Deletebutton_Click(object sender, EventArgs e)
        {
            if (!validatecategoryform1())
            {
                return;
            }
            int id = Convert.ToInt32(CategoryIDtextBox3.Text);
            ProductFormProperties pf = new ProductFormProperties
            {
                CategoryID = id,
            };
            DialogResult dr = MessageBox.Show("Are you sure??", "Confirm", MessageBoxButtons.YesNo);
            if (dr == DialogResult.Yes)
            {
                int result = bll.DeleteProductBLL(pf); ;
                if (result > 0)
                {
                    MessageBox.Show("Category Deleted successfully");
                    loadcategories();
                    clearform();
                }
                else
                {
                    MessageBox.Show("Error while deleting categpry");
                }
            }
            ProductNametextBox1.Focus();
        }

        private void Clearbutton_Click(object sender, EventArgs e)
        {
            clearform();
            ProductNametextBox1.Focus();
        }

        private void ProductForm_Load(object sender, EventArgs e)
        {
            loadcategories();
            ProductNametextBox1.Focus();
        }


        public bool validatecategoryform1()
        {
            string productname = ProductNametextBox1.Text.Trim();
            if (string.IsNullOrEmpty(productname))
            {
                MessageBox.Show("Product Name cannot be empty ");
                ProductNametextBox1.Focus();
                return false;
            }
            return true;
        }


        public void loadcategories()
        {
            CategoryDAL category = new CategoryDAL();
            bool showdeleted = chkShowDeleted.Checked;
            DataTable dt = bll.ViewProductBLL(showdeleted);
            dataGridView1.DataSource = dt;

            comboBoxCategory.DataSource = category.getactivecategories();
            comboBoxCategory.DisplayMember = "CategoryName";
            comboBoxCategory.ValueMember = "CategoryID";
        }

        public void clearform()
        {
            ProductNametextBox1.Text = "";
            comboBoxCategory.SelectedItem=-1;
            QuantiytextBox2.Text = "";
            PricetextBox3.Text = "";
            chkShowDeleted.Checked = false;
        }

        private void chkShowDeleted_CheckedChanged(object sender, EventArgs e)
        {
            loadcategories();
        }

        private void comboBoxCategory_SelectedIndexChanged(object sender, EventArgs e)
        {
            
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            dataGridView1.CurrentRow.Selected = true;
            ProductNametextBox1.Text = dataGridView1.Rows[e.RowIndex].Cells["ProductName"].Value.ToString();
            QuantiytextBox2.Text = dataGridView1.Rows[e.RowIndex].Cells["Quantity"].Value.ToString();
        }
    }
}
