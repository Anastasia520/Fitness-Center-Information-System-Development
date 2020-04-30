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
using Microsoft.Office.Interop.Excel;
using Application = Microsoft.Office.Interop.Excel.Application;
using Word = Microsoft.Office.Interop.Word;
using System.Reflection;


namespace Fitness
{
    public partial class statistics : Form
    {
        public statistics()
        {
            InitializeComponent();

        }
        public SqlConnection sqlcon = new SqlConnection(@"Data Source=LENOVO-PC\MSSQLSERVEREXPRE;Initial Catalog=Fitness-club;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");
        public int stastic = 0;
        private void btnByClasses_Click(object sender, EventArgs e)
        {
            lblStart.Visible = true;
            lblFinish.Visible = true;
            dtpFinish.Visible = true;
            dtpStart.Visible = true;
            btnGetData.Visible = true;
            stastic = 1;
            lblCheck.Visible = false;
            dataGridViewStatistics.ColumnCount = 3;
            dataGridViewStatistics.RowCount = 1;
            dataGridViewStatistics.Rows.Clear();
            dataGridViewStatistics.Columns.Clear();
        }

        private void btnGetData_Click(object sender, EventArgs e)
        {
            if (dtpStart.Value <= dtpFinish.Value)
            {
                
                sqlcon.Open();
                switch (stastic)
                {
                    case 1:
                        dataGridViewStatistics.Visible = true;
                        chartTreners.Visible = false;
                        btnSaveInExcel.Visible = true;
                        btnSaveInExcel.Text = "Выгрузить в Excel";
                        //добавление необходимых  колонок
                        DataGridViewTextBoxColumn dgvDate = new DataGridViewTextBoxColumn();
                        //установка свойств
                        dgvDate.Name = "Date";
                        dgvDate.HeaderText = "Дата";
                        //добавили колонку
                        dataGridViewStatistics.Columns.Add(dgvDate);
                        DataGridViewTextBoxColumn dgvName = new DataGridViewTextBoxColumn();
                        //установка свойств
                        dgvName.Name = "Name";
                        dgvName.HeaderText = "Название тренировки";
                        //добавили колонку
                        dataGridViewStatistics.Columns.Add(dgvName);
                        DataGridViewTextBoxColumn dgvKol = new DataGridViewTextBoxColumn();
                        //установка свойств
                        dgvKol.Name = "Kol";
                        dgvKol.HeaderText = "Количество посетивших";
                        //добавили колонку
                        dataGridViewStatistics.Columns.Add(dgvKol);
                        int last = 0;
                        int kol = 0;
                        List<string[]> data = new List<string[]>();
                        string query = @"select Data_time, Classes_name,Last_count_places, Room_capacity from Class_data_worker 
where Data_time between '"+ dtpStart.Value + "'and'"+dtpFinish.Value+"'";
                        SqlCommand com = new SqlCommand(query, sqlcon);
                        SqlDataReader reader = com.ExecuteReader();
                        while (reader.Read())
                        {
                            data.Add(new string[3]);
                            data[data.Count - 1][0] = reader[0].ToString();
                            data[data.Count - 1][1] = reader[1].ToString();
                            last = (int)reader[2];
                            kol = (int)reader[3];
                            data[data.Count - 1][2] = (kol-last).ToString();
                        }
                        reader.Close();
                        
                        int j = 0;
                        foreach(string [] s in data)
                        {
                            dataGridViewStatistics.RowCount += 1;
                            dataGridViewStatistics[0, j].Value = s[0];
                            dataGridViewStatistics[1, j].Value = s[1];
                            dataGridViewStatistics[2, j].Value = s[2];
                            j++;
                        }
                       
                        break;

                    case 2: dataGridViewStatistics.Visible = true;
                        chartTreners.Visible = false;
                        btnSaveInExcel.Visible = true;
                        btnSaveInExcel.Text = "Выгрузить в Word";
                        //добавление необходимых  колонок
                        dgvDate = new DataGridViewTextBoxColumn();
                        //установка свойств
                        dgvDate.Name = "Date";
                        dgvDate.HeaderText = "Дата регистрации";
                        //добавили колонку
                        dataGridViewStatistics.Columns.Add(dgvDate);
                         dgvName = new DataGridViewTextBoxColumn();
                        //установка свойств
                        dgvName.Name = "Name";
                        dgvName.HeaderText = "Фамилия и имя клиента";
                        //добавили колонку
                        dataGridViewStatistics.Columns.Add(dgvName);
                        DataGridViewTextBoxColumn dgvPass = new DataGridViewTextBoxColumn();
                        //установка свойств
                        dgvPass.Name = "Pass";
                        dgvPass.HeaderText = "Абонемент";
                        //добавили колонку
                        dataGridViewStatistics.Columns.Add(dgvPass);
                       
                        List<string[]> date = new List<string[]>();
                        
                        query = @"select Client.Start_data_client, Client.Surname_client,Client.Name_client, Client_pass.Pass_name from Client
inner join Client_pass on Client.Id_client=Client_pass.Client_id where Client.Start_data_client between '" + dtpStart.Value + "'and'" + dtpFinish.Value + "'";
                         com = new SqlCommand(query, sqlcon);
                         reader = com.ExecuteReader();
                        while (reader.Read())
                        {
                            date.Add(new string[3]);
                            date[date.Count - 1][0] = reader[0].ToString();

                            date[date.Count - 1][1] = reader[1].ToString() + " " + reader[2].ToString();

                            date[date.Count - 1][2] = reader[3].ToString();
                        }
                            reader.Close();

                        j = 0;
                        foreach (string[] s in date)
                        {
                            dataGridViewStatistics.RowCount += 1;
                            dataGridViewStatistics[0, j].Value = s[0];
                            dataGridViewStatistics[1, j].Value = s[1];
                            dataGridViewStatistics[2, j].Value = s[2];
                            j++;
                        }
                        break;
                    case 3:
                        dataGridViewStatistics.Visible = false;
                        chartTreners.Visible = true;
                        btnSaveInExcel.Visible = false;
                        chartTreners.Titles.Add(@"Диаграмма занятости тренеров 
с " + dtpStart.Value.ToShortDateString() +" по "+ dtpFinish.Value.ToShortDateString());

                        List<string[]> chardate = new List<string[]>();
                        query = @"select  Workers.Surname_worker, Workers.Id_worker from Workers";
                        com = new SqlCommand(query, sqlcon);
                        reader = com.ExecuteReader();
                        while (reader.Read())
                        {
                            chardate.Add(new string[3]);
                            chardate[chardate.Count - 1][0] = reader[0].ToString();
                            chardate[chardate.Count - 1][1] = reader[1].ToString();
                            chardate[chardate.Count - 1][2] = "";
                        }
                        reader.Close();
                        foreach (string[] s in chardate)
                        {
                            query = @"select  Count(Classes_Name) from Class_data_worker where  Class_data_worker.Workers_id='"+s[1]+"' and Class_data_worker.Data_time between '" + dtpStart.Value + "'and'" + dtpFinish.Value + "'";
                            com = new SqlCommand(query, sqlcon);
                            reader = com.ExecuteReader();
                            while (reader.Read())
                            {
                                s[2] = reader[0].ToString();
                            }
                            reader.Close();
                        }
                        foreach (string[] s in chardate)
                        {
                            chartTreners.Series["Trener"].IsValueShownAsLabel = true; 
                            chartTreners.Series["Trener"].Points.AddXY(s[0], s[2]);
                        }
                        break;


                }
               

                sqlcon.Close();
            }
            else
            {
                MessageBox.Show("Даты отчетности некорректны");
            }
        }

        private void btnSaveInExcel_Click(object sender, EventArgs e)
        {
            
            switch (stastic)
            {
                case 1:
            var app = new Application();
            var wb = app.Workbooks.Add();
            var ws = wb.Worksheets[1] as Worksheet;
            ws.Cells[1, 1].Value = "Отчет по тренировкам с " + dtpStart.Value.ToShortDateString() + " по " + dtpFinish.Value.ToShortDateString();
            ws.Cells[3, 1].Value = "Дата";
            ws.Cells[3, 2].Value = "Название тренировка";
            ws.Cells[3, 3].Value = "Количество посетивших";
            for (int i = 0; i < dataGridViewStatistics.ColumnCount; i++)
            {
                for (int j = 0; j < dataGridViewStatistics.RowCount; j++)
                {
                    if (dataGridViewStatistics[i, j].Value != null)
                        app.Cells[j + 4, i + 1] = (dataGridViewStatistics[i, j].Value).ToString();

                }
                app.Visible = true;
            }
            wb.SaveAs("Отчет по тренировкам с " + dtpStart.Value.ToShortDateString() + " по " + dtpFinish.Value.ToShortDateString() + ".xlsx");
            app.Quit();
                    break;

                case 2:
                    if (dataGridViewStatistics.Rows.Count != 0)
                    {
                        int RowCount = dataGridViewStatistics.Rows.Count;
                        int ColumnCount = dataGridViewStatistics.Columns.Count;
                        Object[,] DataArray = new object[RowCount + 1, ColumnCount + 1];

                        //add rows
                        int r = 0;
                        for (int c = 0; c <= ColumnCount - 1; c++)
                        {
                            for (r = 0; r <= RowCount - 1; r++)
                            {
                                DataArray[r, c] = dataGridViewStatistics.Rows[r].Cells[c].Value;
                            } //end row loop
                        } //end column loop

                        Word.Document oDoc = new Word.Document();
                        oDoc.Application.Visible = true;

                        //page orintation
                        oDoc.PageSetup.Orientation = Word.WdOrientation.wdOrientLandscape;


                        dynamic oRange = oDoc.Content.Application.Selection.Range;
                        string oTemp = "";
                        for (r = 0; r <= RowCount - 1; r++)
                        {
                            for (int c = 0; c <= ColumnCount - 1; c++)
                            {
                                oTemp = oTemp + DataArray[r, c] + "\t";

                            }
                        }

                        //table format
                        oRange.Text = oTemp;

                        object Separator = Word.WdTableFieldSeparator.wdSeparateByTabs;
                        object ApplyBorders = true;
                        object AutoFit = true;
                        object AutoFitBehavior = Word.WdAutoFitBehavior.wdAutoFitContent;

                        oRange.ConvertToTable(ref Separator, ref RowCount, ref ColumnCount,
                                              Type.Missing, Type.Missing, ref ApplyBorders,
                                              Type.Missing, Type.Missing, Type.Missing,
                                              Type.Missing, Type.Missing, Type.Missing,
                                              Type.Missing, ref AutoFit, ref AutoFitBehavior, Type.Missing);

                        oRange.Select();

                        oDoc.Application.Selection.Tables[1].Select();
                        oDoc.Application.Selection.Tables[1].Rows.AllowBreakAcrossPages = 0;
                        oDoc.Application.Selection.Tables[1].Rows.Alignment = 0;
                        oDoc.Application.Selection.Tables[1].Rows[1].Select();
                        oDoc.Application.Selection.InsertRowsAbove(1);
                        oDoc.Application.Selection.Tables[1].Rows[1].Select();

                        //header row style
                        oDoc.Application.Selection.Tables[1].Rows[1].Range.Bold = 1;
                        oDoc.Application.Selection.Tables[1].Rows[1].Range.Font.Name = "Tahoma";
                        oDoc.Application.Selection.Tables[1].Rows[1].Range.Font.Size = 14;

                        //add header row manually
                        for (int c = 0; c <= ColumnCount - 1; c++)
                        {
                            oDoc.Application.Selection.Tables[1].Cell(1, c + 1).Range.Text = dataGridViewStatistics.Columns[c].HeaderText;
                        }

                        //table style 
                        //oDoc.Application.Selection.Tables[1].set_Style("Grid Table 4 - Accent 5");
                        oDoc.Application.Selection.Tables[1].Rows[1].Select();
                        oDoc.Application.Selection.Cells.VerticalAlignment = Word.WdCellVerticalAlignment.wdCellAlignVerticalCenter;

                        //header text
                        foreach (Word.Section section in oDoc.Application.ActiveDocument.Sections)
                        {
                            Word.Range headerRange = section.Headers[Word.WdHeaderFooterIndex.wdHeaderFooterPrimary].Range;
                            headerRange.Fields.Add(headerRange, Word.WdFieldType.wdFieldPage);
                            headerRange.Text = "Отчет по клиентам с " + dtpStart.Value.ToShortDateString() + " по " + dtpFinish.Value.ToShortDateString() ;

                            headerRange.Font.Size = 16;
                            headerRange.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphCenter;
                        }

                        //save the file
                        oDoc.SaveAs("Отчет по клиентам с " + dtpStart.Value.ToShortDateString() + " по " + dtpFinish.Value.ToShortDateString() + ".docx");
                        //oDoc.ClosePrintPreview();

                    }
                    break;

        }
        }

        private void btnByClients_Click(object sender, EventArgs e)
        {
            lblStart.Visible = true;
            lblFinish.Visible = true;
            dtpFinish.Visible = true;
            btnGetData.Visible = true;
            dtpStart.Visible = true;
            lblCheck.Visible = false;
            stastic = 2;
            dataGridViewStatistics.ColumnCount = 3;
            dataGridViewStatistics.RowCount = 1;
            dataGridViewStatistics.Rows.Clear();
            dataGridViewStatistics.Columns.Clear();
        }

        private void btnByTreners_Click(object sender, EventArgs e)
        {
            lblStart.Visible = true;
            lblFinish.Visible = true;
            dtpFinish.Visible = true;
            dtpStart.Visible = true;
            btnGetData.Visible = true;
            lblCheck.Visible = false;
            stastic = 3;
        }
    }
}
