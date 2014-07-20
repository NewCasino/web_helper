namespace web_helper
{
    partial class frm_match_bar
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
            this.btn_compute_by_company = new System.Windows.Forms.Button();
            this.btn_down_excel = new System.Windows.Forms.Button();
            this.frm_match_company_info = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.btn_match_100 = new System.Windows.Forms.Button();
            this.btn_html_analyse = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btn_compute_by_company
            // 
            this.btn_compute_by_company.Location = new System.Drawing.Point(10, 12);
            this.btn_compute_by_company.Name = "btn_compute_by_company";
            this.btn_compute_by_company.Size = new System.Drawing.Size(112, 21);
            this.btn_compute_by_company.TabIndex = 0;
            this.btn_compute_by_company.Text = "Company Compute";
            this.btn_compute_by_company.UseVisualStyleBackColor = true;
            this.btn_compute_by_company.Click += new System.EventHandler(this.btn_compute_by_company_Click);
            // 
            // btn_down_excel
            // 
            this.btn_down_excel.Location = new System.Drawing.Point(130, 12);
            this.btn_down_excel.Name = "btn_down_excel";
            this.btn_down_excel.Size = new System.Drawing.Size(112, 21);
            this.btn_down_excel.TabIndex = 1;
            this.btn_down_excel.Text = "Down Excel";
            this.btn_down_excel.UseVisualStyleBackColor = true;
            this.btn_down_excel.Click += new System.EventHandler(this.btn_down_excel_Click);
            // 
            // frm_match_company_info
            // 
            this.frm_match_company_info.Location = new System.Drawing.Point(250, 12);
            this.frm_match_company_info.Name = "frm_match_company_info";
            this.frm_match_company_info.Size = new System.Drawing.Size(112, 21);
            this.frm_match_company_info.TabIndex = 2;
            this.frm_match_company_info.Text = "Company Info";
            this.frm_match_company_info.UseVisualStyleBackColor = true;
            this.frm_match_company_info.Click += new System.EventHandler(this.frm_match_company_info_Click);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(368, 12);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(112, 21);
            this.button1.TabIndex = 3;
            this.button1.Text = "Mix Compute";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // btn_match_100
            // 
            this.btn_match_100.Location = new System.Drawing.Point(130, 39);
            this.btn_match_100.Name = "btn_match_100";
            this.btn_match_100.Size = new System.Drawing.Size(112, 21);
            this.btn_match_100.TabIndex = 5;
            this.btn_match_100.Text = "100 Analyse";
            this.btn_match_100.UseVisualStyleBackColor = true;
            this.btn_match_100.Click += new System.EventHandler(this.btn_match_100_Click);
            // 
            // btn_html_analyse
            // 
            this.btn_html_analyse.Location = new System.Drawing.Point(10, 39);
            this.btn_html_analyse.Name = "btn_html_analyse";
            this.btn_html_analyse.Size = new System.Drawing.Size(112, 21);
            this.btn_html_analyse.TabIndex = 4;
            this.btn_html_analyse.Text = "HTML Analyse";
            this.btn_html_analyse.UseVisualStyleBackColor = true;
            this.btn_html_analyse.Click += new System.EventHandler(this.btn_html_analyse_Click);
            // 
            // frm_match_bar
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 75);
            this.Controls.Add(this.btn_match_100);
            this.Controls.Add(this.btn_html_analyse);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.frm_match_company_info);
            this.Controls.Add(this.btn_down_excel);
            this.Controls.Add(this.btn_compute_by_company);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "frm_match_bar";
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btn_compute_by_company;
        private System.Windows.Forms.Button btn_down_excel;
        private System.Windows.Forms.Button frm_match_company_info;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button btn_match_100;
        private System.Windows.Forms.Button btn_html_analyse;
    }
}