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
    public partial class ExtentionEditForm : ExtentionIncreaseForm
    {
        public ExtentionEditForm()
        {
            InitializeComponent();
        }

        public ExtentionEditForm(string ID, string grantNo):base(ID)
        {
            InitializeComponent();
            this.textBoxGrantNumber.ReadOnly = true;
            LoadInformation(grantNo);

        }

        public void LoadInformation(string grantNumber)
        {
            //核准日期
            string grantdate = SQL.Read_SQL_data("grantdate", "extendduration", "project_no = '" + ProjectNumber + "' AND grantnumber = '" + grantNumber + "'");
            this.dateTimeGrantDate.Value = Functions.TransferSQLDateToDateTime(grantdate);
            //核准文號
            this.textBoxGrantNumber.Text = grantNumber;
            //追加金額
            this.numericExtendValue.Value = Convert.ToDecimal(SQL.Read_SQL_data("extendvalue", "extendduration", "project_no = '" + ProjectNumber + "' AND grantnumber = '" + grantNumber + "'"));
            //追加起算日
            string extendstartdate = SQL.Read_SQL_data("extendstartdate", "extendduration", "project_no = '" + ProjectNumber + "' AND grantnumber = '" + grantNumber + "'");
            this.dateTimeExtendStartDate.Value = Functions.TransferSQLDateToDateTime(extendstartdate);
            //追加工期
            this.numericExtendDuration.Value = Convert.ToDecimal(SQL.Read_SQL_data("extendduration", "extendduration", "project_no = '" + ProjectNumber + "' AND grantnumber = '" + grantNumber + "'"));
            //填寫日期
            string writedate = SQL.Read_SQL_data("writedate", "extendduration", "project_no = '" + ProjectNumber + "' AND grantnumber = '" + grantNumber + "'");
            this.dateTimeFilledDate.Value = Functions.TransferSQLDateToDateTime(writedate);
        }

        protected override void btnOK_Click(object sender, EventArgs e)
        {
            label12.Visible = false;

            if (textBoxGrantNumber.Text == string.Empty)
                label12.Visible = true;

            if (textBoxGrantNumber.Text == string.Empty)
                return;


            //覆寫原有資料
            DialogResult result = MessageBox.Show("確定要修改追加工期資料?", "確定", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation);
            if (result == DialogResult.Yes)
            {
                //核准日期
                SQL.Set_SQL_data("grantdate", "extendduration", "project_no = '" + ProjectNumber + "' AND grantnumber = '" + this.textBoxGrantNumber.Text + "'", Functions.TransferDateTimeToSQL(dateTimeGrantDate.Value));
                //核准文號不變動
                //追加金額
                SQL.Set_SQL_data("extendvalue", "extendduration", "project_no = '" + ProjectNumber + "' AND grantnumber = '" + this.textBoxGrantNumber.Text + "'", numericExtendValue.Value.ToString());
                //追加起算日
                SQL.Set_SQL_data("extendstartdate", "extendduration", "project_no = '" + ProjectNumber + "' AND grantnumber = '" + this.textBoxGrantNumber.Text + "'", Functions.TransferDateTimeToSQL(dateTimeExtendStartDate.Value));
                //追加工期
                SQL.Set_SQL_data("extendduration", "extendduration", "project_no = '" + ProjectNumber + "' AND grantnumber = '" + this.textBoxGrantNumber.Text + "'", numericExtendDuration.Value.ToString());
                //填寫日期
                SQL.Set_SQL_data("writedate", "extendduration", "project_no = '" + ProjectNumber + "' AND grantnumber = '" + this.textBoxGrantNumber.Text + "'", Functions.TransferDateTimeToSQL(dateTimeFilledDate.Value));
            }
            //InsertIntoDB();
            this.Close();
        }
    }
}
