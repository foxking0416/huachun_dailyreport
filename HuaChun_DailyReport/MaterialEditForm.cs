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
    public partial class MaterialEditForm : EditFormBase
    {

        public MaterialEditForm()
        {
            InitializeComponent();
            functionName = "材料";
            functionNameEng = "material";

            this.Text = "材料編輯作業";
            this.label1.Text = functionName + "編號";
            this.label2.Text = functionName + "名稱";
            this.btnAddEdit.Text = "修改";
            this.dataGridView1.CurrentCellChanged += new EventHandler(dataGridView1_CurrentCellChanged);
            this.textBox_No.ReadOnly = true;
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

        public void LoadInformation(string number)
        {
            this.textBox_No.Text = SQL.Read_SQL_data("number", functionNameEng, "number = '" + number + "'");
            this.textBox_Name.Text = SQL.Read_SQL_data("name", functionNameEng, "number = '" + number + "'");
            this.textBox_Unit.Text = SQL.Read_SQL_data("unit", functionNameEng, "number = '" + number + "'");
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

        protected void dataGridView1_CurrentCellChanged(object sender, EventArgs e)
        {
            try
            {
                string number = dataGridView1[0, dataGridView1.CurrentRow.Index].Value.ToString();
                this.textBox_No.Text = number;
                this.textBox_Name.Text = SQL.Read_SQL_data("name", functionNameEng, "number = '" + number + "'");
                this.textBox_Unit.Text = SQL.Read_SQL_data("unit", functionNameEng, "number = '" + number + "'");
            }
            catch
            { }
        }

        protected override void btnAddEdit_Click(object sender, EventArgs e)
        {
            labelWarning1.Visible = false;
            labelWarning2.Visible = false;
            labelWarning3.Visible = false;

            if (textBox_Name.Text == string.Empty)
                labelWarning2.Visible = true;
            if (textBox_Unit.Text == string.Empty)
                labelWarning3.Visible = true;

            if (textBox_Name.Text == string.Empty)
                return;
            if (textBox_Unit.Text == string.Empty)
                return;


            DialogResult result = MessageBox.Show("確定要修改" + functionName + "資料?", "確定", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation);
            if (result == DialogResult.Yes)
            {
                SQL.Set_SQL_data("name", functionNameEng, "number = '" + this.textBox_No.Text + "'", this.textBox_Name.Text);
                SQL.Set_SQL_data("unit", functionNameEng, "number = '" + this.textBox_No.Text + "'", this.textBox_Unit.Text);

                RefreshDatagridview();
                textBox_No.Clear();
                textBox_Name.Clear();
                textBox_Unit.Clear();
            }
        }

        protected override void btnSearch_Click(object sender, EventArgs e)
        {
            MaterialSearchForm searchform = new MaterialSearchForm(this);
            searchform.ShowDialog();
        }

        protected override void btnDelete_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("確定要刪除" + functionName + "資料?", "確定", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation);
            if (result == DialogResult.Yes)
            {
                SQL.NoHistoryDelete_SQL(functionNameEng, "number = '" + this.textBox_No.Text + "'");

                RefreshDatagridview();
                textBox_No.Clear();
                textBox_Name.Clear();
                textBox_Unit.Clear();
            }
        }
    }
}
