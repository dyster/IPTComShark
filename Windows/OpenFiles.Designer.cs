namespace IPTComShark.Windows
{
    partial class OpenFiles
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
            this.labelDateFrom = new System.Windows.Forms.Label();
            this.labelDateTo = new System.Windows.Forms.Label();
            this.buttonOK = new System.Windows.Forms.Button();
            this.dateTimePickerFrom = new System.Windows.Forms.DateTimePicker();
            this.dateTimePickerTo = new System.Windows.Forms.DateTimePicker();
            this.dateTimePickerFromTime = new System.Windows.Forms.DateTimePicker();
            this.dateTimePickerToTime = new System.Windows.Forms.DateTimePicker();
            this.SuspendLayout();
            // 
            // labelDateFrom
            // 
            this.labelDateFrom.AutoSize = true;
            this.labelDateFrom.Location = new System.Drawing.Point(16, 11);
            this.labelDateFrom.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.labelDateFrom.Name = "labelDateFrom";
            this.labelDateFrom.Size = new System.Drawing.Size(98, 16);
            this.labelDateFrom.TabIndex = 0;
            this.labelDateFrom.Text = "labelDateFrom";
            // 
            // labelDateTo
            // 
            this.labelDateTo.AutoSize = true;
            this.labelDateTo.Location = new System.Drawing.Point(408, 11);
            this.labelDateTo.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.labelDateTo.Name = "labelDateTo";
            this.labelDateTo.Size = new System.Drawing.Size(84, 16);
            this.labelDateTo.TabIndex = 1;
            this.labelDateTo.Text = "labelDateTo";
            // 
            // buttonOK
            // 
            this.buttonOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonOK.Location = new System.Drawing.Point(697, 133);
            this.buttonOK.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.buttonOK.Name = "buttonOK";
            this.buttonOK.Size = new System.Drawing.Size(100, 28);
            this.buttonOK.TabIndex = 2;
            this.buttonOK.Text = "OK";
            this.buttonOK.UseVisualStyleBackColor = true;
            this.buttonOK.Click += new System.EventHandler(this.buttonOK_Click);
            // 
            // dateTimePickerFrom
            // 
            this.dateTimePickerFrom.Location = new System.Drawing.Point(16, 62);
            this.dateTimePickerFrom.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.dateTimePickerFrom.Name = "dateTimePickerFrom";
            this.dateTimePickerFrom.Size = new System.Drawing.Size(387, 22);
            this.dateTimePickerFrom.TabIndex = 3;
            // 
            // dateTimePickerTo
            // 
            this.dateTimePickerTo.Location = new System.Drawing.Point(412, 62);
            this.dateTimePickerTo.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.dateTimePickerTo.Name = "dateTimePickerTo";
            this.dateTimePickerTo.Size = new System.Drawing.Size(387, 22);
            this.dateTimePickerTo.TabIndex = 4;
            // 
            // dateTimePickerFromTime
            // 
            this.dateTimePickerFromTime.Format = System.Windows.Forms.DateTimePickerFormat.Time;
            this.dateTimePickerFromTime.Location = new System.Drawing.Point(16, 94);
            this.dateTimePickerFromTime.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.dateTimePickerFromTime.Name = "dateTimePickerFromTime";
            this.dateTimePickerFromTime.Size = new System.Drawing.Size(387, 22);
            this.dateTimePickerFromTime.TabIndex = 5;
            // 
            // dateTimePickerToTime
            // 
            this.dateTimePickerToTime.Format = System.Windows.Forms.DateTimePickerFormat.Time;
            this.dateTimePickerToTime.Location = new System.Drawing.Point(412, 94);
            this.dateTimePickerToTime.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.dateTimePickerToTime.Name = "dateTimePickerToTime";
            this.dateTimePickerToTime.Size = new System.Drawing.Size(387, 22);
            this.dateTimePickerToTime.TabIndex = 6;
            // 
            // OpenFiles
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(810, 174);
            this.Controls.Add(this.dateTimePickerToTime);
            this.Controls.Add(this.dateTimePickerFromTime);
            this.Controls.Add(this.dateTimePickerTo);
            this.Controls.Add(this.dateTimePickerFrom);
            this.Controls.Add(this.buttonOK);
            this.Controls.Add(this.labelDateTo);
            this.Controls.Add(this.labelDateFrom);
            this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.Name = "OpenFiles";
            this.Text = "OpenFiles";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label labelDateFrom;
        private System.Windows.Forms.Label labelDateTo;
        private System.Windows.Forms.Button buttonOK;
        private System.Windows.Forms.DateTimePicker dateTimePickerFrom;
        private System.Windows.Forms.DateTimePicker dateTimePickerTo;
        private System.Windows.Forms.DateTimePicker dateTimePickerFromTime;
        private System.Windows.Forms.DateTimePicker dateTimePickerToTime;
    }
}