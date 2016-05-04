using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Collections;
using MySql.Data.MySqlClient;

namespace HuaChun_DailyReport
{

    public partial class MemberIncreaseForm : Form
    {

        string dbHost;
        string dbUser;
        string dbPass;
        string dbName;
        protected MySQL SQL;
        ArrayList arrayCity;

        public MemberIncreaseForm()
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

            arrayCity = new ArrayList();

            string[] cities = SQL.Read1DArrayNoCondition_SQL_Data("distinct city", "city");

            for (int i = 0; i < cities.Length; i++)
            {
                comboBoxCity.Items.Add(cities[i]);
                comboBoxCity2.Items.Add(cities[i]);
            }
        }

        protected void InsertIntoDB()
        {
            string connStr = "server=" + dbHost + ";uid=" + dbUser + ";pwd=" + dbPass + ";database=" + dbName;
            MySqlConnection conn = new MySqlConnection(connStr);
            MySqlCommand command = conn.CreateCommand();
            conn.Open();

            string commandStr = "Insert into member(";
            commandStr = commandStr + "account,";//系統帳號
            commandStr = commandStr + "name,";//姓名
            commandStr = commandStr + "password,";//密碼  
            commandStr = commandStr + "level,";//職級
            commandStr = commandStr + "dayoff,";//5
            commandStr = commandStr + "id,";//身分證字號
            commandStr = commandStr + "sex,";//性別
            commandStr = commandStr + "birthdate,";//生日
            commandStr = commandStr + "degree,";//學歷
            commandStr = commandStr + "resident_city,";//戶籍城市
            commandStr = commandStr + "resident_district,";//戶籍區
            commandStr = commandStr + "resident_address,";//戶籍街道
            commandStr = commandStr + "living_city,";//現居城市
            commandStr = commandStr + "living_district,";//現居區
            commandStr = commandStr + "living_address,";//現居街道
            commandStr = commandStr + "phone,";//市內電話
            commandStr = commandStr + "cell,";//行動電話
            commandStr = commandStr + "startdate,";//到職日
            commandStr = commandStr + "insurancedate,";//勞保日期
            commandStr = commandStr + "enddate,";//離職日
            commandStr = commandStr + "position,";//值位
            commandStr = commandStr + "serviceyear,";
            commandStr = commandStr + "relative,";//撫養親屬
            commandStr = commandStr + "bank_name,";//銀行戶名
            commandStr = commandStr + "bank_account,";//銀行帳號
            commandStr = commandStr + "workingtype,";//25
            commandStr = commandStr + "onjob";
            commandStr = commandStr + ") values('";
            commandStr = commandStr + textBoxAccount.Text + "','";
            commandStr = commandStr + textBoxName.Text + "','";
            commandStr = commandStr + "" + "','";//password
            commandStr = commandStr + "" + "','";//level
            commandStr = commandStr + "" + "','";//dayoff
            commandStr = commandStr + textBoxID.Text + "','";//id
            commandStr = (radioBtnSexM.Checked) ? (commandStr + "1" + "','") : (commandStr + "2" + "','");//sex
            commandStr = commandStr + Functions.GetDateTimeValue(dateTimeBirthdate.Value) + "','";//birthdate
            commandStr = commandStr + textBoxEducation.Text + "','";//degree
            commandStr = commandStr + comboBoxCity.Text + "','";//resident_city
            commandStr = commandStr + comboBoxDistrict.Text + "','";//resident_district
            commandStr = commandStr + textBoxAddress.Text + "','";//resident_address
            commandStr = commandStr + comboBoxCity2.Text + "','";//living_city
            commandStr = commandStr + comboBoxDistrict2.Text + "','";//living_district
            commandStr = commandStr + textBoxAddress2.Text + "','";//living_address
            commandStr = commandStr + textBoxPhone.Text + "','";//phone
            commandStr = commandStr + textBoxCell.Text + "','";//cell
            commandStr = commandStr + Functions.GetDateTimeValue(dateTimeStart.Value) + "','";//startdate
            commandStr = commandStr + Functions.GetDateTimeValue(dateTimeInsurance.Value) + "','";//insurancedate
            commandStr = commandStr + Functions.GetDateTimeValue(dateTimeLeave.Value) + "','";//enddate
            commandStr = commandStr + textBoxPosition.Text + "','";//position
            commandStr = commandStr + textBoxServiceYear.Text + "','";//serviceyear
            commandStr = commandStr + numericRelative.Text + "','";//relative
            commandStr = commandStr + textBoxBankName.Text + "','";//bank_name
            commandStr = commandStr + textBoxBankAccount.Text + "','";//bank_account
            if(radioButton1.Checked)
                commandStr = commandStr + "1" + "','";//working type
            else if(radioButton2.Checked)
                commandStr = commandStr + "2" + "','";
            else if (radioButton3.Checked)
                commandStr = commandStr + "3" + "','";
            else if (radioButton4.Checked)
                commandStr = commandStr + "4" + "','";
            else if (radioButton5.Checked)
                commandStr = commandStr + "5" + "','";
            else if (radioButton6.Checked)
                commandStr = commandStr + "6" + "','";

            if (radioBtnOnJobN.Checked)
                commandStr = commandStr + "1";//onjob
            else if (radioBtnOnJobY.Checked)
                commandStr = commandStr + "2";
            commandStr = commandStr + "')";




            command.CommandText = commandStr;// "Insert into vendor(vendor_no,vendor_name,vendor_abbre) values('" + textBoxVendor_No.Text + "','" + textBoxVendor_Name.Text + "','" + textBoxVendor_Abbre.Text + "')";
            command.ExecuteNonQuery();
            conn.Close();
        }

