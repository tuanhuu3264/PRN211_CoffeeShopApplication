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
using Team4.GroupProject.BusinessObjects.Models;
using Team4.GroupProject.BusinessServices.Interface;
using Team4.GroupProject.BusinessServices.Services;

namespace PetMS
{
    public partial class Products : Form
    {
        IProductService _productService;
        ICategoryService _categoryService;
        IBillDetailService _billDetailService;
        public Products()
        {
            InitializeComponent();
            _productService = new ProductService();
            _categoryService = new CategoryService();
            _billDetailService = new BillDetailService();
            DisplayProduct();
            var categories = _categoryService.GetAll();
            BindingSource source1 = new BindingSource();
            source1.DataSource = categories;

            cb_Category.DataSource = source1;
            cb_Category.ValueMember = "Id";
            cb_Category.DisplayMember = "Name";
            //
            var allCategory = new Category() { Id = 0, Name = "All" };
            var categoriesWithAll = categories.Concat(new[] { allCategory }).ToList();
            BindingSource source2 = new BindingSource();
            source2.DataSource = categoriesWithAll;
            cbox_FilterCategory.DataSource = source2;
            cbox_FilterCategory.ValueMember = "Id";
            cbox_FilterCategory.DisplayMember = "Name";

            EmpNameLbl.Text = Login.Employee;
        }

        SqlConnection Con = new SqlConnection(@"Server=DESKTOP-PNA89UR;Database=PetShopp;Trusted_Connection=True;TrustServerCertificate=True");
        private void DisplayProduct()
        {
            var products = _productService.GetAll();
            BindingSource source = new BindingSource();
            source.DataSource = products;
            ProductDGV.DataSource = products;
        }

