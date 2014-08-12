namespace web_helper
{
    partial class frm_match_500_team
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
            this.btn_read_to_db = new System.Windows.Forms.Button();
            this.btn_step_3 = new System.Windows.Forms.Button();
            this.btn_step_2 = new System.Windows.Forms.Button();
            this.btn_step_1 = new System.Windows.Forms.Button();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.Text = new System.Windows.Forms.TabPage();
            this.txt_result = new System.Windows.Forms.TextBox();
            this.btn_read_detail = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.Text.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.btn_read_detail);
            this.groupBox1.Controls.Add(this.btn_read_to_db);
            this.groupBox1.Controls.Add(this.btn_step_3);
            this.groupBox1.Controls.Add(this.btn_step_2);
            this.groupBox1.Controls.Add(this.btn_step_1);
            this.groupBox1.Location = new System.Drawing.Point(4, 2);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(981, 69);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Operation";
            // 
            // btn_read_to_db
            // 
            this.btn_read_to_db.Location = new System.Drawing.Point(284, 28);
            this.btn_read_to_db.Name = "btn_read_to_db";
            this.btn_read_to_db.Size = new System.Drawing.Size(85, 23);
            this.btn_read_to_db.TabIndex = 3;
            this.btn_read_to_db.Text = "Read To DB";
            this.btn_read_to_db.UseVisualStyleBackColor = true;
            this.btn_read_to_db.Click += new System.EventHandler(this.btn_read_to_db_Click);
            // 
            // btn_step_3
            // 
            this.btn_step_3.Location = new System.Drawing.Point(182, 28);
            this.btn_step_3.Name = "btn_step_3";
            this.btn_step_3.Size = new System.Drawing.Size(75, 23);
            this.btn_step_3.TabIndex = 2;
            this.btn_step_3.Text = "Step 3";
            this.btn_step_3.UseVisualStyleBackColor = true;
            this.btn_step_3.Click += new System.EventHandler(this.btn_step_3_Click);
            // 
            // btn_step_2
            // 
            this.btn_step_2.Location = new System.Drawing.Point(101, 28);
            this.btn_step_2.Name = "btn_step_2";
            this.btn_step_2.Size = new System.Drawing.Size(75, 23);
            this.btn_step_2.TabIndex = 1;
            this.btn_step_2.Text = "Step 2";
            this.btn_step_2.UseVisualStyleBackColor = true;
            this.btn_step_2.Click += new System.EventHandler(this.btn_step_2_Click);
            // 
            // btn_step_1
            // 
            this.btn_step_1.Location = new System.Drawing.Point(20, 28);
            this.btn_step_1.Name = "btn_step_1";
            this.btn_step_1.Size = new System.Drawing.Size(75, 23);
            this.btn_step_1.TabIndex = 0;
            this.btn_step_1.Text = "Step 1";
            this.btn_step_1.UseVisualStyleBackColor = true;
            this.btn_step_1.Click += new System.EventHandler(this.btn_step_1_Click);
            // 
            // tabControl1
            // 
            this.tabControl1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.tabControl1.Controls.Add(this.Text);
            this.tabControl1.Location = new System.Drawing.Point(4, 77);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(985, 488);
            this.tabControl1.TabIndex = 1;
            // 
            // Text
            // 
            this.Text.Controls.Add(this.txt_result);
            this.Text.Location = new System.Drawing.Point(4, 22);
            this.Text.Name = "Text";
            this.Text.Padding = new System.Windows.Forms.Padding(3);
            this.Text.Size = new System.Drawing.Size(977, 462);
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
            this.txt_result.Size = new System.Drawing.Size(971, 456);
            this.txt_result.TabIndex = 0;
            this.txt_result.WordWrap = false;
            this.txt_result.TextChanged += new System.EventHandler(this.txt_result_TextChanged);
            // 
            // btn_read_detail
            // 
            this.btn_read_detail.Location = new System.Drawing.Point(389, 28);
            this.btn_read_detail.Name = "btn_read_detail";
            this.btn_read_detail.Size = new System.Drawing.Size(75, 23);
            this.btn_read_detail.TabIndex = 4;
            this.btn_read_detail.Text = "Read Detail";
            this.btn_read_detail.UseVisualStyleBackColor = true;
            this.btn_read_detail.Click += new System.EventHandler(this.btn_read_detail_Click);
            // 
            // frm_match_500_team
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(992, 566);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.groupBox1);
            this.Name = "frm_match_500_team";
            this.groupBox1.ResumeLayout(false);
            this.tabControl1.ResumeLayout(false);
            this.Text.ResumeLayout(false);
            this.Text.PerformLayout();
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
        private System.Windows.Forms.Button btn_read_detail;
    }
}