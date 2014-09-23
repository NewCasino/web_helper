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
            this.btn_add_all = new System.Windows.Forms.Button();
            this.btn_check_matchs = new System.Windows.Forms.Button();
            this.btn_check_persent = new System.Windows.Forms.Button();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.txt_result = new System.Windows.Forms.TextBox();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.dgv_result = new System.Windows.Forms.DataGridView();
            this.btn_check_qty = new System.Windows.Forms.Button();
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
            this.groupBox1.Controls.Add(this.btn_check_qty);
            this.groupBox1.Controls.Add(this.btn_add_all);
            this.groupBox1.Controls.Add(this.btn_check_matchs);
            this.groupBox1.Controls.Add(this.btn_check_persent);
            this.groupBox1.Location = new System.Drawing.Point(4, 2);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(916, 56);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Operation";
            // 
            // btn_add_all
            // 
            this.btn_add_all.Location = new System.Drawing.Point(252, 19);
            this.btn_add_all.Name = "btn_add_all";
            this.btn_add_all.Size = new System.Drawing.Size(116, 25);
            this.btn_add_all.TabIndex = 2;
            this.btn_add_all.Text = "Add ALL";
            this.btn_add_all.UseVisualStyleBackColor = true;
            this.btn_add_all.Click += new System.EventHandler(this.btn_add_all_Click);
            // 
            // btn_check_matchs
            // 
            this.btn_check_matchs.Location = new System.Drawing.Point(130, 19);
            this.btn_check_matchs.Name = "btn_check_matchs";
            this.btn_check_matchs.Size = new System.Drawing.Size(116, 25);
            this.btn_check_matchs.TabIndex = 1;
            this.btn_check_matchs.Text = "Check Matchs";
            this.btn_check_matchs.UseVisualStyleBackColor = true;
            this.btn_check_matchs.Click += new System.EventHandler(this.btn_check_matchs_Click);
            // 
            // btn_check_persent
            // 
            this.btn_check_persent.Location = new System.Drawing.Point(8, 19);
            this.btn_check_persent.Name = "btn_check_persent";
            this.btn_check_persent.Size = new System.Drawing.Size(116, 25);
            this.btn_check_persent.TabIndex = 0;
            this.btn_check_persent.Text = "Check Persent";
            this.btn_check_persent.UseVisualStyleBackColor = true;
            this.btn_check_persent.Click += new System.EventHandler(this.btn_check_persent_Click);
            // 
            // tabControl1
            // 
            this.tabControl1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Location = new System.Drawing.Point(4, 64);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(924, 481);
            this.tabControl1.TabIndex = 1;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.txt_result);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(916, 455);
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
            this.txt_result.Size = new System.Drawing.Size(910, 449);
            this.txt_result.TabIndex = 0;
            this.txt_result.WordWrap = false;
            this.txt_result.TextChanged += new System.EventHandler(this.txt_result_TextChanged);
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.dgv_result);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Size = new System.Drawing.Size(916, 455);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "TABLE";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // dgv_result
            // 
            this.dgv_result.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgv_result.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgv_result.Location = new System.Drawing.Point(0, 0);
            this.dgv_result.Name = "dgv_result";
            this.dgv_result.Size = new System.Drawing.Size(916, 455);
            this.dgv_result.TabIndex = 0;
            // 
            // btn_check_qty
            // 
            this.btn_check_qty.Location = new System.Drawing.Point(374, 19);
            this.btn_check_qty.Name = "btn_check_qty";
            this.btn_check_qty.Size = new System.Drawing.Size(116, 25);
            this.btn_check_qty.TabIndex = 3;
            this.btn_check_qty.Text = "Check Qty";
            this.btn_check_qty.UseVisualStyleBackColor = true;
            this.btn_check_qty.Click += new System.EventHandler(this.btn_check_qty_Click);
            // 
            // frm_match_100_check
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(926, 545);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.groupBox1);
            this.Name = "frm_match_100_check";
            this.Text = "100 Check";
            this.groupBox1.ResumeLayout(false);
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
        private System.Windows.Forms.Button btn_check_persent;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.DataGridView dgv_result;
        private System.Windows.Forms.Button btn_check_matchs;
        private System.Windows.Forms.Button btn_add_all;
        private System.Windows.Forms.Button btn_check_qty;
    }
}