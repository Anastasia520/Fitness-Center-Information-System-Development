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

namespace Fitness
{
    public partial class StartTablecs : Form
    {
        public StartTablecs()
        {
            InitializeComponent();
            LoadData();
        }
        public SqlConnection sqlcon = new SqlConnection(@"Data Source=LENOVO-PC\MSSQLSERVEREXPRE;Initial Catalog=Fitness-club;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");

        public void LoadData()
        {

            sqlcon.Open();
            ///1 week
            DateTime dateNow = DateTime.Now; // 20.07.2015 18:30:25
            int ad = 0;
            string d = dateNow.ToString("ddd");
            string query = "";
            if (d == "Пн")
            {
                ad = 6;
                query = @"Select Class_data_worker.Classes_Name, Workers.Surname_worker ,
Class_data_worker.Last_count_places ,Class_data_worker.Date , Class_data_worker.Time from Class_data_worker  
inner join Workers on Class_data_worker.Workers_id=Workers.Id_worker where Class_data_worker.Data_time between getdate() and dateadd(day,6,getdate())  order by Class_data_worker.Data_time";
            }

            if (d == "Вт")
            {
                ad = 5;
                query = @"Select Class_data_worker.Classes_Name, Workers.Surname_worker ,
Class_data_worker.Last_count_places ,Class_data_worker.Date , Class_data_worker.Time from Class_data_worker  
inner join Workers on Class_data_worker.Workers_id=Workers.Id_worker where Class_data_worker.Data_time between getdate() and dateadd(day,5,getdate())  order by Class_data_worker.Data_time";
            }
            if (d == "Ср")
            {
                ad = 4; query = @"Select Class_data_worker.Classes_Name, Workers.Surname_worker ,
Class_data_worker.Last_count_places ,Class_data_worker.Date , Class_data_worker.Time from Class_data_worker  
inner join Workers on Class_data_worker.Workers_id=Workers.Id_worker where Class_data_worker.Data_time between getdate() and dateadd(day,4,getdate())  order by Class_data_worker.Data_time";
            }

            if (d == "Чт")
            {
                ad = 3;
                query = @"Select Class_data_worker.Classes_Name, Workers.Surname_worker ,
Class_data_worker.Last_count_places ,Class_data_worker.Date , Class_data_worker.Time from Class_data_worker  
inner join Workers on Class_data_worker.Workers_id=Workers.Id_worker where Class_data_worker.Data_time between getdate() and dateadd(day,3,getdate())  order by Class_data_worker.Data_time";
            }
            if (d == "Пт")
            {
                ad = 2;
                query = @"Select Class_data_worker.Classes_Name, Workers.Surname_worker ,
Class_data_worker.Last_count_places ,Class_data_worker.Date , Class_data_worker.Time from Class_data_worker  
inner join Workers on Class_data_worker.Workers_id=Workers.Id_worker where Class_data_worker.Data_time between getdate() and dateadd(day,2,getdate())  order by Class_data_worker.Data_time";
            }
            if (d == "Сб")
            {
                ad = 8;
                query = @"Select Class_data_worker.Classes_Name, Workers.Surname_worker ,
Class_data_worker.Last_count_places ,Class_data_worker.Date , Class_data_worker.Time from Class_data_worker  
inner join Workers on Class_data_worker.Workers_id=Workers.Id_worker where Class_data_worker.Data_time between getdate() and dateadd(day,7,getdate())  order by Class_data_worker.Data_time";
            }
            if (d == "Вс")
            {
                ad = 7;
                query = @"Select Class_data_worker.Classes_Name, Workers.Surname_worker ,
Class_data_worker.Last_count_places ,Class_data_worker.Date , Class_data_worker.Time from Class_data_worker  
inner join Workers on Class_data_worker.Workers_id=Workers.Id_worker where Class_data_worker.Data_time between getdate() and dateadd(day,8,getdate())  order by Class_data_worker.Data_time";
            }

            SqlCommand com = new SqlCommand(query, sqlcon);
            SqlDataReader reader = com.ExecuteReader();
            List<string[]> data = new List<string[]>();
            //getdate() вставить вместо даты и выводить с текущего дня
            //Class_data_worker.Data_time >= '2020-03-02'

            while (reader.Read())
            {
                data.Add(new string[3]);

                data[data.Count - 1][0] = reader[0].ToString();//+ " Тренер: "+reader[1].ToString()+ " Мест: " +  reader[2].ToString();
                data[data.Count - 1][1] = reader[3].ToString().Substring(0, 10);//дата занятия
                data[data.Count - 1][2] = reader[4].ToString().Substring(0, 5);//время
            }


            dataGridView1.Rows.Clear();



            dataGridView1.ColumnCount = 6;
            dataGridView1.RowCount = 8;

            List<string> time = new List<string>(4);
            time.Add("08:00");
            time.Add("09:00");//стр 0
            time.Add("10:00");
            time.Add("12:00");
            time.Add("16:00");
            time.Add("18:00");
            time.Add("19:00");
            time.Add("20:00");
            int j = 0;

            foreach (string s in time)
            {
                dataGridView1[0, j].Value = s;

                j++;
            }
            DateTime dd = dateNow;
            for (int p = 0; p < ad; p++)
            {
                dd = dateNow.AddDays(p);
                string day = dd.ToString("ddd");
                if (day == "Пн") { Monday.HeaderText = "Пн " + dd.ToShortDateString(); }
                if (day == "Вт") { Tuesday.HeaderText = "Вт " + dd.ToShortDateString(); }
                if (day == "Ср") { Wednesday.HeaderText = "Ср " + dd.ToShortDateString(); }
                if (day == "Чт") { Thursday.HeaderText = "Чт " + dd.ToShortDateString(); }
                if (day == "Пт") { Friday.HeaderText = "Пт " + dd.ToShortDateString(); }


            }
            int i = 1;
            foreach (string[] s in data)
            {
                if (s[2] == "08:00") j = 0;
                if (s[2] == "09:00") j = 1;
                if (s[2] == "10:00") j = 2;
                if (s[2] == "12:00") j = 3;
                if (s[2] == "16:00") j = 4;
                if (s[2] == "18:00") j = 5;
                if (s[2] == "19:00") j = 6;
                if (s[2] == "20:00") j = 7;
                DateTime dt = Convert.ToDateTime(s[1]);
                string day = dt.ToString("ddd");
                if (day == "Пн") { i = 1; Monday.HeaderText = "Пн " + s[1]; }
                if (day == "Вт") { i = 2; Tuesday.HeaderText = "Вт " + s[1]; }
                if (day == "Ср") { i = 3; Wednesday.HeaderText = "Ср " + s[1]; }
                if (day == "Чт") { i = 4; Thursday.HeaderText = "Чт " + s[1]; }
                if (day == "Пт") { i = 5; Friday.HeaderText = "Пт " + s[1]; }

                dataGridView1[i, j].Value = s[0];

            }


            reader.Close();
            sqlcon.Close();

        }

        private void pictureBox4_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void panelAbout_Paint(object sender, PaintEventArgs e)
        {

        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void panelExit_Paint(object sender, PaintEventArgs e)
        {

        }

        private void pictureBox8_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void lblLogin_Click(object sender, EventArgs e)
        {
            LoginForm lf = new LoginForm();
            lf.ShowDialog();
            if (lf.cmnd==1)
            this.Hide();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            AboutUs au = new AboutUs();
            au.ShowDialog();
        }
    }
}
