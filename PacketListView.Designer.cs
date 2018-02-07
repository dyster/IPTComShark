namespace IPTComShark
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
            this.olvColumnFrom = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.olvColumnTo = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.olvColumnProtocol = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.olvColumnName = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.olvColumnDictionary = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.olvColumnIPTWPType = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.timerAddBuffer = new System.Windows.Forms.Timer(this.components);
            this.olvColumnComId = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            ((System.ComponentModel.ISupportInitialize)(this.fastObjectListView1)).BeginInit();
            this.SuspendLayout();
            // 
            // fastObjectListView1
            // 
            this.fastObjectListView1.AllColumns.Add(this.olvColumnNo);
            this.fastObjectListView1.AllColumns.Add(this.olvColumnDate);
            this.fastObjectListView1.AllColumns.Add(this.olvColumnFrom);
            this.fastObjectListView1.AllColumns.Add(this.olvColumnTo);
            this.fastObjectListView1.AllColumns.Add(this.olvColumnProtocol);
            this.fastObjectListView1.AllColumns.Add(this.olvColumnName);
            this.fastObjectListView1.AllColumns.Add(this.olvColumnDictionary);
            this.fastObjectListView1.AllColumns.Add(this.olvColumnIPTWPType);
            this.fastObjectListView1.AllColumns.Add(this.olvColumnComId);
            this.fastObjectListView1.CellEditUseWholeCell = false;
            this.fastObjectListView1.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.olvColumnNo,
            this.olvColumnDate,
            this.olvColumnFrom,
            this.olvColumnTo,
            this.olvColumnProtocol,
            this.olvColumnName,
            this.olvColumnDictionary,
            this.olvColumnIPTWPType,
            this.olvColumnComId});
            this.fastObjectListView1.Cursor = System.Windows.Forms.Cursors.Default;
            this.fastObjectListView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.fastObjectListView1.FullRowSelect = true;
            this.fastObjectListView1.Location = new System.Drawing.Point(0, 0);
            this.fastObjectListView1.Name = "fastObjectListView1";
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
            this.olvColumnDate.Width = 100;
            // 
            // olvColumnFrom
            // 
            this.olvColumnFrom.AspectName = "IPv4Packet.SourceAddress";
            this.olvColumnFrom.Text = "From";
            // 
            // olvColumnTo
            // 
            this.olvColumnTo.AspectName = "IPv4Packet.DestinationAddress";
            this.olvColumnTo.Text = "To";
            // 
            // olvColumnProtocol
            // 
            this.olvColumnProtocol.AspectName = "IPv4Packet.Protocol";
            this.olvColumnProtocol.Text = "Protocol";
            this.olvColumnProtocol.Width = 100;
            // 
            // olvColumnName
            // 
            this.olvColumnName.AspectName = "Name";
            this.olvColumnName.Text = "Name";
            this.olvColumnName.Width = 150;
            // 
            // olvColumnDictionary
            // 
            this.olvColumnDictionary.AspectName = "ParsedDate.DictionaryData";
            this.olvColumnDictionary.Text = "Parsed";
            this.olvColumnDictionary.Width = 398;
            // 
            // olvColumnIPTWPType
            // 
            this.olvColumnIPTWPType.AspectName = "IPTWPPacket.IPTWPType";
            this.olvColumnIPTWPType.Text = "IPTWP Type";
            // 
            // timerAddBuffer
            // 
            this.timerAddBuffer.Enabled = true;
            this.timerAddBuffer.Interval = 200;
            this.timerAddBuffer.Tick += new System.EventHandler(this.timerAddBuffer_Tick);
            // 
            // olvColumnComId
            // 
            this.olvColumnComId.AspectName = "IPTWPPacket.Comid";
            this.olvColumnComId.Text = "ComID";
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
    }
}
