namespace web_helper
{
    partial class frm_match_company_info
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
            this.dgv_company = new System.Windows.Forms.DataGridView();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btn_save = new System.Windows.Forms.Button();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.txt_pay_ways = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.txt_other_urls = new System.Windows.Forms.TextBox();
            this.txt_other_names = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.lb_row_id = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.lb_company_id = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.btn_update = new System.Windows.Forms.Button();
            this.txt_name = new System.Windows.Forms.TextBox();
            this.txt_url = new System.Windows.Forms.TextBox();
            this.btn_load = new System.Windows.Forms.Button();
            this.txt_condition = new System.Windows.Forms.TextBox();
            this.txt_result = new System.Windows.Forms.TextBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.btn_update_grid = new System.Windows.Forms.Button();
            this.btn_check_json = new System.Windows.Forms.Button();
            this.btn_json_beautify = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_company)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // dgv_company
            // 
            this.dgv_company.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.dgv_company.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgv_company.Location = new System.Drawing.Point(516, 18);
            this.dgv_company.Name = "dgv_company";
            this.dgv_company.Size = new System.Drawing.Size(655, 273);
            this.dgv_company.TabIndex = 0;
            this.dgv_company.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgv_company_CellClick);
            this.dgv_company.DataBindingComplete += new System.Windows.Forms.DataGridViewBindingCompleteEventHandler(this.dgv_company_DataBindingComplete);
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.btn_save);
            this.groupBox1.Controls.Add(this.groupBox3);
            this.groupBox1.Controls.Add(this.btn_load);
            this.groupBox1.Controls.Add(this.txt_condition);
            this.groupBox1.Controls.Add(this.dgv_company);
            this.groupBox1.Location = new System.Drawing.Point(6, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(1177, 297);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Operation";
            // 
            // btn_save
            // 
            this.btn_save.Location = new System.Drawing.Point(425, 18);
            this.btn_save.Name = "btn_save";
            this.btn_save.Size = new System.Drawing.Size(75, 23);
            this.btn_save.TabIndex = 5;
            this.btn_save.Text = "Save";
            this.btn_save.UseVisualStyleBackColor = true;
            this.btn_save.Click += new System.EventHandler(this.btn_save_Click);
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.txt_pay_ways);
            this.groupBox3.Controls.Add(this.label7);
            this.groupBox3.Controls.Add(this.txt_other_urls);
            this.groupBox3.Controls.Add(this.txt_other_names);
            this.groupBox3.Controls.Add(this.label6);
            this.groupBox3.Controls.Add(this.label4);
            this.groupBox3.Controls.Add(this.lb_row_id);
            this.groupBox3.Controls.Add(this.label5);
            this.groupBox3.Controls.Add(this.lb_company_id);
            this.groupBox3.Controls.Add(this.label3);
            this.groupBox3.Controls.Add(this.label2);
            this.groupBox3.Controls.Add(this.label1);
            this.groupBox3.Controls.Add(this.btn_update);
            this.groupBox3.Controls.Add(this.txt_name);
            this.groupBox3.Controls.Add(this.txt_url);
            this.groupBox3.Location = new System.Drawing.Point(19, 48);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(491, 243);
            this.groupBox3.TabIndex = 3;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Infomation";
            // 
            // txt_pay_ways
            // 
            this.txt_pay_ways.Location = new System.Drawing.Point(101, 196);
            this.txt_pay_ways.Name = "txt_pay_ways";
            this.txt_pay_ways.Size = new System.Drawing.Size(379, 20);
            this.txt_pay_ways.TabIndex = 30;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(11, 199);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(53, 13);
            this.label7.TabIndex = 29;
            this.label7.Text = "Pay Way:";
            // 
            // txt_other_urls
            // 
            this.txt_other_urls.Location = new System.Drawing.Point(102, 125);
            this.txt_other_urls.Multiline = true;
            this.txt_other_urls.Name = "txt_other_urls";
            this.txt_other_urls.Size = new System.Drawing.Size(379, 65);
            this.txt_other_urls.TabIndex = 28;
            // 
            // txt_other_names
            // 
            this.txt_other_names.Location = new System.Drawing.Point(101, 99);
            this.txt_other_names.Name = "txt_other_names";
            this.txt_other_names.Size = new System.Drawing.Size(379, 20);
            this.txt_other_names.TabIndex = 27;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(11, 128);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(79, 13);
            this.label6.TabIndex = 26;
            this.label6.Text = "Company URL:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(11, 99);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(72, 13);
            this.label4.TabIndex = 25;
            this.label4.Text = "Other Names:";
            // 
            // lb_row_id
            // 
            this.lb_row_id.AutoSize = true;
            this.lb_row_id.Location = new System.Drawing.Point(193, 24);
            this.lb_row_id.Name = "lb_row_id";
            this.lb_row_id.Size = new System.Drawing.Size(0, 13);
            this.lb_row_id.TabIndex = 24;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(142, 24);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(46, 13);
            this.label5.TabIndex = 23;
            this.label5.Text = "Row ID:";
            // 
            // lb_company_id
            // 
            this.lb_company_id.AutoSize = true;
            this.lb_company_id.Location = new System.Drawing.Point(84, 24);
            this.lb_company_id.Name = "lb_company_id";
            this.lb_company_id.Size = new System.Drawing.Size(0, 13);
            this.lb_company_id.TabIndex = 22;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(11, 24);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(68, 13);
            this.label3.TabIndex = 21;
            this.label3.Text = "Company ID:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(11, 74);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(79, 13);
            this.label2.TabIndex = 20;
            this.label2.Text = "Company URL:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(11, 49);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(85, 13);
            this.label1.TabIndex = 19;
            this.label1.Text = "Company Name:";
            // 
            // btn_update
            // 
            this.btn_update.Location = new System.Drawing.Point(390, 15);
            this.btn_update.Name = "btn_update";
            this.btn_update.Size = new System.Drawing.Size(75, 23);
            this.btn_update.TabIndex = 14;
            this.btn_update.Text = "Update";
            this.btn_update.UseVisualStyleBackColor = true;
            this.btn_update.Click += new System.EventHandler(this.btn_update_Click);
            // 
            // txt_name
            // 
            this.txt_name.Location = new System.Drawing.Point(101, 44);
            this.txt_name.Name = "txt_name";
            this.txt_name.Size = new System.Drawing.Size(380, 20);
            this.txt_name.TabIndex = 13;
            // 
            // txt_url
            // 
            this.txt_url.Location = new System.Drawing.Point(102, 71);
            this.txt_url.Name = "txt_url";
            this.txt_url.Size = new System.Drawing.Size(379, 20);
            this.txt_url.TabIndex = 15;
            // 
            // btn_load
            // 
            this.btn_load.Location = new System.Drawing.Point(344, 19);
            this.btn_load.Name = "btn_load";
            this.btn_load.Size = new System.Drawing.Size(75, 23);
            this.btn_load.TabIndex = 4;
            this.btn_load.Text = "Load";
            this.btn_load.UseVisualStyleBackColor = true;
            this.btn_load.Click += new System.EventHandler(this.btn_load_Click);
            // 
            // txt_condition
            // 
            this.txt_condition.Location = new System.Drawing.Point(29, 20);
            this.txt_condition.Name = "txt_condition";
            this.txt_condition.Size = new System.Drawing.Size(309, 20);
            this.txt_condition.TabIndex = 3;
            // 
            // txt_result
            // 
            this.txt_result.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.txt_result.Location = new System.Drawing.Point(19, 19);
            this.txt_result.Multiline = true;
            this.txt_result.Name = "txt_result";
            this.txt_result.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.txt_result.Size = new System.Drawing.Size(1034, 214);
            this.txt_result.TabIndex = 2;
            // 
            // groupBox2
            // 
            this.groupBox2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox2.Controls.Add(this.btn_update_grid);
            this.groupBox2.Controls.Add(this.btn_check_json);
            this.groupBox2.Controls.Add(this.btn_json_beautify);
            this.groupBox2.Controls.Add(this.txt_result);
            this.groupBox2.Location = new System.Drawing.Point(6, 315);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(1177, 239);
            this.groupBox2.TabIndex = 2;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Result";
            // 
            // btn_update_grid
            // 
            this.btn_update_grid.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btn_update_grid.Location = new System.Drawing.Point(1060, 87);
            this.btn_update_grid.Name = "btn_update_grid";
            this.btn_update_grid.Size = new System.Drawing.Size(102, 23);
            this.btn_update_grid.TabIndex = 5;
            this.btn_update_grid.Text = "Update Grid";
            this.btn_update_grid.UseVisualStyleBackColor = true;
            this.btn_update_grid.Click += new System.EventHandler(this.btn_update_grid_Click);
            // 
            // btn_check_json
            // 
            this.btn_check_json.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btn_check_json.Location = new System.Drawing.Point(1059, 57);
            this.btn_check_json.Name = "btn_check_json";
            this.btn_check_json.Size = new System.Drawing.Size(103, 23);
            this.btn_check_json.TabIndex = 4;
            this.btn_check_json.Text = "Check JSON";
            this.btn_check_json.UseVisualStyleBackColor = true;
            this.btn_check_json.Click += new System.EventHandler(this.btn_check_json_Click);
            // 
            // btn_json_beautify
            // 
            this.btn_json_beautify.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btn_json_beautify.Location = new System.Drawing.Point(1059, 28);
            this.btn_json_beautify.Name = "btn_json_beautify";
            this.btn_json_beautify.Size = new System.Drawing.Size(103, 23);
            this.btn_json_beautify.TabIndex = 3;
            this.btn_json_beautify.Text = "Beautify JSON";
            this.btn_json_beautify.UseVisualStyleBackColor = true;
            this.btn_json_beautify.Click += new System.EventHandler(this.btn_json_beautify_Click);
            // 
            // frm_match_company_info
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1192, 566);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Name = "frm_match_company_info"; 
            ((System.ComponentModel.ISupportInitialize)(this.dgv_company)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dgv_company;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TextBox txt_result;
        private System.Windows.Forms.Button btn_load;
        private System.Windows.Forms.TextBox txt_condition;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Button btn_save;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Label lb_row_id;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label lb_company_id;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button btn_check_json;
        private System.Windows.Forms.Button btn_json_beautify;
        private System.Windows.Forms.Button btn_update_grid;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btn_update;
        private System.Windows.Forms.TextBox txt_name;
        private System.Windows.Forms.TextBox txt_url;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txt_other_names;
        private System.Windows.Forms.TextBox txt_other_urls;
        private System.Windows.Forms.TextBox txt_pay_ways;
        private System.Windows.Forms.Label label7;
    }
}