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
        private int formOrText;

        public MemberSearchForm(MemberEditForm form)
        {
            InitializeComponent();
            editForm = form;
            InitializeMaterialSearchForm();
            Initialize();
            formOrText = 0;
        }

        public MemberSearchForm(TextBox textbox)
        {
            InitializeComponent();
            textBoxMember = textbox;
            InitializeMaterialSearchForm();
            Initialize();
            formOrText = 1;
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
            if (formOrText == 0)
            {
                string number = dataGridView1[0, dataGridView1.CurrentRow.Index].Value.ToString();
                editForm.LoadInformation(number);
            }
            else if (formOrText == 1)
            {
                string name = dataGridView1[1, dataGridView1.CurrentRow.Index].Value.ToString();
                textBoxMember.Text = name;
            }
            this.Close();
        }
    }
}
