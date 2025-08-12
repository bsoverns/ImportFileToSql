//SELECT('SELECT * FROM .[' + TABLE_SCHEMA + '].[' + TABLE_NAME + ']')[SELECT], 
//('DROP TABLE .[' + TABLE_SCHEMA + '].[' + TABLE_NAME + ']')[DROP],
//*FROM INFORMATION_SCHEMA.TABLES

//CSV EXAMPLE SOURCE: https://people.sc.fsu.edu/~jburkardt/data/csv/csv.html

using FileReadToSQLDB.FileReadToSQLDB;
using Microsoft.VisualBasic.FileIO;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Net;
using System.Text;
using System.Threading;
using System.Windows.Forms;
//using FileReadToSQLDB.Classes;

namespace FileReadToSQLDB
{
    public partial class FrmMain : Form
    {
        string DataSource; //used to save the datasource from the dsnNames
        string InitialCatalog; //used to save the InitialCatalog        
        string ReportRow;
        string ReportField;
        int threadCountToUse = 0;
        int fileProcessThreadCount = 0;
        string fileType = "*.csv";
        bool isQuoted = false;
        string delimiters = ",";        
        SQLConnectionClass SQLConnect = new SQLConnectionClass("", "", "", "", false);

        private int _activeFileWorkers = 0;
        private volatile bool _folderProcessing = false;

        public FrmMain()
        {
            InitializeComponent();
            SetDefaults();
        }

        private void SetDefaults()
        {
            cmbDelimiter.SelectedItem = "comma (,)";
            cmbQuoted.SelectedItem = "True";
            cmbThreadCount.SelectedItem = "30";
            cmbFileType.SelectedItem = "*.csv";

#if DEBUG
            txtInstance.Text = @"BRADHP";
            txtDatabase.Text = @"GoogleAnalysis";
            txtLogin.Text = @"sa";
            txtPassword.Text = @"Tenet007";
            txtFolder.Text = @"D:\@CSVs";
#endif
        }

