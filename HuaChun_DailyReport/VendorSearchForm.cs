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
    public partial class VendorSearchForm : Form
    {
        string dbHost;//資料庫位址
        string dbUser;//資料庫使用者帳號
        string dbPass;//資料庫使用者密碼
        string dbName;//資料庫名稱
        MySQL SQL;
        string[] vendor_no;
        string[] vendor_name;
        DataTable vendorTable;
        VendorEditForm vendorEditForm;
        private int formType;
        //private string inputString;
        private String inputString;

        public VendorSearchForm(VendorEditForm form)
        {
            formType = 0;
            InitializeComponent();
            vendorEditForm = form;
            Initialize();
        }

        public VendorSearchForm(String str)
        {
            formType = 1;
            InitializeComponent();
            inputString = str;
            Initialize();
        }

        private void Initialize()
        {

            textBoxName.Enabled = false;
            dbHost = AppSetting.LoadInitialSetting("DB_IP", "127.0.0.1");
            dbUser = AppSetting.LoadInitialSetting("DB_USER", "root");
            dbPass = AppSetting.LoadInitialSetting("DB_PASSWORD", "123");
            dbName = AppSetting.LoadInitialSetting("DB_NAME", "huachun");
            SQL = new MySQL(dbHost, dbUser, dbPass, dbName);

            vendor_no = SQL.Read1DArrayNoCondition_SQL_Data("vendor_no", "vendor");
            vendor_name = SQL.Read1DArrayNoCondition_SQL_Data("vendor_name", "vendor");

            vendorTable = new DataTable("MyNewTable");
            vendorTable.Columns.Add("廠商編號", typeof(String));
            vendorTable.Columns.Add("廠商名稱", typeof(String));
            dataGridView1.DataSource = vendorTable;
            dataGridView1.ReadOnly = true;
            dataGridView1.AllowUserToAddRows = false;
            dataGridView1.MultiSelect = false;
        }

        private void textBoxNo_TextChanged(object sender, EventArgs e)
        {
            vendorTable.Clear();

            ArrayList array = new ArrayList();
            for (int i = 0; i < vendor_no.Length; i++)
            {
                if(vendor_no[i].Contains(textBoxNo.Text))
                    array.Add(vendor_no[i]);
            }


            DataRow vendorRow;
            for (int i = 0; i < array.Count; i++)
            {
                vendorRow = vendorTable.NewRow();
                vendorRow["廠商編號"] = array[i];
                vendorRow["廠商名稱"] = SQL.Read_SQL_data("vendor_name", "vendor", "vendor_no = '" + array[i] + "'");
                vendorTable.Rows.Add(vendorRow);
            }

        }

        private void textBoxName_TextChanged(object sender, EventArgs e)
        {
            vendorTable.Clear();

            ArrayList array = new ArrayList();
            for (int i = 0; i < vendor_name.Length; i++)
            {
                if (vendor_name[i].Contains(textBoxName.Text))
                    array.Add(vendor_name[i]);
            }


            DataRow vendorRow;
            for (int i = 0; i < array.Count; i++)
            {
                vendorRow = vendorTable.NewRow();
                vendorRow["廠商編號"] = SQL.Read_SQL_data("vendor_no", "vendor", "vendor_name = '" + array[i] + "'");
                vendorRow["廠商名稱"] = array[i]; 
                vendorTable.Rows.Add(vendorRow);
            }
        }

        private void radioBtn_CheckedChanged(object sender, EventArgs e)
        {
            if (radioBtnNo.Checked)
            {
                textBoxNo.Enabled = true;
                textBoxName.Enabled = false;
            }
            else
            {
                textBoxNo.Enabled = false;
                textBoxName.Enabled = true;
            }
        }

        private void btnCheck_Click(object sender, EventArgs e)
        {
            string number = dataGridView1[0, dataGridView1.CurrentRow.Index].Value.ToString();
            if (formType == 0)
                vendorEditForm.LoadInformation(number);
            else if (formType == 1)
                inputString = number;
            this.Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }





    }
}
