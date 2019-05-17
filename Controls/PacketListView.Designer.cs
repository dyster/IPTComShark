namespace IPTComShark.Controls
{
    partial class PacketListView
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
            this.fastObjectListView1 = new BrightIdeasSoftware.FastObjectListView();
            this.olvColumnNo = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.olvColumnDate = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.olvColumnMS = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.olvColumnFrom = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.olvColumnTo = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.olvColumnProtocol = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.olvColumnProtocolInfo = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.olvColumnName = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.olvColumnDictionary = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.olvColumnIPTWPType = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.olvColumnComId = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.contextMenuMouse = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.copyRawByteshexStringToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.copyParsedDatatextStringToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.analyzeChainToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.addToIgnoredComIDsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.timerAddBuffer = new System.Windows.Forms.Timer(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.fastObjectListView1)).BeginInit();
            this.contextMenuMouse.SuspendLayout();
            this.SuspendLayout();
            // 
            // fastObjectListView1
            // 
            this.fastObjectListView1.AllColumns.Add(this.olvColumnNo);
            this.fastObjectListView1.AllColumns.Add(this.olvColumnDate);
            this.fastObjectListView1.AllColumns.Add(this.olvColumnMS);
            this.fastObjectListView1.AllColumns.Add(this.olvColumnFrom);
            this.fastObjectListView1.AllColumns.Add(this.olvColumnTo);
            this.fastObjectListView1.AllColumns.Add(this.olvColumnProtocol);
            this.fastObjectListView1.AllColumns.Add(this.olvColumnProtocolInfo);
            this.fastObjectListView1.AllColumns.Add(this.olvColumnName);
            this.fastObjectListView1.AllColumns.Add(this.olvColumnDictionary);
            this.fastObjectListView1.AllColumns.Add(this.olvColumnIPTWPType);
            this.fastObjectListView1.AllColumns.Add(this.olvColumnComId);
            this.fastObjectListView1.AllowColumnReorder = true;
            this.fastObjectListView1.CellEditUseWholeCell = false;
            this.fastObjectListView1.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.olvColumnNo,
            this.olvColumnDate,
            this.olvColumnMS,
            this.olvColumnFrom,
            this.olvColumnTo,
            this.olvColumnProtocol,
            this.olvColumnProtocolInfo,
            this.olvColumnName,
            this.olvColumnDictionary,
            this.olvColumnIPTWPType,
            this.olvColumnComId});
            this.fastObjectListView1.ContextMenuStrip = this.contextMenuMouse;
            this.fastObjectListView1.Cursor = System.Windows.Forms.Cursors.Default;
            this.fastObjectListView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.fastObjectListView1.EmptyListMsg = "No files loaded, use File->Open or Drag&Drop";
            this.fastObjectListView1.FullRowSelect = true;
            this.fastObjectListView1.Location = new System.Drawing.Point(0, 0);
            this.fastObjectListView1.Name = "fastObjectListView1";
            this.fastObjectListView1.OverlayText.Alignment = System.Drawing.ContentAlignment.MiddleCenter;
            this.fastObjectListView1.OverlayText.Text = "";
            this.fastObjectListView1.ShowGroups = false;
            this.fastObjectListView1.Size = new System.Drawing.Size(1112, 763);
            this.fastObjectListView1.TabIndex = 4;
            this.fastObjectListView1.UseCompatibleStateImageBehavior = false;
            this.fastObjectListView1.UseFiltering = true;
            this.fastObjectListView1.View = System.Windows.Forms.View.Details;
            this.fastObjectListView1.VirtualMode = true;
            this.fastObjectListView1.SelectedIndexChanged += new System.EventHandler(this.fastObjectListView1_SelectedIndexChanged);
            // 
            // olvColumnNo
            // 
            this.olvColumnNo.AspectName = "No";
            this.olvColumnNo.Text = "No";
            // 
            // olvColumnDate
            // 
            this.olvColumnDate.AspectName = "Date";
            this.olvColumnDate.Text = "Date";
            this.olvColumnDate.Width = 130;
            // 
            // olvColumnMS
            // 
            this.olvColumnMS.Text = "ms";
            this.olvColumnMS.Width = 40;
            // 
            // olvColumnFrom
            // 
            this.olvColumnFrom.AspectName = "";
            this.olvColumnFrom.Text = "From";
            // 
            // olvColumnTo
            // 
            this.olvColumnTo.AspectName = "";
            this.olvColumnTo.Text = "To";
            // 
            // olvColumnProtocol
            // 
            this.olvColumnProtocol.AspectName = "Protocol";
            this.olvColumnProtocol.Text = "Protocol";
            this.olvColumnProtocol.Width = 100;
            // 
            // olvColumnProtocolInfo
            // 
            this.olvColumnProtocolInfo.AspectName = "ProtocolInfo";
            this.olvColumnProtocolInfo.Text = "Protocol Info";
            this.olvColumnProtocolInfo.Width = 100;
            // 
            // olvColumnName
            // 
            this.olvColumnName.AspectName = "Name";
            this.olvColumnName.Text = "Name";
            this.olvColumnName.Width = 150;
            // 
            // olvColumnDictionary
            // 
            this.olvColumnDictionary.AspectName = "ParsedData";
            this.olvColumnDictionary.Text = "Parsed";
            this.olvColumnDictionary.Width = 398;
            // 
            // olvColumnIPTWPType
            // 
            this.olvColumnIPTWPType.AspectName = "IPTWPPacket.IPTWPType";
            this.olvColumnIPTWPType.Text = "IPTWP Type";
            // 
            // olvColumnComId
            // 
            this.olvColumnComId.AspectName = "IPTWPPacket.Comid";
            this.olvColumnComId.Text = "ComID";
            // 
            // contextMenuMouse
            // 
            this.contextMenuMouse.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.copyRawByteshexStringToolStripMenuItem,
            this.copyParsedDatatextStringToolStripMenuItem,
            this.analyzeChainToolStripMenuItem,
            this.addToIgnoredComIDsToolStripMenuItem});
            this.contextMenuMouse.Name = "contextMenuMouse";
            this.contextMenuMouse.Size = new System.Drawing.Size(230, 92);
            // 
            // copyRawByteshexStringToolStripMenuItem
            // 
            this.copyRawByteshexStringToolStripMenuItem.Name = "copyRawByteshexStringToolStripMenuItem";
            this.copyRawByteshexStringToolStripMenuItem.Size = new System.Drawing.Size(229, 22);
            this.copyRawByteshexStringToolStripMenuItem.Text = "Copy raw bytes (hex string)";
            this.copyRawByteshexStringToolStripMenuItem.Click += new System.EventHandler(this.copyRawByteshexStringToolStripMenuItem_Click);
            // 
            // copyParsedDatatextStringToolStripMenuItem
            // 
            this.copyParsedDatatextStringToolStripMenuItem.Name = "copyParsedDatatextStringToolStripMenuItem";
            this.copyParsedDatatextStringToolStripMenuItem.Size = new System.Drawing.Size(229, 22);
            this.copyParsedDatatextStringToolStripMenuItem.Text = "Copy parsed data (text string)";
            this.copyParsedDatatextStringToolStripMenuItem.Click += new System.EventHandler(this.copyParsedDatatextStringToolStripMenuItem_Click);
            // 
            // analyzeChainToolStripMenuItem
            // 
            this.analyzeChainToolStripMenuItem.Name = "analyzeChainToolStripMenuItem";
            this.analyzeChainToolStripMenuItem.Size = new System.Drawing.Size(229, 22);
            this.analyzeChainToolStripMenuItem.Text = "Analyze chain";
            this.analyzeChainToolStripMenuItem.Click += new System.EventHandler(this.analyzeChainToolStripMenuItem_Click);
            // 
            // addToIgnoredComIDsToolStripMenuItem
            // 
            this.addToIgnoredComIDsToolStripMenuItem.Name = "addToIgnoredComIDsToolStripMenuItem";
            this.addToIgnoredComIDsToolStripMenuItem.Size = new System.Drawing.Size(229, 22);
            this.addToIgnoredComIDsToolStripMenuItem.Text = "Add to ignored ComID\'s";
            this.addToIgnoredComIDsToolStripMenuItem.Click += new System.EventHandler(this.addToIgnoredComIDsToolStripMenuItem_Click);
            // 
            // timerAddBuffer
            // 
            this.timerAddBuffer.Enabled = true;
            this.timerAddBuffer.Interval = 1000;
            this.timerAddBuffer.Tick += new System.EventHandler(this.timerAddBuffer_Tick);
            // 
            // PacketListView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.fastObjectListView1);
            this.DoubleBuffered = true;
            this.Name = "PacketListView";
            this.Size = new System.Drawing.Size(1112, 763);
            this.Load += new System.EventHandler(this.PacketListView_Load);
            ((System.ComponentModel.ISupportInitialize)(this.fastObjectListView1)).EndInit();
            this.contextMenuMouse.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private BrightIdeasSoftware.FastObjectListView fastObjectListView1;
        private BrightIdeasSoftware.OLVColumn olvColumnNo;
        private BrightIdeasSoftware.OLVColumn olvColumnDate;
        private BrightIdeasSoftware.OLVColumn olvColumnFrom;
        private BrightIdeasSoftware.OLVColumn olvColumnTo;
        private BrightIdeasSoftware.OLVColumn olvColumnName;
        private BrightIdeasSoftware.OLVColumn olvColumnDictionary;
        private BrightIdeasSoftware.OLVColumn olvColumnProtocol;
        private BrightIdeasSoftware.OLVColumn olvColumnIPTWPType;
        private System.Windows.Forms.Timer timerAddBuffer;
        private BrightIdeasSoftware.OLVColumn olvColumnComId;
        private BrightIdeasSoftware.OLVColumn olvColumnProtocolInfo;
        private System.Windows.Forms.ContextMenuStrip contextMenuMouse;
        private System.Windows.Forms.ToolStripMenuItem copyRawByteshexStringToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem copyParsedDatatextStringToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem analyzeChainToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem addToIgnoredComIDsToolStripMenuItem;
        private BrightIdeasSoftware.OLVColumn olvColumnMS;
    }
}
