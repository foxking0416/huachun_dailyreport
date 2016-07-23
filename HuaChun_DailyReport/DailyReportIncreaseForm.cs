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
        protected string dbHost;
        protected string dbUser;
        protected string dbPass;
        protected string dbName;
        protected MySQL SQL;
        protected DataTable dataTableMaterial;
        protected DataTable dataTableManPower;
        protected DataTable dataTableTool;
        protected DataTable dataTableOutsourcing;
        protected DataTable dataTableVacation;
        protected string g_ProjectNumber;
        protected string g_StartDate;
        protected string g_ComputeType;
        protected string g_CountHoliday;
        protected string g_RainydayCountType;
        protected bool g_LockDate = false;

        public DailyReportIncreaseForm()
        {
            InitializeComponent();
            Initialize();
        }

        public DailyReportIncreaseForm(string projectNo)
        {
            InitializeComponent();
            Initialize();
            LoadProjectInfo(projectNo);
        }

        public DailyReportIncreaseForm(bool lockDate)
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
            this.comboBoxWeatherMorning.Items.Add("豪雨");
            this.comboBoxWeatherMorning.Items.Add("颱風");
            this.comboBoxWeatherMorning.Items.Add("酷熱");       
            this.comboBoxWeatherMorning.SelectedIndex = 0;
            this.comboBoxWeatherAfternoon.Items.Add("晴");
            this.comboBoxWeatherAfternoon.Items.Add("雨");
            this.comboBoxWeatherAfternoon.Items.Add("豪雨");
            this.comboBoxWeatherAfternoon.Items.Add("颱風");
            this.comboBoxWeatherAfternoon.Items.Add("酷熱");
            this.comboBoxWeatherAfternoon.SelectedIndex = 0;

            this.comboBoxConditionMorning.Items.Add("無");
            this.comboBoxConditionMorning.Items.Add("停電");
            this.comboBoxConditionMorning.Items.Add("停工");
            this.comboBoxConditionMorning.Items.Add("補假");
            this.comboBoxConditionMorning.Items.Add("選舉");
            this.comboBoxConditionMorning.Items.Add("雨後泥濘");
            this.comboBoxConditionMorning.SelectedIndex = 0;
            this.comboBoxConditionAfternoon.Items.Add("無");
            this.comboBoxConditionAfternoon.Items.Add("停電");
            this.comboBoxConditionAfternoon.Items.Add("停工");
            this.comboBoxConditionAfternoon.Items.Add("補假");
            this.comboBoxConditionAfternoon.Items.Add("選舉");
            this.comboBoxConditionAfternoon.Items.Add("雨後泥濘");
            this.comboBoxConditionAfternoon.SelectedIndex = 0;

            this.comboBoxNoCount.Items.Add("0");
            this.comboBoxNoCount.Items.Add("0.5");
            this.comboBoxNoCount.Items.Add("1");
            this.comboBoxNoCount.SelectedIndex = 0;

            this.comboBoxNoCountByType.Items.Add("0");
            this.comboBoxNoCountByType.Items.Add("1");
            this.comboBoxNoCountByType.SelectedIndex = 0;
            ComputeDayOfWeek();


            ///////////////////////////材料使用//////////////////////////////
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
            dataGridViewVacation.DataSource = dataTableVacation;
            dataGridViewVacation.ReadOnly = false;
            dataGridViewVacation.AllowUserToAddRows = false;
            dataGridViewVacation.MultiSelect = false;
        }

        //計算星期幾
        protected void ComputeDayOfWeek()
        {
            this.textBoxWeekDay.Text = Functions.ComputeDayOfWeek(dateToday.Value);

        }

        public virtual void LoadProjectInfo(string projectNo) 
        {
            //專案編號
            this.textBoxProjectNo.Text = projectNo;
            g_ProjectNumber = projectNo;
            if (this.textBoxProjectNo.Text != string.Empty)
            {
                EnableAll();
            }
            else
            {
                return;
            }
            
            
            
            //專案名稱   
            this.textBoxProjectName.Text = SQL.Read_SQL_data("project_name", "project_info", "project_no ='" + projectNo + "'");
            //開工日期
            g_StartDate = SQL.Read_SQL_data("startdate", "project_info", "project_no ='" + projectNo + "'");
            this.dateStart.Value = Functions.TransferSQLDateToDateTime(g_StartDate);
            //契約工期
            this.textBoxContractDuration.Text = SQL.Read_SQL_data("contractduration", "project_info", "project_no ='" + projectNo + "'");
            //契約天數
            this.textBoxContractDays.Text = SQL.Read_SQL_data("contractdays", "project_info", "project_no ='" + projectNo + "'");

            if (SQL.Read_SQL_data("date", "dailyreport", "project_no = '" + projectNo + "'") != string.Empty)
            {
                DateTime lastInputDate = Functions.TransferSQLDateToDateTime(SQL.Read_SQL_data("date", "dailyreport", "project_no = '" + projectNo + "' ORDER BY date DESC"));
                this.dateToday.Value = lastInputDate.AddDays(1);
            }

            LoadInfoByDate(g_ProjectNumber);

            //追加工期後總計天數
            g_ComputeType = SQL.Read_SQL_data("computetype", "project_info", "project_no = '" + projectNo + "'");
            g_CountHoliday = SQL.Read_SQL_data("holiday", "project_info", "project_no = '" + projectNo + "'");
            g_RainydayCountType = SQL.Read_SQL_data("rainyday", "project_info", "project_no = '" + projectNo + "'");

            if (g_ProjectNumber != null && g_ComputeType != null && g_CountHoliday != null && g_StartDate != null)
            Compute(projectNo, g_ComputeType, g_CountHoliday, g_StartDate);

        }

        private void LoadInfoByDate(string projectNo)
        {
            //今日開始追加工期
            int accuextendduration = 0;
            ArrayList extendDate = new ArrayList();
            string[] extendDates = SQL.Read1DArray_SQL_Data("extendstartdate", "extendduration", "project_no ='" + projectNo + "'");
            this.textBoxExtendToday.Text = "0";
            for (int i = 0; i < extendDates.Length; i++)
            {
                DateTime extDate = Functions.TransferSQLDateToDateTime(extendDates[i]);
                if (extDate.Date.CompareTo(dateToday.Value.Date) == 0)//為追加起始日
                {
                    this.textBoxExtendToday.Text = SQL.Read_SQL_data("extendduration", "extendduration", "project_no ='" + projectNo + "' AND extendstartdate = '" + Functions.TransferDateTimeToSQL(dateToday.Value) + "'");
                }

                if ((extDate.Date.CompareTo(dateToday.Value.Date) == 0 || extDate.Date.CompareTo(dateToday.Value.Date) == -1) && extDate.Date.CompareTo(Functions.TransferSQLDateToDateTime(g_StartDate)) != -1)//0為追加起始日   -1為開始日比今日日期早
                {
                    int extendDuration = Convert.ToInt32(SQL.Read_SQL_data("extendduration", "extendduration", "project_no = '" + projectNo + "' AND extendstartdate = '" + Functions.TransferDateTimeToSQL(extDate) + "'"));
                    accuextendduration += extendDuration;
                }

            }
            //累計追加工期
            this.textBoxAccumulateExtend.Text = accuextendduration.ToString();
            //工期總計
            this.textBoxTotalDuration.Text = Convert.ToString(Convert.ToSingle(SQL.Read_SQL_data("contractduration", "project_info", "project_no ='" + projectNo + "'")) + accuextendduration);
        }

        protected void Compute(string projectNo, string computeType, string countHoliday, string startDate)
        {
            DayCompute dayCompute = new DayCompute();
            comboBoxNoCountByType.Text = "0";

            if (computeType == "1")//限期完工  日曆天
            {
                //追加工期後總計天數
                this.textBoxTotalDays.Text = this.textBoxTotalDuration.Text;
                this.label29.Text = "工期計算方式為限期完工";
            }
            else if (computeType == "2")
            {
                //追加工期後總計天數
                this.textBoxTotalDays.Text = this.textBoxTotalDuration.Text;
                this.label29.Text = "工期計算方式為日曆天";
            }
            else
            {
                if (computeType == "3")//工作天 無休
                {
                    dayCompute.restOnSaturday = false;
                    dayCompute.restOnSunday = false;
                    this.label29.Text = "工期計算方式為工作工，無週休二日";
                }
                else if (computeType == "4")//工作天 周休一日
                {
                    dayCompute.restOnSaturday = false;
                    dayCompute.restOnSunday = true;
                    this.label29.Text = "工期計算方式為工作工，週休一日";
                    if (dateToday.Value.DayOfWeek == DayOfWeek.Sunday)
                    {
                        comboBoxNoCountByType.Text = "1";
                        
                    }   
                }
                else if (computeType == "5")//工作天 周休二日
                {
                    dayCompute.restOnSaturday = true;
                    dayCompute.restOnSunday = true;
                    this.label29.Text = "工期計算方式為工作工，週休二日";
                    if (dateToday.Value.DayOfWeek == DayOfWeek.Sunday)
                        comboBoxNoCountByType.Text = "1";
                    else if (dateToday.Value.DayOfWeek == DayOfWeek.Saturday)
                    {
                        string extraDay = SQL.Read_SQL_data("working", "holiday", "date = '" + Functions.TransferDateTimeToSQL(dateToday.Value) + "'");
                        if(extraDay == string.Empty || extraDay == "1")
                            comboBoxNoCountByType.Text = "1";
                    }

                }

                if (countHoliday == "1")
                {
                    dayCompute.restOnHoliday = true;
                    this.label29.Text += "，國定假日不施工";
                    string holiday = SQL.Read_SQL_data("working", "holiday", "date = '" + Functions.TransferDateTimeToSQL(dateToday.Value) + "'");
                    if (holiday == "1")
                        comboBoxNoCountByType.Text = "1";
                }
                else if (countHoliday == "0")
                {
                    dayCompute.restOnHoliday = false;
                    this.label29.Text += "，國定假日照常施工";
                }


                DateTime FinishDateAfterExtention = dayCompute.CountByDuration(Functions.TransferSQLDateToDateTime(startDate), Convert.ToSingle(this.textBoxTotalDuration.Text));
                //DateTime FinishDateContract = dayCompute.CountByDuration(Functions.TransferSQLDateToDateTime(startDate), Convert.ToSingle(this.textBoxContractDuration.Text));
                //追加工期後總計天數
                this.textBoxTotalDays.Text = Convert.ToString(FinishDateAfterExtention.Subtract(dateStart.Value).Days + 1);
                ////契約天數
                //this.textBoxContractDays.Text = Convert.ToString(FinishDateContract.Subtract(dateStart.Value).Days + 1);
            
            }


            //開工迄今天數 = 今日日期 - 開工日期 + 1
            if (this.dateToday.Value.Date.Subtract(this.dateStart.Value.Date).Days + 1 < 0)
                this.dateToday.Value = this.dateStart.Value;
            else
                this.textBoxDaysStartToCurrent.Text = Convert.ToString(this.dateToday.Value.Date.Subtract(this.dateStart.Value.Date).Days + 1);

            //不計工期
            string[] reportDates = SQL.Read1DArray_SQL_Data("date", "dailyreport", "project_no = '" + projectNo + "'");
            for (int i = 0; i < reportDates.Length; i++)
            {
                if (Functions.TransferSQLDateToDateTime(reportDates[i]).CompareTo(dateToday.Value) == -1)//發生早於今日日期
                {
                    float nonCountingDays = Convert.ToSingle(SQL.Read_SQL_data("nonecounting", "dailyreport", "project_no = '" + projectNo + "' AND date = '" + Functions.TransferSQLDateToDateOnly(reportDates[i]) + "'"));
                    if (nonCountingDays == 0.5)
                        dayCompute.AddNotWorking(Functions.TransferSQLDateToDateTime(reportDates[i]), 0);
                    else if (nonCountingDays == 1)
                    {
                        dayCompute.AddNotWorking(Functions.TransferSQLDateToDateTime(reportDates[i]), 0);
                        dayCompute.AddNotWorking(Functions.TransferSQLDateToDateTime(reportDates[i]), 1);
                    }
                }
            }
            if (comboBoxNoCount.Text == "0.5")
                dayCompute.AddNotWorking(dateToday.Value, 0);
            else if (comboBoxNoCount.Text == "1")
            {
                dayCompute.AddNotWorking(dateToday.Value, 0);
                dayCompute.AddNotWorking(dateToday.Value, 1);
            }
            this.textBoxDurationNotCount.Text = Convert.ToString(dayCompute.CountTotalNotWorkingDay(dateStart.Value, dateToday.Value));
            //實際工期 = 開工迄今天數 - 不計工期
            this.textBoxRealDuration.Text = Convert.ToString(Convert.ToDecimal(this.textBoxDaysStartToCurrent.Text) - Convert.ToDecimal(this.textBoxDurationNotCount.Text));
            //剩餘工期 = 工期總計 - 實際工期 
            this.textBoxRestDuration.Text = Convert.ToString(Convert.ToDecimal(this.textBoxTotalDuration.Text) - Convert.ToDecimal(this.textBoxRealDuration.Text));
            //契約完工日
            this.dateProjectEnd_Contract.Value = Functions.TransferSQLDateToDateTime(SQL.Read_SQL_data("contract_finishdate", "project_info", "project_no ='" + projectNo + "'"));

            if (Convert.ToSingle(this.textBoxRestDuration.Text) < 0)
            {
                //變動完工日從SQL讀出來
                this.dateProjectEnd_Modify.Value = Functions.TransferSQLDateToDateTime( SQL.Read_SQL_data("modified_finishdate", "project_info", "project_no = '" + g_ProjectNumber + "'"));
                //剩餘天數 = 變動完工日 - 今日日期
                this.textBoxRestDays.Text = Convert.ToString(this.dateProjectEnd_Modify.Value.Subtract(dateToday.Value).Days);

                //逾期天數
                this.textBoxOverDays.Text = Convert.ToString(dateToday.Value.Subtract(dateProjectEnd_Modify.Value).Days);
            }
            else
            {
                //變動完工日
                this.dateProjectEnd_Modify.Value = dayCompute.CountByDuration(dateToday.Value.AddDays(1), Convert.ToSingle(this.textBoxRestDuration.Text));
                //把變動完工日寫進SQL
                SQL.Set_SQL_data("modified_finishdate", "project_info", "project_no = '" + g_ProjectNumber + "'", Functions.TransferDateTimeToSQL(this.dateProjectEnd_Modify.Value));


                //剩餘天數 = 變動完工日 - 今日日期
                this.textBoxRestDays.Text = Convert.ToString(this.dateProjectEnd_Modify.Value.Subtract(dateToday.Value).Days);
                //逾期天數
                this.textBoxOverDays.Text = "0";
            }
        }

       

        public virtual void SetDateTodayValue(DateTime date)
        {
            dateToday.Value = date;
        }


        
        protected void DisableAll()
        {
            this.dateToday.Enabled = false;
            this.btnSave.Enabled = false;
            this.comboBoxWeatherAfternoon.Enabled = false;
            this.comboBoxWeatherMorning.Enabled = false;
            this.textBoxInterference.Enabled = false;
            this.comboBoxConditionMorning.Enabled = false;
            this.comboBoxNoCount.Enabled = false;
            this.btnAddData.Enabled = false;
            this.btnDeleteData.Enabled = false;
        }

        protected void DisableAllExceptDateToday()
        {
            this.btnSave.Enabled = false;
            this.comboBoxWeatherAfternoon.Enabled = false;
            this.comboBoxWeatherMorning.Enabled = false;
            this.textBoxInterference.Enabled = false;
            this.comboBoxConditionMorning.Enabled = false;
            this.comboBoxNoCount.Enabled = false;
            this.btnAddData.Enabled = false;
            this.btnDeleteData.Enabled = false;
        }

        protected void EnableAll()
        {
            this.dateToday.Enabled = true;
            this.btnSave.Enabled = true;
            this.comboBoxWeatherAfternoon.Enabled = true;
            this.comboBoxWeatherMorning.Enabled = true;
            this.textBoxInterference.Enabled = true;
            this.comboBoxConditionMorning.Enabled = true;
            this.comboBoxConditionAfternoon.Enabled = true;
            this.comboBoxNoCount.Enabled = true;
            this.btnAddData.Enabled = true;
            this.btnDeleteData.Enabled = true;
        }

        ///////////Event Handler////////
        protected virtual void comboBoxNoCount_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (g_ProjectNumber != null && g_ComputeType != null && g_CountHoliday != null && g_StartDate != null)
            //LoadProjectInfo(g_ProjectNumber);
            Compute(g_ProjectNumber, g_ComputeType, g_CountHoliday, g_StartDate);
        }

        private void comboBoxWeatherAndCondition_SelectedIndexChanged(object sender, EventArgs e)
        {

            int morningNoCount = 0;
            int afternoonNoCount = 0;


            int totalNoCount = 0;

            if (comboBoxWeatherMorning.Text == "晴")
            {
                morningNoCount = 0;
            }
            else if (comboBoxWeatherMorning.Text == "雨")
            {
                if (g_RainydayCountType == "0")//小雨就不計工期
                    morningNoCount = 1;
                else//大雨才不計工期
                    morningNoCount = 0;
            }
            else if (comboBoxWeatherMorning.Text == "豪雨")
            {
                morningNoCount = 1;
            }
            else if (comboBoxWeatherMorning.Text == "颱風")
            {
                morningNoCount = 1;
            }
            else if (comboBoxWeatherMorning.Text == "酷熱")
            {
                morningNoCount = 1;
            }


            if (comboBoxWeatherAfternoon.Text == "晴")
            {
                afternoonNoCount = 0;
            }
            else if (comboBoxWeatherAfternoon.Text == "雨")
            {
                if (g_RainydayCountType == "0")//小雨就不計工期
                    afternoonNoCount = 1;
                else//大雨才不計工期
                    afternoonNoCount = 0;
            }
            else if (comboBoxWeatherAfternoon.Text == "豪雨")
            {
                afternoonNoCount = 1;
            }
            else if (comboBoxWeatherAfternoon.Text == "颱風")
            {
                afternoonNoCount = 1;
            }
            else if (comboBoxWeatherAfternoon.Text == "酷熱")
            {
                afternoonNoCount = 1;
            }


            if (comboBoxConditionMorning.Text == "無")
            {
                morningNoCount |= 0;
            }
            else if (comboBoxConditionMorning.Text == "停電")
            {
                morningNoCount |= 1;
                comboBoxConditionAfternoon.Text = "停電";
            }
            else if (comboBoxConditionMorning.Text == "停工")
            {
                morningNoCount |= 1;
                comboBoxConditionAfternoon.Text = "停工";
            }
            else if (comboBoxConditionMorning.Text == "選舉")
            {
                morningNoCount |= 1;
                comboBoxConditionAfternoon.Text = "選舉";
            }
            else if (comboBoxConditionMorning.Text == "雨後泥濘")
            {
                morningNoCount |= 1;
            }
            else if (comboBoxConditionMorning.Text == "補假")
            {
                morningNoCount |= 1;
                comboBoxConditionAfternoon.Text = "補假";
            }

            if (comboBoxConditionAfternoon.Text == "無")
            {
                afternoonNoCount |= 0;
            }
            else if (comboBoxConditionAfternoon.Text == "停電")
            {
                afternoonNoCount |= 1;
            }
            else if (comboBoxConditionAfternoon.Text == "停工")
            {
                afternoonNoCount |= 1;
            }
            else if (comboBoxConditionAfternoon.Text == "選舉")
            {
                afternoonNoCount |= 1;
            }
            else if (comboBoxConditionAfternoon.Text == "雨後泥濘")
            {
                afternoonNoCount |= 1;
            }
            else if (comboBoxConditionAfternoon.Text == "補假")
            {
                afternoonNoCount |= 1;
            }

            if (morningNoCount + afternoonNoCount == 0)
                comboBoxNoCount.Text = "0";
            else if (morningNoCount + afternoonNoCount == 1)
                comboBoxNoCount.Text = "0.5";
            else if (morningNoCount + afternoonNoCount == 2)
                comboBoxNoCount.Text = "1";
        }

        protected virtual void dateToday_ValueChanged(object sender, EventArgs e)
        {
            ComputeDayOfWeek();

            string[] report = SQL.Read1DArray_SQL_Data("nonecounting", "dailyreport", "project_no = '" + g_ProjectNumber + "' AND date = '" + Functions.TransferDateTimeToSQL(dateToday.Value) + "'");
            if (report.Length != 0)
            {
                label25.Visible = true;
                DisableAllExceptDateToday();
            }
            else
            {
                label25.Visible = false;
                //LoadProjectInfo(projectNumber);
                LoadInfoByDate(g_ProjectNumber);
                //追加工期後總計天數

                if (g_ProjectNumber != null && g_ComputeType != null && g_CountHoliday != null && g_StartDate != null)
                Compute(g_ProjectNumber, g_ComputeType, g_CountHoliday, g_StartDate);

                EnableAll();
            }
        }

        protected virtual void btnSave_Click(object sender, EventArgs e)
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

            string[] sameDate = SQL.Read1DArray_SQL_Data("morning_weather", "dailyreport", "project_no = '" + textBoxProjectNo.Text + "' AND date = '" + Functions.TransferDateTimeToSQL(dateToday.Value) + "'");
            if (sameDate.Length != 0)
            {
                label25.Text = "已存在相同日期的日報表";
                label25.Visible = true;
                return;
            }
            if (dateToday.Value.CompareTo(dateStart.Value) < 0)
            {
                label25.Text = "日期早於開工日期";
                label25.Visible = true;
                return;
            }

            string commandStrDailyReport = "Insert into dailyreport values('";
            commandStrDailyReport = commandStrDailyReport + textBoxProjectNo.Text + "','";
            commandStrDailyReport = commandStrDailyReport + Functions.TransferDateTimeToSQL(dateToday.Value) + "','";
            commandStrDailyReport = commandStrDailyReport + comboBoxWeatherMorning.Text + "','";
            commandStrDailyReport = commandStrDailyReport + comboBoxWeatherAfternoon.Text + "','";
            commandStrDailyReport = commandStrDailyReport + textBoxInterference.Text + "','";
            commandStrDailyReport = commandStrDailyReport + comboBoxConditionMorning.Text + "','";
            commandStrDailyReport = commandStrDailyReport + comboBoxConditionAfternoon.Text + "','";
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
                commandStr = commandStr + Functions.TransferDateTimeToSQL(dateToday.Value) + "','";
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
                commandStr = commandStr + Functions.TransferDateTimeToSQL(dateToday.Value) + "','";
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
                commandStr = commandStr + Functions.TransferDateTimeToSQL(dateToday.Value) + "','";
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
                commandStr = commandStr + Functions.TransferDateTimeToSQL(dateToday.Value) + "','";
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

            //儲存休假資料進SQL
            #region
            for (int i = 0; i < dataGridViewVacation.RowCount; i++)
            {
                string commandStr = "Insert into dailyreport_vacation values('";
                commandStr = commandStr + textBoxProjectNo.Text + "','";
                commandStr = commandStr + Functions.TransferDateTimeToSQL(dateToday.Value) + "','";
                commandStr = commandStr + i + "','";
                commandStr = commandStr + dataGridViewOutsourcing.Rows[i].Cells[0].Value + "','";//員工編號
                commandStr = commandStr + dataGridViewOutsourcing.Rows[i].Cells[1].Value + "','";//員工姓名
                commandStr = commandStr + dataGridViewOutsourcing.Rows[i].Cells[2].Value + "','";//休假天數             
                commandStr = commandStr + dataGridViewOutsourcing.Rows[i].Cells[3].Value + "')";//備註
            }

            #endregion

            conn.Close();

            //this.Close();
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        

        #region dataGridView related
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
            switch (dataGridViewIndex)
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
                    dataGridViewMaterial.Columns[6].ReadOnly = true;//已進數量為唯讀
                    dataGridViewMaterial.Columns[8].ReadOnly = true;//累計進數為唯讀
                    dataGridViewMaterial.Columns[9].ReadOnly = true;//已使用為唯讀
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
                    dataTableOutsourcing.Columns[5].ReadOnly = true;//已出工為唯讀
                    dataTableOutsourcing.Columns[7].ReadOnly = true;//累計出工為唯讀
                    dataTableOutsourcing.Columns[8].ReadOnly = true;//已施作為唯讀
                    dataTableOutsourcing.Columns[10].ReadOnly = true;//累計施作為唯讀
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
        #endregion










    }
}
