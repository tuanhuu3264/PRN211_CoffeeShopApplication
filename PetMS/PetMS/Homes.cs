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
    public partial class Homes : Form
    {
        IProductService _productService;
        public Homes()
        {
            _productService = new ProductService();
            InitializeComponent();
            EmpNameLbl.Text = Login.Employee;
            CountsDogs();
            CountsCats();
            CountsBirds();
        }
        

        private void CountsDogs()
        {
            var dogs = _productService.GetByCategoryId(1);
            
            DogsLbl.Text = dogs.Count().ToString();
         
        }
        private void CountsCats()
        {
            var cats = _productService.GetByCategoryId(2);
            CatsLbl.Text = cats.Count().ToString();
        }
        private void CountsBirds()
        {
            var birds = _productService.GetByCategoryId(5);
            BirdsLbl.Text = birds.Count().ToString();
 
        }
        private void DogsLbl_Click(object sender, EventArgs e)
        {

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
    }
}
