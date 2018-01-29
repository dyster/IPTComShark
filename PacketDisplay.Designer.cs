namespace LiveRecorder
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
            this.dataListViewRight = new BrightIdeasSoftware.DataListView();
            this.olvColumnDataLineName = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.olvColumnDataLineType = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.olvColumnDataLineValue = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.textBoxComid = new System.Windows.Forms.TextBox();
            this.textBoxSize = new System.Windows.Forms.TextBox();
            this.textBoxType = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.textBoxRAW = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.dataListViewRight)).BeginInit();
            this.SuspendLayout();
            // 
            // dataListViewRight
            // 
            this.dataListViewRight.AllColumns.Add(this.olvColumnDataLineName);
            this.dataListViewRight.AllColumns.Add(this.olvColumnDataLineType);
            this.dataListViewRight.AllColumns.Add(this.olvColumnDataLineValue);
            this.dataListViewRight.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dataListViewRight.CellEditUseWholeCell = false;
            this.dataListViewRight.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.olvColumnDataLineName,
            this.olvColumnDataLineType,
            this.olvColumnDataLineValue});
            this.dataListViewRight.Cursor = System.Windows.Forms.Cursors.Default;
            this.dataListViewRight.DataSource = null;
            this.dataListViewRight.Location = new System.Drawing.Point(-1, 104);
            this.dataListViewRight.Name = "dataListViewRight";
            this.dataListViewRight.ShowGroups = false;
            this.dataListViewRight.Size = new System.Drawing.Size(337, 236);
            this.dataListViewRight.TabIndex = 17;
            this.dataListViewRight.UseCompatibleStateImageBehavior = false;
            this.dataListViewRight.View = System.Windows.Forms.View.Details;
            // 
            // olvColumnDataLineName
            // 
            this.olvColumnDataLineName.AspectName = "Name";
            this.olvColumnDataLineName.Text = "Name";
            this.olvColumnDataLineName.Width = 150;
            // 
            // olvColumnDataLineType
            // 
            this.olvColumnDataLineType.AspectName = "Type";
            this.olvColumnDataLineType.Text = "Type";
            this.olvColumnDataLineType.Width = 100;
            // 
            // olvColumnDataLineValue
            // 
            this.olvColumnDataLineValue.AspectName = "Value";
            this.olvColumnDataLineValue.Text = "Value";
            this.olvColumnDataLineValue.Width = 500;
            // 
            // textBoxComid
            // 
            this.textBoxComid.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxComid.Location = new System.Drawing.Point(42, 26);
            this.textBoxComid.Name = "textBoxComid";
            this.textBoxComid.Size = new System.Drawing.Size(294, 20);
            this.textBoxComid.TabIndex = 16;
            // 
            // textBoxSize
            // 
            this.textBoxSize.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxSize.Location = new System.Drawing.Point(42, 52);
            this.textBoxSize.Name = "textBoxSize";
            this.textBoxSize.Size = new System.Drawing.Size(294, 20);
            this.textBoxSize.TabIndex = 15;
            // 
            // textBoxType
            // 
            this.textBoxType.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxType.Location = new System.Drawing.Point(42, 78);
            this.textBoxType.Name = "textBoxType";
            this.textBoxType.Size = new System.Drawing.Size(294, 20);
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
            this.textBoxRAW.Size = new System.Drawing.Size(294, 20);
            this.textBoxRAW.TabIndex = 9;
            // 
            // PacketDisplay
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.dataListViewRight);
            this.Controls.Add(this.textBoxComid);
            this.Controls.Add(this.textBoxSize);
            this.Controls.Add(this.textBoxType);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.textBoxRAW);
            this.Name = "PacketDisplay";
            this.Size = new System.Drawing.Size(336, 340);
            ((System.ComponentModel.ISupportInitialize)(this.dataListViewRight)).EndInit();
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
    }
}
