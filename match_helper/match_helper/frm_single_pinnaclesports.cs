﻿﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Net;
using System.IO;

namespace match_helper
{
    public partial class frm_match_100_pinnaclesports : Form
    {
        public frm_match_100_pinnaclesports()
        {
            InitializeComponent();
        }

        private void txt_result_TextChanged(object sender, EventArgs e)
        {
            this.txt_result.SelectionStart = this.txt_result.TextLength;
            this.txt_result.ScrollToCaret();
        }

        private void btn_get_html_Click(object sender, EventArgs e)
        {
            this.txt_result.Text = PinApi.leagues();
            //this.txt_result.Text = PinApi.feeds_by_sport_id("29");
        }

        private void btn_get_data_Click(object sender, EventArgs e)
        {
            this.txt_result.Text = PinData.insert_odds(this.txt_result.Text);
            MessageBox.Show("Insert Ok!!!");
        }
    }
}