        private void btnBrowse_Click(object sender, EventArgs e)
        {
            //Files
            var fileContent = string.Empty;
            var filePath = string.Empty;

            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                //openFileDialog.InitialDirectory = @"c:\";
                openFileDialog.InitialDirectory = @"D:\@CSVs\";
                openFileDialog.Filter = "csv files (*.csv)|*.csv|txt files (*.txt)|*.txt|All files (*.*)|*.*";
                openFileDialog.FilterIndex = 3;
                openFileDialog.RestoreDirectory = true;

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    //Get the path of specified file
                    filePath = openFileDialog.FileName;

                    txtFile.Text = filePath;
                }
            }
        }

        private void btnConnect_Click(object sender, EventArgs e)
        {
            if (txtInstance.Text != "" && txtDatabase.Text != "" && txtLogin.Text != "" && txtPassword.Text != "")
            {
                btnConnect.Enabled = false;
                txtDatabase.Enabled = false;
                txtInstance.Enabled = false;
                txtLogin.Enabled = false;
                txtPassword.Enabled = false;
                ckbxSQL_DirectIP.Enabled = false;
                btnConnect.Text = "Please wait";

                SQLConnect.dataSource = txtInstance.Text;
                SQLConnect.initialCatalog = txtDatabase.Text;
                SQLConnect.userID = txtLogin.Text;
                SQLConnect.password = txtPassword.Text;
                SQLConnect.useIPAdress = ckbxSQL_DirectIP.Checked;

                try
                {
                    DataSource = txtInstance.Text;
                    InitialCatalog = txtDatabase.Text;

                    SqlConnection sqlConnString = new SqlConnection("Data Source=" + SQLConnect.dataSource + ";Initial Catalog=" + SQLConnect.initialCatalog + ";User ID=" + SQLConnect.userID + ";Password=" + SQLConnect.password + ";Connect Timeout = 10;");

                    if (SQLConnect.useIPAdress == true)
                    {
                        IPAddress ip = new IPHelper().get_ip_from_host_name(SQLConnect.dataSource);
                        sqlConnString = new SqlConnection("Data Source=" + ip.ToString() + ";Initial Catalog=" + SQLConnect.initialCatalog + ";User ID=" + SQLConnect.userID + ";Password=" + SQLConnect.password + ";Connect Timeout = 10;");

                        //MessageBox.Show(ip.ToString());
                    }

                    //version
                    SqlDataAdapter read_version = new SqlDataAdapter("select top 1 TABLE_NAME from INFORMATION_SCHEMA.TABLES", sqlConnString);
                    DataTable version_table = new DataTable();

                    sqlConnString.Open();
                    read_version.Fill(version_table);
                    sqlConnString.Close();

                    btnConnect.Text = "Valid";
                    grpFileImport.Enabled = true;
                    grpFolderImport.Enabled = true;
                    grpBoxExportType.Enabled = true;
                    version_table.Dispose();
                }

                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Error connecting to database");
                    btnConnect.Enabled = true;
                    btnConnect.Text = "Connect";
                }
            }
        }

        private void btnProcess_Click(object sender, EventArgs e)
        {
            cmbDelimiter.Enabled = false;
            cmbQuoted.Enabled = false;
            grpFileImport.Enabled = false;
            grpFolderImport.Enabled = false;
            delimiters = DelimiterTypeCheck(cmbDelimiter.SelectedItem.ToString());
            isQuoted = Convert.ToBoolean(cmbQuoted.SelectedItem.ToString());
            fileType = cmbFileType.SelectedItem.ToString();

            //Thread process_file = new Thread(processFile);
            Thread process_file = new Thread(() => processFile(txtFile.Text.ToString()));
            process_file.IsBackground = true;
            process_file.Start();
            //processFile(txtFile.Text.ToString());
        }

        //private void processFile(string fileToProcess)
        //private void processFile(string fileInformation)
        //{
        //    //string fileToProcess = txtFile.Text.ToString();
        //    string fileToProcess = fileInformation;
        //    System.IO.FileInfo fileData = new System.IO.FileInfo(fileToProcess);
        //    LogWriter errorLog = new LogWriter();
        //    //MessageBox.Show(fileData.Name);

        //    try
        //    {
        //        if (File.Exists(fileData.FullName))
        //        {
        //            using (TextFieldParser parser = new TextFieldParser(fileData.FullName))
        //            {
        //                int row = 1;
        //                int column = 1;
        //                int columnCount = 1;
        //                int insertCount = 1;
        //                string createQueryBuilder = "";
        //                string insertQueryBuilder = "";
        //                string valueQueryBuilder = " VALUES (";
        //                string tableName = ".dbo.[" + fileData.Name + "]";

        //                string checkTableExistsQuery = $"IF OBJECT_ID('{tableName}', 'U') IS NOT NULL BEGIN TRUNCATE TABLE {tableName}; END ELSE BEGIN CREATE TABLE {tableName} (";
        //                parser.TextFieldType = FieldType.Delimited;
        //                parser.HasFieldsEnclosedInQuotes = isQuoted;
        //                parser.SetDelimiters(delimiters);

        //                while(!parser.EndOfData)
        //                {
        //                    string[] fields = parser.ReadFields();
        //                    foreach(string field in fields)
        //                    {
        //                        if (column == 1 && row == 1)
        //                        {
        //                            ReportRow = row.ToString();
        //                            ReportField = field.ToString();
        //                            //createQueryBuilder = "CREATE TABLE .dbo.[" + fileData.Name + "] (";
        //                            createQueryBuilder = checkTableExistsQuery;
        //                            insertQueryBuilder = $"INSERT INTO {tableName} (";
        //                            Invoke(new UIUpdate(StartUpdate));
        //                        }

        //                        if (row == 1)
        //                        {
        //                            if (column == 1)
        //                            {
        //                                //createQueryBuilder = createQueryBuilder + "[" + field.ToString() + "] VARCHAR(MAX)";
        //                                createQueryBuilder = createQueryBuilder + "[" + field.ToString() + "] VARCHAR(4000)";
        //                                insertQueryBuilder = insertQueryBuilder + "[" + field.ToString() + "]";
        //                            }

        //                            else
        //                            {
        //                                //createQueryBuilder = createQueryBuilder + ", " + "[" + field.ToString() + "] VARCHAR(MAX)";
        //                                createQueryBuilder = createQueryBuilder + ", " + "[" + field.ToString() + "] VARCHAR(4000)";
        //                                insertQueryBuilder = insertQueryBuilder + ", " + "[" + field.ToString() + "]";
        //                            }
        //                            columnCount++;
        //                        }

        //                        if (row != 1)
        //                        {
        //                            if (column == 1)
        //                            {
        //                                if (field.ToString().ToUpper() == "NULL")
        //                                {
        //                                    valueQueryBuilder = valueQueryBuilder + field.ToString();
        //                                }

        //                                else
        //                                    valueQueryBuilder = valueQueryBuilder + "'" + field.ToString().Replace("'", "''") + "'";
        //                            }

        //                            else
        //                            {
        //                                if (field.ToString().ToUpper() == "NULL")
        //                                {
        //                                    valueQueryBuilder = valueQueryBuilder + ", " + field.ToString();
        //                                }

        //                                else
        //                                {
        //                                    valueQueryBuilder = valueQueryBuilder + ", " + "'" + field.ToString().Replace("'", "''") + "'";
        //                                }
        //                            }
        //                            insertCount++;
        //                        }

        //                        //MessageBox.Show("ColumnTest " + field.ToString());
        //                        column++;
        //                    }

        //                    if (row == 1)
        //                    {
        //                        createQueryBuilder = createQueryBuilder + ") END";
        //                        insertQueryBuilder = insertQueryBuilder + ")";

        //                        SQLCustomProcess CreateTable = new SQLCustomProcess();
        //                        CreateTable.SqlCommandProcess(createQueryBuilder, SQLConnect, fileData.DirectoryName.ToString());
        //                    }

        //                    if (row != 1)
        //                    {
        //                        valueQueryBuilder = valueQueryBuilder + ");";

        //                        if (columnCount == insertCount)
        //                        {
        //                            SQLCustomProcess InsertData = new SQLCustomProcess();
        //                            InsertData.SqlCommandProcess((insertQueryBuilder + " " + valueQueryBuilder), SQLConnect, fileData.DirectoryName.ToString());
        //                        }

        //                        else
        //                        {
        //                            if (columnCount > insertCount)
        //                            {
        //                                //MessageBox.Show("Less items than expected: " + columnCount.ToString() + " > " + insertCount.ToString()); 
        //                                errorLog.write_log(fileData.DirectoryName.ToString(), (fileData.FullName.ToString() + " - " + "Less items than expected: " + columnCount.ToString() + " > " + insertCount.ToString() + " - "
        //                                        + "Row => " + row.ToString()));
        //                            }

        //                            else if (columnCount < insertCount)
        //                            {
        //                                //MessageBox.Show("More items than expected: " + columnCount.ToString() + " < " + insertCount.ToString()); 
        //                                errorLog.write_log(fileData.DirectoryName.ToString(), (fileData.FullName.ToString() + " - " + "More items than expected: " + columnCount.ToString() + " < " + insertCount.ToString() + " - "
        //                                        + "Row => " + row.ToString()));
        //                            }

        //                            else
        //                            {
        //                                //MessageBox.Show("Unknown: " + columnCount.ToString() + " < " + insertCount.ToString());
        //                                errorLog.write_log(fileData.DirectoryName.ToString(), (fileData.FullName.ToString() + " - " + "Unknown: " + columnCount.ToString() + " < " + insertCount.ToString() + " - "
        //                                        + "Row => " + row.ToString()));
        //                            }
        //                        }
        //                    }

        //                    //log.write_log(logDirectory, "Create note for encounter: " + enc_nbr.ToString() + "...." + DateTime.Now.ToString("hh:mm:ss"));
        //                    column = 1;
        //                    row++;
        //                    valueQueryBuilder = " VALUES (";
        //                    insertCount = 1;
        //                }
        //            }
        //        }
        //    }

        //    catch (Exception ex)
        //    {
        //        //MessageBox.Show(ex.ToString());
        //        errorLog.write_log(fileData.DirectoryName.ToString(), ex.ToString() + " - ");
        //    }

        //    finally
        //    {
        //        //fileProcessThreadCount = Process.GetCurrentProcess().Threads.Count;
        //        //fileData.MoveTo(@"C:\Users\brads\Desktop\Voters\Files\Backup\" + fileData.Name.ToString());
        //        //if (fileProcessThreadCount < (threadCountToUse))                
        //        //{
        //        //    MessageBox.Show("Done");
        //        //    grpFileImport.Enabled = true;
        //        //    grpFolderImport.Enabled = true;
        //        //}

        //        Invoke(new UIUpdate(CompleteProcess));
        //    }            
        //}

        private void processFile(string fileInformation)
        {
            var fileData = new FileInfo(fileInformation);
            var errorLog = new LogWriter();

            // Sanitize the SQL object name: drop extension and use [dbo].[FileName]
            //string rawName = Path.GetFileNameWithoutExtension(fileData.Name);
            //string tableName = $"[dbo].[{rawName}]";

            // Keep the original filename (including .csv) as the table name
            // Escape closing bracket just in case a filename ever contains ']'
            string safeFileName = fileData.Name.Replace("]", "]]");
            string tableName = $"[dbo].[{safeFileName}]";


            try
            {
                if (!File.Exists(fileData.FullName))
                    return;

                using (var parser = new TextFieldParser(fileData.FullName))
                using (var sql = new SQLCustomProcess())
                {
                    // initialize once per file
                    sql.Initialize(SQLConnect);

                    int row = 1, column = 1, columnCount = 1, insertCount = 1;
                    string createQueryBuilder = "";
                    string insertQueryBuilder = "";
                    string valueQueryBuilder = " VALUES (";

                    string checkTableExistsQuery =
                        $"IF OBJECT_ID(N'{tableName}', N'U') IS NOT NULL " +
                        $"BEGIN TRUNCATE TABLE {tableName}; END " +
                        $"ELSE BEGIN CREATE TABLE {tableName} (";

                    parser.TextFieldType = FieldType.Delimited;
                    parser.HasFieldsEnclosedInQuotes = isQuoted;
                    parser.SetDelimiters(delimiters);

                    while (!parser.EndOfData)
                    {
                        string[] fields = parser.ReadFields();
                        foreach (string field in fields)
                        {
                            if (column == 1 && row == 1)
                            {
                                ReportRow = row.ToString();
                                ReportField = field;
                                createQueryBuilder = checkTableExistsQuery;
                                insertQueryBuilder = $"INSERT INTO {tableName} (";
                                Invoke(new UIUpdate(StartUpdate));
                            }

                            if (row == 1)
                            {
                                if (column == 1)
                                {
                                    createQueryBuilder += $"[{field}] VARCHAR(4000)";
                                    insertQueryBuilder += $"[{field}]";
                                }
                                else
                                {
                                    createQueryBuilder += $", [{field}] VARCHAR(4000)";
                                    insertQueryBuilder += $", [{field}]";
                                }
                                columnCount++;
                            }
                            else
                            {
                                if (column == 1)
                                {
                                    valueQueryBuilder += string.Equals(field, "NULL", StringComparison.OrdinalIgnoreCase)
                                                         ? "NULL"
                                                         : $"'{field.Replace("'", "''")}'";
                                }
                                else
                                {
                                    valueQueryBuilder += string.Equals(field, "NULL", StringComparison.OrdinalIgnoreCase)
                                                         ? ", NULL"
                                                         : $", '{field.Replace("'", "''")}'";
                                }
                                insertCount++;
                            }

                            column++;
                        }

                        if (row == 1)
                        {
                            createQueryBuilder += ") END";
                            insertQueryBuilder += ")";

                            // run once per file on the single connection
                            sql.ExecuteNonQuery(createQueryBuilder);
                        }
                        else
                        {
                            valueQueryBuilder += ");";

                            if (columnCount == insertCount)
                            {
                                sql.ExecuteNonQuery(insertQueryBuilder + " " + valueQueryBuilder);
                            }
                            else
                            {
                                if (columnCount > insertCount)
                                    errorLog.write_log(fileData.DirectoryName, $"{fileData.FullName} - Less items than expected: {columnCount} > {insertCount} - Row => {row}");
                                else if (columnCount < insertCount)
                                    errorLog.write_log(fileData.DirectoryName, $"{fileData.FullName} - More items than expected: {columnCount} < {insertCount} - Row => {row}");
                                else
                                    errorLog.write_log(fileData.DirectoryName, $"{fileData.FullName} - Unknown: {columnCount} <> {insertCount} - Row => {row}");
                            }
                        }

                        column = 1;
                        row++;
                        valueQueryBuilder = " VALUES (";
                        insertCount = 1;
                    }

                    // commit once per file
                    sql.Commit();
                }
            }

            catch (Exception ex)
            {
                errorLog.write_log(fileData.DirectoryName, ex.ToString() + " - ");
            }

            //finally
            //{
            //    Invoke(new UIUpdate(CompleteProcess));
            //}

            finally
            {
                // For single-file mode, keep your existing UI reset
                // For folder mode, only when the last worker finishes
                if (_folderProcessing)
                {
                    if (System.Threading.Interlocked.Decrement(ref _activeFileWorkers) == 0)
                    {
                        // marshal to UI thread
                        this.BeginInvoke((Action)(OnAllFilesCompleted));
                    }
                }
                else
                {
                    Invoke(new UIUpdate(CompleteProcess));
                }
            }

        }

        private void OnAllFilesCompleted()
        {
            _folderProcessing = false;

            // Re-enable UI that was disabled at start
            cmbDelimiter.Enabled = true;
            cmbQuoted.Enabled = true;
            grpFileImport.Enabled = true;
            grpFolderImport.Enabled = true;

            // Re-enable the button now that all files are done
            btnProcessFolder.Enabled = true;  // <-- replace if your control is named differently

            MessageBox.Show(this, "All files processed.", "Done", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        //public void CompleteProcess()
        //{
        //    if (_folderProcessing) return; // wait for all files

        //    cmbDelimiter.Enabled = true;
        //    cmbQuoted.Enabled = true;
        //    grpFileImport.Enabled = true;
        //    grpFolderImport.Enabled = true;
        //}

        //UI delegate
        public delegate void UIUpdate();

        public void StartUpdate()
        {
            lblTestLabel.Text = (ReportRow.ToString() + " - " + ReportField.ToString());
            this.lblTestLabel.Refresh();
        }

        public void CompleteProcess()
        {

            cmbDelimiter.Enabled = true;
            cmbQuoted.Enabled = true;
            grpFileImport.Enabled = true;
            grpFolderImport.Enabled = true;
        }

        private void btnFolder_Click(object sender, EventArgs e)
        {
            //Files
            var fileContent = string.Empty;
            var filePath = string.Empty;

            FolderBrowserDialog diag = new FolderBrowserDialog();

            if (diag.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                string folder = diag.SelectedPath;  //selected folder path
                txtFolder.Text = folder;
            }
        }

        //private void button1_Click(object sender, EventArgs e)
        //{            
        //    threadCountToUse = Process.GetCurrentProcess().Threads.Count + 1 + Convert.ToInt32(cmbThreadCount.SelectedItem.ToString());
        //    delimiters = DelimiterTypeCheck(cmbDelimiter.SelectedItem.ToString());
        //    isQuoted = Convert.ToBoolean(cmbQuoted.SelectedItem.ToString());
        //    fileType = cmbFileType.SelectedItem.ToString();
        //    cmbDelimiter.Enabled = false;
        //    cmbQuoted.Enabled = false;
        //    grpFileImport.Enabled = false;
        //    grpFolderImport.Enabled = false;
        //    Thread process_folder = new Thread(processFolder);
        //    process_folder.IsBackground = true;
        //    process_folder.Start();
        //    //processFile(txtFile.Text.ToString());
        //}

        private void button1_Click(object sender, EventArgs e)
        {
            // Disable until all files finish
            btnProcessFolder.Enabled = false;   // <-- replace with your actual button variable if different
            _folderProcessing = true;
            _activeFileWorkers = 0;

            threadCountToUse = Process.GetCurrentProcess().Threads.Count + 1 + Convert.ToInt32(cmbThreadCount.SelectedItem.ToString());
            delimiters = DelimiterTypeCheck(cmbDelimiter.SelectedItem.ToString());
            isQuoted = Convert.ToBoolean(cmbQuoted.SelectedItem.ToString());
            fileType = cmbFileType.SelectedItem.ToString();

            cmbDelimiter.Enabled = false;
            cmbQuoted.Enabled = false;
            grpFileImport.Enabled = false;
            grpFolderImport.Enabled = false;

            Thread process_folder = new Thread(processFolder) { IsBackground = true };
            process_folder.Start();
        }


        //private void processFolder()
        //{           
        //    if (!ValidateDirectory())
        //    {
        //        MessageBox.Show("Doesn't exists");
        //    }

        //    else
        //    {
        //        System.IO.DirectoryInfo fileRoot = new DirectoryInfo(System.IO.Path.Combine(txtFolder.Text));                
        //        System.IO.FileInfo[] fileList = fileRoot.GetFiles(fileType);

        //        foreach (var importFile in fileList)
        //        {
        //            fileProcessThreadCount = Process.GetCurrentProcess().Threads.Count;

        //            if (File.Exists(importFile.FullName))
        //            {
        //                string rowNumber = "1";
        //                ReportRow = rowNumber;
        //                ReportField = importFile.Name;
        //                Invoke(new UIUpdate(StartUpdate));

        //                while (fileProcessThreadCount >= (threadCountToUse))
        //                {
        //                    fileProcessThreadCount = Process.GetCurrentProcess().Threads.Count;
        //                    Thread.Sleep(15000);
        //                }

        //                Thread process_file = new Thread(() => processFile(importFile.FullName));
        //                process_file.IsBackground = true;
        //                process_file.Start();
        //            }
        //        }
        //    }
        //}

        private void processFolder()
        {
            if (!ValidateDirectory())
            {
                MessageBox.Show("Doesn't exists");
            }
            else
            {
                DirectoryInfo fileRoot = new DirectoryInfo(Path.Combine(txtFolder.Text));
                FileInfo[] fileList = fileRoot.GetFiles(fileType);

                foreach (var importFile in fileList)
                {
                    fileProcessThreadCount = Process.GetCurrentProcess().Threads.Count;

                    if (File.Exists(importFile.FullName))
                    {
                        ReportRow = "1";
                        ReportField = importFile.Name;
                        Invoke(new UIUpdate(StartUpdate));

                        while (fileProcessThreadCount >= (threadCountToUse))
                        {
                            fileProcessThreadCount = Process.GetCurrentProcess().Threads.Count;
                            Thread.Sleep(15000);
                        }

                        // *** increment before launching worker
                        System.Threading.Interlocked.Increment(ref _activeFileWorkers);

                        Thread process_file = new Thread(() => processFile(importFile.FullName))
                        {
                            IsBackground = true
                        };
                        process_file.Start();
                    }
                }
            }
        }

        private string DelimiterTypeCheck(string characterSelect)
        {
            string returnCharacter = ",";

            switch (characterSelect)
            {
                case "Comma (,)":
                    returnCharacter= ",";
                    break;
                case "Pipe (|)":
                    returnCharacter = "|";
                    break;
                case "Tab(\t)":
                    returnCharacter = "\t";
                    break;                    
                case "Fixed Width": //This code is not complete.
                    returnCharacter = ",";
                    break;
                case "Embedded File": //This code is not complete.
                    returnCharacter = ",";
                    break;
                default:
                    returnCharacter = ",";
                    break;
            }     
            
            return returnCharacter;
        }

        private bool ValidateDirectory()
        {
            try
            {
                if (!Directory.Exists(this.txtFolder.Text))
                    return false;

                return true;
            }

            catch (Exception ex)
            {
                return false;
            }
        }
    }
}
