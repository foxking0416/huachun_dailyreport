using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing.Printing;
using System.Collections;
using MySql.Data.MySqlClient;

namespace HuaChun_DailyReport
{
    public partial class DailyReportIncreaseForm : Form
    {
        string dbHost;
        string dbUser;
        string dbPass;
        string dbName;
        protected MySQL SQL;
        private DataTable dataTableMaterial;
        private DataTable dataTableManPower;
        private DataTable dataTableTool;
        private DataTable dataTableOutsourcing;
        private DataTable dataTableVacation;

        public DailyReportIncreaseForm()
        {
            InitializeComponent();
            Initialize();
        }

        private void Initialize()
        {
            dbHost = AppSetting.LoadInitialSetting("DB_IP", "127.0.0.1");
            dbUser = AppSetting.LoadInitialSetting("DB_USER", "root");
            dbPass = AppSetting.LoadInitialSetting("DB_PASSWORD", "123");
            dbName = AppSetting.LoadInitialSetting("DB_NAME", "huachun");

            SQL = new MySQL(dbHost, dbUser, dbPass, dbName);
            this.comboBoxWeatherMorning.Items.Add("晴");
            this.comboBoxWeatherMorning.Items.Add("雨");
            this.comboBoxWeatherMorning.SelectedIndex = 0;
            this.comboBoxWeatherAfternoon.Items.Add("晴");
            this.comboBoxWeatherAfternoon.Items.Add("雨");
            this.comboBoxWeatherAfternoon.SelectedIndex = 0;
            this.comboBoxCondition.Items.Add("颱風");
            this.comboBoxCondition.Items.Add("豪雨");
            this.comboBoxCondition.Items.Add("停電");
            this.comboBoxCondition.Items.Add("停工");
            this.comboBoxCondition.Items.Add("雨後泥濘");
            this.comboBoxCondition.Items.Add("補假");
            this.comboBoxCondition.Items.Add("選舉");
            this.comboBoxNoCount.Items.Add("0");
            this.comboBoxNoCount.Items.Add("0.5");
            this.comboBoxNoCount.Items.Add("1");
            this.comboBoxNoCount.SelectedIndex = 0;

            ComputeDayOfWeek();


            ////////////////////////////////////////////////////////////////
            dataTableMaterial = new DataTable("MaterialTable");
            dataTableMaterial.Columns.Add("廠商編號", typeof(String));
            dataTableMaterial.Columns.Add("廠商名稱", typeof(String));
            dataTableMaterial.Columns.Add("料號", typeof(String));
            dataTableMaterial.Columns.Add("名稱", typeof(String));
            dataTableMaterial.Columns.Add("單位", typeof(String));
            dataTableMaterial.Columns.Add("位置", typeof(String));
            dataTableMaterial.Columns.Add("已進數量", typeof(String));
            dataTableMaterial.Columns.Add("本日進量", typeof(String));
            dataTableMaterial.Columns.Add("累計進數", typeof(String));
            dataTableMaterial.Columns.Add("已使用", typeof(String));
            dataTableMaterial.Columns.Add("本日用量", typeof(String));
            dataTableMaterial.Columns.Add("累計用量", typeof(String));
            dataTableMaterial.Columns.Add("庫存", typeof(String));
            //dataTableMaterial.Rows.Add(dataTableMaterial.NewRow());
            dataGridViewMaterial.DataSource = dataTableMaterial;
            dataGridViewMaterial.ReadOnly = false;
            dataGridViewMaterial.AllowUserToAddRows = false;
            dataGridViewMaterial.MultiSelect = false;
            dataGridViewMaterial.EditMode = DataGridViewEditMode.EditOnKeystroke;
            

            ////////////////////////////////////////////////////////////////
            dataTableManPower = new DataTable("ManPowerTable");
            dataTableManPower.Columns.Add("廠商編號", typeof(String));
            dataTableManPower.Columns.Add("廠商名稱", typeof(String));
            dataTableManPower.Columns.Add("工別編號", typeof(String));
            dataTableManPower.Columns.Add("工別名稱", typeof(String));
            dataTableManPower.Columns.Add("出工人數", typeof(String));
            dataTableManPower.Columns.Add("工時", typeof(String));
            dataTableManPower.Columns.Add("本日工數", typeof(String));
            dataTableManPower.Columns.Add("備註", typeof(String));
            //dataTableManPower.Rows.Add(dataTableManPower.NewRow());
            dataGridViewManPower.DataSource = dataTableManPower;
            dataGridViewManPower.ReadOnly = false;
            dataGridViewManPower.AllowUserToAddRows = false;
            dataGridViewManPower.MultiSelect = false;


            ////////////////////////////////////////////////////////////////
            dataTableTool = new DataTable("ToolTable");
            dataTableTool.Columns.Add("廠商編號", typeof(String));
            dataTableTool.Columns.Add("廠商名稱", typeof(String));
            dataTableTool.Columns.Add("機具編號", typeof(String));
            dataTableTool.Columns.Add("機具名稱", typeof(String));
            dataTableTool.Columns.Add("出工數", typeof(String));
            dataTableTool.Columns.Add("工時", typeof(String));
            dataTableTool.Columns.Add("本日工數", typeof(String));
            dataTableTool.Columns.Add("備註", typeof(String));
            dataTableTool.Rows.Add(dataTableTool.NewRow());
            dataGridViewTool.DataSource = dataTableTool;
            dataGridViewTool.ReadOnly = true;
            dataGridViewTool.AllowUserToAddRows = false;
            dataGridViewTool.MultiSelect = false;


            ////////////////////////////////////////////////////////////////
            dataTableOutsourcing = new DataTable("OutsourcingTable");
            dataTableOutsourcing.Columns.Add("廠商編號", typeof(String));
            dataTableOutsourcing.Columns.Add("廠商名稱", typeof(String));
            dataTableOutsourcing.Columns.Add("施工編號", typeof(String));
            dataTableOutsourcing.Columns.Add("施工名稱", typeof(String));
            dataTableOutsourcing.Columns.Add("單位", typeof(String));
            dataTableOutsourcing.Columns.Add("已出工", typeof(String));
            dataTableOutsourcing.Columns.Add("出工", typeof(String));
            dataTableOutsourcing.Columns.Add("累計出工", typeof(String));
            dataTableOutsourcing.Columns.Add("已施作", typeof(String));
            dataTableOutsourcing.Columns.Add("施作", typeof(String));
            dataTableOutsourcing.Columns.Add("累計施作", typeof(String));
            dataTableOutsourcing.Columns.Add("備註", typeof(String));
            //dataTableOutsourcing.Rows.Add(dataTableOutsourcing.NewRow());
            dataGridViewOutsourcing.DataSource = dataTableOutsourcing;
            dataGridViewOutsourcing.ReadOnly = false;
            dataGridViewOutsourcing.AllowUserToAddRows = false;
            dataGridViewOutsourcing.MultiSelect = false;


            ////////////////////////////////////////////////////////////////
            dataTableVacation = new DataTable("VacationTable");
            dataTableVacation.Columns.Add("員工編號", typeof(String));
            dataTableVacation.Columns.Add("姓名", typeof(String));
            dataTableVacation.Columns.Add("休假天數", typeof(String));
            dataTableVacation.Columns.Add("備註", typeof(String));
            //dataTableVacation.Rows.Add(dataTableVacation.NewRow());
            dataGridViewVacation.DataSource = dataTableVacation;
            dataGridViewVacation.ReadOnly = false;
            dataGridViewVacation.AllowUserToAddRows = false;
            dataGridViewVacation.MultiSelect = false;
        }

