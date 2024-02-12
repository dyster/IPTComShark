namespace TrainShark.Windows
{
    partial class RemoteCap
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
            this.components = new System.ComponentModel.Container();
            this.listBox1 = new System.Windows.Forms.ListBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.buttonGO = new System.Windows.Forms.Button();
            this.backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
            this.labelReadData = new System.Windows.Forms.Label();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.buttonStop = new System.Windows.Forms.Button();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.textBoxPrefix = new System.Windows.Forms.TextBox();
            this.textBoxFolder = new System.Windows.Forms.TextBox();
            this.textBoxPassword = new System.Windows.Forms.TextBox();
            this.textBoxUsername = new System.Windows.Forms.TextBox();
            this.textBoxPort = new System.Windows.Forms.TextBox();
            this.textBoxIP = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.buttonOpenLastFile = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // listBox1
            // 
            this.listBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.listBox1.FormattingEnabled = true;
            this.listBox1.Location = new System.Drawing.Point(0, 182);
            this.listBox1.Name = "listBox1";
            this.listBox1.Size = new System.Drawing.Size(800, 264);
            this.listBox1.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 15);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(17, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "IP";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 41);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(26, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "Port";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 93);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(53, 13);
            this.label3.TabIndex = 7;
            this.label3.Text = "Password";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(12, 67);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(55, 13);
            this.label4.TabIndex = 6;
            this.label4.Text = "Username";
            // 
            // buttonGO
            // 
            this.buttonGO.Location = new System.Drawing.Point(199, 36);
            this.buttonGO.Name = "buttonGO";
            this.buttonGO.Size = new System.Drawing.Size(75, 23);
            this.buttonGO.TabIndex = 9;
            this.buttonGO.Text = "GO";
            this.buttonGO.UseVisualStyleBackColor = true;
            this.buttonGO.Click += new System.EventHandler(this.ButtonGO_Click);
            // 
            // backgroundWorker1
            // 
            this.backgroundWorker1.DoWork += new System.ComponentModel.DoWorkEventHandler(this.BackgroundWorker1_DoWork);
            // 
            // labelReadData
            // 
            this.labelReadData.AutoSize = true;
            this.labelReadData.Location = new System.Drawing.Point(196, 15);
            this.labelReadData.Name = "labelReadData";
            this.labelReadData.Size = new System.Drawing.Size(91, 13);
            this.labelReadData.TabIndex = 10;
            this.labelReadData.Text = "Data Received: 0";
            // 
            // timer1
            // 
            this.timer1.Enabled = true;
            this.timer1.Interval = 1000;
            this.timer1.Tick += new System.EventHandler(this.Timer1_Tick);
            // 
            // buttonStop
            // 
            this.buttonStop.Location = new System.Drawing.Point(199, 62);
            this.buttonStop.Name = "buttonStop";
            this.buttonStop.Size = new System.Drawing.Size(75, 23);
            this.buttonStop.TabIndex = 11;
            this.buttonStop.Text = "Stop";
            this.buttonStop.UseVisualStyleBackColor = true;
            this.buttonStop.Click += new System.EventHandler(this.ButtonStop_Click);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(292, 41);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(64, 13);
            this.label5.TabIndex = 13;
            this.label5.Text = "Save Folder";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(304, 67);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(52, 13);
            this.label6.TabIndex = 14;
            this.label6.Text = "File Prefix";
            // 
            // textBoxPrefix
            // 
            this.textBoxPrefix.DataBindings.Add(new System.Windows.Forms.Binding("Text", global::TrainShark.Properties.Settings.Default, "RemoteCapPrefix", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.textBoxPrefix.Location = new System.Drawing.Point(362, 64);
            this.textBoxPrefix.Name = "textBoxPrefix";
            this.textBoxPrefix.Size = new System.Drawing.Size(355, 20);
            this.textBoxPrefix.TabIndex = 15;
            this.textBoxPrefix.Text = global::TrainShark.Properties.Settings.Default.RemoteCapPrefix;
            // 
            // textBoxFolder
            // 
            this.textBoxFolder.DataBindings.Add(new System.Windows.Forms.Binding("Text", global::TrainShark.Properties.Settings.Default, "RemoteCapFolder", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.textBoxFolder.Location = new System.Drawing.Point(362, 38);
            this.textBoxFolder.Name = "textBoxFolder";
            this.textBoxFolder.Size = new System.Drawing.Size(355, 20);
            this.textBoxFolder.TabIndex = 12;
            this.textBoxFolder.Text = global::TrainShark.Properties.Settings.Default.RemoteCapFolder;
            // 
            // textBoxPassword
            // 
            this.textBoxPassword.DataBindings.Add(new System.Windows.Forms.Binding("Text", global::TrainShark.Properties.Settings.Default, "RemoteCapPassword", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.textBoxPassword.Location = new System.Drawing.Point(90, 90);
            this.textBoxPassword.Name = "textBoxPassword";
            this.textBoxPassword.Size = new System.Drawing.Size(100, 20);
            this.textBoxPassword.TabIndex = 8;
            this.textBoxPassword.Text = global::TrainShark.Properties.Settings.Default.RemoteCapPassword;
            // 
            // textBoxUsername
            // 
            this.textBoxUsername.DataBindings.Add(new System.Windows.Forms.Binding("Text", global::TrainShark.Properties.Settings.Default, "RemoteCapUsername", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.textBoxUsername.Location = new System.Drawing.Point(90, 64);
            this.textBoxUsername.Name = "textBoxUsername";
            this.textBoxUsername.Size = new System.Drawing.Size(100, 20);
            this.textBoxUsername.TabIndex = 5;
            this.textBoxUsername.Text = global::TrainShark.Properties.Settings.Default.RemoteCapUsername;
            // 
            // textBoxPort
            // 
            this.textBoxPort.DataBindings.Add(new System.Windows.Forms.Binding("Text", global::TrainShark.Properties.Settings.Default, "RemoteCapPort", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.textBoxPort.Location = new System.Drawing.Point(90, 38);
            this.textBoxPort.Name = "textBoxPort";
            this.textBoxPort.Size = new System.Drawing.Size(100, 20);
            this.textBoxPort.TabIndex = 4;
            this.textBoxPort.Text = global::TrainShark.Properties.Settings.Default.RemoteCapPort;
            // 
            // textBoxIP
            // 
            this.textBoxIP.DataBindings.Add(new System.Windows.Forms.Binding("Text", global::TrainShark.Properties.Settings.Default, "RemoteCapIP", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.textBoxIP.Location = new System.Drawing.Point(90, 12);
            this.textBoxIP.Name = "textBoxIP";
            this.textBoxIP.Size = new System.Drawing.Size(100, 20);
            this.textBoxIP.TabIndex = 1;
            this.textBoxIP.Text = global::TrainShark.Properties.Settings.Default.RemoteCapIP;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(359, 93);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(347, 26);
            this.label7.TabIndex = 16;
            this.label7.Text = "Numbered files will be created in the Save Folder according to File Prefix\r\n(if m" +
    "ultiple files are recorded)";
            // 
            // buttonOpenLastFile
            // 
            this.buttonOpenLastFile.Location = new System.Drawing.Point(362, 123);
            this.buttonOpenLastFile.Name = "buttonOpenLastFile";
            this.buttonOpenLastFile.Size = new System.Drawing.Size(85, 23);
            this.buttonOpenLastFile.TabIndex = 17;
            this.buttonOpenLastFile.Text = "Open Last File";
            this.buttonOpenLastFile.UseVisualStyleBackColor = true;
            this.buttonOpenLastFile.Click += new System.EventHandler(this.ButtonOpenLastFile_Click);
            // 
            // RemoteCap
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.buttonOpenLastFile);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.textBoxPrefix);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.textBoxFolder);
            this.Controls.Add(this.buttonStop);
            this.Controls.Add(this.labelReadData);
            this.Controls.Add(this.buttonGO);
            this.Controls.Add(this.textBoxPassword);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.textBoxUsername);
            this.Controls.Add(this.textBoxPort);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.textBoxIP);
            this.Controls.Add(this.listBox1);
            this.Name = "RemoteCap";
            this.Text = "RemoteCap";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.RemoteCap_FormClosing);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListBox listBox1;
        private System.Windows.Forms.TextBox textBoxIP;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox textBoxPort;
        private System.Windows.Forms.TextBox textBoxPassword;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox textBoxUsername;
        private System.Windows.Forms.Button buttonGO;
        private System.ComponentModel.BackgroundWorker backgroundWorker1;
        private System.Windows.Forms.Label labelReadData;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.Button buttonStop;
        private System.Windows.Forms.TextBox textBoxFolder;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox textBoxPrefix;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Button buttonOpenLastFile;
    }
}