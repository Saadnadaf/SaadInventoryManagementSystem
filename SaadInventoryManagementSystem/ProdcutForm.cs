﻿using System;
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
        CategoryBLL cbll = new CategoryBLL(); 
        ProductBLL bll = new ProductBLL();
        CategoryfrmProperties cf = new CategoryfrmProperties();

        public ProductForm()
        {
            InitializeComponent();
            QuantiytextBox2.KeyPress += QuantiytextBox2_KeyPress;
            PricetextBox3.KeyPress += PricetextBox3_KeyPress;
            ProductNametextBox1.KeyPress += ProductNametextBox1_KeyPress;

        }

        private void Savebutton_Click(object sender, EventArgs e)
        {
            if (!validateproductform1())
            {
                return; 
            }
            string categoryname = comboBoxCategory.Text.Trim();
            string description = DescriptiontextBox.Text.Trim();
            int categoryid;
            try
            {
                categoryid = cbll.GetorCreateCategoryID(categoryname,description);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return;
            }
            ProductFormProperties pf = new ProductFormProperties
            {
                CategoryName = categoryname,
                CategoryId = categoryid,
                Description = description,
                ProductName = ProductNametextBox1.Text.Trim(),
                Quantity = Convert.ToInt32(QuantiytextBox2.Text.Trim()),
                Price = Convert.ToDecimal(PricetextBox3.Text.Trim())
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

            comboBoxCategory.Focus();

        }

        private void Updatebutton_Click(object sender, EventArgs e)
        {
            
            if (!validateproductform1())
            {
                comboBoxCategory.Focus();
                return;
            }

            if (dataGridView1.SelectedRows.Count == 0)
            {
                MessageBox.Show("Please select a product to return");
                return;
            }

            string categoryname = comboBoxCategory.Text.Trim();
            string description = DescriptiontextBox.Text.Trim();
            int categoryid = cbll.GetorCreateCategoryID(categoryname,description);
            int productid = Convert.ToInt32(dataGridView1.SelectedRows[0].Cells["ProductId"].Value);
            ProductFormProperties pf = new ProductFormProperties
            {
                CategoryId = categoryid,
                Description = description,
                //CategoryId = Convert.ToInt32(comboBoxCategory.SelectedValue),
                ProductName = ProductNametextBox1.Text.Trim(),
                Quantity = Convert.ToInt32(QuantiytextBox2.Text.Trim()),
                Price = Convert.ToDecimal(PricetextBox3.Text.Trim()),
                ProductId = productid

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
            comboBoxCategory.Focus();
        }



        private void Deletebutton_Click(object sender, EventArgs e)
        {
            if (!validateproductform1())
            {
                comboBoxCategory.Focus();
                return;
            }
            int productid = Convert.ToInt32(dataGridView1.SelectedRows[0].Cells["ProductId"].Value);
            ProductFormProperties pf = new ProductFormProperties
            {
                ProductId = productid

            };
            DialogResult dr = MessageBox.Show("Are you sure??", "Confirm", MessageBoxButtons.YesNo);
            if (dr == DialogResult.Yes)
            {
                int result = bll.DeleteProductBLL(pf); ;
                if (result > 0)
                {
                    MessageBox.Show("Product Deleted successfully");
                    loadcategories();
                    clearform();
                }
                else
                {
                    MessageBox.Show("Error while deleting product");
                }
            }
            comboBoxCategory.Focus(); 
        }

        private void Clearbutton_Click(object sender, EventArgs e)
        {
            clearform();
            comboBoxCategory.Focus();
        }

        private void ProductForm_Load(object sender, EventArgs e)
        {
            loadcategories();
            BeginInvoke((MethodInvoker)(() => comboBoxCategory.Focus()));
        }


        public bool validateproductform1()
        {
            string cmbcategory = comboBoxCategory.Text.Trim();
            if (string.IsNullOrEmpty(cmbcategory))
            {
                MessageBox.Show("Category Name cannot be empty ");
                comboBoxCategory.Focus();
                return false;
            }

            string productname = ProductNametextBox1.Text.Trim();
            if (string.IsNullOrEmpty(productname))
            {
                MessageBox.Show("Product Name cannot be empty ");
                ProductNametextBox1.Focus();
                return false;
            }
            string quantitystr = QuantiytextBox2.Text.Trim();
            if (string.IsNullOrEmpty(quantitystr))
            {
                MessageBox.Show("Quanity cannot be empty ");
                QuantiytextBox2.Focus();
                return false;
            }
            int quantity = Convert.ToInt32(QuantiytextBox2.Text.Trim());
            
            if (quantity <= 0 )
            {
                MessageBox.Show("Quantity should be more than 0");
                QuantiytextBox2.Focus();
                return false;
            }
            string pricestr = PricetextBox3.Text.Trim();
            if (string.IsNullOrEmpty(pricestr))
            {
                MessageBox.Show("Price should not be empty");
                PricetextBox3.Focus();
                return false;
            }
            decimal price = Convert.ToDecimal(PricetextBox3.Text.Trim());
            if (price <= 0)
            {
                MessageBox.Show("Price should be more than 0");
                PricetextBox3.Focus();
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
            dataGridView1.Columns["ProductId"].Visible = false;
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            comboBoxCategory.DataSource = category.getactivecategories();
            comboBoxCategory.DisplayMember = "CategoryName";
            comboBoxCategory.ValueMember = "CategoryID";
            comboBoxCategory.SelectedIndex = -1;
            comboBoxCategory.DropDownStyle = ComboBoxStyle.DropDown;
            comboBoxCategory.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            comboBoxCategory.AutoCompleteSource = AutoCompleteSource.ListItems;
            comboBoxCategory.IntegralHeight = false;
        }


        public void clearform()
        {
            ProductNametextBox1.Text = "";
            DescriptiontextBox.Text = "";
            comboBoxCategory.SelectedIndex = -1;
            QuantiytextBox2.Text = "";
            PricetextBox3.Text = "";
            chkShowDeleted.Checked = false;
        }

        private void chkShowDeleted_CheckedChanged(object sender, EventArgs e)
        {
            loadcategories();
            comboBoxCategory.Focus();
        }

        private void comboBoxCategory_SelectedIndexChanged(object sender, EventArgs e) { }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e) { }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;
            dataGridView1.CurrentRow.Selected = true;
            ProductNametextBox1.Text = dataGridView1.Rows[e.RowIndex].Cells["ProductName"].Value.ToString();
            QuantiytextBox2.Text = dataGridView1.Rows[e.RowIndex].Cells["Quantity"].Value.ToString();
            PricetextBox3.Text = dataGridView1.Rows[e.RowIndex].Cells["Price"].Value.ToString();
            comboBoxCategory.Text = dataGridView1.Rows[e.RowIndex].Cells["CategoryName"].Value.ToString();
            DescriptiontextBox.Text= dataGridView1.Rows[e.RowIndex].Cells["Description"].Value.ToString();
        }

        private void QuantiytextBox2_KeyPress(object sender, KeyPressEventArgs e)
        {
            if(!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void PricetextBox3_KeyPress(object sender, KeyPressEventArgs e)
        {
            if(!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar))
            {
                e.Handled = true;
            }
        }   

        private void ProductNametextBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            
            if(!char.IsLetter(e.KeyChar) && !char.IsControl(e.KeyChar) && !char.IsWhiteSpace(e.KeyChar))
            {
                e.Handled = true;
            }
        }
    }
}
