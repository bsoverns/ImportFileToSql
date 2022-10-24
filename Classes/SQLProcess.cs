using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Net;
using System.Data.SqlClient;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Xml;
using System.Windows.Forms;
//using ImportFileToSQLDB.Classes;

namespace FileReadToSQLDB
{
    class SQLGBSCustomProcess
    {
        public void SqlCommandProcess(string sqlCommandQuery, SQLConnectionClass SQLConnect, string FilePath)
        {
            LogWriter errorLog = new LogWriter();
            SqlConnection sqlConnString = new SqlConnection("Data Source=" + SQLConnect.dataSource + ";Initial Catalog=" + SQLConnect.initialCatalog + ";User ID=" + SQLConnect.userID + ";Password=" + SQLConnect.password + ";Connect Timeout = 10;");

            try
            {
                if (SQLConnect.useIPAdress == true)
                {
                    IPAddress ip = new IPHelper().get_ip_from_host_name(SQLConnect.dataSource);
                    sqlConnString = new SqlConnection("Data Source=" + ip.ToString() + ";Initial Catalog=" + SQLConnect.initialCatalog + ";User ID=" + SQLConnect.userID + ";Password=" + SQLConnect.password + ";Connect Timeout = 10;");

                    //MessageBox.Show(ip.ToString());
                }

                SqlCommand queryCommand = sqlConnString.CreateCommand();
                queryCommand.CommandText = sqlCommandQuery;                

                if (sqlConnString.State.ToString() == "Closed")
                {
                    sqlConnString.Open();
                }

                queryCommand.ExecuteNonQuery();
            }

            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
                errorLog.write_log(FilePath.ToString(), sqlCommandQuery.ToString());
            }

            finally
            {
                if (sqlConnString.State.ToString() == "Open")
                {
                    sqlConnString.Close();
                }
            }
        }

        public string cleanEnc(string encNbr)
        {
            Regex digitsOnly = new Regex(@"[^\d]");
            return digitsOnly.Replace(encNbr, "");
        }

        public string cleanBody(string body)
        {
            string newBody = body.Replace("\r\n", "\r");
            newBody = newBody.Replace("\n", "\r");
            newBody = newBody.Replace("\r", "\r\n");
            return newBody;
        }
    }
}

