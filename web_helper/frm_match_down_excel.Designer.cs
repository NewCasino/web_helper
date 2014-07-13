namespace web_helper
{
    partial class frm_match_down_excel
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
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.txt_result = new System.Windows.Forms.TextBox();
            this.btn_down_excel = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.btn_down_excel);
            this.groupBox1.Location = new System.Drawing.Point(3, 3);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(821, 77);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Operation";
            // 
            // txt_result
            // 
            this.txt_result.Location = new System.Drawing.Point(3, 86);
            this.txt_result.Multiline = true;
            this.txt_result.Name = "txt_result";
            this.txt_result.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.txt_result.Size = new System.Drawing.Size(821, 345);
            this.txt_result.TabIndex = 1;
            // 
            // btn_down_excel
            // 
            this.btn_down_excel.Location = new System.Drawing.Point(9, 32);
            this.btn_down_excel.Name = "btn_down_excel";
            this.btn_down_excel.Size = new System.Drawing.Size(91, 23);
            this.btn_down_excel.TabIndex = 0;
            this.btn_down_excel.Text = "Down Excel";
            this.btn_down_excel.UseVisualStyleBackColor = true;
            this.btn_down_excel.Click += new System.EventHandler(this.btn_down_excel_Click);
            // 
            // frm_match_down_excel
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(827, 443);
            this.Controls.Add(this.txt_result);
            this.Controls.Add(this.groupBox1);
            this.Name = "frm_match_down_excel";
            this.groupBox1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button btn_down_excel;
        private System.Windows.Forms.TextBox txt_result;
    }
}