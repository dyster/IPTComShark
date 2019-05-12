namespace IPTComShark.FileManager
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
            this.olvColumnFileInfo = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.olvColumnStart = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.olvColumnEndTime = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.olvColumnSource = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.olvColumnPackets = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.olvColumnArchiveKey = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.olvColumnUse = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.richTextBox1 = new System.Windows.Forms.RichTextBox();
            this.datePicker1 = new IPTComShark.FileManager.DatePicker();
            this.buttonGO = new System.Windows.Forms.Button();
            this.buttonMerge = new System.Windows.Forms.Button();
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
            this.listBox1.Location = new System.Drawing.Point(-1, 491);
            this.listBox1.Name = "listBox1";
            this.listBox1.Size = new System.Drawing.Size(962, 134);
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
            this.dataListView1.Location = new System.Drawing.Point(-1, 145);
            this.dataListView1.Name = "dataListView1";
            this.dataListView1.ShowGroups = false;
            this.dataListView1.Size = new System.Drawing.Size(962, 340);
            this.dataListView1.TabIndex = 1;
            this.dataListView1.UseCompatibleStateImageBehavior = false;
            this.dataListView1.View = System.Windows.Forms.View.Details;
            // 
            // olvColumnFileInfo
            // 
            this.olvColumnFileInfo.AspectName = "FileInfo";
            this.olvColumnFileInfo.IsEditable = false;
            this.olvColumnFileInfo.Text = "File Info";
            this.olvColumnFileInfo.Width = 150;
            // 
            // olvColumnStart
            // 
            this.olvColumnStart.AspectName = "StartTime";
            this.olvColumnStart.IsEditable = false;
            this.olvColumnStart.Text = "Start Time";
            this.olvColumnStart.Width = 120;
            // 
            // olvColumnEndTime
            // 
            this.olvColumnEndTime.AspectName = "EndTime";
            this.olvColumnEndTime.IsEditable = false;
            this.olvColumnEndTime.Text = "End Time";
            this.olvColumnEndTime.Width = 120;
            // 
            // olvColumnSource
            // 
            this.olvColumnSource.AspectName = "SourceType";
            this.olvColumnSource.IsEditable = false;
            this.olvColumnSource.Text = "Source Type";
            this.olvColumnSource.Width = 80;
            // 
            // olvColumnPackets
            // 
            this.olvColumnPackets.AspectName = "Packets";
            this.olvColumnPackets.IsEditable = false;
            this.olvColumnPackets.Text = "Packets";
            // 
            // olvColumnArchiveKey
            // 
            this.olvColumnArchiveKey.AspectName = "ArchiveKey";
            this.olvColumnArchiveKey.IsEditable = false;
            this.olvColumnArchiveKey.Text = "Archive Key";
            this.olvColumnArchiveKey.Width = 200;
            // 
            // olvColumnUse
            // 
            this.olvColumnUse.AspectName = "Use";
            this.olvColumnUse.Text = "Use";
            this.olvColumnUse.Width = 0;
            // 
            // richTextBox1
            // 
            this.richTextBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.richTextBox1.BackColor = System.Drawing.SystemColors.Control;
            this.richTextBox1.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.richTextBox1.Location = new System.Drawing.Point(12, 12);
            this.richTextBox1.Name = "richTextBox1";
            this.richTextBox1.ReadOnly = true;
            this.richTextBox1.Size = new System.Drawing.Size(937, 45);
            this.richTextBox1.TabIndex = 4;
            this.richTextBox1.Text = resources.GetString("richTextBox1.Text");
            // 
            // datePicker1
            // 
            this.datePicker1.Location = new System.Drawing.Point(12, 49);
            this.datePicker1.Name = "datePicker1";
            this.datePicker1.Size = new System.Drawing.Size(620, 90);
            this.datePicker1.TabIndex = 2;
            // 
            // buttonGO
            // 
            this.buttonGO.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonGO.Enabled = false;
            this.buttonGO.Location = new System.Drawing.Point(844, 116);
            this.buttonGO.Name = "buttonGO";
            this.buttonGO.Size = new System.Drawing.Size(105, 23);
            this.buttonGO.TabIndex = 5;
            this.buttonGO.Text = "Parse Selection";
            this.buttonGO.UseVisualStyleBackColor = true;
            this.buttonGO.Click += new System.EventHandler(this.ButtonGO_Click);
            // 
            // buttonMerge
            // 
            this.buttonMerge.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonMerge.Enabled = false;
            this.buttonMerge.Location = new System.Drawing.Point(844, 87);
            this.buttonMerge.Name = "buttonMerge";
            this.buttonMerge.Size = new System.Drawing.Size(105, 23);
            this.buttonMerge.TabIndex = 6;
            this.buttonMerge.Text = "Merge to new file";
            this.buttonMerge.UseVisualStyleBackColor = true;
            this.buttonMerge.Click += new System.EventHandler(this.ButtonMerge_Click);
            // 
            // FileOpener
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(961, 624);
            this.Controls.Add(this.buttonMerge);
            this.Controls.Add(this.buttonGO);
            this.Controls.Add(this.richTextBox1);
            this.Controls.Add(this.datePicker1);
            this.Controls.Add(this.dataListView1);
            this.Controls.Add(this.listBox1);
            this.Name = "FileOpener";
            this.Text = "FileOpener";
            ((System.ComponentModel.ISupportInitialize)(this.dataListView1)).EndInit();
            this.ResumeLayout(false);

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
    }
}