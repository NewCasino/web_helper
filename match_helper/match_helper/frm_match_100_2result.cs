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
            ArrayList list = new ArrayList();
            list.Add("PinnacleSports");
            BsonDocument doc = Analyse2Result.get_best(1, 50, list);
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("-------------------------------------------------------------------------------------------------------------");
            sb.AppendLine(Analyse2Result.get_info(doc));
            sb.AppendLine("-------------------------------------------------------------------------------------------------------------");
            this.txt_result.Text = sb.ToString();
        }
    }
}
