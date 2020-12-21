namespace IPTComShark.Export
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
            this.radioButtonSelectAll = new System.Windows.Forms.RadioButton();
            this.radioButtonSelectFilter = new System.Windows.Forms.RadioButton();
            this.radioButtonSelectSelected = new System.Windows.Forms.RadioButton();
            this.buttonOK = new System.Windows.Forms.Button();
            this.checkBoxProfibus = new System.Windows.Forms.CheckBox();
            this.groupBoxPacketSelect.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBoxPacketSelect
            // 
            this.groupBoxPacketSelect.Controls.Add(this.radioButtonSelectSelected);
            this.groupBoxPacketSelect.Controls.Add(this.radioButtonSelectFilter);
            this.groupBoxPacketSelect.Controls.Add(this.radioButtonSelectAll);
            this.groupBoxPacketSelect.Location = new System.Drawing.Point(12, 12);
            this.groupBoxPacketSelect.Name = "groupBoxPacketSelect";
            this.groupBoxPacketSelect.Size = new System.Drawing.Size(357, 94);
            this.groupBoxPacketSelect.TabIndex = 0;
            this.groupBoxPacketSelect.TabStop = false;
            this.groupBoxPacketSelect.Text = "Select packets";
            // 
            // radioButtonSelectAll
            // 
            this.radioButtonSelectAll.AutoSize = true;
            this.radioButtonSelectAll.Location = new System.Drawing.Point(6, 19);
            this.radioButtonSelectAll.Name = "radioButtonSelectAll";
            this.radioButtonSelectAll.Size = new System.Drawing.Size(36, 17);
            this.radioButtonSelectAll.TabIndex = 1;
            this.radioButtonSelectAll.TabStop = true;
            this.radioButtonSelectAll.Text = "All";
            this.radioButtonSelectAll.UseVisualStyleBackColor = true;
            // 
            // radioButtonSelectFilter
            // 
            this.radioButtonSelectFilter.AutoSize = true;
            this.radioButtonSelectFilter.Location = new System.Drawing.Point(6, 42);
            this.radioButtonSelectFilter.Name = "radioButtonSelectFilter";
            this.radioButtonSelectFilter.Size = new System.Drawing.Size(47, 17);
            this.radioButtonSelectFilter.TabIndex = 1;
            this.radioButtonSelectFilter.TabStop = true;
            this.radioButtonSelectFilter.Text = "Filter";
            this.radioButtonSelectFilter.UseVisualStyleBackColor = true;
            // 
            // radioButtonSelectSelected
            // 
            this.radioButtonSelectSelected.AutoSize = true;
            this.radioButtonSelectSelected.Location = new System.Drawing.Point(6, 65);
            this.radioButtonSelectSelected.Name = "radioButtonSelectSelected";
            this.radioButtonSelectSelected.Size = new System.Drawing.Size(67, 17);
            this.radioButtonSelectSelected.TabIndex = 2;
            this.radioButtonSelectSelected.TabStop = true;
            this.radioButtonSelectSelected.Text = "Selected";
            this.radioButtonSelectSelected.UseVisualStyleBackColor = true;
            // 
            // buttonOK
            // 
            this.buttonOK.Location = new System.Drawing.Point(559, 160);
            this.buttonOK.Name = "buttonOK";
            this.buttonOK.Size = new System.Drawing.Size(75, 23);
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
            this.checkBoxProfibus.Location = new System.Drawing.Point(12, 112);
            this.checkBoxProfibus.Name = "checkBoxProfibus";
            this.checkBoxProfibus.Size = new System.Drawing.Size(64, 17);
            this.checkBoxProfibus.TabIndex = 2;
            this.checkBoxProfibus.Text = "Profibus";
            this.checkBoxProfibus.UseVisualStyleBackColor = true;
            // 
            // Exporterer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(646, 195);
            this.Controls.Add(this.checkBoxProfibus);
            this.Controls.Add(this.buttonOK);
            this.Controls.Add(this.groupBoxPacketSelect);
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
    }
}