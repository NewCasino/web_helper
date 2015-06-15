using System;
using System.Collections;
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
    public partial class frm_match_100_2result : Form
    {
        public frm_match_100_2result()
        {
            InitializeComponent();
        }

        private void btn_test_Click(object sender, EventArgs e)
        {
            this.txt_result.Text = PinData.insert_odds(this.txt_result.Text); 
        } 
    }
}
