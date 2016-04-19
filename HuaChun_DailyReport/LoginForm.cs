using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
using System.Collections;

namespace HuaChun_DailyReport
{
    public partial class LoginForm : Form
    {
        string dbHost;//資料庫位址
        string dbUser;//資料庫使用者帳號
        string dbPass;//資料庫使用者密碼
        string dbName;//資料庫名稱
        MySQL SQL;

        Main mainForm;

        public LoginForm(Main form)
        {
            mainForm = form;
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
        }

        private void LoginForm_Load(object sender, EventArgs e)
        {

        }

        private void btnLoginOk_Click(object sender, EventArgs e)
        {


            if (textBoxAccount.Text == string.Empty)
            {
                MessageBox.Show("請輸入使用者帳號", "錯誤", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else if (textBoxPassword.Text == string.Empty)
            {
                MessageBox.Show("請輸入使用者密碼", "錯誤", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {

                string member = SQL.Read_SQL_data("name", "member", "account = '" + textBoxAccount.Text +"' AND password = '" + textBoxPassword.Text + "'");

                if (member == string.Empty)
                {
                    DialogResult result = MessageBox.Show("密碼與帳號不符", "錯誤", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    if (result == DialogResult.OK)
                    {
                        textBoxPassword.Text = string.Empty;
                    }
                }
                else
                {
                    DialogResult result = MessageBox.Show(member + "您好", "歡迎", MessageBoxButtons.OK);
                    if (result == DialogResult.OK)
                    {
                        this.mainForm.Login();
                        this.Close();
                    }
                }
            }
        }



        private void btnLoginCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
