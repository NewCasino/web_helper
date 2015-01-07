namespace web_helper
{
    partial class frm_match_100_check
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
            this.btn_clear_text = new System.Windows.Forms.Button();
            this.btn_repair = new System.Windows.Forms.Button();
            this.btn_insert_office = new System.Windows.Forms.Button();
            this.btn_analyse_one_by_other = new System.Windows.Forms.Button();
            this.btn_analyse_by_similar_name = new System.Windows.Forms.Button();
            this.btn_analyse_by_date_odd = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.btn_analyse_by_hand = new System.Windows.Forms.Button();
            this.btn_check_qty = new System.Windows.Forms.Button();
            this.btn_add_all = new System.Windows.Forms.Button();
            this.btn_check_matchs = new System.Windows.Forms.Button();
            this.btn_team_discrimination = new System.Windows.Forms.Button();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.txt_result = new System.Windows.Forms.TextBox();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.btn_update = new System.Windows.Forms.Button();
            this.dgv_result = new System.Windows.Forms.DataGridView();
            this.groupBox1.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_result)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.btn_clear_text);
            this.groupBox1.Controls.Add(this.btn_repair);
            this.groupBox1.Controls.Add(this.btn_insert_office);
            this.groupBox1.Controls.Add(this.btn_analyse_one_by_other);
            this.groupBox1.Controls.Add(this.btn_analyse_by_similar_name);
            this.groupBox1.Controls.Add(this.btn_analyse_by_date_odd);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.btn_analyse_by_hand);
            this.groupBox1.Controls.Add(this.btn_check_qty);
            this.groupBox1.Controls.Add(this.btn_add_all);
            this.groupBox1.Controls.Add(this.btn_check_matchs);
            this.groupBox1.Controls.Add(this.btn_team_discrimination);
            this.groupBox1.Location = new System.Drawing.Point(4, 2);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(1109, 79);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Operation";
            // 
            // btn_clear_text
            // 
            this.btn_clear_text.Location = new System.Drawing.Point(799, 19);
            this.btn_clear_text.Name = "btn_clear_text";
            this.btn_clear_text.Size = new System.Drawing.Size(116, 25);
            this.btn_clear_text.TabIndex = 12;
            this.btn_clear_text.Text = "Clear Text";
            this.btn_clear_text.UseVisualStyleBackColor = true;
            this.btn_clear_text.Click += new System.EventHandler(this.btn_clear_text_Click);
            // 
            // btn_repair
            // 
            this.btn_repair.Location = new System.Drawing.Point(189, 48);
            this.btn_repair.Name = "btn_repair";
            this.btn_repair.Size = new System.Drawing.Size(116, 25);
            this.btn_repair.TabIndex = 11;
            this.btn_repair.Text = "Repair MB";
            this.btn_repair.UseVisualStyleBackColor = true;
            this.btn_repair.Click += new System.EventHandler(this.btn_repair_Click);
            // 
            // btn_insert_office
            // 
            this.btn_insert_office.Location = new System.Drawing.Point(799, 48);
            this.btn_insert_office.Name = "btn_insert_office";
            this.btn_insert_office.Size = new System.Drawing.Size(116, 25);
            this.btn_insert_office.TabIndex = 10;
            this.btn_insert_office.Text = "Insert Office";
            this.btn_insert_office.UseVisualStyleBackColor = true;
            this.btn_insert_office.Click += new System.EventHandler(this.btn_insert_office_Click);
            // 
            // btn_analyse_one_by_other
            // 
            this.btn_analyse_one_by_other.Location = new System.Drawing.Point(311, 48);
            this.btn_analyse_one_by_other.Name = "btn_analyse_one_by_other";
            this.btn_analyse_one_by_other.Size = new System.Drawing.Size(116, 25);
            this.btn_analyse_one_by_other.TabIndex = 9;
            this.btn_analyse_one_by_other.Text = "A(One By Ohter)";
            this.btn_analyse_one_by_other.UseVisualStyleBackColor = true;
            this.btn_analyse_one_by_other.Click += new System.EventHandler(this.btn_analyse_one_by_other_Click);
            // 
            // btn_analyse_by_similar_name
            // 
            this.btn_analyse_by_similar_name.Location = new System.Drawing.Point(433, 48);
            this.btn_analyse_by_similar_name.Name = "btn_analyse_by_similar_name";
            this.btn_analyse_by_similar_name.Size = new System.Drawing.Size(116, 25);
            this.btn_analyse_by_similar_name.TabIndex = 8;
            this.btn_analyse_by_similar_name.Text = "A(Similar T)";
            this.btn_analyse_by_similar_name.UseVisualStyleBackColor = true;
            this.btn_analyse_by_similar_name.Click += new System.EventHandler(this.btn_analyse_by_similar_name_Click);
            // 
            // btn_analyse_by_date_odd
            // 
            this.btn_analyse_by_date_odd.Location = new System.Drawing.Point(555, 48);
            this.btn_analyse_by_date_odd.Name = "btn_analyse_by_date_odd";
            this.btn_analyse_by_date_odd.Size = new System.Drawing.Size(116, 25);
            this.btn_analyse_by_date_odd.TabIndex = 7;
            this.btn_analyse_by_date_odd.Text = "A(S and W Persent)";
            this.btn_analyse_by_date_odd.UseVisualStyleBackColor = true;
            this.btn_analyse_by_date_odd.Click += new System.EventHandler(this.btn_analyse_by_date_odd_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(11, 54);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(45, 13);
            this.label2.TabIndex = 6;
            this.label2.Text = "Update:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(14, 25);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(42, 13);
            this.label1.TabIndex = 5;
            this.label1.Text = "Report:";
            // 
            // btn_analyse_by_hand
            // 
            this.btn_analyse_by_hand.Location = new System.Drawing.Point(677, 48);
            this.btn_analyse_by_hand.Name = "btn_analyse_by_hand";
            this.btn_analyse_by_hand.Size = new System.Drawing.Size(116, 25);
            this.btn_analyse_by_hand.TabIndex = 4;
            this.btn_analyse_by_hand.Text = "A(By Hand)";
            this.btn_analyse_by_hand.UseVisualStyleBackColor = true;
            this.btn_analyse_by_hand.Click += new System.EventHandler(this.btn_analyse_by_hand_Click);
            // 
            // btn_check_qty
            // 
            this.btn_check_qty.Location = new System.Drawing.Point(311, 19);
            this.btn_check_qty.Name = "btn_check_qty";
            this.btn_check_qty.Size = new System.Drawing.Size(116, 25);
            this.btn_check_qty.TabIndex = 3;
            this.btn_check_qty.Text = "Load Qty";
            this.btn_check_qty.UseVisualStyleBackColor = true;
            this.btn_check_qty.Click += new System.EventHandler(this.btn_check_qty_Click);
            // 
            // btn_add_all
            // 
            this.btn_add_all.Location = new System.Drawing.Point(67, 48);
            this.btn_add_all.Name = "btn_add_all";
            this.btn_add_all.Size = new System.Drawing.Size(116, 25);
            this.btn_add_all.TabIndex = 2;
            this.btn_add_all.Text = "Add Persent";
            this.btn_add_all.UseVisualStyleBackColor = true;
            this.btn_add_all.Click += new System.EventHandler(this.btn_add_all_Click);
            // 
            // btn_check_matchs
            // 
            this.btn_check_matchs.Location = new System.Drawing.Point(189, 19);
            this.btn_check_matchs.Name = "btn_check_matchs";
            this.btn_check_matchs.Size = new System.Drawing.Size(116, 25);
            this.btn_check_matchs.TabIndex = 1;
            this.btn_check_matchs.Text = "Check Matchs";
            this.btn_check_matchs.UseVisualStyleBackColor = true;
            this.btn_check_matchs.Click += new System.EventHandler(this.btn_check_matchs_Click);
            // 
            // btn_team_discrimination
            // 
            this.btn_team_discrimination.Location = new System.Drawing.Point(67, 19);
            this.btn_team_discrimination.Name = "btn_team_discrimination";
            this.btn_team_discrimination.Size = new System.Drawing.Size(116, 25);
            this.btn_team_discrimination.TabIndex = 0;
            this.btn_team_discrimination.Text = "T Discrimination(%)";
            this.btn_team_discrimination.UseVisualStyleBackColor = true;
            this.btn_team_discrimination.Click += new System.EventHandler(this.btn_team_discrimination_Click);
            // 
            // tabControl1
            // 
            this.tabControl1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Location = new System.Drawing.Point(4, 87);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(1109, 458);
            this.tabControl1.TabIndex = 1;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.txt_result);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(1101, 432);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "TEXT";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // txt_result
            // 
            this.txt_result.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txt_result.Font = new System.Drawing.Font("Lucida Console", 8.25F);
            this.txt_result.Location = new System.Drawing.Point(3, 3);
            this.txt_result.Multiline = true;
            this.txt_result.Name = "txt_result";
            this.txt_result.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.txt_result.Size = new System.Drawing.Size(1095, 426);
            this.txt_result.TabIndex = 0;
            this.txt_result.WordWrap = false;
            this.txt_result.TextChanged += new System.EventHandler(this.txt_result_TextChanged);
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.btn_update);
            this.tabPage2.Controls.Add(this.dgv_result);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Size = new System.Drawing.Size(1101, 432);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "TABLE";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // btn_update
            // 
            this.btn_update.Location = new System.Drawing.Point(4, 7);
            this.btn_update.Name = "btn_update";
            this.btn_update.Size = new System.Drawing.Size(75, 23);
            this.btn_update.TabIndex = 5;
            this.btn_update.Text = "Update Grid";
            this.btn_update.UseVisualStyleBackColor = true;
            this.btn_update.Click += new System.EventHandler(this.btn_update_Click);
            // 
            // dgv_result
            // 
            this.dgv_result.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.dgv_result.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgv_result.Location = new System.Drawing.Point(8, 36);
            this.dgv_result.Name = "dgv_result";
            this.dgv_result.Size = new System.Drawing.Size(1085, 388);
            this.dgv_result.TabIndex = 0;
            // 
            // frm_match_100_check
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1119, 545);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.groupBox1);
            this.Name = "frm_match_100_check";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            this.tabPage2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgv_result)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TextBox txt_result;
        private System.Windows.Forms.Button btn_team_discrimination;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.DataGridView dgv_result;
        private System.Windows.Forms.Button btn_check_matchs;
        private System.Windows.Forms.Button btn_add_all;
        private System.Windows.Forms.Button btn_check_qty;
        private System.Windows.Forms.Button btn_analyse_by_hand;
        private System.Windows.Forms.Button btn_update;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btn_analyse_by_date_odd;
        private System.Windows.Forms.Button btn_analyse_by_similar_name;
        private System.Windows.Forms.Button btn_insert_office;
        private System.Windows.Forms.Button btn_analyse_one_by_other;
        private System.Windows.Forms.Button btn_repair;
        private System.Windows.Forms.Button btn_clear_text;
    }
}