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
    public partial class ChangeTrener : Form
    {
        public ChangeTrener()
        {
            InitializeComponent();
            lblId.Visible = true;
            txtId.Visible = true;
            btnChange.Visible = true;
        }
        public SqlConnection sqlcon = new SqlConnection(@"Data Source=LENOVO-PC\MSSQLSERVEREXPRE;Initial Catalog=Fitness-club;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");
        public DateTime dayNow = DateTime.Now;
        public int id_client = 0;
        public int countPass = 0;
        //public string cmndBtn 
        //public string cmndLbl 
        public string Pass = "";
        public string Email = "";
        public int LastCount = 0;
        public void LoadTrenerData()
        {

            sqlcon.Open();

            string query = @"select Surname_worker, Data_birth_worker, Email_worker,Job_worker,Sex_worker from Workers where Id_worker='" + Convert.ToInt32(txtId.Text) + "'";
            SqlCommand com = new SqlCommand(query, sqlcon);
            SqlDataReader reader = com.ExecuteReader();
            while (reader.Read())
            {
                txtName.Text = reader[0].ToString();
                dtpBirth.Text = reader[1].ToString();
                txtEmail.Text = reader[2].ToString();
                cmbSex.Text = reader[4].ToString();
                Email = txtEmail.Text;
                cmbJob.Text = reader[3].ToString();
            }
            lblName.Visible = true;
            lblBirth.Visible = true;
            lblEmail.Visible = true;
            lblSex.Visible = true;


            txtEmail.Visible = true;
            txtName.Visible = true;

            dtpBirth.Visible = true;
            btnGo.Visible = true;
            cmbJob.Visible = true;
            cmbSex.Visible = true;

            sqlcon.Close();
        }
        public string Password = "";
        private void btnChange_Click(object sender, EventArgs e)
        {
            if (txtId.Text != "")
                LoadTrenerData();
            else MessageBox.Show("Введите ID клиента");
        }

        private void btnGo_Click(object sender, EventArgs e)
        {
            sqlcon.Open();
            if (txtEmail.Text != "" && txtName.Text != "" && cmbJob.Text != "" && cmbSex.Text != "" && dtpBirth.Text != "")
            {
                string cond = @"(\w+([-+.]\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*)";


                if (Regex.IsMatch(txtEmail.Text, cond))
                {
                    if (Email != txtEmail.Text)
                    {
                        string quer = @"select Password from Login  where Email='" + Email + "'";
                        SqlCommand co = new SqlCommand(quer, sqlcon);
                        SqlDataReader reade = co.ExecuteReader();
                        while (reade.Read()) Password = reade[0].ToString();
                        reade.Close();

                        quer = @"insert into  Login (Email,Password) Values( '" + txtEmail.Text + "','" + Password + "')";
                        co = new SqlCommand(quer, sqlcon);
                        reade = co.ExecuteReader();
                        reade.Close();

                        string query = @"update Workers set Surname_worker='" + txtName.Text +
                        "', Data_birth_worker='" + dtpBirth.Value.ToShortDateString()
                        + "', Email_worker='" + txtEmail.Text + "',Sex_worker='" + cmbSex.Text + "',Job_worker='" + cmbJob.Text + "'where Id_worker='" + txtId.Text + "'";
                        SqlCommand com = new SqlCommand(query, sqlcon);
                        SqlDataReader reader = com.ExecuteReader();
                        reader.Close();


                        quer = @"delete from  Login where Email='" + Email + "'";
                        co = new SqlCommand(quer, sqlcon);
                        reade = co.ExecuteReader();
                        reade.Close();
                    }
                    else
                    {
                        string query = @"update Workers set Surname_worker='" + txtName.Text +
                        "', Data_birth_worker='" + dtpBirth.Value.ToShortDateString()
                        + "', Email_worker='" + txtEmail.Text + "',Sex_worker='" + cmbSex.Text + "',Job_worker='" + cmbJob.Text + "'where Id_worker='" + txtId.Text + "'";
                        SqlCommand com = new SqlCommand(query, sqlcon);
                        SqlDataReader reader = com.ExecuteReader();
                        reader.Close();
                    }
                }
                else MessageBox.Show("Формат почты неверен");
            } else MessageBox.Show("Не все поля заполнены");
                    sqlcon.Close();
                }
            }
        } 
