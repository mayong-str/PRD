namespace MFC
{
    partial class MFC
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

        #region 元件設計工具產生的程式碼

        /// <summary> 
        /// 此為設計工具支援所需的方法 - 請勿使用程式碼編輯器
        /// 修改這個方法的內容。
        /// </summary>
        private void InitializeComponent()
        {
            this.shapeContainer1 = new Microsoft.VisualBasic.PowerPacks.ShapeContainer();
            this.rs_MFC = new Microsoft.VisualBasic.PowerPacks.RectangleShape();
            this.rs_border = new Microsoft.VisualBasic.PowerPacks.RectangleShape();
            this.lab_Name = new System.Windows.Forms.Label();
            this.lab_Value = new System.Windows.Forms.Label();
            this.lab_UNit = new System.Windows.Forms.Label();
            this.gb_temp = new System.Windows.Forms.GroupBox();
            this.btn_cancel = new System.Windows.Forms.Button();
            this.btn_Set = new System.Windows.Forms.Button();
            this.txt_Value = new System.Windows.Forms.TextBox();
            this.gb_temp.SuspendLayout();
            this.SuspendLayout();
            // 
            // shapeContainer1
            // 
            this.shapeContainer1.Location = new System.Drawing.Point(0, 0);
            this.shapeContainer1.Margin = new System.Windows.Forms.Padding(0);
            this.shapeContainer1.Name = "shapeContainer1";
            this.shapeContainer1.Shapes.AddRange(new Microsoft.VisualBasic.PowerPacks.Shape[] {
            this.rs_MFC,
            this.rs_border});
            this.shapeContainer1.Size = new System.Drawing.Size(96, 87);
            this.shapeContainer1.TabIndex = 0;
            this.shapeContainer1.TabStop = false;
            // 
            // rs_MFC
            // 
            this.rs_MFC.BorderStyle = System.Drawing.Drawing2D.DashStyle.Dash;
            this.rs_MFC.Location = new System.Drawing.Point(3, 3);
            this.rs_MFC.Name = "rs_MFC";
            this.rs_MFC.Size = new System.Drawing.Size(89, 29);
            // 
            // rs_border
            // 
            this.rs_border.FillColor = System.Drawing.Color.Maroon;
            this.rs_border.Location = new System.Drawing.Point(0, 0);
            this.rs_border.Name = "rs_border";
            this.rs_border.Size = new System.Drawing.Size(95, 35);
            // 
            // lab_Name
            // 
            this.lab_Name.AutoSize = true;
            this.lab_Name.BackColor = System.Drawing.Color.Transparent;
            this.lab_Name.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lab_Name.Font = new System.Drawing.Font("新細明體-ExtB", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.lab_Name.Location = new System.Drawing.Point(2, 2);
            this.lab_Name.Name = "lab_Name";
            this.lab_Name.Size = new System.Drawing.Size(28, 18);
            this.lab_Name.TabIndex = 1;
            this.lab_Name.Text = "Ar";
            this.lab_Name.DoubleClick += new System.EventHandler(this.lab_Name_DoubleClick);
            // 
            // lab_Value
            // 
            this.lab_Value.AutoSize = true;
            this.lab_Value.BackColor = System.Drawing.Color.Transparent;
            this.lab_Value.Font = new System.Drawing.Font("新細明體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.lab_Value.Location = new System.Drawing.Point(41, 6);
            this.lab_Value.Name = "lab_Value";
            this.lab_Value.Size = new System.Drawing.Size(32, 16);
            this.lab_Value.TabIndex = 4;
            this.lab_Value.Text = "168";
            this.lab_Value.DoubleClick += new System.EventHandler(this.lab_Name_DoubleClick);
            // 
            // lab_UNit
            // 
            this.lab_UNit.AutoSize = true;
            this.lab_UNit.BackColor = System.Drawing.Color.Transparent;
            this.lab_UNit.Font = new System.Drawing.Font("新細明體", 6F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.lab_UNit.Location = new System.Drawing.Point(62, 24);
            this.lab_UNit.Name = "lab_UNit";
            this.lab_UNit.Size = new System.Drawing.Size(30, 8);
            this.lab_UNit.TabIndex = 5;
            this.lab_UNit.Text = "SCCM";
            this.lab_UNit.TextAlign = System.Drawing.ContentAlignment.BottomRight;
            this.lab_UNit.DoubleClick += new System.EventHandler(this.lab_Name_DoubleClick);
            // 
            // gb_temp
            // 
            this.gb_temp.Controls.Add(this.btn_cancel);
            this.gb_temp.Controls.Add(this.btn_Set);
            this.gb_temp.Controls.Add(this.txt_Value);
            this.gb_temp.Location = new System.Drawing.Point(0, 30);
            this.gb_temp.Name = "gb_temp";
            this.gb_temp.Size = new System.Drawing.Size(95, 57);
            this.gb_temp.TabIndex = 7;
            this.gb_temp.TabStop = false;
            // 
            // btn_cancel
            // 
            this.btn_cancel.Location = new System.Drawing.Point(4, 32);
            this.btn_cancel.Name = "btn_cancel";
            this.btn_cancel.Size = new System.Drawing.Size(38, 20);
            this.btn_cancel.TabIndex = 9;
            this.btn_cancel.Text = "CL";
            this.btn_cancel.UseVisualStyleBackColor = true;
            this.btn_cancel.Click += new System.EventHandler(this.btn_cancel_Click);
            // 
            // btn_Set
            // 
            this.btn_Set.Location = new System.Drawing.Point(54, 32);
            this.btn_Set.Name = "btn_Set";
            this.btn_Set.Size = new System.Drawing.Size(37, 20);
            this.btn_Set.TabIndex = 8;
            this.btn_Set.Text = "Set";
            this.btn_Set.UseVisualStyleBackColor = true;
            this.btn_Set.Click += new System.EventHandler(this.btn_Set_Click);
            // 
            // txt_Value
            // 
            this.txt_Value.Location = new System.Drawing.Point(5, 10);
            this.txt_Value.Name = "txt_Value";
            this.txt_Value.Size = new System.Drawing.Size(85, 22);
            this.txt_Value.TabIndex = 7;
            this.txt_Value.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Enter_KeyDown);
            this.txt_Value.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txt_Value_KeyPress);
            // 
            // MFC
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.lab_Name);
            this.Controls.Add(this.lab_Value);
            this.Controls.Add(this.lab_UNit);
            this.Controls.Add(this.shapeContainer1);
            this.Controls.Add(this.gb_temp);
            this.Name = "MFC";
            this.Size = new System.Drawing.Size(96, 87);
            this.Load += new System.EventHandler(this.MFC_Load);
            this.DoubleClick += new System.EventHandler(this.MFC_DoubleClick);
            this.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.MFC_MouseDoubleClick);
            this.Resize += new System.EventHandler(this.MFC_Resize);
            this.gb_temp.ResumeLayout(false);
            this.gb_temp.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Microsoft.VisualBasic.PowerPacks.ShapeContainer shapeContainer1;
        private System.Windows.Forms.Label lab_Name;
        private System.Windows.Forms.Label lab_Value;
        private System.Windows.Forms.Label lab_UNit;
        private System.Windows.Forms.GroupBox gb_temp;
        private Microsoft.VisualBasic.PowerPacks.RectangleShape rs_MFC;
        private Microsoft.VisualBasic.PowerPacks.RectangleShape rs_border;
        private System.Windows.Forms.Button btn_cancel;
        private System.Windows.Forms.Button btn_Set;
        private System.Windows.Forms.TextBox txt_Value;
    }
}
