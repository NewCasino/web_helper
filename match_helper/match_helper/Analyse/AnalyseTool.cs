using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using HtmlAgilityPack;
using MongoDB.Bson;
using MongoDB.Driver;
using System.Data.OleDb;
using System.Threading;
using mshtml;
using System.Reflection;
using System.Data;
using System.IO;

 
    class AnalyseTool
    {
        public static BsonDocument get_odd_doc_from_europe(string win, string draw, string lose)
        {
            BsonDocument doc = new BsonDocument();

            double d_win = Convert.ToDouble(win);
            double d_draw = Convert.ToDouble(draw);
            double d_lose = Convert.ToDouble(lose);

            double d_return_persent = (1 / (1 / d_win + 1 / d_draw + 1 / d_lose)) * 100;
            double d_win_persent = d_return_persent / d_win;
            double d_draw_persent = d_return_persent / d_draw;
            double d_lose_persent = d_return_persent / d_lose;


            doc.Add("win", d_win.ToString("###.000"));
            doc.Add("draw", d_draw.ToString("###.000"));
            doc.Add("lose", d_lose.ToString("###.000"));
            doc.Add("persent_return", Math.Round(d_return_persent, 3).ToString());
            doc.Add("persent_win", Math.Round(d_win_persent, 3).ToString());
            doc.Add("persent_draw", Math.Round(d_draw_persent, 3).ToString());
            doc.Add("persent_lose", Math.Round(d_lose_persent, 3).ToString());

            return doc;
        }
        public static BsonDocument get_odd_doc_from_english(string win, string draw, string lose)
        {
            BsonDocument doc = new BsonDocument();

            double d_win = Convert.ToDouble(convert_english_odd(win));
            double d_draw = Convert.ToDouble(convert_english_odd(draw));
            double d_lose = Convert.ToDouble(convert_english_odd(lose));

            double d_return_persent = (1 / (1 / d_win + 1 / d_draw + 1 / d_lose)) * 100;
            double d_win_persent = d_return_persent / d_win;
            double d_draw_persent = d_return_persent / d_draw;
            double d_lose_persent = d_return_persent / d_lose;


            doc.Add("win", d_win.ToString("###.000"));
            doc.Add("darw", d_draw.ToString("###.000"));
            doc.Add("lose", d_lose.ToString("###.000"));
            doc.Add("return_persent", Math.Round(d_return_persent, 3).ToString());
            doc.Add("win_persent", Math.Round(d_win_persent, 3).ToString());
            doc.Add("draw_persent", Math.Round(d_draw_persent, 3).ToString());
            doc.Add("lose_persent", Math.Round(d_lose_persent, 3).ToString());

            return doc;
        }
        public static BsonDocument get_odd_doc_from_ameriaca(string win, string draw, string lose)
        {
            BsonDocument doc = new BsonDocument();

            double d_win = Convert.ToDouble(convert_ameriaca_odd(win));
            double d_draw = Convert.ToDouble(convert_ameriaca_odd(draw));
            double d_lose = Convert.ToDouble(convert_ameriaca_odd(lose));

            double d_return_persent = (1 / (1 / d_win + 1 / d_draw + 1 / d_lose)) * 100;
            double d_win_persent = d_return_persent / d_win;
            double d_draw_persent = d_return_persent / d_draw;
            double d_lose_persent = d_return_persent / d_lose;


            doc.Add("win", d_win.ToString("###.000"));
            doc.Add("darw", d_draw.ToString("###.000"));
            doc.Add("lose", d_lose.ToString("###.000"));
            doc.Add("return_persent", Math.Round(d_return_persent, 3).ToString());
            doc.Add("win_persent", Math.Round(d_win_persent, 3).ToString());
            doc.Add("draw_persent", Math.Round(d_draw_persent, 3).ToString());
            doc.Add("lose_persent", Math.Round(d_lose_persent, 3).ToString());

            return doc;
        }
        public static BsonDocument get_odd_doc_from_europe(string home, string away)
        {
            BsonDocument doc = new BsonDocument();

            double d_home = Convert.ToDouble(home);
            double d_away = Convert.ToDouble(away);

            double d_return_persent = (1 / (1 / d_home + 1 / d_away)) * 100;
            double d_home_persent = d_return_persent / d_home;
            double d_away_persent = d_return_persent / d_away;


            doc.Add("home", d_home.ToString("###.000"));
            doc.Add("away", d_away.ToString("###.000"));
            doc.Add("persent_return", Math.Round(d_return_persent, 3).ToString());
            doc.Add("persent_home", Math.Round(d_home_persent, 3).ToString());
            doc.Add("persent_away", Math.Round(d_away_persent, 3).ToString());

            return doc;
        }
        public static BsonDocument get_odd_doc_from_europe(string[] list)
        {

            BsonDocument doc = new BsonDocument();

            double[] odds = new double[list.Length];
            for (int i = 0; i < list.Length; i++)
            {
                odds[i] = Convert.ToDouble(list[i]);
            }

            double temp = 0;
            for (int i = 0; i < odds.Length; i++)
            {
                temp = temp + 1 / odds[i];
            }
            double d_return_persent = (1 / temp) * 100;

            BsonArray ba_odds = new BsonArray();
            BsonArray ba_persents = new BsonArray();
            for (int i = 0; i < list.Length; i++)
            {
                ba_odds.Add(odds[i].ToString("###.000"));
                ba_persents.Add(Math.Round(d_return_persent / odds[i], 3).ToString());
            }

            doc.Add("odds", ba_odds);
            doc.Add("persents", ba_persents);
            doc.Add("persent_return", Math.Round(d_return_persent, 3).ToString());

            return doc;
        }
        public static string convert_english_odd(string str)
        {
            string result = "";
            string[] list = str.Split('/');
            if (list.Length == 2)
            {
                double d1 = Convert.ToDouble(list[0].ToString().Trim());
                double d2 = Convert.ToDouble(list[1].ToString().Trim());
                result = Math.Round(d1 / d2 + 1, 3).ToString("###.000");
            }
            return result;
        }
        public static string convert_ameriaca_odd(string str)
        {
            string result = "";
            double d1 = Convert.ToDouble(str.Replace("-", "").Replace("+", "").Trim());
            if (str.Contains("-"))
            {
                result = Math.Round(100 / d1 + 1, 3).ToString("###.000");
            }
            else
            {
                result = Math.Round(d1 / 100 + 1, 3).ToString("###.000");
            }
            return result;
        }
    }
 
