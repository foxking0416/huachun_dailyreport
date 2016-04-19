using System;
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
            dbPass = AppSetting.LoadInitialSetting("DB_PASSWORD", "chichi1219");
            dbName = AppSetting.LoadInitialSetting("DB_NAME", "huachun");

            SQL = new MySQL(dbHost, dbUser, dbPass, dbName);

            numericUpDownDuration.ReadOnly = true;
            numericUpDownDays.ReadOnly = true;
            dateTimeFinish.Enabled = true;
            groupBox2.Enabled = false;
            checkBoxHoliday.Enabled = false;
            btnCalculateByDuration.Enabled = false;
            btnCalculateByFinish.Enabled = false;
            btnCalculateByTotalDays.Enabled = false;

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
            commandStr = commandStr + "holiday";
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
            commandStr = commandStr + Functions.GetDateTimeValue(dateTimeSigned.Value)  + "','";
            commandStr = commandStr + Functions.GetDateTimeValue(dateTimeStart.Value) + "','";
            commandStr = commandStr + Functions.GetDateTimeValue(dateTimeFinish.Value) + "','";
            commandStr = commandStr + textBoxAmount.Text + "','";
            commandStr = commandStr + numericUpDownDuration.Text + "','";
            commandStr = commandStr + numericUpDownDays.Text + "','";
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

            if(checkBoxHoliday.Checked)
                commandStr = commandStr + "1" + "','";
            else
                commandStr = commandStr + "0";
            commandStr = commandStr + "')";




            command.CommandText = commandStr;// "Insert into vendor(vendor_no,vendor_name,vendor_abbre) values('" + textBoxVendor_No.Text + "','" + textBoxVendor_Name.Text + "','" + textBoxVendor_Abbre.Text + "')";
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
            this.checkBoxHoliday.Checked = false;
            this.textBoxAmount.Clear();
            this.textBoxHandle1.Clear();
            this.textBoxHandle2.Clear();
            this.textBoxHandle3.Clear();
            this.textBoxHandle4.Clear();
            this.textBoxPhone1.Clear();
            this.textBoxPhone2.Clear();
            this.textBoxPhone3.Clear();
            this.textBoxPhone4.Clear();
            this.numericUpDownDays.Value = 0;
            this.numericUpDownDuration.Value = 0;
        }

         
        private void SetupDayComputer(DayCompute dayCompute)
        {
            string[] holidays = SQL.Read1DArrayNoCondition_SQL_Data("date", "holiday");
            for (int i = 0; i < holidays.Length; i++)
            {
                DateTime holiday = Functions.TransferSQLDateToDateTime(holidays[i]);
                dayCompute.AddHoliday(holiday);
            }

            if (radioBtnNoWeekend.Checked == true)
            {
                dayCompute.countSaturday = false;
                dayCompute.countSunday = false;
                if (checkBoxHoliday.Checked == true)
                    dayCompute.countHoliday = true;
                else
                    dayCompute.countHoliday = false;
            }
            else if (radioBtnSun.Checked == true)
            {
                dayCompute.countSaturday = false;
                dayCompute.countSunday = true;
                if (checkBoxHoliday.Checked == true)
                    dayCompute.countHoliday = true;
                else
                    dayCompute.countHoliday = false;
            }
            else if (radioBtnSatSun.Checked == true)
            {
                dayCompute.countSaturday = true;
                dayCompute.countSunday = true;
                if (checkBoxHoliday.Checked == true)
                    dayCompute.countHoliday = true;
                else
                    dayCompute.countHoliday = false;
            }
        }

        private void btnCalculateByDuration_Click(object sender, EventArgs e)
        {
            DayCompute dayCompute = new DayCompute(dateTimeStart.Value, Convert.ToInt32(numericUpDownDuration.Value));

            SetupDayComputer(dayCompute);


            DateTime FinishDate =  dayCompute.CountByDuration(dateTimeStart.Value, Convert.ToInt32(numericUpDownDuration.Value));
            dateTimeFinish.Value = FinishDate;
            numericUpDownDays.Value = FinishDate.Subtract(dateTimeStart.Value).Days + 1;
        }

        private void btnCalculateByFinish_Click(object sender, EventArgs e)
        {
            DayCompute dayCompute = new DayCompute(dateTimeStart.Value, Convert.ToInt32(numericUpDownDuration.Value));
            SetupDayComputer(dayCompute);

            int duration = dateTimeFinish.Value.Date.Subtract(dateTimeStart.Value.Date).Days;
            numericUpDownDays.Value = duration + 1;
            numericUpDownDuration.Value = dayCompute.CountByFinishDay(dateTimeStart.Value, dateTimeFinish.Value);
        }

        private void btnCalculateByTotalDays_Click(object sender, EventArgs e)
        {
            DayCompute dayCompute = new DayCompute(dateTimeStart.Value, Convert.ToInt32(numericUpDownDuration.Value));
            SetupDayComputer(dayCompute);

            dateTimeFinish.Value = dateTimeStart.Value.AddDays(Convert.ToInt32(numericUpDownDays.Value) - 1);
            numericUpDownDuration.Value = dayCompute.CountByFinishDay(dateTimeStart.Value, dateTimeFinish.Value);
        }

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


            InsertIntoDB();
            Clear();
        }

        private void buttonExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void radioButton123_CheckedChanged(object sender, EventArgs e)
        {
            if (radioBtnCalenderDay.Checked == true)
            {
                numericUpDownDuration.ReadOnly = false;
                numericUpDownDays.ReadOnly = true;
                dateTimeFinish.Enabled = false;
                groupBox2.Enabled = false;
                checkBoxHoliday.Enabled = false;
                btnCalculateByDuration.Enabled = false;
                btnCalculateByFinish.Enabled = false;
                btnCalculateByTotalDays.Enabled = false;

                int v1 = (int)Math.Round((double)numericUpDownDuration.Value / 0.5);
                numericUpDownDuration.Value = Convert.ToDecimal(v1 * 0.5);
                numericUpDownDays.Value = numericUpDownDuration.Value;
                dateTimeFinish.Value = dateTimeStart.Value.AddDays(Convert.ToInt16(Math.Ceiling(v1 * 0.5))-1);
                

            }
            else if (radioBtnWorkingDay.Checked == true)
            {
                numericUpDownDuration.ReadOnly = false;
                numericUpDownDays.ReadOnly = false;
                dateTimeFinish.Enabled = true;
                
                groupBox2.Enabled = true;
                checkBoxHoliday.Enabled = true;
                btnCalculateByDuration.Enabled = true;
                btnCalculateByFinish.Enabled = true;
                btnCalculateByTotalDays.Enabled = true;
            }
            else if (radioBtnRestrictSchedule.Checked == true)
            {
                numericUpDownDuration.ReadOnly = true;
                numericUpDownDays.ReadOnly = true;
                dateTimeFinish.Enabled = true;
                groupBox2.Enabled = false;
                checkBoxHoliday.Enabled = false;
                btnCalculateByDuration.Enabled = false;
                btnCalculateByFinish.Enabled = false;
                btnCalculateByTotalDays.Enabled = false;
                //int hours = dateTimeFinish.Value.Subtract(dateTimeStart.Value).Hours;
                int duration = dateTimeFinish.Value.Date.Subtract(dateTimeStart.Value.Date).Days;
                //if (hours > 20)
                //    duration++;
                this.numericUpDownDays.Value = Convert.ToDecimal(duration) + 1;
                this.numericUpDownDuration.Value = Convert.ToDecimal(duration) + 1;
                
            }
        }

        private void numericUpDownDays_ValueChanged(object sender, EventArgs e)
        {
            int v1 = (int)Math.Round((double)numericUpDownDays.Value / 0.5);
            numericUpDownDays.Value = Convert.ToDecimal(v1 * 0.5);

            if (radioBtnCalenderDay.Checked == true)
            {
                numericUpDownDuration.Value = numericUpDownDays.Value;
            }
        }

        private void numericUpDownDuration_ValueChanged(object sender, EventArgs e)
        {
            int v1 = (int)Math.Round((double)numericUpDownDuration.Value / 0.5);
            numericUpDownDuration.Value = Convert.ToDecimal(v1 * 0.5);

            if (radioBtnCalenderDay.Checked == true)
            {
                numericUpDownDays.Value = numericUpDownDuration.Value;
                dateTimeFinish.Value = dateTimeStart.Value.AddDays(Convert.ToInt16(Math.Ceiling(v1 * 0.5)) - 1);
            }
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
                    this.numericUpDownDays.Value = Convert.ToDecimal(duration) + 1;
                    this.numericUpDownDuration.Value = Convert.ToDecimal(duration) + 1;
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
                this.numericUpDownDays.Value = Convert.ToDecimal(duration) + 1;
                this.numericUpDownDuration.Value = Convert.ToDecimal(duration) + 1;
            }
        }
    }
}
