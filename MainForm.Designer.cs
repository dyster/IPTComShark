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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.buttonStart = new System.Windows.Forms.Button();
            this.buttonStop = new System.Windows.Forms.Button();
            this.backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
            this.timerFlicker = new System.Windows.Forms.Timer(this.components);
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.statusLeft = new System.Windows.Forms.ToolStripStatusLabel();
            this.statusRight = new System.Windows.Forms.ToolStripStatusLabel();
            this.buttonSaveFiltered = new System.Windows.Forms.Button();
            this.buttonOpen = new System.Windows.Forms.Button();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.packetListView1 = new IPTComShark.PacketListView();
            this.packetDisplay1 = new IPTComShark.PacketDisplay();
            this.checkBoxParserOnly = new System.Windows.Forms.CheckBox();
            this.checkBoxHideDupes = new System.Windows.Forms.CheckBox();
            this.checkBoxAutoScroll = new System.Windows.Forms.CheckBox();
            this.buttonSaveAll = new System.Windows.Forms.Button();
            this.buttonRestart = new System.Windows.Forms.Button();
            this.buttonCleanPcap = new System.Windows.Forms.Button();
            this.buttonSimulate = new System.Windows.Forms.Button();
            this.statusStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.SuspendLayout();
            // 
            // buttonStart
            // 
            this.buttonStart.Location = new System.Drawing.Point(12, 12);
            this.buttonStart.Name = "buttonStart";
            this.buttonStart.Size = new System.Drawing.Size(75, 23);
            this.buttonStart.TabIndex = 1;
            this.buttonStart.Text = "Start";
            this.buttonStart.UseVisualStyleBackColor = true;
            this.buttonStart.Click += new System.EventHandler(this.buttonStart_Click);
            // 
            // buttonStop
            // 
            this.buttonStop.Enabled = false;
            this.buttonStop.Location = new System.Drawing.Point(93, 12);
            this.buttonStop.Name = "buttonStop";
            this.buttonStop.Size = new System.Drawing.Size(75, 23);
            this.buttonStop.TabIndex = 2;
            this.buttonStop.Text = "Stop";
            this.buttonStop.UseVisualStyleBackColor = true;
            this.buttonStop.Click += new System.EventHandler(this.buttonStop_Click);
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
            // buttonSaveFiltered
            // 
            this.buttonSaveFiltered.Location = new System.Drawing.Point(336, 12);
            this.buttonSaveFiltered.Name = "buttonSaveFiltered";
            this.buttonSaveFiltered.Size = new System.Drawing.Size(81, 23);
            this.buttonSaveFiltered.TabIndex = 5;
            this.buttonSaveFiltered.Text = "Save Filtered";
            this.buttonSaveFiltered.UseVisualStyleBackColor = true;
            this.buttonSaveFiltered.Click += new System.EventHandler(this.buttonSave_Click);
            // 
            // buttonOpen
            // 
            this.buttonOpen.Location = new System.Drawing.Point(423, 12);
            this.buttonOpen.Name = "buttonOpen";
            this.buttonOpen.Size = new System.Drawing.Size(75, 23);
            this.buttonOpen.TabIndex = 6;
            this.buttonOpen.Text = "Open pcap";
            this.buttonOpen.UseVisualStyleBackColor = true;
            this.buttonOpen.Click += new System.EventHandler(this.buttonTest_Click);
            // 
            // splitContainer1
            // 
            this.splitContainer1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.splitContainer1.Location = new System.Drawing.Point(0, 42);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.packetListView1);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.packetDisplay1);
            this.splitContainer1.Size = new System.Drawing.Size(1433, 530);
            this.splitContainer1.SplitterDistance = 1030;
            this.splitContainer1.TabIndex = 7;
            // 
            // packetListView1
            // 
            this.packetListView1.AutoScroll = true;
            this.packetListView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.packetListView1.Location = new System.Drawing.Point(0, 0);
            this.packetListView1.Name = "packetListView1";
            this.packetListView1.Size = new System.Drawing.Size(1030, 530);
            this.packetListView1.TabIndex = 0;
            // 
            // packetDisplay1
            // 
            this.packetDisplay1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.packetDisplay1.IptConfigReader = null;
            this.packetDisplay1.Location = new System.Drawing.Point(0, 0);
            this.packetDisplay1.Name = "packetDisplay1";
            this.packetDisplay1.Size = new System.Drawing.Size(399, 530);
            this.packetDisplay1.TabIndex = 0;
            // 
            // checkBoxParserOnly
            // 
            this.checkBoxParserOnly.AutoSize = true;
            this.checkBoxParserOnly.Checked = true;
            this.checkBoxParserOnly.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBoxParserOnly.Location = new System.Drawing.Point(504, 16);
            this.checkBoxParserOnly.Name = "checkBoxParserOnly";
            this.checkBoxParserOnly.Size = new System.Drawing.Size(102, 17);
            this.checkBoxParserOnly.TabIndex = 8;
            this.checkBoxParserOnly.Text = "Show VSIS only";
            this.checkBoxParserOnly.UseVisualStyleBackColor = true;
            // 
            // checkBoxHideDupes
            // 
            this.checkBoxHideDupes.AutoSize = true;
            this.checkBoxHideDupes.Checked = true;
            this.checkBoxHideDupes.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBoxHideDupes.Location = new System.Drawing.Point(612, 16);
            this.checkBoxHideDupes.Name = "checkBoxHideDupes";
            this.checkBoxHideDupes.Size = new System.Drawing.Size(166, 17);
            this.checkBoxHideDupes.TabIndex = 9;
            this.checkBoxHideDupes.Text = "Hide Duplicated ProcessData";
            this.checkBoxHideDupes.UseVisualStyleBackColor = true;
            // 
            // checkBoxAutoScroll
            // 
            this.checkBoxAutoScroll.AutoSize = true;
            this.checkBoxAutoScroll.Checked = true;
            this.checkBoxAutoScroll.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBoxAutoScroll.Location = new System.Drawing.Point(784, 16);
            this.checkBoxAutoScroll.Name = "checkBoxAutoScroll";
            this.checkBoxAutoScroll.Size = new System.Drawing.Size(74, 17);
            this.checkBoxAutoScroll.TabIndex = 10;
            this.checkBoxAutoScroll.Text = "AutoScroll";
            this.checkBoxAutoScroll.UseVisualStyleBackColor = true;
            this.checkBoxAutoScroll.CheckedChanged += new System.EventHandler(this.checkBoxAutoScroll_CheckedChanged);
            // 
            // buttonSaveAll
            // 
            this.buttonSaveAll.Location = new System.Drawing.Point(255, 12);
            this.buttonSaveAll.Name = "buttonSaveAll";
            this.buttonSaveAll.Size = new System.Drawing.Size(75, 23);
            this.buttonSaveAll.TabIndex = 11;
            this.buttonSaveAll.Text = "Save All";
            this.buttonSaveAll.UseVisualStyleBackColor = true;
            this.buttonSaveAll.Click += new System.EventHandler(this.buttonSaveAll_Click);
            // 
            // buttonRestart
            // 
            this.buttonRestart.Location = new System.Drawing.Point(174, 12);
            this.buttonRestart.Name = "buttonRestart";
            this.buttonRestart.Size = new System.Drawing.Size(75, 23);
            this.buttonRestart.TabIndex = 12;
            this.buttonRestart.Text = "Restart";
            this.buttonRestart.UseVisualStyleBackColor = true;
            this.buttonRestart.Click += new System.EventHandler(this.buttonRestart_Click);
            // 
            // buttonCleanPcap
            // 
            this.buttonCleanPcap.Location = new System.Drawing.Point(864, 12);
            this.buttonCleanPcap.Name = "buttonCleanPcap";
            this.buttonCleanPcap.Size = new System.Drawing.Size(150, 23);
            this.buttonCleanPcap.TabIndex = 13;
            this.buttonCleanPcap.Text = "Cleanup PCAP file";
            this.buttonCleanPcap.UseVisualStyleBackColor = true;
            this.buttonCleanPcap.Click += new System.EventHandler(this.buttonCleanPcap_Click);
            // 
            // buttonSimulate
            // 
            this.buttonSimulate.Location = new System.Drawing.Point(1020, 12);
            this.buttonSimulate.Name = "buttonSimulate";
            this.buttonSimulate.Size = new System.Drawing.Size(75, 23);
            this.buttonSimulate.TabIndex = 14;
            this.buttonSimulate.Text = "Simulate";
            this.buttonSimulate.UseVisualStyleBackColor = true;
            this.buttonSimulate.Click += new System.EventHandler(this.buttonSimulate_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1433, 597);
            this.Controls.Add(this.buttonSimulate);
            this.Controls.Add(this.buttonCleanPcap);
            this.Controls.Add(this.buttonRestart);
            this.Controls.Add(this.buttonSaveAll);
            this.Controls.Add(this.checkBoxAutoScroll);
            this.Controls.Add(this.checkBoxHideDupes);
            this.Controls.Add(this.checkBoxParserOnly);
            this.Controls.Add(this.splitContainer1);
            this.Controls.Add(this.buttonOpen);
            this.Controls.Add(this.buttonSaveFiltered);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.buttonStop);
            this.Controls.Add(this.buttonStart);
            this.DoubleBuffered = true;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "MainForm";
            this.Text = "IPTComShark";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button buttonStart;
        private System.Windows.Forms.Button buttonStop;
        private System.ComponentModel.BackgroundWorker backgroundWorker1;
        private System.Windows.Forms.Timer timerFlicker;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel statusLeft;
        private System.Windows.Forms.ToolStripStatusLabel statusRight;
        private System.Windows.Forms.Button buttonSaveFiltered;
        private System.Windows.Forms.Button buttonOpen;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.CheckBox checkBoxParserOnly;
        private System.Windows.Forms.CheckBox checkBoxHideDupes;
        private System.Windows.Forms.CheckBox checkBoxAutoScroll;
        private System.Windows.Forms.Button buttonSaveAll;
        private System.Windows.Forms.Button buttonRestart;
        private PacketDisplay packetDisplay1;
        private PacketListView packetListView1;
        private System.Windows.Forms.Button buttonCleanPcap;
        private System.Windows.Forms.Button buttonSimulate;
    }
}

