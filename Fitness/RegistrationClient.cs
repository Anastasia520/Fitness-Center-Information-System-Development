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
using Excel = Microsoft.Office.Interop.Excel;

namespace Fitness
{
    public partial class RegistrationClient : Form
    {
        public RegistrationClient()
        {
            InitializeComponent();
            //lblRegChange.Text = cmndLbl;
            //btnGo.Text = cmndBtn;
           
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
       
        public void btnGo_Click(object sender, EventArgs e)
        {
            sqlcon.Open();

            if (txtEmail.Text != "" && txtName.Text != "" && txtSurname.Text != "" && cmbSex.Text != "" && txtPassword.Text != "" && cmbPass.Text != "")
            {
                string cond = @"(\w+([-+.]\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*)";


                if (Regex.IsMatch(txtEmail.Text, cond))
                {
                    string query = @"insert into Login ( Email,Password) 
Values ('" + txtEmail.Text + "','" + txtPassword.Text + "')";
                    SqlCommand com = new SqlCommand(query, sqlcon);
                    SqlDataReader reader = com.ExecuteReader();
                    reader.Close();

                    query = @"insert into Client (Name_client, Surname_client, Birth_data_client, Start_data_client, Email_client,Sex_client) 
Values ('" + txtName.Text + "','" + txtSurname.Text + "','" + dtpBirth.Value.ToShortDateString() + "','" + dayNow.ToShortDateString() + "','" + txtEmail.Text + "','" + cmbSex.Text + "')";
                    com = new SqlCommand(query, sqlcon);
                    reader = com.ExecuteReader();
                    reader.Close();

                    query = @"select Id_client from Client where Name_client='" + txtName.Text + "'and Surname_client='" + txtSurname.Text
                        + "'and Birth_data_client='" + dtpBirth.Value.ToShortDateString() + "'and Start_data_client='" + dayNow.ToShortDateString()
                        + "'and Email_client='" + txtEmail.Text + "'and Sex_client='" + cmbSex.Text + "'";
                    com = new SqlCommand(query, sqlcon);
                    reader = com.ExecuteReader();
                    while (reader.Read()) id_client = (int)reader[0];
                    reader.Close();

                    query = @"select Count_classes from Passes where Name_pass='" + cmbPass.Text + "'";
                    com = new SqlCommand(query, sqlcon);
                    reader = com.ExecuteReader();
                    while (reader.Read()) countPass = (int)reader[0];
                    reader.Close();

                    query = @"insert into Client_pass (Client_id, Pass_name, Last_count) 
Values ('" + id_client + "','" + cmbPass.Text + "','" + countPass + "')";
                    com = new SqlCommand(query, sqlcon);
                    reader = com.ExecuteReader();
                    reader.Close();
                }


                else MessageBox.Show("Почта введена не корректно");
            }
            else
            {
                MessageBox.Show("Заполнены не все поля!");
            }
            
           
            sqlcon.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog() == DialogResult.Cancel)
                return;
            // получаем выбранный файл
            string filename = openFileDialog1.FileName;
            Excel.Application excelApp = new Excel.Application();
            excelApp.Visible = true;
            excelApp.Workbooks.Open(filename);
            int row = 1;
            List<string[]> maping = new List<string[]>();
            var lastCell = excelApp.Cells.SpecialCells(Excel.XlCellType.xlCellTypeLastCell);//1 ячейку
            Excel.Worksheet currentSheet = (Excel.Worksheet)excelApp.Workbooks[1].Worksheets[1];
            string[,] list = new string[lastCell.Column, lastCell.Row]; // массив значений с листа равен по размеру листу
            for (int i = 0; i < lastCell.Column; i++)
            {//по всем колонкам
                for (int j = 1; j < lastCell.Row; j++)
                { // по всем строкам
                    if (excelApp.Cells[j + 1, i + 1].Text != "")
                    {
                        maping.Add(new string[2]);
                        maping[maping.Count-1][1]=(excelApp.Cells[j + 1, i + 1].Text.ToString());//считываем текст в строку
                        maping[maping.Count - 1][0] = (excelApp.Cells[j, i + 1].Text.ToString());//считываем текст в строку
                    }
                    
                }

            }       
            foreach (string [] s in maping)
            {
                switch (s[0])
                {
                    case "Имя":
                        txtName.Text = s[1];
                        break;
                    case "Фамилия":
                        txtSurname.Text = s[1];
                        break;
                    case "Дата рождения":
                        dtpBirth.Text = s[1];
                        break;
                    case "Пол":
                        cmbSex.Text = s[1];
                        break;
                    case "Email":
                        txtEmail.Text = s[1];
                        break;
                    case "Пароль":
                        txtPassword.Text = s[1];
                        break;
                    case "Абонемент":
                        cmbPass.Text = s[1];
                        break;

                }
            }
            
            excelApp.Quit();
        }
    }
}
