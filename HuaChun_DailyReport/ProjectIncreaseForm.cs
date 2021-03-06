﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace HuaChun_DailyReport
{
    public partial class ProjectIncreaseForm : Form
    {
        string dbHost;
        string dbUser;
        string dbPass;
        string dbName;
        protected MySQL SQL;


        public ProjectIncreaseForm()
        {
            InitializeComponent();
            Initialize();
        }

        private void Initialize() 
        {
            dbHost = AppSetting.LoadInitialSetting("DB_IP", "127.0.0.1");
            dbUser = AppSetting.LoadInitialSetting("DB_USER", "root");
            dbPass = AppSetting.LoadInitialSetting("DB_PASSWORD", "123");
            dbName = AppSetting.LoadInitialSetting("DB_NAME", "huachun");

            SQL = new MySQL(dbHost, dbUser, dbPass, dbName);

            numericDuration.ReadOnly = true;//初始化工期設定
            numericDays.ReadOnly = true;//初始化天數設定
            dateTimeFinish.Enabled = true;
            groupBox2.Enabled = false;
            radioBtnHolidayNeedWorking.Checked = true;
        }

        protected void InsertIntoDB()
        {
            string connStr = "server=" + dbHost + ";uid=" + dbUser + ";pwd=" + dbPass + ";database=" + dbName;
            MySqlConnection conn = new MySqlConnection(connStr);
            MySqlCommand command = conn.CreateCommand();
            conn.Open();

            string commandStr = "Insert into project_info(";
            commandStr = commandStr + "project_no,";
            commandStr = commandStr + "contract_no,";
            commandStr = commandStr + "project_name,";
            commandStr = commandStr + "project_location,";
            commandStr = commandStr + "contractor,";
            commandStr = commandStr + "owner,";
            commandStr = commandStr + "manage,";
            commandStr = commandStr + "design,";
            commandStr = commandStr + "supervise,";
            commandStr = commandStr + "responsible,";
            commandStr = commandStr + "quality,";
            commandStr = commandStr + "biddate,";
            commandStr = commandStr + "startdate,";
            commandStr = commandStr + "contract_finishdate,";
            commandStr = commandStr + "contractamount,";
            commandStr = commandStr + "contractduration,";
            commandStr = commandStr + "contractdays,";
            commandStr = commandStr + "handle1,";
            commandStr = commandStr + "phone1,";
            commandStr = commandStr + "handle2,";
            commandStr = commandStr + "phone2,";
            commandStr = commandStr + "handle3,";
            commandStr = commandStr + "phone3,";
            commandStr = commandStr + "phone4,";
            commandStr = commandStr + "handle4,";
            commandStr = commandStr + "onsite,";
            commandStr = commandStr + "security,";
            commandStr = commandStr + "computetype,";
            commandStr = commandStr + "holiday,";
            commandStr = commandStr + "rainyday";
            commandStr = commandStr + ") values('";
            commandStr = commandStr + textBoxProjectNo.Text + "','";
            commandStr = commandStr + textBoxContractNo.Text + "','";
            commandStr = commandStr + textBoxProjectName.Text + "','";
            commandStr = commandStr + textBoxProjectLocation.Text + "','";
            commandStr = commandStr + textBoxContractor.Text + "','";
            commandStr = commandStr + textBoxOwner.Text + "','";
            commandStr = commandStr + textBoxManage.Text + "','";
            commandStr = commandStr + textBoxDesign.Text + "','";
            commandStr = commandStr + textBoxSupervisor.Text + "','";
            commandStr = commandStr + textBoxResponsible.Text + "','";
            commandStr = commandStr + textBoxQuality.Text + "','";
            commandStr = commandStr + Functions.TransferDateTimeToSQL(dateTimeBid.Value)  + "','";
            commandStr = commandStr + Functions.TransferDateTimeToSQL(dateTimeStart.Value) + "','";
            commandStr = commandStr + Functions.TransferDateTimeToSQL(dateTimeFinish.Value) + "','";
            commandStr = commandStr + numericAmount.Text + "','";
            commandStr = commandStr + numericDuration.Text + "','";
            commandStr = commandStr + numericDays.Text + "','";
            commandStr = commandStr + textBoxHandle1.Text + "','";
            commandStr = commandStr + textBoxPhone1.Text + "','";
            commandStr = commandStr + textBoxHandle2.Text + "','";
            commandStr = commandStr + textBoxPhone2.Text + "','";
            commandStr = commandStr + textBoxHandle3.Text + "','";
            commandStr = commandStr + textBoxPhone3.Text + "','";
            commandStr = commandStr + textBoxHandle4.Text + "','";
            commandStr = commandStr + textBoxPhone4.Text + "','";
            commandStr = commandStr + textBoxOnsite.Text + "','";
            commandStr = commandStr + textBoxSecurity.Text + "','";
            if (radioBtnRestrictSchedule.Checked == true)
            {
                commandStr = commandStr + "1" + "','";
            }
            else if (radioBtnCalenderDay.Checked == true)
            {
                commandStr = commandStr + "2" + "','";
            }
            else if (radioBtnWorkingDay.Checked == true)
            {
                if (radioBtnNoWeekend.Checked == true)
                    commandStr = commandStr + "3" + "','";
                else if (radioBtnSun.Checked == true)
                    commandStr = commandStr + "4" + "','";
                else if (radioBtnSatSun.Checked == true)
                    commandStr = commandStr + "5" + "','";
            }

            if (radioBtnHolidayNoWorking.Checked)
                commandStr = commandStr + "1";
            else
                commandStr = commandStr + "0";

            if (radioBtnNoWorkingOnHeavyRainyDay.Checked)
                commandStr = commandStr + "1";//大雨才不計工期
            else
                commandStr = commandStr + "0";//小雨就不計工期

            commandStr = commandStr + "')";


            command.CommandText = commandStr;
            command.ExecuteNonQuery();
            conn.Close();
        }

        protected void Clear()
        {
            this.textBoxProjectNo.Clear();
            this.textBoxProjectName.Clear();
            this.textBoxContractNo.Clear();
            this.textBoxProjectLocation.Clear();
            this.textBoxContractor.Clear();
            this.textBoxOwner.Clear();
            this.textBoxManage.Clear();
            this.textBoxDesign.Clear();
            this.textBoxSupervisor.Clear();
            this.textBoxResponsible.Clear();
            this.textBoxQuality.Clear();
            this.radioBtnCalenderDay.Checked = true;
            this.radioBtnWorkingDay.Checked = false;
            this.radioBtnNoWeekend.Checked = true;
            this.radioBtnSun.Checked = false;
            this.radioBtnSatSun.Checked = false;
            this.radioBtnHolidayNeedWorking.Checked = true;
            this.radioBtnHolidayNoWorking.Checked = false;
            this.textBoxHandle1.Clear();
            this.textBoxHandle2.Clear();
            this.textBoxHandle3.Clear();
            this.textBoxHandle4.Clear();
            this.textBoxPhone1.Clear();
            this.textBoxPhone2.Clear();
            this.textBoxPhone3.Clear();
            this.textBoxPhone4.Clear();
            this.numericAmount.Value = 0;
            this.numericDays.Value = 0;
            this.numericDuration.Value = 0;
        }

        protected void calculateByDuration()
        {
            DayCompute dayCompute = new DayCompute();

            //設定工期計算方式 
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


            DateTime FinishDate = dayCompute.CountByDuration(dateTimeStart.Value, Convert.ToSingle(numericDuration.Value));
            dateTimeFinish.Value = FinishDate;
            numericDays.Value = FinishDate.Subtract(dateTimeStart.Value).Days + 1;
        }

        //private void btnCalculateByDuration_Click(object sender, EventArgs e)
        //{
        //    DayCompute dayCompute = new DayCompute(dateTimeStart.Value, Convert.ToInt32(numericDuration.Value));

        //    SetupDayComputer(dayCompute);


        //    DateTime FinishDate =  dayCompute.CountByDuration(dateTimeStart.Value, Convert.ToInt32(numericDuration.Value));
        //    dateTimeFinish.Value = FinishDate;
        //    numericDays.Value = FinishDate.Subtract(dateTimeStart.Value).Days + 1;
        //}



        //private void btnCalculateByFinish_Click(object sender, EventArgs e)
        //{
        //    DayCompute dayCompute = new DayCompute(dateTimeStart.Value, Convert.ToInt32(numericDuration.Value));
        //    SetupDayComputer(dayCompute);

        //    int duration = dateTimeFinish.Value.Date.Subtract(dateTimeStart.Value.Date).Days;
        //    numericDays.Value = duration + 1;
        //    numericDuration.Value = dayCompute.CountByFinishDay(dateTimeStart.Value, dateTimeFinish.Value);
        //}

        //private void btnCalculateByTotalDays_Click(object sender, EventArgs e)
        //{
        //    DayCompute dayCompute = new DayCompute(dateTimeStart.Value, Convert.ToInt32(numericDuration.Value));
        //    SetupDayComputer(dayCompute);

        //    dateTimeFinish.Value = dateTimeStart.Value.AddDays(Convert.ToInt32(numericDays.Value) - 1);
        //    numericDuration.Value = dayCompute.CountByFinishDay(dateTimeStart.Value, dateTimeFinish.Value);
        //}

        private void btnSearchSponsor_Click(object sender, EventArgs e)
        {
            MemberSearchForm searchForm = new MemberSearchForm(this.textBoxResponsible);
            searchForm.ShowDialog();
        }

        private void btnSearchQA_Click(object sender, EventArgs e)
        {
            MemberSearchForm searchForm = new MemberSearchForm(this.textBoxQuality);
            searchForm.ShowDialog();
        }

        private void btnSearchOnsite_Click(object sender, EventArgs e)
        {
            MemberSearchForm searchForm = new MemberSearchForm(this.textBoxOnsite);
            searchForm.ShowDialog();
        }

        private void btnSearchSecurity_Click(object sender, EventArgs e)
        {
            MemberSearchForm searchForm = new MemberSearchForm(this.textBoxSecurity);
            searchForm.ShowDialog();
        }

        protected virtual void btnSave_Click(object sender, EventArgs e)
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

            string[] sameNo = SQL.Read1DArray_SQL_Data("project_no", "project_info", "project_no = '" + textBoxProjectNo.Text + "'");
            if (sameNo.Length != 0)
            {
                label28.Text = "已存在相同工程編號";
                label28.Visible = true;
                return;
            }

            sameNo = SQL.Read1DArray_SQL_Data("contract_no", "project_info", "contract_no = '" + textBoxContractNo.Text + "'");
            if (sameNo.Length != 0)
            {
                label30.Text = "已存在相同契約號";
                label30.Visible = true;
                return;
            }


            InsertIntoDB();//插入資料進SQL
            this.Close();
        }

        private void buttonExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void radioButton123_CheckedChanged(object sender, EventArgs e)
        {
            if (radioBtnRestrictSchedule.Checked == true)//限期完工
            {
                numericDuration.ReadOnly = true;
                numericDays.ReadOnly = true;
                dateTimeFinish.Enabled = true;
                groupBox2.Enabled = false;
                //checkBoxHoliday.Enabled = false;
                radioBtnHolidayNoWorking.Enabled = false;
                radioBtnHolidayNeedWorking.Enabled = false;
                int duration = dateTimeFinish.Value.Date.Subtract(dateTimeStart.Value.Date).Days;
                this.numericDays.Value = Convert.ToDecimal(duration) + 1;
                this.numericDuration.Value = Convert.ToDecimal(duration) + 1;
                
            }
            else if (radioBtnCalenderDay.Checked == true)//日曆天
            {
                numericDuration.ReadOnly = false;
                numericDays.ReadOnly = true;
                dateTimeFinish.Enabled = false;
                groupBox2.Enabled = false;
                //checkBoxHoliday.Enabled = false;
                radioBtnHolidayNeedWorking.Enabled = false;
                radioBtnHolidayNoWorking.Enabled = false;

                int v1 = (int)Math.Round((double)numericDuration.Value / 0.5);//為了讓numericUpDown固定以0.5為單位
                numericDuration.Value = Convert.ToDecimal(v1 * 0.5);
                numericDays.Value = numericDuration.Value;
                dateTimeFinish.Value = dateTimeStart.Value.AddDays(Convert.ToInt16(Math.Ceiling(v1 * 0.5))-1);
                

            }
            else if (radioBtnWorkingDay.Checked == true)//工作天
            {
                numericDuration.ReadOnly = false;
                numericDays.ReadOnly = true;
                dateTimeFinish.Enabled = false;
                
                groupBox2.Enabled = true;
                //checkBoxHoliday.Enabled = true;
                radioBtnHolidayNoWorking.Enabled = true;
                radioBtnHolidayNeedWorking.Enabled = true;
                calculateByDuration();
            }
        }

        private void numericDays_ValueChanged(object sender, EventArgs e)
        {
            int v1 = (int)Math.Round((double)numericDays.Value / 0.5);//為了讓numericUpDown固定以0.5為單位
            numericDays.Value = Convert.ToDecimal(v1 * 0.5);

            if (radioBtnCalenderDay.Checked == true)
            {
                numericDuration.Value = numericDays.Value;
            }
        }

        private void numericDuration_ValueChanged(object sender, EventArgs e)
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
        }

        private void workingDayConditionChanged(object sender, EventArgs e)
        {
            calculateByDuration();
        }


        private void dateTimeFinish_ValueChanged(object sender, EventArgs e)
        {
            int duration = dateTimeFinish.Value.Date.Subtract(dateTimeStart.Value.Date).Days;

            if (duration < 0)
            {
                MessageBox.Show("完工日期不得早於開工日期", "錯誤", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                dateTimeFinish.Value = dateTimeStart.Value;
            }
            else
            {
                if (radioBtnRestrictSchedule.Checked == true)
                {
                    this.numericDays.Value = Convert.ToDecimal(duration) + 1;
                    this.numericDuration.Value = Convert.ToDecimal(duration) + 1;
                }
            }
        }

        private void dateTimeStart_ValueChanged(object sender, EventArgs e)
        {
            int duration = dateTimeFinish.Value.Date.Subtract(dateTimeStart.Value.Date).Days;

            if (duration < 0)
            {
                dateTimeFinish.Value = dateTimeStart.Value;
                duration = 0;
            }
            if (radioBtnRestrictSchedule.Checked == true)
            {
                this.numericDays.Value = Convert.ToDecimal(duration) + 1;
                this.numericDuration.Value = Convert.ToDecimal(duration) + 1;
            }
        }
    }
}
