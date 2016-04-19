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
    public partial class LaborSearchForm : SearchFormBase
    {
        private LaborEditForm editForm;

        public LaborSearchForm(LaborEditForm form)
        {
            InitializeComponent();
            editForm = form;
            InitializeMaterialSearchForm();
            Initialize();
        }

        private void InitializeMaterialSearchForm()
        {
            this.Text = "搜尋工別";
            this.DB_TableName = "labor";
            this.DB_No = "number";
            this.DB_Name = "name";

            this.rowNo = "工別編號";
            this.rowName = "工別名稱";

            this.radioBtnNo.Text = "搜尋工別編號";
            this.radioBtnName.Text = "搜尋工別名稱";
        }

        protected override void btnCheck_Click(object sender, EventArgs e)
        {
            string number = dataGridView1[0, dataGridView1.CurrentRow.Index].Value.ToString();
            editForm.LoadInformation(number);

            this.Close();
        }
    }
}
