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
            this.packetListView1 = new IPTComShark.Controls.PacketListView();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.packetDisplay1 = new IPTComShark.Controls.PacketDisplay();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.textBoxSearch = new IPTComShark.Controls.TypeDelayTextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.checkBoxParserOnly = new System.Windows.Forms.CheckBox();
            this.checkBoxAutoScroll = new System.Windows.Forms.CheckBox();
            this.checkBoxIgnoreLoopback = new System.Windows.Forms.CheckBox();
            this.checkBoxHideDupes = new System.Windows.Forms.CheckBox();
            this.textBoxIgnoreComid = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.textBoxIgnoreVars = new IPTComShark.Controls.TypeDelayTextBox();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openFilesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openFolderToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveAllToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveCurrentFilterToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.simulateTrafficToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exportSVGSequenceDiagramToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exportXLSXToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exportCSVToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.remoteCaptureToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.testToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.startToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.stopToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.restartToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.clearToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.eVA2XMLExportToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.bDSToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.reportAnIssueToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openCapturesDialog = new System.Windows.Forms.OpenFileDialog();
            this.statusStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.flowLayoutPanel1.SuspendLayout();
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
            this.timerFlicker.Interval = 500;
            this.timerFlicker.Tick += new System.EventHandler(this.timerFlicker_Tick);
            // 
            // statusStrip1
            // 
            this.statusStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.statusLeft,
            this.statusRight});
            this.statusStrip1.Location = new System.Drawing.Point(0, 667);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Padding = new System.Windows.Forms.Padding(1, 0, 16, 0);
            this.statusStrip1.ShowItemToolTips = true;
            this.statusStrip1.Size = new System.Drawing.Size(1672, 22);
            this.statusStrip1.TabIndex = 4;
            this.statusStrip1.Text = "statusStrip1";
            this.statusStrip1.DoubleClick += new System.EventHandler(this.statusStrip1_DoubleClick);
            // 
            // statusLeft
            // 
            this.statusLeft.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.statusLeft.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.statusLeft.Name = "statusLeft";
            this.statusLeft.Size = new System.Drawing.Size(0, 17);
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
            this.splitContainer1.Location = new System.Drawing.Point(0, 31);
            this.splitContainer1.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.packetListView1);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.tabControl1);
            this.splitContainer1.Size = new System.Drawing.Size(1672, 629);
            this.splitContainer1.SplitterDistance = 1200;
            this.splitContainer1.SplitterWidth = 5;
            this.splitContainer1.TabIndex = 7;
            // 
            // packetListView1
            // 
            this.packetListView1.AllowDrop = true;
            this.packetListView1.AutoScroll = true;
            this.packetListView1.BackStore = null;
            this.packetListView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.packetListView1.Location = new System.Drawing.Point(0, 0);
            this.packetListView1.Margin = new System.Windows.Forms.Padding(5);
            this.packetListView1.Name = "packetListView1";
            this.packetListView1.ParserFactory = null;
            this.packetListView1.SearchString = null;
            packetListSettings1.AutoScroll = false;
            packetListSettings1.ColumnInfos = null;
            packetListSettings1.IgnoreComid = "";
            packetListSettings1.IgnoreDuplicatedPD = false;
            packetListSettings1.IgnoreLoopback = false;
            packetListSettings1.IgnoreUnknownData = false;
            packetListSettings1.IgnoreVariables = new string[] {
        "MMI_M_PACKET",
        "MMI_L_PACKET"};
            this.packetListView1.Settings = packetListSettings1;
            this.packetListView1.Size = new System.Drawing.Size(1200, 629);
            this.packetListView1.TabIndex = 0;
            this.packetListView1.DragDrop += new System.Windows.Forms.DragEventHandler(this.packetListView1_DragDrop);
            this.packetListView1.DragEnter += new System.Windows.Forms.DragEventHandler(this.packetListView1_DragEnter);
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(467, 629);
            this.tabControl1.TabIndex = 14;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.packetDisplay1);
            this.tabPage1.Location = new System.Drawing.Point(4, 24);
            this.tabPage1.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.tabPage1.Size = new System.Drawing.Size(459, 601);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Data";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // packetDisplay1
            // 
            this.packetDisplay1.BackStore = null;
            this.packetDisplay1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.packetDisplay1.Location = new System.Drawing.Point(4, 3);
            this.packetDisplay1.Margin = new System.Windows.Forms.Padding(5);
            this.packetDisplay1.Name = "packetDisplay1";
            this.packetDisplay1.ParserFactory = null;
            this.packetDisplay1.Size = new System.Drawing.Size(451, 595);
            this.packetDisplay1.TabIndex = 0;
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.textBoxSearch);
            this.tabPage2.Controls.Add(this.label3);
            this.tabPage2.Controls.Add(this.label2);
            this.tabPage2.Controls.Add(this.flowLayoutPanel1);
            this.tabPage2.Controls.Add(this.textBoxIgnoreComid);
            this.tabPage2.Controls.Add(this.label1);
            this.tabPage2.Controls.Add(this.textBoxIgnoreVars);
            this.tabPage2.Location = new System.Drawing.Point(4, 24);
            this.tabPage2.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.tabPage2.Size = new System.Drawing.Size(459, 601);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Filters";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // textBoxSearch
            // 
            this.textBoxSearch.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxSearch.Location = new System.Drawing.Point(0, 393);
            this.textBoxSearch.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.textBoxSearch.Name = "textBoxSearch";
            this.textBoxSearch.Size = new System.Drawing.Size(457, 23);
            this.textBoxSearch.TabIndex = 19;
            this.textBoxSearch.TypingFinished += new System.EventHandler(this.typeDelayTextBox1_TypingFinished);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(7, 375);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(42, 15);
            this.label3.TabIndex = 18;
            this.label3.Text = "Search";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(4, 183);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(205, 15);
            this.label2.TabIndex = 15;
            this.label2.Text = "Ignore Variables (separate by newline)";
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.Controls.Add(this.checkBoxParserOnly);
            this.flowLayoutPanel1.Controls.Add(this.checkBoxAutoScroll);
            this.flowLayoutPanel1.Controls.Add(this.checkBoxIgnoreLoopback);
            this.flowLayoutPanel1.Controls.Add(this.checkBoxHideDupes);
            this.flowLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.flowLayoutPanel1.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.flowLayoutPanel1.Location = new System.Drawing.Point(4, 3);
            this.flowLayoutPanel1.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(451, 132);
            this.flowLayoutPanel1.TabIndex = 14;
            // 
            // checkBoxParserOnly
            // 
            this.checkBoxParserOnly.AutoSize = true;
            this.checkBoxParserOnly.Checked = true;
            this.checkBoxParserOnly.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBoxParserOnly.Location = new System.Drawing.Point(4, 3);
            this.checkBoxParserOnly.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.checkBoxParserOnly.Name = "checkBoxParserOnly";
            this.checkBoxParserOnly.Size = new System.Drawing.Size(146, 19);
            this.checkBoxParserOnly.TabIndex = 8;
            this.checkBoxParserOnly.Text = "Show only known data";
            this.checkBoxParserOnly.UseVisualStyleBackColor = true;
            // 
            // checkBoxAutoScroll
            // 
            this.checkBoxAutoScroll.AutoSize = true;
            this.checkBoxAutoScroll.Location = new System.Drawing.Point(4, 28);
            this.checkBoxAutoScroll.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.checkBoxAutoScroll.Name = "checkBoxAutoScroll";
            this.checkBoxAutoScroll.Size = new System.Drawing.Size(81, 19);
            this.checkBoxAutoScroll.TabIndex = 10;
            this.checkBoxAutoScroll.Text = "AutoScroll";
            this.checkBoxAutoScroll.UseVisualStyleBackColor = true;
            // 
            // checkBoxIgnoreLoopback
            // 
            this.checkBoxIgnoreLoopback.AutoSize = true;
            this.checkBoxIgnoreLoopback.Location = new System.Drawing.Point(4, 53);
            this.checkBoxIgnoreLoopback.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.checkBoxIgnoreLoopback.Name = "checkBoxIgnoreLoopback";
            this.checkBoxIgnoreLoopback.Size = new System.Drawing.Size(115, 19);
            this.checkBoxIgnoreLoopback.TabIndex = 11;
            this.checkBoxIgnoreLoopback.Text = "Ignore Loopback";
            this.checkBoxIgnoreLoopback.UseVisualStyleBackColor = true;
            // 
            // checkBoxHideDupes
            // 
            this.checkBoxHideDupes.AutoSize = true;
            this.checkBoxHideDupes.Checked = true;
            this.checkBoxHideDupes.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBoxHideDupes.Location = new System.Drawing.Point(4, 78);
            this.checkBoxHideDupes.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.checkBoxHideDupes.Name = "checkBoxHideDupes";
            this.checkBoxHideDupes.Size = new System.Drawing.Size(187, 19);
            this.checkBoxHideDupes.TabIndex = 9;
            this.checkBoxHideDupes.Text = "Ignore Duplicated ProcessData";
            this.checkBoxHideDupes.UseVisualStyleBackColor = true;
            // 
            // textBoxIgnoreComid
            // 
            this.textBoxIgnoreComid.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxIgnoreComid.Location = new System.Drawing.Point(0, 157);
            this.textBoxIgnoreComid.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.textBoxIgnoreComid.Name = "textBoxIgnoreComid";
            this.textBoxIgnoreComid.Size = new System.Drawing.Size(457, 23);
            this.textBoxIgnoreComid.TabIndex = 12;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(4, 138);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(195, 15);
            this.label1.TabIndex = 13;
            this.label1.Text = "Ignore Comid (separate by comma)";
            // 
            // textBoxIgnoreVars
            // 
            this.textBoxIgnoreVars.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxIgnoreVars.Location = new System.Drawing.Point(0, 202);
            this.textBoxIgnoreVars.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.textBoxIgnoreVars.Multiline = true;
            this.textBoxIgnoreVars.Name = "textBoxIgnoreVars";
            this.textBoxIgnoreVars.Size = new System.Drawing.Size(457, 169);
            this.textBoxIgnoreVars.TabIndex = 17;
            this.textBoxIgnoreVars.Text = "MMI_M_PACKET\r\nMMI_L_PACKET";
            this.textBoxIgnoreVars.TypingFinished += new System.EventHandler(this.textBoxIgnoreVars_TypingFinished);
            // 
            // menuStrip1
            // 
            this.menuStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.toolsToolStripMenuItem,
            this.startToolStripMenuItem,
            this.stopToolStripMenuItem,
            this.restartToolStripMenuItem,
            this.clearToolStripMenuItem,
            this.toolStripMenuItem1,
            this.reportAnIssueToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Padding = new System.Windows.Forms.Padding(7, 2, 0, 2);
            this.menuStrip1.Size = new System.Drawing.Size(1672, 24);
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
            this.exportSVGSequenceDiagramToolStripMenuItem,
            this.exportXLSXToolStripMenuItem,
            this.exportCSVToolStripMenuItem,
            this.remoteCaptureToolStripMenuItem,
            this.testToolStripMenuItem});
            this.toolsToolStripMenuItem.Name = "toolsToolStripMenuItem";
            this.toolsToolStripMenuItem.Size = new System.Drawing.Size(46, 20);
            this.toolsToolStripMenuItem.Text = "Tools";
            // 
            // simulateTrafficToolStripMenuItem
            // 
            this.simulateTrafficToolStripMenuItem.Name = "simulateTrafficToolStripMenuItem";
            this.simulateTrafficToolStripMenuItem.Size = new System.Drawing.Size(232, 22);
            this.simulateTrafficToolStripMenuItem.Text = "Simulate Traffic";
            this.simulateTrafficToolStripMenuItem.Click += new System.EventHandler(this.simulateTrafficToolStripMenuItem_Click);
            // 
            // exportSVGSequenceDiagramToolStripMenuItem
            // 
            this.exportSVGSequenceDiagramToolStripMenuItem.Name = "exportSVGSequenceDiagramToolStripMenuItem";
            this.exportSVGSequenceDiagramToolStripMenuItem.Size = new System.Drawing.Size(232, 22);
            this.exportSVGSequenceDiagramToolStripMenuItem.Text = "Export SVG sequence diagram";
            this.exportSVGSequenceDiagramToolStripMenuItem.Click += new System.EventHandler(this.exportSVGSequenceDiagramToolStripMenuItem_Click);
            // 
            // exportXLSXToolStripMenuItem
            // 
            this.exportXLSXToolStripMenuItem.Name = "exportXLSXToolStripMenuItem";
            this.exportXLSXToolStripMenuItem.Size = new System.Drawing.Size(232, 22);
            this.exportXLSXToolStripMenuItem.Text = "Export XLSX";
            this.exportXLSXToolStripMenuItem.Click += new System.EventHandler(this.exportXLSXToolStripMenuItem_Click);
            // 
            // exportCSVToolStripMenuItem
            // 
            this.exportCSVToolStripMenuItem.Name = "exportCSVToolStripMenuItem";
            this.exportCSVToolStripMenuItem.Size = new System.Drawing.Size(232, 22);
            this.exportCSVToolStripMenuItem.Text = "Export CSV";
            // 
            // remoteCaptureToolStripMenuItem
            // 
            this.remoteCaptureToolStripMenuItem.Name = "remoteCaptureToolStripMenuItem";
            this.remoteCaptureToolStripMenuItem.Size = new System.Drawing.Size(232, 22);
            this.remoteCaptureToolStripMenuItem.Text = "Remote Capture";
            this.remoteCaptureToolStripMenuItem.Click += new System.EventHandler(this.RemoteCaptureToolStripMenuItem_Click);
            // 
            // testToolStripMenuItem
            // 
            this.testToolStripMenuItem.Name = "testToolStripMenuItem";
            this.testToolStripMenuItem.Size = new System.Drawing.Size(232, 22);
            this.testToolStripMenuItem.Text = "Test";
            this.testToolStripMenuItem.Click += new System.EventHandler(this.testToolStripMenuItem_Click);
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
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.eVA2XMLExportToolStripMenuItem,
            this.bDSToolStripMenuItem});
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(55, 20);
            this.toolStripMenuItem1.Text = "Import";
            // 
            // eVA2XMLExportToolStripMenuItem
            // 
            this.eVA2XMLExportToolStripMenuItem.Name = "eVA2XMLExportToolStripMenuItem";
            this.eVA2XMLExportToolStripMenuItem.Size = new System.Drawing.Size(164, 22);
            this.eVA2XMLExportToolStripMenuItem.Text = "EVA2 XML Export";
            this.eVA2XMLExportToolStripMenuItem.Click += new System.EventHandler(this.eVA2XMLExportToolStripMenuItem_Click);
            // 
            // bDSToolStripMenuItem
            // 
            this.bDSToolStripMenuItem.Name = "bDSToolStripMenuItem";
            this.bDSToolStripMenuItem.Size = new System.Drawing.Size(164, 22);
            this.bDSToolStripMenuItem.Text = "BDS";
            this.bDSToolStripMenuItem.Click += new System.EventHandler(this.bDSToolStripMenuItem_Click);
            // 
            // reportAnIssueToolStripMenuItem
            // 
            this.reportAnIssueToolStripMenuItem.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            this.reportAnIssueToolStripMenuItem.Name = "reportAnIssueToolStripMenuItem";
            this.reportAnIssueToolStripMenuItem.Size = new System.Drawing.Size(101, 20);
            this.reportAnIssueToolStripMenuItem.Text = "Report An Issue";
            this.reportAnIssueToolStripMenuItem.Click += new System.EventHandler(this.reportAnIssueToolStripMenuItem_Click);
            // 
            // openCapturesDialog
            // 
            this.openCapturesDialog.FileName = "openFileDialog1";
            this.openCapturesDialog.Multiselect = true;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1672, 689);
            this.Controls.Add(this.splitContainer1);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.menuStrip1);
            this.DoubleBuffered = true;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.KeyPreview = true;
            this.MainMenuStrip = this.menuStrip1;
            this.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.Name = "MainForm";
            this.Text = "IPTComShark";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.KeyUp += new System.Windows.Forms.KeyEventHandler(this.MainForm_KeyUp);
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage2.ResumeLayout(false);
            this.tabPage2.PerformLayout();
            this.flowLayoutPanel1.ResumeLayout(false);
            this.flowLayoutPanel1.PerformLayout();
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
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem eVA2XMLExportToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem reportAnIssueToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem remoteCaptureToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem bDSToolStripMenuItem;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private System.Windows.Forms.Label label2;
        private TypeDelayTextBox textBoxIgnoreVars;
        private TypeDelayTextBox textBoxSearch;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ToolStripMenuItem testToolStripMenuItem;
    }
}

