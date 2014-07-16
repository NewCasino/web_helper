namespace web_helper
{
    partial class frm_match_compute_by_company
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
            this.groupBox6 = new System.Windows.Forms.GroupBox();
            this.btn_three_range = new System.Windows.Forms.Button();
            this.cb_three_persent_desc = new System.Windows.Forms.CheckBox();
            this.cb_three_persent_asc = new System.Windows.Forms.CheckBox();
            this.cb_three_company_desc = new System.Windows.Forms.CheckBox();
            this.cb_three_company_asc = new System.Windows.Forms.CheckBox();
            this.btn_three_match = new System.Windows.Forms.Button();
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.btn_two_range = new System.Windows.Forms.Button();
            this.cb_two_persent_desc = new System.Windows.Forms.CheckBox();
            this.cb_two_persent_asc = new System.Windows.Forms.CheckBox();
            this.cb_two_company_desc = new System.Windows.Forms.CheckBox();
            this.cb_two_company_asc = new System.Windows.Forms.CheckBox();
            this.btn_two_match = new System.Windows.Forms.Button();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.btn_single_range = new System.Windows.Forms.Button();
            this.cb_persent_desc = new System.Windows.Forms.CheckBox();
            this.cb_persent_asc = new System.Windows.Forms.CheckBox();
            this.cb_company_desc = new System.Windows.Forms.CheckBox();
            this.cb_company_asc = new System.Windows.Forms.CheckBox();
            this.btn_single_match = new System.Windows.Forms.Button();
            this.txt_condition = new System.Windows.Forms.TextBox();
            this.btn_load = new System.Windows.Forms.Button();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.Condition = new System.Windows.Forms.TabPage();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.btn_company_reverse = new System.Windows.Forms.Button();
            this.btn_company_all = new System.Windows.Forms.Button();
            this.dgv_company = new System.Windows.Forms.DataGridView();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.btn_match_reverse = new System.Windows.Forms.Button();
            this.btn_match_all = new System.Windows.Forms.Button();
            this.dgv_match = new System.Windows.Forms.DataGridView();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.dgv_all = new System.Windows.Forms.DataGridView();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.txt_result = new System.Windows.Forms.TextBox();
            this.groupBox1.SuspendLayout();
            this.groupBox6.SuspendLayout();
            this.groupBox5.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.Condition.SuspendLayout();
            this.groupBox3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_company)).BeginInit();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_match)).BeginInit();
            this.tabPage1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_all)).BeginInit();
            this.tabPage2.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.groupBox1.Controls.Add(this.groupBox6);
            this.groupBox1.Controls.Add(this.groupBox5);
            this.groupBox1.Controls.Add(this.groupBox4);
            this.groupBox1.Controls.Add(this.txt_condition);
            this.groupBox1.Controls.Add(this.btn_load);
            this.groupBox1.Location = new System.Drawing.Point(6, 2);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(264, 563);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Operation";
            // 
            // groupBox6
            // 
            this.groupBox6.Controls.Add(this.btn_three_range);
            this.groupBox6.Controls.Add(this.cb_three_persent_desc);
            this.groupBox6.Controls.Add(this.cb_three_persent_asc);
            this.groupBox6.Controls.Add(this.cb_three_company_desc);
            this.groupBox6.Controls.Add(this.cb_three_company_asc);
            this.groupBox6.Controls.Add(this.btn_three_match);
            this.groupBox6.Location = new System.Drawing.Point(6, 270);
            this.groupBox6.Name = "groupBox6";
            this.groupBox6.Size = new System.Drawing.Size(252, 108);
            this.groupBox6.TabIndex = 8;
            this.groupBox6.TabStop = false;
            this.groupBox6.Text = "Three";
            // 
            // btn_three_range
            // 
            this.btn_three_range.Location = new System.Drawing.Point(118, 72);
            this.btn_three_range.Name = "btn_three_range";
            this.btn_three_range.Size = new System.Drawing.Size(75, 21);
            this.btn_three_range.TabIndex = 14;
            this.btn_three_range.Text = "Range";
            this.btn_three_range.UseVisualStyleBackColor = true;
            this.btn_three_range.Click += new System.EventHandler(this.btn_three_range_Click);
            // 
            // cb_three_persent_desc
            // 
            this.cb_three_persent_desc.AutoSize = true;
            this.cb_three_persent_desc.Location = new System.Drawing.Point(118, 20);
            this.cb_three_persent_desc.Name = "cb_three_persent_desc";
            this.cb_three_persent_desc.Size = new System.Drawing.Size(96, 16);
            this.cb_three_persent_desc.TabIndex = 12;
            this.cb_three_persent_desc.Text = "Persent DESC";
            this.cb_three_persent_desc.UseVisualStyleBackColor = true;
            // 
            // cb_three_persent_asc
            // 
            this.cb_three_persent_asc.AutoSize = true;
            this.cb_three_persent_asc.Checked = true;
            this.cb_three_persent_asc.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cb_three_persent_asc.Location = new System.Drawing.Point(6, 20);
            this.cb_three_persent_asc.Name = "cb_three_persent_asc";
            this.cb_three_persent_asc.Size = new System.Drawing.Size(90, 16);
            this.cb_three_persent_asc.TabIndex = 11;
            this.cb_three_persent_asc.Text = "Persent ASC";
            this.cb_three_persent_asc.UseVisualStyleBackColor = true;
            // 
            // cb_three_company_desc
            // 
            this.cb_three_company_desc.AutoSize = true;
            this.cb_three_company_desc.Location = new System.Drawing.Point(118, 48);
            this.cb_three_company_desc.Name = "cb_three_company_desc";
            this.cb_three_company_desc.Size = new System.Drawing.Size(96, 16);
            this.cb_three_company_desc.TabIndex = 10;
            this.cb_three_company_desc.Text = "Company DESC";
            this.cb_three_company_desc.UseVisualStyleBackColor = true;
            // 
            // cb_three_company_asc
            // 
            this.cb_three_company_asc.AutoSize = true;
            this.cb_three_company_asc.Location = new System.Drawing.Point(6, 48);
            this.cb_three_company_asc.Name = "cb_three_company_asc";
            this.cb_three_company_asc.Size = new System.Drawing.Size(90, 16);
            this.cb_three_company_asc.TabIndex = 9;
            this.cb_three_company_asc.Text = "Company ASC";
            this.cb_three_company_asc.UseVisualStyleBackColor = true;
            // 
            // btn_three_match
            // 
            this.btn_three_match.Location = new System.Drawing.Point(25, 72);
            this.btn_three_match.Name = "btn_three_match";
            this.btn_three_match.Size = new System.Drawing.Size(75, 21);
            this.btn_three_match.TabIndex = 5;
            this.btn_three_match.Text = "Three";
            this.btn_three_match.UseVisualStyleBackColor = true;
            this.btn_three_match.Click += new System.EventHandler(this.btn_three_match_Click);
            // 
            // groupBox5
            // 
            this.groupBox5.Controls.Add(this.btn_two_range);
            this.groupBox5.Controls.Add(this.cb_two_persent_desc);
            this.groupBox5.Controls.Add(this.cb_two_persent_asc);
            this.groupBox5.Controls.Add(this.cb_two_company_desc);
            this.groupBox5.Controls.Add(this.cb_two_company_asc);
            this.groupBox5.Controls.Add(this.btn_two_match);
            this.groupBox5.Location = new System.Drawing.Point(6, 156);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Size = new System.Drawing.Size(252, 108);
            this.groupBox5.TabIndex = 7;
            this.groupBox5.TabStop = false;
            this.groupBox5.Text = "Two";
            // 
            // btn_two_range
            // 
            this.btn_two_range.Location = new System.Drawing.Point(118, 69);
            this.btn_two_range.Name = "btn_two_range";
            this.btn_two_range.Size = new System.Drawing.Size(75, 21);
            this.btn_two_range.TabIndex = 13;
            this.btn_two_range.Text = "Range";
            this.btn_two_range.UseVisualStyleBackColor = true;
            this.btn_two_range.Click += new System.EventHandler(this.btn_two_range_Click);
            // 
            // cb_two_persent_desc
            // 
            this.cb_two_persent_desc.AutoSize = true;
            this.cb_two_persent_desc.Location = new System.Drawing.Point(118, 20);
            this.cb_two_persent_desc.Name = "cb_two_persent_desc";
            this.cb_two_persent_desc.Size = new System.Drawing.Size(96, 16);
            this.cb_two_persent_desc.TabIndex = 12;
            this.cb_two_persent_desc.Text = "Persent DESC";
            this.cb_two_persent_desc.UseVisualStyleBackColor = true;
            // 
            // cb_two_persent_asc
            // 
            this.cb_two_persent_asc.AutoSize = true;
            this.cb_two_persent_asc.Checked = true;
            this.cb_two_persent_asc.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cb_two_persent_asc.Location = new System.Drawing.Point(6, 20);
            this.cb_two_persent_asc.Name = "cb_two_persent_asc";
            this.cb_two_persent_asc.Size = new System.Drawing.Size(90, 16);
            this.cb_two_persent_asc.TabIndex = 11;
            this.cb_two_persent_asc.Text = "Persent ASC";
            this.cb_two_persent_asc.UseVisualStyleBackColor = true;
            // 
            // cb_two_company_desc
            // 
            this.cb_two_company_desc.AutoSize = true;
            this.cb_two_company_desc.Location = new System.Drawing.Point(118, 48);
            this.cb_two_company_desc.Name = "cb_two_company_desc";
            this.cb_two_company_desc.Size = new System.Drawing.Size(96, 16);
            this.cb_two_company_desc.TabIndex = 10;
            this.cb_two_company_desc.Text = "Company DESC";
            this.cb_two_company_desc.UseVisualStyleBackColor = true;
            // 
            // cb_two_company_asc
            // 
            this.cb_two_company_asc.AutoSize = true;
            this.cb_two_company_asc.Location = new System.Drawing.Point(6, 48);
            this.cb_two_company_asc.Name = "cb_two_company_asc";
            this.cb_two_company_asc.Size = new System.Drawing.Size(90, 16);
            this.cb_two_company_asc.TabIndex = 9;
            this.cb_two_company_asc.Text = "Company ASC";
            this.cb_two_company_asc.UseVisualStyleBackColor = true;
            // 
            // btn_two_match
            // 
            this.btn_two_match.Location = new System.Drawing.Point(25, 69);
            this.btn_two_match.Name = "btn_two_match";
            this.btn_two_match.Size = new System.Drawing.Size(75, 21);
            this.btn_two_match.TabIndex = 5;
            this.btn_two_match.Text = "Two";
            this.btn_two_match.UseVisualStyleBackColor = true;
            this.btn_two_match.Click += new System.EventHandler(this.btn_two_match_Click);
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.btn_single_range);
            this.groupBox4.Controls.Add(this.cb_persent_desc);
            this.groupBox4.Controls.Add(this.cb_persent_asc);
            this.groupBox4.Controls.Add(this.cb_company_desc);
            this.groupBox4.Controls.Add(this.cb_company_asc);
            this.groupBox4.Controls.Add(this.btn_single_match);
            this.groupBox4.Location = new System.Drawing.Point(6, 47);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(252, 103);
            this.groupBox4.TabIndex = 6;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Single";
            // 
            // btn_single_range
            // 
            this.btn_single_range.Location = new System.Drawing.Point(118, 70);
            this.btn_single_range.Name = "btn_single_range";
            this.btn_single_range.Size = new System.Drawing.Size(75, 21);
            this.btn_single_range.TabIndex = 9;
            this.btn_single_range.Text = "Range";
            this.btn_single_range.UseVisualStyleBackColor = true;
            this.btn_single_range.Click += new System.EventHandler(this.btn_single_range_Click);
            // 
            // cb_persent_desc
            // 
            this.cb_persent_desc.AutoSize = true;
            this.cb_persent_desc.Location = new System.Drawing.Point(118, 21);
            this.cb_persent_desc.Name = "cb_persent_desc";
            this.cb_persent_desc.Size = new System.Drawing.Size(96, 16);
            this.cb_persent_desc.TabIndex = 8;
            this.cb_persent_desc.Text = "Persent DESC";
            this.cb_persent_desc.UseVisualStyleBackColor = true;
            // 
            // cb_persent_asc
            // 
            this.cb_persent_asc.AutoSize = true;
            this.cb_persent_asc.Checked = true;
            this.cb_persent_asc.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cb_persent_asc.Location = new System.Drawing.Point(6, 21);
            this.cb_persent_asc.Name = "cb_persent_asc";
            this.cb_persent_asc.Size = new System.Drawing.Size(90, 16);
            this.cb_persent_asc.TabIndex = 7;
            this.cb_persent_asc.Text = "Persent ASC";
            this.cb_persent_asc.UseVisualStyleBackColor = true;
            // 
            // cb_company_desc
            // 
            this.cb_company_desc.AutoSize = true;
            this.cb_company_desc.Location = new System.Drawing.Point(118, 49);
            this.cb_company_desc.Name = "cb_company_desc";
            this.cb_company_desc.Size = new System.Drawing.Size(96, 16);
            this.cb_company_desc.TabIndex = 6;
            this.cb_company_desc.Text = "Company DESC";
            this.cb_company_desc.UseVisualStyleBackColor = true;
            // 
            // cb_company_asc
            // 
            this.cb_company_asc.AutoSize = true;
            this.cb_company_asc.Location = new System.Drawing.Point(6, 49);
            this.cb_company_asc.Name = "cb_company_asc";
            this.cb_company_asc.Size = new System.Drawing.Size(90, 16);
            this.cb_company_asc.TabIndex = 5;
            this.cb_company_asc.Text = "Company ASC";
            this.cb_company_asc.UseVisualStyleBackColor = true;
            // 
            // btn_single_match
            // 
            this.btn_single_match.Location = new System.Drawing.Point(25, 70);
            this.btn_single_match.Name = "btn_single_match";
            this.btn_single_match.Size = new System.Drawing.Size(75, 21);
            this.btn_single_match.TabIndex = 4;
            this.btn_single_match.Text = "Single";
            this.btn_single_match.UseVisualStyleBackColor = true;
            this.btn_single_match.Click += new System.EventHandler(this.btn_single_match_Click);
            // 
            // txt_condition
            // 
            this.txt_condition.Location = new System.Drawing.Point(6, 20);
            this.txt_condition.Name = "txt_condition";
            this.txt_condition.Size = new System.Drawing.Size(171, 21);
            this.txt_condition.TabIndex = 3;
            // 
            // btn_load
            // 
            this.btn_load.Location = new System.Drawing.Point(183, 19);
            this.btn_load.Name = "btn_load";
            this.btn_load.Size = new System.Drawing.Size(75, 21);
            this.btn_load.TabIndex = 2;
            this.btn_load.Text = "Load";
            this.btn_load.UseVisualStyleBackColor = true;
            this.btn_load.Click += new System.EventHandler(this.btn_load_Click);
            // 
            // tabControl1
            // 
            this.tabControl1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tabControl1.Controls.Add(this.Condition);
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Location = new System.Drawing.Point(276, 2);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(909, 563);
            this.tabControl1.TabIndex = 1;
            // 
            // Condition
            // 
            this.Condition.Controls.Add(this.groupBox3);
            this.Condition.Controls.Add(this.groupBox2);
            this.Condition.Location = new System.Drawing.Point(4, 22);
            this.Condition.Name = "Condition";
            this.Condition.Size = new System.Drawing.Size(901, 537);
            this.Condition.TabIndex = 2;
            this.Condition.Text = "Condition";
            this.Condition.UseVisualStyleBackColor = true;
            // 
            // groupBox3
            // 
            this.groupBox3.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox3.Controls.Add(this.btn_company_reverse);
            this.groupBox3.Controls.Add(this.btn_company_all);
            this.groupBox3.Controls.Add(this.dgv_company);
            this.groupBox3.Location = new System.Drawing.Point(502, 5);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(391, 526);
            this.groupBox3.TabIndex = 3;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Company";
            // 
            // btn_company_reverse
            // 
            this.btn_company_reverse.Location = new System.Drawing.Point(87, 20);
            this.btn_company_reverse.Name = "btn_company_reverse";
            this.btn_company_reverse.Size = new System.Drawing.Size(75, 23);
            this.btn_company_reverse.TabIndex = 3;
            this.btn_company_reverse.Text = "REVERSE";
            this.btn_company_reverse.UseVisualStyleBackColor = true;
            this.btn_company_reverse.Click += new System.EventHandler(this.btn_company_reverse_Click);
            // 
            // btn_company_all
            // 
            this.btn_company_all.Location = new System.Drawing.Point(6, 20);
            this.btn_company_all.Name = "btn_company_all";
            this.btn_company_all.Size = new System.Drawing.Size(75, 23);
            this.btn_company_all.TabIndex = 2;
            this.btn_company_all.Text = "ALL";
            this.btn_company_all.UseVisualStyleBackColor = true;
            this.btn_company_all.Click += new System.EventHandler(this.btn_company_all_Click);
            // 
            // dgv_company
            // 
            this.dgv_company.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgv_company.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgv_company.Location = new System.Drawing.Point(6, 54);
            this.dgv_company.Name = "dgv_company";
            this.dgv_company.Size = new System.Drawing.Size(379, 465);
            this.dgv_company.TabIndex = 1;
            this.dgv_company.DataBindingComplete += new System.Windows.Forms.DataGridViewBindingCompleteEventHandler(this.dgv_company_DataBindingComplete);
            // 
            // groupBox2
            // 
            this.groupBox2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.groupBox2.Controls.Add(this.btn_match_reverse);
            this.groupBox2.Controls.Add(this.btn_match_all);
            this.groupBox2.Controls.Add(this.dgv_match);
            this.groupBox2.Location = new System.Drawing.Point(3, 5);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(493, 526);
            this.groupBox2.TabIndex = 2;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Condition";
            // 
            // btn_match_reverse
            // 
            this.btn_match_reverse.Location = new System.Drawing.Point(87, 20);
            this.btn_match_reverse.Name = "btn_match_reverse";
            this.btn_match_reverse.Size = new System.Drawing.Size(75, 23);
            this.btn_match_reverse.TabIndex = 2;
            this.btn_match_reverse.Text = "REVERSE";
            this.btn_match_reverse.UseVisualStyleBackColor = true;
            this.btn_match_reverse.Click += new System.EventHandler(this.btn_match_reverse_Click);
            // 
            // btn_match_all
            // 
            this.btn_match_all.Location = new System.Drawing.Point(6, 20);
            this.btn_match_all.Name = "btn_match_all";
            this.btn_match_all.Size = new System.Drawing.Size(75, 23);
            this.btn_match_all.TabIndex = 1;
            this.btn_match_all.Text = "ALL";
            this.btn_match_all.UseVisualStyleBackColor = true;
            this.btn_match_all.Click += new System.EventHandler(this.btn_match_all_Click);
            // 
            // dgv_match
            // 
            this.dgv_match.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgv_match.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgv_match.Location = new System.Drawing.Point(6, 54);
            this.dgv_match.Name = "dgv_match";
            this.dgv_match.Size = new System.Drawing.Size(481, 465);
            this.dgv_match.TabIndex = 0;
            this.dgv_match.DataBindingComplete += new System.Windows.Forms.DataGridViewBindingCompleteEventHandler(this.dgv_match_DataBindingComplete);
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.dgv_all);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(901, 537);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "All Data";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // dgv_all
            // 
            this.dgv_all.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgv_all.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgv_all.Location = new System.Drawing.Point(3, 3);
            this.dgv_all.Name = "dgv_all";
            this.dgv_all.Size = new System.Drawing.Size(895, 531);
            this.dgv_all.TabIndex = 0;
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.txt_result);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(901, 537);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Result Text";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // txt_result
            // 
            this.txt_result.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txt_result.Font = new System.Drawing.Font("Lucida Console", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_result.Location = new System.Drawing.Point(3, 3);
            this.txt_result.Multiline = true;
            this.txt_result.Name = "txt_result";
            this.txt_result.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.txt_result.Size = new System.Drawing.Size(895, 531);
            this.txt_result.TabIndex = 0;
            this.txt_result.WordWrap = false;
            this.txt_result.TextChanged += new System.EventHandler(this.txt_result_TextChanged);
            // 
            // frm_match_compute_by_company
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1185, 570);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.groupBox1);
            this.Name = "frm_match_compute_by_company";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox6.ResumeLayout(false);
            this.groupBox6.PerformLayout();
            this.groupBox5.ResumeLayout(false);
            this.groupBox5.PerformLayout();
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            this.tabControl1.ResumeLayout(false);
            this.Condition.ResumeLayout(false);
            this.groupBox3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgv_company)).EndInit();
            this.groupBox2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgv_match)).EndInit();
            this.tabPage1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgv_all)).EndInit();
            this.tabPage2.ResumeLayout(false);
            this.tabPage2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.DataGridView dgv_all;
        private System.Windows.Forms.TextBox txt_result;
        private System.Windows.Forms.Button btn_load;
        private System.Windows.Forms.TabPage Condition;
        private System.Windows.Forms.DataGridView dgv_company;
        private System.Windows.Forms.DataGridView dgv_match;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.TextBox txt_condition;
        private System.Windows.Forms.Button btn_single_match;
        private System.Windows.Forms.Button btn_two_match;
        private System.Windows.Forms.GroupBox groupBox5;
        private System.Windows.Forms.CheckBox cb_two_persent_desc;
        private System.Windows.Forms.CheckBox cb_two_persent_asc;
        private System.Windows.Forms.CheckBox cb_two_company_desc;
        private System.Windows.Forms.CheckBox cb_two_company_asc;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.CheckBox cb_persent_desc;
        private System.Windows.Forms.CheckBox cb_persent_asc;
        private System.Windows.Forms.CheckBox cb_company_desc;
        private System.Windows.Forms.CheckBox cb_company_asc;
        private System.Windows.Forms.Button btn_company_all;
        private System.Windows.Forms.Button btn_match_all;
        private System.Windows.Forms.Button btn_company_reverse;
        private System.Windows.Forms.Button btn_match_reverse;
        private System.Windows.Forms.GroupBox groupBox6;
        private System.Windows.Forms.CheckBox cb_three_persent_desc;
        private System.Windows.Forms.CheckBox cb_three_persent_asc;
        private System.Windows.Forms.CheckBox cb_three_company_desc;
        private System.Windows.Forms.CheckBox cb_three_company_asc;
        private System.Windows.Forms.Button btn_three_match;
        private System.Windows.Forms.Button btn_single_range;
        private System.Windows.Forms.Button btn_three_range;
        private System.Windows.Forms.Button btn_two_range;
    }
}