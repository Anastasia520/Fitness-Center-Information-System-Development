using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Fitness
{
    public partial class ManagerSchedule : Form
    {
        public SqlConnection sqlcon = new SqlConnection(@"Data Source=LENOVO-PC\MSSQLSERVEREXPRE;Initial Catalog=Fitness-club;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");

        public ManagerSchedule()
        {
            InitializeComponent();
            LoadData();
        }
        public string datetimeid;
        public string timeid;
        public string TrenersName;
        public string TrenersId;
        public string ClassName;
        public int Kol;
        public int LastKol;

        public void   LoadData()
        {
            panelDescription.Visible = true;
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

            SqlCommand  com = new SqlCommand(query, sqlcon);
          SqlDataReader  reader = com.ExecuteReader();
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
            dataGridView2.Rows.Clear();

            
            dataGridView1.ColumnCount = 6;
            dataGridView1.RowCount = 8;
            dataGridView2.ColumnCount = 6;
            dataGridView2.RowCount = 8;
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
                dataGridView2[0, j].Value = s;
                j++;
            }
            DateTime dd = dateNow;
           for ( int p=0; p< ad; p++)
            {
                dd = dateNow.AddDays(p);
                string day = dd.ToString("ddd");
                if (day == "Пн") {  Monday.HeaderText = "Пн " + dd.ToShortDateString(); }
                if (day == "Вт") {  Tuesday.HeaderText = "Вт " + dd.ToShortDateString(); }
                if (day == "Ср") {  Wednesday.HeaderText = "Ср " + dd.ToShortDateString(); }
                if (day == "Чт") {  Thursday.HeaderText = "Чт " + dd.ToShortDateString(); }
                if (day == "Пт") { Friday.HeaderText = "Пт " + dd.ToShortDateString(); }
                

            }
               int  i = 1;
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
                if (day == "Вт")  { i = 2; Tuesday.HeaderText = "Вт " + s[1];  } 
                if (day == "Ср")  { i = 3; Wednesday.HeaderText = "Ср " + s[1];  } 
                if (day == "Чт")  { i = 4; Thursday.HeaderText = "Чт " + s[1]; } 
                if (day == "Пт")  { i = 5; Friday.HeaderText = "Пт " + s[1];  }

                dataGridView1[i, j].Value = s[0];
               
                //dataGridView1.Rows.Add(s);
            }

           
            reader.Close();
            AddNullTreners(1);

            dateNow = dateNow.AddDays(ad);
          d = dateNow.ToString("ddd");
           
            if (ad == 2)
            {
              
                query = @"Declare @nextWeek datetime = dateadd(day,2, getdate());
Select Class_data_worker.Classes_Name, Workers.Surname_worker ,
Class_data_worker.Last_count_places ,Class_data_worker.Date , Class_data_worker.Time from Class_data_worker  
inner join Workers on Class_data_worker.Workers_id=Workers.Id_worker where Class_data_worker.Data_time between @nextWeek and dateadd(day,6, @nextWeek)  order by Class_data_worker.Data_time";
            }

            if (ad == 3)
            {
                
                query = @"Declare @nextWeek datetime = dateadd(day,3, getdate());
Select Class_data_worker.Classes_Name, Workers.Surname_worker ,
Class_data_worker.Last_count_places ,Class_data_worker.Date , Class_data_worker.Time from Class_data_worker  
inner join Workers on Class_data_worker.Workers_id=Workers.Id_worker where Class_data_worker.Data_time between  @nextWeek and dateadd(day,6, @nextWeek)  order by Class_data_worker.Data_time";
            }
            if (ad == 4)
            {
                query = @"Declare @nextWeek datetime = dateadd(day,4, getdate());
Select Class_data_worker.Classes_Name, Workers.Surname_worker ,
Class_data_worker.Last_count_places ,Class_data_worker.Date , Class_data_worker.Time from Class_data_worker  
inner join Workers on Class_data_worker.Workers_id=Workers.Id_worker where Class_data_worker.Data_time between  @nextWeek and dateadd(day,6, @nextWeek)   order by Class_data_worker.Data_time";
            }

            if (ad == 5)
            {
                query = @"Declare @nextWeek datetime = dateadd(day,5, getdate());
Select Class_data_worker.Classes_Name, Workers.Surname_worker ,
Class_data_worker.Last_count_places ,Class_data_worker.Date , Class_data_worker.Time from Class_data_worker  
inner join Workers on Class_data_worker.Workers_id=Workers.Id_worker where Class_data_worker.Data_time between  @nextWeek and dateadd(day,6, @nextWeek)   order by Class_data_worker.Data_time";
            }
            if (ad == 6)
            {
                query = @"Declare @nextWeek datetime = dateadd(day,6, getdate());
Select Class_data_worker.Classes_Name, Workers.Surname_worker ,
Class_data_worker.Last_count_places ,Class_data_worker.Date , Class_data_worker.Time from Class_data_worker  
inner join Workers on Class_data_worker.Workers_id=Workers.Id_worker where Class_data_worker.Data_time between  @nextWeek and dateadd(day,6, @nextWeek)  order by Class_data_worker.Data_time";
            }
            if (ad == 7)
            {
                query = @"Declare @nextWeek datetime = dateadd(day,8, getdate());
Select Class_data_worker.Classes_Name, Workers.Surname_worker ,
Class_data_worker.Last_count_places ,Class_data_worker.Date , Class_data_worker.Time from Class_data_worker  
inner join Workers on Class_data_worker.Workers_id=Workers.Id_worker where Class_data_worker.Data_time between  @nextWeek and dateadd(day,6, @nextWeek)  order by Class_data_worker.Data_time";
            }
            if (ad == 8)
            {
                query = @"Declare @nextWeek datetime = dateadd(day,7, getdate());
Select Class_data_worker.Classes_Name, Workers.Surname_worker ,
Class_data_worker.Last_count_places ,Class_data_worker.Date , Class_data_worker.Time from Class_data_worker  
inner join Workers on Class_data_worker.Workers_id=Workers.Id_worker where Class_data_worker.Data_time between  @nextWeek and dateadd(day,6, @nextWeek)   order by Class_data_worker.Data_time";
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
                data2[data2.Count - 1][1] = reader[3].ToString().Substring(0, 10);//дата занятия
                data2[data2.Count - 1][2] = reader[4].ToString().Substring(0, 5);//время


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

                //dataGridView1.Rows.Add(s);
            }
            reader.Close();
            AddTo2Grid(ad);

           
            sqlcon.Close();
        }


        public void AddNullTreners(int k)
        {
            DateTime dateNow = DateTime.Now;
            if (k == 1)
            {
                 dateNow = DateTime.Now; // 20.07.2015 18:30:25
                int ad = 0;
                string d = dateNow.ToString("ddd");
                string query = "";
                if (d == "Пн")
                {
                    ad = 6;
                    query = @"Select Class_data_worker.Classes_Name ,
Class_data_worker.Last_count_places ,Class_data_worker.Date , Class_data_worker.Time from Class_data_worker  
 where Class_data_worker.Data_time between getdate() and dateadd(day,6,getdate())  order by Class_data_worker.Data_time";
                }

                if (d == "Вт")
                {
                    ad = 5;
                    query = @"Select Class_data_worker.Classes_Name,
Class_data_worker.Last_count_places ,Class_data_worker.Date , Class_data_worker.Time from Class_data_worker  
 where Class_data_worker.Data_time between getdate() and dateadd(day,5,getdate())  order by Class_data_worker.Data_time";
                }
                if (d == "Ср")
                {
                    ad = 4; query = @"Select Class_data_worker.Classes_Name, 
Class_data_worker.Last_count_places ,Class_data_worker.Date , Class_data_worker.Time from Class_data_worker  
 where Class_data_worker.Data_time between getdate() and dateadd(day,4,getdate())  order by Class_data_worker.Data_time";
                }

                if (d == "Чт")
                {
                    ad = 3;
                    query = @"Select Class_data_worker.Classes_Name,
Class_data_worker.Last_count_places ,Class_data_worker.Date , Class_data_worker.Time from Class_data_worker  
 where Class_data_worker.Data_time between getdate() and dateadd(day,3,getdate())  order by Class_data_worker.Data_time";
                }
                if (d == "Пт")
                {
                    ad = 2;
                    query = @"Select Class_data_worker.Classes_Name,
Class_data_worker.Last_count_places ,Class_data_worker.Date , Class_data_worker.Time from Class_data_worker  
where Class_data_worker.Data_time between getdate() and dateadd(day,2,getdate())  order by Class_data_worker.Data_time";
                }
                if (d == "Сб")
                {
                    ad = 8;
                    query = @"Select Class_data_worker.Classes_Name, 
Class_data_worker.Last_count_places ,Class_data_worker.Date , Class_data_worker.Time from Class_data_worker  
 where Class_data_worker.Data_time between getdate() and dateadd(day,7,getdate())  order by Class_data_worker.Data_time";
                }
                if (d == "Вс")
                {
                    ad = 7;
                    query = @"Select Class_data_worker.Classes_Name, 
Class_data_worker.Last_count_places ,Class_data_worker.Date , Class_data_worker.Time from Class_data_worker  
where Class_data_worker.Data_time between getdate() and dateadd(day,8,getdate())  order by Class_data_worker.Data_time";
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
                    data[data.Count - 1][1] = reader[2].ToString().Substring(0, 10);//дата занятия
                    data[data.Count - 1][2] = reader[3].ToString().Substring(0, 5);//время

                }
                int j = 0;
                int i = 1;
                reader.Close();
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

                    //dataGridView1.Rows.Add(s);
                }
            }
            


        }
        public void AddTo2Grid(int ad)
        {
            DateTime dateNow = DateTime.Now;
            dateNow = dateNow.AddDays(ad);
            string d = dateNow.ToString("ddd");
            string query = "";
            if (ad == 2)
            {
                query = @"Declare @nextWeek datetime = dateadd(day,2, getdate());
Select Class_data_worker.Classes_Name, 
Class_data_worker.Last_count_places ,Class_data_worker.Date , Class_data_worker.Time from Class_data_worker  
 where Class_data_worker.Data_time between @nextWeek and dateadd(day,6, @nextWeek)  order by Class_data_worker.Data_time";
            }

            if (ad == 3)
            {
                query = @"Declare @nextWeek datetime = dateadd(day,3, getdate());
Select Class_data_worker.Classes_Name, 
Class_data_worker.Last_count_places ,Class_data_worker.Date , Class_data_worker.Time from Class_data_worker  
 where Class_data_worker.Data_time between  @nextWeek and dateadd(day,6, @nextWeek)  order by Class_data_worker.Data_time";
            }
            if (ad == 4)
            {
                query = @"Declare @nextWeek datetime = dateadd(day,4, getdate());
Select Class_data_worker.Classes_Name, 
Class_data_worker.Last_count_places ,Class_data_worker.Date , Class_data_worker.Time from Class_data_worker  
 where Class_data_worker.Data_time between  @nextWeek and dateadd(day,6, @nextWeek)   order by Class_data_worker.Data_time";
            }

            if (ad == 5)
            {
                query = @"Declare @nextWeek datetime = dateadd(day,5, getdate());
Select Class_data_worker.Classes_Name, 
Class_data_worker.Last_count_places ,Class_data_worker.Date , Class_data_worker.Time from Class_data_worker  
 where Class_data_worker.Data_time between  @nextWeek and dateadd(day,6, @nextWeek)   order by Class_data_worker.Data_time";
            }
            if (ad == 6)
            {
                query = @"Declare @nextWeek datetime = dateadd(day,6, getdate());
Select Class_data_worker.Classes_Name, 
Class_data_worker.Last_count_places ,Class_data_worker.Date , Class_data_worker.Time from Class_data_worker  
where Class_data_worker.Data_time between  @nextWeek and dateadd(day,6, @nextWeek)  order by Class_data_worker.Data_time";
            }
            if (ad == 7)
            {
                query = @"Declare @nextWeek datetime = dateadd(day,8, getdate());
Select Class_data_worker.Classes_Name, 
Class_data_worker.Last_count_places ,Class_data_worker.Date , Class_data_worker.Time from Class_data_worker  
 where Class_data_worker.Data_time between  @nextWeek and dateadd(day,6, @nextWeek)  order by Class_data_worker.Data_time";
            }
            if (ad == 8)
            {
                query = @"Declare @nextWeek datetime = dateadd(day,7, getdate());
Select Class_data_worker.Classes_Name, 
Class_data_worker.Last_count_places ,Class_data_worker.Date , Class_data_worker.Time from Class_data_worker  
 where Class_data_worker.Data_time between  @nextWeek and dateadd(day,6, @nextWeek)   order by Class_data_worker.Data_time";
            }
            SqlCommand com = new SqlCommand(query, sqlcon);
            SqlDataReader reader = com.ExecuteReader();
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
            int j = 0;
            int i = 1;
            reader.Close();
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

                //dataGridView1.Rows.Add(s);
            }
        }
    
        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            ClassDiscriptionLoad(dataGridView1);
            
        }
        
        public void ClassDiscriptionLoad(DataGridView d)
        {
            btnAdd.Visible = false;
            btnChange.Visible = false;
            btnCancel.Visible = false;
            sqlcon.Open();
            lblChange.Text = "Описание тренировки:";
            panelDescription.Visible = true;
            lblName.Text = "Название";
            lblPutType.Text ="";
            lblPutComplexity.Text = "";
            lblPutPlace.Text = "";
            lblPutTrener.Text = "";
            if (d.CurrentCell.Value != null)
            {
               
                btnChange.Visible = true;
                btnCancel.Visible = true;
                string str = d.CurrentCell.Value.ToString();           
                string query = @"Select Classes.Name_class, Classes.Type_class, Complexity.Complexity_discription , Class_data_worker.Workers_id,
Class_data_worker.Last_count_places, Class_data_worker.Data_Time,  Class_data_worker.Time, Class_data_worker.Room_capacity from Classes  
inner join Complexity on Classes.Complexity_class=Complexity.Comlexity 
inner join Class_data_worker on Class_data_worker.Classes_Name=Classes.Name_class 
where Classes.Name_class='" + str + "'";
                SqlCommand com = new SqlCommand(query, sqlcon);
                SqlDataReader reader = com.ExecuteReader();

                while (reader.Read())
                {
                    lblName.Text = reader[0].ToString();
                    lblPutType.Text = reader[1].ToString();
                    lblPutComplexity.Text = reader[2].ToString();
                    lblPutPlace.Text=reader[4].ToString();
                    TrenersId=  reader[3].ToString();
                 ClassName= reader[0].ToString();
                        Kol= Convert.ToInt32(reader[7]);
                    LastKol =Convert.ToInt32( reader[4]);
        datetimeid = reader[5].ToString();
                    timeid = reader[6].ToString();
                }
                reader.Close();
                if (TrenersId == "")
                {
                    lblPutTrener.Text= " Тренер не объявлен";
                }
                else
                {
                    query = @"select Workers.Surname_worker  from Workers where Workers.Id_worker = '" + Convert.ToInt32(TrenersId) + "'";
                    com = new SqlCommand(query, sqlcon);
                   reader = com.ExecuteReader();
                    while (reader.Read())
                    {
                       TrenersName =reader[0].ToString();
                    }
                    lblPutTrener.Text = TrenersName;
                    reader.Close();
                }
            }
            else
            {
             
                btnAdd.Visible = true;
                int row = d.CurrentCell.RowIndex;
                switch (row)
                {
                    case 0:
                        timeforadd = "8:00";
                        break;
                    case 1:
                        timeforadd = "9:00";
                        break;
                    case 2:
                        timeforadd = "10:00";
                        break;
                    case 3:
                        timeforadd = "12:00";
                        break;
                    case 4:
                        timeforadd = "16:00";
                        break;
                    case 5:
                        timeforadd = "18:00";
                        break;
                    case 6:
                        timeforadd = "19:00";
                        break;
                    case 7:
                        timeforadd = "20:00";
                        break;
                }
                int col = d.CurrentCell.ColumnIndex;
                try
                {
                    if (d == dataGridView1)
                        switch (col)
                        {
                            case 1:
                                dateforadd = Monday.HeaderText.Substring(3);
                                break;
                            case 2:
                                dateforadd = Tuesday.HeaderText.Substring(3);
                                break;
                            case 3:
                                dateforadd = Wednesday.HeaderText.Substring(3);
                                break;
                            case 4:
                                dateforadd = Thursday.HeaderText.Substring(3);
                                break;
                            case 5:
                                dateforadd = Friday.HeaderText.Substring(3);
                                break;
                        }
                    else
                    {
                        switch (col)
                        {
                            case 1:
                                dateforadd = Monday2.HeaderText.Substring(3);
                                break;
                            case 2:
                                dateforadd = Tuesday2.HeaderText.Substring(3);
                                break;
                            case 3:
                                dateforadd = Wednesday2.HeaderText.Substring(3);
                                break;
                            case 4:
                                dateforadd = Thursday2.HeaderText.Substring(3);
                                break;
                            case 5:
                                dateforadd = Friday2.HeaderText.Substring(3);
                                break;
                        }
                    }
                }
                catch (ArgumentOutOfRangeException)
                {
                    
                    MessageBox.Show("Нельзя длать записи в прошлом");
                }
            }
            sqlcon.Close();
        }
        public string timeforadd = "";
        public string dateforadd = "";
        
        private void btnCancel_Click(object sender, EventArgs e)//отмена тренировки
        {
            sqlcon.Open();
            //выборка клиентов на эту тренировку
            string query = @"Select Client_Class.Clients_id, Client_pass.Last_count from Client_Class
inner join Client_pass  on Client_pass.Client_id=Client_Class.Clients_id where Client_Class.Class_date_time = '"+datetimeid+"'";
            SqlCommand com = new SqlCommand(query, sqlcon);
            SqlDataReader reader = com.ExecuteReader();
            List<int[]> clientsId = new List<int[]>();
            int i = 0;
            while (reader.Read())
            {
                clientsId.Add(new int[2]);

                clientsId[clientsId.Count - 1][0] =(int) reader[0];//id
                clientsId[clientsId.Count - 1][1] = (int)reader[1];//last pass count
                clientsId[clientsId.Count - 1][1] += 1;
            }
            reader.Close();
            List<string> emailData = new List<string>();
            query = @"select Client.Email_client from Client 
inner join Client_Class on Client.Id_client=Client_Class.Clients_id where Class_date_time='" + datetimeid + "'";
            SqlCommand come = new SqlCommand(query, sqlcon);
            SqlDataReader readere = come.ExecuteReader();
            while (readere.Read())
            {
                emailData.Add(readere[0].ToString());
            }
            readere.Close();

            // Реализовать отправку сообщений на емаил, что тренировка отменена

                        foreach (string s in emailData)
            {

                // отправитель - устанавливаем адрес и отображаемое в письме имя
                MailAddress fro = new MailAddress("anastasiapetrunina@mail.ru", "Your Fitness-club");
                // кому отправляем

                MailAddress t = new MailAddress(s);//s
                // создаем объект сообщения
                MailMessage mm = new MailMessage(fro, t);
                // тема письма
                mm.Subject = "Произошли изменения в вашем расписании!";
                // текст письма
                mm.Body = "Кажется ваша тренировка запланированная на " + datetimeid + " была отменена. Просим прощение за изменения!";
                // письмо представляет код html
                mm.IsBodyHtml = true;
                // адрес smtp-сервера и порт, с которого будем отправлять письмо
                SmtpClient smt = new SmtpClient("smtp.mail.ru", 587);
                // логин и пароль
                smt.Credentials = new NetworkCredential("anastasiapetrunina@mail.ru", "Nikita13062000");
                smt.EnableSsl = true;
                smt.Send(mm);

            }
            foreach (int[] s in clientsId)//удаляем тренировку у клиентов
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
            //выбираем тренера у которого отмена тренировки 
            string Emailworker="";
            query = @"select Workers.Email_worker from Workers 
inner join Class_data_worker on Workers.Id_worker=Class_data_worker.Workers_id where Class_data_worker.Data_time='" + datetimeid + "'";
            com = new SqlCommand(query, sqlcon);
            reader = com.ExecuteReader();
            while (reader.Read()) Emailworker = reader[0].ToString();
            reader.Close();
            //Отправляем емаилы тренерам об отмене тренировки

            // отправитель - устанавливаем адрес и отображаемое в письме имя
            MailAddress from = new MailAddress("anastasiapetrunina@mail.ru", "Your Fitness-club");
            // кому отправляем

            MailAddress to = new MailAddress("pnastya5@mail.ru");
            // создаем объект сообщения
            MailMessage m = new MailMessage(from, to);
            // тема письма
            m.Subject = "Произошли изменения в вашем расписании!";
            // текст письма
            m.Body = "Кажется ваша тренировка запланированная на " + datetimeid + " была отменена. Просим прощение за изменения!";
            // письмо представляет код html
            m.IsBodyHtml = true;
            // адрес smtp-сервера и порт, с которого будем отправлять письмо
            SmtpClient smtp = new SmtpClient("smtp.mail.ru", 587);
            // логин и пароль
            smtp.Credentials = new NetworkCredential("anastasiapetrunina@mail.ru", "Nikita13062000");
            smtp.EnableSsl = true;
            smtp.Send(m);

            //удаляем у работников
            query = @"delete from Class_data_worker where Data_time='" + datetimeid + "'";
            com = new SqlCommand(query, sqlcon);
            reader = com.ExecuteReader();
            // MessageBox.Show("Тренировка отменена");
            
            reader.Close();
            sqlcon.Close();
            ClassCancel cc = new ClassCancel();
            cc.ShowDialog();
            LoadData();
        }

        private void dataGridView2_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            ClassDiscriptionLoad(dataGridView2);
        }

        private void btnChange_Click(object sender, EventArgs e)
        {
            sqlcon.Open();
            List<string> workers = new List<string>();
            string query = @"select Surname_worker from Workers";
            SqlCommand com = new SqlCommand(query, sqlcon);
            SqlDataReader reader = com.ExecuteReader();
            while (reader.Read())
            {
                workers.Add(reader[0].ToString());
            }
            cmbTrener.Items.Clear();
            foreach (string s in workers)
            {
                cmbTrener.Items.Add(s);
            }
            reader.Close();
            lblDate.Visible = true;
            lblNameClass.Visible = true;
            lblPlaceCapacity.Visible = true;
            lblPlaceLost.Visible = true;
            lblTime.Visible = true;
            lblTrener.Visible = true;
            cmbCapacity.Visible = true;
            cmbClassName.Visible = true;
            cmbLost.Visible = true;
            cmbTime.Visible = true;
            cmbTrener.Visible = true;
            dateTimePicker1.Visible = true;
            btnMakeChange.Visible = true;
            btnMakeChange.Text = "Внести изменения";
            dateTimePicker1.Text = datetimeid;
            cmbTime.Text = timeid;
            cmbTrener.Text = TrenersName;
            cmbClassName.Text = ClassName;
            cmbLost.Text = LastKol.ToString();
            cmbCapacity.Text = Kol.ToString();
            sqlcon.Close();

        }

        private void btnMakeChange_Click(object sender, EventArgs e)
        {
            sqlcon.Open();
            string query = "";
            string newDateTimeId = "";
            if (btnMakeChange.Text == "Внести изменения")
            {
                if (dateTimePicker1.Text != "" && cmbTime.Text != "" && cmbTrener.Text != "" && cmbLost.Text != "" && cmbClassName.Text != "" && cmbCapacity.Text != "")
                {
                    if (Convert.ToInt32(cmbCapacity.Text) >= Convert.ToInt32(cmbLost.Text))
                    {
                    newDateTimeId = dateTimePicker1.Text + " " + cmbTime.Text;
                        //выбираем тренера у которого отмена тренировки 
                        string Emailworker = "";
                        query = @"select Workers.Email_worker from Workers 
inner join Class_data_worker on Workers.Id_worker=Class_data_worker.Workers_id where Class_data_worker.Data_time='" + datetimeid + "'";
                        SqlCommand com = new SqlCommand(query, sqlcon);
                      SqlDataReader  reader = com.ExecuteReader();
                        while (reader.Read()) Emailworker = reader[0].ToString();
                        reader.Close();
                        //Отправляем емаилы тренерам об отмене тренировки

                        // отправитель - устанавливаем адрес и отображаемое в письме имя
                        MailAddress from = new MailAddress("anastasiapetrunina@mail.ru", "Your Fitness-club");
                        // кому отправляем

                        MailAddress to = new MailAddress("pnastya5@mail.ru");
                        // создаем объект сообщения
                        MailMessage m = new MailMessage(from, to);
                        // тема письма
                        m.Subject = "Произошли изменения в вашем расписании!";
                        // текст письма
                        m.Body = "Кажется ваша тренировка запланированная на " + datetimeid + " была отменена. Просим прощение за изменения!";
                        // письмо представляет код html
                        m.IsBodyHtml = true;
                        // адрес smtp-сервера и порт, с которого будем отправлять письмо
                        SmtpClient smtp = new SmtpClient("smtp.mail.ru", 587);
                        // логин и пароль
                        smtp.Credentials = new NetworkCredential("anastasiapetrunina@mail.ru", "Nikita13062000");
                        smtp.EnableSsl = true;
                        smtp.Send(m);
                        //ищем емаилы клиентов у кого изменились данные тренировки
                        List<string> emailData = new List<string>();
                        query = @"select Client.Email_client from Client 
inner join Client_Class on Client.Id_client=Client_Class.Clients_id where Class_date_time='" + datetimeid + "'";
                        SqlCommand come = new SqlCommand(query, sqlcon);
                        SqlDataReader readere = come.ExecuteReader();
                        while (readere.Read())
                        {
                            emailData.Add(readere[0].ToString());
                        }
                        readere.Close();
                        //ищем данные клиентов у которых изменилось
                         query = @"Select Client_Class.Clients_id, Client_pass.Last_count from Client_Class
inner join Client_pass  on Client_pass.Client_id=Client_Class.Clients_id where Client_Class.Class_date_time = '" + datetimeid + "'";
                         com = new SqlCommand(query, sqlcon);
                         reader = com.ExecuteReader();
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
                        //возвращаем одно место за тренировку
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
                        //удаляем у клиентов тренировку
                        query = @"delete from Client_Class where Class_date_time='" + datetimeid + "'";
                     come = new SqlCommand(query, sqlcon);
                     readere = come.ExecuteReader();
                    readere.Close();

                    int id_worker = 0;
                    query = @"select Id_worker from Workers where Surname_worker = '" + cmbTrener.Text + "'";
                    com = new SqlCommand(query, sqlcon);
                    reader = com.ExecuteReader();
                    while (reader.Read())
                    {
                        id_worker = (int)reader[0];
                    }
                    reader.Close();



                        //Реализовать отправку сообщений на емаил, что тренировка изменена

                        foreach (string s in emailData)
                        {
                            
                                // отправитель - устанавливаем адрес и отображаемое в письме имя
                                MailAddress fromm = new MailAddress("anastasiapetrunina@mail.ru", "Your Fitness-club");
                                // кому отправляем

                                MailAddress too = new MailAddress("pnastya5@mail.ru");//s
                                // создаем объект сообщения
                                MailMessage mm = new MailMessage(fromm, too);
                                // тема письма
                                mm.Subject = "Произошли изменения в вашем расписании!";
                                // текст письма
                                mm.Body = "Кажется ваша тренировка запланированная на "+ datetimeid+" изменилась. Посмотрите изменения и запишитесь повторно";
                                // письмо представляет код html
                                mm.IsBodyHtml = true;
                                // адрес smtp-сервера и порт, с которого будем отправлять письмо
                                SmtpClient smtpp = new SmtpClient("smtp.mail.ru", 587);
                                // логин и пароль
                                smtpp.Credentials = new NetworkCredential("anastasiapetrunina@mail.ru", "Nikita13062000");
                                smtpp.EnableSsl = true;
                                smtpp.Send(mm);
                            
                        }
                        query = @"update Class_data_worker set Data_time='" + newDateTimeId + "',Date='" + dateTimePicker1.Text + "',Time='" + cmbTime.Text +
                         "', Workers_id = '" + id_worker + "', Last_count_places = '" + cmbLost.Text + "',Classes_Name = '" + cmbClassName.Text + "' ,Room_capacity = '"
                         + cmbCapacity.Text + "' where Data_time = '" + datetimeid + "'";
                        com = new SqlCommand(query, sqlcon);
                        reader = com.ExecuteReader();
                        reader.Close();
                        datetimeid = newDateTimeId;
                    }
                    else
                    {
                        MessageBox.Show("Количество свободных мест и вместимости зала некорректны!");
                    }
                }
                else MessageBox.Show("Заполнены не все поля");
            }
            else //добавить тренировку
            {
                
                if (cmbCapacity.Text!="" && cmbClassName.Text != "")
                {
                    dateforadd = dateTimePicker1.Text;
                    timeforadd = cmbTime.Text;
                        if (cmbTrener.Text != "")
                        {
                            int id_worker = 0;
                            query = @"select Workers.Id_worker from Workers where Workers.Surname_worker = '" + cmbTrener.Text + "'";
                            SqlCommand com = new SqlCommand(query, sqlcon);
                            SqlDataReader reader = com.ExecuteReader();
                            while (reader.Read())
                            {
                                id_worker = (int)reader[0];
                            }
                            reader.Close();

                            newDateTimeId = dateforadd + " " + timeforadd + ":00";
                            query = @"insert into Class_data_worker (Data_time, Workers_id, Last_count_places, Classes_Name, Room_capacity, Date, Time) 
                            Values ( '" + newDateTimeId + "','" + id_worker + "','" + cmbCapacity.Text + "','"
                                 + cmbClassName.Text + "','" + cmbCapacity.Text + "','" + dateforadd + "','" + timeforadd + "')";
                            com = new SqlCommand(query, sqlcon);
                            reader = com.ExecuteReader();
                            reader.Close();
                        }
                        else
                        {
                        newDateTimeId = dateforadd + " " + timeforadd + ":00";
                        query = @"insert into Class_data_worker (Data_time, Last_count_places, Classes_Name, Room_capacity, Date, Time) 
Values ( '" + newDateTimeId +  "','" + cmbCapacity.Text + "','"
                                 + cmbClassName.Text + "','" + cmbCapacity.Text + "','" + dateforadd + "','" + timeforadd + "')";
                           SqlCommand com = new SqlCommand(query, sqlcon);
                          SqlDataReader  reader = com.ExecuteReader();
                           reader.Close();
                        }
                
                    btnAdd.Visible = false;
                }
                else MessageBox.Show("Заполнены не все поля");
            }
            sqlcon.Close();
            LoadData();
            btnAdd.Visible = false;
            btnChange.Visible = false;
            btnCancel.Visible = false;
            cmbCapacity.Text="";
            cmbClassName.Text = "";
            cmbLost.Text = "";
            cmbTime.Text = "";
            cmbTrener.Text = "";
            lblDate.Visible = false;
            lblNameClass.Visible = false;
            lblPlaceCapacity.Visible = false;
            lblPlaceLost.Visible = false;
            lblTime.Visible = false;
            lblTrener.Visible = false;
            cmbCapacity.Visible = false;
            cmbClassName.Visible = false;
            cmbLost.Visible = false;
            cmbTime.Visible = false;
            cmbTrener.Visible = false;
            dateTimePicker1.Visible = false;
            btnMakeChange.Visible = false;
            btnMakeChange.Visible = false;
            //query = @"update Class_data_worker set Data_time='" + newDateTimeId + "' Workers_id ='" + cmbTrener.Text + "',Last_count_places='" + cmbLost.Text
            //     + "',Classes_Name='" + cmbClassName.Text + "',Room_capacity='" + cmbCapacity.Text + "',Date='" + dateTimePicker1.Text +
            //     "',Time='" + cmbTime.Text + "' where Data_time = '" + datetimeid + "'";
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            sqlcon.Open();
            List<string> workers = new List<string>();
            string query = @"select Surname_worker from Workers";
            SqlCommand com = new SqlCommand(query, sqlcon);
            SqlDataReader reader = com.ExecuteReader();
            while (reader.Read())
            {
                workers.Add(reader[0].ToString());
            }
            cmbTrener.Items.Clear();
            foreach (string s in workers)
            {
                cmbTrener.Items.Add(s);
            }
            reader.Close();
            lblDate.Visible = true;
            lblNameClass.Visible = true;
            lblPlaceCapacity.Visible = true;
            //lblPlaceLost.Visible = true;
            lblTime.Visible = true;
            lblTrener.Visible = true;
            cmbCapacity.Visible = true;
            cmbClassName.Visible = true;
            //cmbLost.Visible = true;
            cmbTime.Visible = true;
            cmbTrener.Visible = true;
            dateTimePicker1.Visible = true;
            btnMakeChange.Visible = true;
            btnMakeChange.Text = "Добавить";
            cmbTime.Text = timeforadd;
            dateTimePicker1.Text = dateforadd;
            sqlcon.Close();
        }

        private void зарегистрироватьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            
        }

        private void изменитьДанныеToolStripMenuItem_Click(object sender, EventArgs e)
        {
           
        }

        private void ManagerSchedule_Load(object sender, EventArgs e)
        {

        }

        private void menuStrip2_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }

        private void panel3_Paint(object sender, PaintEventArgs e)
        {

        }

        private void pictureBox4_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void зарегистрироватьToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            TrenerRegistration tr = new TrenerRegistration();
            tr.ShowDialog();
        }

        private void изменитьДанныеToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            ChangeTrener ct = new ChangeTrener();
            ct.ShowDialog();
        }

        private void зарегистрироватьToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            RegistrationClient rg = new RegistrationClient();

            rg.ShowDialog();
        }

        private void изменитьДанныеToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            ChangeClient rg = new ChangeClient();

            rg.ShowDialog();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            statistics s = new statistics();
            s.ShowDialog();
        }
    }
}
