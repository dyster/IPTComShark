namespace TrainShark.FileManager
{
    partial class FileOpener
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FileOpener));
            this.backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
            this.listBox1 = new System.Windows.Forms.ListBox();
            this.dataListView1 = new BrightIdeasSoftware.DataListView();
            this.olvColumnFileInfo = new BrightIdeasSoftware.OLVColumn();
            this.olvColumnStart = new BrightIdeasSoftware.OLVColumn();
            this.olvColumnEndTime = new BrightIdeasSoftware.OLVColumn();
            this.olvColumnSource = new BrightIdeasSoftware.OLVColumn();
            this.olvColumnPackets = new BrightIdeasSoftware.OLVColumn();
            this.olvColumnArchiveKey = new BrightIdeasSoftware.OLVColumn();
            this.olvColumnUse = new BrightIdeasSoftware.OLVColumn();
            this.richTextBox1 = new System.Windows.Forms.RichTextBox();
            this.datePicker1 = new TrainShark.FileManager.DatePicker();
            this.buttonGO = new System.Windows.Forms.Button();
            this.buttonMerge = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.textBoxIncludeIP = new System.Windows.Forms.TextBox();
            this.textBoxExcludeIP = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dataListView1)).BeginInit();
            this.SuspendLayout();
            // 
            // backgroundWorker1
            // 
            this.backgroundWorker1.DoWork += new System.ComponentModel.DoWorkEventHandler(this.BackgroundWorker1_DoWork);
            this.backgroundWorker1.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.BackgroundWorker1_RunWorkerCompleted);
            // 
            // listBox1
            // 
            this.listBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.listBox1.FormattingEnabled = true;
            this.listBox1.ItemHeight = 15;
            this.listBox1.Location = new System.Drawing.Point(0, 582);
            this.listBox1.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.listBox1.Name = "listBox1";
            this.listBox1.Size = new System.Drawing.Size(1120, 139);
            this.listBox1.TabIndex = 0;
            // 
            // dataListView1
            // 
            this.dataListView1.AllColumns.Add(this.olvColumnFileInfo);
            this.dataListView1.AllColumns.Add(this.olvColumnStart);
            this.dataListView1.AllColumns.Add(this.olvColumnEndTime);
            this.dataListView1.AllColumns.Add(this.olvColumnSource);
            this.dataListView1.AllColumns.Add(this.olvColumnPackets);
            this.dataListView1.AllColumns.Add(this.olvColumnArchiveKey);
            this.dataListView1.AllColumns.Add(this.olvColumnUse);
            this.dataListView1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dataListView1.CellEditActivation = BrightIdeasSoftware.ObjectListView.CellEditActivateMode.SingleClick;
            this.dataListView1.CheckBoxes = true;
            this.dataListView1.CheckedAspectName = "Use";
            this.dataListView1.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.olvColumnFileInfo,
            this.olvColumnStart,
            this.olvColumnEndTime,
            this.olvColumnSource,
            this.olvColumnPackets,
            this.olvColumnArchiveKey,
            this.olvColumnUse});
            this.dataListView1.Cursor = System.Windows.Forms.Cursors.Default;
            this.dataListView1.DataSource = null;
            this.dataListView1.HideSelection = false;
            this.dataListView1.Location = new System.Drawing.Point(0, 280);
            this.dataListView1.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.dataListView1.Name = "dataListView1";
            this.dataListView1.ShowGroups = false;
            this.dataListView1.Size = new System.Drawing.Size(1120, 300);
            this.dataListView1.TabIndex = 1;
            this.dataListView1.UseCompatibleStateImageBehavior = false;
            this.dataListView1.View = System.Windows.Forms.View.Details;
            // 
            // olvColumnFileInfo
            // 
            this.olvColumnFileInfo.AspectName = "FileInfo";
            this.olvColumnFileInfo.IsEditable = false;
            this.olvColumnFileInfo.Name = "olvColumnFileInfo";
            this.olvColumnFileInfo.Text = "File Info";
            this.olvColumnFileInfo.Width = 150;
            // 
            // olvColumnStart
            // 
            this.olvColumnStart.AspectName = "StartTime";
            this.olvColumnStart.IsEditable = false;
            this.olvColumnStart.Name = "olvColumnStart";
            this.olvColumnStart.Text = "Start Time";
            this.olvColumnStart.Width = 120;
            // 
            // olvColumnEndTime
            // 
            this.olvColumnEndTime.AspectName = "EndTime";
            this.olvColumnEndTime.IsEditable = false;
            this.olvColumnEndTime.Name = "olvColumnEndTime";
            this.olvColumnEndTime.Text = "End Time";
            this.olvColumnEndTime.Width = 120;
            // 
            // olvColumnSource
            // 
            this.olvColumnSource.AspectName = "SourceType";
            this.olvColumnSource.IsEditable = false;
            this.olvColumnSource.Name = "olvColumnSource";
            this.olvColumnSource.Text = "Source Type";
            this.olvColumnSource.Width = 80;
            // 
            // olvColumnPackets
            // 
            this.olvColumnPackets.AspectName = "Packets";
            this.olvColumnPackets.IsEditable = false;
            this.olvColumnPackets.Name = "olvColumnPackets";
            this.olvColumnPackets.Text = "Packets";
            // 
            // olvColumnArchiveKey
            // 
            this.olvColumnArchiveKey.AspectName = "ArchiveKey";
            this.olvColumnArchiveKey.IsEditable = false;
            this.olvColumnArchiveKey.Name = "olvColumnArchiveKey";
            this.olvColumnArchiveKey.Text = "Archive Key";
            this.olvColumnArchiveKey.Width = 200;
            // 
            // olvColumnUse
            // 
            this.olvColumnUse.AspectName = "Use";
            this.olvColumnUse.Name = "olvColumnUse";
            this.olvColumnUse.Text = "Use";
            this.olvColumnUse.Width = 0;
            // 
            // richTextBox1
            // 
            this.richTextBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.richTextBox1.BackColor = System.Drawing.SystemColors.Control;
            this.richTextBox1.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.richTextBox1.Location = new System.Drawing.Point(14, 14);
            this.richTextBox1.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.richTextBox1.Name = "richTextBox1";
            this.richTextBox1.ReadOnly = true;
            this.richTextBox1.Size = new System.Drawing.Size(1093, 52);
            this.richTextBox1.TabIndex = 4;
            this.richTextBox1.Text = resources.GetString("richTextBox1.Text");
            // 
            // datePicker1
            // 
            this.datePicker1.Location = new System.Drawing.Point(14, 57);
            this.datePicker1.Margin = new System.Windows.Forms.Padding(5, 3, 5, 3);
            this.datePicker1.Name = "datePicker1";
            this.datePicker1.Size = new System.Drawing.Size(723, 104);
            this.datePicker1.TabIndex = 2;
            // 
            // buttonGO
            // 
            this.buttonGO.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonGO.Enabled = false;
            this.buttonGO.Location = new System.Drawing.Point(985, 134);
            this.buttonGO.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.buttonGO.Name = "buttonGO";
            this.buttonGO.Size = new System.Drawing.Size(122, 27);
            this.buttonGO.TabIndex = 5;
            this.buttonGO.Text = "Parse Selection";
            this.buttonGO.UseVisualStyleBackColor = true;
            this.buttonGO.Click += new System.EventHandler(this.ButtonGO_Click);
            // 
            // buttonMerge
            // 
            this.buttonMerge.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonMerge.Enabled = false;
            this.buttonMerge.Location = new System.Drawing.Point(985, 100);
            this.buttonMerge.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.buttonMerge.Name = "buttonMerge";
            this.buttonMerge.Size = new System.Drawing.Size(122, 27);
            this.buttonMerge.TabIndex = 6;
            this.buttonMerge.Text = "Merge to new file";
            this.buttonMerge.UseVisualStyleBackColor = true;
            this.buttonMerge.Click += new System.EventHandler(this.ButtonMerge_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(14, 210);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(67, 15);
            this.label1.TabIndex = 7;
            this.label1.Text = "Include IP\'s";
            // 
            // textBoxIncludeIP
            // 
            this.textBoxIncludeIP.Location = new System.Drawing.Point(93, 207);
            this.textBoxIncludeIP.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.textBoxIncludeIP.Name = "textBoxIncludeIP";
            this.textBoxIncludeIP.Size = new System.Drawing.Size(611, 23);
            this.textBoxIncludeIP.TabIndex = 9;
            // 
            // textBoxExcludeIP
            // 
            this.textBoxExcludeIP.Location = new System.Drawing.Point(93, 236);
            this.textBoxExcludeIP.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.textBoxExcludeIP.Name = "textBoxExcludeIP";
            this.textBoxExcludeIP.Size = new System.Drawing.Size(611, 23);
            this.textBoxExcludeIP.TabIndex = 11;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(14, 239);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(69, 15);
            this.label2.TabIndex = 10;
            this.label2.Text = "Exclude IP\'s";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(294, 262);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(204, 15);
            this.label3.TabIndex = 12;
            this.label3.Text = "separate multiple values with comma";
            // 
            // FileOpener
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1121, 720);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.textBoxExcludeIP);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.textBoxIncludeIP);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.buttonMerge);
            this.Controls.Add(this.buttonGO);
            this.Controls.Add(this.richTextBox1);
            this.Controls.Add(this.datePicker1);
            this.Controls.Add(this.dataListView1);
            this.Controls.Add(this.listBox1);
            this.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.Name = "FileOpener";
            this.Text = "FileOpener";
            ((System.ComponentModel.ISupportInitialize)(this.dataListView1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.ComponentModel.BackgroundWorker backgroundWorker1;
        private System.Windows.Forms.ListBox listBox1;
        private BrightIdeasSoftware.DataListView dataListView1;
        private BrightIdeasSoftware.OLVColumn olvColumnFileInfo;
        private BrightIdeasSoftware.OLVColumn olvColumnStart;
        private BrightIdeasSoftware.OLVColumn olvColumnEndTime;
        private BrightIdeasSoftware.OLVColumn olvColumnSource;
        private BrightIdeasSoftware.OLVColumn olvColumnPackets;
        private BrightIdeasSoftware.OLVColumn olvColumnArchiveKey;
        private BrightIdeasSoftware.OLVColumn olvColumnUse;
        private DatePicker datePicker1;
        private System.Windows.Forms.RichTextBox richTextBox1;
        private System.Windows.Forms.Button buttonGO;
        private System.Windows.Forms.Button buttonMerge;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox textBoxIncludeIP;
        private System.Windows.Forms.TextBox textBoxExcludeIP;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
    }
}