namespace DuplicateFinder
{
    partial class Form1
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.folderBrowserDialog1 = new System.Windows.Forms.FolderBrowserDialog();
            this.btnRootDir = new System.Windows.Forms.Button();
            this.txtRootDir = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.btnSearch = new System.Windows.Forms.Button();
            this.btnStopSearch = new System.Windows.Forms.Button();
            this.treeResults = new System.Windows.Forms.TreeView();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.chkBxRecycle = new System.Windows.Forms.CheckBox();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.helpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripCurrentStatus = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripSpacer = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripLabelNumEntries = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripRunTime = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripNumberOfFiles = new System.Windows.Forms.ToolStripStatusLabel();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.axWindowsMediaPlayer1 = new AxWMPLib.AxWindowsMediaPlayer();
            this.webBrowser1 = new System.Windows.Forms.WebBrowser();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.menuStrip1.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.axWindowsMediaPlayer1)).BeginInit();
            this.SuspendLayout();
            // 
            // folderBrowserDialog1
            // 
            this.folderBrowserDialog1.RootFolder = System.Environment.SpecialFolder.MyComputer;
            this.folderBrowserDialog1.ShowNewFolderButton = false;
            // 
            // btnRootDir
            // 
            this.btnRootDir.Location = new System.Drawing.Point(310, 30);
            this.btnRootDir.Name = "btnRootDir";
            this.btnRootDir.Size = new System.Drawing.Size(75, 23);
            this.btnRootDir.TabIndex = 1;
            this.btnRootDir.Text = "Browse...";
            this.btnRootDir.UseVisualStyleBackColor = true;
            this.btnRootDir.Click += new System.EventHandler(this.btnRootDir_Click);
            // 
            // txtRootDir
            // 
            this.txtRootDir.Enabled = false;
            this.txtRootDir.Location = new System.Drawing.Point(93, 32);
            this.txtRootDir.Name = "txtRootDir";
            this.txtRootDir.Size = new System.Drawing.Size(211, 20);
            this.txtRootDir.TabIndex = 2;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 35);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(78, 13);
            this.label1.TabIndex = 3;
            this.label1.Text = "Root Directory:";
            // 
            // btnSearch
            // 
            this.btnSearch.Location = new System.Drawing.Point(391, 30);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(100, 23);
            this.btnSearch.TabIndex = 4;
            this.btnSearch.Text = "Search";
            this.btnSearch.UseVisualStyleBackColor = true;
            this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);
            // 
            // btnStopSearch
            // 
            this.btnStopSearch.Enabled = false;
            this.btnStopSearch.Location = new System.Drawing.Point(497, 30);
            this.btnStopSearch.Name = "btnStopSearch";
            this.btnStopSearch.Size = new System.Drawing.Size(105, 23);
            this.btnStopSearch.TabIndex = 7;
            this.btnStopSearch.Text = "Stop";
            this.btnStopSearch.UseVisualStyleBackColor = true;
            this.btnStopSearch.Click += new System.EventHandler(this.btnStopSearch_Click);
            // 
            // treeResults
            // 
            this.treeResults.Location = new System.Drawing.Point(15, 59);
            this.treeResults.Name = "treeResults";
            this.treeResults.Size = new System.Drawing.Size(454, 374);
            this.treeResults.TabIndex = 9;
            this.treeResults.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.treeResults_AfterSelect);
            this.treeResults.NodeMouseDoubleClick += new System.Windows.Forms.TreeNodeMouseClickEventHandler(this.treeResults_NodeMouseDoubleClick);
            this.treeResults.KeyDown += new System.Windows.Forms.KeyEventHandler(this.treeResults_KeyDown);
            // 
            // pictureBox1
            // 
            this.pictureBox1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pictureBox1.Location = new System.Drawing.Point(6, 19);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(421, 349);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox1.TabIndex = 11;
            this.pictureBox1.TabStop = false;
            // 
            // chkBxRecycle
            // 
            this.chkBxRecycle.AutoSize = true;
            this.chkBxRecycle.Checked = true;
            this.chkBxRecycle.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkBxRecycle.Location = new System.Drawing.Point(609, 34);
            this.chkBxRecycle.Name = "chkBxRecycle";
            this.chkBxRecycle.Size = new System.Drawing.Size(83, 17);
            this.chkBxRecycle.TabIndex = 12;
            this.chkBxRecycle.Text = "Recycle Bin";
            this.chkBxRecycle.UseVisualStyleBackColor = true;
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItem1,
            this.helpToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(917, 24);
            this.menuStrip1.TabIndex = 13;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.exitToolStripMenuItem});
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(37, 20);
            this.toolStripMenuItem1.Text = "File";
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(93, 22);
            this.exitToolStripMenuItem.Text = "Exit";
            this.exitToolStripMenuItem.Click += new System.EventHandler(this.exitToolStripMenuItem_Click);
            // 
            // helpToolStripMenuItem
            // 
            this.helpToolStripMenuItem.Name = "helpToolStripMenuItem";
            this.helpToolStripMenuItem.Size = new System.Drawing.Size(44, 20);
            this.helpToolStripMenuItem.Text = "Help";
            this.helpToolStripMenuItem.Click += new System.EventHandler(this.helpToolStripMenuItem_Click);
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabel1,
            this.toolStripCurrentStatus,
            this.toolStripSpacer,
            this.toolStripLabelNumEntries,
            this.toolStripRunTime,
            this.toolStripNumberOfFiles});
            this.statusStrip1.Location = new System.Drawing.Point(0, 436);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(917, 22);
            this.statusStrip1.SizingGrip = false;
            this.statusStrip1.Stretch = false;
            this.statusStrip1.TabIndex = 14;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // toolStripStatusLabel1
            // 
            this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            this.toolStripStatusLabel1.Size = new System.Drawing.Size(42, 17);
            this.toolStripStatusLabel1.Text = "Status:";
            // 
            // toolStripCurrentStatus
            // 
            this.toolStripCurrentStatus.Name = "toolStripCurrentStatus";
            this.toolStripCurrentStatus.Size = new System.Drawing.Size(26, 17);
            this.toolStripCurrentStatus.Text = "Idle";
            // 
            // toolStripSpacer
            // 
            this.toolStripSpacer.Name = "toolStripSpacer";
            this.toolStripSpacer.Size = new System.Drawing.Size(834, 17);
            this.toolStripSpacer.Spring = true;
            this.toolStripSpacer.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // toolStripLabelNumEntries
            // 
            this.toolStripLabelNumEntries.Name = "toolStripLabelNumEntries";
            this.toolStripLabelNumEntries.Size = new System.Drawing.Size(0, 17);
            // 
            // toolStripRunTime
            // 
            this.toolStripRunTime.Name = "toolStripRunTime";
            this.toolStripRunTime.Size = new System.Drawing.Size(0, 17);
            // 
            // toolStripNumberOfFiles
            // 
            this.toolStripNumberOfFiles.Name = "toolStripNumberOfFiles";
            this.toolStripNumberOfFiles.Size = new System.Drawing.Size(0, 17);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.axWindowsMediaPlayer1);
            this.groupBox1.Controls.Add(this.webBrowser1);
            this.groupBox1.Controls.Add(this.pictureBox1);
            this.groupBox1.Location = new System.Drawing.Point(476, 59);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(434, 374);
            this.groupBox1.TabIndex = 15;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Preview";
            // 
            // axWindowsMediaPlayer1
            // 
            this.axWindowsMediaPlayer1.Enabled = true;
            this.axWindowsMediaPlayer1.Location = new System.Drawing.Point(6, 19);
            this.axWindowsMediaPlayer1.Name = "axWindowsMediaPlayer1";
            this.axWindowsMediaPlayer1.OcxState = ((System.Windows.Forms.AxHost.State)(resources.GetObject("axWindowsMediaPlayer1.OcxState")));
            this.axWindowsMediaPlayer1.Size = new System.Drawing.Size(421, 349);
            this.axWindowsMediaPlayer1.TabIndex = 12;
            // 
            // webBrowser1
            // 
            this.webBrowser1.Location = new System.Drawing.Point(6, 19);
            this.webBrowser1.MaximumSize = new System.Drawing.Size(421, 349);
            this.webBrowser1.MinimumSize = new System.Drawing.Size(20, 20);
            this.webBrowser1.Name = "webBrowser1";
            this.webBrowser1.Size = new System.Drawing.Size(421, 349);
            this.webBrowser1.TabIndex = 16;
            // 
            // timer1
            // 
            this.timer1.Interval = 1000;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(917, 458);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.chkBxRecycle);
            this.Controls.Add(this.treeResults);
            this.Controls.Add(this.btnStopSearch);
            this.Controls.Add(this.btnSearch);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtRootDir);
            this.Controls.Add(this.btnRootDir);
            this.Controls.Add(this.menuStrip1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menuStrip1;
            this.MaximizeBox = false;
            this.Name = "Form1";
            this.Text = "Duplicate File Finder";
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.axWindowsMediaPlayer1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog1;
        private System.Windows.Forms.Button btnRootDir;
        private System.Windows.Forms.TextBox txtRootDir;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnSearch;
        private System.Windows.Forms.Button btnStopSearch;
        private System.Windows.Forms.TreeView treeResults;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.CheckBox chkBxRecycle;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel1;
        private System.Windows.Forms.ToolStripStatusLabel toolStripCurrentStatus;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem helpToolStripMenuItem;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.ToolStripStatusLabel toolStripSpacer;
        private System.Windows.Forms.ToolStripStatusLabel toolStripLabelNumEntries;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.ToolStripStatusLabel toolStripRunTime;
        private System.Windows.Forms.ToolStripStatusLabel toolStripNumberOfFiles;
        private AxWMPLib.AxWindowsMediaPlayer axWindowsMediaPlayer1;
        private System.Windows.Forms.WebBrowser webBrowser1;
    }
}

