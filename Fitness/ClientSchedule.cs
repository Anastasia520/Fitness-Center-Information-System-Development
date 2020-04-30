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
    public partial class ClientSchedule : Form
    {
        public ClientSchedule()
        {
            InitializeComponent();
            clientsId = 1;//LoginForm.Id;
            LoadData();
            //dataGridView1.DefaultCellStyle.WrapMode = DataGridViewTriState.True;
            //dataGridView1.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;

        }
      public  SqlConnection sqlcon = new SqlConnection(@"Data Source=LENOVO-PC\MSSQLSERVEREXPRE;Initial Catalog=Fitness-club;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");
        private void LoadData()
        {
            panelDescription.Visible = true;
            sqlcon.Open();
            
            string query = @"Select Class_date_time from Client_Class 
where Class_date_time between getdate() and dateadd(week,1,getdate()) and Clients_id='" + clientsId + "' order by Class_date_time";
            SqlCommand com = new SqlCommand(query, sqlcon);
            SqlDataReader reader = com.ExecuteReader();
            string[] chosenClass = new string[20];
           int  i = 0;
            while (reader.Read())
            {
                chosenClass[i] = reader[0].ToString();
                i++;
            }
            reader.Close();
            query = @"Select Class_data_worker.Classes_Name, Workers.Surname_worker ,
Class_data_worker.Last_count_places ,Class_data_worker.Date , Class_data_worker.Time, Class_data_worker.Data_time from Class_data_worker  
inner join Workers on Class_data_worker.Workers_id=Workers.Id_worker where Class_data_worker.Data_time between getdate() and dateadd(week,1,getdate())  order by Class_data_worker.Data_time";
             com = new SqlCommand( query, sqlcon);
            reader = com.ExecuteReader();
            List<string[]> data = new List<string[]>();
            //getdate() вставить вместо даты и выводить с текущего дня
            //Class_data_worker.Data_time >= '2020-03-02'
            string[] allClass = new string[20];
            i = 0;
            while (reader.Read())
            {
                allClass[i] = reader[5].ToString();
                i++;
            }
            string [] green = new string[20];
            int l = 0;
            for (i = 0; i < allClass.Length; i++)
                for (int k =0;k<chosenClass.Length;k++)
            {
                    if (allClass[i]!=null && chosenClass[k]!=null)
                    if (allClass[i] == chosenClass[k]) { green[l] = allClass[i]; l++; }
                    
            }
            reader.Close();
            com = new SqlCommand(query, sqlcon);
            reader = com.ExecuteReader();
            while (reader.Read())
            {
                data.Add(new string[4]);

                data[data.Count - 1][0] = reader[0].ToString();//+ " Тренер: "+reader[1].ToString()+ " Мест: " +  reader[2].ToString();
                data[data.Count - 1][1] = reader[3].ToString().Substring(0, 10);//дата занятия
                data[data.Count - 1][2] = reader[4].ToString().Substring(0,5);//время
               for (i = 0; i < green.Count(); i++)
                {
                    if (reader[5].ToString() == green[i])
                    { data[data.Count - 1][3] = "yes"; break; }
                    else data[data.Count - 1][3] = "no";
                }
               
            }
            
            sqlcon.Close();
             i = 1;
            
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
            DateTime dd = DateTime.Now;
            for (int p = 0; p < 6; p++)
            {
                dd = DateTime.Now.AddDays(p);
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
                if (day == "Ср") {i = 3; Wednesday.HeaderText = "Ср " + s[1];}
                if (day == "Чт") { i = 4; Thursday.HeaderText = "Чт " + s[1]; }
                if (day == "Пт") { i = 5; Friday.HeaderText = "Пт " + s[1]; }

                        dataGridView1[i, j].Value = s[0];
                if (s[3]=="yes") dataGridView1[i, j].Style.BackColor = Color.Green;
                //i++;
                //dataGridView1.Rows.Add(s);
            }

            sqlcon.Close();
            reader.Close();
            LoadPassKol();
        }
        public bool isGo;
        public int PassLost = 0;
        public void LoadPassKol()
        {
            sqlcon.Open();
            string query = @"Select Last_count from Client_pass
where Client_id='" + clientsId + "'";
            SqlCommand com = new SqlCommand(query, sqlcon);
            SqlDataReader reader = com.ExecuteReader();
            int i = 0;
            int[] passMas = new int[20];
            while (reader.Read())
            {
                passMas[i] = (int)reader[0];
                i++;
            }
            reader.Close();
            PassLost = passMas.OrderByDescending(x => x).First();
            lblNum.Text = PassLost.ToString();
            sqlcon.Close();

        }
        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            sqlcon.Open();
            lblDiscription.Text = "Описание тренировки:";
           
            panelDescription.Visible = true;
            lblName.Text = "Название";
            lblPutType.Text = "";
            lblPutComplexity.Text = "";
            lblPutPlace.Text = "";
            lblPutTrener.Text = "";
           
            if (dataGridView1.CurrentCell.Value != null)
            { 
                
            string str = dataGridView1.CurrentCell.Value.ToString();
            string query = @"Select Classes.Name_class, Classes.Type_class, Complexity.Complexity_discription ,Workers.Surname_worker ,
Class_data_worker.Last_count_places, Class_data_worker.Data_Time from Classes  
inner join Complexity on Classes.Complexity_class=Complexity.Comlexity 
inner join Class_data_worker on Class_data_worker.Classes_Name=Classes.Name_class 
inner join Workers on Class_data_worker.Workers_id=Workers.Id_worker
where Classes.Name_class='" + str + "'";
            SqlCommand com = new SqlCommand(query, sqlcon);
            SqlDataReader reader = com.ExecuteReader();

            while (reader.Read())
            {
                    lblName.Text = reader[0].ToString();
                    lblPutType.Text = reader[1].ToString();
                    lblPutComplexity.Text = reader[2].ToString();
                    lblPutPlace.Text = reader[4].ToString();
                    lblPutTrener.Text = reader[3].ToString();
                    //ClassName = reader[0].ToString();
                    //txtClassDiscrip.Text = reader[0].ToString() + " " + reader[1].ToString() + " " + reader[2].ToString() + " " + reader[3].ToString() + " Мест: " + reader[4].ToString();
                places = (int)reader[4];
                datetimeid = reader[5].ToString();
            }
            reader.Close();
            
                if (dataGridView1.CurrentCell.Style.BackColor != Color.Green)
                {
                    btnGo.Visible = true;
                    btnGo.Text = "Записаться";
                    
                    
                    isGo = true;
                    
                }
                else
                {
                    btnGo.Visible = true;
                    btnGo.Text = "Отменить запись";
                    panelDescription.Visible = true;
                    
                    isGo = false;
                }
               
            }
            sqlcon.Close();
        }
        public int places = 0;
        public string datetimeid;
        public int clientsId = 1;

        private void btnGo_Click(object sender, EventArgs e)
        {
            sqlcon.Open();
            string query = @"Select Last_count_places from Class_data_worker where Data_time='" + datetimeid + "'";
            SqlCommand com = new SqlCommand(query, sqlcon);
            SqlDataReader reader = com.ExecuteReader();
            int lastCount = 0;
            while (reader.Read())
            {
                lastCount = (int)reader[0];
            }            
            sqlcon.Close();
            reader.Close();

            if (isGo && places > 0 && PassLost > 0)
            {
                sqlcon.Open();
                query = @"Insert into Client_Class

(Clients_id, Class_date_time)

Values

('" + clientsId + "','" + datetimeid + "')";
                com = new SqlCommand(query, sqlcon);
                reader = com.ExecuteReader();
                reader.Close();
                MessageBox.Show("Done!");
                dataGridView1.CurrentCell.Style.BackColor = Color.Green;
                lastCount -= 1;
                query = @"update Class_data_worker set Last_count_places = '" + lastCount + "'where Data_time='" + datetimeid + "'";
                com = new SqlCommand(query, sqlcon);
                reader = com.ExecuteReader();
                reader.Close();

                PassLost -= 1;
                query = @"update Client_pass set Last_count = '" + PassLost + "'where Client_id='" + clientsId + "'";
                com = new SqlCommand(query, sqlcon);
                reader = com.ExecuteReader();
                reader.Close();
                sqlcon.Close();
                LoadPassKol();
            }
            else {
                if (places == 0) MessageBox.Show("Вы не можете записаться на днную тренировку, на ней уже все места заняты");
                if (PassLost == 0) MessageBox.Show("Вы не можете записаться на днную тренировку, у вас закончился абонемент");
            }

             if (!isGo)
            {
                sqlcon.Open();
                 query = @"delete from Client_Class where Class_date_time='"+datetimeid+"'and Clients_id='"+clientsId+"'";
              com = new SqlCommand(query, sqlcon);
               reader = com.ExecuteReader();
                MessageBox.Show("Done!");
                dataGridView1.CurrentCell.Style.BackColor = Color.Gray;
                reader.Close();

                lastCount += 1;
                PassLost += 1;
                query = @"update Class_data_worker set Last_count_places = '" + lastCount + "'where Data_time='" + datetimeid + "'";
                com = new SqlCommand(query, sqlcon);
                reader = com.ExecuteReader();
                reader.Close();
                query = @"update Client_pass set Last_count = '" + PassLost + "'where Client_id='" + clientsId + "'";
                com = new SqlCommand(query, sqlcon);
                reader = com.ExecuteReader();
                reader.Close();
                sqlcon.Close();
                LoadPassKol();
            }

        }
        public string complex = "";

        private void button1_Click(object sender, EventArgs e)
        {
            if (cmbFiltr.Text != "")
            {
                //находим индекс сложности 
                sqlcon.Open();
                string query = @"Select Comlexity from Complexity where Complexity_discription = '" + cmbFiltr.Text + "'";
                SqlCommand com = new SqlCommand(query, sqlcon);
                SqlDataReader reader = com.ExecuteReader();
                while (reader.Read()) complex = reader[0].ToString();
                reader.Close();

                query = @"Select Class_date_time from Client_Class 
where Class_date_time between getdate() and dateadd(week,1,getdate()) and Clients_id='" + clientsId + "' order by Class_date_time";
                com = new SqlCommand(query, sqlcon);
                reader = com.ExecuteReader();
                string[] chosenClass = new string[20];
                int i = 0;
                while (reader.Read())
                {
                    chosenClass[i] = reader[0].ToString();
                    i++;
                }
                reader.Close();

                query = @"Select Class_data_worker.Classes_Name, Workers.Surname_worker ,
Class_data_worker.Last_count_places ,Class_data_worker.Date , Class_data_worker.Time, Class_data_worker.Data_time, Classes.Name_class from Class_data_worker  
inner join Workers on Class_data_worker.Workers_id=Workers.Id_worker 
inner join Classes on Classes.Name_class = Class_data_worker.Classes_Name where Class_data_worker.Data_time between getdate() and dateadd(week,1,getdate()) and Classes.Complexity_class = '" + complex + "'  order by Class_data_worker.Data_time";
                com = new SqlCommand(query, sqlcon);
                reader = com.ExecuteReader();
                List<string[]> data = new List<string[]>();
                //getdate() вставить вместо даты и выводить с текущего дня
                //Class_data_worker.Data_time >= '2020-03-02'
                string[] allClass = new string[20];
                i = 0;
                while (reader.Read())
                {
                    allClass[i] = reader[5].ToString();
                    i++;
                }
                string[] green = new string[20];
                int l = 0;
                for (i = 0; i < allClass.Length; i++)
                    for (int k = 0; k < chosenClass.Length; k++)
                    {
                        if (allClass[i] != null && chosenClass[k] != null)
                            if (allClass[i] == chosenClass[k]) { green[l] = allClass[i]; l++; }

                    }
                reader.Close();
                com = new SqlCommand(query, sqlcon);
                reader = com.ExecuteReader();
                while (reader.Read())
                {
                    data.Add(new string[4]);

                    data[data.Count - 1][0] = reader[0].ToString();//+ " Тренер: "+reader[1].ToString()+ " Мест: " +  reader[2].ToString();
                    data[data.Count - 1][1] = reader[3].ToString().Substring(0, 10);//дата занятия
                    data[data.Count - 1][2] = reader[4].ToString().Substring(0, 5);//время
                    for (i = 0; i < green.Count(); i++)
                    {
                        if (reader[5].ToString() == green[i])
                        { data[data.Count - 1][3] = "yes"; break; }
                        else data[data.Count - 1][3] = "no";
                    }

                }

                sqlcon.Close();
                i = 1;
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
                DateTime dd = DateTime.Now;
                for (int p = 0; p < 6; p++)
                {
                    dd = DateTime.Now.AddDays(p);
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
                    if (s[3] == "yes") dataGridView1[i, j].Style.BackColor = Color.Green;
                    //i++;
                    //dataGridView1.Rows.Add(s);
                }

                sqlcon.Close();
                reader.Close();
                LoadPassKol();
            }
            else LoadData();
        }

        private void pictureBox4_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void pictureBox8_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void btnSupport_Click(object sender, EventArgs e)
        {
            Support s = new Support();
            s.ShowDialog();
        }
    }
}
