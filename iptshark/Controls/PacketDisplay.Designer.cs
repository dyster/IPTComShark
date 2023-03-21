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
            this.olvColumnNo = new BrightIdeasSoftware.OLVColumn();
            this.olvColumnDataLineName = new BrightIdeasSoftware.OLVColumn();
            this.olvColumnDataLineValue = new BrightIdeasSoftware.OLVColumn();
            this.olvColumnDataLineType = new BrightIdeasSoftware.OLVColumn();
            this.olvColumnTrueValue = new BrightIdeasSoftware.OLVColumn();
            this.olvColumnComment = new BrightIdeasSoftware.OLVColumn();
            this.olvColumnChanged = new BrightIdeasSoftware.OLVColumn();
            this.olvColumnIsCategory = new BrightIdeasSoftware.OLVColumn();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.analyzeValueToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.copyValueToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.copyAllValuesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
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
            ((System.ComponentModel.ISupportInitialize)(this.dataListViewRight)).BeginInit();
            this.contextMenuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.SuspendLayout();
            // 
            // dataListViewRight
            // 
            this.dataListViewRight.AllColumns.Add(this.olvColumnNo);
            this.dataListViewRight.AllColumns.Add(this.olvColumnDataLineName);
            this.dataListViewRight.AllColumns.Add(this.olvColumnDataLineValue);
            this.dataListViewRight.AllColumns.Add(this.olvColumnDataLineType);
            this.dataListViewRight.AllColumns.Add(this.olvColumnTrueValue);
            this.dataListViewRight.AllColumns.Add(this.olvColumnComment);
            this.dataListViewRight.AllColumns.Add(this.olvColumnChanged);
            this.dataListViewRight.AllColumns.Add(this.olvColumnIsCategory);
            this.dataListViewRight.CellEditUseWholeCell = false;
            this.dataListViewRight.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.olvColumnNo,
            this.olvColumnDataLineName,
            this.olvColumnDataLineValue,
            this.olvColumnDataLineType,
            this.olvColumnTrueValue,
            this.olvColumnComment});
            this.dataListViewRight.ContextMenuStrip = this.contextMenuStrip1;
            this.dataListViewRight.DataSource = null;
            this.dataListViewRight.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataListViewRight.Location = new System.Drawing.Point(0, 0);
            this.dataListViewRight.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.dataListViewRight.Name = "dataListViewRight";
            this.dataListViewRight.ShowGroups = false;
            this.dataListViewRight.Size = new System.Drawing.Size(880, 405);
            this.dataListViewRight.TabIndex = 17;
            this.dataListViewRight.UseCompatibleStateImageBehavior = false;
            this.dataListViewRight.View = System.Windows.Forms.View.Details;
            // 
            // olvColumnNo
            // 
            this.olvColumnNo.AspectName = "No";
            this.olvColumnNo.Name = "olvColumnNo";
            this.olvColumnNo.Text = "No";
            this.olvColumnNo.Width = 30;
            // 
            // olvColumnDataLineName
            // 
            this.olvColumnDataLineName.AspectName = "Name";
            this.olvColumnDataLineName.Name = "olvColumnDataLineName";
            this.olvColumnDataLineName.Text = "Name";
            this.olvColumnDataLineName.Width = 180;
            // 
            // olvColumnDataLineValue
            // 
            this.olvColumnDataLineValue.AspectName = "Value";
            this.olvColumnDataLineValue.Name = "olvColumnDataLineValue";
            this.olvColumnDataLineValue.Text = "Value";
            this.olvColumnDataLineValue.Width = 150;
            // 
            // olvColumnDataLineType
            // 
            this.olvColumnDataLineType.AspectName = "Type";
            this.olvColumnDataLineType.IsVisible = false;
            this.olvColumnDataLineType.Name = "olvColumnDataLineType";
            this.olvColumnDataLineType.Text = "Type";
            this.olvColumnDataLineType.Width = 64;
            // 
            // olvColumnTrueValue
            // 
            this.olvColumnTrueValue.AspectName = "TrueValue";
            this.olvColumnTrueValue.Name = "olvColumnTrueValue";
            this.olvColumnTrueValue.Text = "True Value";
            this.olvColumnTrueValue.Width = 100;
            // 
            // olvColumnComment
            // 
            this.olvColumnComment.AspectName = "Comment";
            this.olvColumnComment.Name = "olvColumnComment";
            this.olvColumnComment.Text = "Comment";
            this.olvColumnComment.Width = 200;
            // 
            // olvColumnChanged
            // 
            this.olvColumnChanged.AspectName = "Changed";
            this.olvColumnChanged.DisplayIndex = 5;
            this.olvColumnChanged.IsVisible = false;
            this.olvColumnChanged.Name = "olvColumnChanged";
            this.olvColumnChanged.Text = "Changed";
            this.olvColumnChanged.Width = 0;
            // 
            // olvColumnIsCategory
            // 
            this.olvColumnIsCategory.AspectName = "IsCategory";
            this.olvColumnIsCategory.DisplayIndex = 6;
            this.olvColumnIsCategory.IsVisible = false;
            this.olvColumnIsCategory.Name = "olvColumnIsCategory";
            this.olvColumnIsCategory.Text = "IsCategory";
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.analyzeValueToolStripMenuItem, this.copyValueToolStripMenuItem, this.copyAllValuesToolStripMenuItem});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(147, 26);
            // 
            // analyzeValueToolStripMenuItem
            // 
            this.analyzeValueToolStripMenuItem.Name = "analyzeValueToolStripMenuItem";
            this.analyzeValueToolStripMenuItem.Size = new System.Drawing.Size(146, 22);
            this.analyzeValueToolStripMenuItem.Text = "Analyze value";
            this.analyzeValueToolStripMenuItem.Click += new System.EventHandler(this.analyzeValueToolStripMenuItem_Click);
            // 
            // copyValueToolStripMenuItem
            // 
            this.copyValueToolStripMenuItem.Name = "copyValueToolStripMenuItem";
            this.copyValueToolStripMenuItem.Size = new System.Drawing.Size(146, 22);
            this.copyValueToolStripMenuItem.Text = "Copy value";
            this.copyValueToolStripMenuItem.Click += new System.EventHandler(this.copyValueToolStripMenuItem_Click);
            // 
            // copyAllValuesToolStripMenuItem
            // 
            this.copyAllValuesToolStripMenuItem.Name = "copyAllValuesToolStripMenuItem";
            this.copyAllValuesToolStripMenuItem.Size = new System.Drawing.Size(146, 22);
            this.copyAllValuesToolStripMenuItem.Text = "Copy whole line";
            this.copyAllValuesToolStripMenuItem.Click += new System.EventHandler(this.copyAllValuesToolStripMenuItem_Click);
            // 
            // textBoxComid
            // 
            this.textBoxComid.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxComid.Location = new System.Drawing.Point(49, 30);
            this.textBoxComid.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.textBoxComid.Name = "textBoxComid";
            this.textBoxComid.Size = new System.Drawing.Size(831, 23);
            this.textBoxComid.TabIndex = 16;
            // 
            // textBoxSize
            // 
            this.textBoxSize.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxSize.Location = new System.Drawing.Point(49, 60);
            this.textBoxSize.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.textBoxSize.Name = "textBoxSize";
            this.textBoxSize.Size = new System.Drawing.Size(831, 23);
            this.textBoxSize.TabIndex = 15;
            // 
            // textBoxType
            // 
            this.textBoxType.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxType.Location = new System.Drawing.Point(49, 90);
            this.textBoxType.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.textBoxType.Name = "textBoxType";
            this.textBoxType.Size = new System.Drawing.Size(831, 23);
            this.textBoxType.TabIndex = 14;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(4, 93);
            this.label4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(31, 15);
            this.label4.TabIndex = 13;
            this.label4.Text = "Type";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(4, 63);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(27, 15);
            this.label3.TabIndex = 12;
            this.label3.Text = "Size";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(4, 33);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(43, 15);
            this.label2.TabIndex = 11;
            this.label2.Text = "Comid";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(4, 3);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(32, 15);
            this.label1.TabIndex = 10;
            this.label1.Text = "RAW";
            // 
            // textBoxRAW
            // 
            this.textBoxRAW.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxRAW.Location = new System.Drawing.Point(49, 0);
            this.textBoxRAW.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.textBoxRAW.Name = "textBoxRAW";
            this.textBoxRAW.Size = new System.Drawing.Size(831, 23);
            this.textBoxRAW.TabIndex = 9;
            // 
            // splitContainer1
            // 
            this.splitContainer1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.splitContainer1.Location = new System.Drawing.Point(0, 120);
            this.splitContainer1.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
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
            this.splitContainer1.Size = new System.Drawing.Size(880, 551);
            this.splitContainer1.SplitterDistance = 405;
            this.splitContainer1.SplitterWidth = 5;
            this.splitContainer1.TabIndex = 18;
            // 
            // richTextBox1
            // 
            this.richTextBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.richTextBox1.Font = new System.Drawing.Font("Courier New", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.richTextBox1.Location = new System.Drawing.Point(0, 0);
            this.richTextBox1.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.richTextBox1.Name = "richTextBox1";
            this.richTextBox1.Size = new System.Drawing.Size(880, 141);
            this.richTextBox1.TabIndex = 0;
            this.richTextBox1.Text = "";
            // 
            // PacketDisplay
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
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
            this.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.Name = "PacketDisplay";
            this.Size = new System.Drawing.Size(880, 671);
            ((System.ComponentModel.ISupportInitialize)(this.dataListViewRight)).EndInit();
            this.contextMenuStrip1.ResumeLayout(false);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
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
        private System.Windows.Forms.ToolStripMenuItem copyValueToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem copyAllValuesToolStripMenuItem;
        private BrightIdeasSoftware.OLVColumn olvColumnNo;
        private BrightIdeasSoftware.OLVColumn olvColumnTrueValue;
        private BrightIdeasSoftware.OLVColumn olvColumnIsCategory;
    }
}
