using EmployeeManagementSystem.Forms;
using EmployeeManagementSystem.Models;
using EmployManagementSystemAPIs.Services.SalaryInfoServices;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EmployeeManagementSystem
{
    public partial class SalaryInfoForm : Form
    {
        public SalaryInfoForm()
        {
            InitializeComponent();
        }
        public async void populateSalaryInfo()
        {
            SalaryInfoServices objSalaryInfo = new SalaryInfoServices();
            var salaryInfoList = await objSalaryInfo.GetAllSalaryInfo();
            basicGridView.DataSource = salaryInfoList;

        }

        private void SalaryInfoForm_Load(object sender, EventArgs e)
        {
            populateSalaryInfo();
        }

        private void label7_Click(object sender, EventArgs e)
        {

        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            txtBasicId.Clear();           
            txtBasicSalary.Clear();
            txtAllowance.Clear();
            txtBonus.Clear();
           
        }

        private async void btnSave_Click(object sender, EventArgs e)
        {
            SalaryInfoServices ECobj = new SalaryInfoServices();
            SalaryInfo salaryInfo = new SalaryInfo();
            salaryInfo.BasicId = Convert.ToInt32(txtBasicId.Text.Trim());
            salaryInfo.SalaryMonth = Convert.ToDateTime( dateTimePicker.Text.Trim());
            salaryInfo.BasicSalary = Convert.ToInt32(txtBasicSalary.Text.Trim());
            salaryInfo.Allowance = Convert.ToInt32(txtAllowance.Text.Trim());
            salaryInfo.Bonus = Convert.ToInt32(txtBonus.Text.Trim());

            int sum = salaryInfo.BasicSalary + salaryInfo.Allowance + salaryInfo.Bonus;
            salaryInfo.TotalSalary = sum;
            lblTotalSalary.Text = sum.ToString();

            int result = await ECobj.PostSalaryInfo(salaryInfo);
            if (result > 0)
            {
                MessageBox.Show("Employee Salary Information saved successfully", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("Error found.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            populateSalaryInfo();
        }

        private async void btnUpdate_Click(object sender, EventArgs e)
        {
            SalaryInfoServices SIobj = new SalaryInfoServices();
            SalaryInfo salaryInfo = new SalaryInfo();

            salaryInfo.Id = Convert.ToInt32(txtEmpId.Text.Trim());
            salaryInfo.BasicId = Convert.ToInt32(txtBasicId.Text.Trim());
            salaryInfo.SalaryMonth = Convert.ToDateTime(dateTimePicker.Text.Trim());
            salaryInfo.BasicSalary = Convert.ToInt32(txtBasicSalary.Text.Trim());
            salaryInfo.Allowance = Convert.ToInt32(txtAllowance.Text.Trim());
            salaryInfo.Bonus = Convert.ToInt32(txtBonus.Text.Trim());

            int sum = salaryInfo.BasicSalary + salaryInfo.Allowance + salaryInfo.Bonus;
            salaryInfo.TotalSalary = sum;
            lblTotalSalary.Text = sum.ToString();

            if (txtEmpId.Text.Trim().Length == 0)
            {
                MessageBox.Show("Please enter Employ Id", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            int result = await SIobj.UpdateSalaryInfo(salaryInfo);
            if (result > 0)
                MessageBox.Show("Employee Salary Information updated successfully", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
            else
                MessageBox.Show("Error found.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            populateSalaryInfo();
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private async void btnDelete_Click(object sender, EventArgs e)
        {
            if (txtEmpId.Text.Trim().Length == 0)
            {
                MessageBox.Show("Employee Not Selected", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            var confirmResult = MessageBox.Show("Are you sure to delete this Data ?",
                                     "Confirm Delete!!",
                                     MessageBoxButtons.YesNo);
            if (confirmResult == DialogResult.Yes)
            {

                SalaryInfoServices SIobj = new SalaryInfoServices();
                int id = Convert.ToInt32(txtEmpId.Text);
                int result = await SIobj.DeleteSalaryInfo(id);
                if (result > 0)
                    MessageBox.Show("Employee Salary Information deleted successfully", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                else
                    MessageBox.Show("Error found.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                populateSalaryInfo();
            }
        }

        private void btnnext1_Click(object sender, EventArgs e)
        {
            HolidayInfoForm objholidayInfo = new HolidayInfoForm();
            this.Hide();
            objholidayInfo.Show();

        }

        private void basicGridView_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
