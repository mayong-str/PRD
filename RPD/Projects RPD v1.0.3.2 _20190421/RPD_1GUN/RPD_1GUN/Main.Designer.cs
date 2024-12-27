namespace RPD_1GUN
{
    partial class Main
    {
        /// <summary>
        /// 設計工具所需的變數。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清除任何使用中的資源。
        /// </summary>
        /// <param name="disposing">如果應該處置 Managed 資源則為 true，否則為 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form 設計工具產生的程式碼

        /// <summary>
        /// 此為設計工具支援所需的方法 - 請勿使用程式碼編輯器
        /// 修改這個方法的內容。
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Main));
            this.panel1 = new System.Windows.Forms.Panel();
            this.lblTimer = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btnLoadRecipe = new System.Windows.Forms.Button();
            this.ckbForceRecord = new System.Windows.Forms.CheckBox();
            this.lblCurrentRecipe = new System.Windows.Forms.Label();
            this.cmbRecipe = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.lblEQStatus_Dis = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.lblEQStatus = new System.Windows.Forms.Label();
            this.pnlAlarm = new System.Windows.Forms.Panel();
            this.lblAlarm = new System.Windows.Forms.Label();
            this.btnClear = new System.Windows.Forms.Button();
            this.groupBox8 = new System.Windows.Forms.GroupBox();
            this.label1 = new System.Windows.Forms.Label();
            this.lblLoginID = new System.Windows.Forms.Label();
            this.lblVersion = new System.Windows.Forms.Label();
            this.btnUser = new System.Windows.Forms.Button();
            this.btnExit = new System.Windows.Forms.Button();
            this.btnLogin = new System.Windows.Forms.Button();
            this.groupBox14 = new System.Windows.Forms.GroupBox();
            this.btnForceExclude = new System.Windows.Forms.Button();
            this.btnManualPage = new System.Windows.Forms.Button();
            this.btnTactTimeReset = new System.Windows.Forms.Button();
            this.btnAbort = new System.Windows.Forms.Button();
            this.txtAlarmMsg = new System.Windows.Forms.TextBox();
            this.txtEventMsg = new System.Windows.Forms.TextBox();
            this.btnBuzzerOff = new System.Windows.Forms.Button();
            this.btnInitial = new System.Windows.Forms.Button();
            this.btnStop = new System.Windows.Forms.Button();
            this.btnStart = new System.Windows.Forms.Button();
            this.btnManual = new System.Windows.Forms.Button();
            this.btnAuto = new System.Windows.Forms.Button();
            this.label37 = new System.Windows.Forms.Label();
            this.label36 = new System.Windows.Forms.Label();
            this.groupBox9 = new System.Windows.Forms.GroupBox();
            this.btnHandShake = new System.Windows.Forms.Button();
            this.btnMain = new System.Windows.Forms.Button();
            this.btnEventLog = new System.Windows.Forms.Button();
            this.btnAlarm = new System.Windows.Forms.Button();
            this.groupBox13 = new System.Windows.Forms.GroupBox();
            this.label45 = new System.Windows.Forms.Label();
            this.label44 = new System.Windows.Forms.Label();
            this.label42 = new System.Windows.Forms.Label();
            this.shapeContainer1 = new Microsoft.VisualBasic.PowerPacks.ShapeContainer();
            this.os_Warn = new Microsoft.VisualBasic.PowerPacks.OvalShape();
            this.os_Alarm = new Microsoft.VisualBasic.PowerPacks.OvalShape();
            this.os_PLC = new Microsoft.VisualBasic.PowerPacks.OvalShape();
            this.DisplayTimer = new System.Windows.Forms.Timer(this.components);
            this.panel1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.pnlAlarm.SuspendLayout();
            this.groupBox8.SuspendLayout();
            this.groupBox14.SuspendLayout();
            this.groupBox9.SuspendLayout();
            this.groupBox13.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.AutoScroll = true;
            this.panel1.BackColor = System.Drawing.Color.LightSteelBlue;
            this.panel1.Controls.Add(this.lblTimer);
            this.panel1.Controls.Add(this.groupBox1);
            this.panel1.Controls.Add(this.pnlAlarm);
            this.panel1.Controls.Add(this.groupBox8);
            this.panel1.Controls.Add(this.groupBox14);
            this.panel1.Controls.Add(this.groupBox9);
            this.panel1.Controls.Add(this.groupBox13);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1540, 132);
            this.panel1.TabIndex = 3;
            // 
            // lblTimer
            // 
            this.lblTimer.AutoSize = true;
            this.lblTimer.BackColor = System.Drawing.SystemColors.Control;
            this.lblTimer.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblTimer.Font = new System.Drawing.Font("微軟正黑體", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.lblTimer.ForeColor = System.Drawing.Color.Red;
            this.lblTimer.Location = new System.Drawing.Point(1655, 34);
            this.lblTimer.Name = "lblTimer";
            this.lblTimer.Size = new System.Drawing.Size(127, 18);
            this.lblTimer.TabIndex = 2;
            this.lblTimer.Text = "2014/05/20 12:30:00";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.btnLoadRecipe);
            this.groupBox1.Controls.Add(this.ckbForceRecord);
            this.groupBox1.Controls.Add(this.lblCurrentRecipe);
            this.groupBox1.Controls.Add(this.cmbRecipe);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.lblEQStatus_Dis);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.lblEQStatus);
            this.groupBox1.Location = new System.Drawing.Point(270, -2);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(252, 119);
            this.groupBox1.TabIndex = 13;
            this.groupBox1.TabStop = false;
            // 
            // btnLoadRecipe
            // 
            this.btnLoadRecipe.Font = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Bold);
            this.btnLoadRecipe.Location = new System.Drawing.Point(100, 86);
            this.btnLoadRecipe.Name = "btnLoadRecipe";
            this.btnLoadRecipe.Size = new System.Drawing.Size(146, 30);
            this.btnLoadRecipe.TabIndex = 15;
            this.btnLoadRecipe.Text = "Load Recipe";
            this.btnLoadRecipe.UseVisualStyleBackColor = true;
            this.btnLoadRecipe.Click += new System.EventHandler(this.btnLoadRecipe_Click);
            // 
            // ckbForceRecord
            // 
            this.ckbForceRecord.AutoSize = true;
            this.ckbForceRecord.Checked = true;
            this.ckbForceRecord.CheckState = System.Windows.Forms.CheckState.Checked;
            this.ckbForceRecord.Font = new System.Drawing.Font("微軟正黑體", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.ckbForceRecord.Location = new System.Drawing.Point(6, 95);
            this.ckbForceRecord.Name = "ckbForceRecord";
            this.ckbForceRecord.Size = new System.Drawing.Size(95, 19);
            this.ckbForceRecord.TabIndex = 140;
            this.ckbForceRecord.Text = "Force record ";
            this.ckbForceRecord.UseVisualStyleBackColor = true;
            // 
            // lblCurrentRecipe
            // 
            this.lblCurrentRecipe.AutoSize = true;
            this.lblCurrentRecipe.BackColor = System.Drawing.Color.Gold;
            this.lblCurrentRecipe.Font = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.lblCurrentRecipe.ForeColor = System.Drawing.Color.Black;
            this.lblCurrentRecipe.Location = new System.Drawing.Point(102, 61);
            this.lblCurrentRecipe.Name = "lblCurrentRecipe";
            this.lblCurrentRecipe.Size = new System.Drawing.Size(66, 21);
            this.lblCurrentRecipe.TabIndex = 139;
            this.lblCurrentRecipe.Text = "Default";
            // 
            // cmbRecipe
            // 
            this.cmbRecipe.Font = new System.Drawing.Font("微軟正黑體", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.cmbRecipe.FormattingEnabled = true;
            this.cmbRecipe.Location = new System.Drawing.Point(100, 34);
            this.cmbRecipe.Name = "cmbRecipe";
            this.cmbRecipe.Size = new System.Drawing.Size(146, 25);
            this.cmbRecipe.TabIndex = 5;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("微軟正黑體", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.label2.Location = new System.Drawing.Point(10, 40);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(85, 17);
            this.label2.TabIndex = 137;
            this.label2.Text = "Select Recipe";
            // 
            // lblEQStatus_Dis
            // 
            this.lblEQStatus_Dis.AutoSize = true;
            this.lblEQStatus_Dis.Font = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.lblEQStatus_Dis.Location = new System.Drawing.Point(6, 11);
            this.lblEQStatus_Dis.Name = "lblEQStatus_Dis";
            this.lblEQStatus_Dis.Size = new System.Drawing.Size(82, 20);
            this.lblEQStatus_Dis.TabIndex = 13;
            this.lblEQStatus_Dis.Text = "EQ Status";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("微軟正黑體", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.label3.Location = new System.Drawing.Point(2, 61);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(97, 17);
            this.label3.TabIndex = 138;
            this.label3.Text = "Current Recipe";
            // 
            // lblEQStatus
            // 
            this.lblEQStatus.AutoSize = true;
            this.lblEQStatus.BackColor = System.Drawing.Color.Black;
            this.lblEQStatus.Font = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.lblEQStatus.ForeColor = System.Drawing.Color.Lime;
            this.lblEQStatus.Location = new System.Drawing.Point(100, 10);
            this.lblEQStatus.Name = "lblEQStatus";
            this.lblEQStatus.Size = new System.Drawing.Size(45, 21);
            this.lblEQStatus.TabIndex = 14;
            this.lblEQStatus.Text = "Stop";
            // 
            // pnlAlarm
            // 
            this.pnlAlarm.AutoSize = true;
            this.pnlAlarm.BackColor = System.Drawing.Color.Lime;
            this.pnlAlarm.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlAlarm.Controls.Add(this.lblAlarm);
            this.pnlAlarm.Controls.Add(this.btnClear);
            this.pnlAlarm.Location = new System.Drawing.Point(1800, 5);
            this.pnlAlarm.Name = "pnlAlarm";
            this.pnlAlarm.Size = new System.Drawing.Size(109, 111);
            this.pnlAlarm.TabIndex = 135;
            // 
            // lblAlarm
            // 
            this.lblAlarm.AutoSize = true;
            this.lblAlarm.Font = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.lblAlarm.Location = new System.Drawing.Point(25, 33);
            this.lblAlarm.Name = "lblAlarm";
            this.lblAlarm.Size = new System.Drawing.Size(56, 21);
            this.lblAlarm.TabIndex = 13;
            this.lblAlarm.Text = "Alarm";
            // 
            // btnClear
            // 
            this.btnClear.BackColor = System.Drawing.Color.Transparent;
            this.btnClear.Font = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.btnClear.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.btnClear.Location = new System.Drawing.Point(7, 71);
            this.btnClear.Name = "btnClear";
            this.btnClear.Size = new System.Drawing.Size(97, 35);
            this.btnClear.TabIndex = 12;
            this.btnClear.Text = "Clear";
            this.btnClear.UseVisualStyleBackColor = false;
            this.btnClear.Click += new System.EventHandler(this.btnClear_Click);
            // 
            // groupBox8
            // 
            this.groupBox8.Controls.Add(this.label1);
            this.groupBox8.Controls.Add(this.lblLoginID);
            this.groupBox8.Controls.Add(this.lblVersion);
            this.groupBox8.Controls.Add(this.btnUser);
            this.groupBox8.Controls.Add(this.btnExit);
            this.groupBox8.Controls.Add(this.btnLogin);
            this.groupBox8.Location = new System.Drawing.Point(1545, -2);
            this.groupBox8.Name = "groupBox8";
            this.groupBox8.Size = new System.Drawing.Size(248, 118);
            this.groupBox8.TabIndex = 13;
            this.groupBox8.TabStop = false;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.label1.Location = new System.Drawing.Point(3, 13);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(68, 20);
            this.label1.TabIndex = 13;
            this.label1.Text = "UserID :";
            // 
            // lblLoginID
            // 
            this.lblLoginID.AutoSize = true;
            this.lblLoginID.Font = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.lblLoginID.Location = new System.Drawing.Point(130, 11);
            this.lblLoginID.Name = "lblLoginID";
            this.lblLoginID.Size = new System.Drawing.Size(0, 20);
            this.lblLoginID.TabIndex = 12;
            // 
            // lblVersion
            // 
            this.lblVersion.AutoSize = true;
            this.lblVersion.Font = new System.Drawing.Font("微軟正黑體", 9.75F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.lblVersion.ImageAlign = System.Drawing.ContentAlignment.TopLeft;
            this.lblVersion.Location = new System.Drawing.Point(4, 37);
            this.lblVersion.Name = "lblVersion";
            this.lblVersion.Size = new System.Drawing.Size(103, 17);
            this.lblVersion.TabIndex = 136;
            this.lblVersion.Text = "GUI Ver. 1.0.3.3";
            // 
            // btnUser
            // 
            this.btnUser.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnUser.BackgroundImage")));
            this.btnUser.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnUser.Location = new System.Drawing.Point(6, 53);
            this.btnUser.Name = "btnUser";
            this.btnUser.Size = new System.Drawing.Size(74, 62);
            this.btnUser.TabIndex = 11;
            this.btnUser.UseVisualStyleBackColor = true;
            this.btnUser.Visible = false;
            // 
            // btnExit
            // 
            this.btnExit.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnExit.BackgroundImage")));
            this.btnExit.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnExit.Location = new System.Drawing.Point(166, 54);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(74, 62);
            this.btnExit.TabIndex = 4;
            this.btnExit.UseVisualStyleBackColor = true;
            this.btnExit.Click += new System.EventHandler(this.btnExit_Click);
            // 
            // btnLogin
            // 
            this.btnLogin.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnLogin.BackgroundImage")));
            this.btnLogin.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnLogin.Location = new System.Drawing.Point(86, 55);
            this.btnLogin.Name = "btnLogin";
            this.btnLogin.Size = new System.Drawing.Size(74, 60);
            this.btnLogin.TabIndex = 3;
            this.btnLogin.UseVisualStyleBackColor = true;
            this.btnLogin.Click += new System.EventHandler(this.btnLogin_Click);
            // 
            // groupBox14
            // 
            this.groupBox14.Controls.Add(this.btnForceExclude);
            this.groupBox14.Controls.Add(this.btnManualPage);
            this.groupBox14.Controls.Add(this.btnTactTimeReset);
            this.groupBox14.Controls.Add(this.btnAbort);
            this.groupBox14.Controls.Add(this.txtAlarmMsg);
            this.groupBox14.Controls.Add(this.txtEventMsg);
            this.groupBox14.Controls.Add(this.btnBuzzerOff);
            this.groupBox14.Controls.Add(this.btnInitial);
            this.groupBox14.Controls.Add(this.btnStop);
            this.groupBox14.Controls.Add(this.btnStart);
            this.groupBox14.Controls.Add(this.btnManual);
            this.groupBox14.Controls.Add(this.btnAuto);
            this.groupBox14.Controls.Add(this.label37);
            this.groupBox14.Controls.Add(this.label36);
            this.groupBox14.Location = new System.Drawing.Point(683, -2);
            this.groupBox14.Name = "groupBox14";
            this.groupBox14.Size = new System.Drawing.Size(859, 119);
            this.groupBox14.TabIndex = 12;
            this.groupBox14.TabStop = false;
            // 
            // btnForceExclude
            // 
            this.btnForceExclude.BackColor = System.Drawing.Color.Transparent;
            this.btnForceExclude.Font = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.btnForceExclude.Location = new System.Drawing.Point(570, 77);
            this.btnForceExclude.Name = "btnForceExclude";
            this.btnForceExclude.Size = new System.Drawing.Size(124, 37);
            this.btnForceExclude.TabIndex = 26;
            this.btnForceExclude.Text = "Force Exclude";
            this.btnForceExclude.UseVisualStyleBackColor = false;
            // 
            // btnManualPage
            // 
            this.btnManualPage.BackColor = System.Drawing.Color.Transparent;
            this.btnManualPage.Font = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.btnManualPage.Location = new System.Drawing.Point(704, 78);
            this.btnManualPage.Name = "btnManualPage";
            this.btnManualPage.Size = new System.Drawing.Size(146, 37);
            this.btnManualPage.TabIndex = 25;
            this.btnManualPage.Text = "Manual Page";
            this.btnManualPage.UseVisualStyleBackColor = false;
            this.btnManualPage.Click += new System.EventHandler(this.btnManualPage_Click);
            // 
            // btnTactTimeReset
            // 
            this.btnTactTimeReset.BackColor = System.Drawing.Color.Transparent;
            this.btnTactTimeReset.Font = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.btnTactTimeReset.Location = new System.Drawing.Point(704, 43);
            this.btnTactTimeReset.Name = "btnTactTimeReset";
            this.btnTactTimeReset.Size = new System.Drawing.Size(146, 35);
            this.btnTactTimeReset.TabIndex = 24;
            this.btnTactTimeReset.Text = "Tact Time Reset";
            this.btnTactTimeReset.UseVisualStyleBackColor = false;
            // 
            // btnAbort
            // 
            this.btnAbort.BackColor = System.Drawing.Color.Transparent;
            this.btnAbort.Font = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.btnAbort.Location = new System.Drawing.Point(476, 77);
            this.btnAbort.Name = "btnAbort";
            this.btnAbort.Size = new System.Drawing.Size(80, 37);
            this.btnAbort.TabIndex = 22;
            this.btnAbort.Text = "Abort";
            this.btnAbort.UseVisualStyleBackColor = false;
            // 
            // txtAlarmMsg
            // 
            this.txtAlarmMsg.Font = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.txtAlarmMsg.Location = new System.Drawing.Point(102, 45);
            this.txtAlarmMsg.Name = "txtAlarmMsg";
            this.txtAlarmMsg.Size = new System.Drawing.Size(596, 29);
            this.txtAlarmMsg.TabIndex = 18;
            // 
            // txtEventMsg
            // 
            this.txtEventMsg.Font = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.txtEventMsg.Location = new System.Drawing.Point(102, 10);
            this.txtEventMsg.Name = "txtEventMsg";
            this.txtEventMsg.Size = new System.Drawing.Size(596, 29);
            this.txtEventMsg.TabIndex = 17;
            // 
            // btnBuzzerOff
            // 
            this.btnBuzzerOff.BackColor = System.Drawing.Color.Transparent;
            this.btnBuzzerOff.Font = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.btnBuzzerOff.Location = new System.Drawing.Point(704, 8);
            this.btnBuzzerOff.Name = "btnBuzzerOff";
            this.btnBuzzerOff.Size = new System.Drawing.Size(146, 35);
            this.btnBuzzerOff.TabIndex = 12;
            this.btnBuzzerOff.Text = "Buzzer Off";
            this.btnBuzzerOff.UseVisualStyleBackColor = false;
            // 
            // btnInitial
            // 
            this.btnInitial.BackColor = System.Drawing.Color.Transparent;
            this.btnInitial.Font = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.btnInitial.Location = new System.Drawing.Point(382, 77);
            this.btnInitial.Name = "btnInitial";
            this.btnInitial.Size = new System.Drawing.Size(80, 37);
            this.btnInitial.TabIndex = 16;
            this.btnInitial.Text = "Initial";
            this.btnInitial.UseVisualStyleBackColor = false;
            // 
            // btnStop
            // 
            this.btnStop.BackColor = System.Drawing.Color.Transparent;
            this.btnStop.Font = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.btnStop.Location = new System.Drawing.Point(288, 77);
            this.btnStop.Name = "btnStop";
            this.btnStop.Size = new System.Drawing.Size(80, 37);
            this.btnStop.TabIndex = 15;
            this.btnStop.Text = "Stop";
            this.btnStop.UseVisualStyleBackColor = false;
            // 
            // btnStart
            // 
            this.btnStart.BackColor = System.Drawing.Color.Transparent;
            this.btnStart.Font = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.btnStart.Location = new System.Drawing.Point(194, 77);
            this.btnStart.Name = "btnStart";
            this.btnStart.Size = new System.Drawing.Size(80, 37);
            this.btnStart.TabIndex = 14;
            this.btnStart.Text = "Start";
            this.btnStart.UseVisualStyleBackColor = false;
            // 
            // btnManual
            // 
            this.btnManual.BackColor = System.Drawing.Color.Transparent;
            this.btnManual.Font = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.btnManual.Location = new System.Drawing.Point(100, 77);
            this.btnManual.Name = "btnManual";
            this.btnManual.Size = new System.Drawing.Size(80, 37);
            this.btnManual.TabIndex = 13;
            this.btnManual.Text = "Manual";
            this.btnManual.UseVisualStyleBackColor = false;
            // 
            // btnAuto
            // 
            this.btnAuto.BackColor = System.Drawing.Color.Transparent;
            this.btnAuto.Font = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.btnAuto.Location = new System.Drawing.Point(6, 77);
            this.btnAuto.Name = "btnAuto";
            this.btnAuto.Size = new System.Drawing.Size(80, 37);
            this.btnAuto.TabIndex = 11;
            this.btnAuto.Tag = "";
            this.btnAuto.Text = "Auto";
            this.btnAuto.UseVisualStyleBackColor = false;
            // 
            // label37
            // 
            this.label37.AutoSize = true;
            this.label37.Font = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.label37.Location = new System.Drawing.Point(6, 45);
            this.label37.Name = "label37";
            this.label37.Size = new System.Drawing.Size(90, 20);
            this.label37.TabIndex = 10;
            this.label37.Text = "Alarm Msg";
            // 
            // label36
            // 
            this.label36.AutoSize = true;
            this.label36.Font = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.label36.Location = new System.Drawing.Point(6, 13);
            this.label36.Name = "label36";
            this.label36.Size = new System.Drawing.Size(87, 20);
            this.label36.TabIndex = 9;
            this.label36.Text = "Event Msg";
            // 
            // groupBox9
            // 
            this.groupBox9.Controls.Add(this.btnHandShake);
            this.groupBox9.Controls.Add(this.btnMain);
            this.groupBox9.Controls.Add(this.btnEventLog);
            this.groupBox9.Controls.Add(this.btnAlarm);
            this.groupBox9.Location = new System.Drawing.Point(528, -2);
            this.groupBox9.Name = "groupBox9";
            this.groupBox9.Size = new System.Drawing.Size(152, 119);
            this.groupBox9.TabIndex = 11;
            this.groupBox9.TabStop = false;
            // 
            // btnHandShake
            // 
            this.btnHandShake.Font = new System.Drawing.Font("微軟正黑體", 8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.btnHandShake.Location = new System.Drawing.Point(5, 63);
            this.btnHandShake.Name = "btnHandShake";
            this.btnHandShake.Size = new System.Drawing.Size(70, 52);
            this.btnHandShake.TabIndex = 14;
            this.btnHandShake.Text = "Inter Lock";
            this.btnHandShake.UseVisualStyleBackColor = true;
            this.btnHandShake.Click += new System.EventHandler(this.btnHandShake_Click);
            // 
            // btnMain
            // 
            this.btnMain.Font = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.btnMain.Location = new System.Drawing.Point(5, 11);
            this.btnMain.Name = "btnMain";
            this.btnMain.Size = new System.Drawing.Size(70, 52);
            this.btnMain.TabIndex = 12;
            this.btnMain.Text = "Main";
            this.btnMain.UseVisualStyleBackColor = true;
            this.btnMain.Click += new System.EventHandler(this.btnMain_Click);
            // 
            // btnEventLog
            // 
            this.btnEventLog.Font = new System.Drawing.Font("微軟正黑體", 8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.btnEventLog.Location = new System.Drawing.Point(77, 63);
            this.btnEventLog.Name = "btnEventLog";
            this.btnEventLog.Size = new System.Drawing.Size(70, 52);
            this.btnEventLog.TabIndex = 9;
            this.btnEventLog.Text = "Event Log";
            this.btnEventLog.UseVisualStyleBackColor = true;
            this.btnEventLog.Click += new System.EventHandler(this.btnEventLog_Click);
            // 
            // btnAlarm
            // 
            this.btnAlarm.Font = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.btnAlarm.Location = new System.Drawing.Point(76, 11);
            this.btnAlarm.Name = "btnAlarm";
            this.btnAlarm.Size = new System.Drawing.Size(70, 52);
            this.btnAlarm.TabIndex = 8;
            this.btnAlarm.Text = "Alarm";
            this.btnAlarm.UseVisualStyleBackColor = true;
            this.btnAlarm.Click += new System.EventHandler(this.btnAlarm_Click);
            // 
            // groupBox13
            // 
            this.groupBox13.Controls.Add(this.label45);
            this.groupBox13.Controls.Add(this.label44);
            this.groupBox13.Controls.Add(this.label42);
            this.groupBox13.Controls.Add(this.shapeContainer1);
            this.groupBox13.Location = new System.Drawing.Point(3, -2);
            this.groupBox13.Name = "groupBox13";
            this.groupBox13.Size = new System.Drawing.Size(267, 113);
            this.groupBox13.TabIndex = 10;
            this.groupBox13.TabStop = false;
            // 
            // label45
            // 
            this.label45.AutoSize = true;
            this.label45.Font = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.label45.Location = new System.Drawing.Point(10, 77);
            this.label45.Name = "label45";
            this.label45.Size = new System.Drawing.Size(74, 20);
            this.label45.TabIndex = 11;
            this.label45.Text = "Warning";
            // 
            // label44
            // 
            this.label44.AutoSize = true;
            this.label44.Font = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.label44.Location = new System.Drawing.Point(10, 45);
            this.label44.Name = "label44";
            this.label44.Size = new System.Drawing.Size(54, 20);
            this.label44.TabIndex = 10;
            this.label44.Text = "Alarm";
            // 
            // label42
            // 
            this.label42.AutoSize = true;
            this.label42.Font = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.label42.Location = new System.Drawing.Point(10, 13);
            this.label42.Name = "label42";
            this.label42.Size = new System.Drawing.Size(89, 20);
            this.label42.TabIndex = 8;
            this.label42.Text = "PLC Status";
            // 
            // shapeContainer1
            // 
            this.shapeContainer1.Location = new System.Drawing.Point(3, 17);
            this.shapeContainer1.Margin = new System.Windows.Forms.Padding(0);
            this.shapeContainer1.Name = "shapeContainer1";
            this.shapeContainer1.Shapes.AddRange(new Microsoft.VisualBasic.PowerPacks.Shape[] {
            this.os_Warn,
            this.os_Alarm,
            this.os_PLC});
            this.shapeContainer1.Size = new System.Drawing.Size(261, 93);
            this.shapeContainer1.TabIndex = 12;
            this.shapeContainer1.TabStop = false;
            // 
            // os_Warn
            // 
            this.os_Warn.FillColor = System.Drawing.Color.Red;
            this.os_Warn.FillStyle = Microsoft.VisualBasic.PowerPacks.FillStyle.Solid;
            this.os_Warn.Location = new System.Drawing.Point(178, 62);
            this.os_Warn.Name = "os_Warn";
            this.os_Warn.Size = new System.Drawing.Size(15, 15);
            // 
            // os_Alarm
            // 
            this.os_Alarm.FillColor = System.Drawing.Color.Red;
            this.os_Alarm.FillStyle = Microsoft.VisualBasic.PowerPacks.FillStyle.Solid;
            this.os_Alarm.Location = new System.Drawing.Point(178, 31);
            this.os_Alarm.Name = "os_Alarm";
            this.os_Alarm.Size = new System.Drawing.Size(15, 15);
            // 
            // os_PLC
            // 
            this.os_PLC.FillColor = System.Drawing.Color.Red;
            this.os_PLC.FillStyle = Microsoft.VisualBasic.PowerPacks.FillStyle.Solid;
            this.os_PLC.Location = new System.Drawing.Point(178, 0);
            this.os_PLC.Name = "os_PLC";
            this.os_PLC.Size = new System.Drawing.Size(15, 15);
            // 
            // DisplayTimer
            // 
            this.DisplayTimer.Interval = 400;
            this.DisplayTimer.Tick += new System.EventHandler(this.DisplayTimer_Tick);
            // 
            // Main
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1540, 888);
            this.ControlBox = false;
            this.Controls.Add(this.panel1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.IsMdiContainer = true;
            this.Name = "Main";
            this.Text = "RPD-1";
            this.Load += new System.EventHandler(this.Main_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.pnlAlarm.ResumeLayout(false);
            this.pnlAlarm.PerformLayout();
            this.groupBox8.ResumeLayout(false);
            this.groupBox8.PerformLayout();
            this.groupBox14.ResumeLayout(false);
            this.groupBox14.PerformLayout();
            this.groupBox9.ResumeLayout(false);
            this.groupBox13.ResumeLayout(false);
            this.groupBox13.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.GroupBox groupBox13;
        private System.Windows.Forms.Label label45;
        private System.Windows.Forms.Label label44;
        private System.Windows.Forms.Label label42;
        private System.Windows.Forms.Label lblTimer;
        private System.Windows.Forms.Label lblVersion;
        private System.Windows.Forms.Panel pnlAlarm;
        private System.Windows.Forms.Label lblAlarm;
        private System.Windows.Forms.Button btnClear;
        private System.Windows.Forms.GroupBox groupBox8;
        private System.Windows.Forms.Label lblLoginID;
        private System.Windows.Forms.Button btnUser;
        private System.Windows.Forms.Button btnExit;
        private System.Windows.Forms.Button btnLogin;
        private System.Windows.Forms.GroupBox groupBox14;
        private System.Windows.Forms.TextBox txtAlarmMsg;
        private System.Windows.Forms.TextBox txtEventMsg;
        private System.Windows.Forms.Button btnBuzzerOff;
        private System.Windows.Forms.Button btnInitial;
        private System.Windows.Forms.Button btnStop;
        private System.Windows.Forms.Button btnStart;
        private System.Windows.Forms.Button btnManual;
        private System.Windows.Forms.Button btnAuto;
        private System.Windows.Forms.Label label37;
        private System.Windows.Forms.Label label36;
        private System.Windows.Forms.GroupBox groupBox9;
        private System.Windows.Forms.Button btnHandShake;
        private System.Windows.Forms.Button btnMain;
        private System.Windows.Forms.Button btnEventLog;
        private System.Windows.Forms.Button btnAlarm;
        private System.Windows.Forms.Label label1;
        private Microsoft.VisualBasic.PowerPacks.ShapeContainer shapeContainer1;
        private Microsoft.VisualBasic.PowerPacks.OvalShape os_Warn;
        private Microsoft.VisualBasic.PowerPacks.OvalShape os_Alarm;
        private Microsoft.VisualBasic.PowerPacks.OvalShape os_PLC;
        private System.Windows.Forms.Timer DisplayTimer;
        private System.Windows.Forms.Button btnTactTimeReset;
        private System.Windows.Forms.Button btnAbort;
        private System.Windows.Forms.Label lblEQStatus_Dis;
        private System.Windows.Forms.Label lblEQStatus;
        private System.Windows.Forms.Button btnLoadRecipe;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label lblCurrentRecipe;
        private System.Windows.Forms.ComboBox cmbRecipe;
        private System.Windows.Forms.Button btnManualPage;
        private System.Windows.Forms.Button btnForceExclude;
        private System.Windows.Forms.CheckBox ckbForceRecord;
    }
}

