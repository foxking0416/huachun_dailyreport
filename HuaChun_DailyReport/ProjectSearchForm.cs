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
    public partial class ProjectSearchForm : SearchFormBase
    {
        private int inputFormType;
        private ProjectEditForm editForm;
        private DailyReportIncreaseForm reportForm;
        private QueryFormBase queryForm;

        public ProjectSearchForm(ProjectEditForm form)
        {
            inputFormType = 0;
            InitializeComponent();
            editForm = form;
            InitializeProjectSearchForm();
            Initialize();
        }

        public ProjectSearchForm(DailyReportIncreaseForm form)
        {
            inputFormType = 1;
            InitializeComponent();
            reportForm = form;
            InitializeProjectSearchForm();
            Initialize();
        }

        public ProjectSearchForm(QueryFormBase form)
        {
            inputFormType = 2;
            InitializeComponent();
            queryForm = form;
            InitializeProjectSearchForm();
            Initialize();
        }

        private void InitializeProjectSearchForm()
        {
            this.Text = "搜尋工程";
            this.DB_TableName = "project_info";
            this.DB_No = "project_no";
            this.DB_Name = "project_name";

            this.rowNo = "工程編號";
            this.rowName = "工程名稱";

            this.radioBtnNo.Text = "搜尋工程編號";
            this.radioBtnName.Text = "搜尋工程名稱";
        }

        protected override void btnCheck_Click(object sender, EventArgs e)
        {

            string number = dataGridView1[0, dataGridView1.CurrentRow.Index].Value.ToString();

            if (inputFormType == 0)
                editForm.LoadInformation(number);
            else if (inputFormType == 1)
                reportForm.LoadProjectInfo(number);
            else if (inputFormType == 2)
                queryForm.LoadProjectInfo(number);
            this.Close();
        }
    }
}
