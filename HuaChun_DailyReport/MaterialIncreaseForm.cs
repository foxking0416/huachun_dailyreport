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
    public partial class MaterialIncreaseForm : IncreaseEditFormBase
    {
        public MaterialIncreaseForm()
        {
            InitializeComponent();
            functionName = "材料";
            functionNameEng = "material";

            this.Text = "材料新增作業";
            this.label1.Text = functionName + "編號";
            this.label2.Text = functionName + "名稱";
            this.btnAddEdit.Text = "新增";
            Initialize();
        }


        protected override void InitializeDataTable()
        {
            dataTable = new DataTable("MyNewTable");
            dataTable.Columns.Add(functionName + "編號", typeof(String));
            dataTable.Columns.Add(functionName + "名稱", typeof(String));
            dataTable.Columns.Add("單位", typeof(String));
            dataGridView1.DataSource = dataTable;
            dataGridView1.ReadOnly = true;
            dataGridView1.AllowUserToAddRows = false;
            dataGridView1.MultiSelect = false;

            RefreshDatagridview();
        }

        private void RefreshDatagridview()
        {
            dataTable.Clear();

            string[] numberArr = SQL.Read1DArrayNoCondition_SQL_Data("number", functionNameEng);
            Array.Sort(numberArr);

            DataRow dataRow;
            for (int i = 0; i < numberArr.Length; i++)
            {
                dataRow = dataTable.NewRow();
                dataRow[functionName + "編號"] = numberArr[i];
                dataRow[functionName + "名稱"] = SQL.Read_SQL_data("name", functionNameEng, "number = '" + numberArr[i] + "'");
                dataRow["單位"] = SQL.Read_SQL_data("unit", functionNameEng, "number = '" + numberArr[i] + "'");
                dataTable.Rows.Add(dataRow);
            }
        }

        private void InsertIntoDB()
        {
            string connStr = "server=" + dbHost + ";uid=" + dbUser + ";pwd=" + dbPass + ";database=" + dbName;
            MySqlConnection conn = new MySqlConnection(connStr);
            MySqlCommand command = conn.CreateCommand();
            conn.Open();

            string commandStr = "Insert into " + functionNameEng + "(";
            commandStr = commandStr + "number,";
            commandStr = commandStr + "name,";
            commandStr = commandStr + "unit";
            commandStr = commandStr + ") values('";
            commandStr = commandStr + textBox_No.Text + "','";
            commandStr = commandStr + textBox_Name.Text + "','";
            commandStr = commandStr + textBox_Unit.Text;
            commandStr = commandStr + "')";

            command.CommandText = commandStr;
            command.ExecuteNonQuery();
            conn.Close();
        }

        protected override void btnAddEdit_Click(object sender, EventArgs e)
        {
            labelWarning1.Visible = false;
            labelWarning2.Visible = false;
            labelWarning3.Visible = false;

            if (textBox_No.Text == string.Empty)
                labelWarning1.Visible = true;
            if (textBox_Name.Text == string.Empty)
                labelWarning2.Visible = true;
            if (textBox_Unit.Text == string.Empty)
                labelWarning3.Visible = true;

            if (textBox_No.Text == string.Empty)
                return;
            if (textBox_Name.Text == string.Empty)
                return;
            if (textBox_Unit.Text == string.Empty)
                return;

            int i = 0;
            bool result = int.TryParse(textBox_No.Text, out i);
            if (!result)
            {
                labelWarning1.Text = "編號只能為數字";
                labelWarning1.Visible = true;
                return;
            }
            else
                labelWarning1.Text = "編號不可為空白";


            string[] sameNo = SQL.Read1DArray_SQL_Data("number", functionNameEng, "number = '" + textBox_No.Text + "'");
            if (sameNo.Length != 0)
            {
                labelWarning1.Text = "已存在相同" + functionName + "編號";
                labelWarning1.Visible = true;
                return;
            }


            InsertIntoDB();
            RefreshDatagridview();
            textBox_No.Clear();
            textBox_Name.Clear();
            textBox_Unit.Clear();
        }


    }
}
