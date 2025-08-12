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
    // SQLCustomProcess.cs (replace with this)
    using System;
    using System.Data.SqlClient;
    using System.Net;

    namespace FileReadToSQLDB
    {
        class SQLCustomProcess : IDisposable
        {
            private SqlConnection _conn;
            private SqlTransaction _tx;
            private bool _initialized;

            public void Initialize(SQLConnectionClass SQLConnect)
            {
                if (_initialized) return;

                string dataSource = SQLConnect.dataSource;

                if (SQLConnect.useIPAdress)
                {
                    var ip = new IPHelper().get_ip_from_host_name(SQLConnect.dataSource);
                    dataSource = ip?.ToString() ?? SQLConnect.dataSource;
                }

                string cs = $"Data Source={dataSource};Initial Catalog={SQLConnect.initialCatalog};User ID={SQLConnect.userID};Password={SQLConnect.password};Connect Timeout=30;";

                _conn = new SqlConnection(cs);
                _conn.Open();
                _tx = _conn.BeginTransaction();   // one tx for all statements in this file
                _initialized = true;
            }

            public void ExecuteNonQuery(string sql)
            {
                using (var cmd = _conn.CreateCommand())
                {
                    cmd.Transaction = _tx;
                    cmd.CommandTimeout = 0;       // long batches okay
                    cmd.CommandText = sql;
                    cmd.ExecuteNonQuery();
                }
            }

            public void Commit()
            {
                _tx?.Commit();
            }

            public void Dispose()
            {
                try { _tx?.Dispose(); } catch { }
                try { if (_conn?.State == System.Data.ConnectionState.Open) _conn.Close(); } catch { }
                try { _conn?.Dispose(); } catch { }
            }
        }
    }

    //class SQLCustomProcess
    //{
    //    public void SqlCommandProcess(string sqlCommandQuery, SQLConnectionClass SQLConnect, string FilePath)
    //    {
    //        LogWriter errorLog = new LogWriter();
    //        SqlConnection sqlConnString = new SqlConnection("Data Source=" + SQLConnect.dataSource + ";Initial Catalog=" + SQLConnect.initialCatalog + ";User ID=" + SQLConnect.userID + ";Password=" + SQLConnect.password + ";Connect Timeout = 10;");

    //        try
    //        {
    //            if (SQLConnect.useIPAdress == true)
    //            {
    //                IPAddress ip = new IPHelper().get_ip_from_host_name(SQLConnect.dataSource);
    //                sqlConnString = new SqlConnection("Data Source=" + ip.ToString() + ";Initial Catalog=" + SQLConnect.initialCatalog + ";User ID=" + SQLConnect.userID + ";Password=" + SQLConnect.password + ";Connect Timeout = 10;");

    //                //MessageBox.Show(ip.ToString());
    //            }

    //            SqlCommand queryCommand = sqlConnString.CreateCommand();
    //            queryCommand.CommandText = sqlCommandQuery;                

    //            if (sqlConnString.State.ToString() == "Closed")
    //            {
    //                sqlConnString.Open();
    //            }

    //            queryCommand.ExecuteNonQuery();
    //        }

    //        catch (Exception ex)
    //        {
    //            MessageBox.Show(ex.ToString());
    //            errorLog.write_log(FilePath.ToString(), sqlCommandQuery.ToString());
    //        }

    //        finally
    //        {
    //            if (sqlConnString.State.ToString() == "Open")
    //            {
    //                sqlConnString.Close();
    //            }
    //        }
    //    }

    //    public string cleanEnc(string encNbr)
    //    {
    //        Regex digitsOnly = new Regex(@"[^\d]");
    //        return digitsOnly.Replace(encNbr, "");
    //    }

    //    public string cleanBody(string body)
    //    {
    //        string newBody = body.Replace("\r\n", "\r");
    //        newBody = newBody.Replace("\n", "\r");
    //        newBody = newBody.Replace("\r", "\r\n");
    //        return newBody;
    //    }
    //}
}

