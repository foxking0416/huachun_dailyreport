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
    public partial class ToolIncreaseForm : LaborIncreaseForm
    {
        public ToolIncreaseForm()
        {
            InitializeComponent();
            functionName = "機具";
            functionNameEng = "tool";

            this.Text = "機具新增作業";
            this.label1.Text = functionName + "編號";
            this.label2.Text = functionName + "名稱";
            this.label3.Visible = false;
            this.textBox_Unit.Visible = false;
            this.btnAddEdit.Text = "新增";
            this.dataGridView1.Size = new System.Drawing.Size(346, 232);
            this.dataGridView1.Location = new System.Drawing.Point(12, 95);
            Initialize();
        }
    }
}
