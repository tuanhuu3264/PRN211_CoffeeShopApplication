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
using Team4.GroupProject.BusinessServices.Interface;
using Team4.GroupProject.BusinessServices.Services;

namespace PetMS
{
    public partial class Login : Form
    {
        IEmployeeService _employeeService;
        public Login()
        {
            InitializeComponent();
            _employeeService = new EmployeeService();
        }


        public static string Employee = "";
        public static int employeeId =-1;
        private void LoginBtn_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txt_Phonenumber.Text.Trim()))
            {
                MessageBox.Show("The phone number is empty!", "Warning",  MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txt_Phonenumber.Focus();
            }
            if (string.IsNullOrEmpty(txt_Password.Text))
            {
                MessageBox.Show("The password is empty!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txt_Password.Focus();
            }
            var flag = _employeeService.CheckLogin(txt_Phonenumber.Text.Trim(), txt_Password.Text);
            if (flag)
            {
                var employee = _employeeService.GetByPhone(txt_Phonenumber.Text.Trim());
                Employee = employee.Name;
                employeeId=employee.Id;
                Homes homes = new Homes();
                homes.Show();
                this.Hide();
            }
            else
            {
                MessageBox.Show("The phone number/password is wrong!","Warning",MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
        }

        private void ExitBtn_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
