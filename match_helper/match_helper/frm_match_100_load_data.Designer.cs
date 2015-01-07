namespace web_helper
{
    partial class frm_match_100_load_data
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
            this.lb_time = new System.Windows.Forms.Label();
            this.btn_load = new System.Windows.Forms.Button();
            this.btn_stop = new System.Windows.Forms.Button();
            this.btn_start = new System.Windows.Forms.Button();
            this.txt_url = new System.Windows.Forms.TextBox();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.btn_select_gobetgo = new System.Windows.Forms.Button();
            this.btn_select_marathonbet = new System.Windows.Forms.Button();
            this.btn_select_pinnaclesports = new System.Windows.Forms.Button();
            this.btn_reverse = new System.Windows.Forms.Button();
            this.btn_all = new System.Windows.Forms.Button();
            this.dgv_result = new System.Windows.Forms.DataGridView();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.txt_result = new System.Windows.Forms.TextBox();
            this.tab_browser = new System.Windows.Forms.TabPage();
            this.time = new System.Windows.Forms.Timer(this.components);
            this.groupBox1.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.tabPage3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_result)).BeginInit();
            this.tabPage1.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.lb_time);
            this.groupBox1.Controls.Add(this.btn_load);
            this.groupBox1.Controls.Add(this.btn_stop);
            this.groupBox1.Controls.Add(this.btn_start);
            this.groupBox1.Controls.Add(this.txt_url);
            this.groupBox1.Location = new System.Drawing.Point(6, 0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(1178, 56);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Operation";
            // 
            // lb_time
            // 
            this.lb_time.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lb_time.AutoSize = true;
            this.lb_time.Font = new System.Drawing.Font("Microsoft Sans Serif", 12.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lb_time.Location = new System.Drawing.Point(1086, 22);
            this.lb_time.Name = "lb_time";
            this.lb_time.Size = new System.Drawing.Size(73, 20);
            this.lb_time.TabIndex = 5;
            this.lb_time.Text = "00:00:00";
            // 
            // btn_load
            // 
            this.btn_load.Location = new System.Drawing.Point(254, 20);
            this.btn_load.Name = "btn_load";
            this.btn_load.Size = new System.Drawing.Size(75, 23);
            this.btn_load.TabIndex = 3;
            this.btn_load.Text = "Load";
            this.btn_load.UseVisualStyleBackColor = true;
            this.btn_load.Click += new System.EventHandler(this.btn_load_Click);
            // 
            // btn_stop
            // 
            this.btn_stop.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btn_stop.Location = new System.Drawing.Point(1005, 20);
            this.btn_stop.Name = "btn_stop";
            this.btn_stop.Size = new System.Drawing.Size(75, 23);
            this.btn_stop.TabIndex = 4;
            this.btn_stop.Text = "Stop";
            this.btn_stop.UseVisualStyleBackColor = true;
            this.btn_stop.Click += new System.EventHandler(this.btn_stop_Click);
            // 
            // btn_start
            // 
            this.btn_start.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btn_start.Location = new System.Drawing.Point(924, 20);
            this.btn_start.Name = "btn_start";
            this.btn_start.Size = new System.Drawing.Size(75, 23);
            this.btn_start.TabIndex = 3;
            this.btn_start.Text = "Start";
            this.btn_start.UseVisualStyleBackColor = true;
            this.btn_start.Click += new System.EventHandler(this.btn_start_Click);
            // 
            // txt_url
            // 
            this.txt_url.Location = new System.Drawing.Point(17, 22);
            this.txt_url.Name = "txt_url";
            this.txt_url.Size = new System.Drawing.Size(220, 20);
            this.txt_url.TabIndex = 2;
            // 
            // tabControl1
            // 
            this.tabControl1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.tabControl1.Controls.Add(this.tabPage3);
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tab_browser);
            this.tabControl1.Location = new System.Drawing.Point(6, 62);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(1178, 500);
            this.tabControl1.TabIndex = 1;
            // 
            // tabPage3
            // 
            this.tabPage3.Controls.Add(this.btn_select_gobetgo);
            this.tabPage3.Controls.Add(this.btn_select_marathonbet);
            this.tabPage3.Controls.Add(this.btn_select_pinnaclesports);
            this.tabPage3.Controls.Add(this.btn_reverse);
            this.tabPage3.Controls.Add(this.btn_all);
            this.tabPage3.Controls.Add(this.dgv_result);
            this.tabPage3.Location = new System.Drawing.Point(4, 22);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Size = new System.Drawing.Size(1170, 474);
            this.tabPage3.TabIndex = 2;
            this.tabPage3.Text = "Result Table";
            this.tabPage3.UseVisualStyleBackColor = true;
            // 
            // btn_select_gobetgo
            // 
            this.btn_select_gobetgo.Location = new System.Drawing.Point(494, 8);
            this.btn_select_gobetgo.Name = "btn_select_gobetgo";
            this.btn_select_gobetgo.Size = new System.Drawing.Size(41, 23);
            this.btn_select_gobetgo.TabIndex = 5;
            this.btn_select_gobetgo.Text = "S(3)";
            this.btn_select_gobetgo.UseVisualStyleBackColor = true;
            this.btn_select_gobetgo.Click += new System.EventHandler(this.btn_select_website_3_Click);
            // 
            // btn_select_marathonbet
            // 
            this.btn_select_marathonbet.Location = new System.Drawing.Point(450, 8);
            this.btn_select_marathonbet.Name = "btn_select_marathonbet";
            this.btn_select_marathonbet.Size = new System.Drawing.Size(41, 23);
            this.btn_select_marathonbet.TabIndex = 4;
            this.btn_select_marathonbet.Text = "S(2)";
            this.btn_select_marathonbet.UseVisualStyleBackColor = true;
            this.btn_select_marathonbet.Click += new System.EventHandler(this.btn_select_website_2_Click);
            // 
            // btn_select_pinnaclesports
            // 
            this.btn_select_pinnaclesports.Location = new System.Drawing.Point(406, 8);
            this.btn_select_pinnaclesports.Name = "btn_select_pinnaclesports";
            this.btn_select_pinnaclesports.Size = new System.Drawing.Size(41, 23);
            this.btn_select_pinnaclesports.TabIndex = 3;
            this.btn_select_pinnaclesports.Text = "S(1)";
            this.btn_select_pinnaclesports.UseVisualStyleBackColor = true;
            this.btn_select_pinnaclesports.Click += new System.EventHandler(this.btn_select_website_1_Click);
            // 
            // btn_reverse
            // 
            this.btn_reverse.Location = new System.Drawing.Point(88, 8);
            this.btn_reverse.Name = "btn_reverse";
            this.btn_reverse.Size = new System.Drawing.Size(75, 23);
            this.btn_reverse.TabIndex = 2;
            this.btn_reverse.Text = "REVERSE";
            this.btn_reverse.UseVisualStyleBackColor = true;
            this.btn_reverse.Click += new System.EventHandler(this.btn_reverse_Click);
            // 
            // btn_all
            // 
            this.btn_all.Location = new System.Drawing.Point(7, 8);
            this.btn_all.Name = "btn_all";
            this.btn_all.Size = new System.Drawing.Size(75, 23);
            this.btn_all.TabIndex = 1;
            this.btn_all.Text = "ALL";
            this.btn_all.UseVisualStyleBackColor = true;
            this.btn_all.Click += new System.EventHandler(this.btn_all_Click);
            // 
            // dgv_result
            // 
            this.dgv_result.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.dgv_result.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgv_result.Location = new System.Drawing.Point(3, 37);
            this.dgv_result.Name = "dgv_result";
            this.dgv_result.Size = new System.Drawing.Size(1162, 434);
            this.dgv_result.TabIndex = 0;
            this.dgv_result.DataBindingComplete += new System.Windows.Forms.DataGridViewBindingCompleteEventHandler(this.dgv_result_DataBindingComplete);
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.txt_result);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(1170, 474);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Result Text";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // txt_result
            // 
            this.txt_result.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txt_result.Font = new System.Drawing.Font("Lucida Console", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_result.Location = new System.Drawing.Point(3, 3);
            this.txt_result.Multiline = true;
            this.txt_result.Name = "txt_result";
            this.txt_result.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.txt_result.Size = new System.Drawing.Size(1164, 468);
            this.txt_result.TabIndex = 0;
            this.txt_result.WordWrap = false;
            this.txt_result.TextChanged += new System.EventHandler(this.txt_result_TextChanged);
            // 
            // tab_browser
            // 
            this.tab_browser.Location = new System.Drawing.Point(4, 22);
            this.tab_browser.Name = "tab_browser";
            this.tab_browser.Size = new System.Drawing.Size(1170, 474);
            this.tab_browser.TabIndex = 3;
            this.tab_browser.Text = "Browser";
            this.tab_browser.UseVisualStyleBackColor = true;
            // 
            // time
            // 
            this.time.Interval = 1000;
            this.time.Tick += new System.EventHandler(this.time_Tick);
            // 
            // frm_match_100_load_data
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1191, 566);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.groupBox1);
            this.Name = "frm_match_100_load_data";
            this.Load += new System.EventHandler(this.frm_match_100_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.tabControl1.ResumeLayout(false);
            this.tabPage3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgv_result)).EndInit();
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TextBox txt_url;
        private System.Windows.Forms.TextBox txt_result;
        private System.Windows.Forms.Timer time;
        private System.Windows.Forms.TabPage tabPage3;
        private System.Windows.Forms.DataGridView dgv_result;
        private System.Windows.Forms.Button btn_stop;
        private System.Windows.Forms.Button btn_start;
        private System.Windows.Forms.Button btn_load;
        private System.Windows.Forms.Label lb_time;
        private System.Windows.Forms.Button btn_all;
        private System.Windows.Forms.Button btn_reverse;
        private System.Windows.Forms.TabPage tab_browser;
        private System.Windows.Forms.Button btn_select_gobetgo;
        private System.Windows.Forms.Button btn_select_marathonbet;
        private System.Windows.Forms.Button btn_select_pinnaclesports;
    }
}