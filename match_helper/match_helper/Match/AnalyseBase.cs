using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MongoDB.Bson;


class AnalyseBase
{
    static bool is_open_mongo = false;
    public static string get_all_range(double win, double draw, double lose)
    {
        StringBuilder sb = new StringBuilder();
        double count_win = 0;
        double count_draw = 0;
        double count_lose = 0;
        double count_total = 0;
        double count_return = 0;
        double persent_win = 0;
        double persent_draw = 0;
        double persent_lose = 0;
        double persent_min = 0;
        BsonArray doc_array = new BsonArray();



        sb.Append("R-COUNT".PR(10) + "R-PERSENT".PR(10) + "R-IDEAL".PR(10) + "R-ACTURL".PR(10) +
                    "WIN".PR(10) + "DRAW".PR(10) + "LOSE".PR(10) +
                    "WIN-P".PR(10) + "DRAW-P".PR(10) + "LOSE-P".PR(10) + Environment.NewLine);
        sb.Append(M.L(120));
        for (int input = 10; input <= 10000; input++)
        {
            BsonDocument doc_item = new BsonDocument();
            count_win = Math.Round(input / win);
            count_draw = Math.Round(input / draw);
            count_lose = Math.Round(input / lose);
            count_total = count_win + count_draw + count_lose;
            persent_win = (count_win * win - count_total) / count_total * 100;
            persent_draw = (count_draw * draw - count_total) / count_total * 100;
            persent_lose = (count_lose * lose - count_total) / count_total * 100;
            count_return = double.MaxValue;
            if ((count_win * win - count_total) < count_return) count_return = count_win * win - count_total;
            if ((count_draw * draw - count_total) < count_return) count_return = count_draw * draw - count_total;
            if ((count_lose * lose - count_total) < count_return) count_return = count_lose * lose - count_total;
            persent_min = double.MaxValue;
            if (persent_win < persent_min) persent_min = persent_win;
            if (persent_draw < persent_min) persent_min = persent_draw;
            if (persent_lose < persent_min) persent_min = persent_lose;

            string return_count_return = Math.Round(count_return, 2).ToString("f2");
            string return_persent_min = Math.Round(persent_min, 2).ToString("f2");
            string return_count_ideal = Math.Round((double)input).ToString();
            string return_count_actual = Math.Round(count_total).ToString();
            string return_count_win = Math.Round(count_win).ToString();
            string return_count_draw = Math.Round(count_draw).ToString();
            string return_count_lose = Math.Round(count_lose).ToString();
            string return_persent_win = Math.Round(persent_win, 2).ToString("f2");
            string return_persent_draw = Math.Round(persent_draw, 2).ToString("f2");
            string return_persent_lose = Math.Round(persent_lose, 2).ToString("f2");

            sb.Append(return_count_return.PR(10) + (return_persent_min + "%").PR(10) + return_count_ideal.PR(10) + return_count_actual.PR(10) +
                      return_count_win.PR(10) + return_count_draw.PR(10) + return_count_lose.PR(10) +
                      (return_persent_win + "%").PR(10) + (return_persent_draw + "%").PR(10) + (return_persent_lose + "%").PR(10) + Environment.NewLine);

        }

        return sb.ToString();
    }
    public static string get_all_range(double win, double lose)
    {

        StringBuilder sb = new StringBuilder();
        double count_win = 0;
        double count_lose = 0;
        double count_total = 0;
        double count_return = 0;
        double persent_win = 0;
        double persent_lose = 0;
        double persent_min = 0;
        BsonArray doc_array = new BsonArray();



        sb.Append("R-COUNT".PR(10) + "R-PERSENT".PR(10) + "IN-IDEAL".PR(10) + "IN-ACTURL".PR(10) +
                    "WIN".PR(10) + "LOSE".PR(10) +
                    "WIN-P".PR(10) + "LOSE-P".PR(10) + Environment.NewLine);
        sb.Append(M.L(90));


        for (int input = 10; input <= 10000; input++)
        {
            BsonDocument doc_item = new BsonDocument();
            count_win = Math.Round(input / win);
            count_lose = Math.Round(input / lose);
            count_total = count_win + count_lose;
            persent_win = (count_win * win - count_total) / count_total * 100;
            persent_lose = (count_lose * lose - count_total) / count_total * 100;
            count_return = double.MaxValue;
            if ((count_win * win - count_total) < count_return) count_return = count_win * win - count_total;
            if ((count_lose * lose - count_total) < count_return) count_return = count_lose * lose - count_total;
            persent_min = double.MaxValue;
            if (persent_win < persent_min) persent_min = persent_win;
            if (persent_lose < persent_min) persent_min = persent_lose;

            string return_count_return = Math.Round(count_return, 2).ToString("f2");
            string return_persent_min = Math.Round(persent_min, 2).ToString("f2");
            string return_count_ideal = Math.Round((double)input).ToString();
            string return_count_actual = Math.Round(count_total).ToString();
            string return_count_win = Math.Round(count_win).ToString();
            string return_count_lose = Math.Round(count_lose).ToString();
            string return_persent_win = Math.Round(persent_win, 2).ToString("f2");
            string return_persent_lose = Math.Round(persent_lose, 2).ToString("f2");

            sb.Append(return_count_return.PR(10) + (return_persent_min + "%").PR(10) + return_count_ideal.PR(10) + return_count_actual.PR(10) +
                      return_count_win.PR(10) + return_count_lose.PR(10) +
                      (return_persent_win + "%").PR(10) + (return_persent_lose + "%").PR(10) + Environment.NewLine);

        }

        return sb.ToString();
    }
}

