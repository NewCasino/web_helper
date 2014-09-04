using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Collections;
using HtmlAgilityPack;

static class Extensions
{
    //Object Extentsions
    public static string PR(this object o, int len)
    {
        if (o == null) return "  ".PadRight(len, ' ') + "  ";
        if (string.IsNullOrEmpty(o.ToString())) return " ".PadRight(len, ' ') + "  ";

        string input = o.ToString();
        input = input.Replace(Environment.NewLine, " "); //替换换行符
        if (input.Length < len) input = input.PadRight(len, ' ');


        int count = 0;
        for (int i = 0; i < input.Length; i++)
        {
            if (input[i] >= 0x4e00 && input[i] <= 0x9fbb)
            {
                count = count + 2;
            }
            else
            {
                count = count + 1;
            }
            if (count == len) return input.Substring(0, i + 1) + "  ";
        }
        return "PR WRONG".PR(len);
    }

    //String Extentsions 
    public static string E_TRIM(this string str)
    {
        return str.Replace("/r/n", "").Replace(" ", "").Trim();
    }
    public static string[] E_SPLIT(this string str, string mark)
    {
        string[] list = str.ToString().Split(new string[] { mark }, StringSplitOptions.RemoveEmptyEntries);
        return list;
    }
 
    //HtmlNode Extensions
    public static HtmlNode SELECT_NODE(this HtmlNode node_input, string xpath)
    {
        HtmlAgilityPack.HtmlDocument doc = new HtmlAgilityPack.HtmlDocument();
        doc.LoadHtml(node_input.InnerHtml);
        return doc.DocumentNode.SelectSingleNode(xpath);
    }
    public static HtmlNodeCollection SELECT_NODES(this HtmlNode node_input, string xpath)
    {
        HtmlAgilityPack.HtmlDocument doc = new HtmlAgilityPack.HtmlDocument();
        doc.LoadHtml(node_input.InnerHtml);
        return doc.DocumentNode.SelectNodes(xpath);
    }
    public static string CLASS(this HtmlNode node_input)
    {
        if (node_input.Attributes.Contains("class"))
        {
            return node_input.Attributes["class"].Value.ToString();
        }
        else
        {
            return "";
        }
    }
    public static string TEXT(this HtmlNode node_input, int index)
    {
        index = index - 1; //和HTMlAgilityPack对应起来，从1开始
        ArrayList list = new ArrayList();
        foreach (HtmlNode node in node_input.ChildNodes)
        {
            if (node.Name == "#text")
            {
                list.Add(node.OuterHtml);
            }
        }
        if (index < list.Count)
        {
            return list[index].ToString();
        }
        else
        {
            return "";
        }
    }

    //StringBulider Extentsions
    public static string PRINT(this StringBuilder sb)
    {
        string result = "";
        string[] list = sb.ToString().E_SPLIT(Environment.NewLine);
        if (list.Length > 200)
        {
            for (int i = list.Length -200; i < list.Length; i++)
            {
                result = result + list[i] + Environment.NewLine;
            }
        }
        else
        {
            result = sb.ToString();
        }

        sb.Remove(0, sb.Length);
        sb.Append(result);
        return sb.ToString(); 
    }
}