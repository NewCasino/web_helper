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
    public partial class frm_bar : Form
    {
        public frm_bar()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            frm_exchange_rate frm = new frm_exchange_rate();
            frm.Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            frm_btc_analyse frm = new frm_btc_analyse();
            frm.Show();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            frm_btc_load_data frm = new frm_btc_load_data();
            frm.Show();
        }
 
        private void button5_Click(object sender, EventArgs e)
        {
            frm_single_btcchina frm = new frm_single_btcchina();
            frm.Show();
        }
    }
}
