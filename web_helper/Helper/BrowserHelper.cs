using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using MongoDB.Bson;
using MongoDB.Driver;
using System.Data.OleDb;
using System.Threading;
using mshtml;
using System.Reflection;
using System.Data;

using System.Runtime.InteropServices;


class BrowserHelper
{

    public static void get_absolute(ref IHTMLElement element, ref int left, ref int top)
    {
        if (element == null) return;
        if (element.parentElement != null)
        {
            left = left + element.parentElement.offsetLeft;
            top = top + element.parentElement.offsetTop;
            IHTMLElement father_element = element.parentElement;
            get_absolute(ref father_element, ref left, ref top);
        }
    }
    public static DataTable get_postion_table2(ref WebBrowser browser)
    {
        //create positon table
        DataTable dt_position = new DataTable();
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
        dt_position.Columns.Add(col1);
        dt_position.Columns.Add(col2);
        dt_position.Columns.Add(col3);
        dt_position.Columns.Add(col4);
        dt_position.Columns.Add("text");

        if (browser.Document == null) return dt_position;
        HtmlDocument doc_child = browser.Document;
        get_position_from_doc2(ref dt_position, ref doc_child, browser.Document.Window.Position.X, browser.Document.Window.Position.Y);

        return dt_position;

    }
    public static DataTable get_analyse_table2(ref WebBrowser browser)
    {
        DataTable dt = new DataTable();
        dt.Columns.Add("NO");
        dt.Columns.Add("COUNT");
        dt.Columns.Add("TEXT");
        if (browser.Document == null) return dt;

        //create dt from dt_postion  
        DataTable dt_position = new DataTable();
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
        dt_position.Columns.Add(col1);
        dt_position.Columns.Add(col2);
        dt_position.Columns.Add(col3);
        dt_position.Columns.Add(col4);
        dt_position.Columns.Add("text");

        if (browser.Document == null) return dt_position;
        HtmlDocument doc_child = browser.Document;
        get_position_from_doc2(ref dt_position, ref doc_child, browser.Document.Window.Position.X, browser.Document.Window.Position.Y);



        //add column to dt
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


        //add row to dt
        dt_position.DefaultView.Sort = "top asc";
        dt_position = dt_position.DefaultView.ToTable();
        for (int i = 0; i < dt_position.Rows.Count; i++)
        {
            string text = dt_position.Rows[i]["text"].ToString().TrimStart().TrimEnd();
            if (string.IsNullOrEmpty(text)) continue;


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
                row_new["TEXT"] = text;
                row_new[dt_position.Rows[i]["left"].ToString()] = text;
                dt.Rows.Add(row_new);
            }
            else
            {
                dt.Rows[row_id][dt_position.Rows[i]["left"].ToString()] = dt.Rows[row_id][dt_position.Rows[i]["left"].ToString()].ToString() + "●" + text;
                dt.Rows[row_id]["COUNT"] = (Convert.ToInt32(dt.Rows[row_id]["COUNT"].ToString()) + 1).ToString();
                dt.Rows[row_id]["TEXT"] = dt.Rows[row_id]["TEXT"].ToString() + "●" + dt_position.Rows[i]["text"].ToString().TrimStart().TrimEnd();
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
                            text = text + "●" + dt.Rows[j][i].ToString();
                        }
                    }
                }
                row_text[i] = text.ToString();
            }
        }
        dt.Rows.Add(row_count);
        dt.Rows.Add(row_text);

        //for (int i = 0; i < dt.Rows.Count - 2; i++)
        //{
        //    if (dt.Rows[i]["COUNT"].ToString() == "1")
        //    {
        //        dt.Rows[i]["TEXT"] = "";
        //    }
        //}

        //for (int i = 3; i < dt.Columns.Count; i++)
        //{
        //    int count = dt.Rows.Count;
        //    if (dt.Rows[count - 2][i].ToString() == "1")
        //    {
        //        dt.Rows[count - 1][i] = "";
        //    }
        //}

        return dt;


    }
    public static void get_position_from_doc2(ref DataTable dt_position, ref System.Windows.Forms.HtmlDocument doc_input, int start_x, int start_y)
    {

        //try
        //{
        for (int i = 0; i < doc_input.Window.Frames.Count; i++)
        {
            HtmlDocument doc_child = doc_input.Window.Frames[i].Document;
            get_position_from_doc2(ref dt_position, ref doc_child, start_x, start_y);
        }
        //}
        //catch (Exception error) { }

        HtmlElementCollection elements = doc_input.Body.All;

        foreach (HtmlElement element in elements)
        {

            IHTMLElement ielement = (IHTMLElement)element.DomElement;
            IHTMLDOMNode node = (IHTMLDOMNode)ielement;
            IHTMLAttributeCollection attrs = (IHTMLAttributeCollection)node.attributes;

            DataRow row_new = dt_position.NewRow();
            int left = ielement.offsetLeft + doc_input.Window.Position.X - start_x;
            int top = ielement.offsetTop + doc_input.Window.Position.Y - start_y;
            if (element.InnerText == null) continue; string text = element.InnerText.Trim();
            get_absolute(ref ielement, ref left, ref top);

            row_new["left"] = left;
            row_new["top"] = top;
            row_new["width"] = ielement.offsetWidth.ToString();
            row_new["height"] = ielement.offsetHeight.ToString();
            row_new["text"] = text;

            if (((IHTMLElementCollection)ielement.children).length != 0) continue;
            if (string.IsNullOrEmpty(text)) continue;
            if (left == 0 && top == 0 && ielement.offsetWidth == 0 && ielement.offsetHeight == 0) continue;

            dt_position.Rows.Add(row_new);

        }




    }

    public static List<BsonDocument> get_all_elments2(ref WebBrowser browser)
    {
        List<BsonDocument> docs = new List<BsonDocument>();
        HtmlDocument doc_child = browser.Document;
        get_all_elements_loop2(ref docs, ref doc_child);
        return docs;
    }
    public static void get_all_elements_loop2(ref List<BsonDocument> docs, ref System.Windows.Forms.HtmlDocument doc_input)
    {
        try
        {
            for (int i = 0; i < doc_input.Window.Frames.Count; i++)
            {
                HtmlDocument doc_child = doc_input.Window.Frames[i].Document;
                get_all_elements_loop2(ref docs, ref doc_child);
            }
        }
        catch (Exception error) { }

        HtmlElementCollection elements = doc_input.Body.All;

        foreach (HtmlElement element in elements)
        {

            IHTMLElement ielement = (IHTMLElement)element.DomElement;
            IHTMLDOMNode inode = (IHTMLDOMNode)ielement;
            IHTMLAttributeCollection attrs = (IHTMLAttributeCollection)inode.attributes;
            string text = "";
            if (element.InnerText != null) text = element.InnerText;


            if (((IHTMLElementCollection)ielement.children).length != 0) continue;
            if (string.IsNullOrEmpty(text)) continue;

            string type = (element.TagName == null) ? "" : element.TagName;
            string id = (element.Id == null) ? "" : element.Id;
            string name = (element.Name == null) ? "" : element.Name;
            string html = (element.OuterHtml == null) ? "" : element.OuterHtml;
            string class_name = (ielement.className == null) ? "" : ielement.className;


            BsonDocument doc = new BsonDocument();
            doc.Add("type", type);
            doc.Add("id", id);
            doc.Add("name", name);
            doc.Add("html", html);
            doc.Add("text", text);
            doc.Add("class", class_name);
            docs.Add(doc);

        }
    }




    public static string get_text_by_id2(ref WebBrowser browser, string id)
    {
        string result = "";
        HtmlDocument doc_child = browser.Document;
        get_text_by_id_loop2(ref doc_child, ref result, id);
        return result;
    }
    public static void get_text_by_id_loop2(ref System.Windows.Forms.HtmlDocument doc_input, ref string result, string id)
    {
        try
        {
            for (int i = 0; i < doc_input.Window.Frames.Count; i++)
            {
                HtmlDocument doc_child = doc_input.Window.Frames[i].Document;
                get_text_by_id_loop2(ref doc_child, ref result, id);
            }
        }
        catch (Exception error) { result = error.Message; }


        HtmlElementCollection elements = doc_input.Body.All;

        foreach (HtmlElement element in elements)
        {

            IHTMLElement ielement = (IHTMLElement)element.DomElement;
            IHTMLDOMNode node = (IHTMLDOMNode)ielement;
            IHTMLAttributeCollection attrs = (IHTMLAttributeCollection)node.attributes;

            if (element.Id == id)
            {
                result = (element == null) ? "" : element.InnerText.ToString();
            }
        }
    }
    public static void invoke_member_by_id2(ref WebBrowser browser, string id, string member)
    {

        HtmlDocument doc_child = browser.Document;
        invoke_member_by_id_loop2(ref doc_child, id, member);
    }
    public static void invoke_member_by_id_loop2(ref System.Windows.Forms.HtmlDocument doc_input, string id, string member)
    {
        try
        {
            for (int i = 0; i < doc_input.Window.Frames.Count; i++)
            {
                HtmlDocument doc_child = doc_input.Window.Frames[i].Document;
                invoke_member_by_id_loop2(ref doc_child, id, member);
            }
        }
        catch (Exception error) { }

        HtmlElementCollection elements = doc_input.Body.All;

        foreach (HtmlElement element in elements)
        {

            IHTMLElement ielement = (IHTMLElement)element.DomElement;
            IHTMLDOMNode node = (IHTMLDOMNode)ielement;
            IHTMLAttributeCollection attrs = (IHTMLAttributeCollection)node.attributes;

            if (element.Id == id)
            {
                element.InvokeMember(member);
            }
        }
    }



    public static DataTable get_postion_table(ref WebBrowser browser)
    {
        //create positon table
        DataTable dt_position = new DataTable();
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
        dt_position.Columns.Add(col1);
        dt_position.Columns.Add(col2);
        dt_position.Columns.Add(col3);
        dt_position.Columns.Add(col4);
        dt_position.Columns.Add("text");

        if (browser.Document == null) return dt_position;
        mshtml.HTMLDocument doc_child = (mshtml.HTMLDocument)browser.Document.DomDocument;
        get_position_from_doc(ref dt_position, ref doc_child, ((IHTMLWindow3)doc_child.parentWindow).screenLeft, ((IHTMLWindow3)doc_child.parentWindow).screenTop);

        return dt_position;

    }
    public static DataTable get_analyse_table(ref WebBrowser browser)
    {
        DataTable dt = new DataTable();
        dt.Columns.Add("NO");
        dt.Columns.Add("COUNT");
        dt.Columns.Add("TEXT");
        if (browser.Document == null) return dt;

        //create dt from dt_postion  
        DataTable dt_position = new DataTable();
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
        dt_position.Columns.Add(col1);
        dt_position.Columns.Add(col2);
        dt_position.Columns.Add(col3);
        dt_position.Columns.Add(col4);
        dt_position.Columns.Add("text");

        if (browser.Document == null) return dt_position;
        mshtml.HTMLDocument doc_child = (mshtml.HTMLDocument)browser.Document.DomDocument;
        get_position_from_doc(ref dt_position, ref doc_child, ((IHTMLWindow3)doc_child.parentWindow).screenLeft, ((IHTMLWindow3)doc_child.parentWindow).screenTop);



        //add column to dt
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


        //add row to dt
        dt_position.DefaultView.Sort = "top asc";
        dt_position = dt_position.DefaultView.ToTable();
        for (int i = 0; i < dt_position.Rows.Count; i++)
        {
            string text = dt_position.Rows[i]["text"].ToString().TrimStart().TrimEnd();
            if (string.IsNullOrEmpty(text)) continue;


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
                row_new["TEXT"] = text;
                row_new[dt_position.Rows[i]["left"].ToString()] = text;
                dt.Rows.Add(row_new);
            }
            else
            {
                dt.Rows[row_id][dt_position.Rows[i]["left"].ToString()] = dt.Rows[row_id][dt_position.Rows[i]["left"].ToString()].ToString() + "●" + text;
                dt.Rows[row_id]["COUNT"] = (Convert.ToInt32(dt.Rows[row_id]["COUNT"].ToString()) + 1).ToString();
                dt.Rows[row_id]["TEXT"] = dt.Rows[row_id]["TEXT"].ToString() + "●" + dt_position.Rows[i]["text"].ToString().TrimStart().TrimEnd();
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
                            text = text + "●" + dt.Rows[j][i].ToString();
                        }
                    }
                }
                row_text[i] = text.ToString();
            }
        }
        dt.Rows.Add(row_count);
        dt.Rows.Add(row_text);

        //for (int i = 0; i < dt.Rows.Count - 2; i++)
        //{
        //    if (dt.Rows[i]["COUNT"].ToString() == "1")
        //    {
        //        dt.Rows[i]["TEXT"] = "";
        //    }
        //}

        //for (int i = 3; i < dt.Columns.Count; i++)
        //{
        //    int count = dt.Rows.Count;
        //    if (dt.Rows[count - 2][i].ToString() == "1")
        //    {
        //        dt.Rows[count - 1][i] = "";
        //    }
        //}

        return dt;


    }
    public static DataTable get_position_deep_table(ref WebBrowser browser)
    { 
        if (browser.Document == null) return new DataTable();

        //create dt from dt_postion  
        DataTable dt_position = new DataTable();
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
        dt_position.Columns.Add(col1);
        dt_position.Columns.Add(col2);
        dt_position.Columns.Add(col3);
        dt_position.Columns.Add(col4);
        dt_position.Columns.Add("text");

        if (browser.Document == null) return dt_position;
        mshtml.HTMLDocument doc_child = (mshtml.HTMLDocument)browser.Document.DomDocument;
        get_position_from_doc(ref dt_position, ref doc_child, ((IHTMLWindow3)doc_child.parentWindow).screenLeft, ((IHTMLWindow3)doc_child.parentWindow).screenTop);

        dt_position.DefaultView.Sort = "left asc";
        dt_position = dt_position.DefaultView.ToTable();

        for (int i = 0; i < dt_position.Rows.Count; i++)
        {
            for (int j = 0; j < dt_position.Rows.Count; j++)
            {
                if (is_overlap((int)dt_position.Rows[i]["left"], (int)dt_position.Rows[i]["width"], (int)dt_position.Rows[j]["left"], (int)dt_position.Rows[j]["width"]) == true)
                {
                    dt_position.Rows[j]["left"] = dt_position.Rows[i]["Left"];
                    dt_position.Rows[j]["width"] = dt_position.Rows[i]["width"];
                }
            }
        }


        dt_position.DefaultView.Sort = "top asc";
        dt_position = dt_position.DefaultView.ToTable();

        for (int i = 0; i < dt_position.Rows.Count; i++)
        {
            for (int j = 0; j < dt_position.Rows.Count; j++)
            {
                if (is_overlap((int)dt_position.Rows[i]["top"], (int)dt_position.Rows[i]["height"], (int)dt_position.Rows[j]["top"], (int)dt_position.Rows[j]["height"]) == true)
                {
                    dt_position.Rows[j]["top"] = dt_position.Rows[i]["top"];
                    dt_position.Rows[j]["height"] = dt_position.Rows[i]["height"];
                }
            }
        }

        return dt_position; 
    }
    public static DataTable get_analyse_deep_table(ref WebBrowser browser)
    {

        if (browser.Document == null) return new DataTable();

        //create dt from dt_postion  
        DataTable dt_position = new DataTable();
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
        dt_position.Columns.Add(col1);
        dt_position.Columns.Add(col2);
        dt_position.Columns.Add(col3);
        dt_position.Columns.Add(col4);
        dt_position.Columns.Add("text");

        if (browser.Document == null) return dt_position;
        mshtml.HTMLDocument doc_child = (mshtml.HTMLDocument)browser.Document.DomDocument;
        get_position_from_doc(ref dt_position, ref doc_child, ((IHTMLWindow3)doc_child.parentWindow).screenLeft, ((IHTMLWindow3)doc_child.parentWindow).screenTop);

        dt_position.DefaultView.Sort = "left asc";
        dt_position = dt_position.DefaultView.ToTable();

        for (int i = 0; i < dt_position.Rows.Count; i++)
        {
            for (int j = i+1; j < dt_position.Rows.Count; j++)
            {
                if (is_overlap((int)dt_position.Rows[i]["left"], (int)dt_position.Rows[i]["width"], (int)dt_position.Rows[j]["left"], (int)dt_position.Rows[j]["width"]) == true)
                {
                    dt_position.Rows[j]["left"] = dt_position.Rows[i]["Left"];
                    dt_position.Rows[j]["width"] = dt_position.Rows[i]["width"];
                }
            }
        }


        dt_position.DefaultView.Sort = "top asc";
        dt_position = dt_position.DefaultView.ToTable();

        for (int i = 0; i < dt_position.Rows.Count; i++)
        {
            for (int j = i+1; j < dt_position.Rows.Count; j++)
            {
                if (is_overlap((int)dt_position.Rows[i]["top"], (int)dt_position.Rows[i]["height"], (int)dt_position.Rows[j]["top"], (int)dt_position.Rows[j]["height"]) == true)
                {
                    dt_position.Rows[j]["top"] = dt_position.Rows[i]["top"];
                    dt_position.Rows[j]["height"] = dt_position.Rows[i]["height"];
                }
            }
        }

       
        //add column to dt 
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


        //add row to dt
        dt_position.DefaultView.Sort = "top asc";
        dt_position = dt_position.DefaultView.ToTable();
        for (int i = 0; i < dt_position.Rows.Count; i++)
        {
            string text = dt_position.Rows[i]["text"].ToString().TrimStart().TrimEnd();
            if (string.IsNullOrEmpty(text)) continue;


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
                row_new["TEXT"] = text;
                row_new[dt_position.Rows[i]["left"].ToString()] = text;
                dt.Rows.Add(row_new);
            }
            else
            {
                dt.Rows[row_id][dt_position.Rows[i]["left"].ToString()] = dt.Rows[row_id][dt_position.Rows[i]["left"].ToString()].ToString() + "●" + text;
                dt.Rows[row_id]["COUNT"] = (Convert.ToInt32(dt.Rows[row_id]["COUNT"].ToString()) + 1).ToString();
                dt.Rows[row_id]["TEXT"] = dt.Rows[row_id]["TEXT"].ToString() + "●" + dt_position.Rows[i]["text"].ToString().TrimStart().TrimEnd();
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
                            text = text + "●" + dt.Rows[j][i].ToString();
                        }
                    }
                }
                row_text[i] = text.ToString();
            }
        }
        dt.Rows.Add(row_count);
        dt.Rows.Add(row_text); 


        return dt;


    }
    public static void get_position_from_doc(ref DataTable dt_position, ref mshtml.HTMLDocument doc_input, int start_x, int start_y)
    {
        object j;
        for (int i = 0; i < doc_input.parentWindow.frames.length; i++)
        {
            j = i;
            mshtml.IHTMLWindow2 frame = doc_input.parentWindow.frames.item(ref j) as IHTMLWindow2;
            IHTMLDocument3 doc_child3 = CorssDomainHelper.GetDocumentFromWindow(frame.window as IHTMLWindow2);
            mshtml.HTMLDocument doc_child = (mshtml.HTMLDocument)doc_child3;
            get_position_from_doc(ref dt_position, ref doc_child, start_x, start_y);
        }


        mshtml.IHTMLElementCollection ielements = (mshtml.IHTMLElementCollection)doc_input.all;

        foreach (mshtml.IHTMLElement ielement in ielements)
        {

            mshtml.IHTMLDOMNode node = (mshtml.IHTMLDOMNode)ielement;
            IHTMLAttributeCollection attrs = (IHTMLAttributeCollection)node.attributes;

            DataRow row_new = dt_position.NewRow();
            int left = ielement.offsetLeft + ((IHTMLWindow3)doc_input.parentWindow).screenLeft - start_x;
            int top = ielement.offsetTop + ((IHTMLWindow3)doc_input.parentWindow).screenTop - start_y;

            if (ielement.innerText == null) continue; string text = ielement.innerText.Trim();
            IHTMLElement ielement_child = ielement;
            get_absolute(ref ielement_child, ref left, ref top);

            row_new["left"] = left;
            row_new["top"] = top;
            row_new["width"] = ielement.offsetWidth.ToString();
            row_new["height"] = ielement.offsetHeight.ToString();
            row_new["text"] = text;


            bool is_contine = true;

            if (((IHTMLElementCollection)ielement.children).length == 1 && ielement.innerHTML != null && ielement.innerHTML.ToLower().Trim().Contains("<br") == true)
            {
                text = ielement.innerHTML;
                is_contine = false;
            }
            if (((IHTMLElementCollection)ielement.children).length == 0 && !string.IsNullOrEmpty(text)) is_contine = false;
            if (left == 0 && top == 0 && ielement.offsetWidth == 0 && ielement.offsetHeight == 0) is_contine = true; 
            if (is_contine) continue; 

            dt_position.Rows.Add(row_new);
        }




    }
    public static bool is_overlap(int input_left1, int input_width1, int input_left2, int input_width2)
    {
      

        double width1=input_width1;
        double width2=input_width2;
        double  start1 = input_left1;
        double end1 = input_left1 + width1;
        double start2 = input_left2;
        double end2 = input_left2 + width2;

        if (input_width1 == 0 || input_width2 == 0) return false;
        if (width1 / width2 > 2 || width1 / width2 < 0.5) return false;
        if (start1 == start2 && end1 == end2) return true;
        if (start2 <= start1 && end2 >= start1 && end2 <= end1 && (end2 - start1) / width1 > 0.5) return true;
        if (start2 < start1 && end2 > end1) return true;
        if (start2 >= start1 && start2 <= end1 && end2 >= start1 && end2 <= end1) return true;
        if (start2>=start1 && start2<=end1 && end2>end1 && (end1-start2)/width1>0.5) return true;

        return false; 
    }

    public static string get_text_by_id(ref WebBrowser browser, string id)
    {
        string result = "";
        mshtml.HTMLDocument doc_child = (mshtml.HTMLDocument)browser.Document.DomDocument;
        get_text_by_id_loop(ref doc_child, ref result, id);
        return result;
    }
    public static void get_text_by_id_loop(ref mshtml.HTMLDocument doc_input, ref string result, string id)
    {
        object j;
        for (int i = 0; i < doc_input.parentWindow.frames.length; i++)
        {
            j = i;
            mshtml.IHTMLWindow2 frame = doc_input.parentWindow.frames.item(ref j) as IHTMLWindow2;
            IHTMLDocument3 doc_child3 = CorssDomainHelper.GetDocumentFromWindow(frame.window as IHTMLWindow2);
            mshtml.HTMLDocument doc_child = (mshtml.HTMLDocument)doc_child3;
            get_text_by_id_loop(ref doc_child, ref result, id);
        }


        mshtml.IHTMLElementCollection ielements = (mshtml.IHTMLElementCollection)doc_input.all;

        foreach (mshtml.IHTMLElement ielement in ielements)
        {
            mshtml.IHTMLDOMNode node = (mshtml.IHTMLDOMNode)ielement;
            IHTMLAttributeCollection attrs = (IHTMLAttributeCollection)node.attributes;

            if (ielement.id == id)
            {
                result = (ielement == null) ? "" : ielement.innerText;
            }

        }
    }

    public static string invoke_click_by_id(ref WebBrowser browser, string id)
    {
        string result = "";
        mshtml.HTMLDocument doc_child = (mshtml.HTMLDocument)browser.Document.DomDocument;
        invoke_click_by_id_loop(ref doc_child, id);
        return result;
    }
    public static void invoke_click_by_id_loop(ref mshtml.HTMLDocument doc_input, string id)
    {
        object j;
        for (int i = 0; i < doc_input.parentWindow.frames.length; i++)
        {
            j = i;
            mshtml.IHTMLWindow2 frame = doc_input.parentWindow.frames.item(ref j) as IHTMLWindow2;
            IHTMLDocument3 doc_child3 = CorssDomainHelper.GetDocumentFromWindow(frame.window as IHTMLWindow2);
            mshtml.HTMLDocument doc_child = (mshtml.HTMLDocument)doc_child3;

            invoke_click_by_id_loop(ref doc_child, id);
        }


        mshtml.IHTMLElementCollection ielements = (mshtml.IHTMLElementCollection)doc_input.all;

        foreach (mshtml.IHTMLElement ielement in ielements)
        {
            mshtml.IHTMLDOMNode node = (mshtml.IHTMLDOMNode)ielement;
            IHTMLAttributeCollection attrs = (IHTMLAttributeCollection)node.attributes;

            if (ielement.id == id)
            {
                ielement.click();
            }

        }
    }

    public static List<BsonDocument> get_all_elments(ref WebBrowser browser)
    {
        List<BsonDocument> docs = new List<BsonDocument>();
        mshtml.HTMLDocument doc_child = (mshtml.HTMLDocument)browser.Document.DomDocument;
        get_all_elements_loop(ref docs, ref doc_child);
        return docs;
    }
    public static void get_all_elements_loop(ref List<BsonDocument> docs, ref mshtml.HTMLDocument doc_input)
    {
        object j;
        for (int i = 0; i < doc_input.parentWindow.frames.length; i++)
        {
            j = i;
            mshtml.IHTMLWindow2 frame = doc_input.parentWindow.frames.item(ref j) as IHTMLWindow2;
            IHTMLDocument3 doc_child3 = CorssDomainHelper.GetDocumentFromWindow(frame.window as IHTMLWindow2);
            mshtml.HTMLDocument doc_child = (mshtml.HTMLDocument)doc_child3;
            get_all_elements_loop(ref docs, ref doc_child);
        }


        mshtml.IHTMLElementCollection ielements = (mshtml.IHTMLElementCollection)doc_input.all;

        foreach (mshtml.IHTMLElement ielement in ielements)
        {
             
            mshtml.IHTMLDOMNode inode = (mshtml.IHTMLDOMNode)ielement;
            IHTMLAttributeCollection iattrs = (IHTMLAttributeCollection)inode.attributes;

            string text = "";
            if (ielement.innerText != null) text = ielement.innerText;

            bool is_contine = true;
          
            if (((IHTMLElementCollection)ielement.children).length == 1 && ielement.innerHTML!=null && ielement.innerHTML.ToLower().Trim().Contains("<br") == true)
            {
                text = ielement.innerHTML;
                is_contine = false;
            } 
            if (((IHTMLElementCollection)ielement.children).length == 0 && !string.IsNullOrEmpty(text)) is_contine = false;
            if (is_contine) continue;

            string type = (ielement.tagName == null) ? "" : ielement.tagName;
            string id = (ielement.id == null) ? "" : ielement.id;
            //string name = (ielement.getAttribute("name") == null) ? "" : ielement.getAttribute("name").ToString();
            string name = (ielement.getAttribute("name", 0) == null) ? "" : ielement.getAttribute("name", 0).ToString();
            string html = (ielement.outerHTML == null) ? "" : ielement.outerHTML;
            string class_name = (ielement.className == null) ? "" : ielement.className;

            string str_attrs = "";
            if (iattrs != null)
            {
                foreach (IHTMLDOMAttribute attr in iattrs)
                { 
                    if (attr.nodeValue!=null && !string.IsNullOrEmpty(attr.nodeValue.ToString().Trim()))
                    {
                        str_attrs += attr.nodeName + ":" + attr.nodeValue.ToString()+",";
                    }
                }
            }


            BsonDocument doc = new BsonDocument();
            doc.Add("type", type);
            doc.Add("id", id);
            doc.Add("name", name);
            doc.Add("html", html);
            doc.Add("text", text);
            doc.Add("class", class_name);
            doc.Add("attrs", str_attrs);
            docs.Add(doc);
        }
    }
}

