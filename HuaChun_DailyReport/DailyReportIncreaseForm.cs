using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing.Printing;
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
            dbPass = AppSetting.LoadInitialSetting("DB_PASSWORD", "chichi1219");
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
            dataTableMaterial.Rows.Add(dataTableMaterial.NewRow());
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
            dataTableManPower.Rows.Add(dataTableManPower.NewRow());
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
            dataTableOutsourcing.Rows.Add(dataTableOutsourcing.NewRow());
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
            dataTableVacation.Rows.Add(dataTableVacation.NewRow());
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
            string projectName = SQL.Read_SQL_data("project_name", "project_info", "project_no ='" + projectNo + "'");
            this.textBoxProjectName.Text = projectName;
            //開工日期
            string startDate = SQL.Read_SQL_data("startdate", "project_info", "project_no ='" + projectNo + "'");
            this.dateStart.Value = Functions.TransferSQLDateToDateTime(startDate);
            //契約完工日
            string contractFinishDate = SQL.Read_SQL_data("contract_finishdate", "project_info", "project_no ='" + projectNo + "'");
            this.dateProjectEnd_Contract.Value = Functions.TransferSQLDateToDateTime(contractFinishDate);
            //契約工期
            this.textBoxContractDuration.Text = SQL.Read_SQL_data("contractduration", "project_info", "project_no ='" + projectNo + "'");
            //契約天數
            this.textBoxContractDays.Text = SQL.Read_SQL_data("contractdays", "project_info", "project_no ='" + projectNo + "'");
            //開工迄今
            this.textBoxDayStartToCurrent.Text = (this.dateToday.Value.Date.Subtract(this.dateStart.Value.Date).Days + 1).ToString();
            //剩餘天數
            this.textBoxRestDays.Text = (this.dateProjectEnd_Contract.Value.Date.Subtract(this.dateToday.Value.Date).Days).ToString();


        }

        private void btnSave_Click(object sender, EventArgs e)
        {

        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            //Print();
            //ReadDB();
            //AccessDB();

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
