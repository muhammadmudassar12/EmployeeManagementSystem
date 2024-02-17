using EmployeeManagementSystem.Models;
using EmployManagementSystemAPIs.Services.HolidayInfoServices;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EmployeeManagementSystem.Forms
{
    public partial class HolidayInfoForm : Form
    {
        public HolidayInfoForm()
        {
            InitializeComponent();
        }
        public async void populateHolidayInfo()
        {
            HolidayInfoServices objholidaynfo = new HolidayInfoServices();
            var holidayInfoList = await objholidaynfo.GetAllHolidayInfo();
            basicGridView.DataSource = holidayInfoList;

        }
        private async void btnSave_Click(object sender, EventArgs e)
        {
            HolidayInfoServices HIobj = new HolidayInfoServices();
            HolidayInfo holidayInfo = new HolidayInfo();
            holidayInfo.HolidayMonth = Convert.ToDateTime(dateTimePicker1.Text.Trim());
            holidayInfo.Holidays = Convert.ToInt32(txtholidays.Text.Trim());
            holidayInfo.Leaves = Convert.ToInt32(txtLeaves.Text.Trim());

            int sum = holidayInfo.Holidays + holidayInfo.Leaves;
            holidayInfo.TotalHolidays = sum;
            lblTotalHolidays.Text = sum.ToString();

            holidayInfo.BasicId = Convert.ToInt32(txtBasicId.Text.Trim());
            int result = await HIobj.PostHolidayInfo(holidayInfo);
            if (result > 0)
            {
                MessageBox.Show("Employee Holidays Information saved successfully", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("Error found.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            populateHolidayInfo();
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

                HolidayInfoServices HIobj = new HolidayInfoServices();
                int id = Convert.ToInt32(txtEmpId.Text);
                int result = await HIobj.DeleteHolidayInfo(id);
                if (result > 0)
                    MessageBox.Show("Employee Holidays Information deleted successfully", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                else
                    MessageBox.Show("Error found.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                populateHolidayInfo();
            }
        }

        private async void btnUpdate_Click(object sender, EventArgs e)
        {
            HolidayInfoServices HIobj = new HolidayInfoServices();
            HolidayInfo holidayInfo = new HolidayInfo();
            holidayInfo.Id = Convert.ToInt32(txtEmpId.Text.Trim());
            holidayInfo.BasicId = Convert.ToInt32(txtBasicId.Text.Trim());
            holidayInfo.HolidayMonth = Convert.ToDateTime(dateTimePicker1.Text.Trim());
            holidayInfo.Holidays = Convert.ToInt32(txtholidays.Text.Trim());
            holidayInfo.Leaves = Convert.ToInt32(txtLeaves.Text.Trim());

            int sum = holidayInfo.Holidays + holidayInfo.Leaves;
            holidayInfo.TotalHolidays = sum;
            lblTotalHolidays.Text = sum.ToString();

            int result = await HIobj.UpdateHolidayInfo(holidayInfo);
            if (result > 0)
            {
                MessageBox.Show("Employee Holidays Information saved successfully", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("Error found.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            populateHolidayInfo();
        }

        private void HolidayInfoForm_Load(object sender, EventArgs e)
        {
            populateHolidayInfo();
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            txtholidays.Clear();
            txtLeaves.Clear();
            txtBasicId.Clear();
        }
    }
}
