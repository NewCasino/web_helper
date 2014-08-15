namespace web_helper
{
    partial class frm_web_browser
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
            this.btn_method = new System.Windows.Forms.Button();
            this.btn_script = new System.Windows.Forms.Button();
            this.btn_analyse = new System.Windows.Forms.Button();
            this.btn_navigate = new System.Windows.Forms.Button();
            this.txt_url = new System.Windows.Forms.TextBox();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.browser = new System.Windows.Forms.WebBrowser();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.txt_result = new System.Windows.Forms.TextBox();
            this.tabPage6 = new System.Windows.Forms.TabPage();
            this.txt_result_script = new System.Windows.Forms.TextBox();
            this.tabPage7 = new System.Windows.Forms.TabPage();
            this.txt_result_method = new System.Windows.Forms.TextBox();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.dgv_1 = new System.Windows.Forms.DataGridView();
            this.tabPage4 = new System.Windows.Forms.TabPage();
            this.dgv_2 = new System.Windows.Forms.DataGridView();
            this.tabPage8 = new System.Windows.Forms.TabPage();
            this.dgv_3 = new System.Windows.Forms.DataGridView();
            this.tabPage5 = new System.Windows.Forms.TabPage();
            this.dgv_4 = new System.Windows.Forms.DataGridView();
            this.cb_ajust = new System.Windows.Forms.CheckBox();
            this.groupBox1.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.tabPage6.SuspendLayout();
            this.tabPage7.SuspendLayout();
            this.tabPage3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_1)).BeginInit();
            this.tabPage4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_2)).BeginInit();
            this.tabPage8.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_3)).BeginInit();
            this.tabPage5.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_4)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.cb_ajust);
            this.groupBox1.Controls.Add(this.btn_method);
            this.groupBox1.Controls.Add(this.btn_script);
            this.groupBox1.Controls.Add(this.btn_analyse);
            this.groupBox1.Controls.Add(this.btn_navigate);
            this.groupBox1.Controls.Add(this.txt_url);
            this.groupBox1.Location = new System.Drawing.Point(4, 1);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(1215, 56);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Operation";
            // 
            // btn_method
            // 
            this.btn_method.Location = new System.Drawing.Point(855, 18);
            this.btn_method.Name = "btn_method";
            this.btn_method.Size = new System.Drawing.Size(75, 23);
            this.btn_method.TabIndex = 4;
            this.btn_method.Text = "Method";
            this.btn_method.UseVisualStyleBackColor = true;
            this.btn_method.Click += new System.EventHandler(this.btn_method_Click);
            // 
            // btn_script
            // 
            this.btn_script.Location = new System.Drawing.Point(774, 18);
            this.btn_script.Name = "btn_script";
            this.btn_script.Size = new System.Drawing.Size(75, 23);
            this.btn_script.TabIndex = 3;
            this.btn_script.Text = "Script";
            this.btn_script.UseVisualStyleBackColor = true;
            this.btn_script.Click += new System.EventHandler(this.btn_script_Click);
            // 
            // btn_analyse
            // 
            this.btn_analyse.Location = new System.Drawing.Point(692, 18);
            this.btn_analyse.Name = "btn_analyse";
            this.btn_analyse.Size = new System.Drawing.Size(75, 23);
            this.btn_analyse.TabIndex = 2;
            this.btn_analyse.Text = "Analyse";
            this.btn_analyse.UseVisualStyleBackColor = true;
            this.btn_analyse.Click += new System.EventHandler(this.btn_analyse_Click);
            // 
            // btn_navigate
            // 
            this.btn_navigate.Location = new System.Drawing.Point(552, 18);
            this.btn_navigate.Name = "btn_navigate";
            this.btn_navigate.Size = new System.Drawing.Size(75, 23);
            this.btn_navigate.TabIndex = 1;
            this.btn_navigate.Text = "Navigate";
            this.btn_navigate.UseVisualStyleBackColor = true;
            this.btn_navigate.Click += new System.EventHandler(this.btn_navigate_Click);
            // 
            // txt_url
            // 
            this.txt_url.Location = new System.Drawing.Point(12, 20);
            this.txt_url.Name = "txt_url";
            this.txt_url.Size = new System.Drawing.Size(534, 20);
            this.txt_url.TabIndex = 0;
            this.txt_url.Text = "D:\\DOC\\Visual Studio 2008\\MyProjects\\WebSite2\\all.htm";
            // 
            // tabControl1
            // 
            this.tabControl1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Controls.Add(this.tabPage6);
            this.tabControl1.Controls.Add(this.tabPage7);
            this.tabControl1.Controls.Add(this.tabPage3);
            this.tabControl1.Controls.Add(this.tabPage4);
            this.tabControl1.Controls.Add(this.tabPage8);
            this.tabControl1.Controls.Add(this.tabPage5);
            this.tabControl1.Location = new System.Drawing.Point(4, 63);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(1215, 637);
            this.tabControl1.TabIndex = 1;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.browser);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(1207, 611);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Browser";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // browser
            // 
            this.browser.Location = new System.Drawing.Point(3, 4);
            this.browser.MinimumSize = new System.Drawing.Size(20, 20);
            this.browser.Name = "browser";
            this.browser.Size = new System.Drawing.Size(1200, 600);
            this.browser.TabIndex = 0;
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.txt_result);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(1207, 611);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "[Analyse] Text";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // txt_result
            // 
            this.txt_result.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txt_result.Font = new System.Drawing.Font("Lucida Console", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_result.Location = new System.Drawing.Point(3, 3);
            this.txt_result.Multiline = true;
            this.txt_result.Name = "txt_result";
            this.txt_result.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.txt_result.Size = new System.Drawing.Size(1201, 605);
            this.txt_result.TabIndex = 0;
            this.txt_result.WordWrap = false;
            this.txt_result.TextChanged += new System.EventHandler(this.txt_result_TextChanged);
            // 
            // tabPage6
            // 
            this.tabPage6.Controls.Add(this.txt_result_script);
            this.tabPage6.Location = new System.Drawing.Point(4, 22);
            this.tabPage6.Name = "tabPage6";
            this.tabPage6.Size = new System.Drawing.Size(1207, 611);
            this.tabPage6.TabIndex = 5;
            this.tabPage6.Text = "[Script] Text";
            this.tabPage6.UseVisualStyleBackColor = true;
            // 
            // txt_result_script
            // 
            this.txt_result_script.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txt_result_script.Font = new System.Drawing.Font("Lucida Console", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_result_script.Location = new System.Drawing.Point(0, 0);
            this.txt_result_script.Multiline = true;
            this.txt_result_script.Name = "txt_result_script";
            this.txt_result_script.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.txt_result_script.Size = new System.Drawing.Size(1207, 611);
            this.txt_result_script.TabIndex = 1;
            this.txt_result_script.WordWrap = false;
            // 
            // tabPage7
            // 
            this.tabPage7.Controls.Add(this.txt_result_method);
            this.tabPage7.Location = new System.Drawing.Point(4, 22);
            this.tabPage7.Name = "tabPage7";
            this.tabPage7.Size = new System.Drawing.Size(1207, 611);
            this.tabPage7.TabIndex = 6;
            this.tabPage7.Text = "[Method] Method";
            this.tabPage7.UseVisualStyleBackColor = true;
            // 
            // txt_result_method
            // 
            this.txt_result_method.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txt_result_method.Font = new System.Drawing.Font("Lucida Console", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_result_method.Location = new System.Drawing.Point(0, 0);
            this.txt_result_method.Multiline = true;
            this.txt_result_method.Name = "txt_result_method";
            this.txt_result_method.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.txt_result_method.Size = new System.Drawing.Size(1207, 611);
            this.txt_result_method.TabIndex = 1;
            this.txt_result_method.WordWrap = false;
            // 
            // tabPage3
            // 
            this.tabPage3.Controls.Add(this.dgv_1);
            this.tabPage3.Location = new System.Drawing.Point(4, 22);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Size = new System.Drawing.Size(1207, 611);
            this.tabPage3.TabIndex = 2;
            this.tabPage3.Text = "Table-1";
            this.tabPage3.UseVisualStyleBackColor = true;
            // 
            // dgv_1
            // 
            this.dgv_1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgv_1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgv_1.Location = new System.Drawing.Point(0, 0);
            this.dgv_1.Name = "dgv_1";
            this.dgv_1.Size = new System.Drawing.Size(1207, 611);
            this.dgv_1.TabIndex = 0;
            // 
            // tabPage4
            // 
            this.tabPage4.Controls.Add(this.dgv_2);
            this.tabPage4.Location = new System.Drawing.Point(4, 22);
            this.tabPage4.Name = "tabPage4";
            this.tabPage4.Size = new System.Drawing.Size(1207, 611);
            this.tabPage4.TabIndex = 3;
            this.tabPage4.Text = "Table-2";
            this.tabPage4.UseVisualStyleBackColor = true;
            // 
            // dgv_2
            // 
            this.dgv_2.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgv_2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgv_2.Location = new System.Drawing.Point(0, 0);
            this.dgv_2.Name = "dgv_2";
            this.dgv_2.Size = new System.Drawing.Size(1207, 611);
            this.dgv_2.TabIndex = 1;
            // 
            // tabPage8
            // 
            this.tabPage8.Controls.Add(this.dgv_3);
            this.tabPage8.Location = new System.Drawing.Point(4, 22);
            this.tabPage8.Name = "tabPage8";
            this.tabPage8.Size = new System.Drawing.Size(1207, 611);
            this.tabPage8.TabIndex = 7;
            this.tabPage8.Text = "Table-3";
            this.tabPage8.UseVisualStyleBackColor = true;
            // 
            // dgv_3
            // 
            this.dgv_3.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgv_3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgv_3.Location = new System.Drawing.Point(0, 0);
            this.dgv_3.Name = "dgv_3";
            this.dgv_3.Size = new System.Drawing.Size(1207, 611);
            this.dgv_3.TabIndex = 3;
            // 
            // tabPage5
            // 
            this.tabPage5.Controls.Add(this.dgv_4);
            this.tabPage5.Location = new System.Drawing.Point(4, 22);
            this.tabPage5.Name = "tabPage5";
            this.tabPage5.Size = new System.Drawing.Size(1207, 611);
            this.tabPage5.TabIndex = 4;
            this.tabPage5.Text = "Table-4";
            this.tabPage5.UseVisualStyleBackColor = true;
            // 
            // dgv_4
            // 
            this.dgv_4.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgv_4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgv_4.Location = new System.Drawing.Point(0, 0);
            this.dgv_4.Name = "dgv_4";
            this.dgv_4.Size = new System.Drawing.Size(1207, 611);
            this.dgv_4.TabIndex = 2;
            // 
            // cb_ajust
            // 
            this.cb_ajust.AutoSize = true;
            this.cb_ajust.Location = new System.Drawing.Point(634, 22);
            this.cb_ajust.Name = "cb_ajust";
            this.cb_ajust.Size = new System.Drawing.Size(49, 17);
            this.cb_ajust.TabIndex = 5;
            this.cb_ajust.Text = "Ajust";
            this.cb_ajust.UseVisualStyleBackColor = true;
            // 
            // frm_web_browser
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1228, 706);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.groupBox1);
            this.Name = "frm_web_browser";
            this.Load += new System.EventHandler(this.frm_web_browser_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage2.ResumeLayout(false);
            this.tabPage2.PerformLayout();
            this.tabPage6.ResumeLayout(false);
            this.tabPage6.PerformLayout();
            this.tabPage7.ResumeLayout(false);
            this.tabPage7.PerformLayout();
            this.tabPage3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgv_1)).EndInit();
            this.tabPage4.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgv_2)).EndInit();
            this.tabPage8.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgv_3)).EndInit();
            this.tabPage5.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgv_4)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button btn_navigate;
        private System.Windows.Forms.TextBox txt_url;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.WebBrowser browser;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.TextBox txt_result;
        private System.Windows.Forms.Button btn_analyse;
        private System.Windows.Forms.TabPage tabPage3;
        private System.Windows.Forms.DataGridView dgv_1;
        private System.Windows.Forms.Button btn_script;
        private System.Windows.Forms.TabPage tabPage4;
        private System.Windows.Forms.DataGridView dgv_2;
        private System.Windows.Forms.Button btn_method;
        private System.Windows.Forms.TabPage tabPage5;
        private System.Windows.Forms.DataGridView dgv_4;
        private System.Windows.Forms.TabPage tabPage6;
        private System.Windows.Forms.TextBox txt_result_script;
        private System.Windows.Forms.TabPage tabPage7;
        private System.Windows.Forms.TextBox txt_result_method;
        private System.Windows.Forms.TabPage tabPage8;
        private System.Windows.Forms.DataGridView dgv_3;
        private System.Windows.Forms.CheckBox cb_ajust;
    }
}