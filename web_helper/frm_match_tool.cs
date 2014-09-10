using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using MongoDB.Bson;

namespace web_helper
{
    public partial class frm_match_tool : Form
    {
        public frm_match_tool()
        {
            InitializeComponent();
        }
        StringBuilder sb = new StringBuilder();
        private void btn_compute_Click(object sender, EventArgs e)
        {
            string win = txt_win.Text;
            string draw = txt_draw.Text;
            string lose = txt_lose.Text;
            if (string.IsNullOrEmpty(win) || string.IsNullOrEmpty(draw) || string.IsNullOrEmpty(lose)) return;
            BsonDocument doc_odd = Match100Helper.get_odd_doc_from_europe(win, draw, lose);

            if (!string.IsNullOrEmpty(sb.ToString())) { sb.AppendLine("----------------------------------------------"); }
           
            sb.AppendLine("ODD".PR(15) + doc_odd["win"].PR(10) + doc_odd["draw"].PR(10) + doc_odd["lose"].PR(10));
            sb.AppendLine("PERSENT".PR(15) + doc_odd["win_persent"].PR(10) + doc_odd["draw_persent"].PR(10) + doc_odd["lose_persent"].PR(10));
            sb.AppendLine("RETURN PESENT:".PR(15) + doc_odd["return_persent"].PR(10));




            this.txt_result.Text = sb.ToString();
        }

        private void txt_result_TextChanged(object sender, EventArgs e)
        { 
            this.txt_result.SelectionStart = this.txt_result.TextLength;
            this.txt_result.ScrollToCaret(); 
        }
    }
}
