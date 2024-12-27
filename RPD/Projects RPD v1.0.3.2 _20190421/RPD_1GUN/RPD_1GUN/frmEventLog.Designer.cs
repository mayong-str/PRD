namespace RPD_1GUN
{
    partial class frmEventLog
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
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.btn_Export = new System.Windows.Forms.Button();
            this.dgvEventLog = new System.Windows.Forms.DataGridView();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.chkOp = new System.Windows.Forms.CheckBox();
            this.chkEvent = new System.Windows.Forms.CheckBox();
            this.chkWarning = new System.Windows.Forms.CheckBox();
            this.chkAlarm = new System.Windows.Forms.CheckBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btnQuery = new System.Windows.Forms.Button();
            this.CalEndDate = new System.Windows.Forms.MonthCalendar();
            this.CalStartDate = new System.Windows.Forms.MonthCalendar();
            this.cboEndTime = new System.Windows.Forms.ComboBox();
            this.cboStartTime = new System.Windows.Forms.ComboBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.lstModule = new System.Windows.Forms.ListBox();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvEventLog)).BeginInit();
            this.groupBox3.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.btn_Export);
            this.groupBox2.Controls.Add(this.dgvEventLog);
            this.groupBox2.Controls.Add(this.groupBox3);
            this.groupBox2.Font = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.groupBox2.Location = new System.Drawing.Point(353, 4);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(1535, 864);
            this.groupBox2.TabIndex = 34;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Event Logs Details";
            // 
            // btn_Export
            // 
            this.btn_Export.Font = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.btn_Export.Location = new System.Drawing.Point(1368, 72);
            this.btn_Export.Name = "btn_Export";
            this.btn_Export.Size = new System.Drawing.Size(134, 31);
            this.btn_Export.TabIndex = 33;
            this.btn_Export.Text = "Export Data";
            this.btn_Export.UseVisualStyleBackColor = true;
            this.btn_Export.Click += new System.EventHandler(this.btn_Export_Click);
            // 
            // dgvEventLog
            // 
            this.dgvEventLog.AllowUserToAddRows = false;
            this.dgvEventLog.AllowUserToDeleteRows = false;
            this.dgvEventLog.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvEventLog.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvEventLog.Location = new System.Drawing.Point(10, 109);
            this.dgvEventLog.Name = "dgvEventLog";
            this.dgvEventLog.RowTemplate.Height = 24;
            this.dgvEventLog.Size = new System.Drawing.Size(1492, 749);
            this.dgvEventLog.TabIndex = 32;
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.chkOp);
            this.groupBox3.Controls.Add(this.chkEvent);
            this.groupBox3.Controls.Add(this.chkWarning);
            this.groupBox3.Controls.Add(this.chkAlarm);
            this.groupBox3.Font = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.groupBox3.Location = new System.Drawing.Point(6, 23);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(479, 77);
            this.groupBox3.TabIndex = 29;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Select Log Type to Display";
            // 
            // chkOp
            // 
            this.chkOp.AutoSize = true;
            this.chkOp.Checked = true;
            this.chkOp.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkOp.Location = new System.Drawing.Point(364, 32);
            this.chkOp.Name = "chkOp";
            this.chkOp.Size = new System.Drawing.Size(105, 24);
            this.chkOp.TabIndex = 20;
            this.chkOp.Text = "Operation";
            this.chkOp.UseVisualStyleBackColor = true;
            // 
            // chkEvent
            // 
            this.chkEvent.AutoSize = true;
            this.chkEvent.Checked = true;
            this.chkEvent.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkEvent.Location = new System.Drawing.Point(261, 34);
            this.chkEvent.Name = "chkEvent";
            this.chkEvent.Size = new System.Drawing.Size(70, 24);
            this.chkEvent.TabIndex = 19;
            this.chkEvent.Text = "Event";
            this.chkEvent.UseVisualStyleBackColor = true;
            // 
            // chkWarning
            // 
            this.chkWarning.AutoSize = true;
            this.chkWarning.Checked = true;
            this.chkWarning.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkWarning.Location = new System.Drawing.Point(132, 34);
            this.chkWarning.Name = "chkWarning";
            this.chkWarning.Size = new System.Drawing.Size(93, 24);
            this.chkWarning.TabIndex = 18;
            this.chkWarning.Text = "Warning";
            this.chkWarning.UseVisualStyleBackColor = true;
            // 
            // chkAlarm
            // 
            this.chkAlarm.AutoSize = true;
            this.chkAlarm.Checked = true;
            this.chkAlarm.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkAlarm.Location = new System.Drawing.Point(17, 34);
            this.chkAlarm.Name = "chkAlarm";
            this.chkAlarm.Size = new System.Drawing.Size(73, 24);
            this.chkAlarm.TabIndex = 17;
            this.chkAlarm.Text = "Alarm";
            this.chkAlarm.UseVisualStyleBackColor = true;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.btnQuery);
            this.groupBox1.Controls.Add(this.CalEndDate);
            this.groupBox1.Controls.Add(this.CalStartDate);
            this.groupBox1.Controls.Add(this.cboEndTime);
            this.groupBox1.Controls.Add(this.cboStartTime);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.lstModule);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Font = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.groupBox1.Location = new System.Drawing.Point(11, 3);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(336, 865);
            this.groupBox1.TabIndex = 33;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Select Data File to View";
            // 
            // btnQuery
            // 
            this.btnQuery.Font = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.btnQuery.Location = new System.Drawing.Point(50, 694);
            this.btnQuery.Name = "btnQuery";
            this.btnQuery.Size = new System.Drawing.Size(218, 31);
            this.btnQuery.TabIndex = 30;
            this.btnQuery.Text = "Query";
            this.btnQuery.UseVisualStyleBackColor = true;
            this.btnQuery.Click += new System.EventHandler(this.btnQuery_Click);
            // 
            // CalEndDate
            // 
            this.CalEndDate.Location = new System.Drawing.Point(30, 470);
            this.CalEndDate.Name = "CalEndDate";
            this.CalEndDate.TabIndex = 29;
            this.CalEndDate.TodayDate = new System.DateTime(((long)(0)));
            // 
            // CalStartDate
            // 
            this.CalStartDate.Location = new System.Drawing.Point(30, 231);
            this.CalStartDate.Name = "CalStartDate";
            this.CalStartDate.TabIndex = 28;
            this.CalStartDate.TodayDate = new System.DateTime(((long)(0)));
            // 
            // cboEndTime
            // 
            this.cboEndTime.FormattingEnabled = true;
            this.cboEndTime.Location = new System.Drawing.Point(237, 654);
            this.cboEndTime.Name = "cboEndTime";
            this.cboEndTime.Size = new System.Drawing.Size(69, 28);
            this.cboEndTime.TabIndex = 27;
            this.cboEndTime.Text = "00";
            // 
            // cboStartTime
            // 
            this.cboStartTime.FormattingEnabled = true;
            this.cboStartTime.Location = new System.Drawing.Point(243, 414);
            this.cboStartTime.Name = "cboStartTime";
            this.cboStartTime.Size = new System.Drawing.Size(69, 28);
            this.cboStartTime.TabIndex = 26;
            this.cboStartTime.Text = "00";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.label5.Location = new System.Drawing.Point(5, 657);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(223, 20);
            this.label5.TabIndex = 24;
            this.label5.Text = "5). Select a End Time to View";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.label4.Location = new System.Drawing.Point(6, 448);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(222, 20);
            this.label4.TabIndex = 23;
            this.label4.Text = "4). Select a End Date to View";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.label3.Location = new System.Drawing.Point(6, 417);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(230, 20);
            this.label3.TabIndex = 22;
            this.label3.Text = "3). Select a Start Time to View";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.label2.Location = new System.Drawing.Point(5, 210);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(229, 20);
            this.label2.TabIndex = 20;
            this.label2.Text = "2). Select a Start Date to View";
            // 
            // lstModule
            // 
            this.lstModule.FormattingEnabled = true;
            this.lstModule.ItemHeight = 20;
            this.lstModule.Location = new System.Drawing.Point(30, 55);
            this.lstModule.Name = "lstModule";
            this.lstModule.SelectionMode = System.Windows.Forms.SelectionMode.MultiSimple;
            this.lstModule.Size = new System.Drawing.Size(177, 124);
            this.lstModule.TabIndex = 19;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.label1.Location = new System.Drawing.Point(6, 29);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(203, 20);
            this.label1.TabIndex = 18;
            this.label1.Text = "1). Select a Object to View";
            // 
            // frmEventLog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1556, 880);
            this.ControlBox = false;
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "frmEventLog";
            this.Text = "fraEventLog";
            this.Load += new System.EventHandler(this.frmEventLog_Load);
            this.Leave += new System.EventHandler(this.frmEventLog_Leave);
            this.groupBox2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvEventLog)).EndInit();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Button btn_Export;
        private System.Windows.Forms.DataGridView dgvEventLog;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.CheckBox chkOp;
        private System.Windows.Forms.CheckBox chkEvent;
        private System.Windows.Forms.CheckBox chkWarning;
        private System.Windows.Forms.CheckBox chkAlarm;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button btnQuery;
        private System.Windows.Forms.MonthCalendar CalEndDate;
        private System.Windows.Forms.MonthCalendar CalStartDate;
        private System.Windows.Forms.ComboBox cboEndTime;
        private System.Windows.Forms.ComboBox cboStartTime;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ListBox lstModule;
        private System.Windows.Forms.Label label1;
    }
}