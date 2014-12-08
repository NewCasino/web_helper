namespace web_helper
{
    partial class frm_match_tool_range
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
            this.txt_win = new System.Windows.Forms.TextBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btn_compute = new System.Windows.Forms.Button();
            this.txt_lose = new System.Windows.Forms.TextBox();
            this.txt_draw = new System.Windows.Forms.TextBox();
            this.txt_result = new System.Windows.Forms.TextBox();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // txt_win
            // 
            this.txt_win.Location = new System.Drawing.Point(16, 19);
            this.txt_win.Name = "txt_win";
            this.txt_win.Size = new System.Drawing.Size(51, 20);
            this.txt_win.TabIndex = 0;
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.btn_compute);
            this.groupBox1.Controls.Add(this.txt_lose);
            this.groupBox1.Controls.Add(this.txt_draw);
            this.groupBox1.Controls.Add(this.txt_win);
            this.groupBox1.Location = new System.Drawing.Point(7, 3);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(1082, 54);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Operation";
            // 
            // btn_compute
            // 
            this.btn_compute.Location = new System.Drawing.Point(225, 17);
            this.btn_compute.Name = "btn_compute";
            this.btn_compute.Size = new System.Drawing.Size(75, 23);
            this.btn_compute.TabIndex = 3;
            this.btn_compute.Text = "Compute";
            this.btn_compute.UseVisualStyleBackColor = true;
            this.btn_compute.Click += new System.EventHandler(this.btn_compute_Click);
            // 
            // txt_lose
            // 
            this.txt_lose.Location = new System.Drawing.Point(152, 19);
            this.txt_lose.Name = "txt_lose";
            this.txt_lose.Size = new System.Drawing.Size(51, 20);
            this.txt_lose.TabIndex = 2;
            // 
            // txt_draw
            // 
            this.txt_draw.Location = new System.Drawing.Point(84, 19);
            this.txt_draw.Name = "txt_draw";
            this.txt_draw.Size = new System.Drawing.Size(51, 20);
            this.txt_draw.TabIndex = 1;
            // 
            // txt_result
            // 
            this.txt_result.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.txt_result.Font = new System.Drawing.Font("Lucida Console", 8.25F);
            this.txt_result.Location = new System.Drawing.Point(7, 63);
            this.txt_result.Multiline = true;
            this.txt_result.Name = "txt_result";
            this.txt_result.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.txt_result.Size = new System.Drawing.Size(1082, 523);
            this.txt_result.TabIndex = 2;
            this.txt_result.WordWrap = false;
            this.txt_result.TextChanged += new System.EventHandler(this.txt_result_TextChanged);
            // 
            // frm_match_tool_range
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1101, 598);
            this.Controls.Add(this.txt_result);
            this.Controls.Add(this.groupBox1);
            this.Name = "frm_match_tool_range";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txt_win;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button btn_compute;
        private System.Windows.Forms.TextBox txt_lose;
        private System.Windows.Forms.TextBox txt_draw;
        private System.Windows.Forms.TextBox txt_result;
    }
}