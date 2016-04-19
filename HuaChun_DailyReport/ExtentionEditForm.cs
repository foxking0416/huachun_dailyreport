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
            string grantdate = SQL.Read_SQL_data("grantdate", "extendduration", "project = '" + ProjectNumber + "' AND grantnumber = '" + grantNumber + "'");
            this.dateTimeGrantDate.Value = Functions.TransferSQLDateToDateTime(grantdate);
            //核准文號
            this.textBoxGrantNumber.Text = grantNumber;
            //追加金額
            this.textBoxExtendValue.Text = SQL.Read_SQL_data("extendvalue", "extendduration", "project = '" + ProjectNumber + "' AND grantnumber = '" + grantNumber + "'");
            //總金額
            this.textBoxTotalValue.Text = SQL.Read_SQL_data("totalvalue", "extendduration", "project = '" + ProjectNumber + "' AND grantnumber = '" + grantNumber + "'");
            //追加起算日
            string extendstartdate = SQL.Read_SQL_data("extendstartdate", "extendduration", "project = '" + ProjectNumber + "' AND grantnumber = '" + grantNumber + "'");
            this.dateTimeExtendStartDate.Value = Functions.TransferSQLDateToDateTime(extendstartdate);
            //追加工期
            this.numericExtendDuration.Value = Convert.ToDecimal(SQL.Read_SQL_data("extendduration", "extendduration", "project = '" + ProjectNumber + "' AND grantnumber = '" + grantNumber + "'"));
            //累計追加工期
            this.numericAccuExtentionDuration.Value = Convert.ToDecimal(SQL.Read_SQL_data("accuextendduration", "extendduration", "project = '" + ProjectNumber + "' AND grantnumber = '" + grantNumber + "'"));
            //總工期
            this.numericTotalDuration.Value = Convert.ToDecimal(SQL.Read_SQL_data("totalduration", "extendduration", "project = '" + ProjectNumber + "' AND grantnumber = '" + grantNumber + "'"));
            //契約完工日
            string contract_finishdate = SQL.Read_SQL_data("contract_finishdate", "extendduration", "project = '" + ProjectNumber + "' AND grantnumber = '" + grantNumber + "'");
            this.dateTimeContractEndDate.Value = Functions.TransferSQLDateToDateTime(contract_finishdate);
            //變動完工日
            string modified_finishdate = SQL.Read_SQL_data("modified_finishdate", "extendduration", "project = '" + ProjectNumber + "' AND grantnumber = '" + grantNumber + "'");
            this.dateTimeModifiedEndDate.Value = Functions.TransferSQLDateToDateTime(modified_finishdate);
            //填寫日期
            string writedate = SQL.Read_SQL_data("writedate", "extendduration", "project = '" + ProjectNumber + "' AND grantnumber = '" + grantNumber + "'");
            this.dateTimeFilledDate.Value = Functions.TransferSQLDateToDateTime(writedate);
        }

        protected void btnOK_Click(object sender, EventArgs e)
        {
            label12.Visible = false;
            label13.Visible = false;
            label14.Visible = false;

            if (textBoxGrantNumber.Text == string.Empty)
                label12.Visible = true;

            if (textBoxExtendValue.Text == string.Empty)
                label13.Visible = true;

            if (textBoxTotalValue.Text == string.Empty)
                label14.Visible = true;

            if (textBoxGrantNumber.Text == string.Empty)
                return;
            if (textBoxExtendValue.Text == string.Empty)
                return;
            if (textBoxTotalValue.Text == string.Empty)
                return;

            string[] sameNo = SQL.Read1DArray_SQL_Data("grantnumber", "extendduration", "project = '" + ProjectNumber + "' AND grantnumber = '" + textBoxGrantNumber.Text + "'");
            if (sameNo.Length != 0)
            {
                label12.Text = "已存在相同核准文號";
                label12.Visible = true;
                return;
            }


            InsertIntoDB();
            this.Close();
        }
    }
}
