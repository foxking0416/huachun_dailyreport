using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using Microsoft.Office.Core;
using Excel = Microsoft.Office.Interop.Excel;
using System.Runtime.InteropServices;

namespace HuaChun_DailyReport
{
    public partial class QueryFinishForm : Form
    {
        string dbHost;
        string dbUser;
        string dbPass;
        string dbName;
        protected MySQL SQL;
        private DataTable dataTableStatistic;
        DateTime DTStartDate;
        string g_ProjectNo;

        public QueryFinishForm(string projectNo)
        {
            InitializeComponent();
            Initialize();
            LoadProjectInfo(projectNo);
        }

        private void Initialize()
        {
            dbHost = AppSetting.LoadInitialSetting("DB_IP", "127.0.0.1");
            dbUser = AppSetting.LoadInitialSetting("DB_USER", "root");
            dbPass = AppSetting.LoadInitialSetting("DB_PASSWORD", "123");
            dbName = AppSetting.LoadInitialSetting("DB_NAME", "huachun");
            SQL = new MySQL(dbHost, dbUser, dbPass, dbName);

            dataTableStatistic = new DataTable("FinishStatisticTable");
            dataTableStatistic.Columns.Add("日期", typeof(String));
            dataTableStatistic.Columns.Add("開工迄今", typeof(String));
            dataTableStatistic.Columns.Add("星期", typeof(String));
            dataTableStatistic.Columns.Add("節日", typeof(String));
            dataTableStatistic.Columns.Add("上午天氣", typeof(String));
            dataTableStatistic.Columns.Add("下午天氣", typeof(String));
            dataTableStatistic.Columns.Add("上午人為因素", typeof(String));
            dataTableStatistic.Columns.Add("下午人為因素", typeof(String));
            dataTableStatistic.Columns.Add("本日不計工期", typeof(String));
            dataTableStatistic.Columns.Add("累計不計工期", typeof(String));
            dataTableStatistic.Columns.Add("累計工期", typeof(String));
            dataTableStatistic.Columns.Add("原剩餘工期", typeof(String));
            dataTableStatistic.Columns.Add("原剩餘天數", typeof(String));
            dataTableStatistic.Columns.Add("原完工日", typeof(String));

            dataTableStatistic.Columns.Add("追加工期", typeof(String));

            dataTableStatistic.Columns.Add("變動剩餘工期", typeof(String));
            dataTableStatistic.Columns.Add("變動剩餘天數", typeof(String));
            dataTableStatistic.Columns.Add("變動完工日", typeof(String));
            dataTableStatistic.Columns.Add("原百分比", typeof(String));
            dataGridView1.DataSource = dataTableStatistic;
            dataGridView1.ReadOnly = false;
            dataGridView1.AllowUserToAddRows = false;
            dataGridView1.MultiSelect = false;
            dataGridView1.EditMode = DataGridViewEditMode.EditOnKeystroke;


            dataTableStatistic.Columns[3].AllowDBNull = true;
        }

