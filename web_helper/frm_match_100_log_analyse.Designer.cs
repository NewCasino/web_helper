namespace web_helper
{
    partial class frm_match_100_log_analyse
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
            this.btn_into_offical = new System.Windows.Forms.Button();
            this.btn_update = new System.Windows.Forms.Button();
            this.btn_analyse = new System.Windows.Forms.Button();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.txt_result = new System.Windows.Forms.TextBox();
            this.tabPage2 = new System.Windows.Forms.TabPage();
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
            this.groupBox1.Controls.Add(this.btn_into_offical);
            this.groupBox1.Controls.Add(this.btn_update);
            this.groupBox1.Controls.Add(this.btn_analyse);
            this.groupBox1.Location = new System.Drawing.Point(4, 2);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(916, 72);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Operation";
            // 
            // btn_into_offical
            // 
            this.btn_into_offical.Location = new System.Drawing.Point(238, 30);
            this.btn_into_offical.Name = "btn_into_offical";
            this.btn_into_offical.Size = new System.Drawing.Size(98, 25);
            this.btn_into_offical.TabIndex = 2;
            this.btn_into_offical.Text = "Into Offical";
            this.btn_into_offical.UseVisualStyleBackColor = true;
            this.btn_into_offical.Click += new System.EventHandler(this.btn_into_offical_Click);
            // 
            // btn_update
            // 
            this.btn_update.Location = new System.Drawing.Point(128, 30);
            this.btn_update.Name = "btn_update";
            this.btn_update.Size = new System.Drawing.Size(98, 25);
            this.btn_update.TabIndex = 1;
            this.btn_update.Text = "Update Grid";
            this.btn_update.UseVisualStyleBackColor = true;
            this.btn_update.Click += new System.EventHandler(this.btn_update_Click);
            // 
            // btn_analyse
            // 
            this.btn_analyse.Location = new System.Drawing.Point(18, 29);
            this.btn_analyse.Name = "btn_analyse";
            this.btn_analyse.Size = new System.Drawing.Size(98, 25);
            this.btn_analyse.TabIndex = 0;
            this.btn_analyse.Text = "Analyse";
            this.btn_analyse.UseVisualStyleBackColor = true;
            this.btn_analyse.Click += new System.EventHandler(this.btn_analyse_Click);
            // 
            // tabControl1
            // 
            this.tabControl1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Location = new System.Drawing.Point(4, 80);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(924, 465);
            this.tabControl1.TabIndex = 1;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.txt_result);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(916, 439);
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
            this.txt_result.Size = new System.Drawing.Size(910, 433);
            this.txt_result.TabIndex = 0;
            this.txt_result.WordWrap = false;
            this.txt_result.TextChanged += new System.EventHandler(this.txt_result_TextChanged);
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.dgv_result);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Size = new System.Drawing.Size(916, 439);
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
            this.dgv_result.Size = new System.Drawing.Size(916, 439);
            this.dgv_result.TabIndex = 0;
            // 
            // frm_match_100_log_analyse
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(926, 545);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.groupBox1);
            this.Name = "frm_match_100_log_analyse";
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
        private System.Windows.Forms.Button btn_analyse;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.DataGridView dgv_result;
        private System.Windows.Forms.Button btn_update;
        private System.Windows.Forms.Button btn_into_offical;
    }
}