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
    public partial class MemberSearchForm : SearchFormBase
    {
        private MemberEditForm editForm;
        private TextBox textBoxMember;

        public MemberSearchForm(MemberEditForm form)
        {
            InitializeComponent();
            editForm = form;
            InitializeMaterialSearchForm();
            Initialize();
            formType = 0;
        }

        public MemberSearchForm(DailyReportIncreaseForm form, int index, int row, int column)
        {
            formType = 1;
            tabIndex = index;
            rowIndex = row;
            columnIndex = column;
            InitializeComponent();
            reportForm = form;
            InitializeMaterialSearchForm();
            Initialize();
        }

        public MemberSearchForm(TextBox textbox)
        {
            InitializeComponent();
            textBoxMember = textbox;
            InitializeMaterialSearchForm();
            Initialize();
            formType = 2;
        }

        private void InitializeMaterialSearchForm()
        {
            this.Text = "搜尋員工";
            this.DB_TableName = "member";
            this.DB_No = "account";
            this.DB_Name = "name";

            this.rowNo = "員工帳號";
            this.rowName = "員工姓名";

            this.radioBtnNo.Text = "搜尋員工帳號";
            this.radioBtnName.Text = "搜尋員工姓名";
        }

        protected override void btnCheck_Click(object sender, EventArgs e)
        {
            string number = dataGridView1[0, dataGridView1.CurrentRow.Index].Value.ToString();
            string name = dataGridView1[1, dataGridView1.CurrentRow.Index].Value.ToString();
            if (formType == 0)
                editForm.LoadInformation(number);
            if (formType == 1)
            {
                reportForm.SetDataGridViewValue(4, number, columnIndex, rowIndex);
                reportForm.SetDataGridViewValue(4, name, columnIndex + 1, rowIndex);
            }
            else if (formType == 2)
                textBoxMember.Text = name;
            this.Close();
        }
    }
}
