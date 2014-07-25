using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
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

namespace web_helper
{
    public partial class frm_web_browser : Form
    {
        public frm_web_browser()
        {
            InitializeComponent();
        }

        StringBuilder sb = new StringBuilder();
        private void frm_web_browser_Load(object sender, EventArgs e)
        {
            this.browser.ScriptErrorsSuppressed = true;
        }
        private void btn_navigate_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(this.txt_url.Text))
            {
                browser.Navigate("http://wwww.baidu.com");
            }
            else
            {
                browser.Navigate(this.txt_url.Text);
            }
            this.txt_result.Text = sb.ToString();
        }
        private void btn_analyse_Click(object sender, EventArgs e)
        {
            sb.Remove(0, sb.ToString().Length);
            DataTable dt_postion = new DataTable();
            DataColumn col1 = new DataColumn();
            col1.DataType = Type.GetType("System.Int32");
            col1.ColumnName = "left";
            DataColumn col2 = new DataColumn();
            col2.DataType = Type.GetType("System.Int32");
            col2.ColumnName = "top";
            DataColumn col3 = new DataColumn();
            col3.DataType = Type.GetType("System.Int32");
            col3.ColumnName = "width";
            DataColumn col4 = new DataColumn();
            col4.DataType = Type.GetType("System.Int32");
            col4.ColumnName = "height";

            dt_postion.Columns.Add(col1);
            dt_postion.Columns.Add(col2);
            dt_postion.Columns.Add(col3);
            dt_postion.Columns.Add(col4);
            dt_postion.Columns.Add("text");

            if (browser.Document == null) return;
            HtmlElementCollection elements = browser.Document.Body.All;
            sb.AppendLine("Tag".PR(10) + "Class".PR(10) + "ID".PR(10) + "Child".PR(10) + "Left".PR(10) + "Top".PR(10) + "O-Left".PR(10) + "O-Top".PR(10) + "Width".PR(10) + "Height".PR(10) + "Text");
            sb.AppendLine("---------------------------------------------------------------------------------------------------------------------------------------------------------");
            foreach (HtmlElement element in elements)
            {
                string row = "";
                IHTMLElement ielement = (IHTMLElement)element.DomElement;
                IHTMLDOMNode node = (IHTMLDOMNode)ielement;
                IHTMLAttributeCollection attrs = (IHTMLAttributeCollection)node.attributes;

                //if (attrs != null)
                //{
                //    foreach (IHTMLDOMAttribute attr in attrs)
                //    {
                //        row += attr.nodeName + ":" + attr.nodeValue;
                //    } 
                //}


                row += ielement.tagName.PR(10);
                row += ielement.className.PR(10);
                row += ielement.id.PR(10);
                row += ((IHTMLElementCollection)ielement.children).length.PR(10);

                int left = ielement.offsetLeft;
                int top = ielement.offsetTop;
                get_absolute(ref ielement, ref left, ref top);


                row += left.PR(10);
                row += top.PR(10);
                row += ielement.offsetLeft.PR(10);
                row += ielement.offsetTop.PR(10);
                row += ielement.offsetWidth.PR(10);
                row += ielement.offsetHeight.PR(10);
                row += ielement.innerText.PR(100);

                DataRow row_new = dt_postion.NewRow();
                row_new["left"] = left;
                row_new["top"] = top.ToString();
                row_new["width"] = ielement.offsetWidth.ToString();
                row_new["height"] = ielement.offsetHeight.ToString();
                row_new["text"] = ielement.innerText;



                if (((IHTMLElementCollection)ielement.children).length != 0) continue;
                if (string.IsNullOrEmpty(ielement.innerText)) continue;
                if (string.IsNullOrEmpty(row)) continue;
                if (left == 0 && top == 0 && ielement.offsetWidth == 0 && ielement.offsetHeight == 0) continue;

                sb.AppendLine(row);
                dt_postion.Rows.Add(row_new);

            }

            this.txt_result.Text = sb.ToString();
            this.dgv_result.DataSource = analyse_table(dt_postion);
        }
        private void btn_script_Click(object sender, EventArgs e)
        {
            if (browser.Document == null) return;
            sb.Append(browser.Document.Body.OuterHtml);
            HtmlElementCollection list = browser.Document.Body.All;
            for (int i = 0; i < list.Count; i++)
            {
                HtmlElement element = list[i];
                if (element.Id == "txt_add")
                {
                    sb.AppendLine(element.OuterHtml);
                    element.InnerText = "change the value";
                }

            }
            //for (int i = 0; i < doc.Body.All.Count; i++)//取出查看DIV标签
            //{
            //    sb.AppendLine("----------------------------------");
            //    sb.AppendLine(doc.Body.All[i].OuterHtml);
            //} 
            //HtmlElement element = browser.Document.GetElementById("cb_1");
            //element.InvokeMember("click");
            //HtmlElementCollection collection = browser.Document.GetElementsByTagName("input");

            //foreach (HtmlElement element in collection)
            //{
            //    IHTMLElement ielement = (IHTMLElement)element.DomElement;
            //    IHTMLDOMNode node = (IHTMLDOMNode)ielement;

            //    string type = element.GetAttribute("type");
            //    string class_name = element.GetAttribute("class");
            //    string test = element.GetAttribute("test");
            //    sb.AppendLine(element.TagName.PR(10) + element.Id.PR(10));
            //    sb.AppendLine(element.OffsetRectangle.Left.PR(10) + element.OffsetRectangle.Top.PR(10) + element.OffsetRectangle.Width.PR(10) + element.OffsetRectangle.Height.PR(10));
            //    element.InvokeMember("click");
            //}
            this.txt_result.Text = sb.ToString();
        }
        private void txt_result_TextChanged(object sender, EventArgs e)
        {
            this.txt_result.SelectionStart = this.txt_result.TextLength;
            this.txt_result.ScrollToCaret();
        }

        public void get_absolute(ref IHTMLElement element, ref int left, ref int top)
        {
            if (element.parentElement != null)
            {
                left = left + element.parentElement.offsetLeft;
                top = top + element.parentElement.offsetTop;
                IHTMLElement father_element = element.parentElement;
                get_absolute(ref father_element, ref left, ref top);
            }
        }
        public DataTable analyse_table(DataTable dt_position)
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("NO");
            dt.Columns.Add("COUNT");
            dt.Columns.Add("TEXT");

            dt_position.DefaultView.Sort = "left asc";
            dt_position = dt_position.DefaultView.ToTable();
            foreach (DataRow row in dt_position.Rows)
            {
                bool is_has = false;
                foreach (DataColumn col in dt.Columns)
                {
                    if (col.ColumnName.Trim() == row["left"].ToString().Trim())
                    {
                        is_has = true;
                    }
                }
                if (is_has == false)
                {
                    DataColumn column = new DataColumn();
                    column.ColumnName = row["left"].ToString();
                    dt.Columns.Add(column);
                }
            }



            dt_position.DefaultView.Sort = "top asc";
            dt_position = dt_position.DefaultView.ToTable();
            for (int i = 0; i < dt_position.Rows.Count; i++)
            {
                bool is_has = false;
                int row_id = 0;
                for (int j = 0; j < dt.Rows.Count; j++)
                {
                    if (dt.Rows[j]["NO"].ToString() == dt_position.Rows[i]["top"].ToString())
                    {
                        is_has = true;
                        row_id = j;
                    }
                }

                if (is_has == false)
                {
                    DataRow row_new = dt.NewRow();
                    row_new["NO"] = dt_position.Rows[i]["top"].ToString();
                    row_new["COUNT"] = "1";
                    row_new["TEXT"] = dt_position.Rows[i]["text"].ToString().TrimEnd();
                    row_new[dt_position.Rows[i]["left"].ToString()] = dt_position.Rows[i]["text"].ToString().TrimEnd();
                    dt.Rows.Add(row_new);
                }
                else
                {
                    if (!string.IsNullOrEmpty(dt.Rows[row_id][dt_position.Rows[i]["left"].ToString()].ToString().Trim()))
                    {
                        dt.Rows[row_id][dt_position.Rows[i]["left"].ToString()] = dt.Rows[row_id][dt_position.Rows[i]["left"].ToString()].ToString() + "●" + dt_position.Rows[i]["text"].ToString().TrimEnd();
                    }
                    else
                    {
                        dt.Rows[row_id][dt_position.Rows[i]["left"].ToString()] = dt_position.Rows[i]["text"].ToString().TrimEnd();
                    }

                    dt.Rows[row_id]["COUNT"] = (Convert.ToInt32(dt.Rows[row_id]["COUNT"].ToString()) + 1).ToString();
                    dt.Rows[row_id]["TEXT"] = dt.Rows[row_id]["TEXT"].ToString() + "●" + dt_position.Rows[i]["text"].ToString().TrimEnd();
                }
            }


            DataRow row_count = dt.NewRow();
            DataRow row_text = dt.NewRow();
            row_count["NO"] = "COUNT";
            row_text["NO"] = "TEXT";
            for (int i = 0; i < dt.Columns.Count; i++)
            {
                if (i == 1)
                {
                    int total = 0;
                    for (int j = 0; j < dt.Rows.Count; j++)
                    {
                        total = total + Convert.ToInt32(dt.Rows[j][i].ToString());
                    }
                    row_count[1] = total.ToString();

                }
                if (i != 0 && i != 1 && i != 2)
                {
                    int column_total = 0;
                    for (int j = 0; j < dt.Rows.Count; j++)
                    {
                        if (dt.Rows[j][i] != null && !string.IsNullOrEmpty(dt.Rows[j][i].ToString()))
                        {
                            column_total = column_total + 1;
                        }
                    }
                    row_count[i] = column_total.ToString();
                }
                if (i != 0 && i != 1 && i != 2)
                {

                    string text = "";
                    for (int j = 0; j < dt.Rows.Count; j++)
                    {
                        if (dt.Rows[j][i] != null && !string.IsNullOrEmpty(dt.Rows[j][i].ToString()))
                        {
                            if (string.IsNullOrEmpty(text.Trim()))
                            {
                                text = dt.Rows[j][i].ToString();
                            }
                            else
                            {
                                text = text +"●"+ dt.Rows[j][i].ToString();
                            }
                        }
                    }
                    row_text[i] = text.ToString();
                }
            }
            dt.Rows.Add(row_count);
            dt.Rows.Add(row_text);

            for (int i = 0; i < dt.Rows.Count - 2; i++)
            {
                if (dt.Rows[i]["COUNT"].ToString() == "1")
                {
                    dt.Rows[i]["TEXT"] = "";
                }
            }

            for (int i = 3; i < dt.Columns.Count; i++)
            {
                int count = dt.Rows.Count;
                if (dt.Rows[count - 2][i].ToString() == "1")
                {
                    dt.Rows[count - 1][i] = "";
                }
            }

            return dt;

        }

    }
}
