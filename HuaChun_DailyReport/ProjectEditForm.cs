﻿using System;
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
            this.dataGridView1.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
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
            if (projectCount == 0)
                DisableAll();
            else
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
                    SQL.NoHistoryDelete_SQL("extendduration", "project_no = '" + this.textBoxProjectNo.Text + "' AND grantnumber = '" + grantNo + "'");
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

            string[] sameNo = SQL.Read1DArray_SQL_Data("contract_no", "project_info", "contract_no = '" + textBoxContractNo.Text + "' AND project_no <> '" + textBoxProjectNo.Text + "'");
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
                //案號及契約號
                SQL.Set_SQL_data("contract_no", "project_info", "project_no = '" + this.textBoxProjectNo.Text + "'", this.textBoxContractNo.Text);//
                //工程名稱
                SQL.Set_SQL_data("project_name", "project_info", "project_no = '" + this.textBoxProjectNo.Text + "'", this.textBoxProjectName.Text);
                //工程地點
                SQL.Set_SQL_data("project_location", "project_info", "project_no = '" + this.textBoxProjectNo.Text + "'", this.textBoxProjectLocation.Text);
                //承包廠商
                SQL.Set_SQL_data("contractor", "project_info", "project_no = '" + this.textBoxProjectNo.Text + "'", this.textBoxContractor.Text);
                //業主
                SQL.Set_SQL_data("owner", "project_info", "project_no = '" + this.textBoxProjectNo.Text + "'", this.textBoxOwner.Text);
                //專業管理
                SQL.Set_SQL_data("manage", "project_info", "project_no = '" + this.textBoxProjectNo.Text + "'", this.textBoxManage.Text);
                //設計
                SQL.Set_SQL_data("design", "project_info", "project_no = '" + this.textBoxProjectNo.Text + "'", this.textBoxDesign.Text);
                //監造
                SQL.Set_SQL_data("supervise", "project_info", "project_no = '" + this.textBoxProjectNo.Text + "'", this.textBoxSupervisor.Text);
                //工地負責人
                SQL.Set_SQL_data("responsible", "project_info", "project_no = '" + this.textBoxProjectNo.Text + "'", this.textBoxResponsible.Text);
                //品管
                SQL.Set_SQL_data("quality", "project_info", "project_no = '" + this.textBoxProjectNo.Text + "'", this.textBoxQuality.Text);
                //決標日期
                SQL.Set_SQL_data("biddate", "project_info", "project_no = '" + this.textBoxProjectNo.Text + "'", Functions.TransferDateTimeToSQL(dateTimeBid.Value));
                //開工日期
                SQL.Set_SQL_data("startdate", "project_info", "project_no = '" + this.textBoxProjectNo.Text + "'", Functions.TransferDateTimeToSQL(dateTimeStart.Value));
                //契約完工日
                SQL.Set_SQL_data("contract_finishdate", "project_info", "project_no = '" + this.textBoxProjectNo.Text + "'", Functions.TransferDateTimeToSQL(dateTimeFinish.Value));
                //契約金額
                SQL.Set_SQL_data("contractamount", "project_info", "project_no = '" + this.textBoxProjectNo.Text + "'", this.numericAmount.Text);
                //契約工期
                SQL.Set_SQL_data("contractduration", "project_info", "project_no = '" + this.textBoxProjectNo.Text + "'", this.numericDuration.Text);
                //工程總天數
                SQL.Set_SQL_data("contractdays", "project_info", "project_no = '" + this.textBoxProjectNo.Text + "'", this.numericDays.Text);
                //主辦1
                SQL.Set_SQL_data("handle1", "project_info", "project_no = '" + this.textBoxProjectNo.Text + "'", this.textBoxHandle1.Text);
                //主辦1電話
                SQL.Set_SQL_data("phone1", "project_info", "project_no = '" + this.textBoxProjectNo.Text + "'", this.textBoxPhone1.Text);
                //主辦2
                SQL.Set_SQL_data("handle2", "project_info", "project_no = '" + this.textBoxProjectNo.Text + "'", this.textBoxHandle2.Text);
                //主辦2電話
                SQL.Set_SQL_data("phone2", "project_info", "project_no = '" + this.textBoxProjectNo.Text + "'", this.textBoxPhone2.Text);
                //主辦3
                SQL.Set_SQL_data("handle3", "project_info", "project_no = '" + this.textBoxProjectNo.Text + "'", this.textBoxHandle3.Text);
                //主辦3電話
                SQL.Set_SQL_data("phone3", "project_info", "project_no = '" + this.textBoxProjectNo.Text + "'", this.textBoxPhone3.Text);
                //主辦4
                SQL.Set_SQL_data("handle4", "project_info", "project_no = '" + this.textBoxProjectNo.Text + "'", this.textBoxHandle4.Text);
                //主辦4電話
                SQL.Set_SQL_data("phone4", "project_info", "project_no = '" + this.textBoxProjectNo.Text + "'", this.textBoxPhone4.Text);
                //現場
                SQL.Set_SQL_data("onsite", "project_info", "project_no = '" + this.textBoxProjectNo.Text + "'", this.textBoxOnsite.Text);
                //勞安
                SQL.Set_SQL_data("security", "project_info", "project_no = '" + this.textBoxProjectNo.Text + "'", this.textBoxSecurity.Text);
                //工期型式
                
                if (radioBtnRestrictSchedule.Checked == true)
                {
                    SQL.Set_SQL_data("computetype", "project_info", "project_no = '" + this.textBoxProjectNo.Text + "'", "1");
                }
                else if (radioBtnCalenderDay.Checked == true)
                {
                    SQL.Set_SQL_data("computetype", "project_info", "project_no = '" + this.textBoxProjectNo.Text + "'", "2");
                }
                else if (radioBtnWorkingDay.Checked == true)
                {
                    if (radioBtnNoWeekend.Checked == true)
                        SQL.Set_SQL_data("computetype", "project_info", "project_no = '" + this.textBoxProjectNo.Text + "'", "3");
                    else if (radioBtnSun.Checked == true)
                        SQL.Set_SQL_data("computetype", "project_info", "project_no = '" + this.textBoxProjectNo.Text + "'", "4");
                    else if (radioBtnSatSun.Checked == true)
                        SQL.Set_SQL_data("computetype", "project_info", "project_no = '" + this.textBoxProjectNo.Text + "'", "5");
                }
                //計算假日
                if(radioBtnHolidayNoWorking.Checked)
                    SQL.Set_SQL_data("holiday", "project_info", "project_no = '" + this.textBoxProjectNo.Text + "'", "1");
                else
                    SQL.Set_SQL_data("holiday", "project_info", "project_no = '" + this.textBoxProjectNo.Text + "'", "0");
               
                //this.Close();
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



            if (SQL.Read_SQL_data("holiday", "project_info", "project_no = '" + projectNumber + "'") == "1")
            {
                this.radioBtnHolidayNoWorking.Checked = true;
                this.radioBtnHolidayNeedWorking.Checked = false;
            }
            else if (SQL.Read_SQL_data("holiday", "project_info", "project_no = '" + projectNumber + "'") == "0")
            {
                this.radioBtnHolidayNoWorking.Checked = false;
                this.radioBtnHolidayNeedWorking.Checked = true;
            }

            if (SQL.Read_SQL_data("rainyday", "project_info", "project_no = '" + projectNumber + "'") == "1")
            {
                this.radioBtnNoWorkingOnHeavyRainyDay.Checked = true;
                this.radioBtnNoWorkingOnSmallRainyDay.Checked = false;
            }
            else if (SQL.Read_SQL_data("rainyday", "project_info", "project_no = '" + projectNumber + "'") == "0")
            {
                this.radioBtnNoWorkingOnHeavyRainyDay.Checked = false;
                this.radioBtnNoWorkingOnSmallRainyDay.Checked = true;
            }


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

            this.numericAmount.Value = Convert.ToDecimal(SQL.Read_SQL_data("contractamount", "project_info", "project_no = '" + projectNumber + "'"));
            this.textBoxHandle1.Text = SQL.Read_SQL_data("handle1", "project_info", "project_no = '" + projectNumber + "'");
            this.textBoxHandle2.Text = SQL.Read_SQL_data("handle2", "project_info", "project_no = '" + projectNumber + "'");
            this.textBoxHandle3.Text = SQL.Read_SQL_data("handle3", "project_info", "project_no = '" + projectNumber + "'");
            this.textBoxHandle4.Text = SQL.Read_SQL_data("handle4", "project_info", "project_no = '" + projectNumber + "'");
            this.textBoxPhone1.Text = SQL.Read_SQL_data("phone1", "project_info", "project_no = '" + projectNumber + "'");
            this.textBoxPhone2.Text = SQL.Read_SQL_data("phone2", "project_info", "project_no = '" + projectNumber + "'");
            this.textBoxPhone3.Text = SQL.Read_SQL_data("phone3", "project_info", "project_no = '" + projectNumber + "'");
            this.textBoxPhone4.Text = SQL.Read_SQL_data("phone4", "project_info", "project_no = '" + projectNumber + "'");
            this.numericDays.Value = Convert.ToDecimal(SQL.Read_SQL_data("contractdays", "project_info", "project_no = '" + projectNumber + "'"));
            this.numericDuration.Value = Convert.ToDecimal(SQL.Read_SQL_data("contractduration", "project_info", "project_no = '" + projectNumber + "'"));

          
            string startDate = SQL.Read_SQL_data("startdate", "project_info", "project_no = '" + projectNumber + "'");
            dateTimeStart.Value = Functions.TransferSQLDateToDateTime(startDate);
         
            string bidDate = SQL.Read_SQL_data("biddate", "project_info", "project_no = '" + projectNumber + "'");
            dateTimeBid.Value = Functions.TransferSQLDateToDateTime(bidDate);

            string finishDate = SQL.Read_SQL_data("contract_finishdate", "project_info", "project_no = '" + projectNumber + "'");
            dateTimeFinish.Value = Functions.TransferSQLDateToDateTime(finishDate);




            LoadDataTable();
        }

        private void LoadDataTable()
        {
            dataTable.Clear();

            ArrayList array = new ArrayList();
            string[] numbers = SQL.Read1DArray_SQL_Data("grantnumber", "extendduration", "project_no = '" + this.textBoxProjectNo.Text + "' ORDER BY grantdate ASC");

            double totalValue = Convert.ToDouble(this.numericAmount.Value);
            float accuextendduration = 0;
            float totalduration = Convert.ToSingle(this.numericDuration.Value);

            DataRow dataRow;
            for (int i = 1; i <= numbers.Length; i++)
            {
                dataRow = dataTable.NewRow();
                dataRow["No"] = i.ToString();
                dataRow["核准日期"] = Functions.TransferSQLDateToDateOnly(SQL.Read_SQL_data("grantdate", "extendduration", "project_no = '" + this.textBoxProjectNo.Text + "' && grantnumber = '" + numbers[i - 1] + "'"));
                dataRow["核准文號"] = SQL.Read_SQL_data("grantnumber", "extendduration", "project_no = '" + this.textBoxProjectNo.Text + "' && grantnumber = '" + numbers[i - 1] + "'");
                dataRow["追加金額"] = SQL.Read_SQL_data("extendvalue", "extendduration", "project_no = '" + this.textBoxProjectNo.Text + "' && grantnumber = '" + numbers[i - 1] + "'");
                totalValue += Convert.ToDouble(SQL.Read_SQL_data("extendvalue", "extendduration", "project_no = '" + this.textBoxProjectNo.Text + "' && grantnumber = '" + numbers[i - 1] + "'"));
                dataRow["總金額"] = totalValue;
                dataRow["追加起算日"] = Functions.TransferSQLDateToDateOnly(SQL.Read_SQL_data("extendstartdate", "extendduration", "project_no = '" + this.textBoxProjectNo.Text + "' && grantnumber = '" + numbers[i - 1] + "'"));
                float extendDuration = Convert.ToSingle(SQL.Read_SQL_data("extendduration", "extendduration", "project_no = '" + this.textBoxProjectNo.Text + "' && grantnumber = '" + numbers[i - 1] + "'"));
                dataRow["追加工期"] = extendDuration;
                accuextendduration += extendDuration;
                dataRow["累計追加工期"] = accuextendduration;
                totalduration += extendDuration;
                dataRow["總工期"] = totalduration;
                dataRow["契約完工日"] = Functions.GetDateTimeValueSlash(dateTimeFinish.Value);

                DayCompute dayCompute = new DayCompute();

                SetupDayComputer(dayCompute);
                DateTime FinishDate = dayCompute.CountByDuration(dateTimeStart.Value, totalduration);

                dataRow["變動完工日"] = Functions.GetDateTimeValueSlash(FinishDate);//SQL.Read_SQL_data("modified_finishdate", "extendduration", "project_no = '" + this.textBoxProjectNo.Text + "' && grantnumber = '" + numbers[i - 1] + "'");
                dataRow["填寫日期"] = Functions.TransferSQLDateToDateOnly(SQL.Read_SQL_data("writedate", "extendduration", "project_no = '" + this.textBoxProjectNo.Text + "' && grantnumber = '" + numbers[i - 1] + "'"));

                dataTable.Rows.Add(dataRow);
            }
        }

        private void SetupDayComputer(DayCompute dayCompute)
        {

            if (radioBtnRestrictSchedule.Checked == true || radioBtnCalenderDay.Checked == true)
            {
                dayCompute.restOnSaturday = false;
                dayCompute.restOnSunday = false;
                dayCompute.restOnHoliday = false;
            }
            else
            {
                if (radioBtnNoWeekend.Checked == true)
                {
                    dayCompute.restOnSaturday = false;//表示週六要施工
                    dayCompute.restOnSunday = false;//表示週日要施工
                }
                else if (radioBtnSun.Checked == true)
                {
                    dayCompute.restOnSaturday = false;//表示週六要施工
                    dayCompute.restOnSunday = true;//表示週日不施工
                }
                else if (radioBtnSatSun.Checked == true)
                {
                    dayCompute.restOnSaturday = true;//表示週六不施工
                    dayCompute.restOnSunday = true;//表示週日不施工
                }

                if (radioBtnHolidayNoWorking.Checked)
                    dayCompute.restOnHoliday = true;//表示國定假日不施工
                else
                    dayCompute.restOnHoliday = false;//表示國定假日依然要施工
            }
        }

        private void TimeAndValueChanged(object sender, EventArgs e)
        {
            int v1 = (int)Math.Round((double)numericDuration.Value / 0.5);//為了讓numericUpDown固定以0.5為單位
            numericDuration.Value = Convert.ToDecimal(v1 * 0.5);

            if (radioBtnCalenderDay.Checked == true)
            {
                numericDays.Value = numericDuration.Value;
                dateTimeFinish.Value = dateTimeStart.Value.AddDays(Convert.ToInt16(Math.Ceiling(v1 * 0.5)) - 1);
            }
            else if (radioBtnWorkingDay.Checked == true)
            {
                calculateByDuration();
            }
            LoadDataTable();
        }

        private void DisableAll()
        {
            foreach (Control child in this.Controls) {
                child.Enabled = false;
            }
        }

    }
}
