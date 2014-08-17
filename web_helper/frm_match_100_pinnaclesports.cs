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

namespace web_helper
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

        private void btn_get_Click(object sender, EventArgs e)
        {
            var request = (HttpWebRequest)WebRequest.Create("https://api.pinnaclesports.com/v1/leagues?sportid=3");
            string credentials = String.Format("{0}:{1}", "LY713941", "pp568986");
            byte[] bytes = Encoding.UTF8.GetBytes(credentials);
            string base64 = Convert.ToBase64String(bytes);
            string authorization = String.Concat("Basic ", base64);
            request.Headers.Add("Authorization", authorization);
            request.Method = "GET";
            request.Accept = "application/json";
            request.ContentType = "application/json; charset=utf-8";
            string postJson =
            "{\"uniqueRequestId\":\"a1eccb11-7f9b-4bff-94c9-66745050f5dc\"," +
            "\"acceptBetterLine\":\"TRUE\"," +
            "\"stake\":150," +
            "\"winRiskStake\":\"WIN\"," +
            "\"lineId\":104520034," +
            "\"sportId\":29," +
            "\"eventId\":311458946," +
            "\"periodNumber\":0," +
            "\"betType\":\"SPREAD\"," +
            "\"team\":\"TEAM1\"," +
            "\"oddsFormat\":\"AMERICAN\"" +
            "}";

            byte[] byteArray = Encoding.UTF8.GetBytes(postJson);
            Stream dataStream = request.GetRequestStream();
            dataStream.Write(byteArray, 0, byteArray.Length);
            dataStream.Close();

            HttpWebResponse response;
            try
            {
                response = (HttpWebResponse)request.GetResponse();
            }
            catch (WebException ex)
            {
                response = (HttpWebResponse)ex.Response;
            }

            var stream = response.GetResponseStream();
            string responseBody;
            using (var reader = new StreamReader(stream))
            {
                responseBody = reader.ReadToEnd();
            }
            this.txt_result.Text = responseBody;
        }
    }
}