        private void ComputeDayOfWeek()
        {
            if (dateToday.Value.DayOfWeek == DayOfWeek.Sunday)
                this.textBoxWeekDay.Text = "日";
            else if (dateToday.Value.DayOfWeek == DayOfWeek.Monday)
                this.textBoxWeekDay.Text = "一";
            else if (dateToday.Value.DayOfWeek == DayOfWeek.Tuesday)
                this.textBoxWeekDay.Text = "二";
            else if (dateToday.Value.DayOfWeek == DayOfWeek.Wednesday)
                this.textBoxWeekDay.Text = "三";
            else if (dateToday.Value.DayOfWeek == DayOfWeek.Thursday)
                this.textBoxWeekDay.Text = "四";
            else if (dateToday.Value.DayOfWeek == DayOfWeek.Friday)
                this.textBoxWeekDay.Text = "五";
            else if (dateToday.Value.DayOfWeek == DayOfWeek.Saturday)
                this.textBoxWeekDay.Text = "六";
        }

        public void LoadProjectInfo(string projectNo) 
        {
            //專案編號
            this.textBoxProjectNo.Text = projectNo;
            //專案名稱   
            this.textBoxProjectName.Text = SQL.Read_SQL_data("project_name", "project_info", "project_no ='" + projectNo + "'");
            //開工日期
            string startDate = SQL.Read_SQL_data("startdate", "project_info", "project_no ='" + projectNo + "'");
            this.dateStart.Value = Functions.TransferSQLDateToDateTime(startDate);

            //契約工期
            this.textBoxContractDuration.Text = SQL.Read_SQL_data("contractduration", "project_info", "project_no ='" + projectNo + "'");
            //今日開始追加工期
            int accuextendduration = 0;
            int totalduration = 0;
            ArrayList extendDate = new ArrayList();
            string[] extendDates = SQL.Read1DArray_SQL_Data("extendstartdate", "extendduration", "project_no ='" + projectNo + "'");
            this.textBoxExtendDay.Text = "0";
            for (int i = 0; i < extendDates.Length; i++)
            {
                DateTime extDate = Functions.TransferSQLDateToDateTime(extendDates[i]);
                if (extDate.Date.CompareTo(dateToday.Value.Date) == 0)//為追加起始日
                {
                    this.textBoxExtendDay.Text = SQL.Read_SQL_data("extendduration", "extendduration", "project_no ='" + projectNo + "' AND extendstartdate = '" + Functions.GetDateTimeValue(dateToday.Value) + "'");
                }

                if (extDate.Date.CompareTo(dateToday.Value.Date) == 0 || extDate.Date.CompareTo(dateToday.Value.Date) == -1)//0為追加起始日   -1為開始日比今日日期早
                {
                    int extendDuration = Convert.ToInt32(SQL.Read_SQL_data("extendduration", "extendduration", "project_no = '" + projectNo + "' AND extendstartdate = '" + Functions.GetDateTimeValue(extDate) + "'"));
                    accuextendduration += extendDuration;
                }

            }
            //累計追加工期
            this.textBoxAccumulateExtend.Text = accuextendduration.ToString();
            //工期總計
            this.textBoxAccumulateDuration.Text = Convert.ToString(Convert.ToInt32(SQL.Read_SQL_data("contractduration", "project_info", "project_no ='" + projectNo + "'")) + accuextendduration);

            
            //契約天數
            this.textBoxContractDays.Text = SQL.Read_SQL_data("contractdays", "project_info", "project_no ='" + projectNo + "'");
            //追加工期後總計天數
            string computeType = SQL.Read_SQL_data("computetype", "project_info", "project_no = '" + projectNo + "'");
            string countHoliday = SQL.Read_SQL_data("holiday", "project_info", "project_no = '" + projectNo + "'");

            DayCompute dayCompute = new DayCompute(Functions.TransferSQLDateToDateTime(startDate), Convert.ToInt32(this.textBoxAccumulateDuration.Text));            


            if (computeType == "1" || computeType == "2")//限期完工  日曆天
            {
                this.textBoxAccumulateDays.Text = this.textBoxAccumulateDuration.Text;
            }
            else
            {
                if (computeType == "3")//工作天 無休
                {
                    dayCompute.countSaturday = false;
                    dayCompute.countSunday = false;
                }
                else if (computeType == "4")//工作天 周休一日
                {
                    dayCompute.countSaturday = false;
                    dayCompute.countSunday = true;
                }
                else if (computeType == "5")//工作天 周休二日
                {
                    dayCompute.countSaturday = true;
                    dayCompute.countSunday = true;
                }

                if (countHoliday == "1")
                    dayCompute.countHoliday = true;
                else if (countHoliday == "0")
                    dayCompute.countHoliday = false;

                DateTime FinishDate = dayCompute.CountByDuration(Functions.TransferSQLDateToDateTime(startDate), Convert.ToInt32(Convert.ToInt32(this.textBoxAccumulateDuration.Text)));

                this.textBoxAccumulateDays.Text = (FinishDate.Subtract(dateStart.Value).Days + 1).ToString();
            }

            //開工迄今工期



            //不計工期
            string[] reportDates = SQL.Read1DArray_SQL_Data("date", "dailyreport", "project_no = '" + projectNo + "'");
            int nonCountingDays = 0;
            for (int i = 0; i < reportDates.Length; i++)
            {
                if (Functions.TransferSQLDateToDateTime(reportDates[i]).CompareTo(dateToday.Value) == -1)
                {
                    string test = Functions.TransferSQLDateToDateOnly(reportDates[i]);
                    nonCountingDays += Convert.ToInt32(SQL.Read_SQL_data("nonecounting", "dailyreport", "project_no = '" + projectNo + "' AND date = '" + Functions.TransferSQLDateToDateOnly(reportDates[i]) + "'"));
                }
            }

            this.textBoxDayNotAccount.Text = nonCountingDays.ToString();
            //實際工期
            //剩餘工期  

            //開工迄今天數
            this.textBoxDayStartToCurrent.Text = (this.dateToday.Value.Date.Subtract(this.dateStart.Value.Date).Days + 1).ToString();
            //剩餘天數
            this.textBoxRestDays.Text = (this.dateProjectEnd_Contract.Value.Date.Subtract(this.dateToday.Value.Date).Days).ToString();
            
            
        
            
            

            
            //契約完工日
            string contractFinishDate = SQL.Read_SQL_data("contract_finishdate", "project_info", "project_no ='" + projectNo + "'");
            this.dateProjectEnd_Contract.Value = Functions.TransferSQLDateToDateTime(contractFinishDate);
        }


