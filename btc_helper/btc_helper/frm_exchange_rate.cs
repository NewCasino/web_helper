using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using MongoDB.Bson;
using HtmlAgilityPack;
using System.Collections;

namespace btc_helper
{
    public partial class frm_exchange_rate : Form
    {

        StringBuilder sb = new StringBuilder();
        public frm_exchange_rate()
        {
            InitializeComponent();
        }
        private void txt_result_TextChanged(object sender, EventArgs e)
        {
            this.txt_result.SelectionStart = this.txt_result.TextLength;
            this.txt_result.ScrollToCaret();
        } 
        private void timer_Tick(object sender, EventArgs e)
        {
            get_excange_rate();
            this.lb_time.Text = DateTime.Now.ToString("HH:mm:ss");
        }
        private void btn_start_Click(object sender, EventArgs e)
        {
            timer.Start();
        }
        private void btn_stop_Click(object sender, EventArgs e)
        {
            timer.Stop();
        }
        private void btn_get_Click(object sender, EventArgs e)
        {

            get_excange_rate();

        }
        public void  get_excange_rate()
        { 
           
            string html = HtmlHelper.get_html("http://fx.cmbchina.com/hq/"); 
            HtmlAgilityPack.HtmlDocument doc = new HtmlAgilityPack.HtmlDocument();
            doc.LoadHtml(html);

            HtmlNodeCollection nodes_all = doc.DocumentNode.SelectNodes(@"//*");
            List<HtmlNode> nodes = new List<HtmlNode>();
 
            foreach (HtmlNode node in nodes_all)
            {
                if (node.Id=="realRateInfo")
                { 
                    HtmlNodeCollection nodes_tr = node.SELECT_NODES("/table[1]/tr");
                    foreach (HtmlNode node_tr in nodes_tr)
                    {
                        string c_to = "";
                        string c_unit = "";
                        string c_from = "";
                        string c_middle = "";
                        string c_exchange_sell = "";
                        string c_exchange_buy = "";
                        string c_paper_sell = "";
                        string c_paper_buy = "";
                        string c_time = "";

                        c_from = node_tr.SELECT_NODE("/td[1]").InnerText.E_TRIM();
                        c_unit = node_tr.SELECT_NODE("/td[2]").InnerText.E_TRIM();
                        c_to = node_tr.SELECT_NODE("/td[3]").InnerText.E_TRIM();
                        c_middle = node_tr.SELECT_NODE("/td[4]").InnerText.E_TRIM();
                        c_exchange_sell = node_tr.SELECT_NODE("/td[5]").InnerText.E_TRIM();
                        c_exchange_buy = node_tr.SELECT_NODE("/td[6]").InnerText.E_TRIM();
                        c_paper_sell = node_tr.SELECT_NODE("/td[7]").InnerText.E_TRIM();
                        c_paper_buy = node_tr.SELECT_NODE("/td[8]").InnerText.E_TRIM();
                        c_time = node_tr.SELECT_NODE("/td[9]").InnerText.E_TRIM();


                        string c_from_simple = CurrencyHelper.get_simple_name(c_from);
                        if (c_from_simple !="no")
                        {
                            double rate = Convert.ToDouble(c_middle);
                            rate = rate / 100;
                            CurrencyHelper.insert_currency("cmbchina", c_from_simple, "cny", rate.ToString());
                        }

                        sb.Append(c_time.PR(10) + c_to.PR(10) + c_from.PR(10) + c_unit.PR(10) + c_middle.PR(10) +
                                  c_exchange_sell.PR(10) + c_exchange_buy.PR(10) + c_paper_sell.PR(10) + c_paper_buy.PR(10) + M.N); 
                    }
                }
            }
            sb.Append("---------------------------------------------------------------------------------------------------------------" + M.N);
            this.txt_result.Text = sb.ToString();
        }

 
    }
}
