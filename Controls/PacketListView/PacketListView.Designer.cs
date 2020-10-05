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
            this.olvColumnNo = ((IPTComShark.Controls.MyOLVColumn)(new IPTComShark.Controls.MyOLVColumn()));
            this.olvColumnDate = ((IPTComShark.Controls.MyOLVColumn)(new IPTComShark.Controls.MyOLVColumn()));
            this.olvColumnMS = ((IPTComShark.Controls.MyOLVColumn)(new IPTComShark.Controls.MyOLVColumn()));
            this.olvColumnFrom = ((IPTComShark.Controls.MyOLVColumn)(new IPTComShark.Controls.MyOLVColumn()));
            this.olvColumnTo = ((IPTComShark.Controls.MyOLVColumn)(new IPTComShark.Controls.MyOLVColumn()));
            this.olvColumnProtocol = ((IPTComShark.Controls.MyOLVColumn)(new IPTComShark.Controls.MyOLVColumn()));
            this.olvColumnProtocolInfo = ((IPTComShark.Controls.MyOLVColumn)(new IPTComShark.Controls.MyOLVColumn()));
            this.olvColumnName = ((IPTComShark.Controls.MyOLVColumn)(new IPTComShark.Controls.MyOLVColumn()));
            this.olvColumnDictionary = ((IPTComShark.Controls.MyOLVColumn)(new IPTComShark.Controls.MyOLVColumn()));
            this.olvColumnIPTWPType = ((IPTComShark.Controls.MyOLVColumn)(new IPTComShark.Controls.MyOLVColumn()));
            this.olvColumnComId = ((IPTComShark.Controls.MyOLVColumn)(new IPTComShark.Controls.MyOLVColumn()));
            this.olvColumnError = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.contextMenuMouse = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.copyRawByteshexStringToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.copyParsedDatatextStringToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.analyzeChainToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.addToIgnoredComIDsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.timerAddBuffer = new System.Windows.Forms.Timer(this.components);
            this.timerFlicker = new System.Windows.Forms.Timer(this.components);
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
            this.fastObjectListView1.AllColumns.Add(this.olvColumnError);
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
            this.olvColumnComId,
            this.olvColumnError});
            this.fastObjectListView1.ContextMenuStrip = this.contextMenuMouse;
            this.fastObjectListView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.fastObjectListView1.EmptyListMsg = "No files loaded, use File->Open or Drag&Drop";
            this.fastObjectListView1.FullRowSelect = true;
            this.fastObjectListView1.HideSelection = false;
            this.fastObjectListView1.Location = new System.Drawing.Point(0, 0);
            this.fastObjectListView1.Name = "fastObjectListView1";
            this.fastObjectListView1.OverlayText.Alignment = System.Drawing.ContentAlignment.MiddleCenter;
            this.fastObjectListView1.OverlayText.Text = "";
            this.fastObjectListView1.SelectedBackColor = System.Drawing.SystemColors.Highlight;
            this.fastObjectListView1.ShowGroups = false;
            this.fastObjectListView1.Size = new System.Drawing.Size(1112, 763);
            this.fastObjectListView1.TabIndex = 4;
            this.fastObjectListView1.UnfocusedSelectedBackColor = System.Drawing.SystemColors.Highlight;
            this.fastObjectListView1.UseCompatibleStateImageBehavior = false;
            this.fastObjectListView1.UseFiltering = true;
            this.fastObjectListView1.View = System.Windows.Forms.View.Details;
            this.fastObjectListView1.VirtualMode = true;
            this.fastObjectListView1.SelectedIndexChanged += new System.EventHandler(this.fastObjectListView1_SelectedIndexChanged);
            // 
            // olvColumnNo
            // 
            this.olvColumnNo.AspectName = "No";
            this.olvColumnNo.ClusterGetter = null;
            this.olvColumnNo.Searchable = false;
            this.olvColumnNo.Text = "No";
            this.olvColumnNo.UseFiltering = false;
            // 
            // olvColumnDate
            // 
            this.olvColumnDate.AspectName = "Date";
            this.olvColumnDate.ClusterGetter = null;
            this.olvColumnDate.Text = "Date";
            this.olvColumnDate.UseFiltering = false;
            this.olvColumnDate.Width = 130;
            // 
            // olvColumnMS
            // 
            this.olvColumnMS.ClusterGetter = null;
            this.olvColumnMS.Text = "ms";
            this.olvColumnMS.UseFiltering = false;
            this.olvColumnMS.Width = 40;
            // 
            // olvColumnFrom
            // 
            this.olvColumnFrom.AspectName = "";
            this.olvColumnFrom.ClusterGetter = null;
            this.olvColumnFrom.Text = "From";
            // 
            // olvColumnTo
            // 
            this.olvColumnTo.AspectName = "";
            this.olvColumnTo.ClusterGetter = null;
            this.olvColumnTo.Text = "To";
            // 
            // olvColumnProtocol
            // 
            this.olvColumnProtocol.AspectName = "Protocol";
            this.olvColumnProtocol.ClusterGetter = null;
            this.olvColumnProtocol.Text = "Protocol";
            this.olvColumnProtocol.Width = 100;
            // 
            // olvColumnProtocolInfo
            // 
            this.olvColumnProtocolInfo.AspectName = "ProtocolInfo";
            this.olvColumnProtocolInfo.ClusterGetter = null;
            this.olvColumnProtocolInfo.Text = "Protocol Info";
            this.olvColumnProtocolInfo.Width = 100;
            // 
            // olvColumnName
            // 
            this.olvColumnName.AspectName = "Name";
            this.olvColumnName.ClusterGetter = null;
            this.olvColumnName.Text = "Name";
            this.olvColumnName.Width = 150;
            // 
            // olvColumnDictionary
            // 
            this.olvColumnDictionary.AspectName = "No";
            this.olvColumnDictionary.ClusterGetter = null;
            this.olvColumnDictionary.Text = "Parsed";
            this.olvColumnDictionary.UseFiltering = false;
            this.olvColumnDictionary.Width = 398;
            // 
            // olvColumnIPTWPType
            // 
            this.olvColumnIPTWPType.AspectName = "IPTWPPacket.IPTWPType";
            this.olvColumnIPTWPType.ClusterGetter = null;
            this.olvColumnIPTWPType.Text = "IPTWP Type";
            // 
            // olvColumnComId
            // 
            this.olvColumnComId.AspectName = "IPTWPPacket.Comid";
            this.olvColumnComId.ClusterGetter = null;
            this.olvColumnComId.Text = "ComID";
            // 
            // olvColumnError
            // 
            this.olvColumnError.AspectName = "Error";
            this.olvColumnError.Text = "Error";
            // 
            // contextMenuMouse
            // 
            this.contextMenuMouse.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.copyRawByteshexStringToolStripMenuItem,
            this.copyParsedDatatextStringToolStripMenuItem,
            this.analyzeChainToolStripMenuItem,
            this.addToIgnoredComIDsToolStripMenuItem});
            this.contextMenuMouse.Name = "contextMenuMouse";
            this.contextMenuMouse.Size = new System.Drawing.Size(231, 92);
            this.contextMenuMouse.Opening += new System.ComponentModel.CancelEventHandler(this.ContextMenuMouse_Opening);
            // 
            // copyRawByteshexStringToolStripMenuItem
            // 
            this.copyRawByteshexStringToolStripMenuItem.Name = "copyRawByteshexStringToolStripMenuItem";
            this.copyRawByteshexStringToolStripMenuItem.Size = new System.Drawing.Size(230, 22);
            this.copyRawByteshexStringToolStripMenuItem.Text = "Copy raw bytes (hex string)";
            this.copyRawByteshexStringToolStripMenuItem.Click += new System.EventHandler(this.copyRawByteshexStringToolStripMenuItem_Click);
            // 
            // copyParsedDatatextStringToolStripMenuItem
            // 
            this.copyParsedDatatextStringToolStripMenuItem.Name = "copyParsedDatatextStringToolStripMenuItem";
            this.copyParsedDatatextStringToolStripMenuItem.Size = new System.Drawing.Size(230, 22);
            this.copyParsedDatatextStringToolStripMenuItem.Text = "Copy parsed data (text string)";
            this.copyParsedDatatextStringToolStripMenuItem.Click += new System.EventHandler(this.copyParsedDatatextStringToolStripMenuItem_Click);
            // 
            // analyzeChainToolStripMenuItem
            // 
            this.analyzeChainToolStripMenuItem.Name = "analyzeChainToolStripMenuItem";
            this.analyzeChainToolStripMenuItem.Size = new System.Drawing.Size(230, 22);
            this.analyzeChainToolStripMenuItem.Text = "Analyze chain";
            this.analyzeChainToolStripMenuItem.Click += new System.EventHandler(this.analyzeChainToolStripMenuItem_Click);
            // 
            // addToIgnoredComIDsToolStripMenuItem
            // 
            this.addToIgnoredComIDsToolStripMenuItem.Name = "addToIgnoredComIDsToolStripMenuItem";
            this.addToIgnoredComIDsToolStripMenuItem.Size = new System.Drawing.Size(230, 22);
            this.addToIgnoredComIDsToolStripMenuItem.Text = "Add to ignored ComID\'s";
            this.addToIgnoredComIDsToolStripMenuItem.Click += new System.EventHandler(this.addToIgnoredComIDsToolStripMenuItem_Click);
            // 
            // timerAddBuffer
            // 
            this.timerAddBuffer.Enabled = true;
            this.timerAddBuffer.Interval = 1000;
            this.timerAddBuffer.Tick += new System.EventHandler(this.timerAddBuffer_Tick);
            // 
            // timerFlicker
            // 
            this.timerFlicker.Interval = 500;
            this.timerFlicker.Tick += new System.EventHandler(this.timerFlicker_Tick);
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
        private MyOLVColumn olvColumnNo;
        private MyOLVColumn olvColumnDate;
        private MyOLVColumn olvColumnFrom;
        private MyOLVColumn olvColumnTo;
        private MyOLVColumn olvColumnName;
        private MyOLVColumn olvColumnDictionary;
        private MyOLVColumn olvColumnProtocol;
        private MyOLVColumn olvColumnIPTWPType;
        private System.Windows.Forms.Timer timerAddBuffer;
        private MyOLVColumn olvColumnComId;
        private MyOLVColumn olvColumnProtocolInfo;
        private System.Windows.Forms.ContextMenuStrip contextMenuMouse;
        private System.Windows.Forms.ToolStripMenuItem copyRawByteshexStringToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem copyParsedDatatextStringToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem analyzeChainToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem addToIgnoredComIDsToolStripMenuItem;
        private MyOLVColumn olvColumnMS;
        private BrightIdeasSoftware.OLVColumn olvColumnError;
        private System.Windows.Forms.Timer timerFlicker;
    }
}