        private void btnSave_Click(object sender, EventArgs e)
        {
            string connStr = "server=" + dbHost + ";uid=" + dbUser + ";pwd=" + dbPass + ";database=" + dbName;
            MySqlConnection conn = new MySqlConnection(connStr);
            MySqlCommand command = conn.CreateCommand();
            conn.Open();


            label25.Visible = false;
            label26.Visible = false;
            if (textBoxProjectNo.Text == string.Empty)
                label26.Visible = true;
            if (textBoxProjectNo.Text == string.Empty)
                return;

            string[] sameDate = SQL.Read1DArray_SQL_Data("morning_weather", "dailyreport", "project_no = '" + textBoxProjectNo.Text + "' AND date = '" + Functions.GetDateTimeValue(dateToday.Value) + "'");
            if (sameDate.Length != 0)
            {
                label25.Visible = true;
                return;
            }

            string commandStrDailyReport = "Insert into dailyreport values('";
            commandStrDailyReport = commandStrDailyReport + textBoxProjectNo.Text + "','";
            commandStrDailyReport = commandStrDailyReport + Functions.GetDateTimeValue(dateToday.Value) + "','";
            commandStrDailyReport = commandStrDailyReport + comboBoxWeatherMorning.Text + "','";
            commandStrDailyReport = commandStrDailyReport + comboBoxWeatherAfternoon.Text + "','";
            commandStrDailyReport = commandStrDailyReport + textBoxInterference.Text + "','";
            commandStrDailyReport = commandStrDailyReport + comboBoxCondition.Text + "','";
            commandStrDailyReport = commandStrDailyReport + comboBoxNoCount.Text;
            commandStrDailyReport = commandStrDailyReport + "')";
            command.CommandText = commandStrDailyReport;
            command.ExecuteNonQuery();

            //儲存材料使用數量資料進SQL
            #region 
            for (int i = 0; i < dataGridViewMaterial.RowCount; i++)
            {
                string commandStr = "Insert into dailyreport_material(";
                commandStr = commandStr + "project_no,";
                commandStr = commandStr + "date,";
                commandStr = commandStr + "data_index,";
                commandStr = commandStr + "vendor_no,";
                commandStr = commandStr + "vendor_name,";
                commandStr = commandStr + "material_no,";
                commandStr = commandStr + "material_name,";
                commandStr = commandStr + "unit,";
                commandStr = commandStr + "location,";
                commandStr = commandStr + "amount_past,";
                commandStr = commandStr + "amount_today,";
                commandStr = commandStr + "amount_all,";
                commandStr = commandStr + "used_past,";
                commandStr = commandStr + "used_today,";
                commandStr = commandStr + "used_all,";
                commandStr = commandStr + "storage";
                commandStr = commandStr + ") values('";
                commandStr = commandStr + textBoxProjectNo.Text + "','";
                commandStr = commandStr + Functions.GetDateTimeValue(dateToday.Value) + "','";
                commandStr = commandStr + i + "','";
                commandStr = commandStr + dataGridViewMaterial.Rows[i].Cells[0].Value + "','";//廠商編號
                commandStr = commandStr + dataGridViewMaterial.Rows[i].Cells[1].Value + "','";//廠商名稱
                commandStr = commandStr + dataGridViewMaterial.Rows[i].Cells[2].Value + "','";//材料編號
                commandStr = commandStr + dataGridViewMaterial.Rows[i].Cells[3].Value + "','";//材料名稱
                commandStr = commandStr + dataGridViewMaterial.Rows[i].Cells[4].Value + "','";//單位
                commandStr = commandStr + dataGridViewMaterial.Rows[i].Cells[5].Value + "','";//地點
                commandStr = commandStr + dataGridViewMaterial.Rows[i].Cells[6].Value + "','";//已進數量
                commandStr = commandStr + dataGridViewMaterial.Rows[i].Cells[7].Value + "','";//本日進量
                commandStr = commandStr + dataGridViewMaterial.Rows[i].Cells[8].Value + "','";//累計進數
                commandStr = commandStr + dataGridViewMaterial.Rows[i].Cells[9].Value + "','";//已使用
                commandStr = commandStr + dataGridViewMaterial.Rows[i].Cells[10].Value + "','";//本日用量
                commandStr = commandStr + dataGridViewMaterial.Rows[i].Cells[11].Value + "','";//累計用量
                commandStr = commandStr + dataGridViewMaterial.Rows[i].Cells[12].Value + "')";//庫存

                command.CommandText = commandStr;
                command.ExecuteNonQuery();

            }
            #endregion

            //儲存出工人數資料進SQL
            #region 
            for (int i = 0; i < dataGridViewManPower.RowCount; i++)
            {
                string commandStr = "Insert into dailyreport_manpower(";
                commandStr = commandStr + "project_no,";
                commandStr = commandStr + "date,";
                commandStr = commandStr + "data_index,";
                commandStr = commandStr + "vendor_no,";
                commandStr = commandStr + "vendor_name,";
                commandStr = commandStr + "manpower_no,";
                commandStr = commandStr + "manpower_name,";
                commandStr = commandStr + "amount,";
                commandStr = commandStr + "hour,";
                commandStr = commandStr + "amount_today,";
                commandStr = commandStr + "ps";
                commandStr = commandStr + ") values('";
                commandStr = commandStr + textBoxProjectNo.Text + "','";
                commandStr = commandStr + Functions.GetDateTimeValue(dateToday.Value) + "','";
                commandStr = commandStr + i + "','";
                commandStr = commandStr + dataGridViewManPower.Rows[i].Cells[0].Value + "','";//廠商編號
                commandStr = commandStr + dataGridViewManPower.Rows[i].Cells[1].Value + "','";//廠商名稱
                commandStr = commandStr + dataGridViewManPower.Rows[i].Cells[2].Value + "','";//工別編號
                commandStr = commandStr + dataGridViewManPower.Rows[i].Cells[3].Value + "','";//工別名稱
                commandStr = commandStr + dataGridViewManPower.Rows[i].Cells[4].Value + "','";//出工人數
                commandStr = commandStr + dataGridViewManPower.Rows[i].Cells[5].Value + "','";//工時
                commandStr = commandStr + dataGridViewManPower.Rows[i].Cells[6].Value + "','";//本日工數
                commandStr = commandStr + dataGridViewManPower.Rows[i].Cells[7].Value + "')";//備註

                command.CommandText = commandStr;
                command.ExecuteNonQuery();
            }
            #endregion

            //儲存機具使用資料進SQL
            #region 
            for (int i = 0; i < dataGridViewTool.RowCount; i++)
            {
                string commandStr = "Insert into dailyreport_tool(";
                commandStr = commandStr + "project_no,";
                commandStr = commandStr + "date,";
                commandStr = commandStr + "data_index,";
                commandStr = commandStr + "vendor_no,";
                commandStr = commandStr + "vendor_name,";
                commandStr = commandStr + "tool_no,";
                commandStr = commandStr + "tool_name,";
                commandStr = commandStr + "amount,";
                commandStr = commandStr + "hour,";
                commandStr = commandStr + "amount_today,";
                commandStr = commandStr + "ps";
                commandStr = commandStr + ") values('";
                commandStr = commandStr + textBoxProjectNo.Text + "','";
                commandStr = commandStr + Functions.GetDateTimeValue(dateToday.Value) + "','";
                commandStr = commandStr + i + "','";
                commandStr = commandStr + dataGridViewTool.Rows[i].Cells[0].Value + "','";//廠商編號
                commandStr = commandStr + dataGridViewTool.Rows[i].Cells[1].Value + "','";//廠商名稱
                commandStr = commandStr + dataGridViewTool.Rows[i].Cells[2].Value + "','";//機具編號
                commandStr = commandStr + dataGridViewTool.Rows[i].Cells[3].Value + "','";//機具名稱
                commandStr = commandStr + dataGridViewTool.Rows[i].Cells[4].Value + "','";//出工數
                commandStr = commandStr + dataGridViewTool.Rows[i].Cells[5].Value + "','";//工時
                commandStr = commandStr + dataGridViewTool.Rows[i].Cells[6].Value + "','";//本日工數
                commandStr = commandStr + dataGridViewTool.Rows[i].Cells[7].Value + "')";//備註
            }
            #endregion

            //儲存外包項目資料進SQL
            #region
            for (int i = 0; i < dataGridViewOutsourcing.RowCount; i++)
            {
                string commandStr = "Insert into dailyreport_outsourcing(";
                commandStr = commandStr + "project_no,";
                commandStr = commandStr + "date,";
                commandStr = commandStr + "data_index,";
                commandStr = commandStr + "vendor_no,";
                commandStr = commandStr + "vendor_name,";
                commandStr = commandStr + "process_no,";
                commandStr = commandStr + "process_name,";
                commandStr = commandStr + "unit,";
                commandStr = commandStr + "dispatch_past,";
                commandStr = commandStr + "dispatch_today,";
                commandStr = commandStr + "dispatch_all,";
                commandStr = commandStr + "build_past,";
                commandStr = commandStr + "build_today,";
                commandStr = commandStr + "build_all,";
                commandStr = commandStr + "ps";
                commandStr = commandStr + ") values('";
                commandStr = commandStr + textBoxProjectNo.Text + "','";
                commandStr = commandStr + Functions.GetDateTimeValue(dateToday.Value) + "','";
                commandStr = commandStr + i + "','";
                commandStr = commandStr + dataGridViewOutsourcing.Rows[i].Cells[0].Value + "','";//廠商編號
                commandStr = commandStr + dataGridViewOutsourcing.Rows[i].Cells[1].Value + "','";//廠商名稱
                commandStr = commandStr + dataGridViewOutsourcing.Rows[i].Cells[2].Value + "','";//施工編號
                commandStr = commandStr + dataGridViewOutsourcing.Rows[i].Cells[3].Value + "','";//施工名稱
                commandStr = commandStr + dataGridViewOutsourcing.Rows[i].Cells[4].Value + "','";//單位
                commandStr = commandStr + dataGridViewOutsourcing.Rows[i].Cells[5].Value + "','";//已出工
                commandStr = commandStr + dataGridViewOutsourcing.Rows[i].Cells[6].Value + "','";//出工
                commandStr = commandStr + dataGridViewOutsourcing.Rows[i].Cells[7].Value + "','";//累計出工
                commandStr = commandStr + dataGridViewOutsourcing.Rows[i].Cells[8].Value + "','";//已施作
                commandStr = commandStr + dataGridViewOutsourcing.Rows[i].Cells[9].Value + "','";//施作
                commandStr = commandStr + dataGridViewOutsourcing.Rows[i].Cells[10].Value + "','";//累計施作
                commandStr = commandStr + dataGridViewOutsourcing.Rows[i].Cells[11].Value + "')";//備註
            }
            #endregion

            conn.Close();
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnProjectSelect_Click(object sender, EventArgs e)
        {
            ProjectSearchForm form = new ProjectSearchForm(this);
            form.ShowDialog();
        }

        private void dateToday_ValueChanged(object sender, EventArgs e)
        {
            ComputeDayOfWeek();
            //開工迄今
            this.textBoxDayStartToCurrent.Text = (this.dateToday.Value.Date.Subtract(this.dateStart.Value.Date).Days+1).ToString();
            //剩餘天數
            this.textBoxRestDays.Text = (this.dateProjectEnd_Contract.Value.Date.Subtract(this.dateToday.Value.Date).Days).ToString();
        }

        private void dataGridViewMaterial_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            //e.ColumnIndex 0: 廠商編號
            //e.ColumnIndex 1: 廠商名稱
            //e.ColumnIndex 2: 材料編號
            //e.ColumnIndex 3: 材料名稱
            //e.ColumnIndex 4: 單位
            if (e.ColumnIndex == 0 || e.ColumnIndex == 1)
            {
                VendorSearchForm vendorSearchForm = new VendorSearchForm(this, 0, e.RowIndex, 0);
                vendorSearchForm.ShowDialog();
            }
            else if (e.ColumnIndex == 2 || e.ColumnIndex == 3 || e.ColumnIndex == 4)
            {
                MaterialSearchForm materialSearchForm = new MaterialSearchForm(this, e.RowIndex, 2);
                materialSearchForm.ShowDialog();

            }
        }

