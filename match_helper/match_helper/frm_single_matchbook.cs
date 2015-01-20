using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Net;
using System.IO;
using MongoDB.Bson;

namespace web_helper
{
    public partial class frm_single_matchbook : Form
    {
        public frm_single_matchbook()
        {
            InitializeComponent();
        }

        private void txt_result_TextChanged(object sender, EventArgs e)
        {
            this.txt_result.SelectionStart = this.txt_result.TextLength;
            this.txt_result.ScrollToCaret();
        }

        private void btn_get_Click(object sender, EventArgs e)
        {
            this.txt_result.Text = show_all_data(this.txt_result.Text);
        } 
        private void btn_get_all_Click(object sender, EventArgs e)
        {
            
        }

        private void btn_beaurify_Click(object sender, EventArgs e)
        {
            this.txt_result.Text = JsonBeautify.beautify(this.txt_result.Text);
        }

        public string get_events_str()
        {
            string json = HtmlHelper.get_html("");
            return json; 
        }
        public string show_all_data(string input)
        {
            StringBuilder sb = new StringBuilder();
            BsonDocument doc = MongoHelper.get_doc_from_str(input);
            BsonArray es = doc["events"].AsBsonArray;
            foreach (BsonDocument e in es)
            {
                string event_id=e["id"].ToString();
                string start_time = e["start"].ToString();
                string[] teams = e["name"].ToString().E_SPLIT(" vs ");
                string home = teams[0];
                string away = teams[1];

                BsonArray tags = e["meta-tags"].AsBsonArray;
                string country = "";
                string competition = "";
                for (int i = 0; i < tags.Count; i++)
                {
                    if (tags[i]["type"].ToString() == "COUNTRY") country = tags[i]["name"].ToString();
                    if (tags[i]["type"].ToString() == "COMPETITION") competition = tags[i]["name"].ToString();
                }
                string league = country + " " + competition;
                sb.Append(league.PR(30)+start_time.PR(30) +home.PR(30)+away.PR(30)+M.N);
                MbookSQL.insert_events(event_id, "Soccer", country, competition, start_time, home, away);
                BsonArray markets = e["markets"].AsBsonArray;
                foreach (BsonDocument market in markets)
                {
                    string markert_name = market["name"].ToString();  
                    BsonArray runners = market["runners"].AsBsonArray;

                    
                    foreach (BsonDocument runner in runners)
                    {
                        string runner_name = runner["name"].ToString();
                        BsonArray prices = runner["prices"].AsBsonArray;

                        sb.Append(markert_name.PR(20) + runner_name.PR(20));
                        foreach (BsonDocument price in prices)
                        {
                            string side = price["side"].ToString();
                            string odd = price["odds"].ToString();
                            string amount = price["available-amount"].ToString(); 
                            sb.Append( side.ToUpper().PR(10) + odd.PR(10) + amount.PR(20));
                            MbookSQL.insert_market(event_id, markert_name, runner_name, "1", side, odd, amount);
                        }
                        sb.Append(M.N);
                    }
                }
            }
            

            return sb.ToString();
        }
    }
}
