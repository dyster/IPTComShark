namespace IPTComShark.Controls
{
    partial class PacketDisplay
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.dataListViewRight = new BrightIdeasSoftware.DataListView();
            this.olvColumnDataLineName = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.olvColumnDataLineType = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.olvColumnDataLineValue = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.olvColumnComment = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.olvColumnChanged = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.textBoxComid = new System.Windows.Forms.TextBox();
            this.textBoxSize = new System.Windows.Forms.TextBox();
            this.textBoxType = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.textBoxRAW = new System.Windows.Forms.TextBox();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.richTextBox1 = new System.Windows.Forms.RichTextBox();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.analyzeValueToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            ((System.ComponentModel.ISupportInitialize)(this.dataListViewRight)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.contextMenuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // dataListViewRight
            // 
            this.dataListViewRight.AllColumns.Add(this.olvColumnDataLineName);
            this.dataListViewRight.AllColumns.Add(this.olvColumnDataLineType);
            this.dataListViewRight.AllColumns.Add(this.olvColumnDataLineValue);
            this.dataListViewRight.AllColumns.Add(this.olvColumnComment);
            this.dataListViewRight.AllColumns.Add(this.olvColumnChanged);
            this.dataListViewRight.CellEditUseWholeCell = false;
            this.dataListViewRight.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.olvColumnDataLineName,
            this.olvColumnDataLineType,
            this.olvColumnDataLineValue,
            this.olvColumnComment,
            this.olvColumnChanged});
            this.dataListViewRight.ContextMenuStrip = this.contextMenuStrip1;
            this.dataListViewRight.Cursor = System.Windows.Forms.Cursors.Default;
            this.dataListViewRight.DataSource = null;
            this.dataListViewRight.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataListViewRight.HideSelection = false;
            this.dataListViewRight.Location = new System.Drawing.Point(0, 0);
            this.dataListViewRight.Name = "dataListViewRight";
            this.dataListViewRight.ShowGroups = false;
            this.dataListViewRight.Size = new System.Drawing.Size(314, 223);
            this.dataListViewRight.TabIndex = 17;
            this.dataListViewRight.UseCompatibleStateImageBehavior = false;
            this.dataListViewRight.View = System.Windows.Forms.View.Details;
            // 
            // olvColumnDataLineName
            // 
            this.olvColumnDataLineName.AspectName = "Name";
            this.olvColumnDataLineName.Text = "Name";
            this.olvColumnDataLineName.Width = 100;
            // 
            // olvColumnDataLineType
            // 
            this.olvColumnDataLineType.AspectName = "Type";
            this.olvColumnDataLineType.Text = "Type";
            this.olvColumnDataLineType.Width = 50;
            // 
            // olvColumnDataLineValue
            // 
            this.olvColumnDataLineValue.AspectName = "Value";
            this.olvColumnDataLineValue.Text = "Value";
            this.olvColumnDataLineValue.Width = 100;
            // 
            // olvColumnComment
            // 
            this.olvColumnComment.AspectName = "Comment";
            this.olvColumnComment.Text = "Comment";
            this.olvColumnComment.Width = 200;
            // 
            // olvColumnChanged
            // 
            this.olvColumnChanged.AspectName = "Changed";
            this.olvColumnChanged.Text = "Changed";
            this.olvColumnChanged.Width = 0;
            // 
            // textBoxComid
            // 
            this.textBoxComid.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxComid.Location = new System.Drawing.Point(42, 26);
            this.textBoxComid.Name = "textBoxComid";
            this.textBoxComid.Size = new System.Drawing.Size(272, 20);
            this.textBoxComid.TabIndex = 16;
            // 
            // textBoxSize
            // 
            this.textBoxSize.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxSize.Location = new System.Drawing.Point(42, 52);
            this.textBoxSize.Name = "textBoxSize";
            this.textBoxSize.Size = new System.Drawing.Size(272, 20);
            this.textBoxSize.TabIndex = 15;
            // 
            // textBoxType
            // 
            this.textBoxType.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxType.Location = new System.Drawing.Point(42, 78);
            this.textBoxType.Name = "textBoxType";
            this.textBoxType.Size = new System.Drawing.Size(272, 20);
            this.textBoxType.TabIndex = 14;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(3, 81);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(31, 13);
            this.label4.TabIndex = 13;
            this.label4.Text = "Type";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(3, 55);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(27, 13);
            this.label3.TabIndex = 12;
            this.label3.Text = "Size";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(3, 29);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(36, 13);
            this.label2.TabIndex = 11;
            this.label2.Text = "Comid";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(3, 3);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(33, 13);
            this.label1.TabIndex = 10;
            this.label1.Text = "RAW";
            // 
            // textBoxRAW
            // 
            this.textBoxRAW.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxRAW.Location = new System.Drawing.Point(42, 0);
            this.textBoxRAW.Name = "textBoxRAW";
            this.textBoxRAW.Size = new System.Drawing.Size(272, 20);
            this.textBoxRAW.TabIndex = 9;
            // 
            // splitContainer1
            // 
            this.splitContainer1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.splitContainer1.Location = new System.Drawing.Point(0, 104);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.dataListViewRight);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.richTextBox1);
            this.splitContainer1.Size = new System.Drawing.Size(314, 302);
            this.splitContainer1.SplitterDistance = 223;
            this.splitContainer1.TabIndex = 18;
            // 
            // richTextBox1
            // 
            this.richTextBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.richTextBox1.Font = new System.Drawing.Font("Monospac821 BT", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.richTextBox1.Location = new System.Drawing.Point(0, 0);
            this.richTextBox1.Name = "richTextBox1";
            this.richTextBox1.Size = new System.Drawing.Size(314, 75);
            this.richTextBox1.TabIndex = 0;
            this.richTextBox1.Text = "";
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.analyzeValueToolStripMenuItem});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(181, 48);
            // 
            // analyzeValueToolStripMenuItem
            // 
            this.analyzeValueToolStripMenuItem.Name = "analyzeValueToolStripMenuItem";
            this.analyzeValueToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.analyzeValueToolStripMenuItem.Text = "Analyze value";
            this.analyzeValueToolStripMenuItem.Click += new System.EventHandler(this.analyzeValueToolStripMenuItem_Click);
            // 
            // PacketDisplay
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.splitContainer1);
            this.Controls.Add(this.textBoxComid);
            this.Controls.Add(this.textBoxSize);
            this.Controls.Add(this.textBoxType);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.textBoxRAW);
            this.DoubleBuffered = true;
            this.Name = "PacketDisplay";
            this.Size = new System.Drawing.Size(314, 406);
            ((System.ComponentModel.ISupportInitialize)(this.dataListViewRight)).EndInit();
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.contextMenuStrip1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private BrightIdeasSoftware.DataListView dataListViewRight;
        private BrightIdeasSoftware.OLVColumn olvColumnDataLineName;
        private BrightIdeasSoftware.OLVColumn olvColumnDataLineType;
        private BrightIdeasSoftware.OLVColumn olvColumnDataLineValue;
        private System.Windows.Forms.TextBox textBoxComid;
        private System.Windows.Forms.TextBox textBoxSize;
        private System.Windows.Forms.TextBox textBoxType;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox textBoxRAW;
        private BrightIdeasSoftware.OLVColumn olvColumnComment;
        private BrightIdeasSoftware.OLVColumn olvColumnChanged;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.RichTextBox richTextBox1;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem analyzeValueToolStripMenuItem;
    }
}
