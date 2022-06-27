namespace IPTComShark.FileManager
{
    partial class DatePicker
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
            this.dateTimePickerToTime = new System.Windows.Forms.DateTimePicker();
            this.dateTimePickerFromTime = new System.Windows.Forms.DateTimePicker();
            this.dateTimePickerTo = new System.Windows.Forms.DateTimePicker();
            this.dateTimePickerFrom = new System.Windows.Forms.DateTimePicker();
            this.labelDateTo = new System.Windows.Forms.Label();
            this.labelDateFrom = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // dateTimePickerToTime
            // 
            this.dateTimePickerToTime.Format = System.Windows.Forms.DateTimePickerFormat.Time;
            this.dateTimePickerToTime.Location = new System.Drawing.Point(300, 61);
            this.dateTimePickerToTime.Name = "dateTimePickerToTime";
            this.dateTimePickerToTime.Size = new System.Drawing.Size(291, 20);
            this.dateTimePickerToTime.TabIndex = 12;
            this.dateTimePickerToTime.ValueChanged += new System.EventHandler(this.DateTimePickerFrom_ValueChanged);
            // 
            // dateTimePickerFromTime
            // 
            this.dateTimePickerFromTime.Format = System.Windows.Forms.DateTimePickerFormat.Time;
            this.dateTimePickerFromTime.Location = new System.Drawing.Point(3, 61);
            this.dateTimePickerFromTime.Name = "dateTimePickerFromTime";
            this.dateTimePickerFromTime.Size = new System.Drawing.Size(291, 20);
            this.dateTimePickerFromTime.TabIndex = 11;
            this.dateTimePickerFromTime.ValueChanged += new System.EventHandler(this.DateTimePickerFrom_ValueChanged);
            // 
            // dateTimePickerTo
            // 
            this.dateTimePickerTo.Location = new System.Drawing.Point(300, 35);
            this.dateTimePickerTo.Name = "dateTimePickerTo";
            this.dateTimePickerTo.Size = new System.Drawing.Size(291, 20);
            this.dateTimePickerTo.TabIndex = 10;
            this.dateTimePickerTo.ValueChanged += new System.EventHandler(this.DateTimePickerFrom_ValueChanged);
            // 
            // dateTimePickerFrom
            // 
            this.dateTimePickerFrom.Location = new System.Drawing.Point(3, 35);
            this.dateTimePickerFrom.Name = "dateTimePickerFrom";
            this.dateTimePickerFrom.Size = new System.Drawing.Size(291, 20);
            this.dateTimePickerFrom.TabIndex = 9;
            this.dateTimePickerFrom.ValueChanged += new System.EventHandler(this.DateTimePickerFrom_ValueChanged);
            // 
            // labelDateTo
            // 
            this.labelDateTo.AutoSize = true;
            this.labelDateTo.Location = new System.Drawing.Point(297, 9);
            this.labelDateTo.Name = "labelDateTo";
            this.labelDateTo.Size = new System.Drawing.Size(82, 13);
            this.labelDateTo.TabIndex = 8;
            this.labelDateTo.Text = "Waiting for data";
            // 
            // labelDateFrom
            // 
            this.labelDateFrom.AutoSize = true;
            this.labelDateFrom.Location = new System.Drawing.Point(3, 9);
            this.labelDateFrom.Name = "labelDateFrom";
            this.labelDateFrom.Size = new System.Drawing.Size(82, 13);
            this.labelDateFrom.TabIndex = 7;
            this.labelDateFrom.Text = "Waiting for data";
            // 
            // DatePicker
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.dateTimePickerToTime);
            this.Controls.Add(this.dateTimePickerFromTime);
            this.Controls.Add(this.dateTimePickerTo);
            this.Controls.Add(this.dateTimePickerFrom);
            this.Controls.Add(this.labelDateTo);
            this.Controls.Add(this.labelDateFrom);
            this.Name = "DatePicker";
            this.Size = new System.Drawing.Size(620, 90);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DateTimePicker dateTimePickerToTime;
        private System.Windows.Forms.DateTimePicker dateTimePickerFromTime;
        private System.Windows.Forms.DateTimePicker dateTimePickerTo;
        private System.Windows.Forms.DateTimePicker dateTimePickerFrom;
        private System.Windows.Forms.Label labelDateTo;
        private System.Windows.Forms.Label labelDateFrom;
    }
}
