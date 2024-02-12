namespace TrainShark.Controls
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
            components = new System.ComponentModel.Container();
            dataListViewRight = new BrightIdeasSoftware.DataListView();
            olvColumnNo = new BrightIdeasSoftware.OLVColumn();
            olvColumnDataLineName = new BrightIdeasSoftware.OLVColumn();
            olvColumnDataLineValue = new BrightIdeasSoftware.OLVColumn();
            olvColumnDataLineType = new BrightIdeasSoftware.OLVColumn();
            olvColumnTrueValue = new BrightIdeasSoftware.OLVColumn();
            olvColumnComment = new BrightIdeasSoftware.OLVColumn();
            olvColumnChanged = new BrightIdeasSoftware.OLVColumn();
            olvColumnIsCategory = new BrightIdeasSoftware.OLVColumn();
            contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(components);
            analyzeValueToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            copyValueToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            copyAllValuesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            splitContainer1 = new System.Windows.Forms.SplitContainer();
            richTextBox1 = new System.Windows.Forms.RichTextBox();
            propertyGrid1 = new System.Windows.Forms.PropertyGrid();
            ((System.ComponentModel.ISupportInitialize)dataListViewRight).BeginInit();
            contextMenuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)splitContainer1).BeginInit();
            splitContainer1.Panel1.SuspendLayout();
            splitContainer1.Panel2.SuspendLayout();
            splitContainer1.SuspendLayout();
            SuspendLayout();
            // 
            // dataListViewRight
            // 
            dataListViewRight.AllColumns.Add(olvColumnNo);
            dataListViewRight.AllColumns.Add(olvColumnDataLineName);
            dataListViewRight.AllColumns.Add(olvColumnDataLineValue);
            dataListViewRight.AllColumns.Add(olvColumnDataLineType);
            dataListViewRight.AllColumns.Add(olvColumnTrueValue);
            dataListViewRight.AllColumns.Add(olvColumnComment);
            dataListViewRight.AllColumns.Add(olvColumnChanged);
            dataListViewRight.AllColumns.Add(olvColumnIsCategory);
            dataListViewRight.CellEditUseWholeCell = false;
            dataListViewRight.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] { olvColumnNo, olvColumnDataLineName, olvColumnDataLineValue, olvColumnDataLineType, olvColumnTrueValue, olvColumnComment });
            dataListViewRight.ContextMenuStrip = contextMenuStrip1;
            dataListViewRight.DataSource = null;
            dataListViewRight.Dock = System.Windows.Forms.DockStyle.Fill;
            dataListViewRight.Location = new System.Drawing.Point(0, 0);
            dataListViewRight.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            dataListViewRight.Name = "dataListViewRight";
            dataListViewRight.ShowGroups = false;
            dataListViewRight.Size = new System.Drawing.Size(880, 288);
            dataListViewRight.TabIndex = 17;
            dataListViewRight.UseCompatibleStateImageBehavior = false;
            dataListViewRight.View = System.Windows.Forms.View.Details;
            // 
            // olvColumnNo
            // 
            olvColumnNo.AspectName = "No";
            olvColumnNo.Name = "olvColumnNo";
            olvColumnNo.Text = "No";
            olvColumnNo.Width = 30;
            // 
            // olvColumnDataLineName
            // 
            olvColumnDataLineName.AspectName = "Name";
            olvColumnDataLineName.Name = "olvColumnDataLineName";
            olvColumnDataLineName.Text = "Name";
            olvColumnDataLineName.Width = 180;
            // 
            // olvColumnDataLineValue
            // 
            olvColumnDataLineValue.AspectName = "Value";
            olvColumnDataLineValue.Name = "olvColumnDataLineValue";
            olvColumnDataLineValue.Text = "Value";
            olvColumnDataLineValue.Width = 150;
            // 
            // olvColumnDataLineType
            // 
            olvColumnDataLineType.AspectName = "Type";
            olvColumnDataLineType.IsVisible = false;
            olvColumnDataLineType.Name = "olvColumnDataLineType";
            olvColumnDataLineType.Text = "Type";
            olvColumnDataLineType.Width = 64;
            // 
            // olvColumnTrueValue
            // 
            olvColumnTrueValue.AspectName = "TrueValue";
            olvColumnTrueValue.Name = "olvColumnTrueValue";
            olvColumnTrueValue.Text = "True Value";
            olvColumnTrueValue.Width = 100;
            // 
            // olvColumnComment
            // 
            olvColumnComment.AspectName = "Comment";
            olvColumnComment.Name = "olvColumnComment";
            olvColumnComment.Text = "Comment";
            olvColumnComment.Width = 200;
            // 
            // olvColumnChanged
            // 
            olvColumnChanged.AspectName = "Changed";
            olvColumnChanged.DisplayIndex = 5;
            olvColumnChanged.IsVisible = false;
            olvColumnChanged.Name = "olvColumnChanged";
            olvColumnChanged.Text = "Changed";
            olvColumnChanged.Width = 0;
            // 
            // olvColumnIsCategory
            // 
            olvColumnIsCategory.AspectName = "IsCategory";
            olvColumnIsCategory.DisplayIndex = 6;
            olvColumnIsCategory.IsVisible = false;
            olvColumnIsCategory.Name = "olvColumnIsCategory";
            olvColumnIsCategory.Text = "IsCategory";
            // 
            // contextMenuStrip1
            // 
            contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] { analyzeValueToolStripMenuItem, copyValueToolStripMenuItem, copyAllValuesToolStripMenuItem });
            contextMenuStrip1.Name = "contextMenuStrip1";
            contextMenuStrip1.Size = new System.Drawing.Size(160, 70);
            // 
            // analyzeValueToolStripMenuItem
            // 
            analyzeValueToolStripMenuItem.Name = "analyzeValueToolStripMenuItem";
            analyzeValueToolStripMenuItem.Size = new System.Drawing.Size(159, 22);
            analyzeValueToolStripMenuItem.Text = "Analyze value";
            analyzeValueToolStripMenuItem.Click += analyzeValueToolStripMenuItem_Click;
            // 
            // copyValueToolStripMenuItem
            // 
            copyValueToolStripMenuItem.Name = "copyValueToolStripMenuItem";
            copyValueToolStripMenuItem.Size = new System.Drawing.Size(159, 22);
            copyValueToolStripMenuItem.Text = "Copy value";
            copyValueToolStripMenuItem.Click += copyValueToolStripMenuItem_Click;
            // 
            // copyAllValuesToolStripMenuItem
            // 
            copyAllValuesToolStripMenuItem.Name = "copyAllValuesToolStripMenuItem";
            copyAllValuesToolStripMenuItem.Size = new System.Drawing.Size(159, 22);
            copyAllValuesToolStripMenuItem.Text = "Copy whole line";
            copyAllValuesToolStripMenuItem.Click += copyAllValuesToolStripMenuItem_Click;
            // 
            // splitContainer1
            // 
            splitContainer1.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            splitContainer1.Location = new System.Drawing.Point(0, 278);
            splitContainer1.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            splitContainer1.Name = "splitContainer1";
            splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            splitContainer1.Panel1.Controls.Add(dataListViewRight);
            // 
            // splitContainer1.Panel2
            // 
            splitContainer1.Panel2.Controls.Add(richTextBox1);
            splitContainer1.Size = new System.Drawing.Size(880, 393);
            splitContainer1.SplitterDistance = 288;
            splitContainer1.SplitterWidth = 5;
            splitContainer1.TabIndex = 18;
            // 
            // richTextBox1
            // 
            richTextBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            richTextBox1.Font = new System.Drawing.Font("Courier New", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            richTextBox1.Location = new System.Drawing.Point(0, 0);
            richTextBox1.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            richTextBox1.Name = "richTextBox1";
            richTextBox1.Size = new System.Drawing.Size(880, 100);
            richTextBox1.TabIndex = 0;
            richTextBox1.Text = "";
            // 
            // propertyGrid1
            // 
            propertyGrid1.Location = new System.Drawing.Point(3, 3);
            propertyGrid1.Name = "propertyGrid1";
            propertyGrid1.Size = new System.Drawing.Size(874, 269);
            propertyGrid1.TabIndex = 19;
            // 
            // PacketDisplay
            // 
            AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            Controls.Add(propertyGrid1);
            Controls.Add(splitContainer1);
            DoubleBuffered = true;
            Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            Name = "PacketDisplay";
            Size = new System.Drawing.Size(880, 671);
            ((System.ComponentModel.ISupportInitialize)dataListViewRight).EndInit();
            contextMenuStrip1.ResumeLayout(false);
            splitContainer1.Panel1.ResumeLayout(false);
            splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)splitContainer1).EndInit();
            splitContainer1.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private BrightIdeasSoftware.DataListView dataListViewRight;
        private BrightIdeasSoftware.OLVColumn olvColumnDataLineName;
        private BrightIdeasSoftware.OLVColumn olvColumnDataLineType;
        private BrightIdeasSoftware.OLVColumn olvColumnDataLineValue;
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
        private System.Windows.Forms.PropertyGrid propertyGrid1;
    }
}
