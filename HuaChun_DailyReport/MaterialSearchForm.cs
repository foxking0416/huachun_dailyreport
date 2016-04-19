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
    public partial class MaterialSearchForm : SearchFormBase
    {
        private MaterialEditForm editForm;

        public MaterialSearchForm(MaterialEditForm form)
        {
            InitializeComponent();
            editForm = form;
            InitializeMaterialSearchForm();
            Initialize();
        }

        private void InitializeMaterialSearchForm()
        {
            this.Text = "搜尋材料";
            this.DB_TableName = "material";
            this.DB_No = "number";
            this.DB_Name = "name";

            this.rowNo = "材料編號";
            this.rowName = "材料名稱";

            this.radioBtnNo.Text = "搜尋材料編號";
            this.radioBtnName.Text = "搜尋材料名稱";
        }

        protected override void btnCheck_Click(object sender, EventArgs e)
        {
            string number = dataGridView1[0, dataGridView1.CurrentRow.Index].Value.ToString();
            editForm.LoadInformation(number);

            this.Close();
        }
    }
}
