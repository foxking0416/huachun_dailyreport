using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace HuaChun_DailyReport
{
    public partial class ExtentionIncreaseForm : Form
    {
        string dbHost;
        string dbUser;
        string dbPass;
        string dbName;
        protected MySQL SQL;
        protected string ProjectNumber;

        public ExtentionIncreaseForm()
        {
            InitializeComponent();
            Initialize();
        }

        public ExtentionIncreaseForm(string ID)
        {
            ProjectNumber = ID;
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
        }
        
        protected void InsertIntoDB()
        {
            string connStr = "server=" + dbHost + ";uid=" + dbUser + ";pwd=" + dbPass + ";database=" + dbName;
            MySqlConnection conn = new MySqlConnection(connStr);
            MySqlCommand command = conn.CreateCommand();
            conn.Open();

            string commandStr = "Insert into extendduration(";
            commandStr = commandStr + "project_no,";//
            commandStr = commandStr + "grantdate,";//核准日期
            commandStr = commandStr + "grantnumber,";//核准文號
            commandStr = commandStr + "extendvalue,";//追加金額
            commandStr = commandStr + "extendstartdate,";//追加起算日
            commandStr = commandStr + "extendduration,";//追加工期
            commandStr = commandStr + "writedate";//填寫日期
            commandStr = commandStr + ") values('";
            commandStr = commandStr + ProjectNumber + "','";
            commandStr = commandStr + Functions.TransferDateTimeToSQL(dateTimeGrantDate.Value) + "','";//核准日期
            commandStr = commandStr + textBoxGrantNumber.Text + "','";//核准文號
            commandStr = commandStr + numericExtendValue.Value + "','";//追加金額
            commandStr = commandStr + Functions.TransferDateTimeToSQL(dateTimeExtendStartDate.Value) + "','";//追加起算日
            commandStr = commandStr + numericExtendDuration.Value + "','";//追加工期
            commandStr = commandStr + Functions.TransferDateTimeToSQL(dateTimeFilledDate.Value);//填寫日期
            commandStr = commandStr + "')";


            command.CommandText = commandStr;// "Insert into vendor(vendor_no,vendor_name,vendor_abbre) values('" + textBoxVendor_No.Text + "','" + textBoxVendor_Name.Text + "','" + textBoxVendor_Abbre.Text + "')";
            command.ExecuteNonQuery();
            conn.Close();
        }

        protected virtual void btnOK_Click(object sender, EventArgs e)
        {
            label12.Visible = false;

            if (textBoxGrantNumber.Text == string.Empty)
                label12.Visible = true;

            if (textBoxGrantNumber.Text == string.Empty)
                return;

            string[] sameNo = SQL.Read1DArray_SQL_Data("grantnumber", "extendduration", "project_no = '" + ProjectNumber + "' AND grantnumber = '" + textBoxGrantNumber.Text + "'");
            if (sameNo.Length != 0)
            {
                label12.Text = "已存在相同核准文號";
                label12.Visible = true;
                return;
            }


            InsertIntoDB();
            this.Close();
        }

        private void btnCancle_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
