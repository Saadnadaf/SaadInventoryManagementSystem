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
    public partial class CategoriesForm : Form
    {
        public CategoriesForm()
        {
            InitializeComponent();
        }

        private void CategoryNametextBox1_TextChanged(object sender, EventArgs e) { }

        private void DescriptiontextBox2_TextChanged(object sender, EventArgs e) { }

        private void CategoryIDtextBox3_TextChanged(object sender, EventArgs e) { }

        CategoryBLL bll = new CategoryBLL();
        private void Savebutton_Click(object sender, EventArgs e)
        {
            if (!validatecategoryform1())
            {
                return;
            }

            CategoryfrmProperties cf = new CategoryfrmProperties
            {
                CategoryName = CategoryNametextBox1.Text.Trim(),
                Description = DescriptiontextBox2.Text.Trim()
            };
            int result = bll.InsertCategoryBLL(cf);
            if (result > 0)
            {
                MessageBox.Show("Category saved successfully");
                loadcategories();
                clearform();
            }
            else if (result == -2)
            {
                MessageBox.Show("Category already exists");
            }
            else
            {
                MessageBox.Show("Error saving Category");
            }

            CategoryNametextBox1.Focus();
        }

        private void Updatebutton_Click(object sender, EventArgs e)
        {
            if (!validatecategoryform2())
            {
                return;
            }
            CategoryfrmProperties cf = new CategoryfrmProperties
            {
                CategoryID = Convert.ToInt32(CategoryIDtextBox3.Text),
                CategoryName = CategoryNametextBox1.Text,
                Description = DescriptiontextBox2.Text,
                IsActive = Convert.ToInt32(chkShowDeleted.Checked)
            };
            int result = bll.UpdateCategoryBLL(cf);
            if (result > 0)
            {
                MessageBox.Show("Category updated successfully");
                loadcategories();
                clearform();
            }
            else
            {
                MessageBox.Show("Error updating category");
            }
            CategoryNametextBox1.Focus();
        }

        private void Deletebutton_Click(object sender, EventArgs e)
        {
            if (!validatecategoryform2())
            {
                return;
            }
            int id = Convert.ToInt32(CategoryIDtextBox3.Text);
            CategoryfrmProperties cf = new CategoryfrmProperties
            {
                CategoryID = id,
            };
            DialogResult dr = MessageBox.Show("Are you sure??", "Confirm", MessageBoxButtons.YesNo);
            if(dr == DialogResult.Yes)
            {
                int result = bll.DeleteCategoryBLL(cf); ;
                if(result > 0)
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
                CategoryNametextBox1.Focus();
        }

        private void Clearbutton_Click(object sender, EventArgs e)
        {
            clearform();
            CategoryNametextBox1.Focus();
        }
            

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
             
        }

        public void loadcategories()
        {
            bool showdeleted = chkShowDeleted.Checked;
            DataTable dt = bll.ViewCategoriesBLL(showdeleted);
            dataGridView1.DataSource = dt;
            dataGridView1.Columns["CategoryId"].Visible = false;

            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        }


        public void clearform()
        {
            CategoryNametextBox1.Text = "";
            DescriptiontextBox2.Text = "";
            CategoryIDtextBox3.Text = "";
            chkShowDeleted.Checked = false;
        }

        private void CategoriesForm_Load(object sender, EventArgs e)
        {
            loadcategories();
        }

        public bool validatecategoryform1()
        {
            string categoryname = CategoryNametextBox1.Text.Trim();
            if (string.IsNullOrEmpty(categoryname))
            {
                MessageBox.Show("Category Name cannot be empty ");
                CategoryNametextBox1.Focus();
                return false;
            }
            return true;
        }

        public bool validatecategoryform2()
        {
            string categoryid = Convert.ToString(CategoryIDtextBox3.Text.Trim());
            if (categoryid == "")
            {
                MessageBox.Show("Category ID cannot be empty ");
                CategoryNametextBox1.Focus();
                return false;
            }
            return true;
        }
        private void chkShowDeleted_CheckedChanged(object sender, EventArgs e)
        {
            loadcategories();
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            dataGridView1.CurrentRow.Selected = true;
            CategoryNametextBox1.Text = dataGridView1.Rows[e.RowIndex].Cells["CategoryName"].Value.ToString();
            DescriptiontextBox2.Text = dataGridView1.Rows[e.RowIndex].Cells["Description"].Value.ToString();
            CategoryIDtextBox3.Text = dataGridView1.Rows[e.RowIndex].Cells["CategoryId"].Value.ToString();
                 
        }
    }

   
}
