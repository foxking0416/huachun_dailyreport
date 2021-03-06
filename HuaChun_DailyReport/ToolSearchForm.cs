﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace HuaChun_DailyReport
{
    public partial class ToolSearchForm : SearchFormBase
    {
        private ToolEditForm editForm;

        public ToolSearchForm(ToolEditForm form)
        {
            InitializeComponent();
            editForm = form;
            InitializeToolSearchForm();
            Initialize();
        }

        public ToolSearchForm(DailyReportIncreaseForm form, int index, int row, int column)
        {
            formType = 1;
            tabIndex = index;
            rowIndex = row;
            columnIndex = column;
            InitializeComponent();
            reportForm = form;
            InitializeToolSearchForm();
            Initialize();
        }

        private void InitializeToolSearchForm()
        {
            this.Text = "搜尋機具";
            this.DB_TableName = "tool";
            this.DB_No = "number";
            this.DB_Name = "name";

            this.rowNo = "機具編號";
            this.rowName = "機具名稱";

            this.radioBtnNo.Text = "搜尋機具編號";
            this.radioBtnName.Text = "搜尋機具名稱";
        }

        protected override void btnCheck_Click(object sender, EventArgs e)
        {
            string number = dataGridView1[0, dataGridView1.CurrentRow.Index].Value.ToString();
            string name = dataGridView1[1, dataGridView1.CurrentRow.Index].Value.ToString();
            if (formType == 0)
                editForm.LoadInformation(number);
            else if (formType == 1)
            {
                reportForm.SetDataGridViewValue(2, number, columnIndex, rowIndex);
                reportForm.SetDataGridViewValue(2, name, columnIndex + 1, rowIndex);
            }
            this.Close();
        }
    }
}
