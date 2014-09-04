namespace web_helper
{
    partial class frm_match_100_team_analyse
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
            this.btn_read_to_names = new System.Windows.Forms.Button();
            this.btn_add_simple_complex = new System.Windows.Forms.Button();
            this.btn_90vs_read_all_team = new System.Windows.Forms.Button();
            this.btn_90vs_read_leage = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.btn_test = new System.Windows.Forms.Button();
            this.btn_read_to_db = new System.Windows.Forms.Button();
            this.btn_step_3 = new System.Windows.Forms.Button();
            this.btn_step_2 = new System.Windows.Forms.Button();
            this.btn_step_1 = new System.Windows.Forms.Button();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.Text = new System.Windows.Forms.TabPage();
            this.txt_result = new System.Windows.Forms.TextBox();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.dgv_grid = new System.Windows.Forms.DataGridView();
            this.groupBox1.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.Text.SuspendLayout();
            this.tabPage1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_grid)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)));
            this.groupBox1.Controls.Add(this.btn_read_to_names);
            this.groupBox1.Controls.Add(this.btn_add_simple_complex);
            this.groupBox1.Controls.Add(this.btn_90vs_read_all_team);
            this.groupBox1.Controls.Add(this.btn_90vs_read_leage);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.btn_test);
            this.groupBox1.Controls.Add(this.btn_read_to_db);
            this.groupBox1.Controls.Add(this.btn_step_3);
            this.groupBox1.Controls.Add(this.btn_step_2);
            this.groupBox1.Controls.Add(this.btn_step_1);
            this.groupBox1.Location = new System.Drawing.Point(4, 2);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(265, 553);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Operation";
            // 
            // btn_read_to_names
            // 
            this.btn_read_to_names.Enabled = false;
            this.btn_read_to_names.Location = new System.Drawing.Point(6, 233);
            this.btn_read_to_names.Name = "btn_read_to_names";
            this.btn_read_to_names.Size = new System.Drawing.Size(111, 23);
            this.btn_read_to_names.TabIndex = 11;
            this.btn_read_to_names.Text = "Read To Names";
            this.btn_read_to_names.UseVisualStyleBackColor = true;
            this.btn_read_to_names.Click += new System.EventHandler(this.btn_read_to_names_Click);
            // 
            // btn_add_simple_complex
            // 
            this.btn_add_simple_complex.Enabled = false;
            this.btn_add_simple_complex.Location = new System.Drawing.Point(123, 233);
            this.btn_add_simple_complex.Name = "btn_add_simple_complex";
            this.btn_add_simple_complex.Size = new System.Drawing.Size(112, 23);
            this.btn_add_simple_complex.TabIndex = 10;
            this.btn_add_simple_complex.Text = "Add S+T";
            this.btn_add_simple_complex.UseVisualStyleBackColor = true;
            this.btn_add_simple_complex.Click += new System.EventHandler(this.btn_add_simple_complex_Click);
            // 
            // btn_90vs_read_all_team
            // 
            this.btn_90vs_read_all_team.Enabled = false;
            this.btn_90vs_read_all_team.Location = new System.Drawing.Point(122, 130);
            this.btn_90vs_read_all_team.Name = "btn_90vs_read_all_team";
            this.btn_90vs_read_all_team.Size = new System.Drawing.Size(111, 23);
            this.btn_90vs_read_all_team.TabIndex = 9;
            this.btn_90vs_read_all_team.Text = "Read All T";
            this.btn_90vs_read_all_team.UseVisualStyleBackColor = true;
            this.btn_90vs_read_all_team.Click += new System.EventHandler(this.btn_90vs_read_all_team_Click);
            // 
            // btn_90vs_read_leage
            // 
            this.btn_90vs_read_leage.Enabled = false;
            this.btn_90vs_read_leage.Location = new System.Drawing.Point(7, 130);
            this.btn_90vs_read_leage.Name = "btn_90vs_read_leage";
            this.btn_90vs_read_leage.Size = new System.Drawing.Size(111, 23);
            this.btn_90vs_read_leage.TabIndex = 8;
            this.btn_90vs_read_leage.Text = "Read Leage";
            this.btn_90vs_read_leage.UseVisualStyleBackColor = true;
            this.btn_90vs_read_leage.Click += new System.EventHandler(this.btn_90vs_read_leage_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(8, 108);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(48, 13);
            this.label1.TabIndex = 7;
            this.label1.Text = "------90vs";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(6, 22);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(43, 13);
            this.label2.TabIndex = 6;
            this.label2.Text = "------500";
            // 
            // btn_test
            // 
            this.btn_test.Location = new System.Drawing.Point(6, 262);
            this.btn_test.Name = "btn_test";
            this.btn_test.Size = new System.Drawing.Size(112, 23);
            this.btn_test.TabIndex = 4;
            this.btn_test.Text = "Test";
            this.btn_test.UseVisualStyleBackColor = true;
            this.btn_test.Click += new System.EventHandler(this.btn_test_Click);
            // 
            // btn_read_to_db
            // 
            this.btn_read_to_db.Enabled = false;
            this.btn_read_to_db.Location = new System.Drawing.Point(123, 72);
            this.btn_read_to_db.Name = "btn_read_to_db";
            this.btn_read_to_db.Size = new System.Drawing.Size(111, 23);
            this.btn_read_to_db.TabIndex = 3;
            this.btn_read_to_db.Text = "Read To DB";
            this.btn_read_to_db.UseVisualStyleBackColor = true;
            this.btn_read_to_db.Click += new System.EventHandler(this.btn_read_to_db_Click);
            // 
            // btn_step_3
            // 
            this.btn_step_3.Enabled = false;
            this.btn_step_3.Location = new System.Drawing.Point(6, 72);
            this.btn_step_3.Name = "btn_step_3";
            this.btn_step_3.Size = new System.Drawing.Size(111, 23);
            this.btn_step_3.TabIndex = 2;
            this.btn_step_3.Text = "Read T Detail";
            this.btn_step_3.UseVisualStyleBackColor = true;
            this.btn_step_3.Click += new System.EventHandler(this.btn_step_3_Click);
            // 
            // btn_step_2
            // 
            this.btn_step_2.Enabled = false;
            this.btn_step_2.Location = new System.Drawing.Point(122, 42);
            this.btn_step_2.Name = "btn_step_2";
            this.btn_step_2.Size = new System.Drawing.Size(111, 23);
            this.btn_step_2.TabIndex = 1;
            this.btn_step_2.Text = "Read All T";
            this.btn_step_2.UseVisualStyleBackColor = true;
            this.btn_step_2.Click += new System.EventHandler(this.btn_step_2_Click);
            // 
            // btn_step_1
            // 
            this.btn_step_1.Location = new System.Drawing.Point(7, 42);
            this.btn_step_1.Name = "btn_step_1";
            this.btn_step_1.Size = new System.Drawing.Size(111, 23);
            this.btn_step_1.TabIndex = 0;
            this.btn_step_1.Text = "Read Leage";
            this.btn_step_1.UseVisualStyleBackColor = true;
            this.btn_step_1.Click += new System.EventHandler(this.btn_step_1_Click);
            // 
            // tabControl1
            // 
            this.tabControl1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.tabControl1.Controls.Add(this.Text);
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Location = new System.Drawing.Point(275, 2);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(712, 562);
            this.tabControl1.TabIndex = 1;
            // 
            // Text
            // 
            this.Text.Controls.Add(this.txt_result);
            this.Text.Location = new System.Drawing.Point(4, 22);
            this.Text.Name = "Text";
            this.Text.Padding = new System.Windows.Forms.Padding(3);
            this.Text.Size = new System.Drawing.Size(704, 536);
            this.Text.TabIndex = 0;
            this.Text.Text = "Text";
            this.Text.UseVisualStyleBackColor = true;
            // 
            // txt_result
            // 
            this.txt_result.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txt_result.Font = new System.Drawing.Font("Lucida Console", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_result.Location = new System.Drawing.Point(3, 3);
            this.txt_result.MaxLength = 0;
            this.txt_result.Multiline = true;
            this.txt_result.Name = "txt_result";
            this.txt_result.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.txt_result.Size = new System.Drawing.Size(698, 530);
            this.txt_result.TabIndex = 0;
            this.txt_result.WordWrap = false;
            this.txt_result.TextChanged += new System.EventHandler(this.txt_result_TextChanged);
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.dgv_grid);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Size = new System.Drawing.Size(704, 536);
            this.tabPage1.TabIndex = 1;
            this.tabPage1.Text = "TABLE";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // dgv_grid
            // 
            this.dgv_grid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgv_grid.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgv_grid.Location = new System.Drawing.Point(0, 0);
            this.dgv_grid.Name = "dgv_grid";
            this.dgv_grid.Size = new System.Drawing.Size(704, 536);
            this.dgv_grid.TabIndex = 0;
            // 
            // frm_match_100_team_analyse
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(992, 566);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.groupBox1);
            this.Name = "frm_match_100_team_analyse";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.tabControl1.ResumeLayout(false);
            this.Text.ResumeLayout(false);
            this.Text.PerformLayout();
            this.tabPage1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgv_grid)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage Text;
        private System.Windows.Forms.TextBox txt_result;
        private System.Windows.Forms.Button btn_step_1;
        private System.Windows.Forms.Button btn_step_3;
        private System.Windows.Forms.Button btn_step_2;
        private System.Windows.Forms.Button btn_read_to_db;
        private System.Windows.Forms.Button btn_test;
        private System.Windows.Forms.Button btn_90vs_read_all_team;
        private System.Windows.Forms.Button btn_90vs_read_leage;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btn_add_simple_complex;
        private System.Windows.Forms.Button btn_read_to_names;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.DataGridView dgv_grid;
    }
}