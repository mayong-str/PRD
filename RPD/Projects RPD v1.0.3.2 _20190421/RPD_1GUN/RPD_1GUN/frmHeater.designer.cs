namespace RPD_1GUN
{
    partial class frmHeater
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
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.lbl_W2Status = new System.Windows.Forms.Label();
            this.btnW2_Inside_Close = new System.Windows.Forms.Button();
            this.btnW2_Inside_Open = new System.Windows.Forms.Button();
            this.label6 = new System.Windows.Forms.Label();
            this.lblNowValue_W2 = new System.Windows.Forms.Label();
            this.txtSetValue_W2 = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.label14 = new System.Windows.Forms.Label();
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.lbl_W1Status = new System.Windows.Forms.Label();
            this.btnW1_Inside_Close = new System.Windows.Forms.Button();
            this.btnW1_Inside_Open = new System.Windows.Forms.Button();
            this.label21 = new System.Windows.Forms.Label();
            this.lblNowValue_W1 = new System.Windows.Forms.Label();
            this.txtSetValue_W1 = new System.Windows.Forms.TextBox();
            this.label48 = new System.Windows.Forms.Label();
            this.label59 = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.DisplayTimer = new System.Windows.Forms.Timer(this.components);
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.lbl_W3Status = new System.Windows.Forms.Label();
            this.btnW3_Inside_Close = new System.Windows.Forms.Button();
            this.btnW3_Inside_Open = new System.Windows.Forms.Button();
            this.label16 = new System.Windows.Forms.Label();
            this.lblNowValue_W3 = new System.Windows.Forms.Label();
            this.txtSetValue_W3 = new System.Windows.Forms.TextBox();
            this.label18 = new System.Windows.Forms.Label();
            this.label20 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label17 = new System.Windows.Forms.Label();
            this.txtWaterSetTime = new System.Windows.Forms.TextBox();
            this.label62 = new System.Windows.Forms.Label();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.groupBox3.SuspendLayout();
            this.groupBox5.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.label2);
            this.groupBox3.Controls.Add(this.lbl_W2Status);
            this.groupBox3.Controls.Add(this.btnW2_Inside_Close);
            this.groupBox3.Controls.Add(this.btnW2_Inside_Open);
            this.groupBox3.Controls.Add(this.label6);
            this.groupBox3.Controls.Add(this.lblNowValue_W2);
            this.groupBox3.Controls.Add(this.txtSetValue_W2);
            this.groupBox3.Controls.Add(this.label10);
            this.groupBox3.Controls.Add(this.label14);
            this.groupBox3.Font = new System.Drawing.Font("微軟正黑體", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.groupBox3.Location = new System.Drawing.Point(319, 119);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(296, 128);
            this.groupBox3.TabIndex = 389;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "HeaterW2";
            // 
            // lbl_W2Status
            // 
            this.lbl_W2Status.AutoSize = true;
            this.lbl_W2Status.Font = new System.Drawing.Font("微軟正黑體", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.lbl_W2Status.ForeColor = System.Drawing.Color.Blue;
            this.lbl_W2Status.Location = new System.Drawing.Point(6, 107);
            this.lbl_W2Status.Name = "lbl_W2Status";
            this.lbl_W2Status.Size = new System.Drawing.Size(36, 19);
            this.lbl_W2Status.TabIndex = 14;
            this.lbl_W2Status.Text = "111";
            // 
            // btnW2_Inside_Close
            // 
            this.btnW2_Inside_Close.Location = new System.Drawing.Point(93, 22);
            this.btnW2_Inside_Close.Name = "btnW2_Inside_Close";
            this.btnW2_Inside_Close.Size = new System.Drawing.Size(77, 33);
            this.btnW2_Inside_Close.TabIndex = 9;
            this.btnW2_Inside_Close.Text = "Off";
            this.btnW2_Inside_Close.UseVisualStyleBackColor = true;
            // 
            // btnW2_Inside_Open
            // 
            this.btnW2_Inside_Open.Location = new System.Drawing.Point(10, 22);
            this.btnW2_Inside_Open.Name = "btnW2_Inside_Open";
            this.btnW2_Inside_Open.Size = new System.Drawing.Size(77, 33);
            this.btnW2_Inside_Open.TabIndex = 8;
            this.btnW2_Inside_Open.Tag = ",,stop,,";
            this.btnW2_Inside_Open.Text = "On";
            this.btnW2_Inside_Open.UseVisualStyleBackColor = true;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(215, 60);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(25, 19);
            this.label6.TabIndex = 5;
            this.label6.Text = "°C";
            // 
            // lblNowValue_W2
            // 
            this.lblNowValue_W2.AutoSize = true;
            this.lblNowValue_W2.ForeColor = System.Drawing.Color.Blue;
            this.lblNowValue_W2.Location = new System.Drawing.Point(158, 60);
            this.lblNowValue_W2.Name = "lblNowValue_W2";
            this.lblNowValue_W2.Size = new System.Drawing.Size(36, 19);
            this.lblNowValue_W2.TabIndex = 4;
            this.lblNowValue_W2.Text = "150";
            // 
            // txtSetValue_W2
            // 
            this.txtSetValue_W2.Location = new System.Drawing.Point(158, 79);
            this.txtSetValue_W2.MaxLength = 3;
            this.txtSetValue_W2.Name = "txtSetValue_W2";
            this.txtSetValue_W2.Size = new System.Drawing.Size(40, 27);
            this.txtSetValue_W2.TabIndex = 2;
            this.txtSetValue_W2.Text = "25";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(16, 60);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(108, 19);
            this.label10.TabIndex = 1;
            this.label10.Text = "Temperature:";
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(16, 85);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(136, 19);
            this.label14.TabIndex = 0;
            this.label14.Text = "Set Temperature:";
            // 
            // groupBox5
            // 
            this.groupBox5.Controls.Add(this.label1);
            this.groupBox5.Controls.Add(this.lbl_W1Status);
            this.groupBox5.Controls.Add(this.btnW1_Inside_Close);
            this.groupBox5.Controls.Add(this.btnW1_Inside_Open);
            this.groupBox5.Controls.Add(this.label21);
            this.groupBox5.Controls.Add(this.lblNowValue_W1);
            this.groupBox5.Controls.Add(this.txtSetValue_W1);
            this.groupBox5.Controls.Add(this.label48);
            this.groupBox5.Controls.Add(this.label59);
            this.groupBox5.Font = new System.Drawing.Font("微軟正黑體", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.groupBox5.Location = new System.Drawing.Point(18, 119);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Size = new System.Drawing.Size(296, 128);
            this.groupBox5.TabIndex = 387;
            this.groupBox5.TabStop = false;
            this.groupBox5.Text = "HeaterW1";
            // 
            // lbl_W1Status
            // 
            this.lbl_W1Status.AutoSize = true;
            this.lbl_W1Status.Font = new System.Drawing.Font("微軟正黑體", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.lbl_W1Status.ForeColor = System.Drawing.Color.Blue;
            this.lbl_W1Status.Location = new System.Drawing.Point(6, 107);
            this.lbl_W1Status.Name = "lbl_W1Status";
            this.lbl_W1Status.Size = new System.Drawing.Size(36, 19);
            this.lbl_W1Status.TabIndex = 13;
            this.lbl_W1Status.Text = "111";
            // 
            // btnW1_Inside_Close
            // 
            this.btnW1_Inside_Close.Location = new System.Drawing.Point(93, 22);
            this.btnW1_Inside_Close.Name = "btnW1_Inside_Close";
            this.btnW1_Inside_Close.Size = new System.Drawing.Size(77, 33);
            this.btnW1_Inside_Close.TabIndex = 9;
            this.btnW1_Inside_Close.Text = "Off";
            this.btnW1_Inside_Close.UseVisualStyleBackColor = true;
            // 
            // btnW1_Inside_Open
            // 
            this.btnW1_Inside_Open.Location = new System.Drawing.Point(10, 22);
            this.btnW1_Inside_Open.Name = "btnW1_Inside_Open";
            this.btnW1_Inside_Open.Size = new System.Drawing.Size(77, 33);
            this.btnW1_Inside_Open.TabIndex = 8;
            this.btnW1_Inside_Open.Tag = ",,stop,,";
            this.btnW1_Inside_Open.Text = "On";
            this.btnW1_Inside_Open.UseVisualStyleBackColor = true;
            // 
            // label21
            // 
            this.label21.AutoSize = true;
            this.label21.Location = new System.Drawing.Point(215, 60);
            this.label21.Name = "label21";
            this.label21.Size = new System.Drawing.Size(25, 19);
            this.label21.TabIndex = 5;
            this.label21.Text = "°C";
            // 
            // lblNowValue_W1
            // 
            this.lblNowValue_W1.AutoSize = true;
            this.lblNowValue_W1.ForeColor = System.Drawing.Color.Blue;
            this.lblNowValue_W1.Location = new System.Drawing.Point(158, 60);
            this.lblNowValue_W1.Name = "lblNowValue_W1";
            this.lblNowValue_W1.Size = new System.Drawing.Size(36, 19);
            this.lblNowValue_W1.TabIndex = 4;
            this.lblNowValue_W1.Text = "150";
            // 
            // txtSetValue_W1
            // 
            this.txtSetValue_W1.Location = new System.Drawing.Point(158, 79);
            this.txtSetValue_W1.MaxLength = 3;
            this.txtSetValue_W1.Name = "txtSetValue_W1";
            this.txtSetValue_W1.Size = new System.Drawing.Size(40, 27);
            this.txtSetValue_W1.TabIndex = 2;
            this.txtSetValue_W1.Text = "25";
            // 
            // label48
            // 
            this.label48.AutoSize = true;
            this.label48.Location = new System.Drawing.Point(16, 60);
            this.label48.Name = "label48";
            this.label48.Size = new System.Drawing.Size(108, 19);
            this.label48.TabIndex = 1;
            this.label48.Text = "Temperature:";
            // 
            // label59
            // 
            this.label59.AutoSize = true;
            this.label59.Location = new System.Drawing.Point(16, 85);
            this.label59.Name = "label59";
            this.label59.Size = new System.Drawing.Size(136, 19);
            this.label59.TabIndex = 0;
            this.label59.Text = "Set Temperature:";
            // 
            // label13
            // 
            this.label13.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))));
            this.label13.Font = new System.Drawing.Font("微軟正黑體", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.label13.Location = new System.Drawing.Point(444, -41);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(758, 50);
            this.label13.TabIndex = 385;
            this.label13.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // DisplayTimer
            // 
            this.DisplayTimer.Interval = 400;
            this.DisplayTimer.Tick += new System.EventHandler(this.DisplayTimer_Tick);
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.label3);
            this.groupBox4.Controls.Add(this.lbl_W3Status);
            this.groupBox4.Controls.Add(this.btnW3_Inside_Close);
            this.groupBox4.Controls.Add(this.btnW3_Inside_Open);
            this.groupBox4.Controls.Add(this.label16);
            this.groupBox4.Controls.Add(this.lblNowValue_W3);
            this.groupBox4.Controls.Add(this.txtSetValue_W3);
            this.groupBox4.Controls.Add(this.label18);
            this.groupBox4.Controls.Add(this.label20);
            this.groupBox4.Font = new System.Drawing.Font("微軟正黑體", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.groupBox4.Location = new System.Drawing.Point(647, 119);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(296, 128);
            this.groupBox4.TabIndex = 388;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "HeaterW3";
            // 
            // lbl_W3Status
            // 
            this.lbl_W3Status.AutoSize = true;
            this.lbl_W3Status.Font = new System.Drawing.Font("微軟正黑體", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.lbl_W3Status.ForeColor = System.Drawing.Color.Blue;
            this.lbl_W3Status.Location = new System.Drawing.Point(6, 107);
            this.lbl_W3Status.Name = "lbl_W3Status";
            this.lbl_W3Status.Size = new System.Drawing.Size(36, 19);
            this.lbl_W3Status.TabIndex = 15;
            this.lbl_W3Status.Text = "111";
            // 
            // btnW3_Inside_Close
            // 
            this.btnW3_Inside_Close.Location = new System.Drawing.Point(89, 22);
            this.btnW3_Inside_Close.Name = "btnW3_Inside_Close";
            this.btnW3_Inside_Close.Size = new System.Drawing.Size(77, 33);
            this.btnW3_Inside_Close.TabIndex = 9;
            this.btnW3_Inside_Close.Text = "Off";
            this.btnW3_Inside_Close.UseVisualStyleBackColor = true;
            // 
            // btnW3_Inside_Open
            // 
            this.btnW3_Inside_Open.Location = new System.Drawing.Point(6, 22);
            this.btnW3_Inside_Open.Name = "btnW3_Inside_Open";
            this.btnW3_Inside_Open.Size = new System.Drawing.Size(77, 33);
            this.btnW3_Inside_Open.TabIndex = 8;
            this.btnW3_Inside_Open.Tag = ",,stop,,";
            this.btnW3_Inside_Open.Text = "On";
            this.btnW3_Inside_Open.UseVisualStyleBackColor = true;
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Location = new System.Drawing.Point(215, 60);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(25, 19);
            this.label16.TabIndex = 5;
            this.label16.Text = "°C";
            // 
            // lblNowValue_W3
            // 
            this.lblNowValue_W3.AutoSize = true;
            this.lblNowValue_W3.ForeColor = System.Drawing.Color.Blue;
            this.lblNowValue_W3.Location = new System.Drawing.Point(158, 60);
            this.lblNowValue_W3.Name = "lblNowValue_W3";
            this.lblNowValue_W3.Size = new System.Drawing.Size(36, 19);
            this.lblNowValue_W3.TabIndex = 4;
            this.lblNowValue_W3.Text = "150";
            // 
            // txtSetValue_W3
            // 
            this.txtSetValue_W3.Location = new System.Drawing.Point(158, 79);
            this.txtSetValue_W3.MaxLength = 3;
            this.txtSetValue_W3.Name = "txtSetValue_W3";
            this.txtSetValue_W3.Size = new System.Drawing.Size(40, 27);
            this.txtSetValue_W3.TabIndex = 2;
            this.txtSetValue_W3.Text = "25";
            // 
            // label18
            // 
            this.label18.AutoSize = true;
            this.label18.Location = new System.Drawing.Point(16, 60);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(108, 19);
            this.label18.TabIndex = 1;
            this.label18.Text = "Temperature:";
            // 
            // label20
            // 
            this.label20.AutoSize = true;
            this.label20.Location = new System.Drawing.Point(16, 85);
            this.label20.Name = "label20";
            this.label20.Size = new System.Drawing.Size(136, 19);
            this.label20.TabIndex = 0;
            this.label20.Text = "Set Temperature:";
            // 
            // groupBox1
            // 
            this.groupBox1.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.groupBox1.Controls.Add(this.label17);
            this.groupBox1.Controls.Add(this.txtWaterSetTime);
            this.groupBox1.Controls.Add(this.label62);
            this.groupBox1.Font = new System.Drawing.Font("微軟正黑體", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.groupBox1.Location = new System.Drawing.Point(18, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(296, 90);
            this.groupBox1.TabIndex = 291;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "W Heater Stable time";
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.Location = new System.Drawing.Point(150, 30);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(36, 19);
            this.label17.TabIndex = 6;
            this.label17.Text = "min";
            // 
            // txtWaterSetTime
            // 
            this.txtWaterSetTime.Location = new System.Drawing.Point(100, 27);
            this.txtWaterSetTime.MaxLength = 3;
            this.txtWaterSetTime.Name = "txtWaterSetTime";
            this.txtWaterSetTime.Size = new System.Drawing.Size(40, 27);
            this.txtWaterSetTime.TabIndex = 2;
            this.txtWaterSetTime.Text = "260";
            // 
            // label62
            // 
            this.label62.AutoSize = true;
            this.label62.Location = new System.Drawing.Point(16, 30);
            this.label62.Name = "label62";
            this.label62.Size = new System.Drawing.Size(77, 19);
            this.label62.TabIndex = 0;
            this.label62.Text = "Set Time:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(215, 87);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(25, 19);
            this.label1.TabIndex = 14;
            this.label1.Text = "°C";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(215, 87);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(25, 19);
            this.label2.TabIndex = 15;
            this.label2.Text = "°C";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(215, 87);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(25, 19);
            this.label3.TabIndex = 16;
            this.label3.Text = "°C";
            // 
            // frmHeater
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1540, 908);
            this.ControlBox = false;
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox4);
            this.Controls.Add(this.groupBox5);
            this.Controls.Add(this.label13);
            this.Name = "frmHeater";
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.groupBox5.ResumeLayout(false);
            this.groupBox5.PerformLayout();
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Timer DisplayTimer;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Button btnW2_Inside_Close;
        private System.Windows.Forms.Button btnW2_Inside_Open;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label lblNowValue_W2;
        private System.Windows.Forms.TextBox txtSetValue_W2;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.Button btnW3_Inside_Close;
        private System.Windows.Forms.Button btnW3_Inside_Open;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.Label lblNowValue_W3;
        private System.Windows.Forms.TextBox txtSetValue_W3;
        private System.Windows.Forms.Label label18;
        private System.Windows.Forms.Label label20;
        private System.Windows.Forms.GroupBox groupBox5;
        private System.Windows.Forms.Button btnW1_Inside_Close;
        private System.Windows.Forms.Button btnW1_Inside_Open;
        private System.Windows.Forms.Label label21;
        private System.Windows.Forms.Label lblNowValue_W1;
        private System.Windows.Forms.TextBox txtSetValue_W1;
        private System.Windows.Forms.Label label48;
        private System.Windows.Forms.Label label59;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TextBox txtWaterSetTime;
        private System.Windows.Forms.Label label62;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.Label lbl_W2Status;
        private System.Windows.Forms.Label lbl_W1Status;
        private System.Windows.Forms.Label lbl_W3Status;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label3;
    }
}