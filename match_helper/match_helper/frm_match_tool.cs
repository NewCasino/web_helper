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
            if (cb_usa.Checked)
            {
                win = AnalyseTool.convert_ameriaca_odd(win);
                draw = AnalyseTool.convert_ameriaca_odd(draw);
                lose = AnalyseTool.convert_ameriaca_odd(lose);
            }
            if (cb_eng.Checked)
            {
                win = AnalyseTool.convert_english_odd(win);
                draw = AnalyseTool.convert_english_odd(draw);
                lose = AnalyseTool.convert_english_odd(lose);
            }
          
            if (string.IsNullOrEmpty(win) || string.IsNullOrEmpty(draw)) return;

            if (!string.IsNullOrEmpty(sb.ToString())) { sb.AppendLine("----------------------------------------------"); }

            if (cb_usa.Checked || cb_eng.Checked)
            {
                sb.AppendLine("ODD".PR(15) + txt_win.Text.PR(10) + txt_draw.Text.PR(10) + txt_lose.Text.PR(10));
            }
            BsonDocument doc_odd = new BsonDocument();

            if (!string.IsNullOrEmpty(lose))
            {
                doc_odd = AnalyseTool.get_odd_doc_from_europe(win, draw, lose); 
                sb.AppendLine("ODD".PR(15) + doc_odd["win"].PR(10) + doc_odd["draw"].PR(10) + doc_odd["lose"].PR(10));
                sb.AppendLine("PERSENT".PR(15) + doc_odd["persent_win"].PR(10) + doc_odd["persent_draw"].PR(10) + doc_odd["persent_lose"].PR(10));
                sb.AppendLine("RETURN PESENT:".PR(15) + doc_odd["persent_return"].PR(10));
            }
            else
            {
                doc_odd = AnalyseTool.get_odd_doc_from_europe(win, draw);
                sb.AppendLine("ODD".PR(15) + doc_odd["home"].PR(10) + doc_odd["away"].PR(10));
                sb.AppendLine("PERSENT".PR(15)  + doc_odd["persent_home"].PR(10) + doc_odd["persent_away"].PR(10));
                sb.AppendLine("RETURN PESENT:".PR(15) + doc_odd["persent_return"].PR(10));
            } 
            this.txt_result.Text = sb.ToString();
        }

        private void txt_result_TextChanged(object sender, EventArgs e)
        { 
            this.txt_result.SelectionStart = this.txt_result.TextLength;
            this.txt_result.ScrollToCaret(); 
        }
    }
}