        protected void Clear()
        {
            textBoxAccount.Clear();
            textBoxName.Clear();
            textBoxID.Clear();
            textBoxEducation.Clear();
            textBoxAddress.Clear();
            textBoxAddress2.Clear();
            comboBoxCity.Text = "";
            comboBoxCity2.Text = "";
            comboBoxDistrict.Text = "";
            comboBoxDistrict2.Text = "";
            checkBoxSame.Checked = false;
            textBoxPhone.Clear();
            textBoxCell.Clear();
            numericRelative.Value = 0;

            textBoxPosition.Clear();
            textBoxBankName.Clear();
            textBoxBankAccount.Clear();

            radioBtnSexM.Checked = true;
            radioBtnSexF.Checked = false;
            radioButton1.Checked = true;
            radioButton2.Checked = false;
            radioButton3.Checked = false;
            radioButton4.Checked = false;
            radioButton5.Checked = false;
            radioButton6.Checked = false;
            radioBtnOnJobY.Checked = true;
            radioBtnOnJobN.Checked = false;

        }

        private void checkBoxSame_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBoxSame.Checked)
            {
                comboBoxCity2.Text = comboBoxCity.Text;
                comboBoxDistrict2.Text = comboBoxDistrict.Text;
                textBoxAddress2.Text = textBoxAddress.Text;
            }
            else
            {
                comboBoxCity2.Text = "";
                comboBoxDistrict2.Text = "";
                textBoxAddress2.Text = "";
            }
        }

        private void comboBoxCity_SelectedIndexChanged(object sender, EventArgs e)
        {
            string[] districts = SQL.Read1DArray_SQL_Data("district", "city", "city = '" + comboBoxCity.SelectedItem + "'");
            comboBoxDistrict.Items.Clear();
            for (int i = 0; i < districts.Length; i++)
            {
                comboBoxDistrict.Items.Add(districts[i]);
            }
            comboBoxDistrict.SelectedIndex = 0;
        }

        private void comboBoxCity2_SelectedIndexChanged(object sender, EventArgs e)
        {
            string[] districts = SQL.Read1DArray_SQL_Data("district", "city", "city = '" + comboBoxCity2.SelectedItem + "'");
            comboBoxDistrict2.Items.Clear();
            for (int i = 0; i < districts.Length; i++)
            {
                comboBoxDistrict2.Items.Add(districts[i]);
            }
            comboBoxDistrict2.SelectedIndex = 0;
        }

        protected virtual void btnSave_Click(object sender, EventArgs e)
        {
            InsertIntoDB();
            this.Close();
        }

        protected virtual void btnClear_Click(object sender, EventArgs e)
        {
            Clear();
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

    }
}
