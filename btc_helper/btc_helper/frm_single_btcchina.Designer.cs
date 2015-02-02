 
    partial class frm_single_btcchina
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
            this.btn_start_fix = new System.Windows.Forms.Button();
            this.btn_start_socket = new System.Windows.Forms.Button();
            this.btn_analyse_depth = new System.Windows.Forms.Button();
            this.btn_delete = new System.Windows.Forms.Button();
            this.btn_test = new System.Windows.Forms.Button();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.txt_result = new System.Windows.Forms.TextBox();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.grid_result = new System.Windows.Forms.DataGridView();
            this.groupBox1.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grid_result)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.btn_start_fix);
            this.groupBox1.Controls.Add(this.btn_start_socket);
            this.groupBox1.Controls.Add(this.btn_analyse_depth);
            this.groupBox1.Controls.Add(this.btn_delete);
            this.groupBox1.Controls.Add(this.btn_test);
            this.groupBox1.Location = new System.Drawing.Point(2, 2);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(1020, 56);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            // 
            // btn_start_fix
            // 
            this.btn_start_fix.Location = new System.Drawing.Point(253, 21);
            this.btn_start_fix.Name = "btn_start_fix";
            this.btn_start_fix.Size = new System.Drawing.Size(75, 23);
            this.btn_start_fix.TabIndex = 4;
            this.btn_start_fix.Text = "Start Fix";
            this.btn_start_fix.UseVisualStyleBackColor = true;
            this.btn_start_fix.Click += new System.EventHandler(this.btn_start_fix_Click);
            // 
            // btn_start_socket
            // 
            this.btn_start_socket.Location = new System.Drawing.Point(172, 21);
            this.btn_start_socket.Name = "btn_start_socket";
            this.btn_start_socket.Size = new System.Drawing.Size(75, 23);
            this.btn_start_socket.TabIndex = 3;
            this.btn_start_socket.Text = "Start Socket";
            this.btn_start_socket.UseVisualStyleBackColor = true;
            this.btn_start_socket.Click += new System.EventHandler(this.btn_start_socket_Click);
            // 
            // btn_analyse_depth
            // 
            this.btn_analyse_depth.Location = new System.Drawing.Point(379, 21);
            this.btn_analyse_depth.Name = "btn_analyse_depth";
            this.btn_analyse_depth.Size = new System.Drawing.Size(75, 23);
            this.btn_analyse_depth.TabIndex = 2;
            this.btn_analyse_depth.Text = "An Depth";
            this.btn_analyse_depth.UseVisualStyleBackColor = true;
            this.btn_analyse_depth.Click += new System.EventHandler(this.btn_analyse_depth_Click);
            // 
            // btn_delete
            // 
            this.btn_delete.Location = new System.Drawing.Point(91, 21);
            this.btn_delete.Name = "btn_delete";
            this.btn_delete.Size = new System.Drawing.Size(75, 23);
            this.btn_delete.TabIndex = 1;
            this.btn_delete.Text = "Delete";
            this.btn_delete.UseVisualStyleBackColor = true;
            this.btn_delete.Click += new System.EventHandler(this.btn_delete_Click);
            // 
            // btn_test
            // 
            this.btn_test.Location = new System.Drawing.Point(10, 21);
            this.btn_test.Name = "btn_test";
            this.btn_test.Size = new System.Drawing.Size(75, 23);
            this.btn_test.TabIndex = 0;
            this.btn_test.Text = "Test";
            this.btn_test.UseVisualStyleBackColor = true;
            this.btn_test.Click += new System.EventHandler(this.btn_test_Click);
            // 
            // tabControl1
            // 
            this.tabControl1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Location = new System.Drawing.Point(3, 64);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(1020, 439);
            this.tabControl1.TabIndex = 1;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.txt_result);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(1012, 413);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "TXT";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // txt_result
            // 
            this.txt_result.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txt_result.Font = new System.Drawing.Font("Lucida Console", 8.25F);
            this.txt_result.Location = new System.Drawing.Point(3, 3);
            this.txt_result.MaxLength = 0;
            this.txt_result.Multiline = true;
            this.txt_result.Name = "txt_result";
            this.txt_result.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.txt_result.Size = new System.Drawing.Size(1006, 407);
            this.txt_result.TabIndex = 6;
            this.txt_result.WordWrap = false;
            this.txt_result.TextChanged += new System.EventHandler(this.txt_result_TextChanged);
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.grid_result);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Size = new System.Drawing.Size(1012, 413);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "TABLE";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // grid_result
            // 
            this.grid_result.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.grid_result.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grid_result.Location = new System.Drawing.Point(0, 0);
            this.grid_result.Name = "grid_result";
            this.grid_result.Size = new System.Drawing.Size(1012, 413);
            this.grid_result.TabIndex = 0;
            // 
            // frm_single_btcchina
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1028, 504);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.groupBox1);
            this.Name = "frm_single_btcchina";
            this.groupBox1.ResumeLayout(false);
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            this.tabPage2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.grid_result)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button btn_test;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TextBox txt_result;
        private System.Windows.Forms.Button btn_delete;
        private System.Windows.Forms.Button btn_analyse_depth;
        private System.Windows.Forms.Button btn_start_socket;
        private System.Windows.Forms.Button btn_start_fix;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.DataGridView grid_result;
    }