using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Collections;

namespace HuaChun_DailyReport
{
    public partial class VendorSearchForm : SearchFormBase
    {
        private VendorEditForm editForm;

        public VendorSearchForm(VendorEditForm form)
        {
            formType = 0;
            InitializeComponent();
            editForm = form;
            InitializeVendorSearchForm();
            Initialize();
        }

        public VendorSearchForm(DailyReportIncreaseForm form, int index, int row, int column)
        {
            formType = 1;
            tabIndex = index;
            rowIndex = row;
            columnIndex = column;
            InitializeComponent();
            reportForm = form;
            InitializeVendorSearchForm();
            Initialize();
        }

        private void InitializeVendorSearchForm()
        {
            this.Text = "搜尋廠商";
            this.DB_TableName = "vendor";
            this.DB_No = "vendor_no";
            this.DB_Name = "vendor_name";

            this.rowNo = "廠商編號";
            this.rowName = "廠商名稱";

            this.radioBtnNo.Text = "搜尋廠商編號";
            this.radioBtnName.Text = "搜尋廠商名稱";
        }

        protected override void btnCheck_Click(object sender, EventArgs e)
        {
            string number = dataGridView1[0, dataGridView1.CurrentRow.Index].Value.ToString();
            string name = dataGridView1[1, dataGridView1.CurrentRow.Index].Value.ToString();
            if (formType == 0)
                editForm.LoadInformation(number);
            else if (formType == 1)
            {
                switch (tabIndex)
                {
                    case 0:
                        reportForm.SetDataGridViewValue(0, number, columnIndex, rowIndex);
                        reportForm.SetDataGridViewValue(0, name, columnIndex + 1, rowIndex);
                        break;
                    case 1:
                        reportForm.SetDataGridViewValue(1, number, columnIndex, rowIndex);
                        reportForm.SetDataGridViewValue(1, name, columnIndex + 1, rowIndex);
                        break;
                    case 2:
                        reportForm.SetDataGridViewValue(2, number, columnIndex, rowIndex);
                        reportForm.SetDataGridViewValue(2, name, columnIndex + 1, rowIndex);
                        break;
                    case 3:
                        reportForm.SetDataGridViewValue(3, number, columnIndex, rowIndex);
                        reportForm.SetDataGridViewValue(3, name, columnIndex + 1, rowIndex);
                        break;
                    case 4:
                        reportForm.SetDataGridViewValue(4, number, columnIndex, rowIndex);
                        reportForm.SetDataGridViewValue(4, name, columnIndex + 1, rowIndex);
                        break;
                    default:
                        break;
                }
                
            }
            this.Close();
        }
    }
}