        public void LoadProjectInfo(string number)
        {
            g_ProjectNo = number;
            dataTableStatistic.Clear();


            DayCompute dayCompute = new DayCompute();
            string computeType = SQL.Read_SQL_data("computetype", "project_info", "project_no = '" + g_ProjectNo + "'");
            string countHoliday = SQL.Read_SQL_data("holiday", "project_info", "project_no = '" + g_ProjectNo + "'");


            if (computeType == "1")//限期完工  日曆天
            {
                this.label1.Text = "工期計算方式為限期完工";
            }
            else if (computeType == "2")
            {
                this.label1.Text = "工期計算方式為日曆天";
            }
            else if (computeType == "3")//工作天 無休
            {
                dayCompute.restOnSaturday = false;
                dayCompute.restOnSunday = false;
                this.label1.Text = "工期計算方式為工作工，無週休二日";
            }
            else if (computeType == "4")//工作天 周休一日
            {
                dayCompute.restOnSaturday = false;
                dayCompute.restOnSunday = true;
                this.label1.Text = "工期計算方式為工作工，週休一日";
            }
            else if (computeType == "5")//工作天 周休二日
            {
                dayCompute.restOnSaturday = true;
                dayCompute.restOnSunday = true;
                this.label1.Text = "工期計算方式為工作工，週休二日";
            }

            if (countHoliday == "1")
            {
                dayCompute.restOnHoliday = true;
                this.label1.Text += "，國定假日不施工";
            }
            else if (countHoliday == "0")
            {
                dayCompute.restOnHoliday = false;
                this.label1.Text += "，國定假日照常施工";
            }

            string rainyDayCountType = SQL.Read_SQL_data("rainyday", "project_info", "project_no = '" + g_ProjectNo + "'");
            if (rainyDayCountType == "1")
            {
                this.label3.Text += "需豪雨才不計工期";
            }
            else if (rainyDayCountType == "0")
            {
                this.label3.Text += "下雨即不計工期";
            }

            float originalTotalDuration = Convert.ToSingle(SQL.Read_SQL_data("contractduration", "project_info", "project_no = '" + g_ProjectNo + "'"));
            float originalTotalDays = Convert.ToSingle(SQL.Read_SQL_data("contractdays", "project_info", "project_no = '" + g_ProjectNo + "'"));
            DateTime originalFinishDate = Functions.TransferSQLDateToDateTime(SQL.Read_SQL_data("contract_finishdate", "project_info", "project_no = '" + g_ProjectNo + "'"));
            string[] extendDurationStartDates = SQL.Read1DArray_SQL_Data("extendstartdate", "extendduration", "project_no = '" + g_ProjectNo + "'");
            float accumulateExtendDurations = 0;

            string startDate = SQL.Read_SQL_data("startdate", "project_info", "project_no = '" + g_ProjectNo + "'");
            DTStartDate = Functions.TransferSQLDateToDateTime(startDate);
            DataRow dataRow;

            int i = 0;
            bool stop = false;
            while (!stop)
            {

                DateTime dateToday = DTStartDate.AddDays(i);

                dataRow = dataTableStatistic.NewRow();


                dataRow["日期"] = dateToday.ToString("yyyy/MM/dd");
                dataRow["開工迄今"] = (i + 1).ToString();
                dataRow["星期"] = Functions.ComputeDayOfWeek(dateToday);
                //Image img = Image.FromFile("D:\\12Small.jpg");
                //dataRow["農曆"] = imageToByteArray(img);
                dataRow["節日"] = dayCompute.GetCondition(dateToday);
                string morningWeather = SQL.Read_SQL_data("morning_weather", "dailyreport", "project_no = '" + g_ProjectNo + "' AND date = '" + Functions.TransferDateTimeToSQL(dateToday) + "'");
                dataRow["上午天氣"] = (morningWeather == string.Empty) ? "無資料" : morningWeather;
                string afternoonWeather = SQL.Read_SQL_data("afternoon_weather", "dailyreport", "project_no = '" + g_ProjectNo + "' AND date = '" + Functions.TransferDateTimeToSQL(dateToday) + "'");
                dataRow["下午天氣"] = (afternoonWeather == string.Empty) ? "無資料" : afternoonWeather;
                string morningCondition = SQL.Read_SQL_data("morning_condition", "dailyreport", "project_no = '" + g_ProjectNo + "' AND date = '" + Functions.TransferDateTimeToSQL(dateToday) + "'");
                dataRow["上午人為因素"] = (morningCondition == string.Empty) ? "無資料" : morningCondition;
                string afternoonCondition = SQL.Read_SQL_data("afternoon_condition", "dailyreport", "project_no = '" + g_ProjectNo + "' AND date = '" + Functions.TransferDateTimeToSQL(dateToday) + "'");
                dataRow["下午人為因素"] = (afternoonCondition == string.Empty) ? "無資料" : afternoonCondition;
                
                
                string nonCountingToday = SQL.Read_SQL_data("nonecounting", "dailyreport", "project_no = '" + g_ProjectNo + "' AND date = '" + Functions.TransferDateTimeToSQL(dateToday) + "'");



                if (nonCountingToday == "0.5")
                    dayCompute.AddNotWorking(dateToday, 0);
                else if (nonCountingToday == "1")
                {
                    dayCompute.AddNotWorking(dateToday, 0);
                    dayCompute.AddNotWorking(dateToday, 1);
                }

                dataRow["本日不計工期"] = dayCompute.GetWorkingDayNonCounting(dateToday);

                float nonCountingTotal = dayCompute.CountTotalNotWorkingDay(DTStartDate, dateToday);

                dataRow["累計不計工期"] = nonCountingTotal;
                dataRow["累計工期"] = i + 1 - nonCountingTotal;


                dataRow["原剩餘工期"] = originalTotalDuration - 1 - i + dayCompute.CountNotWorkingDayWithoutEverydayCondition(DTStartDate, dateToday);
                dataRow["原剩餘天數"] = originalTotalDays - 1 - i;
                dataRow["原完工日"] = originalFinishDate.ToString("yyyy/MM/dd");


                string extendDuration = SQL.Read_SQL_data("extendduration", "extendduration", "project_no = '" + g_ProjectNo + "' AND extendstartdate = '" + Functions.TransferDateTimeToSQL(dateToday) + "'");
                if (extendDuration != string.Empty)
                {
                    accumulateExtendDurations += Convert.ToSingle(extendDuration);
                    dataRow["追加工期"] = extendDuration;
                }

                float modifiedRestDuration = originalTotalDuration - 1 - i + nonCountingTotal + accumulateExtendDurations;
                dataRow["變動剩餘工期"] = modifiedRestDuration;


                DateTime modifiedFinishDate = dayCompute.CountByDuration(dateToday.AddDays(1), modifiedRestDuration);
                dataRow["變動完工日"] = modifiedFinishDate.ToString("yyyy/MM/dd");
                if (dateToday.CompareTo(modifiedFinishDate) == 0)
                    stop = true;
                dataRow["變動剩餘天數"] = modifiedFinishDate.Subtract(dateToday).Days;

                dataRow["原百分比"] = "";
                dataTableStatistic.Rows.Add(dataRow);
                i++;
            }
            //dataGridView1.Rows[0].DefaultCellStyle.BackColor = Color.Red;
        }

        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            DateTime dateClick = DTStartDate.AddDays(e.RowIndex);
            string morningWeather = SQL.Read_SQL_data("morning_weather", "dailyreport", "project_no = '" + g_ProjectNo + "' AND date = '" + Functions.TransferDateTimeToSQL(dateClick) + "'");
            if (morningWeather == string.Empty)//表示這天沒有日報表
            {
                DailyReportIncreaseForm reportBuildForm = new DailyReportIncreaseForm(false);
                reportBuildForm.LoadProjectInfo(g_ProjectNo);
                reportBuildForm.SetDateTodayValue(dateClick);
                reportBuildForm.ShowDialog();
                LoadProjectInfo(g_ProjectNo);
            }
            else//表示這天已經有日報表
            {
                DailyReportEditForm reportEditForm = new DailyReportEditForm(g_ProjectNo);
                reportEditForm.LoadProjectInfo(g_ProjectNo);
                reportEditForm.SetDateTodayValue(dateClick);
                reportEditForm.ShowDialog();
                LoadProjectInfo(g_ProjectNo);
            }

        }

