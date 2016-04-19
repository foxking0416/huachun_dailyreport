using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
using System.Collections;

namespace HuaChun_DailyReport
{
    public partial class IncreaseEditFormBase : Form
    {
        protected string dbHost;//資料庫位址
        protected string dbUser;//資料庫使用者帳號
        protected string dbPass;//資料庫使用者密碼
        protected string dbName;//資料庫名稱
        protected MySQL SQL;
        protected DataTable dataTable;
        protected string functionName;
        protected string functionNameEng;

        public IncreaseEditFormBase()
        {
            InitializeComponent();
        }

        protected void Initialize()
        {
            InitializeSQL();
            InitializeDataTable();
        }

        private void InitializeSQL()
        {
            dbHost = AppSetting.LoadInitialSetting("DB_IP", "127.0.0.1");
            dbUser = AppSetting.LoadInitialSetting("DB_USER", "root");
            dbPass = AppSetting.LoadInitialSetting("DB_PASSWORD", "123");
            dbName = AppSetting.LoadInitialSetting("DB_NAME", "huachun");
            SQL = new MySQL(dbHost, dbUser, dbPass, dbName);
        }

        protected virtual void InitializeDataTable()
        { }

        private void dataGridView1_SelectionChanged(object sender, EventArgs e)
        {
            dataGridView1.ClearSelection();
        }

        protected virtual void btnAddEdit_Click(object sender, EventArgs e)
        {

        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
