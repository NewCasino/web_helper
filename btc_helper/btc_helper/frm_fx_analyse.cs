using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace btc_helper
{
    public partial class frm_fx_analyse : Form
    {
        public frm_fx_analyse()
        {
            InitializeComponent();
        }
        private void txt_result_TextChanged(object sender, EventArgs e)
        {
            this.txt_result.SelectionStart = this.txt_result.TextLength;
            this.txt_result.ScrollToCaret();
        } 
        private void btn_test_Click(object sender, EventArgs e)
        {
            this.txt_result.Text = OandaData.show_candles(OandaApi.candles());
        } 
    }
}