        public byte[] imageToByteArray(System.Drawing.Image imageIn)
        {
            MemoryStream ms = new MemoryStream();
            imageIn.Save(ms, System.Drawing.Imaging.ImageFormat.Gif);
            return ms.ToArray();
        }

        private void btnOutput_Click(object sender, EventArgs e)
        {

            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "Excel File|*.xls";
            saveFileDialog.Title = "Save an Excel File";
            saveFileDialog.ShowDialog();

            if (saveFileDialog.FileName != "")
            {
                Excel.Workbook xlWorkBook;
                Excel.Worksheet xlWorkSheet;
                string path = Directory.GetCurrentDirectory();
                var xlApp = new Excel.Application();
                xlWorkBook = xlApp.Workbooks.Open(path + "\\完工總表.xls");
                xlWorkSheet = xlWorkBook.Sheets[1];

                string strDate = dataTableStatistic.Rows[i][0].ToString();
                DateTime dtDate = Functions.TransferSQLDateToDateTime(strDate);

                for (int i = 0; i < dataTableStatistic.Rows.Count; i++)
                {
                    for (int j = 0; j < dataTableStatistic.Columns.Count; j++)
                    {
                        string strDate = dataTableStatistic.Rows[i][0].ToString();
                        DateTime dtDate = Functions.TransferSQLDateToDateTime(strDate);

                        xlWorkSheet.Cells[i + 2, j + 1] = dataTableStatistic.Rows[i][j].ToString();






                    }
                }

                xlWorkBook.SaveAs(saveFileDialog.FileName);
                xlWorkBook.Close(true);
                xlApp.Quit();

                Marshal.ReleaseComObject(xlApp);
            }

            
        }
    }
}
