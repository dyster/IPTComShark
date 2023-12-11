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
            components = new System.ComponentModel.Container();
            fastObjectListView1 = new BrightIdeasSoftware.FastObjectListView();
            olvColumnNo = new MyOLVColumn();
            olvColumnDate = new MyOLVColumn();
            olvColumnMS = new MyOLVColumn();
            olvColumnFrom = new MyOLVColumn();
            olvColumnTo = new MyOLVColumn();
            olvColumnProtocol = new MyOLVColumn();
            olvColumnProtocolInfo = new MyOLVColumn();
            olvColumnName = new MyOLVColumn();
            olvColumnDictionary = new MyOLVColumn();
            olvColumnIPTWPType = new MyOLVColumn();
            olvColumnComId = new MyOLVColumn();
            olvColumnError = new BrightIdeasSoftware.OLVColumn();
            contextMenuMouse = new System.Windows.Forms.ContextMenuStrip(components);
            copyRawByteshexStringToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            copyParsedDatatextStringToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            analyzeChainToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            addToIgnoredComIDsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            sPREADSHEETToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            timerAddBuffer = new System.Windows.Forms.Timer(components);
            timerFlicker = new System.Windows.Forms.Timer(components);
            copyDisplayedTextToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            ((System.ComponentModel.ISupportInitialize)fastObjectListView1).BeginInit();
            contextMenuMouse.SuspendLayout();
            SuspendLayout();
            // 
            // fastObjectListView1
            // 
            fastObjectListView1.AllColumns.Add(olvColumnNo);
            fastObjectListView1.AllColumns.Add(olvColumnDate);
            fastObjectListView1.AllColumns.Add(olvColumnMS);
            fastObjectListView1.AllColumns.Add(olvColumnFrom);
            fastObjectListView1.AllColumns.Add(olvColumnTo);
            fastObjectListView1.AllColumns.Add(olvColumnProtocol);
            fastObjectListView1.AllColumns.Add(olvColumnProtocolInfo);
            fastObjectListView1.AllColumns.Add(olvColumnName);
            fastObjectListView1.AllColumns.Add(olvColumnDictionary);
            fastObjectListView1.AllColumns.Add(olvColumnIPTWPType);
            fastObjectListView1.AllColumns.Add(olvColumnComId);
            fastObjectListView1.AllColumns.Add(olvColumnError);
            fastObjectListView1.AllowColumnReorder = true;
            fastObjectListView1.CellEditUseWholeCell = false;
            fastObjectListView1.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] { olvColumnNo, olvColumnDate, olvColumnMS, olvColumnFrom, olvColumnTo, olvColumnProtocol, olvColumnProtocolInfo, olvColumnName, olvColumnDictionary, olvColumnIPTWPType, olvColumnComId, olvColumnError });
            fastObjectListView1.ContextMenuStrip = contextMenuMouse;
            fastObjectListView1.Dock = System.Windows.Forms.DockStyle.Fill;
            fastObjectListView1.EmptyListMsg = "No files loaded, use File->Open or Drag&Drop";
            fastObjectListView1.FullRowSelect = true;
            fastObjectListView1.Location = new System.Drawing.Point(0, 0);
            fastObjectListView1.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            fastObjectListView1.Name = "fastObjectListView1";
            fastObjectListView1.OverlayText.Alignment = System.Drawing.ContentAlignment.MiddleCenter;
            fastObjectListView1.OverlayText.Text = "";
            fastObjectListView1.SelectedBackColor = System.Drawing.SystemColors.Highlight;
            fastObjectListView1.ShowGroups = false;
            fastObjectListView1.Size = new System.Drawing.Size(1297, 880);
            fastObjectListView1.TabIndex = 4;
            fastObjectListView1.UnfocusedSelectedBackColor = System.Drawing.SystemColors.Highlight;
            fastObjectListView1.UseCompatibleStateImageBehavior = false;
            fastObjectListView1.UseFiltering = true;
            fastObjectListView1.View = System.Windows.Forms.View.Details;
            fastObjectListView1.VirtualMode = true;
            fastObjectListView1.SelectedIndexChanged += fastObjectListView1_SelectedIndexChanged;
            // 
            // olvColumnNo
            // 
            olvColumnNo.AspectName = "No";
            olvColumnNo.ClusterGetter = null;
            olvColumnNo.Searchable = false;
            olvColumnNo.Text = "No";
            olvColumnNo.UseFiltering = false;
            // 
            // olvColumnDate
            // 
            olvColumnDate.AspectName = "Date";
            olvColumnDate.ClusterGetter = null;
            olvColumnDate.Text = "Date";
            olvColumnDate.UseFiltering = false;
            olvColumnDate.Width = 130;
            // 
            // olvColumnMS
            // 
            olvColumnMS.ClusterGetter = null;
            olvColumnMS.Text = "ms";
            olvColumnMS.UseFiltering = false;
            olvColumnMS.Width = 40;
            // 
            // olvColumnFrom
            // 
            olvColumnFrom.AspectName = "";
            olvColumnFrom.ClusterGetter = null;
            olvColumnFrom.Text = "From";
            // 
            // olvColumnTo
            // 
            olvColumnTo.AspectName = "";
            olvColumnTo.ClusterGetter = null;
            olvColumnTo.Text = "To";
            // 
            // olvColumnProtocol
            // 
            olvColumnProtocol.AspectName = "Protocol";
            olvColumnProtocol.ClusterGetter = null;
            olvColumnProtocol.Text = "Protocol";
            olvColumnProtocol.Width = 100;
            // 
            // olvColumnProtocolInfo
            // 
            olvColumnProtocolInfo.AspectName = "ProtocolInfo";
            olvColumnProtocolInfo.ClusterGetter = null;
            olvColumnProtocolInfo.Text = "Protocol Info";
            olvColumnProtocolInfo.Width = 100;
            // 
            // olvColumnName
            // 
            olvColumnName.AspectName = "Name";
            olvColumnName.ClusterGetter = null;
            olvColumnName.Text = "Name";
            olvColumnName.Width = 150;
            // 
            // olvColumnDictionary
            // 
            olvColumnDictionary.AspectName = "No";
            olvColumnDictionary.ClusterGetter = null;
            olvColumnDictionary.Text = "Parsed";
            olvColumnDictionary.UseFiltering = false;
            olvColumnDictionary.Width = 398;
            // 
            // olvColumnIPTWPType
            // 
            olvColumnIPTWPType.AspectName = "IPTWPPacket.IPTWPType";
            olvColumnIPTWPType.ClusterGetter = null;
            olvColumnIPTWPType.Text = "IPTWP Type";
            // 
            // olvColumnComId
            // 
            olvColumnComId.AspectName = "IPTWPPacket.Comid";
            olvColumnComId.ClusterGetter = null;
            olvColumnComId.Text = "ComID";
            // 
            // olvColumnError
            // 
            olvColumnError.AspectName = "Error";
            olvColumnError.Text = "Error";
            // 
            // contextMenuMouse
            // 
            contextMenuMouse.Items.AddRange(new System.Windows.Forms.ToolStripItem[] { copyRawByteshexStringToolStripMenuItem, copyDisplayedTextToolStripMenuItem, copyParsedDatatextStringToolStripMenuItem, analyzeChainToolStripMenuItem, addToIgnoredComIDsToolStripMenuItem, sPREADSHEETToolStripMenuItem });
            contextMenuMouse.Name = "contextMenuMouse";
            contextMenuMouse.Size = new System.Drawing.Size(246, 158);
            contextMenuMouse.Opening += ContextMenuMouse_Opening;
            // 
            // copyRawByteshexStringToolStripMenuItem
            // 
            copyRawByteshexStringToolStripMenuItem.Name = "copyRawByteshexStringToolStripMenuItem";
            copyRawByteshexStringToolStripMenuItem.Size = new System.Drawing.Size(245, 22);
            copyRawByteshexStringToolStripMenuItem.Text = "Copy raw bytes (hex string)";
            copyRawByteshexStringToolStripMenuItem.Click += copyRawByteshexStringToolStripMenuItem_Click;
            // 
            // copyParsedDatatextStringToolStripMenuItem
            // 
            copyParsedDatatextStringToolStripMenuItem.Name = "copyParsedDatatextStringToolStripMenuItem";
            copyParsedDatatextStringToolStripMenuItem.Size = new System.Drawing.Size(245, 22);
            copyParsedDatatextStringToolStripMenuItem.Text = "Copy all parsed data (text string)";
            copyParsedDatatextStringToolStripMenuItem.Click += copyParsedDatatextStringToolStripMenuItem_Click;
            // 
            // analyzeChainToolStripMenuItem
            // 
            analyzeChainToolStripMenuItem.Name = "analyzeChainToolStripMenuItem";
            analyzeChainToolStripMenuItem.Size = new System.Drawing.Size(245, 22);
            analyzeChainToolStripMenuItem.Text = "Analyze chain";
            analyzeChainToolStripMenuItem.Click += analyzeChainToolStripMenuItem_Click;
            // 
            // addToIgnoredComIDsToolStripMenuItem
            // 
            addToIgnoredComIDsToolStripMenuItem.Name = "addToIgnoredComIDsToolStripMenuItem";
            addToIgnoredComIDsToolStripMenuItem.Size = new System.Drawing.Size(245, 22);
            addToIgnoredComIDsToolStripMenuItem.Text = "Add to ignored ComID's";
            addToIgnoredComIDsToolStripMenuItem.Click += addToIgnoredComIDsToolStripMenuItem_Click;
            // 
            // sPREADSHEETToolStripMenuItem
            // 
            sPREADSHEETToolStripMenuItem.Name = "sPREADSHEETToolStripMenuItem";
            sPREADSHEETToolStripMenuItem.Size = new System.Drawing.Size(245, 22);
            sPREADSHEETToolStripMenuItem.Text = "SPREADSHEET";
            sPREADSHEETToolStripMenuItem.Click += sPREADSHEETToolStripMenuItem_Click;
            // 
            // timerAddBuffer
            // 
            timerAddBuffer.Enabled = true;
            timerAddBuffer.Interval = 1000;
            timerAddBuffer.Tick += timerAddBuffer_Tick;
            // 
            // timerFlicker
            // 
            timerFlicker.Interval = 500;
            timerFlicker.Tick += timerFlicker_Tick;
            // 
            // copyDisplayedTextToolStripMenuItem
            // 
            copyDisplayedTextToolStripMenuItem.Name = "copyDisplayedTextToolStripMenuItem";
            copyDisplayedTextToolStripMenuItem.Size = new System.Drawing.Size(245, 22);
            copyDisplayedTextToolStripMenuItem.Text = "Copy displayed text";
            copyDisplayedTextToolStripMenuItem.Click += copyDisplayedTextToolStripMenuItem_Click;
            // 
            // PacketListView
            // 
            AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            Controls.Add(fastObjectListView1);
            DoubleBuffered = true;
            Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            Name = "PacketListView";
            Size = new System.Drawing.Size(1297, 880);
            Load += PacketListView_Load;
            ((System.ComponentModel.ISupportInitialize)fastObjectListView1).EndInit();
            contextMenuMouse.ResumeLayout(false);
            ResumeLayout(false);
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
        private System.Windows.Forms.ToolStripMenuItem sPREADSHEETToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem copyDisplayedTextToolStripMenuItem;
    }
}
