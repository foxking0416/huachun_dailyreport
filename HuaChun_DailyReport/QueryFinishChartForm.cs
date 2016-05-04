using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace HuaChun_DailyReport
{
    public partial class QueryFinishChartForm : QueryFormBase
    {
        string dbHost;
        string dbUser;
        string dbPass;
        string dbName;
        protected MySQL SQL;
        private DataTable dataTableStatistic;


        public QueryFinishChartForm()
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

            dataTableStatistic = new DataTable("FinishStatisticTable");
            dataTableStatistic.Columns.Add("日期", typeof(String));
            dataTableStatistic.Columns.Add("開工迄今", typeof(String));
            dataTableStatistic.Columns.Add("星期", typeof(String));
            dataTableStatistic.Columns.Add("農曆", typeof(String));
            dataTableStatistic.Columns.Add("上午", typeof(String));
            dataTableStatistic.Columns.Add("下午", typeof(String));
            dataTableStatistic.Columns.Add("條件", typeof(String));
            dataTableStatistic.Columns.Add("不計工期", typeof(String));
            dataTableStatistic.Columns.Add("累計不計工期", typeof(String));
            dataTableStatistic.Columns.Add("累計工期", typeof(String));
            dataTableStatistic.Columns.Add("原剩餘工期", typeof(String));
            dataTableStatistic.Columns.Add("原百分比", typeof(String));
            dataTableStatistic.Columns.Add("變動剩餘工期", typeof(String));
            dataTableStatistic.Columns.Add("完工日", typeof(String));
            dataTableStatistic.Columns.Add("剩餘天數", typeof(String));
            dataTableStatistic.Rows.Add(dataTableStatistic.NewRow());
            dataGridView1.DataSource = dataTableStatistic;
            dataGridView1.ReadOnly = false;
            dataGridView1.AllowUserToAddRows = false;
            dataGridView1.MultiSelect = false;
            dataGridView1.EditMode = DataGridViewEditMode.EditOnKeystroke;
        }

        public override void LoadProjectInfo(string projectNo)
        { }
    }
}
