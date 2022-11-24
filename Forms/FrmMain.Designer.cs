namespace FileReadToSQLDB
{
    partial class FrmMain
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmMain));
            this.txtFile = new System.Windows.Forms.TextBox();
            this.btnBrowse = new System.Windows.Forms.Button();
            this.grpFileImport = new System.Windows.Forms.GroupBox();
            this.btnProcess = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.grpDatabaseLogin = new System.Windows.Forms.GroupBox();
            this.ckbxSQL_DirectIP = new System.Windows.Forms.CheckBox();
            this.txtPassword = new System.Windows.Forms.TextBox();
            this.txtLogin = new System.Windows.Forms.TextBox();
            this.txtDatabase = new System.Windows.Forms.TextBox();
            this.txtInstance = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.btnConnect = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.grpBoxExportType = new System.Windows.Forms.GroupBox();
            this.cmbFileType = new System.Windows.Forms.ComboBox();
            this.label10 = new System.Windows.Forms.Label();
            this.cmbThreadCount = new System.Windows.Forms.ComboBox();
            this.label9 = new System.Windows.Forms.Label();
            this.cmbQuoted = new System.Windows.Forms.ComboBox();
            this.cmbDelimiter = new System.Windows.Forms.ComboBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.lblTestLabel = new System.Windows.Forms.Label();
            this.grpFolderImport = new System.Windows.Forms.GroupBox();
            this.btnProcessFolder = new System.Windows.Forms.Button();
            this.label8 = new System.Windows.Forms.Label();
            this.btnFolder = new System.Windows.Forms.Button();
            this.txtFolder = new System.Windows.Forms.TextBox();
            this.grpFileImport.SuspendLayout();
            this.grpDatabaseLogin.SuspendLayout();
            this.grpBoxExportType.SuspendLayout();
            this.grpFolderImport.SuspendLayout();
            this.SuspendLayout();
            // 
            // txtFile
            // 
            this.txtFile.Location = new System.Drawing.Point(51, 27);
            this.txtFile.Name = "txtFile";
            this.txtFile.Size = new System.Drawing.Size(519, 20);
            this.txtFile.TabIndex = 0;
            // 
            // btnBrowse
            // 
            this.btnBrowse.Location = new System.Drawing.Point(576, 25);
            this.btnBrowse.Name = "btnBrowse";
            this.btnBrowse.Size = new System.Drawing.Size(75, 23);
            this.btnBrowse.TabIndex = 1;
            this.btnBrowse.Text = "Browse";
            this.btnBrowse.UseVisualStyleBackColor = true;
            this.btnBrowse.Click += new System.EventHandler(this.btnBrowse_Click);
            // 
            // grpFileImport
            // 
            this.grpFileImport.Controls.Add(this.btnProcess);
            this.grpFileImport.Controls.Add(this.label1);
            this.grpFileImport.Controls.Add(this.btnBrowse);
            this.grpFileImport.Controls.Add(this.txtFile);
            this.grpFileImport.Enabled = false;
            this.grpFileImport.Location = new System.Drawing.Point(21, 170);
            this.grpFileImport.Name = "grpFileImport";
            this.grpFileImport.Size = new System.Drawing.Size(665, 101);
            this.grpFileImport.TabIndex = 2;
            this.grpFileImport.TabStop = false;
            this.grpFileImport.Text = "Import File";
            // 
            // btnProcess
            // 
            this.btnProcess.Location = new System.Drawing.Point(51, 53);
            this.btnProcess.Name = "btnProcess";
            this.btnProcess.Size = new System.Drawing.Size(86, 23);
            this.btnProcess.TabIndex = 3;
            this.btnProcess.Text = "Process File";
            this.btnProcess.UseVisualStyleBackColor = true;
            this.btnProcess.Click += new System.EventHandler(this.btnProcess_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(19, 30);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(26, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "File:";
            // 
            // grpDatabaseLogin
            // 
            this.grpDatabaseLogin.Controls.Add(this.ckbxSQL_DirectIP);
            this.grpDatabaseLogin.Controls.Add(this.txtPassword);
            this.grpDatabaseLogin.Controls.Add(this.txtLogin);
            this.grpDatabaseLogin.Controls.Add(this.txtDatabase);
            this.grpDatabaseLogin.Controls.Add(this.txtInstance);
            this.grpDatabaseLogin.Controls.Add(this.label5);
            this.grpDatabaseLogin.Controls.Add(this.btnConnect);
            this.grpDatabaseLogin.Controls.Add(this.label4);
            this.grpDatabaseLogin.Controls.Add(this.label3);
            this.grpDatabaseLogin.Controls.Add(this.label2);
            this.grpDatabaseLogin.Location = new System.Drawing.Point(21, 12);
            this.grpDatabaseLogin.Name = "grpDatabaseLogin";
            this.grpDatabaseLogin.Size = new System.Drawing.Size(379, 152);
            this.grpDatabaseLogin.TabIndex = 26;
            this.grpDatabaseLogin.TabStop = false;
            this.grpDatabaseLogin.Text = "SQL Instance One Login Information";
            // 
            // ckbxSQL_DirectIP
            // 
            this.ckbxSQL_DirectIP.AutoSize = true;
            this.ckbxSQL_DirectIP.Location = new System.Drawing.Point(61, 124);
            this.ckbxSQL_DirectIP.Name = "ckbxSQL_DirectIP";
            this.ckbxSQL_DirectIP.Size = new System.Drawing.Size(132, 17);
            this.ckbxSQL_DirectIP.TabIndex = 22;
            this.ckbxSQL_DirectIP.Text = "Use Direct IP/Address";
            this.ckbxSQL_DirectIP.UseVisualStyleBackColor = true;
            // 
            // txtPassword
            // 
            this.txtPassword.Location = new System.Drawing.Point(61, 95);
            this.txtPassword.Name = "txtPassword";
            this.txtPassword.PasswordChar = '*';
            this.txtPassword.Size = new System.Drawing.Size(312, 20);
            this.txtPassword.TabIndex = 7;
            // 
            // txtLogin
            // 
            this.txtLogin.Location = new System.Drawing.Point(61, 69);
            this.txtLogin.Name = "txtLogin";
            this.txtLogin.Size = new System.Drawing.Size(312, 20);
            this.txtLogin.TabIndex = 6;
            // 
            // txtDatabase
            // 
            this.txtDatabase.Location = new System.Drawing.Point(61, 43);
            this.txtDatabase.Name = "txtDatabase";
            this.txtDatabase.Size = new System.Drawing.Size(312, 20);
            this.txtDatabase.TabIndex = 5;
            // 
            // txtInstance
            // 
            this.txtInstance.Location = new System.Drawing.Point(61, 17);
            this.txtInstance.Name = "txtInstance";
            this.txtInstance.Size = new System.Drawing.Size(312, 20);
            this.txtInstance.TabIndex = 4;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(2, 98);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(53, 13);
            this.label5.TabIndex = 3;
            this.label5.Text = "Password";
            // 
            // btnConnect
            // 
            this.btnConnect.Location = new System.Drawing.Point(298, 120);
            this.btnConnect.Name = "btnConnect";
            this.btnConnect.Size = new System.Drawing.Size(75, 21);
            this.btnConnect.TabIndex = 7;
            this.btnConnect.Text = "Connect";
            this.btnConnect.UseVisualStyleBackColor = true;
            this.btnConnect.Click += new System.EventHandler(this.btnConnect_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(22, 72);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(33, 13);
            this.label4.TabIndex = 2;
            this.label4.Text = "Login";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(6, 46);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(53, 13);
            this.label3.TabIndex = 1;
            this.label3.Text = "Database";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(7, 20);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(48, 13);
            this.label2.TabIndex = 0;
            this.label2.Text = "Instance";
            // 
            // grpBoxExportType
            // 
            this.grpBoxExportType.Controls.Add(this.cmbFileType);
            this.grpBoxExportType.Controls.Add(this.label10);
            this.grpBoxExportType.Controls.Add(this.cmbThreadCount);
            this.grpBoxExportType.Controls.Add(this.label9);
            this.grpBoxExportType.Controls.Add(this.cmbQuoted);
            this.grpBoxExportType.Controls.Add(this.cmbDelimiter);
            this.grpBoxExportType.Controls.Add(this.label6);
            this.grpBoxExportType.Controls.Add(this.label7);
            this.grpBoxExportType.Enabled = false;
            this.grpBoxExportType.Location = new System.Drawing.Point(406, 12);
            this.grpBoxExportType.Name = "grpBoxExportType";
            this.grpBoxExportType.Size = new System.Drawing.Size(280, 136);
            this.grpBoxExportType.TabIndex = 27;
            this.grpBoxExportType.TabStop = false;
            this.grpBoxExportType.Text = "Export Type";
            // 
            // cmbFileType
            // 
            this.cmbFileType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbFileType.FormattingEnabled = true;
            this.cmbFileType.Items.AddRange(new object[] {
            "*.csv",
            "*.txt"});
            this.cmbFileType.Location = new System.Drawing.Point(120, 97);
            this.cmbFileType.Name = "cmbFileType";
            this.cmbFileType.Size = new System.Drawing.Size(146, 21);
            this.cmbFileType.TabIndex = 11;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(72, 100);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(31, 13);
            this.label10.TabIndex = 10;
            this.label10.Text = "Type";
            // 
            // cmbThreadCount
            // 
            this.cmbThreadCount.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbThreadCount.FormattingEnabled = true;
            this.cmbThreadCount.Items.AddRange(new object[] {
            "1",
            "2",
            "3",
            "4",
            "5",
            "6",
            "7",
            "8",
            "9",
            "10",
            "11",
            "12",
            "13",
            "14",
            "15",
            "16",
            "17",
            "18",
            "19",
            "20",
            "21",
            "22",
            "23",
            "24",
            "25",
            "26",
            "27",
            "28",
            "29",
            "30"});
            this.cmbThreadCount.Location = new System.Drawing.Point(120, 70);
            this.cmbThreadCount.Name = "cmbThreadCount";
            this.cmbThreadCount.Size = new System.Drawing.Size(146, 21);
            this.cmbThreadCount.TabIndex = 9;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(72, 73);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(46, 13);
            this.label9.TabIndex = 8;
            this.label9.Text = "Threads";
            // 
            // cmbQuoted
            // 
            this.cmbQuoted.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbQuoted.FormattingEnabled = true;
            this.cmbQuoted.Items.AddRange(new object[] {
            "True",
            "False"});
            this.cmbQuoted.Location = new System.Drawing.Point(120, 43);
            this.cmbQuoted.Name = "cmbQuoted";
            this.cmbQuoted.Size = new System.Drawing.Size(146, 21);
            this.cmbQuoted.TabIndex = 7;
            // 
            // cmbDelimiter
            // 
            this.cmbDelimiter.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbDelimiter.FormattingEnabled = true;
            this.cmbDelimiter.Items.AddRange(new object[] {
            "Comma (,)",
            "Pipe (|)",
            "Tab (\\t)",
            "Fixed Width (Currently Incomplete)",
            "Embedded File (Currently Incomplete)"});
            this.cmbDelimiter.Location = new System.Drawing.Point(120, 17);
            this.cmbDelimiter.Name = "cmbDelimiter";
            this.cmbDelimiter.Size = new System.Drawing.Size(146, 21);
            this.cmbDelimiter.TabIndex = 6;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(72, 46);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(42, 13);
            this.label6.TabIndex = 4;
            this.label6.Text = "Quoted";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(17, 20);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(97, 13);
            this.label7.TabIndex = 3;
            this.label7.Text = "Delimted Character";
            // 
            // lblTestLabel
            // 
            this.lblTestLabel.AutoSize = true;
            this.lblTestLabel.Location = new System.Drawing.Point(403, 154);
            this.lblTestLabel.Name = "lblTestLabel";
            this.lblTestLabel.Size = new System.Drawing.Size(115, 13);
            this.lblTestLabel.TabIndex = 28;
            this.lblTestLabel.Text = "Item Processing Status";
            // 
            // grpFolderImport
            // 
            this.grpFolderImport.Controls.Add(this.btnProcessFolder);
            this.grpFolderImport.Controls.Add(this.label8);
            this.grpFolderImport.Controls.Add(this.btnFolder);
            this.grpFolderImport.Controls.Add(this.txtFolder);
            this.grpFolderImport.Enabled = false;
            this.grpFolderImport.Location = new System.Drawing.Point(21, 277);
            this.grpFolderImport.Name = "grpFolderImport";
            this.grpFolderImport.Size = new System.Drawing.Size(665, 97);
            this.grpFolderImport.TabIndex = 29;
            this.grpFolderImport.TabStop = false;
            this.grpFolderImport.Text = "Import Folder";
            // 
            // btnProcessFolder
            // 
            this.btnProcessFolder.Location = new System.Drawing.Point(51, 53);
            this.btnProcessFolder.Name = "btnProcessFolder";
            this.btnProcessFolder.Size = new System.Drawing.Size(86, 23);
            this.btnProcessFolder.TabIndex = 3;
            this.btnProcessFolder.Text = "Process Folder";
            this.btnProcessFolder.UseVisualStyleBackColor = true;
            this.btnProcessFolder.Click += new System.EventHandler(this.button1_Click);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(6, 30);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(39, 13);
            this.label8.TabIndex = 2;
            this.label8.Text = "Folder:";
            // 
            // btnFolder
            // 
            this.btnFolder.Location = new System.Drawing.Point(576, 25);
            this.btnFolder.Name = "btnFolder";
            this.btnFolder.Size = new System.Drawing.Size(75, 23);
            this.btnFolder.TabIndex = 1;
            this.btnFolder.Text = "Browse";
            this.btnFolder.UseVisualStyleBackColor = true;
            this.btnFolder.Click += new System.EventHandler(this.btnFolder_Click);
            // 
            // txtFolder
            // 
            this.txtFolder.Location = new System.Drawing.Point(51, 27);
            this.txtFolder.Name = "txtFolder";
            this.txtFolder.Size = new System.Drawing.Size(519, 20);
            this.txtFolder.TabIndex = 0;
            // 
            // FrmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(749, 394);
            this.Controls.Add(this.grpFolderImport);
            this.Controls.Add(this.lblTestLabel);
            this.Controls.Add(this.grpBoxExportType);
            this.Controls.Add(this.grpDatabaseLogin);
            this.Controls.Add(this.grpFileImport);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MinimizeBox = false;
            this.Name = "FrmMain";
            this.Text = "CSV Reader to SQL Writer";
            this.grpFileImport.ResumeLayout(false);
            this.grpFileImport.PerformLayout();
            this.grpDatabaseLogin.ResumeLayout(false);
            this.grpDatabaseLogin.PerformLayout();
            this.grpBoxExportType.ResumeLayout(false);
            this.grpBoxExportType.PerformLayout();
            this.grpFolderImport.ResumeLayout(false);
            this.grpFolderImport.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtFile;
        private System.Windows.Forms.Button btnBrowse;
        private System.Windows.Forms.GroupBox grpFileImport;
        private System.Windows.Forms.Button btnProcess;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox grpDatabaseLogin;
        private System.Windows.Forms.CheckBox ckbxSQL_DirectIP;
        private System.Windows.Forms.TextBox txtPassword;
        private System.Windows.Forms.TextBox txtLogin;
        private System.Windows.Forms.TextBox txtDatabase;
        private System.Windows.Forms.TextBox txtInstance;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button btnConnect;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.GroupBox grpBoxExportType;
        private System.Windows.Forms.ComboBox cmbQuoted;
        private System.Windows.Forms.ComboBox cmbDelimiter;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label lblTestLabel;
        private System.Windows.Forms.GroupBox grpFolderImport;
        private System.Windows.Forms.Button btnProcessFolder;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Button btnFolder;
        private System.Windows.Forms.TextBox txtFolder;
        private System.Windows.Forms.ComboBox cmbThreadCount;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.ComboBox cmbFileType;
        private System.Windows.Forms.Label label10;
    }
}

