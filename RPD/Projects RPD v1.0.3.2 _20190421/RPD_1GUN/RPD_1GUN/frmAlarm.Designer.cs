namespace RPD_1GUN
{
    partial class frmAlarm
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
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btn_Clear = new System.Windows.Forms.Button();
            this.tabAlarm = new System.Windows.Forms.TabControl();
            this.SYS_Page = new System.Windows.Forms.TabPage();
            this.dgvSYS = new System.Windows.Forms.DataGridView();
            this.Column2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column5 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.EF_Page = new System.Windows.Forms.TabPage();
            this.dgvEF = new System.Windows.Forms.DataGridView();
            this.dataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.LL_Page = new System.Windows.Forms.TabPage();
            this.dgvLL = new System.Windows.Forms.DataGridView();
            this.dataGridViewTextBoxColumn5 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn6 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn7 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn8 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.TR_Page = new System.Windows.Forms.TabPage();
            this.dgvTR = new System.Windows.Forms.DataGridView();
            this.dataGridViewTextBoxColumn9 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn10 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn11 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn12 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.UL_Page = new System.Windows.Forms.TabPage();
            this.dgvUL = new System.Windows.Forms.DataGridView();
            this.dataGridViewTextBoxColumn13 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn14 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn15 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn16 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DF_Page = new System.Windows.Forms.TabPage();
            this.dgvDF = new System.Windows.Forms.DataGridView();
            this.dataGridViewTextBoxColumn17 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn18 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn19 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn20 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.PCC_Page = new System.Windows.Forms.TabPage();
            this.dgvPCC = new System.Windows.Forms.DataGridView();
            this.dataGridViewTextBoxColumn21 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn22 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn23 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn24 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DisplayTimer = new System.Windows.Forms.Timer(this.components);
            this.groupBox1.SuspendLayout();
            this.tabAlarm.SuspendLayout();
            this.SYS_Page.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvSYS)).BeginInit();
            this.EF_Page.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvEF)).BeginInit();
            this.LL_Page.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvLL)).BeginInit();
            this.TR_Page.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvTR)).BeginInit();
            this.UL_Page.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvUL)).BeginInit();
            this.DF_Page.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvDF)).BeginInit();
            this.PCC_Page.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvPCC)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.btn_Clear);
            this.groupBox1.Controls.Add(this.tabAlarm);
            this.groupBox1.Font = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.groupBox1.Location = new System.Drawing.Point(0, 0);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.groupBox1.Size = new System.Drawing.Size(2992, 1748);
            this.groupBox1.TabIndex = 2;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Alarm Summary";
            // 
            // btn_Clear
            // 
            this.btn_Clear.Location = new System.Drawing.Point(1348, 1446);
            this.btn_Clear.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.btn_Clear.Name = "btn_Clear";
            this.btn_Clear.Size = new System.Drawing.Size(214, 66);
            this.btn_Clear.TabIndex = 3;
            this.btn_Clear.Text = "Clear";
            this.btn_Clear.UseVisualStyleBackColor = true;
            this.btn_Clear.Click += new System.EventHandler(this.btn_Clear_Click);
            // 
            // tabAlarm
            // 
            this.tabAlarm.Controls.Add(this.SYS_Page);
            this.tabAlarm.Controls.Add(this.EF_Page);
            this.tabAlarm.Controls.Add(this.LL_Page);
            this.tabAlarm.Controls.Add(this.TR_Page);
            this.tabAlarm.Controls.Add(this.UL_Page);
            this.tabAlarm.Controls.Add(this.DF_Page);
            this.tabAlarm.Controls.Add(this.PCC_Page);
            this.tabAlarm.Location = new System.Drawing.Point(14, 52);
            this.tabAlarm.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.tabAlarm.Name = "tabAlarm";
            this.tabAlarm.SelectedIndex = 0;
            this.tabAlarm.Size = new System.Drawing.Size(2874, 1390);
            this.tabAlarm.TabIndex = 2;
            // 
            // SYS_Page
            // 
            this.SYS_Page.BackColor = System.Drawing.Color.Transparent;
            this.SYS_Page.Controls.Add(this.dgvSYS);
            this.SYS_Page.Location = new System.Drawing.Point(8, 54);
            this.SYS_Page.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.SYS_Page.Name = "SYS_Page";
            this.SYS_Page.Padding = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.SYS_Page.Size = new System.Drawing.Size(2858, 1328);
            this.SYS_Page.TabIndex = 0;
            this.SYS_Page.Text = "SYS";
            // 
            // dgvSYS
            // 
            this.dgvSYS.AllowUserToAddRows = false;
            this.dgvSYS.AllowUserToDeleteRows = false;
            this.dgvSYS.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvSYS.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvSYS.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Column2,
            this.Column3,
            this.Column5,
            this.Column1});
            this.dgvSYS.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvSYS.Location = new System.Drawing.Point(6, 6);
            this.dgvSYS.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.dgvSYS.Name = "dgvSYS";
            this.dgvSYS.ReadOnly = true;
            this.dgvSYS.RowTemplate.Height = 24;
            this.dgvSYS.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvSYS.Size = new System.Drawing.Size(2846, 1316);
            this.dgvSYS.TabIndex = 2;
            // 
            // Column2
            // 
            this.Column2.HeaderText = "EQ";
            this.Column2.Name = "Column2";
            this.Column2.ReadOnly = true;
            // 
            // Column3
            // 
            this.Column3.HeaderText = "Type";
            this.Column3.Name = "Column3";
            this.Column3.ReadOnly = true;
            // 
            // Column5
            // 
            this.Column5.HeaderText = "Description";
            this.Column5.Name = "Column5";
            this.Column5.ReadOnly = true;
            // 
            // Column1
            // 
            this.Column1.HeaderText = "Time";
            this.Column1.Name = "Column1";
            this.Column1.ReadOnly = true;
            // 
            // EF_Page
            // 
            this.EF_Page.Controls.Add(this.dgvEF);
            this.EF_Page.Location = new System.Drawing.Point(8, 54);
            this.EF_Page.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.EF_Page.Name = "EF_Page";
            this.EF_Page.Padding = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.EF_Page.Size = new System.Drawing.Size(2858, 1328);
            this.EF_Page.TabIndex = 1;
            this.EF_Page.Text = "EF";
            this.EF_Page.UseVisualStyleBackColor = true;
            // 
            // dgvEF
            // 
            this.dgvEF.AllowUserToAddRows = false;
            this.dgvEF.AllowUserToDeleteRows = false;
            this.dgvEF.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvEF.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dataGridViewTextBoxColumn1,
            this.dataGridViewTextBoxColumn2,
            this.dataGridViewTextBoxColumn3,
            this.dataGridViewTextBoxColumn4});
            this.dgvEF.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvEF.Location = new System.Drawing.Point(6, 6);
            this.dgvEF.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.dgvEF.Name = "dgvEF";
            this.dgvEF.ReadOnly = true;
            this.dgvEF.RowTemplate.Height = 24;
            this.dgvEF.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvEF.Size = new System.Drawing.Size(2846, 1316);
            this.dgvEF.TabIndex = 3;
            // 
            // dataGridViewTextBoxColumn1
            // 
            this.dataGridViewTextBoxColumn1.HeaderText = "EQ";
            this.dataGridViewTextBoxColumn1.Name = "dataGridViewTextBoxColumn1";
            this.dataGridViewTextBoxColumn1.ReadOnly = true;
            // 
            // dataGridViewTextBoxColumn2
            // 
            this.dataGridViewTextBoxColumn2.HeaderText = "Type";
            this.dataGridViewTextBoxColumn2.Name = "dataGridViewTextBoxColumn2";
            this.dataGridViewTextBoxColumn2.ReadOnly = true;
            // 
            // dataGridViewTextBoxColumn3
            // 
            this.dataGridViewTextBoxColumn3.HeaderText = "Description";
            this.dataGridViewTextBoxColumn3.Name = "dataGridViewTextBoxColumn3";
            this.dataGridViewTextBoxColumn3.ReadOnly = true;
            this.dataGridViewTextBoxColumn3.Width = 500;
            // 
            // dataGridViewTextBoxColumn4
            // 
            this.dataGridViewTextBoxColumn4.HeaderText = "Time";
            this.dataGridViewTextBoxColumn4.Name = "dataGridViewTextBoxColumn4";
            this.dataGridViewTextBoxColumn4.ReadOnly = true;
            this.dataGridViewTextBoxColumn4.Width = 120;
            // 
            // LL_Page
            // 
            this.LL_Page.Controls.Add(this.dgvLL);
            this.LL_Page.Location = new System.Drawing.Point(8, 54);
            this.LL_Page.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.LL_Page.Name = "LL_Page";
            this.LL_Page.Size = new System.Drawing.Size(2858, 1328);
            this.LL_Page.TabIndex = 4;
            this.LL_Page.Text = "LL";
            this.LL_Page.UseVisualStyleBackColor = true;
            // 
            // dgvLL
            // 
            this.dgvLL.AllowUserToAddRows = false;
            this.dgvLL.AllowUserToDeleteRows = false;
            this.dgvLL.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvLL.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dataGridViewTextBoxColumn5,
            this.dataGridViewTextBoxColumn6,
            this.dataGridViewTextBoxColumn7,
            this.dataGridViewTextBoxColumn8});
            this.dgvLL.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvLL.Location = new System.Drawing.Point(0, 0);
            this.dgvLL.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.dgvLL.Name = "dgvLL";
            this.dgvLL.ReadOnly = true;
            this.dgvLL.RowTemplate.Height = 24;
            this.dgvLL.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvLL.Size = new System.Drawing.Size(2858, 1328);
            this.dgvLL.TabIndex = 3;
            // 
            // dataGridViewTextBoxColumn5
            // 
            this.dataGridViewTextBoxColumn5.HeaderText = "EQ";
            this.dataGridViewTextBoxColumn5.Name = "dataGridViewTextBoxColumn5";
            this.dataGridViewTextBoxColumn5.ReadOnly = true;
            // 
            // dataGridViewTextBoxColumn6
            // 
            this.dataGridViewTextBoxColumn6.HeaderText = "Type";
            this.dataGridViewTextBoxColumn6.Name = "dataGridViewTextBoxColumn6";
            this.dataGridViewTextBoxColumn6.ReadOnly = true;
            // 
            // dataGridViewTextBoxColumn7
            // 
            this.dataGridViewTextBoxColumn7.HeaderText = "Description";
            this.dataGridViewTextBoxColumn7.Name = "dataGridViewTextBoxColumn7";
            this.dataGridViewTextBoxColumn7.ReadOnly = true;
            this.dataGridViewTextBoxColumn7.Width = 500;
            // 
            // dataGridViewTextBoxColumn8
            // 
            this.dataGridViewTextBoxColumn8.HeaderText = "Time";
            this.dataGridViewTextBoxColumn8.Name = "dataGridViewTextBoxColumn8";
            this.dataGridViewTextBoxColumn8.ReadOnly = true;
            this.dataGridViewTextBoxColumn8.Width = 120;
            // 
            // TR_Page
            // 
            this.TR_Page.Controls.Add(this.dgvTR);
            this.TR_Page.Location = new System.Drawing.Point(8, 54);
            this.TR_Page.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.TR_Page.Name = "TR_Page";
            this.TR_Page.Size = new System.Drawing.Size(2858, 1328);
            this.TR_Page.TabIndex = 2;
            this.TR_Page.Text = "TR";
            this.TR_Page.UseVisualStyleBackColor = true;
            // 
            // dgvTR
            // 
            this.dgvTR.AllowUserToAddRows = false;
            this.dgvTR.AllowUserToDeleteRows = false;
            this.dgvTR.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvTR.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dataGridViewTextBoxColumn9,
            this.dataGridViewTextBoxColumn10,
            this.dataGridViewTextBoxColumn11,
            this.dataGridViewTextBoxColumn12});
            this.dgvTR.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvTR.Location = new System.Drawing.Point(0, 0);
            this.dgvTR.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.dgvTR.Name = "dgvTR";
            this.dgvTR.ReadOnly = true;
            this.dgvTR.RowTemplate.Height = 24;
            this.dgvTR.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvTR.Size = new System.Drawing.Size(2858, 1328);
            this.dgvTR.TabIndex = 3;
            // 
            // dataGridViewTextBoxColumn9
            // 
            this.dataGridViewTextBoxColumn9.HeaderText = "EQ";
            this.dataGridViewTextBoxColumn9.Name = "dataGridViewTextBoxColumn9";
            this.dataGridViewTextBoxColumn9.ReadOnly = true;
            // 
            // dataGridViewTextBoxColumn10
            // 
            this.dataGridViewTextBoxColumn10.HeaderText = "Type";
            this.dataGridViewTextBoxColumn10.Name = "dataGridViewTextBoxColumn10";
            this.dataGridViewTextBoxColumn10.ReadOnly = true;
            // 
            // dataGridViewTextBoxColumn11
            // 
            this.dataGridViewTextBoxColumn11.HeaderText = "Description";
            this.dataGridViewTextBoxColumn11.Name = "dataGridViewTextBoxColumn11";
            this.dataGridViewTextBoxColumn11.ReadOnly = true;
            this.dataGridViewTextBoxColumn11.Width = 500;
            // 
            // dataGridViewTextBoxColumn12
            // 
            this.dataGridViewTextBoxColumn12.HeaderText = "Time";
            this.dataGridViewTextBoxColumn12.Name = "dataGridViewTextBoxColumn12";
            this.dataGridViewTextBoxColumn12.ReadOnly = true;
            this.dataGridViewTextBoxColumn12.Width = 120;
            // 
            // UL_Page
            // 
            this.UL_Page.Controls.Add(this.dgvUL);
            this.UL_Page.Location = new System.Drawing.Point(8, 54);
            this.UL_Page.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.UL_Page.Name = "UL_Page";
            this.UL_Page.Size = new System.Drawing.Size(2858, 1328);
            this.UL_Page.TabIndex = 3;
            this.UL_Page.Text = "UL";
            this.UL_Page.UseVisualStyleBackColor = true;
            // 
            // dgvUL
            // 
            this.dgvUL.AllowUserToAddRows = false;
            this.dgvUL.AllowUserToDeleteRows = false;
            this.dgvUL.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvUL.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dataGridViewTextBoxColumn13,
            this.dataGridViewTextBoxColumn14,
            this.dataGridViewTextBoxColumn15,
            this.dataGridViewTextBoxColumn16});
            this.dgvUL.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvUL.Location = new System.Drawing.Point(0, 0);
            this.dgvUL.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.dgvUL.Name = "dgvUL";
            this.dgvUL.ReadOnly = true;
            this.dgvUL.RowTemplate.Height = 24;
            this.dgvUL.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvUL.Size = new System.Drawing.Size(2858, 1328);
            this.dgvUL.TabIndex = 3;
            // 
            // dataGridViewTextBoxColumn13
            // 
            this.dataGridViewTextBoxColumn13.HeaderText = "EQ";
            this.dataGridViewTextBoxColumn13.Name = "dataGridViewTextBoxColumn13";
            this.dataGridViewTextBoxColumn13.ReadOnly = true;
            // 
            // dataGridViewTextBoxColumn14
            // 
            this.dataGridViewTextBoxColumn14.HeaderText = "Type";
            this.dataGridViewTextBoxColumn14.Name = "dataGridViewTextBoxColumn14";
            this.dataGridViewTextBoxColumn14.ReadOnly = true;
            // 
            // dataGridViewTextBoxColumn15
            // 
            this.dataGridViewTextBoxColumn15.HeaderText = "Description";
            this.dataGridViewTextBoxColumn15.Name = "dataGridViewTextBoxColumn15";
            this.dataGridViewTextBoxColumn15.ReadOnly = true;
            this.dataGridViewTextBoxColumn15.Width = 500;
            // 
            // dataGridViewTextBoxColumn16
            // 
            this.dataGridViewTextBoxColumn16.HeaderText = "Time";
            this.dataGridViewTextBoxColumn16.Name = "dataGridViewTextBoxColumn16";
            this.dataGridViewTextBoxColumn16.ReadOnly = true;
            this.dataGridViewTextBoxColumn16.Width = 120;
            // 
            // DF_Page
            // 
            this.DF_Page.Controls.Add(this.dgvDF);
            this.DF_Page.Location = new System.Drawing.Point(8, 54);
            this.DF_Page.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.DF_Page.Name = "DF_Page";
            this.DF_Page.Size = new System.Drawing.Size(2858, 1328);
            this.DF_Page.TabIndex = 5;
            this.DF_Page.Text = "DF";
            this.DF_Page.UseVisualStyleBackColor = true;
            // 
            // dgvDF
            // 
            this.dgvDF.AllowUserToAddRows = false;
            this.dgvDF.AllowUserToDeleteRows = false;
            this.dgvDF.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvDF.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dataGridViewTextBoxColumn17,
            this.dataGridViewTextBoxColumn18,
            this.dataGridViewTextBoxColumn19,
            this.dataGridViewTextBoxColumn20});
            this.dgvDF.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvDF.Location = new System.Drawing.Point(0, 0);
            this.dgvDF.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.dgvDF.Name = "dgvDF";
            this.dgvDF.ReadOnly = true;
            this.dgvDF.RowTemplate.Height = 24;
            this.dgvDF.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvDF.Size = new System.Drawing.Size(2858, 1328);
            this.dgvDF.TabIndex = 3;
            // 
            // dataGridViewTextBoxColumn17
            // 
            this.dataGridViewTextBoxColumn17.HeaderText = "EQ";
            this.dataGridViewTextBoxColumn17.Name = "dataGridViewTextBoxColumn17";
            this.dataGridViewTextBoxColumn17.ReadOnly = true;
            // 
            // dataGridViewTextBoxColumn18
            // 
            this.dataGridViewTextBoxColumn18.HeaderText = "Type";
            this.dataGridViewTextBoxColumn18.Name = "dataGridViewTextBoxColumn18";
            this.dataGridViewTextBoxColumn18.ReadOnly = true;
            // 
            // dataGridViewTextBoxColumn19
            // 
            this.dataGridViewTextBoxColumn19.HeaderText = "Description";
            this.dataGridViewTextBoxColumn19.Name = "dataGridViewTextBoxColumn19";
            this.dataGridViewTextBoxColumn19.ReadOnly = true;
            this.dataGridViewTextBoxColumn19.Width = 500;
            // 
            // dataGridViewTextBoxColumn20
            // 
            this.dataGridViewTextBoxColumn20.HeaderText = "Time";
            this.dataGridViewTextBoxColumn20.Name = "dataGridViewTextBoxColumn20";
            this.dataGridViewTextBoxColumn20.ReadOnly = true;
            this.dataGridViewTextBoxColumn20.Width = 120;
            // 
            // PCC_Page
            // 
            this.PCC_Page.Controls.Add(this.dgvPCC);
            this.PCC_Page.Location = new System.Drawing.Point(8, 54);
            this.PCC_Page.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.PCC_Page.Name = "PCC_Page";
            this.PCC_Page.Size = new System.Drawing.Size(2858, 1328);
            this.PCC_Page.TabIndex = 6;
            this.PCC_Page.Text = "PCC";
            this.PCC_Page.UseVisualStyleBackColor = true;
            // 
            // dgvPCC
            // 
            this.dgvPCC.AllowUserToAddRows = false;
            this.dgvPCC.AllowUserToDeleteRows = false;
            this.dgvPCC.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvPCC.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dataGridViewTextBoxColumn21,
            this.dataGridViewTextBoxColumn22,
            this.dataGridViewTextBoxColumn23,
            this.dataGridViewTextBoxColumn24});
            this.dgvPCC.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvPCC.Location = new System.Drawing.Point(0, 0);
            this.dgvPCC.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.dgvPCC.Name = "dgvPCC";
            this.dgvPCC.ReadOnly = true;
            this.dgvPCC.RowTemplate.Height = 24;
            this.dgvPCC.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvPCC.Size = new System.Drawing.Size(2858, 1328);
            this.dgvPCC.TabIndex = 3;
            // 
            // dataGridViewTextBoxColumn21
            // 
            this.dataGridViewTextBoxColumn21.HeaderText = "EQ";
            this.dataGridViewTextBoxColumn21.Name = "dataGridViewTextBoxColumn21";
            this.dataGridViewTextBoxColumn21.ReadOnly = true;
            // 
            // dataGridViewTextBoxColumn22
            // 
            this.dataGridViewTextBoxColumn22.HeaderText = "Type";
            this.dataGridViewTextBoxColumn22.Name = "dataGridViewTextBoxColumn22";
            this.dataGridViewTextBoxColumn22.ReadOnly = true;
            // 
            // dataGridViewTextBoxColumn23
            // 
            this.dataGridViewTextBoxColumn23.HeaderText = "Description";
            this.dataGridViewTextBoxColumn23.Name = "dataGridViewTextBoxColumn23";
            this.dataGridViewTextBoxColumn23.ReadOnly = true;
            this.dataGridViewTextBoxColumn23.Width = 500;
            // 
            // dataGridViewTextBoxColumn24
            // 
            this.dataGridViewTextBoxColumn24.HeaderText = "Time";
            this.dataGridViewTextBoxColumn24.Name = "dataGridViewTextBoxColumn24";
            this.dataGridViewTextBoxColumn24.ReadOnly = true;
            this.dataGridViewTextBoxColumn24.Width = 120;
            // 
            // DisplayTimer
            // 
            this.DisplayTimer.Interval = 500;
            this.DisplayTimer.Tick += new System.EventHandler(this.DisplayTimer_Tick);
            // 
            // frmAlarm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 24F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(2898, 1748);
            this.ControlBox = false;
            this.Controls.Add(this.groupBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.Name = "frmAlarm";
            this.Text = "frmAlarm";
            this.Load += new System.EventHandler(this.frmAlarm_Load);
            this.groupBox1.ResumeLayout(false);
            this.tabAlarm.ResumeLayout(false);
            this.SYS_Page.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvSYS)).EndInit();
            this.EF_Page.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvEF)).EndInit();
            this.LL_Page.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvLL)).EndInit();
            this.TR_Page.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvTR)).EndInit();
            this.UL_Page.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvUL)).EndInit();
            this.DF_Page.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvDF)).EndInit();
            this.PCC_Page.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvPCC)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TabControl tabAlarm;
        private System.Windows.Forms.TabPage SYS_Page;
        private System.Windows.Forms.Button btn_Clear;
        private System.Windows.Forms.TabPage LL_Page;
        private System.Windows.Forms.TabPage TR_Page;
        private System.Windows.Forms.TabPage UL_Page;
        private System.Windows.Forms.TabPage DF_Page;
        private System.Windows.Forms.TabPage PCC_Page;
        private System.Windows.Forms.DataGridView dgvSYS;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column2;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column3;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column5;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column1;
        private System.Windows.Forms.TabPage EF_Page;
        private System.Windows.Forms.Timer DisplayTimer;
        private System.Windows.Forms.DataGridView dgvEF;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn1;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn2;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn3;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn4;
        private System.Windows.Forms.DataGridView dgvLL;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn5;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn6;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn7;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn8;
        private System.Windows.Forms.DataGridView dgvTR;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn9;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn10;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn11;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn12;
        private System.Windows.Forms.DataGridView dgvUL;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn13;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn14;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn15;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn16;
        private System.Windows.Forms.DataGridView dgvDF;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn17;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn18;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn19;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn20;
        private System.Windows.Forms.DataGridView dgvPCC;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn21;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn22;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn23;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn24;
    }
}