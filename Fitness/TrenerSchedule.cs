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
    public partial class TrenerSchedule : Form
    {
        public SqlConnection sqlcon = new SqlConnection(@"Data Source=LENOVO-PC\MSSQLSERVEREXPRE;Initial Catalog=Fitness-club;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");

        public TrenerSchedule()
        {
            InitializeComponent();
            workerid = 6;//измеенить на ссылку к форме вххода
            sqlcon.Open();
            string query = @"Select Surname_worker from Workers where Id_worker='"+workerid+"'";
            SqlCommand com = new SqlCommand(query, sqlcon);
            SqlDataReader reader = com.ExecuteReader();
            while (reader.Read())
            {
                workerName = reader[0].ToString();
            }
            sqlcon.Close();
            LoadData();
            
        }
        public int workerid = 1;
        public string workerName = "";
        public void LoadData()
        {
           panelDescription.Visible = true;
            sqlcon.Open();

            ///1 week add trener's schedule
            DateTime dateNow = DateTime.Now; // 20.07.2015 18:30:25
            int ad = 0;
            string d = dateNow.ToString("ddd");
            string query = "";
            if (d == "Пн")
            {
                ad = 6;
                query = @"Select Class_data_worker.Classes_Name,
Class_data_worker.Last_count_places ,Class_data_worker.Date , Class_data_worker.Time, Class_data_worker.Data_time from Class_data_worker  
where Class_data_worker.Data_time between getdate() and dateadd(day,6,getdate()) and Class_data_worker.Workers_id='"+workerid+"' order by Class_data_worker.Data_time";
            }

            if (d == "Вт")
            {
                ad = 5;
                query = @"Select Class_data_worker.Classes_Name,
Class_data_worker.Last_count_places ,Class_data_worker.Date , Class_data_worker.Time, Class_data_worker.Data_time from Class_data_worker  
where Class_data_worker.Data_time between getdate() and dateadd(day,5,getdate()) and Class_data_worker.Workers_id='" + workerid + "' order by Class_data_worker.Data_time";
            }
            if (d == "Ср")
            {
                ad = 4;
                query = @"Select Class_data_worker.Classes_Name,
Class_data_worker.Last_count_places ,Class_data_worker.Date , Class_data_worker.Time, Class_data_worker.Data_time from Class_data_worker  
where Class_data_worker.Data_time between getdate() and dateadd(day,4,getdate()) and Class_data_worker.Workers_id='" + workerid + "' order by Class_data_worker.Data_time";
            }

            if (d == "Чт")
            {
                ad = 3;
                query = @"Select Class_data_worker.Classes_Name,
Class_data_worker.Last_count_places ,Class_data_worker.Date , Class_data_worker.Time, Class_data_worker.Data_time from Class_data_worker  
where Class_data_worker.Data_time between getdate() and dateadd(day,3,getdate()) and Class_data_worker.Workers_id='" + workerid + "' order by Class_data_worker.Data_time";
            }
            if (d == "Пт")
            {
                ad = 2;
                query = @"Select Class_data_worker.Classes_Name,
Class_data_worker.Last_count_places ,Class_data_worker.Date , Class_data_worker.Time, Class_data_worker.Data_time from Class_data_worker  
where Class_data_worker.Data_time between getdate() and dateadd(day,2,getdate()) and Class_data_worker.Workers_id='" + workerid + "' order by Class_data_worker.Data_time";
            }
            if (d == "Сб")
            {
                ad = 8;
                query = @"Select Class_data_worker.Classes_Name,
Class_data_worker.Last_count_places ,Class_data_worker.Date , Class_data_worker.Time, Class_data_worker.Data_time from Class_data_worker  
where Class_data_worker.Data_time between getdate() and dateadd(day,7,getdate()) and Class_data_worker.Workers_id='" + workerid + "' order by Class_data_worker.Data_time";
            }
            if (d == "Вс")
            {
                ad = 7;
                query = @"Select Class_data_worker.Classes_Name,
Class_data_worker.Last_count_places ,Class_data_worker.Date , Class_data_worker.Time, Class_data_worker.Data_time from Class_data_worker  
where Class_data_worker.Data_time between getdate() and dateadd(day,8,getdate()) and Class_data_worker.Workers_id='" + workerid + "' order by Class_data_worker.Data_time";
            }

            SqlCommand com = new SqlCommand(query, sqlcon);
           SqlDataReader reader = com.ExecuteReader();
            List<string[]> data = new List<string[]>();
            //getdate() вставить вместо даты и выводить с текущего дня
            //Class_data_worker.Data_time >= '2020-03-02'
            
            //getdate() вставить вместо даты и выводить с текущего дня
            //Class_data_worker.Data_time >= '2020-03-02'
            
           int i = 0;
          
           
            reader.Close();

            com = new SqlCommand(query, sqlcon);
            reader = com.ExecuteReader();
            while (reader.Read())
            {
                data.Add(new string[3]);

                data[data.Count - 1][0] = reader[0].ToString();//+ " Тренер: "+reader[1].ToString()+ " Мест: " +  reader[2].ToString();
                data[data.Count - 1][1] = reader[2].ToString().Substring(0, 10);//дата занятия
                data[data.Count - 1][2] = reader[3].ToString().Substring(0, 5);//время
            }

            dataGridView1.Rows.Clear();
            dataGridView2.Rows.Clear();

            i = 1;
            dataGridView1.ColumnCount = 6;
            dataGridView1.RowCount = 8;
            dataGridView2.ColumnCount = 6;
            dataGridView2.RowCount = 8;
            List<string> time = new List<string>(4);
            time.Add("08:00");
            time.Add("09:00");
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
                dataGridView2[0, j].Value = s;
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
                dataGridView1[i, j].Style.BackColor = Color.Green;
                //dataGridView1.Rows.Add(s);
            }

            reader.Close();
            //week2 add trener's schedule
            dateNow = dateNow.AddDays(ad);
            d = dateNow.ToString("ddd");

            if (ad == 2)
            {

                query = @"Declare @nextWeek datetime = dateadd(day,2, getdate());
Select Class_data_worker.Classes_Name,
Class_data_worker.Last_count_places ,Class_data_worker.Date , Class_data_worker.Time, Class_data_worker.Data_time from Class_data_worker
where Class_data_worker.Data_time between  @nextWeek and dateadd(day,6, @nextWeek) and Class_data_worker.Workers_id='" + workerid + "' order by Class_data_worker.Data_time";
            }

            if (ad == 3)
            {

                query = @"Declare @nextWeek datetime = dateadd(day,3, getdate());
Select Class_data_worker.Classes_Name,
Class_data_worker.Last_count_places ,Class_data_worker.Date , Class_data_worker.Time, Class_data_worker.Data_time from Class_data_worker
where Class_data_worker.Data_time between  @nextWeek and dateadd(day,6, @nextWeek) and Class_data_worker.Workers_id='" + workerid + "' order by Class_data_worker.Data_time";
            }
            if (ad == 4)
            {
                query = @"Declare @nextWeek datetime = dateadd(day,4, getdate());
Select Class_data_worker.Classes_Name,
Class_data_worker.Last_count_places ,Class_data_worker.Date , Class_data_worker.Time, Class_data_worker.Data_time from Class_data_worker
where Class_data_worker.Data_time between  @nextWeek and dateadd(day,6, @nextWeek) and Class_data_worker.Workers_id='" + workerid + "' order by Class_data_worker.Data_time";
            }

            if (ad == 5)
            {
                query = @"Declare @nextWeek datetime = dateadd(day,5, getdate());
Select Class_data_worker.Classes_Name,
Class_data_worker.Last_count_places ,Class_data_worker.Date , Class_data_worker.Time, Class_data_worker.Data_time from Class_data_worker
where Class_data_worker.Data_time between  @nextWeek and dateadd(day,6, @nextWeek) and Class_data_worker.Workers_id='" + workerid + "' order by Class_data_worker.Data_time";
            }
            if (ad == 6)
            {
                query = @"Declare @nextWeek datetime = dateadd(day,6, getdate());
Select Class_data_worker.Classes_Name,
Class_data_worker.Last_count_places ,Class_data_worker.Date , Class_data_worker.Time, Class_data_worker.Data_time from Class_data_worker
where Class_data_worker.Data_time between  @nextWeek and dateadd(day,6, @nextWeek) and Class_data_worker.Workers_id='" + workerid + "' order by Class_data_worker.Data_time";
            }
            if (ad == 7)
            {
                query = @"Declare @nextWeek datetime = dateadd(day,8, getdate());
Select Class_data_worker.Classes_Name,
Class_data_worker.Last_count_places ,Class_data_worker.Date , Class_data_worker.Time, Class_data_worker.Data_time from Class_data_worker
where Class_data_worker.Data_time between  @nextWeek and dateadd(day,6, @nextWeek) and Class_data_worker.Workers_id='" + workerid + "' order by Class_data_worker.Data_time";
            }
            if (ad == 8)
            {
                query = @"Declare @nextWeek datetime = dateadd(day,7, getdate());
Select Class_data_worker.Classes_Name,
Class_data_worker.Last_count_places ,Class_data_worker.Date , Class_data_worker.Time, Class_data_worker.Data_time from Class_data_worker
where Class_data_worker.Data_time between  @nextWeek and dateadd(day,6, @nextWeek) and Class_data_worker.Workers_id='" + workerid + "' order by Class_data_worker.Data_time";
            }
            com = new SqlCommand(query, sqlcon);
            reader = com.ExecuteReader();
            List<string[]> data2 = new List<string[]>();
            //getdate() вставить вместо даты и выводить с текущего дня
            //Class_data_worker.Data_time >= '2020-03-02'

            while (reader.Read())
            {
                data2.Add(new string[3]);

                data2[data2.Count - 1][0] = reader[0].ToString();//+ " Тренер: "+reader[1].ToString()+ " Мест: " +  reader[2].ToString();
                data2[data2.Count - 1][1] = reader[2].ToString().Substring(0, 10);//дата занятия
                data2[data2.Count - 1][2] = reader[3].ToString().Substring(0, 5);//время


            }
             dd = dateNow;
            for (int p = 0; p < 6; p++)
            {
                dd = dateNow.AddDays(p);
                string day = dd.ToString("ddd");
                if (day == "Пн") { Monday2.HeaderText = "Пн " + dd.ToShortDateString(); }
                if (day == "Вт") { Tuesday2.HeaderText = "Вт " + dd.ToShortDateString(); }
                if (day == "Ср") { Wednesday2.HeaderText = "Ср " + dd.ToShortDateString(); }
                if (day == "Чт") { Thursday2.HeaderText = "Чт " + dd.ToShortDateString(); }
                if (day == "Пт") { Friday2.HeaderText = "Пт " + dd.ToShortDateString(); }


            }
            foreach (string[] s in data2)
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
                if (day == "Пн") { i = 1; Monday2.HeaderText = "Пн " + s[1]; }
                if (day == "Вт") { i = 2; Tuesday2.HeaderText = "Вт " + s[1]; }
                if (day == "Ср") { i = 3; Wednesday2.HeaderText = "Ср " + s[1]; }
                if (day == "Чт") { i = 4; Thursday2.HeaderText = "Чт " + s[1]; }
                if (day == "Пт") { i = 5; Friday2.HeaderText = "Пт " + s[1]; }

               dataGridView2[i, j].Value = s[0];
                dataGridView2[i, j].Style.BackColor = Color.Green;
                //dataGridView1.Rows.Add(s);
            }
            reader.Close();
            sqlcon.Close();
            ///1 and 2 weeks add null schedule
            AddNullSchedule();
        }
        public void AddNullSchedule()
        {
            sqlcon.Open();

            ///1 week add trener's schedule
            DateTime dateNow = DateTime.Now; // 20.07.2015 18:30:25
            int ad = 0;
            string d = dateNow.ToString("ddd");
            string query = "";
            if (d == "Пн")
            {
                ad = 6;
                query = @"Select Class_data_worker.Classes_Name,
Class_data_worker.Last_count_places ,Class_data_worker.Date , Class_data_worker.Time, Class_data_worker.Data_time from Class_data_worker  
where Class_data_worker.Data_time between getdate() and dateadd(day,6,getdate()) and Class_data_worker.Workers_id is NULL order by Class_data_worker.Data_time";
            }

            if (d == "Вт")
            {
                ad = 5;
                query = @"Select Class_data_worker.Classes_Name,
Class_data_worker.Last_count_places ,Class_data_worker.Date , Class_data_worker.Time, Class_data_worker.Data_time from Class_data_worker  
where Class_data_worker.Data_time between getdate() and dateadd(day,5,getdate()) and Class_data_worker.Workers_id is NULL  order by Class_data_worker.Data_time";
            }
            if (d == "Ср")
            {
                ad = 4;
                query = @"Select Class_data_worker.Classes_Name,
Class_data_worker.Last_count_places ,Class_data_worker.Date , Class_data_worker.Time, Class_data_worker.Data_time from Class_data_worker  
where Class_data_worker.Data_time between getdate() and dateadd(day,4,getdate()) and Class_data_worker.Workers_id is NULL  order by Class_data_worker.Data_time";
            }

            if (d == "Чт")
            {
                ad = 3;
                query = @"Select Class_data_worker.Classes_Name,
Class_data_worker.Last_count_places ,Class_data_worker.Date , Class_data_worker.Time, Class_data_worker.Data_time from Class_data_worker  
where Class_data_worker.Data_time between getdate() and dateadd(day,3,getdate()) and Class_data_worker.Workers_id is NULL  order by Class_data_worker.Data_time";
            }
            if (d == "Пт")
            {
                ad = 2;
                query = @"Select Class_data_worker.Classes_Name,
Class_data_worker.Last_count_places ,Class_data_worker.Date , Class_data_worker.Time, Class_data_worker.Data_time from Class_data_worker  
where Class_data_worker.Data_time between getdate() and dateadd(day,2,getdate()) and Class_data_worker.Workers_id is NULL  order by Class_data_worker.Data_time";
            }
            if (d == "Сб")
            {
                ad = 8;
                query = @"Select Class_data_worker.Classes_Name,
Class_data_worker.Last_count_places ,Class_data_worker.Date , Class_data_worker.Time, Class_data_worker.Data_time from Class_data_worker  
where Class_data_worker.Data_time between getdate() and dateadd(day,7,getdate()) and Class_data_worker.Workers_id is NULL  order by Class_data_worker.Data_time";
            }
            if (d == "Вс")
            {
                ad = 7;
                query = @"Select Class_data_worker.Classes_Name,
Class_data_worker.Last_count_places ,Class_data_worker.Date , Class_data_worker.Time, Class_data_worker.Data_time from Class_data_worker  
where Class_data_worker.Data_time between getdate() and dateadd(day,8,getdate()) and Class_data_worker.Workers_id is NULL order by Class_data_worker.Data_time";
            }

            SqlCommand com = new SqlCommand(query, sqlcon);
            SqlDataReader reader = com.ExecuteReader();
            List<string[]> data = new List<string[]>();
            //getdate() вставить вместо даты и выводить с текущего дня
            //Class_data_worker.Data_time >= '2020-03-02'

            //getdate() вставить вместо даты и выводить с текущего дня
            //Class_data_worker.Data_time >= '2020-03-02'

            int i = 0;


            reader.Close();

            com = new SqlCommand(query, sqlcon);
            reader = com.ExecuteReader();
            while (reader.Read())
            {
                data.Add(new string[3]);

                data[data.Count - 1][0] = reader[0].ToString();//+ " Тренер: "+reader[1].ToString()+ " Мест: " +  reader[2].ToString();
                data[data.Count - 1][1] = reader[2].ToString().Substring(0, 10);//дата занятия
                data[data.Count - 1][2] = reader[3].ToString().Substring(0, 5);//время


            }

            int j = 0;
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
            //week2 add null schedule
            dateNow = dateNow.AddDays(ad);
            d = dateNow.ToString("ddd");

            if (ad == 2)
            {

                query = @"Declare @nextWeek datetime = dateadd(day,2, getdate());
Select Class_data_worker.Classes_Name,
Class_data_worker.Last_count_places ,Class_data_worker.Date , Class_data_worker.Time, Class_data_worker.Data_time from Class_data_worker
where Class_data_worker.Data_time between  @nextWeek and dateadd(day,6, @nextWeek) and Class_data_worker.Workers_id is NULL order by Class_data_worker.Data_time";
            }

            if (ad == 3)
            {

                query = @"Declare @nextWeek datetime = dateadd(day,3, getdate());
Select Class_data_worker.Classes_Name,
Class_data_worker.Last_count_places ,Class_data_worker.Date , Class_data_worker.Time, Class_data_worker.Data_time from Class_data_worker
where Class_data_worker.Data_time between  @nextWeek and dateadd(day,6, @nextWeek) and Class_data_worker.Workers_id is NULL order by Class_data_worker.Data_time";
            }
            if (ad == 4)
            {
                query = @"Declare @nextWeek datetime = dateadd(day,4, getdate());
Select Class_data_worker.Classes_Name,
Class_data_worker.Last_count_places ,Class_data_worker.Date , Class_data_worker.Time, Class_data_worker.Data_time from Class_data_worker
where Class_data_worker.Data_time between  @nextWeek and dateadd(day,6, @nextWeek) and Class_data_worker.Workers_id is NULL order by Class_data_worker.Data_time";
            }

            if (ad == 5)
            {
                query = @"Declare @nextWeek datetime = dateadd(day,5, getdate());
Select Class_data_worker.Classes_Name,
Class_data_worker.Last_count_places ,Class_data_worker.Date , Class_data_worker.Time, Class_data_worker.Data_time from Class_data_worker
where Class_data_worker.Data_time between  @nextWeek and dateadd(day,6, @nextWeek) and Class_data_worker.Workers_id is NULL order by Class_data_worker.Data_time";
            }
            if (ad == 6)
            {
                query = @"Declare @nextWeek datetime = dateadd(day,6, getdate());
Select Class_data_worker.Classes_Name,
Class_data_worker.Last_count_places ,Class_data_worker.Date , Class_data_worker.Time, Class_data_worker.Data_time from Class_data_worker
where Class_data_worker.Data_time between  @nextWeek and dateadd(day,6, @nextWeek) and Class_data_worker.Workers_id is NULL order by Class_data_worker.Data_time";
            }
            if (ad == 7)
            {
                query = @"Declare @nextWeek datetime = dateadd(day,8, getdate());
Select Class_data_worker.Classes_Name,
Class_data_worker.Last_count_places ,Class_data_worker.Date , Class_data_worker.Time, Class_data_worker.Data_time from Class_data_worker
where Class_data_worker.Data_time between  @nextWeek and dateadd(day,6, @nextWeek) and Class_data_worker.Workers_id is NULL  order by Class_data_worker.Data_time";
            }
            if (ad == 8)
            {
                query = @"Declare @nextWeek datetime = dateadd(day,7, getdate());
Select Class_data_worker.Classes_Name,
Class_data_worker.Last_count_places ,Class_data_worker.Date , Class_data_worker.Time, Class_data_worker.Data_time from Class_data_worker
where Class_data_worker.Data_time between  @nextWeek and dateadd(day,6, @nextWeek) and Class_data_worker.Workers_id is NULL  order by Class_data_worker.Data_time";
            }
            com = new SqlCommand(query, sqlcon);
            reader = com.ExecuteReader();
            List<string[]> data2 = new List<string[]>();
            //getdate() вставить вместо даты и выводить с текущего дня
            //Class_data_worker.Data_time >= '2020-03-02'

            while (reader.Read())
            {
                data2.Add(new string[3]);

                data2[data2.Count - 1][0] = reader[0].ToString();//+ " Тренер: "+reader[1].ToString()+ " Мест: " +  reader[2].ToString();
                data2[data2.Count - 1][1] = reader[2].ToString().Substring(0, 10);//дата занятия
                data2[data2.Count - 1][2] = reader[3].ToString().Substring(0, 5);//время


            }
            foreach (string[] s in data2)
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
                if (day == "Пн") { i = 1; Monday2.HeaderText = "Пн " + s[1]; }
                if (day == "Вт") { i = 2; Tuesday2.HeaderText = "Вт " + s[1]; }
                if (day == "Ср") { i = 3; Wednesday2.HeaderText = "Ср " + s[1]; }
                if (day == "Чт") { i = 4; Thursday2.HeaderText = "Чт " + s[1]; }
                if (day == "Пт") { i = 5; Friday2.HeaderText = "Пт " + s[1]; }

                dataGridView2[i, j].Value = s[0];
            }
            reader.Close();
            sqlcon.Close();
        }
        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            ClassDiscriptionLoad(dataGridView1);
        }

        private void dataGridView2_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            ClassDiscriptionLoad(dataGridView2);
        }
        public string datetimeid;
      public string timeid;
        public void ClassDiscriptionLoad(DataGridView d)
        {
           
            sqlcon.Open();
            lblName.Text = "Название";
            lblPutType.Text = "";
            lblPutComplexity.Text = "";
            //lblPutPlace.Text = "";
            //lblPutTrener.Text = "";
            lblChange.Text = "Описание тренировки:";
            //txtClassDiscrip.Visible = true;
            if (d.CurrentCell.Value != null)
            {
                int row = d.CurrentCell.RowIndex;
                switch (row)
                {
                    case 0:
                        timeid = "8:00";
                        break;
                    case 1:
                        timeid = "9:00";
                        break;
                    case 2:
                        timeid = "10:00";
                        break;
                    case 3:
                        timeid = "12:00";
                        break;
                    case 4:
                        timeid = "16:00";
                        break;
                    case 5:
                        timeid = "18:00";
                        break;
                    case 6:
                        timeid = "19:00";
                        break;
                    case 7:
                        timeid = "20:00";
                        break;
                }
                int col = d.CurrentCell.ColumnIndex;
               
                    if (d == dataGridView1)
                        switch (col)
                        {
                            case 1:
                                datetimeid = Monday.HeaderText.Substring(3);
                                break;
                            case 2:
                                datetimeid = Tuesday.HeaderText.Substring(3);
                                break;
                            case 3:
                                datetimeid = Wednesday.HeaderText.Substring(3);
                                break;
                            case 4:
                                datetimeid = Thursday.HeaderText.Substring(3);
                                break;
                            case 5:
                                datetimeid = Friday.HeaderText.Substring(3);
                                break;
                        }
                    else
                    {
                        switch (col)
                        {
                            case 1:
                                datetimeid = Monday2.HeaderText.Substring(3);
                                break;
                            case 2:
                                datetimeid = Tuesday2.HeaderText.Substring(3);
                                break;
                            case 3:
                                datetimeid = Wednesday2.HeaderText.Substring(3);
                                break;
                            case 4:
                                datetimeid = Thursday2.HeaderText.Substring(3);
                                break;
                            case 5:
                                datetimeid = Friday2.HeaderText.Substring(3);
                                break;
                        }
                    }
                
                    string idtimedate = datetimeid + " " + timeid + ":00";
                    string str = d.CurrentCell.Value.ToString();
                string query = @"Select Classes.Name_class, Classes.Type_class, Complexity.Complexity_discription ,
Class_data_worker.Last_count_places, Class_data_worker.Data_Time,  Class_data_worker.Time , Class_data_worker.Room_capacity from Classes  
inner join Complexity on Classes.Complexity_class=Complexity.Comlexity 
inner join Class_data_worker on Class_data_worker.Classes_Name=Classes.Name_class 
where Class_data_worker.Data_Time='" + idtimedate + "'";
                SqlCommand com = new SqlCommand(query, sqlcon);
                SqlDataReader reader = com.ExecuteReader();

                while (reader.Read())
                {
                    lblName.Text = reader[0].ToString();
                    lblPutType.Text = reader[1].ToString();
                    lblPutComplexity.Text = reader[2].ToString();
                    //lblPutPlace.Text = reader[4].ToString();
                    //TrenersId = reader[3].ToString();
                    //txtClassDiscrip.Text = reader[0].ToString() + " " + reader[1].ToString() + " " + reader[2].ToString() + " " + " Мест: " + reader[3].ToString();
                    datetimeid = reader[4].ToString();
                }
                reader.Close();

                if (d.CurrentCell.Style.BackColor == Color.Green)
                {
                    btnGo.Visible = true;
                    btnGo.Text = "Отказаться от тренировки";
                }
                else
                {
                    btnGo.Visible = true;
                    btnGo.Text = "Выбрать тренировку";
                }
            }          
            sqlcon.Close();
        }

        private void btnGo_Click(object sender, EventArgs e)
        {
            if (btnGo.Text == "Отказаться от тренировки")
            {
                sqlcon.Open();
                string query = @"Select Client_Class.Clients_id, Client_pass.Last_count from Client_Class
inner join Client_pass  on Client_pass.Client_id=Client_Class.Clients_id where Client_Class.Class_date_time = '" + datetimeid + "'";
                SqlCommand com = new SqlCommand(query, sqlcon);
                SqlDataReader reader = com.ExecuteReader();
                List<int[]> clientsId = new List<int[]>();
                int i = 0;
                while (reader.Read())
                {
                    clientsId.Add(new int[2]);

                    clientsId[clientsId.Count - 1][0] = (int)reader[0];//id
                    clientsId[clientsId.Count - 1][1] = (int)reader[1];//last pass count
                    clientsId[clientsId.Count - 1][1] += 1;
                }
                reader.Close();

                foreach (int[] s in clientsId)
                {
                    query = @"update Client_pass set Last_count = '" + s[1] + "'where Client_id='" + s[0] + "'";
                    com = new SqlCommand(query, sqlcon);
                    reader = com.ExecuteReader();
                    reader.Close();
                    query = @"delete from Client_Class where Class_date_time='" + datetimeid + "'and Clients_id='" + s[0] + "'";
                    com = new SqlCommand(query, sqlcon);
                    reader = com.ExecuteReader();
                    reader.Close();
                }

                query = @"update  Class_data_worker set Workers_id = NULL where Data_time='" + datetimeid + "'";
                com = new SqlCommand(query, sqlcon);
                reader = com.ExecuteReader();
                MessageBox.Show("Done!");

                reader.Close();
                sqlcon.Close();
                
            }
            else
            {
                sqlcon.Open();
                string query = @"update  Class_data_worker set Workers_id = '"+workerid+"'where Data_time='" + datetimeid + "'";
                SqlCommand com = new SqlCommand(query, sqlcon);
                SqlDataReader reader = com.ExecuteReader();
                reader.Close();
                sqlcon.Close();
            }
            LoadData();
        }

        private void pictureBox4_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void btnSupport_Click(object sender, EventArgs e)
        {
            Support s = new Support();
            s.ShowDialog();
        }
    }
}
