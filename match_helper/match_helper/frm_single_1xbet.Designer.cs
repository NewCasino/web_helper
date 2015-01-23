﻿namespace web_helper
 {
     partial class frm_single_1xbet
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
             this.btn_beaurify = new System.Windows.Forms.Button();
             this.btn_get_data = new System.Windows.Forms.Button();
             this.btn_get_html = new System.Windows.Forms.Button();
             this.tabControl1 = new System.Windows.Forms.TabControl();
             this.tabPage1 = new System.Windows.Forms.TabPage();
             this.txt_result = new System.Windows.Forms.TextBox();
             this.groupBox1.SuspendLayout();
             this.tabControl1.SuspendLayout();
             this.tabPage1.SuspendLayout();
             this.SuspendLayout();
             // 
             // groupBox1
             // 
             this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                         | System.Windows.Forms.AnchorStyles.Right)));
             this.groupBox1.Controls.Add(this.btn_beaurify);
             this.groupBox1.Controls.Add(this.btn_get_data);
             this.groupBox1.Controls.Add(this.btn_get_html);
             this.groupBox1.Location = new System.Drawing.Point(4, 2);
             this.groupBox1.Name = "groupBox1";
             this.groupBox1.Size = new System.Drawing.Size(820, 57);
             this.groupBox1.TabIndex = 0;
             this.groupBox1.TabStop = false;
             this.groupBox1.Text = "Operation";
             // 
             // btn_beaurify
             // 
             this.btn_beaurify.Location = new System.Drawing.Point(215, 22);
             this.btn_beaurify.Name = "btn_beaurify";
             this.btn_beaurify.Size = new System.Drawing.Size(75, 25);
             this.btn_beaurify.TabIndex = 2;
             this.btn_beaurify.Text = "Beautify";
             this.btn_beaurify.UseVisualStyleBackColor = true;
             this.btn_beaurify.Click += new System.EventHandler(this.btn_beaurify_Click);
             // 
             // btn_get_data
             // 
             this.btn_get_data.Location = new System.Drawing.Point(122, 22);
             this.btn_get_data.Name = "btn_get_data";
             this.btn_get_data.Size = new System.Drawing.Size(75, 25);
             this.btn_get_data.TabIndex = 1;
             this.btn_get_data.Text = "Get DATA";
             this.btn_get_data.UseVisualStyleBackColor = true;
             this.btn_get_data.Click += new System.EventHandler(this.btn_get_data_Click);
             // 
             // btn_get_html
             // 
             this.btn_get_html.Location = new System.Drawing.Point(32, 22);
             this.btn_get_html.Name = "btn_get_html";
             this.btn_get_html.Size = new System.Drawing.Size(75, 25);
             this.btn_get_html.TabIndex = 0;
             this.btn_get_html.Text = "Get HTML";
             this.btn_get_html.UseVisualStyleBackColor = true;
             this.btn_get_html.Click += new System.EventHandler(this.btn_get_html_Click);
             // 
             // tabControl1
             // 
             this.tabControl1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                         | System.Windows.Forms.AnchorStyles.Left)
                         | System.Windows.Forms.AnchorStyles.Right)));
             this.tabControl1.Controls.Add(this.tabPage1);
             this.tabControl1.Location = new System.Drawing.Point(4, 66);
             this.tabControl1.Name = "tabControl1";
             this.tabControl1.SelectedIndex = 0;
             this.tabControl1.Size = new System.Drawing.Size(820, 406);
             this.tabControl1.TabIndex = 1;
             // 
             // tabPage1
             // 
             this.tabPage1.Controls.Add(this.txt_result);
             this.tabPage1.Location = new System.Drawing.Point(4, 22);
             this.tabPage1.Name = "tabPage1";
             this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
             this.tabPage1.Size = new System.Drawing.Size(812, 380);
             this.tabPage1.TabIndex = 0;
             this.tabPage1.Text = "TEXT";
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
             this.txt_result.Size = new System.Drawing.Size(806, 374);
             this.txt_result.TabIndex = 1;
             this.txt_result.WordWrap = false;
             this.txt_result.TextChanged += new System.EventHandler(this.txt_result_TextChanged);
             // 
             // frm_single_matchbook
             // 
             this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
             this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
             this.ClientSize = new System.Drawing.Size(836, 476);
             this.Controls.Add(this.tabControl1);
             this.Controls.Add(this.groupBox1);
             this.Name = "frm_single_matchbook";
             this.groupBox1.ResumeLayout(false);
             this.tabControl1.ResumeLayout(false);
             this.tabPage1.ResumeLayout(false);
             this.tabPage1.PerformLayout();
             this.ResumeLayout(false);

         }

         #endregion

         private System.Windows.Forms.GroupBox groupBox1;
         private System.Windows.Forms.Button btn_get_html;
         private System.Windows.Forms.TabControl tabControl1;
         private System.Windows.Forms.TabPage tabPage1;
         private System.Windows.Forms.TextBox txt_result;
         private System.Windows.Forms.Button btn_get_data;
         private System.Windows.Forms.Button btn_beaurify;
     }
 }