﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing.Printing;

namespace HuaChun_DailyReport
{
    public partial class Main : Form
    {
        LoginForm loginForm;
        public Main()
        {
            InitializeComponent();
            Login();
            //loginForm = new LoginForm(this);
            //ToolStripMenuItem1.Enabled = false;
            //ToolStripMenuItem2.Enabled = false;
            //ToolStripMenuItem3.Enabled = false;
            //ToolStripMenuItem4.Enabled = false;
        }

        //MenuItem Click Event

        //登入登出
        private void MenuItemLogin_Click(object sender, EventArgs e)
        {
            loginForm.ShowDialog();
        }

        private void MenuItemLogout_Click(object sender, EventArgs e)
        {

        }

        //基本資料維護
        private void MenuItemProjectIncrease_Click(object sender, EventArgs e)
        {
            ProjectIncreaseForm form = new ProjectIncreaseForm();
            form.ShowDialog();
        }

        private void MenuItemProjectEdit_Click(object sender, EventArgs e)
        {
            ProjectEditForm form = new ProjectEditForm();
            form.ShowDialog();
        }

        private void MenuItemVendorIncrease_Click(object sender, EventArgs e)
        {
            VendorIncreaseForm form = new VendorIncreaseForm();
            form.ShowDialog();
        }

        private void MenuItemVendorEdit_Click(object sender, EventArgs e)
        {
            VendorEditForm form = new VendorEditForm();
            form.ShowDialog();
        }

        private void MenuItemMaterialIncrease_Click(object sender, EventArgs e)
        {
            MaterialIncreaseForm materialIncreaseForm = new MaterialIncreaseForm();
            materialIncreaseForm.ShowDialog();
        }

        private void MenuItemMaterialEdit_Click(object sender, EventArgs e)
        {
            MaterialEditForm materialEditForm = new MaterialEditForm();
            materialEditForm.ShowDialog();
        }

        private void MenuItemToolIncrease_Click(object sender, EventArgs e)
        {
            ToolIncreaseForm toolIncreaseForm = new ToolIncreaseForm();
            toolIncreaseForm.ShowDialog();
        }

        private void MenuItemToolEdit_Click(object sender, EventArgs e)
        {
            ToolEditForm toolEditForm = new ToolEditForm();
            toolEditForm.ShowDialog();
        }

        private void MenuItemLaborIncrease_Click(object sender, EventArgs e)
        {
            //新增工人別
            LaborIncreaseForm laborIncreaseForm = new LaborIncreaseForm();
            laborIncreaseForm.ShowDialog();
        }

        private void MenuItemLaborEdit_Click(object sender, EventArgs e)
        {
            LaborEditForm laborEditForm = new LaborEditForm();
            laborEditForm.ShowDialog();
        }

        private void MenuItemEmployeeIncrease_Click(object sender, EventArgs e)
        {
            MemberIncreaseForm form = new MemberIncreaseForm();
            form.ShowDialog();
        }

        private void MenuItemEmployeeEdit_Click(object sender, EventArgs e)
        {
            MemberEditForm form = new MemberEditForm();
            form.ShowDialog();
        }

        private void MenuItemHolidayManage_Click(object sender, EventArgs e)
        {
            HolidaySettingForm holidaySettingForm = new HolidaySettingForm();
            holidaySettingForm.ShowDialog();
        }

        private void MenuItemProcessCodeIncrease_Click(object sender, EventArgs e)
        {
            ProcessCodeIncreaseForm processCodeIncreaseForm = new ProcessCodeIncreaseForm();
            processCodeIncreaseForm.ShowDialog();
        }

        private void MenuItemProcessCodeEdit_Click(object sender, EventArgs e)
        {
            ProcessCodeEditForm processCodeEditForm = new ProcessCodeEditForm();
            processCodeEditForm.ShowDialog();
        }

        private void MenuItemHolidayName_Click(object sender, EventArgs e)
        {

        }

        private void MenuItemEnd_Click(object sender, EventArgs e)
        {

        }


        //日報表作業
        private void MenuItemDailyReportBuild_Click(object sender, EventArgs e)
        {
            DailyReportIncreaseForm reportBuildForm = new DailyReportIncreaseForm();
            reportBuildForm.ShowDialog();
        }

        private void MenuItemDailyReportEdit_Click(object sender, EventArgs e)
        {

        }

        private void MenuItemDailyReportCheck_Click(object sender, EventArgs e)
        {

        }
        

        //查詢廠商明細表
        private void MenuItemVendorList_Click(object sender, EventArgs e)
        {

        }
        //查詢人事明細表
        private void MenuItemEmployeeList_Click(object sender, EventArgs e)
        {

        }
        //查詢晴雨表
        private void MenuItemWeatherChart_Click(object sender, EventArgs e)
        {
            QueryWeatherChartForm queryWeatherChartForm = new QueryWeatherChartForm();
            queryWeatherChartForm.ShowDialog();
        }
        //查詢日報明細表
        private void MenuItemDailyReportList_Click(object sender, EventArgs e)
        {
            QueryDailyReportListForm queryDailyReportListForm = new QueryDailyReportListForm();
            queryDailyReportListForm.ShowDialog();
        }
        //查詢不計工期圖表
        private void MenuItemNonworkingDayChart_Click(object sender, EventArgs e)
        {
            QueryNonworkingDayChartForm queryNonworkingDayChartForm = new QueryNonworkingDayChartForm();
            queryNonworkingDayChartForm.ShowDialog();
        }
        //查詢不計工期統計表
        private void MenuItemNonworkingDayStatistic_Click(object sender, EventArgs e)
        {
            QueryNonworkingDayStatisticForm queryNonworkingDayStatisticForm = new QueryNonworkingDayStatisticForm();
            queryNonworkingDayStatisticForm.ShowDialog();
        }
        //查詢不計工期明細表
        private void MenuItemNonworkingDayDetail_Click(object sender, EventArgs e)
        {
            QueryNonworkingDayDetailForm queryNonworkingDayDetailForm = new QueryNonworkingDayDetailForm();
            queryNonworkingDayDetailForm.ShowDialog();
        }
        //查詢完工表表
        private void MenuItemFinishChart_Click(object sender, EventArgs e)
        {
            QueryFinishChartForm queryFinishChartForm = new QueryFinishChartForm();
            queryFinishChartForm.ShowDialog();
        }




        private void Main_Load(object sender, EventArgs e)
        {

        }

        private void Main_Shown(object sender, EventArgs e)
        {
            


        }

        private static void OnColumnChanged(object sender, DataColumnChangeEventArgs e)
        {
            string test = e.Column.ColumnName;
            DataRow test2 = e.Row;
            string test3 = test2.ItemArray[0].ToString();


            int i = 0;
        }

        public void Login()
        {
            ToolStripMenuItemLogin.Enabled = false;
            MenuItemBasicInfo.Enabled = true;
            MenuItemDailyReport.Enabled = true;
            MenuItemQuery.Enabled = true;
            MenuItemSystem.Enabled = true;
        }



        private void Print()
        {
            PrintDocument PD = new PrintDocument();

            //寫到 += 的時候按下Tab鍵會自動跳出後面的內容

            // 並且出現void PD_PrintPage(...)的列印事件

            PD.PrintPage += new PrintPageEventHandler(PD_PrintPage);

            PrintPreviewDialog PPD = new PrintPreviewDialog();

            PPD.Document = PD;

            PPD.ShowDialog();
        }

        void PD_PrintPage(object sender, PrintPageEventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            ClassPDFGenerator pdfGen = new ClassPDFGenerator();

        }

  


































        

  

    }
}
