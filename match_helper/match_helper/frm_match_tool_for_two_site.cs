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
    public partial class frm_match_tool_for_two_site : Form
    {
        public frm_match_tool_for_two_site()
        {
            InitializeComponent();
        }
        StringBuilder sb = new StringBuilder();
        private void btn_compute_Click(object sender, EventArgs e)
        {
            string win1 = txt_win_1.Text;
            string draw1 = txt_draw_1.Text;
            string lose1 = txt_lose_1.Text;
            if (cb_usa_1.Checked)
            {
                win1 = Match100Helper.convert_ameriaca_odd(win1);
                draw1 = Match100Helper.convert_ameriaca_odd(draw1);
                lose1 = Match100Helper.convert_ameriaca_odd(lose1);
            }
            if (cb_eng_1.Checked)
            {
                win1 = Match100Helper.convert_english_odd(win1);
                draw1 = Match100Helper.convert_english_odd(draw1);
                lose1 = Match100Helper.convert_english_odd(lose1);
            }

            string win2 = txt_win_2.Text;
            string draw2 = txt_draw_2.Text;
            string lose2 = txt_lose_2.Text;
            if (cb_usa_2.Checked)
            {
                win2 = Match100Helper.convert_ameriaca_odd(win2);
                draw2 = Match100Helper.convert_ameriaca_odd(draw2);
                lose2 = Match100Helper.convert_ameriaca_odd(lose2);
            }
            if (cb_eng_2.Checked)
            {
                win2 = Match100Helper.convert_english_odd(win2);
                draw2 = Match100Helper.convert_english_odd(draw2);
                lose2 = Match100Helper.convert_english_odd(lose2);
            }

            string win = win1;
            string draw = draw1;
            string lose = lose1;
            if (Convert.ToDecimal(win2) > Convert.ToDecimal(win)) win = win2;
            if (Convert.ToDecimal(draw2) > Convert.ToDecimal(win)) draw = draw2;
            if (Convert.ToDecimal(lose2) > Convert.ToDecimal(lose)) lose = lose2;


            if (string.IsNullOrEmpty(win) || string.IsNullOrEmpty(draw) || string.IsNullOrEmpty(lose)) return;

            BsonDocument doc_odd1 = Match100Helper.get_odd_doc_from_europe(win1, draw1, lose1);
            BsonDocument doc_odd2 = Match100Helper.get_odd_doc_from_europe(win2, draw2, lose2);
            BsonDocument doc_odd = Match100Helper.get_odd_doc_from_europe(win, draw, lose);

            if (!string.IsNullOrEmpty(sb.ToString())) { sb.AppendLine("================================================="); }


            sb.AppendLine("-----------ONE");
            if (cb_usa_1.Checked)
            {
                sb.AppendLine("USA".PR(15) + txt_win_1.Text.PR(10) + txt_draw_1.Text.PR(10) + txt_lose_1.Text.PR(10)); 
            }
            if (cb_eng_1.Checked)
            {
                sb.AppendLine("ENG".PR(15) + txt_win_1.Text.PR(10) + txt_draw_1.Text.PR(10) + txt_lose_1.Text.PR(10));
            }
            sb.AppendLine("ODD".PR(15) + doc_odd1["win"].PR(10) + doc_odd1["draw"].PR(10) + doc_odd1["lose"].PR(10));
            sb.AppendLine("PERSENT".PR(15) + doc_odd1["persent_win"].PR(10) + doc_odd1["persent_draw"].PR(10) + doc_odd1["persent_lose"].PR(10));
            sb.AppendLine("RETURN PESENT:".PR(15) + doc_odd1["persent_return"].PR(10));


            sb.AppendLine("-----------TWO");
            if (cb_usa_2.Checked)
            {
                sb.AppendLine("USA".PR(15) + txt_win_2.Text.PR(10) + txt_draw_2.Text.PR(10) + txt_lose_2.Text.PR(10));
                sb.AppendLine("".PR(15) + win2.PR(10) + draw2.PR(10) + lose2.PR(10));
            }
            if (cb_eng_2.Checked)
            {
                sb.AppendLine("ENG".PR(15) + txt_win_2.Text.PR(10) + txt_draw_2.Text.PR(10) + txt_lose_2.Text.PR(10));
                sb.AppendLine("".PR(15) + win2.PR(10) + draw2.PR(10) + lose2.PR(10));
            } 
            sb.AppendLine("ODD".PR(15) + doc_odd2["win"].PR(10) + doc_odd2["draw"].PR(10) + doc_odd2["lose"].PR(10));
            sb.AppendLine("PERSENT".PR(15) + doc_odd2["persent_win"].PR(10) + doc_odd2["persent_draw"].PR(10) + doc_odd2["persent_lose"].PR(10));
            sb.AppendLine("RETURN PESENT:".PR(15) + doc_odd2["persent_return"].PR(10));


            sb.AppendLine("-----------ALL");
            sb.AppendLine("ODD".PR(15) + doc_odd["win"].PR(10) + doc_odd["draw"].PR(10) + doc_odd["lose"].PR(10));
            sb.AppendLine("PERSENT".PR(15) + doc_odd["persent_win"].PR(10) + doc_odd["persent_draw"].PR(10) + doc_odd["persent_lose"].PR(10));
            sb.AppendLine("RETURN PESENT:".PR(15) + doc_odd["persent_return"].PR(10));

            this.txt_result.Text = sb.ToString();
        }

        private void txt_result_TextChanged(object sender, EventArgs e)
        { 
            this.txt_result.SelectionStart = this.txt_result.TextLength;
            this.txt_result.ScrollToCaret(); 
        }
 
    }
}