        public void SetDataGridViewValue(int dataGridViewIndex, string value, int column, int row)
        {
            switch(dataGridViewIndex)
            {
                case 0://材料使用數量
                    dataGridViewMaterial.Rows[row].Cells[column].Value = value;
                    dataGridViewMaterial.EndEdit();
                    break;
                case 1://出工人數
                    dataGridViewManPower.Rows[row].Cells[column].Value = value;
                    dataGridViewManPower.EndEdit();
                    break;
                case 2://機具使用
                    dataGridViewTool.Rows[row].Cells[column].Value = value;
                    dataGridViewTool.EndEdit();
                    break;
                case 3://外包項目
                    dataGridViewOutsourcing.Rows[row].Cells[column].Value = value;
                    dataGridViewOutsourcing.EndEdit();
                    break;
                case 4://休假紀錄
                    dataGridViewVacation.Rows[row].Cells[column].Value = value;
                    dataGridViewVacation.EndEdit();
                    break;
            }
        }



        private void dataGridViewManPower_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            //e.ColumnIndex 0: 廠商編號
            //e.ColumnIndex 1: 廠商名稱
            //e.ColumnIndex 2: 工別編號
            //e.ColumnIndex 3: 工別名稱
            if (e.ColumnIndex == 0 || e.ColumnIndex == 1)
            {
                VendorSearchForm vendorSearchForm = new VendorSearchForm(this, 1, e.RowIndex, 0);
                vendorSearchForm.ShowDialog();
            }
            else if (e.ColumnIndex == 2 || e.ColumnIndex == 3)
            {
                LaborSearchForm laborSearchForm = new LaborSearchForm(this, 1, e.RowIndex, 2);
                laborSearchForm.ShowDialog();

            }
        }

