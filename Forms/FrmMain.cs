//SELECT('SELECT * FROM .[' + TABLE_SCHEMA + '].[' + TABLE_NAME + ']')[SELECT], 
//('DROP TABLE .[' + TABLE_SCHEMA + '].[' + TABLE_NAME + ']')[DROP],
//*FROM INFORMATION_SCHEMA.TABLES

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
using Microsoft.VisualBasic.FileIO;
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
        SQLConnectionClass SQLConnect = new SQLConnectionClass("", "", "", "", false);

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
            txtInstance.Text = @"";
            txtDatabase.Text = @"";
            txtLogin.Text = @"";
            txtPassword.Text = @"";
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
            //Thread process_file = new Thread(processFile);
            Thread process_file = new Thread(() => processFile(txtFile.Text.ToString()));
            process_file.IsBackground = true;
            process_file.Start();
            //processFile(txtFile.Text.ToString());
        }

        //private void processFile(string fileToProcess)
        private void processFile(string fileInformation)
        {
            //string fileToProcess = txtFile.Text.ToString();
            string fileToProcess = fileInformation;
            System.IO.FileInfo fileData = new System.IO.FileInfo(fileToProcess);
            LogWriter errorLog = new LogWriter();
            //MessageBox.Show(fileData.Name);

            try
            {
                if (File.Exists(fileData.FullName))
                {
                    using (TextFieldParser parser = new TextFieldParser(fileData.FullName))
                    {
                        int row = 1;
                        int column = 1;
                        int columnCount = 1;
                        int insertCount = 1;
                        string createQueryBuilder = "";
                        string insertQueryBuilder = "";
                        string valueQueryBuilder = " VALUES (";

                        parser.TextFieldType = FieldType.Delimited;
                        parser.HasFieldsEnclosedInQuotes = true;
                        parser.SetDelimiters(",");

                        while(!parser.EndOfData)
                        {
                            string[] fields = parser.ReadFields();
                            foreach(string field in fields)
                            {
                                if (column == 1 && row == 1)
                                {
                                    ReportRow = row.ToString();
                                    ReportField = field.ToString();
                                    createQueryBuilder = "CREATE TABLE .dbo.[" + fileData.Name + "] (";
                                    insertQueryBuilder = "INSERT INTO .dbo.[" + fileData.Name + "] (";
                                    Invoke(new UIUpdate(StartUpdate));
                                }

                                if (row == 1)
                                {
                                    if (column == 1)
                                    {
                                        createQueryBuilder = createQueryBuilder + "[" + field.ToString() + "] VARCHAR(MAX)";
                                        insertQueryBuilder = insertQueryBuilder + "[" + field.ToString() + "]";
                                    }
                                    
                                    else
                                    {
                                        createQueryBuilder = createQueryBuilder + ", " + "[" + field.ToString() + "] VARCHAR(MAX)";
                                        insertQueryBuilder = insertQueryBuilder + ", " + "[" + field.ToString() + "]";
                                    }
                                    columnCount++;
                                }

                                if (row != 1)
                                {
                                    if (column == 1)
                                    {
                                        if (field.ToString().ToUpper() == "NULL")
                                        {
                                            valueQueryBuilder = valueQueryBuilder + field.ToString();
                                        }

                                        else
                                            valueQueryBuilder = valueQueryBuilder + "'" + field.ToString().Replace("'", "''") + "'";
                                    }

                                    else
                                    {
                                        if (field.ToString().ToUpper() == "NULL")
                                        {
                                            valueQueryBuilder = valueQueryBuilder + ", " + field.ToString();
                                        }

                                        else
                                        {
                                            valueQueryBuilder = valueQueryBuilder + ", " + "'" + field.ToString().Replace("'", "''") + "'";
                                        }
                                    }
                                    insertCount++;
                                }

                                //MessageBox.Show("ColumnTest " + field.ToString());
                                column++;
                            }

                            if (row == 1)
                            {
                                createQueryBuilder = createQueryBuilder + ")";
                                insertQueryBuilder = insertQueryBuilder + ")";

                                SQLGBSCustomProcess CreateTable = new SQLGBSCustomProcess();
                                CreateTable.SqlCommandProcess(createQueryBuilder, SQLConnect, fileData.DirectoryName.ToString());
                            }

                            if (row != 1)
                            {
                                valueQueryBuilder = valueQueryBuilder + ");";

                                if (columnCount == insertCount)
                                {
                                    SQLGBSCustomProcess InsertData = new SQLGBSCustomProcess();
                                    InsertData.SqlCommandProcess((insertQueryBuilder + " " + valueQueryBuilder), SQLConnect, fileData.DirectoryName.ToString());
                                }

                                else
                                {
                                    if (columnCount > insertCount)
                                    {
                                        //MessageBox.Show("Less items than expected: " + columnCount.ToString() + " > " + insertCount.ToString()); 
                                        errorLog.write_log(fileData.DirectoryName.ToString(), (fileData.FullName.ToString() + " - " + "Less items than expected: " + columnCount.ToString() + " > " + insertCount.ToString() + " - "
                                                + "Row => " + row.ToString()));
                                    }

                                    else if (columnCount < insertCount)
                                    {
                                        //MessageBox.Show("More items than expected: " + columnCount.ToString() + " < " + insertCount.ToString()); 
                                        errorLog.write_log(fileData.DirectoryName.ToString(), (fileData.FullName.ToString() + " - " + "More items than expected: " + columnCount.ToString() + " < " + insertCount.ToString() + " - "
                                                + "Row => " + row.ToString()));
                                    }

                                    else
                                    {
                                        //MessageBox.Show("Unknown: " + columnCount.ToString() + " < " + insertCount.ToString());
                                        errorLog.write_log(fileData.DirectoryName.ToString(), (fileData.FullName.ToString() + " - " + "Unknown: " + columnCount.ToString() + " < " + insertCount.ToString() + " - "
                                                + "Row => " + row.ToString()));
                                    }
                                }
                            }

                            //log.write_log(logDirectory, "Create note for encounter: " + enc_nbr.ToString() + "...." + DateTime.Now.ToString("hh:mm:ss"));
                            column = 1;
                            row++;
                            valueQueryBuilder = " VALUES (";
                            insertCount = 1;
                        }
                    }
                }
            }

            catch (Exception ex)
            {
                //MessageBox.Show(ex.ToString());
                errorLog.write_log(fileData.DirectoryName.ToString(), ex.ToString() + " - ");
            }

            finally
            {
                //fileProcessThreadCount = Process.GetCurrentProcess().Threads.Count;
                //fileData.MoveTo(@"C:\Users\brads\Desktop\Voters\Files\Backup\" + fileData.Name.ToString());
                //if (fileProcessThreadCount < (threadCountToUse))                
                //{
                //    MessageBox.Show("Done");
                //    grpFileImport.Enabled = true;
                //    grpFolderImport.Enabled = true;
                //}

                Invoke(new UIUpdate(CompleteProcess));
            }            
        }

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

        private void button1_Click(object sender, EventArgs e)
        {            
            threadCountToUse = Process.GetCurrentProcess().Threads.Count + 1 + Convert.ToInt32(cmbThreadCount.SelectedItem.ToString());
            fileType = cmbFileType.SelectedItem.ToString();
            cmbDelimiter.Enabled = false;
            cmbQuoted.Enabled = false;
            grpFileImport.Enabled = false;
            grpFolderImport.Enabled = false;
            Thread process_folder = new Thread(processFolder);
            process_folder.IsBackground = true;
            process_folder.Start();
            //processFile(txtFile.Text.ToString());
        }

        private void processFolder()
        {           
            if (!ValidateDirectory())
            {
                MessageBox.Show("Doesn't exists");
            }

            else
            {
                System.IO.DirectoryInfo fileRoot = new DirectoryInfo(System.IO.Path.Combine(txtFolder.Text));                
                System.IO.FileInfo[] fileList = fileRoot.GetFiles(fileType);

                foreach (var importFile in fileList)
                {
                    fileProcessThreadCount = Process.GetCurrentProcess().Threads.Count;

                    if (File.Exists(importFile.FullName))
                    {
                        string rowNumber = "1";
                        ReportRow = rowNumber;
                        ReportField = importFile.Name;
                        Invoke(new UIUpdate(StartUpdate));

                        while (fileProcessThreadCount >= (threadCountToUse))
                        {
                            fileProcessThreadCount = Process.GetCurrentProcess().Threads.Count;
                            Thread.Sleep(15000);
                        }

                        Thread process_file = new Thread(() => processFile(importFile.FullName));
                        process_file.IsBackground = true;
                        process_file.Start();
                    }
                }
            }
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
