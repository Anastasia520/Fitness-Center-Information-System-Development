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
    public partial class ChangeClient : Form
    {
        public ChangeClient()
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
        public void LoadClientData()
        {
            bool ok = false;
            sqlcon.Open();
            try
            {
                string query = @"select Client.Name_client, Client.Surname_client, Client.Birth_data_client, Client.Email_client, 
Client.Sex_client from Client where Id_client='" + Convert.ToInt32(txtId.Text) + "'";
                SqlCommand com = new SqlCommand(query, sqlcon);
                SqlDataReader reader = com.ExecuteReader();
                while (reader.Read())
                {
                    txtName.Text = reader[0].ToString();
                    txtSurname.Text = reader[1].ToString();
                    dtpBirth.Text = reader[2].ToString();
                    txtEmail.Text = reader[3].ToString();
                    cmbSex.Text = reader[4].ToString();
                    Email = txtEmail.Text;
                    ok = true;
                }
                reader.Close();

            }
            catch (Exception)
            {
                MessageBox.Show("Данный ID отсутсвует в базе данных, повторите попытку");
            }
            if (ok)
            {
                lblName.Visible = true;
                lblBirth.Visible = true;
                lblEmail.Visible = true;
                lblSex.Visible = true;
                lblSurname.Visible = true;
                lblPass.Visible = true;
                txtEmail.Visible = true;
                txtName.Visible = true;
                txtSurname.Visible = true;
                dtpBirth.Visible = true;
                btnGo.Visible = true;
                cmbPass.Visible = true;
                cmbSex.Visible = true;
               string query = @"select Client_pass.Pass_name ,Client_pass.Last_count from Client_pass where Client_id='" + Convert.ToInt32(txtId.Text) + "'";
               SqlCommand com = new SqlCommand(query, sqlcon);
              SqlDataReader  reader = com.ExecuteReader();
                while (reader.Read())
                {
                    cmbPass.Text = reader[0].ToString();
                    Pass = cmbPass.Text;
                    LastCount = (int)reader[1];



                }
            }
            sqlcon.Close();
        }
        public string Password = "";
        private void btnGo_Click(object sender, EventArgs e)
        {
            sqlcon.Open();
            if (txtEmail.Text != "" && txtName.Text != "" && txtSurname.Text != "" && cmbPass.Text != "" && cmbSex.Text != "" && dtpBirth.Text != "")
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

                        string query = @"update Client set Client.Name_client='" + txtName.Text + "', Client.Surname_client='" + txtSurname.Text +
                        "', Client.Birth_data_client='" + dtpBirth.Value.ToShortDateString()
                        + "', Client.Email_client='" + txtEmail.Text + "',Client.Sex_client='" + cmbSex.Text + "'where Id_client='" + txtId.Text + "'";
                        SqlCommand com = new SqlCommand(query, sqlcon);
                        SqlDataReader reader = com.ExecuteReader();
                        reader.Close();


                        quer = @"delete from  Login where Email='" + Email + "'";
                        co = new SqlCommand(quer, sqlcon);
                        reade = co.ExecuteReader();
                        reade.Close();
                    }

                    if (Pass != cmbPass.Text)
                    {
                        string quer = @"select Count_classes from Passes where Name_pass='" + cmbPass.Text + "'";
                        SqlCommand co = new SqlCommand(quer, sqlcon);
                        SqlDataReader reade = co.ExecuteReader();
                        while (reade.Read()) countPass = (int)reade[0];
                        reade.Close();

                        quer = @"update Client_pass set Pass_name ='" + cmbPass.Text + "',Last_count='" + (countPass + LastCount) + "'where Client_id='" + txtId.Text + "'";
                        co = new SqlCommand(quer, sqlcon);
                        reade = co.ExecuteReader();
                        reade.Close();
                        string query = @"update Client set Client.Name_client='" + txtName.Text + "', Client.Surname_client='" + txtSurname.Text +
                        "', Client.Birth_data_client='" + dtpBirth.Value.ToShortDateString()
                        + "', Client.Email_client='" + txtEmail.Text + "',Client.Sex_client='" + cmbSex.Text + "'where Id_client='" + txtId.Text + "'";
                        SqlCommand com = new SqlCommand(query, sqlcon);
                        SqlDataReader reader = com.ExecuteReader();
                        reader.Close();

                    }
                    else
                    {
                        string query = @"update Client set Client.Name_client='" + txtName.Text + "', Client.Surname_client='" + txtSurname.Text +
                        "', Client.Birth_data_client='" + dtpBirth.Value.ToShortDateString()
                        + "', Client.Email_client='" + txtEmail.Text + "',Client.Sex_client='" + cmbSex.Text + "'where Id_client='" + txtId.Text + "'";
                        SqlCommand com = new SqlCommand(query, sqlcon);
                        SqlDataReader reader = com.ExecuteReader();
                        reader.Close();
                    }
                }
                else MessageBox.Show("Формат почты невернен");
                
            }
            else MessageBox.Show("Заполнены не все поля");
            sqlcon.Close();

        }

        private void btnChange_Click(object sender, EventArgs e)
        {
            
                if (txtId.Text != "")
                    LoadClientData();
                else MessageBox.Show("Введите ID клиента");
           
        }

        private void ChangeClient_Load(object sender, EventArgs e)
        {

        }
    }
}
