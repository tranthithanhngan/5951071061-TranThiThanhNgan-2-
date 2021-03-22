using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient; 

namespace TranThiThanhNgan_5951071061
{
    public partial class Form1 : Form
    {
        

        public Form1()
        {
            InitializeComponent();
            GetStudentsRecord();

        }
        private void Form1_Load(object sender, EventArgs e)
        {
            GetStudentsRecord();
        }
        private void GetStudentsRecord()
        {
            SqlConnection con = new SqlConnection(@"Data Source=DESKTOP-EDKIR8O;Initial Catalog=DemoCRUD;Integrated Security=True");
            SqlCommand cmd = new SqlCommand("SELECT * FROM StudentsTb", con);
            DataTable dt = new DataTable();
            con.Open();
            SqlDataReader sdr = cmd.ExecuteReader();
            dt.Load(sdr);
            con.Close();
            StudentRecordData.DataSource = dt;
        }
        private bool IsValidData()
        {
            if(txtHName.Text == string.Empty
               || txtTName.Text == string.Empty
               || txtAddress.Text == string.Empty
               || string.IsNullOrEmpty(txtPhone.Text)
               || string.IsNullOrEmpty(txtRoll.Text))
            {
                MessageBox.Show("Có chổ chưa nhập dử liệu !!!", "Lỗi dử liệu", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            return true;
        }
        
        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            if(IsValidData())
            {
                SqlCommand cmd = new SqlCommand("INSERT INTO StudentsTb VALUES" + "(@Name, @FatherName,@RollNumber,@Address,@Mobile)", con);
                cmd.CommandType = CommandType.Text;
                cmd.Parameters.AddWithValue("@Name", txtHName.Text);
                cmd.Parameters.AddWithValue("@FatherName", txtTName.Text);
                cmd.Parameters.AddWithValue("@RollNumber", txtRoll.Text);
                cmd.Parameters.AddWithValue("@Address", txtAddress.Text);
                cmd.Parameters.AddWithValue("@Mobile", txtPhone.Text);

                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
                GetStudentsRecord();
            }    
        }

        public int StudenID;
        private void StudentRecordData_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            StudenID = Convert.ToInt32(StudentRecordData.Rows[0].Cells[0].Value);
            txtHName.Text = StudentRecordData.Rows[0].Cells[1].ToString();
            txtTName.Text = StudentRecordData.Rows[0].Cells[2].ToString();
            txtRoll.Text = StudentRecordData.Rows[0].Cells[3].ToString();
            txtAddress.Text = StudentRecordData.Rows[0].Cells[4].ToString();
            txtPhone.Text = StudentRecordData.Rows[0].Cells[5].ToString();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if( StudenID>0)
            {
                SqlCommand cmd = new SqlCommand("UPDATE StudentsTb SET" + "Name=@Name, FatherName=@FatherName," + "RollNumber = @RollNumber, Address = @Address," + "Mobile = @Mobile where StudentID=@ID", con);
                cmd.CommandType = CommandType.Text;
                cmd.Parameters.AddWithValue("@Name", txtHName.Text);
                cmd.Parameters.AddWithValue("@FatherName", txtTName.Text);
                cmd.Parameters.AddWithValue("@RollNumber", txtRoll.Text);
                cmd.Parameters.AddWithValue("@Address", txtAddress.Text);
                cmd.Parameters.AddWithValue("@Mobile", txtPhone.Text);
                cmd.Parameters.AddWithValue("@ID", this.StudenID);

                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
                GetStudentsRecord();
                ResetData();

            }    else
            {
                MessageBox.Show("Cập nhật bị lỗi!!!", "Lỗi!", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if(StudenID > 0)
            {
                SqlCommand cmd = new SqlCommand("DELETE FROM StudentsTb where StudentID=@ID", con);
                cmd.CommandType = CommandType.Text;
               
                cmd.Parameters.AddWithValue("@ID", this.StudenID);

                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
                GetStudentsRecord();
                ResetData();

            }    else
            {
                MessageBox.Show("Cập nhật bị lỗi!!!", "Lỗi!", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

     
    }
}
