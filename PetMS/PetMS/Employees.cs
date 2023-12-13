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
    public partial class Employees : Form
    {
        IEmployeeService _employeeService;
        IBillService _billService;
        public Employees()
        {
            InitializeComponent();
            _employeeService = new EmployeeService();
            _billService = new BillService();
            DisplayEmployees();

            EmpNameLbl.Text = Login.Employee;
        }

        SqlConnection Con = new SqlConnection(@"Server=DESKTOP-PNA89UR;Database=PetShopp;Trusted_Connection=True;TrustServerCertificate=True");


        private void DisplayEmployees()
        {
            var employees = _employeeService.GetAll();
            BindingSource source = new BindingSource();
            source.DataSource = employees;
            EmployeesDGV.DataSource = source;
            Con.Close();
        }
        private void Clear()
        {
            Key = 0;
            EmpNameTb.Text = "";
            EmpAddTb.Text = "";
            EmpPhoneTb.Text = "";
            PasswordTb.Text = "";
        }
        int Key = 0;

        private void Savebtn_Click(object sender, EventArgs e)
        {
            if (Key != 0)
            {
                MessageBox.Show("You must reset value by Reset button then you can create new data", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (EmpNameTb.Text.Trim() == "")
            {
                MessageBox.Show("Missing Information Name.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (EmpAddTb.Text.Trim() == "")
            {
                MessageBox.Show("Missing Information Address.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (EmpPhoneTb.Text.Trim() == "")
            {
                MessageBox.Show("Missing Information Phone.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            var existedPhone = _employeeService.GetByPhone(EmpPhoneTb.Text.Trim());
            if (existedPhone != null)
            {
                MessageBox.Show("Dupplicatied phone!!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                Employee employee = new Employee()
                {
                    Name = EmpNameTb.Text.Trim(),
                    Address = EmpAddTb.Text.Trim(),
                    Phone = EmpPhoneTb.Text.Trim(),
                    Password = PasswordTb.Text
                };
                if (_employeeService.Add(employee))
                {
                    MessageBox.Show("Employee is added successfully", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    DisplayEmployees();
                    Clear();
                    return;
                }
                MessageBox.Show("Employee is added unsuccessfully", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);

            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message);
            }


        }

        private void Editbtn_Click(object sender, EventArgs e)
        {
            if (Key == 0)
            {
                MessageBox.Show("You must choose a employee to updated", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (EmpNameTb.Text.Trim() == "")
            {
                MessageBox.Show("Missing Information Name.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (EmpAddTb.Text.Trim() == "")
            {
                MessageBox.Show("Missing Information Address.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (EmpPhoneTb.Text.Trim() == "")
            {
                MessageBox.Show("Missing Information Phone.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            var existedPhone = _employeeService.GetByPhone(EmpPhoneTb.Text.Trim());
            if (existedPhone != null && existedPhone.Id != Key)
            {
                MessageBox.Show("Dupplicatied phone!!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                var employee = _employeeService.GetById(Key);

                employee.Name = EmpNameTb.Text.Trim();
                employee.Address = EmpAddTb.Text.Trim();
                employee.Phone = EmpPhoneTb.Text.Trim();
                employee.Password = PasswordTb.Text;

                if (_employeeService.Update(employee))
                {
                    MessageBox.Show("Employee is updated successfully", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    DisplayEmployees();
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

        private void Deletebtn_Click(object sender, EventArgs e)
        {
            if (Key == 0)
            {
                MessageBox.Show("Must select an employee to delete.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            else
            {
                var conditionBill = _billService.GetByEmployeeId(Key);
                if (conditionBill.Any())
                {
                    MessageBox.Show("Cannot delete this employee that has bills.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                var deletedEmployee = _employeeService.GetById(Key);
                if (deletedEmployee == null)
                {
                    MessageBox.Show("Must select an employee to delete.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                if (_employeeService.Delete(Key))
                {
                    MessageBox.Show("Delete successfully.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    DisplayEmployees();
                    Clear();
                    return;
                }
                MessageBox.Show("Delete unsuccessfully.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void EmployeesDGV_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

            int ind = e.RowIndex;
            DataGridViewRow selectedRows = EmployeesDGV.Rows[ind];
            if (ind != -1)
            {
                EmpNameTb.Text = selectedRows.Cells[1].Value.ToString();
                EmpAddTb.Text = selectedRows.Cells[2].Value.ToString();
                EmpPhoneTb.Text = selectedRows.Cells[3].Value.ToString();
                PasswordTb.Text = selectedRows.Cells[4].Value.ToString();
            }
            if (EmpNameTb.Text == "")
            {
                Key = 0;
            }
            else
            {
                Key = Convert.ToInt32(selectedRows.Cells[0].Value.ToString());
            }
        }

        private void label4_Click(object sender, EventArgs e)
        {
            Customers obj = new Customers();
            obj.Show();
            this.Hide();
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

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            string keywork = txt_Search.Text.Trim().ToLower();
            if (string.IsNullOrEmpty(keywork))
            {
                DisplayEmployees();
                return;
            }
            var employees = _employeeService.Search(keywork);
            BindingSource source = new BindingSource();
            source.DataSource = employees;
            EmployeesDGV.DataSource = source;

        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void Employees_Load(object sender, EventArgs e)
        {
            DisplayEmployees();
        }

        private void btn_Reset_Click(object sender, EventArgs e)
        {
            Clear();
        }
    }
}
