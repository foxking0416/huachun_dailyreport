using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Collections;

namespace HuaChun_DailyReport
{
    public partial class SearchFormBase : Form
    {
        protected string dbHost;//資料庫位址
        protected string dbUser;//資料庫使用者帳號
        protected string dbPass;//資料庫使用者密碼
        protected string dbName;//資料庫名稱
        protected MySQL SQL;
        protected string[] numbers;
        protected string[] names;
        protected DataTable dataTable;
        protected string DB_TableName;
        protected string DB_No;
        protected string DB_Name;
        protected string rowNo;
        protected string rowName;
        protected int formType;
        protected int rowIndex;
        protected int columnIndex;
        protected int tabIndex;
        protected DailyReportIncreaseForm reportForm;

        public SearchFormBase()
        {
            InitializeComponent();
            
        }

        protected virtual void Initialize()
        {

            textBox2.Enabled = false;
            dbHost = AppSetting.LoadInitialSetting("DB_IP", "127.0.0.1");
            dbUser = AppSetting.LoadInitialSetting("DB_USER", "root");
            dbPass = AppSetting.LoadInitialSetting("DB_PASSWORD", "123");
            dbName = AppSetting.LoadInitialSetting("DB_NAME", "huachun");
            SQL = new MySQL(dbHost, dbUser, dbPass, dbName);

            numbers = SQL.Read1DArrayNoCondition_SQL_Data(DB_No, DB_TableName);
            names = SQL.Read1DArrayNoCondition_SQL_Data(DB_Name, DB_TableName);

            dataTable = new DataTable("MyNewTable");
            dataTable.Columns.Add(rowNo, typeof(String));
            dataTable.Columns.Add(rowName, typeof(String));
            dataGridView1.DataSource = dataTable;
            dataGridView1.ReadOnly = true;
            dataGridView1.AllowUserToAddRows = false;
            dataGridView1.MultiSelect = false;

            DataRow dataRow;
            for (int i = 0; i < numbers.Length; i++)
            {
                dataRow = dataTable.NewRow();
                dataRow[rowNo] = numbers[i];
                dataRow[rowName] = SQL.Read_SQL_data(DB_Name, DB_TableName, DB_No + " = '" + numbers[i] + "'");
                dataTable.Rows.Add(dataRow);
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            if (radioBtnNo.Checked)
            {
                textBox1.Enabled = true;
                textBox2.Enabled = false;
            }
            else
            {
                textBox1.Enabled = false;
                textBox2.Enabled = true;
            }
        }

        protected virtual void btnCheck_Click(object sender, EventArgs e)
        {
        }

        protected virtual void textBox1_TextChanged(object sender, EventArgs e)
        {
            dataTable.Clear();

            ArrayList array = new ArrayList();
            for (int i = 0; i < numbers.Length; i++)
            {
                if (numbers[i].Contains(textBox1.Text))
                    array.Add(numbers[i]);
            }


            DataRow dataRow;
            for (int i = 0; i < array.Count; i++)
            {
                dataRow = dataTable.NewRow();
                dataRow[rowNo] = array[i];
                dataRow[rowName] = SQL.Read_SQL_data(DB_Name, DB_TableName, DB_No + " = '" + array[i] + "'");
                dataTable.Rows.Add(dataRow);
            }
        }

        protected virtual void textBox2_TextChanged(object sender, EventArgs e)
        {
            dataTable.Clear();

            ArrayList array = new ArrayList();
            for (int i = 0; i < names.Length; i++)
            {
                if (names[i].Contains(textBox2.Text))
                    array.Add(names[i]);
            }


            DataRow dataRow;
            for (int i = 0; i < array.Count; i++)
            {
                dataRow = dataTable.NewRow();
                dataRow[rowNo] = SQL.Read_SQL_data(DB_No, DB_TableName, DB_Name + " = '" + array[i] + "'");
                dataRow[rowName] = array[i];
                dataTable.Rows.Add(dataRow);
            }
        }


    }
}
