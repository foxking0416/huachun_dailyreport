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
            dbPass = AppSetting.LoadInitialSetting("DB_PASSWORD", "chichi1219");
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
            commandStr = commandStr + "project,";//
            commandStr = commandStr + "grantdate,";//核准日期
            commandStr = commandStr + "grantnumber,";//核准文號
            commandStr = commandStr + "extendvalue,";//追加金額
            commandStr = commandStr + "totalvalue,";//總金額
            commandStr = commandStr + "extendstartdate,";//追加起算日
            commandStr = commandStr + "extendduration,";//追加工期
            commandStr = commandStr + "accuextendduration,";//累計追加工期
            commandStr = commandStr + "totalduration,";//總工期
            commandStr = commandStr + "contract_finishdate,";//契約完工日
            commandStr = commandStr + "modified_finishdate,";//變動完工日
            commandStr = commandStr + "writedate";//填寫日期
            commandStr = commandStr + ") values('";
            commandStr = commandStr + ProjectNumber + "','";
            commandStr = commandStr + Functions.GetDateTimeValue(dateTimeGrantDate.Value) + "','";//核准日期
            commandStr = commandStr + textBoxGrantNumber.Text + "','";//核准文號
            commandStr = commandStr + textBoxExtendValue.Text + "','";//追加金額
            commandStr = commandStr + textBoxTotalValue.Text + "','";//總金額
            commandStr = commandStr + Functions.GetDateTimeValue(dateTimeExtendStartDate.Value) + "','";//追加起算日
            commandStr = commandStr + numericExtendDuration.Value + "','";//追加工期
            commandStr = commandStr + numericAccuExtentionDuration.Value + "','";//累計追加工期
            commandStr = commandStr + numericTotalDuration.Value + "','";
            commandStr = commandStr + Functions.GetDateTimeValue(dateTimeContractEndDate.Value) + "','";
            commandStr = commandStr + Functions.GetDateTimeValue(dateTimeModifiedEndDate.Value) + "','";
            commandStr = commandStr + Functions.GetDateTimeValue(dateTimeFilledDate.Value);
            commandStr = commandStr + "')";


            command.CommandText = commandStr;// "Insert into vendor(vendor_no,vendor_name,vendor_abbre) values('" + textBoxVendor_No.Text + "','" + textBoxVendor_Name.Text + "','" + textBoxVendor_Abbre.Text + "')";
            command.ExecuteNonQuery();
            conn.Close();
        }

        protected void btnOK_Click(object sender, EventArgs e)
        {
            label12.Visible = false;
            label13.Visible = false;
            label14.Visible = false;

            if (textBoxGrantNumber.Text == string.Empty)
                label12.Visible = true;

            if (textBoxExtendValue.Text == string.Empty)
                label13.Visible = true;

            if (textBoxTotalValue.Text == string.Empty)
                label14.Visible = true;

            if (textBoxGrantNumber.Text == string.Empty)
                return;
            if (textBoxExtendValue.Text == string.Empty)
                return;
            if (textBoxTotalValue.Text == string.Empty)
                return;

            string[] sameNo = SQL.Read1DArray_SQL_Data("grantnumber", "extendduration", "project = '" + ProjectNumber + "' AND grantnumber = '" + textBoxGrantNumber.Text + "'");
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