        private void dataGridViewTool_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            //e.ColumnIndex 0: 廠商編號
            //e.ColumnIndex 1: 廠商名稱
            //e.ColumnIndex 2: 機具編號
            //e.ColumnIndex 3: 機具名稱
            if (e.ColumnIndex == 0 || e.ColumnIndex == 1)
            {
                VendorSearchForm vendorSearchForm = new VendorSearchForm(this, 2, e.RowIndex, 0);
                vendorSearchForm.ShowDialog();
            }
            else if (e.ColumnIndex == 2 || e.ColumnIndex == 3)
            {
                ToolSearchForm toolSearchForm = new ToolSearchForm(this, 2, e.RowIndex, 2);
                toolSearchForm.ShowDialog();

            }
        }

        private void dataGridViewOutsourcing_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            //e.ColumnIndex 0: 廠商編號
            //e.ColumnIndex 1: 廠商名稱
            //e.ColumnIndex 2: 施工編號
            //e.ColumnIndex 3: 施工名稱
            //e.ColumnIndex 4: 單位
            if (e.ColumnIndex == 0 || e.ColumnIndex == 1)
            {
                VendorSearchForm vendorSearchForm = new VendorSearchForm(this, 3, e.RowIndex, 0);
                vendorSearchForm.ShowDialog();
            }
            else if (e.ColumnIndex == 2 || e.ColumnIndex == 3 || e.ColumnIndex == 4)
            {
                ProcessCodeSearchForm processCodeSearchForm = new ProcessCodeSearchForm(this, 3, e.RowIndex, 2);
                processCodeSearchForm.ShowDialog();

            }
        }

        private void dataGridViewVacation_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            //e.ColumnIndex 0: 員工編號
            //e.ColumnIndex 1: 員工名稱
            if (e.ColumnIndex == 0 || e.ColumnIndex == 1)
            {
                MemberSearchForm memberSearchForm = new MemberSearchForm(this, 4, e.RowIndex, 0);
                memberSearchForm.ShowDialog();
            }
        }

        private void btnAddData_Click(object sender, EventArgs e)
        {
            switch (tabControl1.SelectedIndex)
            {
                case 0://材料使用數量
                    dataTableMaterial.Rows.Add(dataTableMaterial.NewRow());
                    dataGridViewMaterial.Columns[8].ReadOnly = true;//累計進數為唯讀
                    dataGridViewMaterial.Columns[11].ReadOnly = true;//累計用量為唯讀
                    break;
                case 1://出工人數
                    dataTableManPower.Rows.Add(dataTableManPower.NewRow());
                    break;
                case 2://機具使用
                    dataTableTool.Rows.Add(dataTableTool.NewRow());
                    break;
                case 3://外包項目
                    dataTableOutsourcing.Rows.Add(dataTableOutsourcing.NewRow());
                    break;
                case 4://休假紀錄
                    dataTableVacation.Rows.Add(dataTableVacation.NewRow());
                    break;
                case 5://其他
                    break;
            }
        }

        private void btnDeleteData_Click(object sender, EventArgs e)
        {
            switch (tabControl1.SelectedIndex)
            {
                case 0://材料使用數量
                    dataTableMaterial.Rows.RemoveAt(dataGridViewMaterial.CurrentCell.RowIndex);
                    break;
                case 1://出工人數
                    dataTableManPower.Rows.RemoveAt(dataGridViewManPower.CurrentCell.RowIndex);
                    break;
                case 2://機具使用
                    dataTableTool.Rows.RemoveAt(dataGridViewTool.CurrentCell.RowIndex);
                    break;
                case 3://外包項目
                    dataTableOutsourcing.Rows.RemoveAt(dataGridViewOutsourcing.CurrentCell.RowIndex);
                    break;
                case 4://休假紀錄
                    dataTableVacation.Rows.RemoveAt(dataGridViewVacation.CurrentCell.RowIndex);
                    break;
                case 5://其他
                    break;
            }
        }















    }
}
