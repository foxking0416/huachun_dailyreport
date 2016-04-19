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
    public partial class ProcessCodeSearchForm : SearchFormBase
    {
        private ProcessCodeEditForm editForm;

        public ProcessCodeSearchForm(ProcessCodeEditForm form)
        {
            InitializeComponent();
            editForm = form;
            InitializeMaterialSearchForm();
            Initialize();   
        }

        private void InitializeMaterialSearchForm()
        {
            this.Text = "搜尋施工項目";
            this.DB_TableName = "processcode";
            this.DB_No = "number";
            this.DB_Name = "name";

            this.rowNo = "施工編號";
            this.rowName = "施工名稱";

            this.radioBtnNo.Text = "搜尋施工編號";
            this.radioBtnName.Text = "搜尋施工名稱";
        }

        protected override void btnCheck_Click(object sender, EventArgs e)
        {
            string number = dataGridView1[0, dataGridView1.CurrentRow.Index].Value.ToString();
            editForm.LoadInformation(number);

            this.Close();
        }
    }
}
