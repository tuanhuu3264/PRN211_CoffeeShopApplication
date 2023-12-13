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
    public partial class Billings : Form
    {
        IProductService _productService;
        ICustomerService _customerService;
        IEmployeeService _employeeService;
        IBillDetailService _billDetailService;
        IBillService _billService;
        public Billings()
        {
            // Initialize the services
            _productService = new ProductService();
            _customerService = new CustomerService();
            _employeeService = new EmployeeService();
            _billDetailService = new BillDetailService();
            _billService = new BillService();

            InitializeComponent();
            EmpNameLbl.Text = Login.Employee;
            GetCustomers();
            DisplayProduct();
            DisplayTransactions();
        }

        SqlConnection Con = new SqlConnection(@"Server=DESKTOP-PNA89UR;Database=PetShopp;Trusted_Connection=True;TrustServerCertificate=True");
        // Gán dữ liệu cho combox Customer
        private void GetCustomers()
        {
            try
            {
                // Get all customers from the customer service
                var customers = _customerService.GetAll().ToList();

                // Set the data source of the ComboBox to the list of customers
                cb_Customer.DataSource = customers;

                // Set the property that represents the actual value of the items
                cb_Customer.ValueMember = "Id";

                // Set the property that is displayed for the items
                cb_Customer.DisplayMember = "Name";
            }
            catch (Exception ex)
            {

            }

        }
        private void DisplayProduct()
        {
            var products = _productService.GetAll().Select(p => new
            {
                p.Id,
                p.Name,
                p.Price,
                p.Description,
                p.Quantity,
                CategoryName = p.Category.Name
            });
            BindingSource source = new BindingSource();
            source.DataSource = products;
            ProductsDGV.DataSource = source;
        }
        private void DisplayTransactions()
        {
            var transactions = _billService.GetAll().Select(t => new
            {
                t.Id,
                t.Date,
                EmployeeName = t.Employee.Name,
                CustomerName = t.Customer.Name,
                CustomerPhone = t.Customer.Phone
            });
            BindingSource source = new BindingSource() { DataSource = transactions };
            Transactions.DataSource = source;
        }
        private void GetCustName()
        {
            if (int.TryParse(cb_Customer.SelectedValue?.ToString(), out int customerId))
            {
                var customer = _customerService.GetById(customerId); ;

                txt_CustomerName.Text = customer.Name;
            }
            else
            {
                
            }
        }
        private void UpdateStock()
        {
            try
            {
                int descreasedQuantity = Convert.ToInt32(QtyTb.Text.Trim());
                _productService.UpdateNumberDecrease(Key, descreasedQuantity);
                DisplayProduct();
            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message);
            }
        }



        private void label5_Click(object sender, EventArgs e)
        {
            Billings obj = new Billings();
            obj.Show();
            this.Hide();
        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }
        int n = 0; decimal GrdTotal = 0;
        private void AddToBillBtn_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(QtyTb.Text.Trim()))
                {
                    MessageBox.Show("The quantity needs be bought is empty.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                int quantity = int.Parse(QtyTb.Text.Trim());

            }
            catch (FormatException)
            {
                MessageBox.Show("The quantity is not correct format.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (Convert.ToInt32(QtyTb.Text) > Stock)
            {
                MessageBox.Show("No Enough In House");
            }
            else if (QtyTb.Text == "" || Key == 0)
            {
                MessageBox.Show("Missing Information");
            }
            else
            {

                decimal total = Convert.ToInt32(QtyTb.Text) * Convert.ToDecimal(PrPriceTb.Text.Trim());
                DataGridViewRow newRow = new DataGridViewRow();
                newRow.CreateCells(BillDGV);
                DataGridViewCell CellByName(string columnName)
                {
                    var column = BillDGV.Columns[columnName];
                    if (column == null)
                        throw new InvalidOperationException("Unknown column name: " + columnName);
                    return newRow.Cells[column.Index];
                }

                foreach (DataGridViewRow row in BillDGV.Rows)
                {
                    int productId = Convert.ToInt32(row.Cells["ID"].Value);
                    if (productId == Key)
                    {
                        try
                        {

                            int quantityNow = Convert.ToInt32(row.Cells["Quantity"].Value.ToString());
                            if (quantityNow >= Stock)
                            {
                                MessageBox.Show("The stock is full. Can not add continuesly this product", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                return;
                            }
                            int quantityPlus = Convert.ToInt32(QtyTb.Text.Trim());
                            decimal totalNow = Convert.ToDecimal(row.Cells["Total"].Value.ToString());
                            totalNow += total;
                            row.Cells["Total"].Value = totalNow;
                            row.Cells["Quantity"].Value = (quantityNow + quantityPlus).ToString();
                            return;
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show("Error:" + ex.Message + " at 185 line");
                            return;
                        }

                    }
                }
                CellByName("ID").Value = Key;
                CellByName("Product").Value = PrNameTb.Text;
                CellByName("Price").Value = PrPriceTb.Text;
                CellByName("Quantity").Value = QtyTb.Text;
                CellByName("Total").Value = total;
                GrdTotal += total;
                BillDGV.Rows.Add(newRow);
                n++;
                TotalLbl.Text = "Rs" + GrdTotal;
                Reset();
            }
        }

        private void ResetBtn_Click(object sender, EventArgs e)
        {
            Reset();
        }

        private void PrintBtn_Click(object sender, EventArgs e)
        {
            if (InsertBill())
            {
                printDocument1.DefaultPageSettings.PaperSize = new System.Drawing.Printing.PaperSize("pprnm", 285, 600);
                if (printPreviewDialog1.ShowDialog() == DialogResult.OK)
                {
                    printDocument1.Print();
                }
                UpdateStock();
            }
        }

        private void CustIdCb_SelectedIndexChanged(object sender, EventArgs e)
        {
            GetCustName();
        }
        int Key = 0, Stock = 0;
        private void Reset()
        {
            PrNameTb.Text = "";
            QtyTb.Text = "";
            PrPriceTb.Text = "";
            Stock = 0;
            Key = 0;

        }
        string prodid, prodqty, prodprice, tottal = "";
        int pos = 60;

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

        private void printDocument1_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            String searchValue = "somestring";
            int rowIndex = -1;
            e.Graphics.DrawString("PetShop", new Font("Century Gothic", 12, FontStyle.Bold), Brushes.Red, new Point(120));
            e.Graphics.DrawString("ID| PRODUCT| PRICE| QUATITY| TOTAL", new Font("Century Gothic", 10, FontStyle.Bold), Brushes.Red, new Point(18, 40));
            foreach (DataGridViewRow row in BillDGV.Rows)
            {

                DataGridViewCell CellByName(string columnName)
                {
                    var column = BillDGV.Columns[columnName];
                    if (column == null)
                        throw new InvalidOperationException("Unknown column name: " + columnName);
                    return row.Cells[column.Index];
                }
                prodid = "" + CellByName("ID").Value;
                prodname = "" + CellByName("Product").Value;
                prodprice = "" + CellByName("Price").Value;
                prodqty = "" + CellByName("Quantity").Value;
                tottal = "" + CellByName("Total").Value;

                e.Graphics.DrawString("" + prodid, new Font("Century Gothic", 8, FontStyle.Bold), Brushes.Blue, new Point(26, pos));
                e.Graphics.DrawString("" + prodname, new Font("Century Gothic", 8, FontStyle.Bold), Brushes.Blue, new Point(45, pos));
                e.Graphics.DrawString("" + prodprice, new Font("Century Gothic", 8, FontStyle.Bold), Brushes.Blue, new Point(120, pos));
                e.Graphics.DrawString("" + prodqty, new Font("Century Gothic", 8, FontStyle.Bold), Brushes.Blue, new Point(170, pos));
                e.Graphics.DrawString("" + tottal, new Font("Century Gothic", 8, FontStyle.Bold), Brushes.Blue, new Point(235, pos));
                pos += 30;

            }
            e.Graphics.DrawString("Grand Total: Rs" + GrdTotal, new Font("Century Gothic", 12, FontStyle.Bold), Brushes.Crimson, new Point(50, pos + 30));
            e.Graphics.DrawString("***************PetShop****************", new Font("Century Gothic", 12, FontStyle.Bold), Brushes.Crimson, new Point(7, pos + 85));
            BillDGV.Rows.Clear();
            BillDGV.Refresh();
            /*pos = 100;*/
            GrdTotal = 0;
            TotalLbl.Text = "Rs" + GrdTotal;
            n = 0;
        }

        private void label6_Click(object sender, EventArgs e)
        {
            Login obj = new Login();
            obj.Show();
            this.Hide();
        }

        private void label2_Click(object sender, EventArgs e)
        {
            Homes obj = new Homes();
            obj.Show();
            this.Hide();
        }

        private void ProductsDGV_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

            int ind = e.RowIndex;
            DataGridViewRow selectedRows = ProductsDGV.Rows[ind];
            if (ind != -1)
            {
                PrNameTb.Text = selectedRows.Cells["Name"].Value.ToString();
                Stock = Convert.ToInt32(selectedRows.Cells["Quantity"].Value.ToString());
                PrPriceTb.Text = selectedRows.Cells["Price"].Value.ToString();
            }
            if (string.IsNullOrEmpty(PrNameTb.Text))
            {
                Key = 0;
            }
            else
            {
                Key = Convert.ToInt32(selectedRows.Cells["Id"].Value.ToString());
            }
        }

        private void label13_Click(object sender, EventArgs e)
        {

        }

        private bool InsertBill()
        {
            try
            {
                Bill newBill = new Bill() { CustomerId = int.Parse(cb_Customer.SelectedValue.ToString()), EmployeeId = 2, Date = DateTime.Now };
                if (_billService.Add(newBill))
                {
                    var addedBill = _billService.GetByCustomerIdAndDate(newBill.CustomerId, newBill.Date);
                    if (addedBill == null)
                    {
                        MessageBox.Show("Add Bill failed.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return false;
                    }
                    List<BillDetail> list = new List<BillDetail>();

                    foreach (DataGridViewRow row in BillDGV.Rows)
                    {
                        int productId = Convert.ToInt32(row.Cells["ID"].Value);
                        int quantity = Convert.ToInt32(row.Cells["Quantity"].Value);

                        var product = _productService.GetById(productId);

                        if (product != null)
                        {
                            BillDetail newBillDetail = new BillDetail
                            {
                                BillId = addedBill.Id,
                                Price = product.Price,
                                ProductId = productId,
                                Quantity = quantity,
                                Total = product.Price * quantity
                            };

                            list.Add(newBillDetail);
                        }
                    }
                    if (_billDetailService.AddRange(list))
                    {
                        MessageBox.Show("Add bill successfully.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        DisplayProduct();
                        DisplayTransactions();
                    }
                    else
                    {
                        MessageBox.Show("Bill is added fail!!");
                        return false;
                    }
                }
                else
                {
                    MessageBox.Show("Bill is added fail!!");
                    return false;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error during bill insertion: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            DisplayTransactions();
            return true;
        }
        String prodname;

        private void Billings_Load(object sender, EventArgs e)
        {
            DisplayProduct();
            DisplayTransactions();
        }
        private void CheckAndInscreaseExistingItemsInCart(int productId)
        {
            foreach (DataGridViewRow row in BillDGV.Rows)
            {

            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            BillDGV.Rows.Clear();
            GrdTotal = 0;
            TotalLbl.Text = "Rs" + GrdTotal;
        }
    }
}
