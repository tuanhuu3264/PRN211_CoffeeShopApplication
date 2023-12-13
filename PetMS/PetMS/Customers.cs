using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Team4.GroupProject.BusinessObjects.Models;
using Team4.GroupProject.BusinessServices.Interface;
using Team4.GroupProject.BusinessServices.Services;

namespace PetMS
{
    public partial class Customers : Form
    {
        ICustomerService _customerService;
        IBillService _billService;
        public Customers()
        {
            InitializeComponent();
            _customerService = new CustomerService();
            _billService = new BillService();
            DisplayCustomers();

            EmpNameLbl.Text = Login.Employee;
        }
        SqlConnection Con = new SqlConnection(@"Server=DESKTOP-PNA89UR;Database=PetShopp;Trusted_Connection=True;TrustServerCertificate=True");
        private void DisplayCustomers()
        {

            var customers = _customerService.GetAll();

            BindingSource source = new BindingSource();
            source.DataSource = customers;
            CustomerDGV.DataSource = source;

        }

        private void Clear()
        {
            Key = 0;
            CustNameTb.Text = "";
            CustAddTb.Text = "";
            CustPhoneTb.Text = "";
        }
        private void SaveBtn_Click(object sender, EventArgs e)
        {
            if (Key != 0)
            {
                MessageBox.Show("You must reset value by Reset button then you can create new data", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (CustNameTb.Text.Trim() == "")
            {
                MessageBox.Show("Missing Information Name.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (CustAddTb.Text.Trim() == "")
            {
                MessageBox.Show("Missing Information Address.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (CustPhoneTb.Text.Trim() == "")
            {
                MessageBox.Show("Missing Information Phone.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            var existedPhone = _customerService.GetByPhone(CustPhoneTb.Text.Trim());
            if (existedPhone != null)
            {
                MessageBox.Show("Dupplicatied phone!!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                Customer customer = new Customer()
                {
                    Name = CustNameTb.Text.Trim(),
                    Address = CustAddTb.Text.Trim(),
                    Phone = CustPhoneTb.Text.Trim()
                };
                if (_customerService.Add(customer))
                {
                    MessageBox.Show("Customer is added successfully", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    DisplayCustomers();
                    Clear();
                    return;
                }
                MessageBox.Show("Customer is added unsuccessfully", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);

            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message);
            }
        }
        int Key = 0;
        private void EditBtn_Click(object sender, EventArgs e)
        {
            if (Key == 0)
            {
                MessageBox.Show("You must choose a customer to updated", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (CustNameTb.Text.Trim() == "")
            {
                MessageBox.Show("Missing Information Name.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (CustAddTb.Text.Trim() == "")
            {
                MessageBox.Show("Missing Information Address.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (CustPhoneTb.Text.Trim() == "")
            {
                MessageBox.Show("Missing Information Phone.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            var existedPhone = _customerService.GetByPhone(CustPhoneTb.Text.Trim());
            if (existedPhone != null && existedPhone.Id != Key)
            {
                MessageBox.Show("Dupplicatied phone!!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                var customer = _customerService.GetById(Key);

                customer.Name = CustNameTb.Text.Trim();
                customer.Address = CustAddTb.Text.Trim();
                customer.Phone = CustPhoneTb.Text.Trim();
        

                if (_customerService.Update(customer))
                {
                    MessageBox.Show("Custloyee is updated successfully", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    DisplayCustomers();
                    Clear();
                    return;
                }
                MessageBox.Show("Employee is updated unsuccessfully", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);

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
                MessageBox.Show("Must select an customer to delete.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            else
            {
                var conditionBill = _billService.GetByCustomerId(Key);
                if (conditionBill.Any())
                {
                    MessageBox.Show("Cannot delete this customer that has bills.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                var deletedEmployee = _customerService.GetById(Key);
                if (deletedEmployee == null)
                {
                    MessageBox.Show("Must select an customer to delete.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                if (_customerService.Delete(Key))
                {
                    MessageBox.Show("Delete successfully.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    DisplayCustomers();
                    Clear();
                    return;
                }
                MessageBox.Show("Delete unsuccessfully.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void CustomerDGV_CellContentClick_1(object sender, DataGridViewCellEventArgs e)
        {
            int ind = e.RowIndex;
            DataGridViewRow selectedRows = CustomerDGV.Rows[ind];
            if (ind != -1)
            {
                CustNameTb.Text = selectedRows.Cells["Name"].Value.ToString();
                CustAddTb.Text = selectedRows.Cells["Address"].Value.ToString();
                CustPhoneTb.Text = selectedRows.Cells["Phone"].Value.ToString();
            }
            if (CustNameTb.Text == "")
            {
                Key = 0;
            }
            else
            {
                Key = Convert.ToInt32(selectedRows.Cells["Id"].Value.ToString());
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

        private void txt_Search_TextChanged(object sender, EventArgs e)
        {
            string keyword = txt_Search.Text.Trim();
            if (string.IsNullOrEmpty(keyword))
            {
                DisplayCustomers();
                return;
            }
            var customers = _customerService.Search(keyword);
            BindingSource source = new BindingSource();
            source.DataSource = customers;
            CustomerDGV.DataSource = source;
        }

        private void Customers_Load(object sender, EventArgs e)
        {
            DisplayCustomers();
        }

        private void btn_Reset_Click(object sender, EventArgs e)
        {
            Clear();
        }
    }
}
