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
    public partial class MemberEditForm : MemberIncreaseForm
    {
        private System.Windows.Forms.Button btnLast;
        private System.Windows.Forms.Button btnNext;
        private System.Windows.Forms.Button btnSearch;
        private string[] members;
        private int selectIndex = 0;
        private int memberCount;
        public MemberEditForm()
        {
            InitializeComponent();
            Initialize();
        }

        private void Initialize()
        {
            this.Size = new System.Drawing.Size(543, 510);
            this.Text = "人事編輯作業";

            this.textBoxAccount.ReadOnly = true;
            this.textBoxName.ReadOnly = true;
            this.btnClear.Text = "刪除";
            
            this.btnLast = new System.Windows.Forms.Button();
            this.btnNext = new System.Windows.Forms.Button();
            this.btnSearch = new System.Windows.Forms.Button();

            // 
            // btnLast
            // 
            this.btnLast.Location = new System.Drawing.Point(14, 419);
            this.btnLast.Name = "btnLast";
            this.btnLast.Size = new System.Drawing.Size(165, 23);
            this.btnLast.TabIndex = 2;
            this.btnLast.Text = "上一個";
            this.btnLast.UseVisualStyleBackColor = true;
            this.btnLast.Click += new System.EventHandler(this.btnLast_Click);
            // 
            // btnNext
            // 
            this.btnNext.Location = new System.Drawing.Point(185, 419);
            this.btnNext.Name = "btnNext";
            this.btnNext.Size = new System.Drawing.Size(165, 23);
            this.btnNext.TabIndex = 2;
            this.btnNext.Text = "下一個";
            this.btnNext.UseVisualStyleBackColor = true;
            this.btnNext.Click += new System.EventHandler(this.btnNext_Click);
            // 
            // btnSearch
            // 
            this.btnSearch.Location = new System.Drawing.Point(356, 419);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(165, 23);
            this.btnSearch.TabIndex = 2;
            this.btnSearch.Text = "搜尋";
            this.btnSearch.UseVisualStyleBackColor = true;
            this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);

            this.Controls.Add(this.btnLast);
            this.Controls.Add(this.btnNext);
            this.Controls.Add(this.btnSearch);

            this.btnSave.Location = new System.Drawing.Point(14, 445);
            this.btnClear.Location = new System.Drawing.Point(185, 445);
            this.btnExit.Location = new System.Drawing.Point(356, 445);

            members = SQL.Read1DArrayNoCondition_SQL_Data("account", "member");
            memberCount = members.Length;
            if (memberCount != 0)
                this.btnClear.Enabled = true;
            else
                this.btnClear.Enabled = false;
            LoadInformation(members[selectIndex]);
        }

        private void btnLast_Click(object sender, EventArgs e)
        {
            selectIndex--;
            if (selectIndex < 0)
                selectIndex = memberCount - 1;

            LoadInformation(members[selectIndex]);
        }

        private void btnNext_Click(object sender, EventArgs e)
        {
            selectIndex++;
            if (selectIndex >= memberCount)
                selectIndex = 0;

            LoadInformation(members[selectIndex]);
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            MemberSearchForm searchform = new MemberSearchForm(this);
            searchform.Show();
        }

        protected override void btnClear_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("確定要刪除員工資料?", "確定", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation);
            if (result == DialogResult.Yes)
            {
                SQL.NoHistoryDelete_SQL("account", "member = '" + this.textBoxAccount.Text + "'");

                members = SQL.Read1DArrayNoCondition_SQL_Data("vendor_no", "vendor");
                memberCount = members.Length;
                --selectIndex;
                if (selectIndex >= 0)
                    LoadInformation(members[selectIndex]);
                else
                {
                    this.btnClear.Enabled = false;
                    selectIndex = 0;
                    Clear();
                }
            }
        }

        protected override void btnSave_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("確定要修改人事資料?", "確定", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation);
            if (result == DialogResult.Yes)
            {
                SQL.Set_SQL_data("id", "member", "account = '" + this.textBoxAccount.Text + "'", this.textBoxID.Text);//
                SQL.Set_SQL_data("sex", "member", "account = '" + this.textBoxAccount.Text + "'", (radioBtnSexM.Checked) ? ("1") : ("2"));
                SQL.Set_SQL_data("birthdate", "member", "account = '" + this.textBoxAccount.Text + "'", Functions.TransferDateTimeToSQL(dateTimeBirthdate.Value));
                SQL.Set_SQL_data("degree", "member", "account = '" + this.textBoxAccount.Text + "'", this.textBoxEducation.Text);//degree
                SQL.Set_SQL_data("resident_city", "member", "account = '" + this.textBoxAccount.Text + "'", this.comboBoxCity.Text);//resident_city
                SQL.Set_SQL_data("resident_district", "member", "account = '" + this.textBoxAccount.Text + "'", this.comboBoxDistrict.Text);//resident_district
                SQL.Set_SQL_data("resident_address", "member", "account = '" + this.textBoxAccount.Text + "'", this.textBoxAddress.Text);//resident_address
                SQL.Set_SQL_data("living_city", "member", "account = '" + this.textBoxAccount.Text + "'", this.comboBoxCity2.Text);//living_city
                SQL.Set_SQL_data("living_district", "member", "account = '" + this.textBoxAccount.Text + "'", this.comboBoxDistrict2.Text);//living_district
                SQL.Set_SQL_data("living_address", "member", "account = '" + this.textBoxAccount.Text + "'", this.textBoxAddress2.Text);//living_address
                SQL.Set_SQL_data("phone", "member", "account = '" + this.textBoxAccount.Text + "'", this.textBoxPhone.Text);//phone
                SQL.Set_SQL_data("cell", "member", "account = '" + this.textBoxAccount.Text + "'", this.textBoxCell.Text);//cell
                SQL.Set_SQL_data("startdate", "member", "account = '" + this.textBoxAccount.Text + "'", Functions.TransferDateTimeToSQL(dateTimeStart.Value));//startdate
                SQL.Set_SQL_data("insurancedate", "member", "account = '" + this.textBoxAccount.Text + "'", Functions.TransferDateTimeToSQL(dateTimeInsurance.Value));//insurancedate
                SQL.Set_SQL_data("enddate", "member", "account = '" + this.textBoxAccount.Text + "'", Functions.TransferDateTimeToSQL(dateTimeLeave.Value));//enddate
                SQL.Set_SQL_data("position", "member", "account = '" + this.textBoxAccount.Text + "'", this.textBoxPosition.Text);//position
                SQL.Set_SQL_data("serviceyear", "member", "account = '" + this.textBoxAccount.Text + "'", this.textBoxServiceYear.Text);//serviceyear
                SQL.Set_SQL_data("relative", "member", "account = '" + this.textBoxAccount.Text + "'", this.numericRelative.Text);//relative
                SQL.Set_SQL_data("bank_name", "member", "account = '" + this.textBoxAccount.Text + "'", this.textBoxBankName.Text);//bank_name
                SQL.Set_SQL_data("bank_account", "member", "account = '" + this.textBoxAccount.Text + "'", this.textBoxBankAccount.Text);//bank_account
                
                if (radioButton1.Checked)
                    SQL.Set_SQL_data("workingtype", "member", "account = '" + this.textBoxAccount.Text + "'", "1");//
                else if (radioButton2.Checked)
                    SQL.Set_SQL_data("workingtype", "member", "account = '" + this.textBoxAccount.Text + "'", "2");//
                else if (radioButton3.Checked)
                    SQL.Set_SQL_data("workingtype", "member", "account = '" + this.textBoxAccount.Text + "'", "3");//
                else if (radioButton4.Checked)
                    SQL.Set_SQL_data("workingtype", "member", "account = '" + this.textBoxAccount.Text + "'", "4");//
                else if (radioButton5.Checked)
                    SQL.Set_SQL_data("workingtype", "member", "account = '" + this.textBoxAccount.Text + "'", "5");//
                else if (radioButton6.Checked)
                    SQL.Set_SQL_data("workingtype", "member", "account = '" + this.textBoxAccount.Text + "'", "6");//

                if (radioBtnOnJobN.Checked)
                    SQL.Set_SQL_data("onjob", "member", "account = '" + this.textBoxAccount.Text + "'", "1");//
                else if (radioBtnOnJobY.Checked)
                    SQL.Set_SQL_data("onjob", "member", "account = '" + this.textBoxAccount.Text + "'", "2");//
                
                this.Close();
            }
        }

        public void LoadInformation(string member_account)
        {
            this.textBoxAccount.Text = SQL.Read_SQL_data("account", "member", "account = '" + member_account + "'");
            this.textBoxName.Text = SQL.Read_SQL_data("name", "member", "account = '" + member_account + "'");
            this.textBoxID.Text = SQL.Read_SQL_data("id", "member", "account = '" + member_account + "'");
            if (SQL.Read_SQL_data("sex", "member", "account = '" + member_account + "'") == "1")
                radioBtnSexM.Checked = true;
            else
                radioBtnSexF.Checked = true;

            string birthDate = SQL.Read_SQL_data("birthdate", "member", "account = '" + member_account + "'");
            dateTimeBirthdate.Value = Functions.TransferSQLDateToDateTime(birthDate);

            this.textBoxEducation.Text = SQL.Read_SQL_data("degree", "member", "account = '" + member_account + "'");
            this.comboBoxCity.Text = SQL.Read_SQL_data("resident_city", "member", "account = '" + member_account + "'");
            this.comboBoxDistrict.Text = SQL.Read_SQL_data("resident_district", "member", "account = '" + member_account + "'");
            this.textBoxAddress.Text = SQL.Read_SQL_data("resident_address", "member", "account = '" + member_account + "'");
            this.comboBoxCity2.Text = SQL.Read_SQL_data("living_city", "member", "account = '" + member_account + "'");
            this.comboBoxDistrict2.Text = SQL.Read_SQL_data("living_district", "member", "account = '" + member_account + "'");
            this.textBoxAddress2.Text = SQL.Read_SQL_data("living_address", "member", "account = '" + member_account + "'");
            this.textBoxPhone.Text = SQL.Read_SQL_data("phone", "member", "account = '" + member_account + "'");
            this.textBoxCell.Text = SQL.Read_SQL_data("cell", "member", "account = '" + member_account + "'");

            string startDate = SQL.Read_SQL_data("startdate", "member", "account = '" + member_account + "'");
            dateTimeStart.Value = Functions.TransferSQLDateToDateTime(startDate);


            string insuranceDate = SQL.Read_SQL_data("insurancedate", "member", "account = '" + member_account + "'");
            dateTimeInsurance.Value = Functions.TransferSQLDateToDateTime(insuranceDate);


            string leaveDate = SQL.Read_SQL_data("enddate", "member", "account = '" + member_account + "'");
            dateTimeLeave.Value = Functions.TransferSQLDateToDateTime(leaveDate);

            this.textBoxPosition.Text = SQL.Read_SQL_data("position", "member", "account = '" + member_account + "'");
            //this.textBoxServiceYear.Text = SQL.Read_SQL_data("phone1", "vendor", "vendor_no = '" + member_account + "'");
            this.numericRelative.Text = SQL.Read_SQL_data("relative", "member", "account = '" + member_account + "'");
            this.textBoxBankName.Text = SQL.Read_SQL_data("bank_name", "member", "account = '" + member_account + "'");
            this.textBoxBankAccount.Text = SQL.Read_SQL_data("bank_account", "member", "account = '" + member_account + "'");
            if (SQL.Read_SQL_data("workingtype", "member", "account = '" + member_account + "'") == "1")
                radioButton1.Checked = true;
            else if (SQL.Read_SQL_data("workingtype", "member", "account = '" + member_account + "'") == "2")
                radioButton2.Checked = true;
            else if (SQL.Read_SQL_data("workingtype", "member", "account = '" + member_account + "'") == "3")
                radioButton3.Checked = true;
            else if (SQL.Read_SQL_data("workingtype", "member", "account = '" + member_account + "'") == "4")
                radioButton4.Checked = true;
            else if (SQL.Read_SQL_data("workingtype", "member", "account = '" + member_account + "'") == "5")
                radioButton5.Checked = true;
            else if (SQL.Read_SQL_data("workingtype", "member", "account = '" + member_account + "'") == "6")
                radioButton6.Checked = true;

            if (SQL.Read_SQL_data("onjob", "member", "account = '" + member_account + "'") == "1")
                radioBtnOnJobY.Checked = true;
            else
                radioBtnOnJobN.Checked = true;

        }

    }
}
