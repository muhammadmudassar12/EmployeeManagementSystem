using EmployeeManagementSystem.Business_Logic;
using EmployeeManagementSystem.Models;
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
public partial class BasicInformation : Form
{
    public BasicInformation()
    {
        InitializeComponent();
    }
    public void populateBasicInfo()
    {
        BasicInfoCRUD objBasicInfo = new BasicInfoCRUD();
        var basicInfoList = objBasicInfo.GetAllBasicInfo();
        basicGridView.DataSource = basicInfoList;

    }
    private void radioButton2_CheckedChanged(object sender, EventArgs e)
    {

    }

    private void BasicInformation_Load(object sender, EventArgs e)
    {
        populateBasicInfo();
    }

    private void txtName_TextChanged(object sender, EventArgs e)
    {

    }

    private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
    {

    }

    private void btnSave_Click(object sender, EventArgs e)
    {
        //if (txtFname.Text.Trim().Length == 0)
        //{
        //    MessageBox.Show("First Name Cannot be Empty", "Error", MessageBoxButtons.OKCancel, MessageBoxIcon.Error);
        //    txtFname.Focus();
        //    return;
        //}
        //if (txtLname.Text.Trim().Length == 0)
        //{
        //    MessageBox.Show("Last Name Cannot be Empty", "Error", MessageBoxButtons.OKCancel, MessageBoxIcon.Error);
        //    txtLname.Focus();
        //    return;
        //}
        //if (txtEmail.Text.Trim().Length == 0)
        //{
        //    MessageBox.Show("Email Cannot be Empty", "Error", MessageBoxButtons.OKCancel, MessageBoxIcon.Error);
        //    txtEmail.Focus();
        //    return;
        //}
        //if (txtAddress.Text.Trim().Length == 0)
        //{
        //    MessageBox.Show("Address Cannot be Empty", "Error", MessageBoxButtons.OKCancel, MessageBoxIcon.Error);
        //    txtAddress.Focus();
        //    return;
        //}
        //if (rdoMale.Checked == false && rdoFemale.Checked == false)
        //{
        //    MessageBox.Show("Please Select Gander", "Error", MessageBoxButtons.OKCancel, MessageBoxIcon.Error);

        //    return;
        //}

        //if (cmbPosition.SelectedIndex < 0)
        //{
        //    MessageBox.Show(" Please Select Position", "Error", MessageBoxButtons.OKCancel, MessageBoxIcon.Error);
        //    txtAddress.Focus();
        //    return;
        //}
        BasicInfoCRUD BDobj = new BasicInfoCRUD();
        BasicInfo basicinfo = new BasicInfo();
        basicinfo.Name = txtName.Text.Trim();
        basicinfo.Email = txtEmail.Text.Trim();
        basicinfo.Address = txtAddress.Text.Trim();

        if (rdoFemale.Checked == true)
            basicinfo.Gender = "Female";
        else
            basicinfo.Gender = "Male";


        basicinfo.Position = cmbPosition.Text;


        int result = BDobj.SaveBasicInfo(basicinfo);
        if (result > 0)
        {
            MessageBox.Show("Employee Basic Information saved successfully", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
        else
        {
            MessageBox.Show("Error found.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
        populateBasicInfo();
    }

    private void btnRefresh_Click(object sender, EventArgs e)
    {
        txtName.Clear();
        txtAddress.Clear();
        rdoMale.Checked = false;
        rdoFemale.Checked = false;
        cmbPosition.SelectedValue = null;
    }

    private void btnUpdate_Click(object sender, EventArgs e)
    {
        BasicInfoCRUD BDobj = new BasicInfoCRUD();
        BasicInfo basicinfo = new BasicInfo();
        basicinfo.Id = Convert.ToInt32(txtEmpId.Text.Trim());
        basicinfo.Name = txtName.Text.Trim();
        basicinfo.Email = txtEmail.Text.Trim();
        basicinfo.Address = txtAddress.Text.Trim();
        basicinfo.Gender = Convert.ToString(rdoMale.Checked || rdoFemale.Checked);
        basicinfo.Position = cmbPosition.Text;
        if (txtEmpId.Text.Trim().Length == 0)
        {
            MessageBox.Show("Please enter details first", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            return;
        }

        int result = BDobj.UpdateBasicInfo(basicinfo);
        if (result > 0)
            MessageBox.Show("Employee Basic Information updated successfully", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
        else
            MessageBox.Show("Error found.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        populateBasicInfo();
    }

    private void btnDelete_Click(object sender, EventArgs e)
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

            BasicInfoCRUD BDobj = new BasicInfoCRUD();
            int id = Convert.ToInt32(txtEmpId.Text);
            int result = BDobj.DeleteBasicInfo(id);
            if (result > 0)
                MessageBox.Show("Employee Basic Information deleted successfully", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
            else
                MessageBox.Show("Error found.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            populateBasicInfo();
        }
    }
}
    class BasicInfoCRUD
    {
        public int SaveBasicInfo(BasicInfo basicInfo)
        {
            using (SqlConnection connection = new SqlConnection(DBConnection.dbConnectionString))
            {
                String query = $"insert into BasicInfo(Name, Email, Address, Gender, Position) " +
                                $"values('{basicInfo.Name}', '{basicInfo.Email}', '{basicInfo.Address}', '{basicInfo.Gender}', '{basicInfo.Position}')";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    if (connection.State == ConnectionState.Closed)
                        connection.Open();
                    int result = command.ExecuteNonQuery();
                    return result;
                }
            }
        }
        public BasicInfo GetBasicInfo(int Id)
        {
            BasicInfo basicInfo = null;
            using (SqlConnection connection = new SqlConnection(DBConnection.dbConnectionString))
            {
                string oString = $"Select * from BasicInfo where Id={Id}";
                SqlCommand oCmd = new SqlCommand(oString, connection);
                if (connection.State == ConnectionState.Closed)
                    connection.Open();
                using (SqlDataReader oReader = oCmd.ExecuteReader())
                {
                    if (oReader.HasRows)
                    {
                        basicInfo = new BasicInfo();
                        while (oReader.Read())
                        {
                            basicInfo.Id = Convert.ToInt32(oReader["Id"]);
                            basicInfo.Name = oReader["Name"].ToString();
                            basicInfo.Email = oReader["Email"].ToString();
                            basicInfo.Address = oReader["Address"].ToString();
                            basicInfo.Gender = oReader["Gender"].ToString();
                            basicInfo.Position = oReader["Position"].ToString();
                        }
                    }

                }
            }
            return basicInfo;
        }

        public List<BasicInfo> GetAllBasicInfo()
        {
            List<BasicInfo> listStudent = new List<BasicInfo>();
            using (SqlConnection connection = new SqlConnection(DBConnection.dbConnectionString))
            {
                string oString = $"Select * from BasicInfo ";
                SqlCommand oCmd = new SqlCommand(oString, connection);
                if (connection.State == ConnectionState.Closed)
                    connection.Open();
                using (SqlDataReader oReader = oCmd.ExecuteReader())
                {
                    if (oReader.HasRows)
                    {
                        BasicInfo basicInfo = null;
                        while (oReader.Read())
                        {
                            basicInfo = new BasicInfo();
                            basicInfo.Id = Convert.ToInt32(oReader["Id"]);
                            basicInfo.Name = oReader["Name"].ToString();
                            basicInfo.Email = oReader["Email"].ToString();
                            basicInfo.Address = oReader["Address"].ToString();
                            basicInfo.Gender = oReader["Gender"].ToString();
                            basicInfo.Position = oReader["Position"].ToString();

                            listStudent.Add(basicInfo);
                        }
                    }
                }
            }
            return listStudent;
        }
        public int UpdateBasicInfo(BasicInfo basicInfo)
        {
            using (SqlConnection connection = new SqlConnection(DBConnection.dbConnectionString))
            {
                String query = $" update BasicInfo set Name = '{basicInfo.Name}', Email ='{basicInfo.Email}', Address='{basicInfo.Address}',  Gender ='{basicInfo.Gender}' , Position ='{basicInfo.Position}' " +
                                    $"where Id = {basicInfo.Id}";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    if (connection.State == ConnectionState.Closed)
                        connection.Open();
                    int result = command.ExecuteNonQuery();

                    return result;

                }
            }
        }
        public int DeleteBasicInfo(int Id)
        {
            using (SqlConnection connection = new SqlConnection(DBConnection.dbConnectionString))
            {
                String query = $"Delete from BasicInfo where Id={Id}"; ;

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    if (connection.State == ConnectionState.Closed)
                        connection.Open();
                    int result = command.ExecuteNonQuery();

                    return result;

                }
            }
        }
    }