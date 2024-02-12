namespace TrainShark.Export
{
    partial class Exporterer
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
            this.groupBoxPacketSelect = new System.Windows.Forms.GroupBox();
            this.radioButtonSelectFile = new System.Windows.Forms.RadioButton();
            this.radioButtonSelectSelected = new System.Windows.Forms.RadioButton();
            this.radioButtonSelectFilter = new System.Windows.Forms.RadioButton();
            this.radioButtonSelectAll = new System.Windows.Forms.RadioButton();
            this.buttonOK = new System.Windows.Forms.Button();
            this.checkBoxProfibus = new System.Windows.Forms.CheckBox();
            this.checkBoxEverything = new System.Windows.Forms.CheckBox();
            this.checkBoxSAPIdle = new System.Windows.Forms.CheckBox();
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            this.groupBoxPacketSelect.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBoxPacketSelect
            // 
            this.groupBoxPacketSelect.Controls.Add(this.radioButtonSelectFile);
            this.groupBoxPacketSelect.Controls.Add(this.radioButtonSelectSelected);
            this.groupBoxPacketSelect.Controls.Add(this.radioButtonSelectFilter);
            this.groupBoxPacketSelect.Controls.Add(this.radioButtonSelectAll);
            this.groupBoxPacketSelect.Location = new System.Drawing.Point(14, 14);
            this.groupBoxPacketSelect.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.groupBoxPacketSelect.Name = "groupBoxPacketSelect";
            this.groupBoxPacketSelect.Padding = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.groupBoxPacketSelect.Size = new System.Drawing.Size(416, 135);
            this.groupBoxPacketSelect.TabIndex = 0;
            this.groupBoxPacketSelect.TabStop = false;
            this.groupBoxPacketSelect.Text = "Select packets";
            // 
            // radioButtonSelectFile
            // 
            this.radioButtonSelectFile.AutoSize = true;
            this.radioButtonSelectFile.Location = new System.Drawing.Point(7, 102);
            this.radioButtonSelectFile.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.radioButtonSelectFile.Name = "radioButtonSelectFile";
            this.radioButtonSelectFile.Size = new System.Drawing.Size(77, 19);
            this.radioButtonSelectFile.TabIndex = 3;
            this.radioButtonSelectFile.TabStop = true;
            this.radioButtonSelectFile.Text = "Select File";
            this.radioButtonSelectFile.UseVisualStyleBackColor = true;
            // 
            // radioButtonSelectSelected
            // 
            this.radioButtonSelectSelected.AutoSize = true;
            this.radioButtonSelectSelected.Location = new System.Drawing.Point(7, 75);
            this.radioButtonSelectSelected.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.radioButtonSelectSelected.Name = "radioButtonSelectSelected";
            this.radioButtonSelectSelected.Size = new System.Drawing.Size(69, 19);
            this.radioButtonSelectSelected.TabIndex = 2;
            this.radioButtonSelectSelected.TabStop = true;
            this.radioButtonSelectSelected.Text = "Selected";
            this.radioButtonSelectSelected.UseVisualStyleBackColor = true;
            // 
            // radioButtonSelectFilter
            // 
            this.radioButtonSelectFilter.AutoSize = true;
            this.radioButtonSelectFilter.Location = new System.Drawing.Point(7, 48);
            this.radioButtonSelectFilter.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.radioButtonSelectFilter.Name = "radioButtonSelectFilter";
            this.radioButtonSelectFilter.Size = new System.Drawing.Size(51, 19);
            this.radioButtonSelectFilter.TabIndex = 1;
            this.radioButtonSelectFilter.TabStop = true;
            this.radioButtonSelectFilter.Text = "Filter";
            this.radioButtonSelectFilter.UseVisualStyleBackColor = true;
            // 
            // radioButtonSelectAll
            // 
            this.radioButtonSelectAll.AutoSize = true;
            this.radioButtonSelectAll.Location = new System.Drawing.Point(7, 22);
            this.radioButtonSelectAll.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.radioButtonSelectAll.Name = "radioButtonSelectAll";
            this.radioButtonSelectAll.Size = new System.Drawing.Size(39, 19);
            this.radioButtonSelectAll.TabIndex = 1;
            this.radioButtonSelectAll.TabStop = true;
            this.radioButtonSelectAll.Text = "All";
            this.radioButtonSelectAll.UseVisualStyleBackColor = true;
            // 
            // buttonOK
            // 
            this.buttonOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonOK.Location = new System.Drawing.Point(653, 238);
            this.buttonOK.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.buttonOK.Name = "buttonOK";
            this.buttonOK.Size = new System.Drawing.Size(88, 23);
            this.buttonOK.TabIndex = 1;
            this.buttonOK.Text = "OK";
            this.buttonOK.UseVisualStyleBackColor = true;
            this.buttonOK.Click += new System.EventHandler(this.buttonOK_Click);
            // 
            // checkBoxProfibus
            // 
            this.checkBoxProfibus.AutoSize = true;
            this.checkBoxProfibus.Checked = true;
            this.checkBoxProfibus.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBoxProfibus.Location = new System.Drawing.Point(14, 182);
            this.checkBoxProfibus.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.checkBoxProfibus.Name = "checkBoxProfibus";
            this.checkBoxProfibus.Size = new System.Drawing.Size(70, 19);
            this.checkBoxProfibus.TabIndex = 2;
            this.checkBoxProfibus.Text = "Profibus";
            this.checkBoxProfibus.UseVisualStyleBackColor = true;
            // 
            // checkBoxEverything
            // 
            this.checkBoxEverything.AutoSize = true;
            this.checkBoxEverything.Checked = true;
            this.checkBoxEverything.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBoxEverything.Location = new System.Drawing.Point(14, 156);
            this.checkBoxEverything.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.checkBoxEverything.Name = "checkBoxEverything";
            this.checkBoxEverything.Size = new System.Drawing.Size(82, 19);
            this.checkBoxEverything.TabIndex = 3;
            this.checkBoxEverything.Text = "Everything";
            this.checkBoxEverything.UseVisualStyleBackColor = true;
            // 
            // checkBoxSAPIdle
            // 
            this.checkBoxSAPIdle.AutoSize = true;
            this.checkBoxSAPIdle.Checked = true;
            this.checkBoxSAPIdle.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBoxSAPIdle.Location = new System.Drawing.Point(13, 207);
            this.checkBoxSAPIdle.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.checkBoxSAPIdle.Name = "checkBoxSAPIdle";
            this.checkBoxSAPIdle.Size = new System.Drawing.Size(115, 19);
            this.checkBoxSAPIdle.TabIndex = 4;
            this.checkBoxSAPIdle.Text = "SAP Idle Analysis";
            this.checkBoxSAPIdle.UseVisualStyleBackColor = true;
            // 
            // progressBar1
            // 
            this.progressBar1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.progressBar1.Location = new System.Drawing.Point(13, 238);
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(633, 23);
            this.progressBar1.TabIndex = 5;
            // 
            // Exporterer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(754, 273);
            this.Controls.Add(this.progressBar1);
            this.Controls.Add(this.checkBoxSAPIdle);
            this.Controls.Add(this.checkBoxEverything);
            this.Controls.Add(this.checkBoxProfibus);
            this.Controls.Add(this.buttonOK);
            this.Controls.Add(this.groupBoxPacketSelect);
            this.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.Name = "Exporterer";
            this.Text = "Exporterer";
            this.groupBoxPacketSelect.ResumeLayout(false);
            this.groupBoxPacketSelect.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBoxPacketSelect;
        private System.Windows.Forms.RadioButton radioButtonSelectSelected;
        private System.Windows.Forms.RadioButton radioButtonSelectFilter;
        private System.Windows.Forms.RadioButton radioButtonSelectAll;
        private System.Windows.Forms.Button buttonOK;
        private System.Windows.Forms.CheckBox checkBoxProfibus;
        private System.Windows.Forms.RadioButton radioButtonSelectFile;
        private System.Windows.Forms.CheckBox checkBoxEverything;
        private System.Windows.Forms.CheckBox checkBoxSAPIdle;
        private System.Windows.Forms.ProgressBar progressBar1;
    }
}