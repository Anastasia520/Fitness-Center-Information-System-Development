using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Fitness
{
    public partial class TrenerRegistration : Form
    {
        public TrenerRegistration()
        {
            InitializeComponent();
        }
        public SqlConnection sqlcon = new SqlConnection(@"Data Source=LENOVO-PC\MSSQLSERVEREXPRE;Initial Catalog=Fitness-club;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");
        public DateTime dayNow = DateTime.Now;
        public int id_client = 0;
        public int countPass = 0;
        private void btnGo_Click(object sender, EventArgs e)
        {
            sqlcon.Open();

            if (txtEmail.Text != "" && txtName.Text != ""  && cmbSex.Text != "" && txtPassword.Text != "" && cmbJob.Text != "")
            {
                string cond = @"(\w+([-+.]\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*)";
                

               
                if (Regex.IsMatch(txtEmail.Text, cond))
                { string query = @"insert into Login ( Email,Password) 
Values ('" + txtEmail.Text + "','" + txtPassword.Text + "')";
                    SqlCommand com = new SqlCommand(query, sqlcon);
                    SqlDataReader reader = com.ExecuteReader();
                    reader.Close();
                    query = @"insert into Workers (Surname_worker, Data_birth_worker, Data_start_worker, Email_worker,Job_worker,Sex_worker) 
Values ('" + txtName.Text + "','" + dtpBirth.Value.ToShortDateString() + "','" + dayNow.ToShortDateString() + "','" + txtEmail.Text + "','" + cmbJob.Text + "','" + cmbSex.Text + "')";
                    com = new SqlCommand(query, sqlcon);
                    reader = com.ExecuteReader();
                    reader.Close(); }
                else MessageBox.Show("Формат почты неверен");
            }
            else
            {
                MessageBox.Show("Заполнены не все поля!");
            }
            sqlcon.Close();
        }

        private void lblRegChange_Click(object sender, EventArgs e)
        {

        }
    }
}
