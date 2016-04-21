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

        public LaborSearchForm(DailyReportIncreaseForm form, int index, int row, int column)
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
            string name = dataGridView1[1, dataGridView1.CurrentRow.Index].Value.ToString();
            if (formType == 0)
                editForm.LoadInformation(number);
            else if (formType == 1)
            {
                //switch (tabIndex)
                //{
                //    case 0:
                //        reportForm.SetDataGridViewValue(0, number, columnIndex, rowIndex);
                //        reportForm.SetDataGridViewValue(0, name, columnIndex + 1, rowIndex);
                //        break;
                //    case 1:
                        reportForm.SetDataGridViewValue(1, number, columnIndex, rowIndex);
                        reportForm.SetDataGridViewValue(1, name, columnIndex + 1, rowIndex);
                //        break;
                //    case 2:
                //        reportForm.SetDataGridViewValue(2, number, columnIndex, rowIndex);
                //        reportForm.SetDataGridViewValue(2, name, columnIndex + 1, rowIndex);
                //        break;
                //    case 3:
                //        reportForm.SetDataGridViewValue(3, number, columnIndex, rowIndex);
                //        reportForm.SetDataGridViewValue(3, name, columnIndex + 1, rowIndex);
                //        break;
                //    case 4:
                //        reportForm.SetDataGridViewValue(4, number, columnIndex, rowIndex);
                //        reportForm.SetDataGridViewValue(4, name, columnIndex + 1, rowIndex);
                //        break;
                //    default:
                //        break;
                //}
            }
            this.Close();
        }
    }
}
