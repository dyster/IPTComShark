using TrainShark.Controls;

namespace TrainShark
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
            components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            PacketListSettings packetListSettings1 = new PacketListSettings();
            backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
            timerFlicker = new System.Windows.Forms.Timer(components);
            statusStrip1 = new System.Windows.Forms.StatusStrip();
            statusLeft = new System.Windows.Forms.ToolStripStatusLabel();
            statusRight = new System.Windows.Forms.ToolStripStatusLabel();
            toolStripStatusLabelSeparator1 = new System.Windows.Forms.ToolStripStatusLabel();
            toolStripSplitButtonOpenLog = new System.Windows.Forms.ToolStripSplitButton();
            toolStripSplitButtonCopyLog = new System.Windows.Forms.ToolStripSplitButton();
            splitContainer1 = new System.Windows.Forms.SplitContainer();
            packetListView1 = new PacketListView();
            tabControl1 = new System.Windows.Forms.TabControl();
            tabPage1 = new System.Windows.Forms.TabPage();
            packetDisplay1 = new PacketDisplay();
            tabPage2 = new System.Windows.Forms.TabPage();
            textBoxSearch = new TypeDelayTextBox();
            label3 = new System.Windows.Forms.Label();
            label2 = new System.Windows.Forms.Label();
            flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            checkBoxParserOnly = new System.Windows.Forms.CheckBox();
            checkBoxAutoScroll = new System.Windows.Forms.CheckBox();
            checkBoxIgnoreLoopback = new System.Windows.Forms.CheckBox();
            checkBoxHideDupes = new System.Windows.Forms.CheckBox();
            textBoxIgnoreComid = new System.Windows.Forms.TextBox();
            label1 = new System.Windows.Forms.Label();
            textBoxIgnoreVars = new TypeDelayTextBox();
            menuStrip1 = new System.Windows.Forms.MenuStrip();
            fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            openFilesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            openFolderToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            saveAllToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            saveCurrentFilterToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            quitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            toolsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            simulateTrafficToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            exportSVGSequenceDiagramToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            exportXLSXToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            exportCSVToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            remoteCaptureToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            testToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            captureToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            startToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            stopToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            restartToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            clearToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            toolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            eVA2XMLExportToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            windowToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            alwaysOnTopToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            reportAnIssueToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            openCapturesDialog = new System.Windows.Forms.OpenFileDialog();
            errorProvider1 = new System.Windows.Forms.ErrorProvider(components);
            statusStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)splitContainer1).BeginInit();
            splitContainer1.Panel1.SuspendLayout();
            splitContainer1.Panel2.SuspendLayout();
            splitContainer1.SuspendLayout();
            tabControl1.SuspendLayout();
            tabPage1.SuspendLayout();
            tabPage2.SuspendLayout();
            flowLayoutPanel1.SuspendLayout();
            menuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)errorProvider1).BeginInit();
            SuspendLayout();
            // 
            // backgroundWorker1
            // 
            backgroundWorker1.WorkerSupportsCancellation = true;
            backgroundWorker1.DoWork += backgroundWorker1_DoWork;
            backgroundWorker1.RunWorkerCompleted += backgroundWorker1_RunWorkerCompleted;
            // 
            // timerFlicker
            // 
            timerFlicker.Interval = 500;
            timerFlicker.Tick += timerFlicker_Tick;
            // 
            // statusStrip1
            // 
            statusStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] { statusLeft, statusRight, toolStripStatusLabelSeparator1, toolStripSplitButtonOpenLog, toolStripSplitButtonCopyLog });
            statusStrip1.Location = new System.Drawing.Point(0, 1116);
            statusStrip1.Name = "statusStrip1";
            statusStrip1.Padding = new System.Windows.Forms.Padding(1, 0, 23, 0);
            statusStrip1.ShowItemToolTips = true;
            statusStrip1.Size = new System.Drawing.Size(2389, 32);
            statusStrip1.TabIndex = 4;
            statusStrip1.Text = "statusStrip1";
            statusStrip1.DoubleClick += statusStrip1_DoubleClick;
            // 
            // statusLeft
            // 
            statusLeft.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            statusLeft.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            statusLeft.Name = "statusLeft";
            statusLeft.Size = new System.Drawing.Size(0, 25);
            statusLeft.Text = "toolStripStatusLabel1";
            // 
            // statusRight
            // 
            statusRight.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            statusRight.Name = "statusRight";
            statusRight.Size = new System.Drawing.Size(179, 25);
            statusRight.Text = "toolStripStatusLabel1";
            // 
            // toolStripStatusLabelSeparator1
            // 
            toolStripStatusLabelSeparator1.Name = "toolStripStatusLabelSeparator1";
            toolStripStatusLabelSeparator1.Size = new System.Drawing.Size(16, 25);
            toolStripStatusLabelSeparator1.Text = "|";
            // 
            // toolStripSplitButtonOpenLog
            // 
            toolStripSplitButtonOpenLog.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            toolStripSplitButtonOpenLog.DropDownButtonWidth = 0;
            toolStripSplitButtonOpenLog.Image = (System.Drawing.Image)resources.GetObject("toolStripSplitButtonOpenLog.Image");
            toolStripSplitButtonOpenLog.ImageTransparentColor = System.Drawing.Color.Magenta;
            toolStripSplitButtonOpenLog.Name = "toolStripSplitButtonOpenLog";
            toolStripSplitButtonOpenLog.Size = new System.Drawing.Size(96, 29);
            toolStripSplitButtonOpenLog.Text = "Open Log";
            toolStripSplitButtonOpenLog.ButtonClick += statusStrip1_DoubleClick;
            // 
            // toolStripSplitButtonCopyLog
            // 
            toolStripSplitButtonCopyLog.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            toolStripSplitButtonCopyLog.DropDownButtonWidth = 0;
            toolStripSplitButtonCopyLog.Image = (System.Drawing.Image)resources.GetObject("toolStripSplitButtonCopyLog.Image");
            toolStripSplitButtonCopyLog.ImageTransparentColor = System.Drawing.Color.Magenta;
            toolStripSplitButtonCopyLog.Name = "toolStripSplitButtonCopyLog";
            toolStripSplitButtonCopyLog.Size = new System.Drawing.Size(94, 29);
            toolStripSplitButtonCopyLog.Text = "Copy Log";
            toolStripSplitButtonCopyLog.ButtonClick += toolStripSplitButtonCopyLog_ButtonClick;
            // 
            // splitContainer1
            // 
            splitContainer1.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            splitContainer1.Location = new System.Drawing.Point(0, 52);
            splitContainer1.Margin = new System.Windows.Forms.Padding(6, 5, 6, 5);
            splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            splitContainer1.Panel1.Controls.Add(packetListView1);
            // 
            // splitContainer1.Panel2
            // 
            splitContainer1.Panel2.Controls.Add(tabControl1);
            splitContainer1.Size = new System.Drawing.Size(2389, 1048);
            splitContainer1.SplitterDistance = 1714;
            splitContainer1.SplitterWidth = 7;
            splitContainer1.TabIndex = 7;
            // 
            // packetListView1
            // 
            packetListView1.AllowDrop = true;
            packetListView1.AutoScroll = true;
            packetListView1.BackStore = null;
            packetListView1.Dock = System.Windows.Forms.DockStyle.Fill;
            packetListView1.Location = new System.Drawing.Point(0, 0);
            packetListView1.Margin = new System.Windows.Forms.Padding(7, 8, 7, 8);
            packetListView1.Name = "packetListView1";
            packetListView1.ParserFactory = null;
            packetListView1.SearchString = null;
            packetListSettings1.AutoScroll = false;
            packetListSettings1.ColumnInfos = null;
            packetListSettings1.IgnoreComid = "";
            packetListSettings1.IgnoreDuplicatedPD = false;
            packetListSettings1.IgnoreLoopback = false;
            packetListSettings1.IgnoreUnknownData = false;
            packetListSettings1.IgnoreVariables = new string[]
    {
    "MMI_M_PACKET",
    "MMI_L_PACKET"
    };
            packetListView1.Settings = packetListSettings1;
            packetListView1.Size = new System.Drawing.Size(1714, 1048);
            packetListView1.TabIndex = 0;
            packetListView1.DragDrop += packetListView1_DragDrop;
            packetListView1.DragEnter += packetListView1_DragEnter;
            // 
            // tabControl1
            // 
            tabControl1.Controls.Add(tabPage1);
            tabControl1.Controls.Add(tabPage2);
            tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            tabControl1.Location = new System.Drawing.Point(0, 0);
            tabControl1.Margin = new System.Windows.Forms.Padding(6, 5, 6, 5);
            tabControl1.Name = "tabControl1";
            tabControl1.SelectedIndex = 0;
            tabControl1.Size = new System.Drawing.Size(668, 1048);
            tabControl1.TabIndex = 14;
            // 
            // tabPage1
            // 
            tabPage1.Controls.Add(packetDisplay1);
            tabPage1.Location = new System.Drawing.Point(4, 34);
            tabPage1.Margin = new System.Windows.Forms.Padding(6, 5, 6, 5);
            tabPage1.Name = "tabPage1";
            tabPage1.Padding = new System.Windows.Forms.Padding(6, 5, 6, 5);
            tabPage1.Size = new System.Drawing.Size(660, 1010);
            tabPage1.TabIndex = 0;
            tabPage1.Text = "Data";
            tabPage1.UseVisualStyleBackColor = true;
            // 
            // packetDisplay1
            // 
            packetDisplay1.BackStore = null;
            packetDisplay1.Dock = System.Windows.Forms.DockStyle.Fill;
            packetDisplay1.Location = new System.Drawing.Point(6, 5);
            packetDisplay1.Margin = new System.Windows.Forms.Padding(7, 8, 7, 8);
            packetDisplay1.Name = "packetDisplay1";
            packetDisplay1.ParserFactory = null;
            packetDisplay1.Size = new System.Drawing.Size(648, 1000);
            packetDisplay1.TabIndex = 0;
            // 
            // tabPage2
            // 
            tabPage2.Controls.Add(textBoxSearch);
            tabPage2.Controls.Add(label3);
            tabPage2.Controls.Add(label2);
            tabPage2.Controls.Add(flowLayoutPanel1);
            tabPage2.Controls.Add(textBoxIgnoreComid);
            tabPage2.Controls.Add(label1);
            tabPage2.Controls.Add(textBoxIgnoreVars);
            tabPage2.Location = new System.Drawing.Point(4, 34);
            tabPage2.Margin = new System.Windows.Forms.Padding(6, 5, 6, 5);
            tabPage2.Name = "tabPage2";
            tabPage2.Padding = new System.Windows.Forms.Padding(6, 5, 6, 5);
            tabPage2.Size = new System.Drawing.Size(660, 1010);
            tabPage2.TabIndex = 1;
            tabPage2.Text = "Filters";
            tabPage2.UseVisualStyleBackColor = true;
            // 
            // textBoxSearch
            // 
            textBoxSearch.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            textBoxSearch.Location = new System.Drawing.Point(0, 655);
            textBoxSearch.Margin = new System.Windows.Forms.Padding(6, 5, 6, 5);
            textBoxSearch.Name = "textBoxSearch";
            textBoxSearch.Size = new System.Drawing.Size(651, 31);
            textBoxSearch.TabIndex = 19;
            textBoxSearch.TypingFinished += typeDelayTextBox1_TypingFinished;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new System.Drawing.Point(10, 625);
            label3.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            label3.Name = "label3";
            label3.Size = new System.Drawing.Size(64, 25);
            label3.TabIndex = 18;
            label3.Text = "Search";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new System.Drawing.Point(6, 305);
            label2.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            label2.Name = "label2";
            label2.Size = new System.Drawing.Size(310, 25);
            label2.TabIndex = 15;
            label2.Text = "Ignore Variables (separate by newline)";
            // 
            // flowLayoutPanel1
            // 
            flowLayoutPanel1.Controls.Add(checkBoxParserOnly);
            flowLayoutPanel1.Controls.Add(checkBoxAutoScroll);
            flowLayoutPanel1.Controls.Add(checkBoxIgnoreLoopback);
            flowLayoutPanel1.Controls.Add(checkBoxHideDupes);
            flowLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Top;
            flowLayoutPanel1.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            flowLayoutPanel1.Location = new System.Drawing.Point(6, 5);
            flowLayoutPanel1.Margin = new System.Windows.Forms.Padding(6, 5, 6, 5);
            flowLayoutPanel1.Name = "flowLayoutPanel1";
            flowLayoutPanel1.Size = new System.Drawing.Size(648, 220);
            flowLayoutPanel1.TabIndex = 14;
            // 
            // checkBoxParserOnly
            // 
            checkBoxParserOnly.AutoSize = true;
            checkBoxParserOnly.Checked = true;
            checkBoxParserOnly.CheckState = System.Windows.Forms.CheckState.Checked;
            checkBoxParserOnly.Location = new System.Drawing.Point(6, 5);
            checkBoxParserOnly.Margin = new System.Windows.Forms.Padding(6, 5, 6, 5);
            checkBoxParserOnly.Name = "checkBoxParserOnly";
            checkBoxParserOnly.Size = new System.Drawing.Size(219, 29);
            checkBoxParserOnly.TabIndex = 8;
            checkBoxParserOnly.Text = "Show only known data";
            checkBoxParserOnly.UseVisualStyleBackColor = true;
            // 
            // checkBoxAutoScroll
            // 
            checkBoxAutoScroll.AutoSize = true;
            checkBoxAutoScroll.Location = new System.Drawing.Point(6, 44);
            checkBoxAutoScroll.Margin = new System.Windows.Forms.Padding(6, 5, 6, 5);
            checkBoxAutoScroll.Name = "checkBoxAutoScroll";
            checkBoxAutoScroll.Size = new System.Drawing.Size(120, 29);
            checkBoxAutoScroll.TabIndex = 10;
            checkBoxAutoScroll.Text = "AutoScroll";
            checkBoxAutoScroll.UseVisualStyleBackColor = true;
            // 
            // checkBoxIgnoreLoopback
            // 
            checkBoxIgnoreLoopback.AutoSize = true;
            checkBoxIgnoreLoopback.Location = new System.Drawing.Point(6, 83);
            checkBoxIgnoreLoopback.Margin = new System.Windows.Forms.Padding(6, 5, 6, 5);
            checkBoxIgnoreLoopback.Name = "checkBoxIgnoreLoopback";
            checkBoxIgnoreLoopback.Size = new System.Drawing.Size(173, 29);
            checkBoxIgnoreLoopback.TabIndex = 11;
            checkBoxIgnoreLoopback.Text = "Ignore Loopback";
            checkBoxIgnoreLoopback.UseVisualStyleBackColor = true;
            // 
            // checkBoxHideDupes
            // 
            checkBoxHideDupes.AutoSize = true;
            checkBoxHideDupes.Checked = true;
            checkBoxHideDupes.CheckState = System.Windows.Forms.CheckState.Checked;
            checkBoxHideDupes.Location = new System.Drawing.Point(6, 122);
            checkBoxHideDupes.Margin = new System.Windows.Forms.Padding(6, 5, 6, 5);
            checkBoxHideDupes.Name = "checkBoxHideDupes";
            checkBoxHideDupes.Size = new System.Drawing.Size(282, 29);
            checkBoxHideDupes.TabIndex = 9;
            checkBoxHideDupes.Text = "Ignore Duplicated ProcessData";
            checkBoxHideDupes.UseVisualStyleBackColor = true;
            // 
            // textBoxIgnoreComid
            // 
            textBoxIgnoreComid.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            textBoxIgnoreComid.Location = new System.Drawing.Point(0, 262);
            textBoxIgnoreComid.Margin = new System.Windows.Forms.Padding(6, 5, 6, 5);
            textBoxIgnoreComid.Name = "textBoxIgnoreComid";
            textBoxIgnoreComid.ShortcutsEnabled = false;
            textBoxIgnoreComid.Size = new System.Drawing.Size(621, 31);
            textBoxIgnoreComid.TabIndex = 12;
            textBoxIgnoreComid.KeyDown += textBoxIgnoreComid_KeyDown;
            textBoxIgnoreComid.KeyPress += textBoxIgnoreComid_KeyPress;
            textBoxIgnoreComid.Validating += textBoxIgnoreComid_Validating;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new System.Drawing.Point(6, 230);
            label1.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            label1.Name = "label1";
            label1.Size = new System.Drawing.Size(294, 25);
            label1.TabIndex = 13;
            label1.Text = "Ignore Comid (separate by comma)";
            // 
            // textBoxIgnoreVars
            // 
            textBoxIgnoreVars.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            textBoxIgnoreVars.Location = new System.Drawing.Point(0, 337);
            textBoxIgnoreVars.Margin = new System.Windows.Forms.Padding(6, 5, 6, 5);
            textBoxIgnoreVars.Multiline = true;
            textBoxIgnoreVars.Name = "textBoxIgnoreVars";
            textBoxIgnoreVars.Size = new System.Drawing.Size(651, 279);
            textBoxIgnoreVars.TabIndex = 17;
            textBoxIgnoreVars.Text = "MMI_M_PACKET\r\nMMI_L_PACKET";
            textBoxIgnoreVars.TypingFinished += textBoxIgnoreVars_TypingFinished;
            // 
            // menuStrip1
            // 
            menuStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] { fileToolStripMenuItem, toolsToolStripMenuItem, captureToolStripMenuItem, toolStripMenuItem1, windowToolStripMenuItem, reportAnIssueToolStripMenuItem });
            menuStrip1.Location = new System.Drawing.Point(0, 0);
            menuStrip1.Name = "menuStrip1";
            menuStrip1.Padding = new System.Windows.Forms.Padding(10, 3, 0, 3);
            menuStrip1.Size = new System.Drawing.Size(2389, 35);
            menuStrip1.TabIndex = 15;
            menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] { openFilesToolStripMenuItem, openFolderToolStripMenuItem, saveAllToolStripMenuItem, saveCurrentFilterToolStripMenuItem, toolStripSeparator2, quitToolStripMenuItem });
            fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            fileToolStripMenuItem.Size = new System.Drawing.Size(54, 29);
            fileToolStripMenuItem.Text = "File";
            // 
            // openFilesToolStripMenuItem
            // 
            openFilesToolStripMenuItem.Name = "openFilesToolStripMenuItem";
            openFilesToolStripMenuItem.Size = new System.Drawing.Size(257, 34);
            openFilesToolStripMenuItem.Text = "Open Files";
            openFilesToolStripMenuItem.Click += openFilesToolStripMenuItem_Click;
            // 
            // openFolderToolStripMenuItem
            // 
            openFolderToolStripMenuItem.Name = "openFolderToolStripMenuItem";
            openFolderToolStripMenuItem.Size = new System.Drawing.Size(257, 34);
            openFolderToolStripMenuItem.Text = "Open Folder";
            openFolderToolStripMenuItem.Click += openFolderToolStripMenuItem_Click;
            // 
            // saveAllToolStripMenuItem
            // 
            saveAllToolStripMenuItem.Name = "saveAllToolStripMenuItem";
            saveAllToolStripMenuItem.Size = new System.Drawing.Size(257, 34);
            saveAllToolStripMenuItem.Text = "Save All";
            saveAllToolStripMenuItem.Click += buttonSaveAll_Click;
            // 
            // saveCurrentFilterToolStripMenuItem
            // 
            saveCurrentFilterToolStripMenuItem.Name = "saveCurrentFilterToolStripMenuItem";
            saveCurrentFilterToolStripMenuItem.Size = new System.Drawing.Size(257, 34);
            saveCurrentFilterToolStripMenuItem.Text = "Save Current Filter";
            saveCurrentFilterToolStripMenuItem.Click += saveCurrentFilterToolStripMenuItem_Click;
            // 
            // toolStripSeparator2
            // 
            toolStripSeparator2.Name = "toolStripSeparator2";
            toolStripSeparator2.Size = new System.Drawing.Size(254, 6);
            // 
            // quitToolStripMenuItem
            // 
            quitToolStripMenuItem.Name = "quitToolStripMenuItem";
            quitToolStripMenuItem.Size = new System.Drawing.Size(257, 34);
            quitToolStripMenuItem.Text = "Quit";
            quitToolStripMenuItem.Click += quitToolStripMenuItem_Click;
            // 
            // toolsToolStripMenuItem
            // 
            toolsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] { simulateTrafficToolStripMenuItem, exportSVGSequenceDiagramToolStripMenuItem, exportXLSXToolStripMenuItem, exportCSVToolStripMenuItem, remoteCaptureToolStripMenuItem, testToolStripMenuItem });
            toolsToolStripMenuItem.Name = "toolsToolStripMenuItem";
            toolsToolStripMenuItem.Size = new System.Drawing.Size(69, 29);
            toolsToolStripMenuItem.Text = "Tools";
            // 
            // simulateTrafficToolStripMenuItem
            // 
            simulateTrafficToolStripMenuItem.Name = "simulateTrafficToolStripMenuItem";
            simulateTrafficToolStripMenuItem.Size = new System.Drawing.Size(353, 34);
            simulateTrafficToolStripMenuItem.Text = "Simulate Traffic";
            simulateTrafficToolStripMenuItem.Click += simulateTrafficToolStripMenuItem_Click;
            // 
            // exportSVGSequenceDiagramToolStripMenuItem
            // 
            exportSVGSequenceDiagramToolStripMenuItem.Enabled = false;
            exportSVGSequenceDiagramToolStripMenuItem.Name = "exportSVGSequenceDiagramToolStripMenuItem";
            exportSVGSequenceDiagramToolStripMenuItem.Size = new System.Drawing.Size(353, 34);
            exportSVGSequenceDiagramToolStripMenuItem.Text = "Export SVG sequence diagram";
            exportSVGSequenceDiagramToolStripMenuItem.Click += exportSVGSequenceDiagramToolStripMenuItem_Click;
            // 
            // exportXLSXToolStripMenuItem
            // 
            exportXLSXToolStripMenuItem.Name = "exportXLSXToolStripMenuItem";
            exportXLSXToolStripMenuItem.Size = new System.Drawing.Size(353, 34);
            exportXLSXToolStripMenuItem.Text = "Export XLSX";
            exportXLSXToolStripMenuItem.Click += exportXLSXToolStripMenuItem_Click;
            // 
            // exportCSVToolStripMenuItem
            // 
            exportCSVToolStripMenuItem.Name = "exportCSVToolStripMenuItem";
            exportCSVToolStripMenuItem.Size = new System.Drawing.Size(353, 34);
            exportCSVToolStripMenuItem.Text = "Export CSV";
            // 
            // remoteCaptureToolStripMenuItem
            // 
            remoteCaptureToolStripMenuItem.Name = "remoteCaptureToolStripMenuItem";
            remoteCaptureToolStripMenuItem.Size = new System.Drawing.Size(353, 34);
            remoteCaptureToolStripMenuItem.Text = "Remote Capture";
            remoteCaptureToolStripMenuItem.Click += RemoteCaptureToolStripMenuItem_Click;
            // 
            // testToolStripMenuItem
            // 
            testToolStripMenuItem.Name = "testToolStripMenuItem";
            testToolStripMenuItem.Size = new System.Drawing.Size(353, 34);
            testToolStripMenuItem.Text = "Test";
            testToolStripMenuItem.Click += testToolStripMenuItem_Click;
            // 
            // captureToolStripMenuItem
            // 
            captureToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] { startToolStripMenuItem, stopToolStripMenuItem, restartToolStripMenuItem, toolStripSeparator1, clearToolStripMenuItem1 });
            captureToolStripMenuItem.Name = "captureToolStripMenuItem";
            captureToolStripMenuItem.Size = new System.Drawing.Size(90, 29);
            captureToolStripMenuItem.Text = "Capture";
            // 
            // startToolStripMenuItem
            // 
            startToolStripMenuItem.Name = "startToolStripMenuItem";
            startToolStripMenuItem.Size = new System.Drawing.Size(168, 34);
            startToolStripMenuItem.Text = "Start";
            startToolStripMenuItem.Click += buttonStart_Click;
            // 
            // stopToolStripMenuItem
            // 
            stopToolStripMenuItem.Name = "stopToolStripMenuItem";
            stopToolStripMenuItem.Size = new System.Drawing.Size(168, 34);
            stopToolStripMenuItem.Text = "Stop";
            stopToolStripMenuItem.Click += buttonStop_Click;
            // 
            // restartToolStripMenuItem
            // 
            restartToolStripMenuItem.Name = "restartToolStripMenuItem";
            restartToolStripMenuItem.Size = new System.Drawing.Size(168, 34);
            restartToolStripMenuItem.Text = "Restart";
            restartToolStripMenuItem.Click += buttonRestart_Click;
            // 
            // toolStripSeparator1
            // 
            toolStripSeparator1.Name = "toolStripSeparator1";
            toolStripSeparator1.Size = new System.Drawing.Size(165, 6);
            // 
            // clearToolStripMenuItem1
            // 
            clearToolStripMenuItem1.Name = "clearToolStripMenuItem1";
            clearToolStripMenuItem1.Size = new System.Drawing.Size(168, 34);
            clearToolStripMenuItem1.Text = "Clear";
            clearToolStripMenuItem1.Click += clearToolStripMenuItem_Click;
            // 
            // toolStripMenuItem1
            // 
            toolStripMenuItem1.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] { eVA2XMLExportToolStripMenuItem });
            toolStripMenuItem1.Name = "toolStripMenuItem1";
            toolStripMenuItem1.Size = new System.Drawing.Size(83, 29);
            toolStripMenuItem1.Text = "Import";
            // 
            // eVA2XMLExportToolStripMenuItem
            // 
            eVA2XMLExportToolStripMenuItem.Name = "eVA2XMLExportToolStripMenuItem";
            eVA2XMLExportToolStripMenuItem.Size = new System.Drawing.Size(270, 34);
            eVA2XMLExportToolStripMenuItem.Text = "EVA2 XML Export";
            eVA2XMLExportToolStripMenuItem.Click += eVA2XMLExportToolStripMenuItem_Click;
            // 
            // windowToolStripMenuItem
            // 
            windowToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] { alwaysOnTopToolStripMenuItem });
            windowToolStripMenuItem.Name = "windowToolStripMenuItem";
            windowToolStripMenuItem.Size = new System.Drawing.Size(94, 29);
            windowToolStripMenuItem.Text = "Window";
            // 
            // alwaysOnTopToolStripMenuItem
            // 
            alwaysOnTopToolStripMenuItem.Name = "alwaysOnTopToolStripMenuItem";
            alwaysOnTopToolStripMenuItem.Size = new System.Drawing.Size(228, 34);
            alwaysOnTopToolStripMenuItem.Text = "Always on top";
            alwaysOnTopToolStripMenuItem.Click += alwaysOnTopToolStripMenuItem_Click;
            // 
            // reportAnIssueToolStripMenuItem
            // 
            reportAnIssueToolStripMenuItem.BackColor = System.Drawing.Color.FromArgb(255, 128, 0);
            reportAnIssueToolStripMenuItem.Name = "reportAnIssueToolStripMenuItem";
            reportAnIssueToolStripMenuItem.Size = new System.Drawing.Size(296, 29);
            reportAnIssueToolStripMenuItem.Text = "Report An Issue / Request Feature";
            reportAnIssueToolStripMenuItem.Click += reportAnIssueToolStripMenuItem_Click;
            // 
            // openCapturesDialog
            // 
            openCapturesDialog.FileName = "openFileDialog1";
            openCapturesDialog.Multiselect = true;
            // 
            // errorProvider1
            // 
            errorProvider1.ContainerControl = this;
            // 
            // MainForm
            // 
            AutoScaleDimensions = new System.Drawing.SizeF(10F, 25F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            ClientSize = new System.Drawing.Size(2389, 1148);
            Controls.Add(splitContainer1);
            Controls.Add(statusStrip1);
            Controls.Add(menuStrip1);
            DoubleBuffered = true;
            Icon = (System.Drawing.Icon)resources.GetObject("$this.Icon");
            KeyPreview = true;
            MainMenuStrip = menuStrip1;
            Margin = new System.Windows.Forms.Padding(6, 5, 6, 5);
            Name = "MainForm";
            Text = "IPTComShark";
            FormClosing += Form1_FormClosing;
            Load += MainForm_Load;
            KeyUp += MainForm_KeyUp;
            statusStrip1.ResumeLayout(false);
            statusStrip1.PerformLayout();
            splitContainer1.Panel1.ResumeLayout(false);
            splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)splitContainer1).EndInit();
            splitContainer1.ResumeLayout(false);
            tabControl1.ResumeLayout(false);
            tabPage1.ResumeLayout(false);
            tabPage2.ResumeLayout(false);
            tabPage2.PerformLayout();
            flowLayoutPanel1.ResumeLayout(false);
            flowLayoutPanel1.PerformLayout();
            menuStrip1.ResumeLayout(false);
            menuStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)errorProvider1).EndInit();
            ResumeLayout(false);
            PerformLayout();
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
        private System.Windows.Forms.CheckBox checkBoxIgnoreLoopback;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox textBoxIgnoreComid;
        private System.Windows.Forms.ToolStripMenuItem exportSVGSequenceDiagramToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exportXLSXToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exportCSVToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem eVA2XMLExportToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem reportAnIssueToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem remoteCaptureToolStripMenuItem;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private System.Windows.Forms.Label label2;
        private TypeDelayTextBox textBoxIgnoreVars;
        private TypeDelayTextBox textBoxSearch;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ToolStripMenuItem testToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem captureToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem startToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem stopToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem restartToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem clearToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem windowToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem alwaysOnTopToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem quitToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ErrorProvider errorProvider1;
        private System.Windows.Forms.ToolStripSplitButton toolStripSplitButtonOpenLog;
        private System.Windows.Forms.ToolStripSplitButton toolStripSplitButtonCopyLog;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabelSeparator1;
    }
}

