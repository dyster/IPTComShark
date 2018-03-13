using IPTComShark.Controls;

namespace IPTComShark
{
    partial class MainForm
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
            IPTComShark.Controls.PacketListSettings packetListSettings1 = new IPTComShark.Controls.PacketListSettings();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
            this.timerFlicker = new System.Windows.Forms.Timer(this.components);
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.statusLeft = new System.Windows.Forms.ToolStripStatusLabel();
            this.statusRight = new System.Windows.Forms.ToolStripStatusLabel();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.label1 = new System.Windows.Forms.Label();
            this.textBoxIgnoreComid = new System.Windows.Forms.TextBox();
            this.checkBoxIgnoreLoopback = new System.Windows.Forms.CheckBox();
            this.checkBoxAutoScroll = new System.Windows.Forms.CheckBox();
            this.checkBoxHideDupes = new System.Windows.Forms.CheckBox();
            this.checkBoxParserOnly = new System.Windows.Forms.CheckBox();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openFilesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openFolderToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveAllToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveCurrentFilterToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.simulateTrafficToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.cleanupPCAPToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exportSVGSequenceDiagramToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.startToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.stopToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.restartToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.clearToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openCapturesDialog = new System.Windows.Forms.OpenFileDialog();
            this.exportXLSXToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exportCSVToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.packetListView1 = new IPTComShark.Controls.PacketListView();
            this.packetDisplay1 = new IPTComShark.Controls.PacketDisplay();
            this.statusStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // backgroundWorker1
            // 
            this.backgroundWorker1.WorkerSupportsCancellation = true;
            this.backgroundWorker1.DoWork += new System.ComponentModel.DoWorkEventHandler(this.backgroundWorker1_DoWork);
            this.backgroundWorker1.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.backgroundWorker1_RunWorkerCompleted);
            // 
            // timerFlicker
            // 
            this.timerFlicker.Enabled = true;
            this.timerFlicker.Interval = 500;
            this.timerFlicker.Tick += new System.EventHandler(this.timerFlicker_Tick);
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.statusLeft,
            this.statusRight});
            this.statusStrip1.Location = new System.Drawing.Point(0, 575);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.ShowItemToolTips = true;
            this.statusStrip1.Size = new System.Drawing.Size(1433, 22);
            this.statusStrip1.TabIndex = 4;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // statusLeft
            // 
            this.statusLeft.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.statusLeft.Name = "statusLeft";
            this.statusLeft.Size = new System.Drawing.Size(118, 17);
            this.statusLeft.Text = "toolStripStatusLabel1";
            // 
            // statusRight
            // 
            this.statusRight.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.statusRight.Name = "statusRight";
            this.statusRight.Size = new System.Drawing.Size(118, 17);
            this.statusRight.Text = "toolStripStatusLabel1";
            // 
            // splitContainer1
            // 
            this.splitContainer1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.splitContainer1.Location = new System.Drawing.Point(0, 27);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.packetListView1);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.label1);
            this.splitContainer1.Panel2.Controls.Add(this.textBoxIgnoreComid);
            this.splitContainer1.Panel2.Controls.Add(this.checkBoxIgnoreLoopback);
            this.splitContainer1.Panel2.Controls.Add(this.checkBoxAutoScroll);
            this.splitContainer1.Panel2.Controls.Add(this.packetDisplay1);
            this.splitContainer1.Panel2.Controls.Add(this.checkBoxHideDupes);
            this.splitContainer1.Panel2.Controls.Add(this.checkBoxParserOnly);
            this.splitContainer1.Size = new System.Drawing.Size(1433, 545);
            this.splitContainer1.SplitterDistance = 1030;
            this.splitContainer1.TabIndex = 7;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(0, 92);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(170, 13);
            this.label1.TabIndex = 13;
            this.label1.Text = "Ignore Comid (separate by comma)";
            // 
            // textBoxIgnoreComid
            // 
            this.textBoxIgnoreComid.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxIgnoreComid.Location = new System.Drawing.Point(3, 108);
            this.textBoxIgnoreComid.Name = "textBoxIgnoreComid";
            this.textBoxIgnoreComid.Size = new System.Drawing.Size(393, 20);
            this.textBoxIgnoreComid.TabIndex = 12;
            // 
            // checkBoxIgnoreLoopback
            // 
            this.checkBoxIgnoreLoopback.AutoSize = true;
            this.checkBoxIgnoreLoopback.Location = new System.Drawing.Point(3, 49);
            this.checkBoxIgnoreLoopback.Name = "checkBoxIgnoreLoopback";
            this.checkBoxIgnoreLoopback.Size = new System.Drawing.Size(107, 17);
            this.checkBoxIgnoreLoopback.TabIndex = 11;
            this.checkBoxIgnoreLoopback.Text = "Ignore Loopback";
            this.checkBoxIgnoreLoopback.UseVisualStyleBackColor = true;
            // 
            // checkBoxAutoScroll
            // 
            this.checkBoxAutoScroll.AutoSize = true;
            this.checkBoxAutoScroll.Location = new System.Drawing.Point(3, 26);
            this.checkBoxAutoScroll.Name = "checkBoxAutoScroll";
            this.checkBoxAutoScroll.Size = new System.Drawing.Size(74, 17);
            this.checkBoxAutoScroll.TabIndex = 10;
            this.checkBoxAutoScroll.Text = "AutoScroll";
            this.checkBoxAutoScroll.UseVisualStyleBackColor = true;
            // 
            // checkBoxHideDupes
            // 
            this.checkBoxHideDupes.AutoSize = true;
            this.checkBoxHideDupes.Checked = true;
            this.checkBoxHideDupes.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBoxHideDupes.Location = new System.Drawing.Point(3, 72);
            this.checkBoxHideDupes.Name = "checkBoxHideDupes";
            this.checkBoxHideDupes.Size = new System.Drawing.Size(174, 17);
            this.checkBoxHideDupes.TabIndex = 9;
            this.checkBoxHideDupes.Text = "Ignore Duplicated ProcessData";
            this.checkBoxHideDupes.UseVisualStyleBackColor = true;
            // 
            // checkBoxParserOnly
            // 
            this.checkBoxParserOnly.AutoSize = true;
            this.checkBoxParserOnly.Checked = true;
            this.checkBoxParserOnly.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBoxParserOnly.Location = new System.Drawing.Point(3, 3);
            this.checkBoxParserOnly.Name = "checkBoxParserOnly";
            this.checkBoxParserOnly.Size = new System.Drawing.Size(102, 17);
            this.checkBoxParserOnly.TabIndex = 8;
            this.checkBoxParserOnly.Text = "Show VSIS only";
            this.checkBoxParserOnly.UseVisualStyleBackColor = true;
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.toolsToolStripMenuItem,
            this.startToolStripMenuItem,
            this.stopToolStripMenuItem,
            this.restartToolStripMenuItem,
            this.clearToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(1433, 24);
            this.menuStrip1.TabIndex = 15;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.openFilesToolStripMenuItem,
            this.openFolderToolStripMenuItem,
            this.saveAllToolStripMenuItem,
            this.saveCurrentFilterToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            this.fileToolStripMenuItem.Text = "File";
            // 
            // openFilesToolStripMenuItem
            // 
            this.openFilesToolStripMenuItem.Name = "openFilesToolStripMenuItem";
            this.openFilesToolStripMenuItem.Size = new System.Drawing.Size(170, 22);
            this.openFilesToolStripMenuItem.Text = "Open Files";
            this.openFilesToolStripMenuItem.Click += new System.EventHandler(this.openFilesToolStripMenuItem_Click);
            // 
            // openFolderToolStripMenuItem
            // 
            this.openFolderToolStripMenuItem.Name = "openFolderToolStripMenuItem";
            this.openFolderToolStripMenuItem.Size = new System.Drawing.Size(170, 22);
            this.openFolderToolStripMenuItem.Text = "Open Folder";
            this.openFolderToolStripMenuItem.Click += new System.EventHandler(this.openFolderToolStripMenuItem_Click);
            // 
            // saveAllToolStripMenuItem
            // 
            this.saveAllToolStripMenuItem.Name = "saveAllToolStripMenuItem";
            this.saveAllToolStripMenuItem.Size = new System.Drawing.Size(170, 22);
            this.saveAllToolStripMenuItem.Text = "Save All";
            this.saveAllToolStripMenuItem.Click += new System.EventHandler(this.buttonSaveAll_Click);
            // 
            // saveCurrentFilterToolStripMenuItem
            // 
            this.saveCurrentFilterToolStripMenuItem.Name = "saveCurrentFilterToolStripMenuItem";
            this.saveCurrentFilterToolStripMenuItem.Size = new System.Drawing.Size(170, 22);
            this.saveCurrentFilterToolStripMenuItem.Text = "Save Current Filter";
            this.saveCurrentFilterToolStripMenuItem.Click += new System.EventHandler(this.saveCurrentFilterToolStripMenuItem_Click);
            // 
            // toolsToolStripMenuItem
            // 
            this.toolsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.simulateTrafficToolStripMenuItem,
            this.cleanupPCAPToolStripMenuItem,
            this.exportSVGSequenceDiagramToolStripMenuItem,
            this.exportXLSXToolStripMenuItem,
            this.exportCSVToolStripMenuItem});
            this.toolsToolStripMenuItem.Name = "toolsToolStripMenuItem";
            this.toolsToolStripMenuItem.Size = new System.Drawing.Size(47, 20);
            this.toolsToolStripMenuItem.Text = "Tools";
            // 
            // simulateTrafficToolStripMenuItem
            // 
            this.simulateTrafficToolStripMenuItem.Name = "simulateTrafficToolStripMenuItem";
            this.simulateTrafficToolStripMenuItem.Size = new System.Drawing.Size(231, 22);
            this.simulateTrafficToolStripMenuItem.Text = "Simulate Traffic";
            this.simulateTrafficToolStripMenuItem.Click += new System.EventHandler(this.simulateTrafficToolStripMenuItem_Click);
            // 
            // cleanupPCAPToolStripMenuItem
            // 
            this.cleanupPCAPToolStripMenuItem.Name = "cleanupPCAPToolStripMenuItem";
            this.cleanupPCAPToolStripMenuItem.Size = new System.Drawing.Size(231, 22);
            this.cleanupPCAPToolStripMenuItem.Text = "Cleanup PCAP";
            this.cleanupPCAPToolStripMenuItem.Click += new System.EventHandler(this.buttonCleanPcap_Click);
            // 
            // exportSVGSequenceDiagramToolStripMenuItem
            // 
            this.exportSVGSequenceDiagramToolStripMenuItem.Name = "exportSVGSequenceDiagramToolStripMenuItem";
            this.exportSVGSequenceDiagramToolStripMenuItem.Size = new System.Drawing.Size(231, 22);
            this.exportSVGSequenceDiagramToolStripMenuItem.Text = "Export SVG sequence diagram";
            this.exportSVGSequenceDiagramToolStripMenuItem.Click += new System.EventHandler(this.exportSVGSequenceDiagramToolStripMenuItem_Click);
            // 
            // startToolStripMenuItem
            // 
            this.startToolStripMenuItem.Name = "startToolStripMenuItem";
            this.startToolStripMenuItem.Size = new System.Drawing.Size(43, 20);
            this.startToolStripMenuItem.Text = "Start";
            this.startToolStripMenuItem.Click += new System.EventHandler(this.buttonStart_Click);
            // 
            // stopToolStripMenuItem
            // 
            this.stopToolStripMenuItem.Name = "stopToolStripMenuItem";
            this.stopToolStripMenuItem.Size = new System.Drawing.Size(43, 20);
            this.stopToolStripMenuItem.Text = "Stop";
            this.stopToolStripMenuItem.Click += new System.EventHandler(this.buttonStop_Click);
            // 
            // restartToolStripMenuItem
            // 
            this.restartToolStripMenuItem.Name = "restartToolStripMenuItem";
            this.restartToolStripMenuItem.Size = new System.Drawing.Size(55, 20);
            this.restartToolStripMenuItem.Text = "Restart";
            this.restartToolStripMenuItem.Click += new System.EventHandler(this.buttonRestart_Click);
            // 
            // clearToolStripMenuItem
            // 
            this.clearToolStripMenuItem.Name = "clearToolStripMenuItem";
            this.clearToolStripMenuItem.Size = new System.Drawing.Size(46, 20);
            this.clearToolStripMenuItem.Text = "Clear";
            this.clearToolStripMenuItem.Click += new System.EventHandler(this.clearToolStripMenuItem_Click);
            // 
            // openCapturesDialog
            // 
            this.openCapturesDialog.FileName = "openFileDialog1";
            this.openCapturesDialog.Multiselect = true;
            // 
            // exportXLSXToolStripMenuItem
            // 
            this.exportXLSXToolStripMenuItem.Name = "exportXLSXToolStripMenuItem";
            this.exportXLSXToolStripMenuItem.Size = new System.Drawing.Size(231, 22);
            this.exportXLSXToolStripMenuItem.Text = "Export XLSX";
            this.exportXLSXToolStripMenuItem.Click += new System.EventHandler(this.exportXLSXToolStripMenuItem_Click);
            // 
            // exportCSVToolStripMenuItem
            // 
            this.exportCSVToolStripMenuItem.Name = "exportCSVToolStripMenuItem";
            this.exportCSVToolStripMenuItem.Size = new System.Drawing.Size(231, 22);
            this.exportCSVToolStripMenuItem.Text = "Export CSV";
            // 
            // packetListView1
            // 
            this.packetListView1.AutoScroll = true;
            this.packetListView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.packetListView1.Location = new System.Drawing.Point(0, 0);
            this.packetListView1.Name = "packetListView1";
            packetListSettings1.AutoScroll = true;
            packetListSettings1.IgnoreComid = null;
            packetListSettings1.IgnoreDuplicatedPD = true;
            packetListSettings1.IgnoreLoopback = true;
            packetListSettings1.IgnoreUnknownData = true;
            this.packetListView1.Settings = packetListSettings1;
            this.packetListView1.Size = new System.Drawing.Size(1030, 545);
            this.packetListView1.TabIndex = 0;
            // 
            // packetDisplay1
            // 
            this.packetDisplay1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.packetDisplay1.IptConfigReader = null;
            this.packetDisplay1.Location = new System.Drawing.Point(0, 134);
            this.packetDisplay1.Name = "packetDisplay1";
            this.packetDisplay1.Size = new System.Drawing.Size(396, 411);
            this.packetDisplay1.TabIndex = 0;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1433, 597);
            this.Controls.Add(this.splitContainer1);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.menuStrip1);
            this.DoubleBuffered = true;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "MainForm";
            this.Text = "IPTComShark";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.ComponentModel.BackgroundWorker backgroundWorker1;
        private System.Windows.Forms.Timer timerFlicker;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel statusLeft;
        private System.Windows.Forms.ToolStripStatusLabel statusRight;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.CheckBox checkBoxParserOnly;
        private System.Windows.Forms.CheckBox checkBoxHideDupes;
        private System.Windows.Forms.CheckBox checkBoxAutoScroll;
        private PacketDisplay packetDisplay1;
        private PacketListView packetListView1;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem openFilesToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem openFolderToolStripMenuItem;
        private System.Windows.Forms.OpenFileDialog openCapturesDialog;
        private System.Windows.Forms.ToolStripMenuItem saveAllToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem saveCurrentFilterToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem toolsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem simulateTrafficToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem cleanupPCAPToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem startToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem stopToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem restartToolStripMenuItem;
        private System.Windows.Forms.CheckBox checkBoxIgnoreLoopback;
        private System.Windows.Forms.ToolStripMenuItem clearToolStripMenuItem;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox textBoxIgnoreComid;
        private System.Windows.Forms.ToolStripMenuItem exportSVGSequenceDiagramToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exportXLSXToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exportCSVToolStripMenuItem;
    }
}

