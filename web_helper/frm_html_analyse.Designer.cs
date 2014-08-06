﻿partial class frm_html_analyse
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
            this.btn_navigate = new System.Windows.Forms.Button();
            this.btn_load_browser = new System.Windows.Forms.Button();
            this.btn_fuzzy_find = new System.Windows.Forms.Button();
            this.txt_condition = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.btn_select = new System.Windows.Forms.Button();
            this.btn_load_web_client = new System.Windows.Forms.Button();
            this.txt_path = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txt_url = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.Brower = new System.Windows.Forms.TabPage();
            this.browser = new System.Windows.Forms.WebBrowser();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.txt_html_source = new System.Windows.Forms.TextBox();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.txt_result = new System.Windows.Forms.TextBox();
            this.tabPage4 = new System.Windows.Forms.TabPage();
            this.dgv_result = new System.Windows.Forms.DataGridView();
            this.groupBox1.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.Brower.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage3.SuspendLayout();
            this.tabPage4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_result)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.btn_navigate);
            this.groupBox1.Controls.Add(this.btn_load_browser);
            this.groupBox1.Controls.Add(this.btn_fuzzy_find);
            this.groupBox1.Controls.Add(this.txt_condition);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.btn_select);
            this.groupBox1.Controls.Add(this.btn_load_web_client);
            this.groupBox1.Controls.Add(this.txt_path);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.txt_url);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Location = new System.Drawing.Point(5, 3);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(984, 129);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Operation";
            // 
            // btn_navigate
            // 
            this.btn_navigate.Location = new System.Drawing.Point(848, 11);
            this.btn_navigate.Name = "btn_navigate";
            this.btn_navigate.Size = new System.Drawing.Size(96, 23);
            this.btn_navigate.TabIndex = 11;
            this.btn_navigate.Text = "Navigate";
            this.btn_navigate.UseVisualStyleBackColor = true;
            this.btn_navigate.Click += new System.EventHandler(this.btn_navigate_Click);
            // 
            // btn_load_browser
            // 
            this.btn_load_browser.Location = new System.Drawing.Point(16, 99);
            this.btn_load_browser.Name = "btn_load_browser";
            this.btn_load_browser.Size = new System.Drawing.Size(113, 23);
            this.btn_load_browser.TabIndex = 10;
            this.btn_load_browser.Text = "Load From Browser";
            this.btn_load_browser.UseVisualStyleBackColor = true;
            this.btn_load_browser.Click += new System.EventHandler(this.btn_load_browser_Click);
            // 
            // btn_fuzzy_find
            // 
            this.btn_fuzzy_find.Location = new System.Drawing.Point(377, 99);
            this.btn_fuzzy_find.Name = "btn_fuzzy_find";
            this.btn_fuzzy_find.Size = new System.Drawing.Size(93, 23);
            this.btn_fuzzy_find.TabIndex = 9;
            this.btn_fuzzy_find.Text = "Fuzzy Select";
            this.btn_fuzzy_find.UseVisualStyleBackColor = true;
            this.btn_fuzzy_find.Click += new System.EventHandler(this.btn_fuzzy_find_Click);
            // 
            // txt_condition
            // 
            this.txt_condition.Location = new System.Drawing.Point(114, 42);
            this.txt_condition.Name = "txt_condition";
            this.txt_condition.Size = new System.Drawing.Size(830, 20);
            this.txt_condition.TabIndex = 8;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(13, 44);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(82, 13);
            this.label3.TabIndex = 7;
            this.label3.Text = "Fuzzy Conditon:";
            // 
            // btn_select
            // 
            this.btn_select.Location = new System.Drawing.Point(476, 99);
            this.btn_select.Name = "btn_select";
            this.btn_select.Size = new System.Drawing.Size(93, 23);
            this.btn_select.TabIndex = 5;
            this.btn_select.Text = "Select Element";
            this.btn_select.UseVisualStyleBackColor = true;
            this.btn_select.Click += new System.EventHandler(this.btn_select_Click);
            // 
            // btn_load_web_client
            // 
            this.btn_load_web_client.Location = new System.Drawing.Point(135, 99);
            this.btn_load_web_client.Name = "btn_load_web_client";
            this.btn_load_web_client.Size = new System.Drawing.Size(125, 23);
            this.btn_load_web_client.TabIndex = 4;
            this.btn_load_web_client.Text = "Load From WebClient";
            this.btn_load_web_client.UseVisualStyleBackColor = true;
            this.btn_load_web_client.Click += new System.EventHandler(this.btn_load_web_client_Click);
            // 
            // txt_path
            // 
            this.txt_path.Location = new System.Drawing.Point(114, 69);
            this.txt_path.Name = "txt_path";
            this.txt_path.Size = new System.Drawing.Size(830, 20);
            this.txt_path.TabIndex = 3;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(25, 69);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(73, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "Element Path:";
            // 
            // txt_url
            // 
            this.txt_url.Location = new System.Drawing.Point(114, 13);
            this.txt_url.Name = "txt_url";
            this.txt_url.Size = new System.Drawing.Size(723, 20);
            this.txt_url.TabIndex = 1;
            this.txt_url.Text = "http://192.168.1.221/efnet_test/src/_Common/AppUtil/FrameSet/EFDBLogin.aspx";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(55, 16);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(49, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Web Url:";
            // 
            // tabControl1
            // 
            this.tabControl1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.tabControl1.Controls.Add(this.Brower);
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage3);
            this.tabControl1.Controls.Add(this.tabPage4);
            this.tabControl1.Location = new System.Drawing.Point(5, 139);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(984, 421);
            this.tabControl1.TabIndex = 1;
            // 
            // Brower
            // 
            this.Brower.Controls.Add(this.browser);
            this.Brower.Location = new System.Drawing.Point(4, 22);
            this.Brower.Name = "Brower";
            this.Brower.Size = new System.Drawing.Size(976, 395);
            this.Brower.TabIndex = 4;
            this.Brower.Text = "Browser";
            this.Brower.UseVisualStyleBackColor = true;
            // 
            // browser
            // 
            this.browser.Dock = System.Windows.Forms.DockStyle.Fill;
            this.browser.Location = new System.Drawing.Point(0, 0);
            this.browser.MinimumSize = new System.Drawing.Size(20, 22);
            this.browser.Name = "browser";
            this.browser.Size = new System.Drawing.Size(976, 395);
            this.browser.TabIndex = 2;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.txt_html_source);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(976, 395);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Html Source";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // txt_html_source
            // 
            this.txt_html_source.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txt_html_source.Location = new System.Drawing.Point(3, 3);
            this.txt_html_source.MaxLength = 0;
            this.txt_html_source.Multiline = true;
            this.txt_html_source.Name = "txt_html_source";
            this.txt_html_source.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.txt_html_source.Size = new System.Drawing.Size(970, 389);
            this.txt_html_source.TabIndex = 0;
            this.txt_html_source.WordWrap = false;
            // 
            // tabPage3
            // 
            this.tabPage3.Controls.Add(this.txt_result);
            this.tabPage3.Location = new System.Drawing.Point(4, 22);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Size = new System.Drawing.Size(976, 395);
            this.tabPage3.TabIndex = 2;
            this.tabPage3.Text = "Elements Text";
            this.tabPage3.UseVisualStyleBackColor = true;
            // 
            // txt_result
            // 
            this.txt_result.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txt_result.Location = new System.Drawing.Point(0, 0);
            this.txt_result.Multiline = true;
            this.txt_result.Name = "txt_result";
            this.txt_result.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.txt_result.Size = new System.Drawing.Size(976, 395);
            this.txt_result.TabIndex = 1;
            this.txt_result.WordWrap = false;
            // 
            // tabPage4
            // 
            this.tabPage4.Controls.Add(this.dgv_result);
            this.tabPage4.Location = new System.Drawing.Point(4, 22);
            this.tabPage4.Name = "tabPage4";
            this.tabPage4.Size = new System.Drawing.Size(976, 395);
            this.tabPage4.TabIndex = 3;
            this.tabPage4.Text = "Element DataTable";
            this.tabPage4.UseVisualStyleBackColor = true;
            // 
            // dgv_result
            // 
            this.dgv_result.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.dgv_result.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgv_result.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgv_result.Location = new System.Drawing.Point(0, 0);
            this.dgv_result.Name = "dgv_result";
            this.dgv_result.Size = new System.Drawing.Size(976, 395);
            this.dgv_result.TabIndex = 0;
            // 
            // frm_html_analyse
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(992, 566);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.groupBox1);
            this.Name = "frm_html_analyse";
            this.Text = "Html Analyse";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.tabControl1.ResumeLayout(false);
            this.Brower.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            this.tabPage3.ResumeLayout(false);
            this.tabPage3.PerformLayout();
            this.tabPage4.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgv_result)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button btn_select;
        private System.Windows.Forms.Button btn_load_web_client;
        private System.Windows.Forms.TextBox txt_path;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txt_url;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage3;
        private System.Windows.Forms.TabPage tabPage4;
        private System.Windows.Forms.TextBox txt_html_source;
        private System.Windows.Forms.TextBox txt_result;
        private System.Windows.Forms.DataGridView dgv_result;
        private System.Windows.Forms.TextBox txt_condition;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button btn_fuzzy_find;
        private System.Windows.Forms.TabPage Brower;
        private System.Windows.Forms.WebBrowser browser;
        private System.Windows.Forms.Button btn_navigate;
        private System.Windows.Forms.Button btn_load_browser;
    } 