        private void Clear()
        {
            Key = 0;
            PrNameTb.Text = "";
            QtyTb.Text = "";
            PriceTb.Text = "";
            cb_Category.SelectedIndex = 0;
        }
        private void SaveBtn_Click(object sender, EventArgs e)
        {
            // Check Validation Of Fields
            if (Key != 0)
            {
                MessageBox.Show("You must reset value by Reset button then you can create new data", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (PrNameTb.Text.Trim() == "")
            {
                MessageBox.Show("Missing Information Name.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (PriceTb.Text.Trim() == "")
            {
                MessageBox.Show("Missing Information Price.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            try
            {
                decimal check = decimal.Parse(PriceTb.Text.Trim());
            }
            catch (FormatException)
            {
                MessageBox.Show("Price is not correct format.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (QtyTb.Text.Trim() == "")
            {
                MessageBox.Show("Missing Information Quantity.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            try
            {
                int check = int.Parse(QtyTb.Text.Trim());
            }
            catch (FormatException)
            {
                MessageBox.Show("Quantity is not correct format.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (txt_Description.Text.Trim() == "")
            {
                MessageBox.Show("Missing Information Description.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (cb_Category.SelectedValue == null)
            {
                MessageBox.Show("Please to choose a category", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;

            }
            // Start to create new Product
            try
            {
                // Take value of combox
                int categoryId = 1;
                object selectedValue = cbox_FilterCategory.SelectedValue;
                if (selectedValue != null && int.TryParse(selectedValue.ToString(), out int parsedCategoryId))
                {
                    categoryId = parsedCategoryId;
                }
                Product product = new Product()
                {
                    Name = PrNameTb.Text.Trim(),
                    Description = txt_Description.Text.Trim(),
                    Price = decimal.Parse(PriceTb.Text.Trim()),
                    Quantity = int.Parse(QtyTb.Text.Trim()),
                    CategoryId = categoryId
                };
                //Add product
                if (_productService.Add(product))
                {
                    MessageBox.Show("Product is added successfully", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    DisplayProduct();
                    Clear();
                    return;
                }
                MessageBox.Show("Product is added unsuccessfully", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);

            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message);
            }
        }

        int Key = 0;

        private void EditBtn_Click(object sender, EventArgs e)
        {
            // Check Validation Of Fields
            if (Key == 0)
            {
                MessageBox.Show("You must choose a product to edit", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (PrNameTb.Text.Trim() == "")
            {
                MessageBox.Show("Missing Information Name.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (PriceTb.Text.Trim() == "")
            {
                MessageBox.Show("Missing Information Price.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            try
            {
                decimal check = decimal.Parse(PriceTb.Text.Trim());
            }
            catch (FormatException)
            {
                MessageBox.Show("Price is not correct format.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (QtyTb.Text.Trim() == "")
            {
                MessageBox.Show("Missing Information Quantity.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            try
            {
                int check = int.Parse(QtyTb.Text.Trim());
            }
            catch (FormatException)
            {
                MessageBox.Show("Quantity is not correct format.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (txt_Description.Text.Trim() == "")
            {
                MessageBox.Show("Missing Information Description.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (cb_Category.SelectedValue == null)
            {
                MessageBox.Show("Please to choose a category", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            // Start to update 
            try
            {
                // Take value of combox
                int categoryId = -1;
                object selectedValue = cbox_FilterCategory.SelectedValue;
                if (selectedValue != null && int.TryParse(selectedValue.ToString(), out int parsedCategoryId))
                {
                    categoryId = parsedCategoryId;
                }
                Product product = _productService.GetById(Key);


                product.Name = PrNameTb.Text.Trim();
                product.Description = txt_Description.Text.Trim();
                product.Price = decimal.Parse(PriceTb.Text.Trim());
                product.Quantity = int.Parse(QtyTb.Text.Trim());
                if (categoryId != -1) product.CategoryId = categoryId;

                //Add product
                if (_productService.Update(product))
                {
                    MessageBox.Show("Product is updated successfully", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    DisplayProduct();
                    Clear();
                    return;
                }
                MessageBox.Show("Product is updated unsuccessfully", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);

            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message);
            }

        }


        private void DeleteBtn_Click(object sender, EventArgs e)
        {
            if (Key == 0)
            {
                MessageBox.Show("Must select an product to delete.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            else
            {
                var conditionBill = _billDetailService.GetByProductId(Key);
                if (conditionBill.Any())
                {
                    MessageBox.Show("Cannot delete this product that has existence in some bills.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                var deletedEmployee = _productService.GetById(Key);
                if (deletedEmployee == null)
                {
                    MessageBox.Show("Must select an product to delete.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                if (_productService.Delete(Key))
                {
                    MessageBox.Show("Delete successfully.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    DisplayProduct();
                    Clear();
                    return;
                }
                MessageBox.Show("Delete unsuccessfully.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void ProductDGV_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

            int ind = e.RowIndex;
            DataGridViewRow selectedRows = ProductDGV.Rows[ind];
            if (ind != -1)
            {
                PrNameTb.Text = selectedRows.Cells[1].Value.ToString();
                cb_Category.SelectedValue = selectedRows.Cells[2].Value;
                QtyTb.Text = selectedRows.Cells[3].Value.ToString();
                PriceTb.Text = selectedRows.Cells[4].Value.ToString();
                txt_Description.Text = selectedRows.Cells["Description"].Value.ToString();
            }
            if (PrNameTb.Text == "")
            {
                Key = 0;
            }
            else
            {
                Key = Convert.ToInt32(selectedRows.Cells[0].Value.ToString());
            }
        }

        private void label2_Click(object sender, EventArgs e)
        {
            Homes obj = new Homes();
            obj.Show();
            this.Hide();
        }

        private void label1_Click(object sender, EventArgs e)
        {
            Products obj = new Products();
            obj.Show();
            this.Hide();
        }

        private void label3_Click(object sender, EventArgs e)
        {
            Employees obj = new Employees();
            obj.Show();
            this.Hide();
        }

        private void label4_Click(object sender, EventArgs e)
        {
            Customers obj = new Customers();
            obj.Show();
            this.Hide();
        }

        private void label5_Click(object sender, EventArgs e)
        {
            Billings obj = new Billings();
            obj.Show();
            this.Hide();
        }

        private void label6_Click(object sender, EventArgs e)
        {
            Login obj = new Login();
            obj.Show();
            this.Hide();
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void bttn_Reset_Click(object sender, EventArgs e)
        {
            Clear();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            Search();
        }

        private void cbox_FilterCategory_SelectedIndexChanged(object sender, EventArgs e)
        {
            Search();
        }
        private void Search()
        {
            string keyword = txt_Search.Text.Trim();
            int categoryId = 0;
            object selectedValue = cbox_FilterCategory.SelectedValue;
            if (selectedValue != null && int.TryParse(selectedValue.ToString(), out int parsedCategoryId))
            {
                categoryId = parsedCategoryId;
            }

            IEnumerable<Product> products;
            if (cbox_FilterCategory.Text.ToString() == "All") products = _productService.Search(keyword, 0);
            else products = _productService.Search(keyword, categoryId);
            BindingSource source = new BindingSource();
            source.DataSource = products;
            ProductDGV.DataSource = source;
        }
    }
}
