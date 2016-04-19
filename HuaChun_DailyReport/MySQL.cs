using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MySql.Data.MySqlClient;
using System.Data;

namespace HuaChun_DailyReport
{
    public class MySQL
    {
        private string dbHost;
        private string dbUser;
        private string dbPass;
        private string dbName;
        public MySQL(string SQL_Host, string SQL_User, string SQL_Pass, string SQL_Name)
        {
            Host = SQL_Host;
            User = SQL_User;
            Password = SQL_Pass;
            Name = SQL_Name;
        }
        public string Host
        {
            set
            {
                dbHost = value;
            }
            get
            {
                return dbHost;
            }
        }
        public string User
        {
            set
            {
                dbUser = value;
            }
            get
            {
                return dbUser;
            }
        }
        public string Password
        {
            set
            {
                dbPass = value;
            }
            get
            {
                return dbPass;
            }
        }
        public string Name
        {
            set
            {
                dbName = value;
            }
            get
            {
                return dbName;
            }
        }

        public bool TestSQLConnect()
        {
            try
            {
                string connStr = "server=" + dbHost + ";uid=" + dbUser + ";pwd=" + dbPass + ";database=" + dbName;
                MySqlConnection conn = new MySqlConnection(connStr);
                conn.Open();
                string CmdText = "SELECT sv_value FROM sv WHERE sv_id = 8000001";
                MySqlCommand cmd = new MySqlCommand(CmdText, conn);
                object ExeScalar = cmd.ExecuteScalar();
                conn.Close();
                return true;
            }
            catch
            {
                return false;
            }
        }

        //===================================Basic function=============================================
        public void Set_SQL_data(string Value_Name, string table, string condition, string New_Value)
        {
            try
            {
                string connStr = "server=" + dbHost + ";uid=" + dbUser + ";pwd=" + dbPass + ";database=" + dbName;
                MySqlConnection conn = new MySqlConnection(connStr);
                conn.Open();
                MySqlCommand cmd = conn.CreateCommand();
                cmd.CommandText = "UPDATE " + table + " SET " + Value_Name + " = " + "'" + New_Value + "'" + " WHERE " + condition;
                cmd.ExecuteNonQuery();
                conn.Close();
            }
            catch
            {
            }
        }
        public string Read_SQL_data(string Value_Name, string table, string condition)
        {
            try
            {
                string connStr = "server=" + dbHost + ";uid=" + dbUser + ";pwd=" + dbPass + ";database=" + dbName;
                MySqlConnection conn = new MySqlConnection(connStr);
                conn.Open();
                string CmdText = "SELECT " + Value_Name + " FROM " + table + " WHERE " + condition;
                MySqlCommand cmd = new MySqlCommand(CmdText, conn);
                object ExeScalar = cmd.ExecuteScalar();
                conn.Close();
                return ExeScalar.ToString();
            }
            catch
            {
                return "";
            }
        }
        public void NoHistoryDelete_SQL(string table, string condition)
        {
            string connStr = "server=" + dbHost + ";uid=" + dbUser + ";pwd=" + dbPass + ";database=" + dbName;
            MySqlConnection conn = new MySqlConnection(connStr);
            conn.Open();
            MySqlCommand cmd = conn.CreateCommand();
            cmd.CommandText = "DELETE FROM " + table + " WHERE " + condition;
            cmd.ExecuteNonQuery();
            //OPTIMIZE TABLE  
            cmd.CommandText = "OPTIMIZE TABLE " + table;
            cmd.ExecuteNonQuery();
            conn.Close();
        }
        public string[] Read1DArray_SQL_Data(string Value_Name, string table, string condition)
        {
            string connStr = "server=" + dbHost + ";uid=" + dbUser + ";pwd=" + dbPass + ";database=" + dbName;
            MySqlConnection conn = new MySqlConnection(connStr);
            conn.Open();
            string CmdText = "SELECT " + Value_Name + " FROM " + table + " WHERE " + condition;
            MySqlCommand cmd = new MySqlCommand(CmdText, conn);
            MySqlDataReader myReader;
            List<string> data_array = new List<string>();
            myReader = cmd.ExecuteReader();
            try
            {
                while (myReader.Read())
                {
                    data_array.Add(myReader.GetString(0));
                }
            }
            catch
            {
            }
            myReader.Close();
            conn.Close();
            return data_array.ToArray();
        }
        public string[] Read1DArrayNoCondition_SQL_Data(string Value_Name, string table)
        {
            string connStr = "server=" + dbHost + ";uid=" + dbUser + ";pwd=" + dbPass + ";database=" + dbName;
            MySqlConnection conn = new MySqlConnection(connStr);
            conn.Open();
            string CmdText = "SELECT " + Value_Name + " FROM " + table;
            MySqlCommand cmd = new MySqlCommand(CmdText, conn);
            MySqlDataReader myReader;
            List<string> data_array = new List<string>();
            myReader = cmd.ExecuteReader();
            try
            {
                while (myReader.Read())
                {
                    data_array.Add(myReader.GetString(0));
                }
            }
            catch
            {
            }
            myReader.Close();
            conn.Close();
            return data_array.ToArray();
        }
        
        public DataTable Read2DArray_SQL_Data(string Value_Name, string table, string condition)
        {
            string connStr = "server=" + dbHost + ";uid=" + dbUser + ";pwd=" + dbPass + ";database=" + dbName;
            MySqlConnection conn = new MySqlConnection(connStr);
            conn.Open();
            string CmdText = "SELECT " + Value_Name + " FROM " + table + " WHERE " + condition;
            MySqlCommand cmd = new MySqlCommand(CmdText, conn);
            MySqlDataAdapter myAdapter;
            DataTable ResultTable;
            myAdapter = new MySqlDataAdapter(CmdText, conn);
            ResultTable = new DataTable();
            myAdapter.Fill(ResultTable);
            conn.Close();
            return ResultTable;
        }
        public void Truncate_SQL_Table(string table)
        {
            string connStr = "server=" + dbHost + ";uid=" + dbUser + ";pwd=" + dbPass + ";database=" + dbName;
            MySqlConnection conn = new MySqlConnection(connStr);
            conn.Open();
            MySqlCommand cmd = conn.CreateCommand();
            cmd.CommandText = "TRUNCATE TABLE " + table;
            cmd.ExecuteNonQuery();
            conn.Close();
        }

        public void Insert_SQL_ProjectDetail(string projectNo, string projectName, string projectLocation, string contractor, string date, int duration, int extend)
        {
            string connStr = "server=" + dbHost + ";uid=" + dbUser + ";pwd=" + dbPass + ";database=" + dbName;
            MySqlConnection conn = new MySqlConnection(connStr);
            conn.Open();
            MySqlCommand cmd = conn.CreateCommand();
            cmd.CommandText = "INSERT INTO project VALUES ('"
                + projectNo + "','" + projectName + "','" + projectLocation + "','" + contractor + "'," + date + "," + duration.ToString() + "," + extend.ToString() + ")";
            cmd.ExecuteNonQuery();
            conn.Close();
        }
        //===================================Basic function=============================================

    }
}
