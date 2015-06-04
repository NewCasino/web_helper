using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using MongoDB.Bson;

namespace match_helper
{
    public partial class frm_match_tool_range : Form
    {
        public frm_match_tool_range()
        {
            InitializeComponent();
        }
        StringBuilder sb = new StringBuilder();
        private void btn_compute_Click(object sender, EventArgs e)
        {


            //MessageBox.Show(browser.Version.ToString());
            //return;

            this.txt_result.Text = "";
            StringBuilder sb = new StringBuilder();

            string str_win = txt_win.Text;
            string str_draw = txt_draw.Text;
            string str_lose = txt_lose.Text;

            double win = 0;
            double draw = 0;
            double lose = 0;
            if (!string.IsNullOrEmpty(str_win) && !string.IsNullOrEmpty(str_draw) && !string.IsNullOrEmpty(str_lose))
            {
                try
                {
                    win = Convert.ToDouble(str_win);
                    draw = Convert.ToDouble(str_draw);
                    lose = Convert.ToDouble(str_lose);


                }
                catch (Exception error)
                {
                    MessageBox.Show("wrong input odd!");
                    return;
                }

                this.txt_result.Text = AnalyseBase.get_all_range(win, draw, lose);
                Application.DoEvents();
            }
            if(!string.IsNullOrEmpty(str_win) && !string.IsNullOrEmpty(str_draw) && string.IsNullOrEmpty(str_lose))
            {
                try
                {
                    win = Convert.ToDouble(str_win);
                    draw = Convert.ToDouble(str_draw); 


                }
                catch (Exception error)
                {
                    MessageBox.Show("wrong input odd!");
                    return;
                }

                this.txt_result.Text = AnalyseBase.get_all_range(win, draw);
                Application.DoEvents(); 
            }
        }

        private void txt_result_TextChanged(object sender, EventArgs e)
        { 
            this.txt_result.SelectionStart = this.txt_result.TextLength;
            this.txt_result.ScrollToCaret(); 
        }

      
    }
}
