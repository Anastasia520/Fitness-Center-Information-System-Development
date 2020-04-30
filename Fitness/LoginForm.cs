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
using System.Net.Mail;
using System.Net;

namespace Fitness
{
    public partial class LoginForm : Form
    {
        public LoginForm()
        {
            InitializeComponent();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            //Application.Exit();
        }
        Point lastPoint;
        private void MainPanel_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                this.Left += e.X - lastPoint.X;
                this.Top += e.Y - lastPoint.Y;
            }
        }

        private void MainPanel_MouseDown(object sender, MouseEventArgs e)
        {
            lastPoint = new Point(e.X, e.Y);
        }

        private void MainPanel_Paint(object sender, PaintEventArgs e)
        {

        }

        private void panel2_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                this.Left += e.X - lastPoint.X;
                this.Top += e.Y - lastPoint.Y;
            }
        }

        private void panel2_MouseDown(object sender, MouseEventArgs e)
        {
            lastPoint = new Point(e.X, e.Y);
        }
        public static int Id;
      public  SqlConnection sqlcon = new SqlConnection(@"Data Source=LENOVO-PC\MSSQLSERVEREXPRE;Initial Catalog=Fitness-club;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");

        private void btnLogIn_Click(object sender, EventArgs e)
        {
           
            sqlcon.Open();
            string query = "Select Client.Id_client from Client inner join Login on Client.Email_client=Login.Email Where Email ='" + textBoxUser.Text.Trim() + "'and Password = '" + textBoxPass.Text.Trim() + "'";
            SqlDataAdapter sda = new SqlDataAdapter(query,sqlcon);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            SqlCommand com = new SqlCommand(query, sqlcon);
            SqlDataReader reader = com.ExecuteReader();

            while (reader.Read())
            { Id = (int)reader[0]; }
           
            reader.Close();

            if (dt.Rows.Count==1)
            {
                cmnd = 1;
                
                ClientSchedule cs = new ClientSchedule();
                cs.Show();
                this.Close();
            }
            else
            {
                query = "Select Workers.Id_worker from Workers inner join Login on Workers.Email_worker=Login.Email Where Email ='" + textBoxUser.Text.Trim() + "'and Password = '" + textBoxPass.Text.Trim() + "'";
                 sda = new SqlDataAdapter(query, sqlcon);
                dt = new DataTable();
                sda.Fill(dt);
                if (dt.Rows.Count == 1)
                {
                    cmnd = 1;
                    this.Hide();
                    TrenerSchedule cs = new TrenerSchedule();
                    cs.Show();
                }
                else
                {
                    query = "Select * from   Login  Where Email ='" + textBoxUser.Text.Trim() + "'and Password = '" + textBoxPass.Text.Trim() + "'";
                    sda = new SqlDataAdapter(query, sqlcon);
                    dt = new DataTable();
                    sda.Fill(dt);
                    if (dt.Rows.Count == 1)
                    {
                        cmnd = 1;
                        this.Close();

                        ManagerSchedule cs = new ManagerSchedule();
                        cs.Show();
                    }
                    else MessageBox.Show("Проверьте корректность введены данных и попробуйте еще раз войти");
                }
            }


            sqlcon.Close();
        }
        public string password = "";
        private void lblForget_Click(object sender, EventArgs e)
        {
            sqlcon.Open();
            string query = "Select Password from  Login  Where Email ='" + textBoxUser.Text.Trim() + "'";
            
            SqlCommand com = new SqlCommand(query, sqlcon);
            SqlDataReader reader = com.ExecuteReader();

            while (reader.Read())
            { password = reader[0].ToString(); }

            reader.Close();
            sqlcon.Close();
            if (password !="")
            {
                // отправитель - устанавливаем адрес и отображаемое в письме имя
                MailAddress from = new MailAddress("anastasiapetrunina@mail.ru", "Your Fitness-club");
                // кому отправляем

                MailAddress to = new MailAddress(textBoxUser.Text);
                // создаем объект сообщения
                MailMessage m = new MailMessage(from, to);
                // тема письма
                m.Subject = "Кажется, вы забыли пароль!";
                // текст письма
                m.Body = "Ваш пароль для входа в фитнес-клуб: "+password;
                // письмо представляет код html
                m.IsBodyHtml = true;
                // адрес smtp-сервера и порт, с которого будем отправлять письмо
                SmtpClient smtp = new SmtpClient("smtp.mail.ru", 587);
                // логин и пароль
                smtp.Credentials = new NetworkCredential("anastasiapetrunina@mail.ru", "Nikita13062000");
                smtp.EnableSsl = true;
                smtp.Send(m);
            }
            else
            {
                MessageBox.Show("Проверьте правильность почты и попробуйте еще раз");
                textBoxUser.Text = "";
            }
        }

        private void btnExit_Click_1(object sender, EventArgs e)
        {
            this.Close();
            //StartTablecs st = new StartTablecs();
            //st.Show();
        }
        public int cmnd = 0;
    }
}
