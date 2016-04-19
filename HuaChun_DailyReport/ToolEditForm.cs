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
    public partial class ToolEditForm : LaborEditForm
    {
        public ToolEditForm()
        {
            InitializeComponent();

            functionName = "機具";
            functionNameEng = "tool";

            this.Text = "機具編輯作業";
            this.label1.Text = functionName + "編號";
            this.label2.Text = functionName + "名稱";
            this.label3.Visible = false;
            this.textBox_Unit.Visible = false;
            this.btnAddEdit.Text = "修改";
            this.dataGridView1.Size = new System.Drawing.Size(346, 232);
            this.dataGridView1.Location = new System.Drawing.Point(12, 95);
            this.dataGridView1.CurrentCellChanged += new EventHandler(dataGridView1_CurrentCellChanged);
            this.textBox_No.ReadOnly = true;
            Initialize();
        }

        protected override void btnSearch_Click(object sender, EventArgs e)
        {
            ToolSearchForm searchform = new ToolSearchForm(this);
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
            }
        }

    }
}
