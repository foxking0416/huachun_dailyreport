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
    public partial class ProjectEditForm : ProjectIncreaseForm
    {
        private System.Windows.Forms.Button btnLast;
        private System.Windows.Forms.Button btnNext;
        private System.Windows.Forms.Button btnSearch;
        private System.Windows.Forms.Button btnAddExtention;
        private System.Windows.Forms.Button btnEditExtention;
        private System.Windows.Forms.Button btnDeleteExtention;
        private System.Windows.Forms.DataGridView dataGridView1;
        private string[] projects;
        private int selectIndex = 0;
        private int projectCount;
        private string grantNo = "";
        private DataTable dataTable;
        protected string rowNo;
        protected string rowName;


        public ProjectEditForm()
        {
            InitializeComponent();
            Initialize();
        }

        private void Initialize()
        {
            this.Size = new System.Drawing.Size(1000, 645);
            dataTable = new DataTable("MyNewTable");
            dataTable.Columns.Add("No", typeof(String));
            dataTable.Columns.Add("核准日期", typeof(String));
            dataTable.Columns.Add("核准文號", typeof(String));
            dataTable.Columns.Add("追加金額", typeof(String));
            dataTable.Columns.Add("總金額", typeof(String));
            dataTable.Columns.Add("追加起算日", typeof(String));
            dataTable.Columns.Add("追加工期", typeof(String));
            dataTable.Columns.Add("累計追加工期", typeof(String));
            dataTable.Columns.Add("總工期", typeof(String));
            dataTable.Columns.Add("契約完工日", typeof(String));
            dataTable.Columns.Add("變動完工日", typeof(String));
            dataTable.Columns.Add("填寫日期", typeof(String));
            // 
            // dataGridView1
            // 
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.dataGridView1.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.DisplayedCells;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Location = new System.Drawing.Point(10, 475);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.Size = new System.Drawing.Size(900, 100);
            this.dataGridView1.TabIndex = 5;
            this.dataGridView1.SelectionChanged += new System.EventHandler(this.dataGridView1_SelectionChanged);
            this.dataGridView1.DataSource = dataTable;
            this.dataGridView1.ReadOnly = true;
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.MultiSelect = false;
            this.dataGridView1.Visible = true;
            this.Text = "工程編輯作業";

            this.textBoxProjectNo.ReadOnly = true;


            this.btnSave.Location = new System.Drawing.Point(10, 580);
            this.btnExit.Location = new System.Drawing.Point(220, 580);

            this.btnLast = new System.Windows.Forms.Button();
            this.btnNext = new System.Windows.Forms.Button();
            this.btnSearch = new System.Windows.Forms.Button();
            this.btnAddExtention = new System.Windows.Forms.Button();
            this.btnEditExtention = new System.Windows.Forms.Button();
            this.btnDeleteExtention = new System.Windows.Forms.Button();

            // 
            // btnLast
            // 
            this.btnLast.Location = new System.Drawing.Point(650, 7);
            this.btnLast.Name = "btnLast";
            this.btnLast.Size = new System.Drawing.Size(100, 23);
            this.btnLast.TabIndex = 38;
            this.btnLast.Text = "上一個";
            this.btnLast.UseVisualStyleBackColor = true;
            this.btnLast.Click += new System.EventHandler(this.btnLast_Click);
            // 
            // btnNext
            // 
            this.btnNext.Location = new System.Drawing.Point(650, 47);
            this.btnNext.Name = "btnNext";
            this.btnNext.Size = new System.Drawing.Size(100, 23);
            this.btnNext.TabIndex = 39;
            this.btnNext.Text = "下一個";
            this.btnNext.UseVisualStyleBackColor = true;
            this.btnNext.Click += new System.EventHandler(this.btnNext_Click);
            // 
            // btnSearch
            // 
            this.btnSearch.Location = new System.Drawing.Point(650, 87);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(100, 23);
            this.btnSearch.TabIndex = 40;
            this.btnSearch.Text = "搜尋";
            this.btnSearch.UseVisualStyleBackColor = true;
            this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);
            // 
            // btnAddExtention
            // 
            this.btnAddExtention.Location = new System.Drawing.Point(915, 475);
            this.btnAddExtention.Name = "btnAddExtention";
            this.btnAddExtention.Size = new System.Drawing.Size(70, 23);
            this.btnAddExtention.TabIndex = 41;
            this.btnAddExtention.Text = "追加工期";
            this.btnAddExtention.UseVisualStyleBackColor = true;
            this.btnAddExtention.Click += new System.EventHandler(this.btnAddExtention_Click);
            // 
            // btnEditExtention
            // 
            this.btnEditExtention.Location = new System.Drawing.Point(915, 510);
            this.btnEditExtention.Name = "btnAddExtention";
            this.btnEditExtention.Size = new System.Drawing.Size(70, 23);
            this.btnEditExtention.TabIndex = 42;
            this.btnEditExtention.Text = "編輯追加";
            this.btnEditExtention.UseVisualStyleBackColor = true;
            this.btnEditExtention.Click += new System.EventHandler(this.btnEditExtention_Click);
            // 
            // btnDeleteExtention
            // 
            this.btnDeleteExtention.Location = new System.Drawing.Point(915, 545);
            this.btnDeleteExtention.Name = "btnDeleteExtention";
            this.btnDeleteExtention.Size = new System.Drawing.Size(70, 23);
            this.btnDeleteExtention.TabIndex = 43;
            this.btnDeleteExtention.Text = "刪除追加";
            this.btnDeleteExtention.UseVisualStyleBackColor = true;
            this.btnDeleteExtention.Click += new System.EventHandler(this.btnDeleteExtention_Click);

            this.Controls.Add(this.btnLast);
            this.Controls.Add(this.btnNext);
            this.Controls.Add(this.btnSearch);
            this.Controls.Add(this.btnAddExtention);
            this.Controls.Add(this.btnEditExtention);
            this.Controls.Add(this.btnDeleteExtention);
            this.Controls.Add(this.dataGridView1);

            projects = SQL.Read1DArrayNoCondition_SQL_Data("project_no", "project_info");
            projectCount = projects.Length;
            //if (projectCount != 0)
            //    this.btnClear.Enabled = true;
            //else
            //    this.btnClear.Enabled = false;
            LoadInformation(projects[selectIndex]);
        }

        private void btnLast_Click(object sender, EventArgs e)
        {
            selectIndex--;
            if (selectIndex < 0)
                selectIndex = projectCount - 1;

            LoadInformation(projects[selectIndex]);
        }

        private void btnNext_Click(object sender, EventArgs e)
        {
            selectIndex++;
            if (selectIndex >= projectCount)
                selectIndex = 0;

            LoadInformation(projects[selectIndex]);
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            ProjectSearchForm searchform = new ProjectSearchForm(this);
            searchform.Show();
        }

        private void btnAddExtention_Click(object sender, EventArgs e)
        {
            ExtentionIncreaseForm addExtentionForm = new ExtentionIncreaseForm(textBoxProjectNo.Text);
            addExtentionForm.ShowDialog();
            LoadDataTable();
        }

        private void btnEditExtention_Click(object sender, EventArgs e)
        {
            ExtentionEditForm editExtentionForm = new ExtentionEditForm(textBoxProjectNo.Text, grantNo);
            editExtentionForm.ShowDialog();
            LoadDataTable();
        }

        private void btnDeleteExtention_Click(object sender, EventArgs e)
        {
            if (grantNo != string.Empty)
            {
                DialogResult result = MessageBox.Show("確定要刪除工期追加資料?", "確定", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation);
                if (result == DialogResult.Yes)
                {
                    SQL.NoHistoryDelete_SQL("extendduration", "project = '" + this.textBoxProjectNo.Text + "' AND grantnumber = '" + grantNo + "'");
                }
            }
            else
            {
                MessageBox.Show("請選擇要刪除的資料?", "確定", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            LoadDataTable();
        }

        protected override void btnSave_Click(object sender, EventArgs e)
        {
            label28.Visible = false;
            label29.Visible = false;
            label30.Visible = false;

            if (textBoxProjectNo.Text == string.Empty)
                label28.Visible = true;

            if (textBoxProjectName.Text == string.Empty)
                label29.Visible = true;

            if (textBoxContractNo.Text == string.Empty)
                label30.Visible = true;

            if (textBoxProjectNo.Text == string.Empty)
                return;
            if (textBoxProjectName.Text == string.Empty)
                return;
            if (textBoxContractNo.Text == string.Empty)
                return;

            string[] sameNo = SQL.Read1DArray_SQL_Data("contract_no", "project_info", "contract_no = '" + textBoxContractNo.Text + "'");
            if (sameNo.Length != 0)
            {
                label30.Text = "已存在相同契約號";
                label30.Visible = true;
                return;
            }


            //覆寫原有資料
            DialogResult result = MessageBox.Show("確定要修改工程資料?", "確定", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation);
            if (result == DialogResult.Yes)
            {
                ////案號及契約號
                //SQL.Set_SQL_data("contract_no", "member", "account = '" + this.textBoxAccount.Text + "'", this.textBoxID.Text);//
                ////工程名稱
                //SQL.Set_SQL_data("project_name", "member", "account = '" + this.textBoxAccount.Text + "'", (radioBtnSexM.Checked) ? ("1") : ("2"));
                ////工程地點
                //SQL.Set_SQL_data("project_location", "member", "account = '" + this.textBoxAccount.Text + "'", Functions.GetDateTimeValue(dateTimeBirthdate.Value));
                ////承包廠商
                //SQL.Set_SQL_data("contractor", "member", "account = '" + this.textBoxAccount.Text + "'", this.textBoxEducation.Text);//degree
                ////業主
                //SQL.Set_SQL_data("owner", "member", "account = '" + this.textBoxAccount.Text + "'", this.comboBoxCity.Text);//resident_city
                ////專業管理
                //SQL.Set_SQL_data("manage", "member", "account = '" + this.textBoxAccount.Text + "'", this.comboBoxDistrict.Text);//resident_district
                ////設計
                //SQL.Set_SQL_data("design", "member", "account = '" + this.textBoxAccount.Text + "'", this.textBoxAddress.Text);//resident_address
                ////監造
                //SQL.Set_SQL_data("supervise", "member", "account = '" + this.textBoxAccount.Text + "'", this.comboBoxCity2.Text);//living_city
                ////工地負責人
                //SQL.Set_SQL_data("responsible", "member", "account = '" + this.textBoxAccount.Text + "'", this.comboBoxDistrict2.Text);//living_district
                ////品管
                //SQL.Set_SQL_data("quality", "member", "account = '" + this.textBoxAccount.Text + "'", this.textBoxAddress2.Text);//living_address
                ////決標日期
                //SQL.Set_SQL_data("biddate", "member", "account = '" + this.textBoxAccount.Text + "'", this.textBoxPhone.Text);//phone
                ////開工日期
                //SQL.Set_SQL_data("startdate", "member", "account = '" + this.textBoxAccount.Text + "'", this.textBoxCell.Text);//cell
                ////契約完工日
                //SQL.Set_SQL_data("contract_finishdate", "member", "account = '" + this.textBoxAccount.Text + "'", Functions.GetDateTimeValue(dateTimeStart.Value));//startdate
                ////契約金額
                //SQL.Set_SQL_data("contractamount", "member", "account = '" + this.textBoxAccount.Text + "'", Functions.GetDateTimeValue(dateTimeInsurance.Value));//insurancedate
                ////契約工期
                //SQL.Set_SQL_data("contractduration", "member", "account = '" + this.textBoxAccount.Text + "'", Functions.GetDateTimeValue(dateTimeLeave.Value));//enddate
                ////工程總天數
                //SQL.Set_SQL_data("contractdays", "member", "account = '" + this.textBoxAccount.Text + "'", this.textBoxPosition.Text);//position
                ////主辦1
                //SQL.Set_SQL_data("handle1", "member", "account = '" + this.textBoxAccount.Text + "'", this.textBoxServiceYear.Text);//serviceyear
                ////主辦1電話
                //SQL.Set_SQL_data("phone1", "member", "account = '" + this.textBoxAccount.Text + "'", this.numericRelative.Text);//relative
                ////主辦2
                //SQL.Set_SQL_data("handle2", "member", "account = '" + this.textBoxAccount.Text + "'", this.textBoxBankName.Text);//bank_name
                ////主辦2電話
                //SQL.Set_SQL_data("phone2", "member", "account = '" + this.textBoxAccount.Text + "'", this.textBoxBankAccount.Text);//bank_account
                ////主辦3
                //SQL.Set_SQL_data("handle3", "member", "account = '" + this.textBoxAccount.Text + "'", this.textBoxServiceYear.Text);//serviceyear
                ////主辦3電話
                //SQL.Set_SQL_data("phone3", "member", "account = '" + this.textBoxAccount.Text + "'", this.numericRelative.Text);//relative
                ////主辦4
                //SQL.Set_SQL_data("handle4", "member", "account = '" + this.textBoxAccount.Text + "'", this.textBoxBankName.Text);//bank_name
                ////主辦4電話
                //SQL.Set_SQL_data("phone4", "member", "account = '" + this.textBoxAccount.Text + "'", this.textBoxBankAccount.Text);//bank_account
                ////現場
                //SQL.Set_SQL_data("onsite", "member", "account = '" + this.textBoxAccount.Text + "'", this.textBoxBankAccount.Text);//bank_account
                ////勞安
                //SQL.Set_SQL_data("security", "member", "account = '" + this.textBoxAccount.Text + "'", this.textBoxBankAccount.Text);//bank_account
                ////
                //SQL.Set_SQL_data("workingtype", "member", "account = '" + this.textBoxAccount.Text + "'", "1");//

                //SQL.Set_SQL_data("onjob", "member", "account = '" + this.textBoxAccount.Text + "'", "1");//
                //if (radioButton1.Checked)
                    
                //else if (radioButton2.Checked)
                //    SQL.Set_SQL_data("workingtype", "member", "account = '" + this.textBoxAccount.Text + "'", "2");//
                //else if (radioButton3.Checked)
                //    SQL.Set_SQL_data("workingtype", "member", "account = '" + this.textBoxAccount.Text + "'", "3");//
                //else if (radioButton4.Checked)
                //    SQL.Set_SQL_data("workingtype", "member", "account = '" + this.textBoxAccount.Text + "'", "4");//
                //else if (radioButton5.Checked)
                //    SQL.Set_SQL_data("workingtype", "member", "account = '" + this.textBoxAccount.Text + "'", "5");//
                //else if (radioButton6.Checked)
                //    SQL.Set_SQL_data("workingtype", "member", "account = '" + this.textBoxAccount.Text + "'", "6");//

                //if (radioBtnOnJobN.Checked)
                //    SQL.Set_SQL_data("onjob", "member", "account = '" + this.textBoxAccount.Text + "'", "1");//
                //else if (radioBtnOnJobY.Checked)
                //    SQL.Set_SQL_data("onjob", "member", "account = '" + this.textBoxAccount.Text + "'", "2");//

                this.Close();
            }
        }

        private void dataGridView1_SelectionChanged(object sender, EventArgs e)
        {
            try
            {
                grantNo = dataGridView1[2, dataGridView1.CurrentRow.Index].Value.ToString();
            }
            catch
            { }
        }

        public void LoadInformation(string projectNumber)
        {
            this.textBoxProjectNo.Text = SQL.Read_SQL_data("project_no", "project_info", "project_no = '" + projectNumber + "'");
            this.textBoxProjectName.Text = SQL.Read_SQL_data("project_name", "project_info", "project_no = '" + projectNumber + "'");
            this.textBoxContractNo.Text = SQL.Read_SQL_data("contract_no", "project_info", "project_no = '" + projectNumber + "'");
            this.textBoxProjectLocation.Text = SQL.Read_SQL_data("project_location", "project_info", "project_no = '" + projectNumber + "'");
            this.textBoxContractor.Text = SQL.Read_SQL_data("contractor", "project_info", "project_no = '" + projectNumber + "'");
            this.textBoxOwner.Text = SQL.Read_SQL_data("owner", "project_info", "project_no = '" + projectNumber + "'");
            this.textBoxManage.Text = SQL.Read_SQL_data("manage", "project_info", "project_no = '" + projectNumber + "'");
            this.textBoxDesign.Text = SQL.Read_SQL_data("design", "project_info", "project_no = '" + projectNumber + "'");
            this.textBoxSupervisor.Text = SQL.Read_SQL_data("supervise", "project_info", "project_no = '" + projectNumber + "'");
            this.textBoxResponsible.Text = SQL.Read_SQL_data("responsible", "project_info", "project_no = '" + projectNumber + "'");
            this.textBoxQuality.Text = SQL.Read_SQL_data("quality", "project_info", "project_no = '" + projectNumber + "'");
            this.textBoxOnsite.Text = SQL.Read_SQL_data("onsite", "project_info", "project_no = '" + projectNumber + "'");
            this.textBoxSecurity.Text = SQL.Read_SQL_data("security", "project_info", "project_no = '" + projectNumber + "'");

            this.textBoxAmount.Text = SQL.Read_SQL_data("contractamount", "project_info", "project_no = '" + projectNumber + "'");
            this.textBoxHandle1.Text = SQL.Read_SQL_data("handle1", "project_info", "project_no = '" + projectNumber + "'");
            this.textBoxHandle2.Text = SQL.Read_SQL_data("handle2", "project_info", "project_no = '" + projectNumber + "'");
            this.textBoxHandle3.Text = SQL.Read_SQL_data("handle3", "project_info", "project_no = '" + projectNumber + "'");
            this.textBoxHandle4.Text = SQL.Read_SQL_data("handle4", "project_info", "project_no = '" + projectNumber + "'");
            this.textBoxPhone1.Text = SQL.Read_SQL_data("phone1", "project_info", "project_no = '" + projectNumber + "'");
            this.textBoxPhone2.Text = SQL.Read_SQL_data("phone2", "project_info", "project_no = '" + projectNumber + "'");
            this.textBoxPhone3.Text = SQL.Read_SQL_data("phone3", "project_info", "project_no = '" + projectNumber + "'");
            this.textBoxPhone4.Text = SQL.Read_SQL_data("phone4", "project_info", "project_no = '" + projectNumber + "'");
            this.numericUpDownDays.Value = Convert.ToDecimal(SQL.Read_SQL_data("contractdays", "project_info", "project_no = '" + projectNumber + "'"));
            this.numericUpDownDuration.Value = Convert.ToDecimal(SQL.Read_SQL_data("contractduration", "project_info", "project_no = '" + projectNumber + "'"));

          
            string startDate = SQL.Read_SQL_data("startdate", "project_info", "project_no = '" + projectNumber + "'");
            dateTimeStart.Value = Functions.TransferSQLDateToDateTime(startDate);
         
            string bidDate = SQL.Read_SQL_data("biddate", "project_info", "project_no = '" + projectNumber + "'");
            dateTimeSigned.Value = Functions.TransferSQLDateToDateTime(bidDate);

            string finishDate = SQL.Read_SQL_data("contract_finishdate", "project_info", "project_no = '" + projectNumber + "'");
            dateTimeFinish.Value = Functions.TransferSQLDateToDateTime(finishDate);

            if (SQL.Read_SQL_data("computetype", "project_info", "project_no = '" + projectNumber + "'") == "1")
            {
                this.radioBtnRestrictSchedule.Checked = true;
                this.radioBtnCalenderDay.Checked = false;
                this.radioBtnWorkingDay.Checked = false;
                this.radioBtnNoWeekend.Checked = true;
                this.radioBtnSun.Checked = false;
                this.radioBtnSatSun.Checked = false;
            }
            else if (SQL.Read_SQL_data("computetype", "project_info", "project_no = '" + projectNumber + "'") == "2")
            {
                this.radioBtnRestrictSchedule.Checked = false;
                this.radioBtnCalenderDay.Checked = true;
                this.radioBtnWorkingDay.Checked = false;
                this.radioBtnNoWeekend.Checked = true;
                this.radioBtnSun.Checked = false;
                this.radioBtnSatSun.Checked = false;
            }
            else if (SQL.Read_SQL_data("computetype", "project_info", "project_no = '" + projectNumber + "'") == "3")
            {
                this.radioBtnRestrictSchedule.Checked = false;
                this.radioBtnCalenderDay.Checked = false;
                this.radioBtnWorkingDay.Checked = true;
                this.radioBtnNoWeekend.Checked = true;
                this.radioBtnSun.Checked = false;
                this.radioBtnSatSun.Checked = false;
            }
            else if (SQL.Read_SQL_data("computetype", "project_info", "project_no = '" + projectNumber + "'") == "4")
            {
                this.radioBtnRestrictSchedule.Checked = false;
                this.radioBtnCalenderDay.Checked = false;
                this.radioBtnWorkingDay.Checked = true;
                this.radioBtnNoWeekend.Checked = false;
                this.radioBtnSun.Checked = true;
                this.radioBtnSatSun.Checked = false;
            }
            else if (SQL.Read_SQL_data("computetype", "project_info", "project_no = '" + projectNumber + "'") == "5")
            {
                this.radioBtnRestrictSchedule.Checked = false;
                this.radioBtnCalenderDay.Checked = false;
                this.radioBtnWorkingDay.Checked = true;
                this.radioBtnNoWeekend.Checked = false;
                this.radioBtnSun.Checked = false;
                this.radioBtnSatSun.Checked = true;
            }



            if(SQL.Read_SQL_data("holiday", "project_info", "project_no = '" + projectNumber + "'") == "1")
                this.checkBoxHoliday.Checked = true;
            else if (SQL.Read_SQL_data("holiday", "project_info", "project_no = '" + projectNumber + "'") == "0")
                this.checkBoxHoliday.Checked = false;


            LoadDataTable();
        }

        private void LoadDataTable()
        {
            dataTable.Clear();

            ArrayList array = new ArrayList();
            string[] numbers = SQL.Read1DArray_SQL_Data("grantnumber", "extendduration", "project = '" + this.textBoxProjectNo.Text + "' ORDER BY grantdate ASC");

            DataRow dataRow;
            for (int i = 1; i <= numbers.Length; i++)
            {
                dataRow = dataTable.NewRow();
                dataRow["No"] = i.ToString();
                dataRow["核准日期"] = SQL.Read_SQL_data("grantdate", "extendduration", "project = '" + this.textBoxProjectNo.Text + "' && grantnumber = '" + numbers[i - 1] + "'");
                dataRow["核准文號"] = SQL.Read_SQL_data("grantnumber", "extendduration", "project = '" + this.textBoxProjectNo.Text + "' && grantnumber = '" + numbers[i - 1] + "'");
                dataRow["追加金額"] = SQL.Read_SQL_data("extendvalue", "extendduration", "project = '" + this.textBoxProjectNo.Text + "' && grantnumber = '" + numbers[i - 1] + "'");
                dataRow["總金額"] = SQL.Read_SQL_data("totalvalue", "extendduration", "project = '" + this.textBoxProjectNo.Text + "' && grantnumber = '" + numbers[i - 1] + "'");
                dataRow["追加起算日"] = SQL.Read_SQL_data("extendstartdate", "extendduration", "project = '" + this.textBoxProjectNo.Text + "' && grantnumber = '" + numbers[i - 1] + "'");
                dataRow["追加工期"] = SQL.Read_SQL_data("extendduration", "extendduration", "project = '" + this.textBoxProjectNo.Text + "' && grantnumber = '" + numbers[i - 1] + "'");
                dataRow["累計追加工期"] = SQL.Read_SQL_data("accuextendduration", "extendduration", "project = '" + this.textBoxProjectNo.Text + "' && grantnumber = '" + numbers[i - 1] + "'");
                dataRow["總工期"] = SQL.Read_SQL_data("totalduration", "extendduration", "project = '" + this.textBoxProjectNo.Text + "' && grantnumber = '" + numbers[i - 1] + "'");
                dataRow["契約完工日"] = SQL.Read_SQL_data("contract_finishdate", "extendduration", "project = '" + this.textBoxProjectNo.Text + "' && grantnumber = '" + numbers[i - 1] + "'");
                dataRow["變動完工日"] = SQL.Read_SQL_data("modified_finishdate", "extendduration", "project = '" + this.textBoxProjectNo.Text + "' && grantnumber = '" + numbers[i - 1] + "'");
                dataRow["填寫日期"] = SQL.Read_SQL_data("writedate", "extendduration", "project = '" + this.textBoxProjectNo.Text + "' && grantnumber = '" + numbers[i - 1] + "'");

                dataTable.Rows.Add(dataRow);
            }


        }
    }
}
