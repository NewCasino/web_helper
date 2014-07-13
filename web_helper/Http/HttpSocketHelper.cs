using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Text.RegularExpressions;
using System.IO.Compression;
using System.Collections;

class HttpSocketHelper
{
    public static Request get_reqeust(string url, string method, Hashtable hash_post, Hashtable hash_cookie)
    {
        Request request = new Request(); 
        string str_send = "";

        string str_cookie = convert_cookie_to_string(hash_cookie);
        string str_post = "";
        byte[] byte_post = new byte[0];
        if (method == "POST")
        {
            str_post = convert_post_to_string(hash_post);
            byte_post = Encoding.ASCII.GetBytes(str_post);
        }


        str_send = method + " " + url + " HTTP/1.1\r\n" +
                 "Host: " + get_host_from_url(url) + "\r\n" +
                 "Content-Length: " + byte_post.Length.ToString() + "\r\n" +
                 "Connection:keep-alive\r\n" +
                 "Accept: text/html,application/xhtml+xml,application/xml;q=0.9,image/webp,*/*;q=0.8\r\n" +
                 "User-Agent: Mozilla/5.0 (Windows NT 6.2; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/31.0.1650.63 Safari/537.36\r\n" +
                 "Accept-Encoding: gzip,deflate,sdch\r\n" +
                 "Accept-Language: zh-CN,zh;q=0.8\r\n" +
                 str_cookie +
                 "\r\n" +
                 str_post;
        request.content = str_send;
        request.url = url;

        return request;
    }
    public static Request get_request_500_excel(string id)
    {
        Request request = new Request(); 
        string str_send = "";
         
        str_send = @"POST http://odds.500.com/fenxi/europe_xls.php HTTP/1.1
Host: odds.500.com
Connection: keep-alive
Content-Length: 1054
Cache-Control: max-age=0
Accept: text/html,application/xhtml+xml,application/xml;q=0.9,image/webp,*/*;q=0.8
Origin: http://odds.500.com
User-Agent: Mozilla/5.0 (Windows NT 6.2; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/34.0.1847.131 Safari/537.36
Content-Type: application/x-www-form-urlencoded
Referer: http://odds.500.com/fenxi/ouzhi-######.shtml
Accept-Encoding: gzip,deflate,sdch
Accept-Language: zh-CN,zh;q=0.8
Cookie: CLICKSTRN_ID=117.91.238.65-1402749723.803921::CF0A3888FAB44290BFEB272F0DCA9CF8; bdshare_firstime=1404659205381; ck_RegFromUrl=http%3A//www.baidu.com/s%3Fwd%3D500%26ie%3Dutf-8%26tn%3Dbaiduhome_pg%26f%3D8%26rsv_bp%3D1%26rsv_spt%3D1%26rsv_sug3%3D2%26rsv_sug4%3D54%26rsv_sug1%3D2%26rsv_sug2%3D0%26inputT%3D946%26bs%3DSingbet; seo_key=baidu%7C500%7Chttp://www.baidu.com/s?wd=500&ie=utf-8&tn=baiduhome_pg&f=8&rsv_bp=1&rsv_spt=1&rsv_sug3=2&rsv_sug4=54&rsv_sug1=2&rsv_sug2=0&inputT=946&bs=Singbet; sdc_session=1405170024898; ck_RegUrl=www.500.com; WT_FPC=id=undefined:lv=1405179189025:ss=1405178450015; sdc_userflag=1405178450018::1405179189029::5; Hm_lvt_4f816d475bb0b9ed640ae412d6b42cab=1404910309,1405000667,1405156593,1405170025; Hm_lpvt_4f816d475bb0b9ed640ae412d6b42cab=1405179189; __utma=63332592.1310368986.1402749766.1405170026.1405178451.17; __utmb=63332592.5.10.1405178451; __utmc=63332592; __utmz=63332592.1405156594.15.14.utmcsr=baidu|utmccn=(organic)|utmcmd=organic|utmctr=500

fixtureid=######&excelst=1&style=0&ctype=1&dcid=&scid=&r=1&mawinc2=3.35&madrawc2=4.15&malostc2=3.75&mawinlc2=46.59%25&madrawlc2=32.25%25&malostlc2=42.30%25&mapaylc2=99.96%25&maklwc2=1.42&makldc2=1.24&makllc2=1.04&mawinj2=2.95&madrawj2=4.20&malostj2=4.00&mawinlj2=48.15%25&madrawlj2=32.25%25&malostlj2=40.55%25&mapaylj2=99.96%25&maklwj2=1.25&makldj2=1.25&makllj2=1.11&miwinc2=1.90&midrawc2=2.70&milostc2=2.10&miwinlc2=27.26%25&midrawlc2=20.35%25&milostlc2=25.18%25&mipaylc2=81.81%25&miklwc2=0.80&mikldc2=0.81&mikllc2=0.58&miwinj2=1.75&midrawj2=2.75&milostj2=2.25&miwinlj2=30.93%25&midrawlj2=20.06%25&milostlj2=23.74%25&mipaylj2=81.81%25&miklwj2=0.74&mikldj2=0.82&mikllj2=0.63&avwinc2=2.27&avdrawc2=3.18&avlostc2=3.31&avwinlc2=41.67%25&avdrawlc2=29.72%25&avlostlc2=28.61%25&avpaylc2=94.36%25&avklwc2=0.96&avkldc2=0.95&avkllc2=0.92&avwinj2=2.24&avdrawj2=3.18&avlostj2=3.42&avwinlj2=42.35%25&avdrawlj2=29.85%25&avlostlj2=27.80%25&avpaylj2=94.77%25&avklwj2=0.95&avkldj2=0.95&avkllj2=0.95&lswc2=35.24&lsdc2=14.69&lslc2=48.62&lswj2=18.96&lsdj2=11.34&lslj2=32.21";
        str_send=str_send.Replace("######", id);
        request.content = str_send;
        request.url = "http://www.500.com" ;

        return request;
    }
    public static Response get_response(Request request)
    {

        Byte[] data_send = Encoding.ASCII.GetBytes(request.content);

        Response response = new Response();
        byte[] data_response = get_response_data(request);
        byte[] data_body;


        int start = find_new_2line_pos(data_response);
        string str_response = Encoding.UTF8.GetString(data_response, 0, data_response.Length);
        string str_header = str_response.Substring(0, str_response.IndexOf("\r\n\r\n"));
        string str_body = "";


        //页面跳转
        if (str_header.StartsWith("HTTP/1.1 302") || str_header.StartsWith("HTTP/1.1 301"))
        {
            string redirect_url = str_header.Substring(str_header.IndexOf("\r\nLocation: ") + "\r\nLocation: ".Length);
            redirect_url = redirect_url.Substring(0, redirect_url.IndexOf("\r\n"));
            Hashtable table = new Hashtable();
            Request request_redirect = get_reqeust(redirect_url, "GET", table, table);
            return get_response(request_redirect);
        }


        data_body = new byte[data_response.Length - start];


        Array.Copy(data_response, start, data_body, 0, data_response.Length - start);



        //当返回头中有Content-Length时,表示一次传输,能够确定长度,
        //当返回头中有Transfer-Encoding: chunked 时,表示,分段传输
        //Content-Length ,Transfer-Encoding: chunked 二者只能由其一
        if (str_header.IndexOf("Content-Length") < 0)
        {
            data_body = get_chuncked_data(data_body);
        }

        //GZip解压
        if (str_header.IndexOf("gzip") > -1)
        {
            data_body = decompress_zip(data_body);
        }


        //根据HTTP Header 解码
        if (str_header.ToLower().Contains("charset=gbk") || str_header.ToLower().Contains("charset=gb2312"))
        {
            str_body = Encoding.GetEncoding("GBK").GetString(data_body, 0, data_body.Length);
        }
        else
        {
            str_body = Encoding.UTF8.GetString(data_body, 0, data_body.Length);
        }


        //根据HTML Header解码 
        Regex reg_GB2312 = new Regex(@"<meta[^>]+Content-Type[^>]+gb2312[^>]*>", RegexOptions.IgnoreCase);
        Regex reg_GBK = new Regex(@"<meta[^>]+Content-Type[^>]+gbk[^>]*>", RegexOptions.IgnoreCase);
        Regex reg_Big5 = new Regex(@"<meta[^>]+Content-Type[^>]+Big5[^>]*>", RegexOptions.IgnoreCase);
        Regex reg_UTF8 = new Regex(@"<meta[^>]+Content-Type[^>]+utf-8[^>]*>", RegexOptions.IgnoreCase);

        if (reg_GB2312.IsMatch(str_body) || reg_GBK.IsMatch(str_body))
        {
            str_body = Encoding.GetEncoding("GBK").GetString(data_body, 0, data_body.Length);
        }
        if (reg_Big5.IsMatch(str_body))
        {
            str_body = Encoding.GetEncoding("Big5").GetString(data_body, 0, data_body.Length);
        }
        if (reg_UTF8.IsMatch(str_body))
        {
            str_body = Encoding.UTF8.GetString(data_body, 0, data_body.Length);
        }


        response.data = data_response;
        response.data_body = data_body;
        response.str_header = str_header;
        response.str_body = str_body;


        //添加Cookie
        foreach (string item in str_header.Split(new char[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries))
        {
            if (item.IndexOf("Set-Cookie:") > -1)
            {
                response.set_cookie.Add(item.Replace("Set-Cookie:", ""));
            }
        }
        return response;
    }
    public static byte[] get_response_data(Request request)
    {
        Byte[] data_send = Encoding.ASCII.GetBytes(request.content);
        MemoryStream ms = new MemoryStream();
        Socket http = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
        http.Connect(get_host_from_url(request.url), 80);
        http.Send(data_send);
        byte[] buffer = new byte[256];

        int count = 0;
        do
        {
            count = http.Receive(buffer, buffer.Length, SocketFlags.None);
            ms.Write(buffer, 0, count);
        }
        while (count > 0);

        ms.Seek(0, SeekOrigin.Begin);
        return ms.ToArray();
    }
    public static string get_host_from_url(string url)
    {
        int index = url.IndexOf(@"//");
        if (index <= 0)
            return "";
        string temp = url.Substring(index + 2);
        index = temp.IndexOf(@"/");
        if (index > 0)
            return temp.Substring(0, index);
        else
            return temp;
    }
    public static byte[] decompress_zip(byte[] data)
    {
        MemoryStream ms = new MemoryStream(data);

        GZipStream stream = new GZipStream(ms, CompressionMode.Decompress);

        byte[] data_total = new byte[40 * 1024];
        long total = 0;

        byte[] buffer = new byte[8];
        int count = 0;
        do
        {
            count = stream.Read(buffer, 0, 8);
            if (data_total.Length <= total + count) //放大数组
            {
                byte[] temp = new byte[data_total.Length * 10];
                data_total.CopyTo(temp, 0);
                data_total = temp;
            }
            buffer.CopyTo(data_total, total);
            total += count;
        } while (count != 0);
        byte[] data_desc = new byte[total];
        Array.Copy(data_total, 0, data_desc, 0, total);
        return data_desc;
    }
    public static byte[] get_chuncked_data(byte[] data)
    {
        MemoryStream ms = new MemoryStream();
        int start = 0;
        int pos = 0;
        int length = 0;
        do
        {
            pos = find_new_line_pos(data, start);
            byte[] length_data = new byte[pos - start];
            Array.Copy(data, start, length_data, 0, pos - start);
            length = Convert.ToInt32(System.Text.Encoding.Default.GetString(length_data), 16);
            ms.Write(data, pos + 2, length);
            start = pos + 2 + length + 2;
        }
        while (length > 0);
        return ms.ToArray();
    }
    public static int find_new_2line_pos(byte[] data)
    {
        for (int i = 0; i < data.Length - 3; ++i)
        {
            if (data[i] == 13 && data[i + 1] == 10 && data[i + 2] == 13 && data[i + 3] == 10)
                return i + 4;
        }
        return -1;
    }
    public static int find_new_line_pos(byte[] data, int start)
    {
        for (int i = start; i < data.Length - 3; ++i)
        {
            if (data[i] == 13 && data[i + 1] == 10)
                return i;
        }
        return -1;
    }
    public static string convert_cookie_to_string(Hashtable table)
    {

        if (table.Count == 0)
        {
            return "";
        }
        string result = "";
        foreach (string key in table.Keys)
        {
            if (!string.IsNullOrEmpty(table[key].ToString()))
            {
                result = result + key + "=" + table[key].ToString() + ";";
            }
            else
            {
                result = result + key;
            }
        }
        return "Cookie: " + result + "\r\n";
    }
    public static string convert_post_to_string(Hashtable table)
    {
        if (table.Count == 0)
        {
            return "";
        }
        string result = "";
        foreach (string key in table.Keys)
        {
            if (!string.IsNullOrEmpty(table[key].ToString()))
            {
                result = result + key + "=" + table[key].ToString() + "&";
            }
            else
            {
                result = result + key;
            }
        }
        return result.Substring(0, result.Length - 1);
    }
    public static Hashtable add_string_to_cookie(string cookie, Hashtable table)
    {
        string[] list = cookie.Split(';');

        foreach (string item in list)
        {
            string name = "";
            string value = "";
            int index = item.IndexOf('=');
            if (index > -1)
            {
                name = item.Substring(0, index);
                value = item.Substring(index + 1, item.Length - index - 1);
                bool is_has = false;
                foreach (string key in table.Keys)
                {
                    if (key == name)
                    {
                        table[key] = value;
                        is_has = true;
                        break;
                    }
                }
                if (is_has == false)
                {
                    table.Add(name, value);
                }
            }

        }
        return table;
    }

    public static void save_file_from_response(Response response, string file_name)
    {
        if (File.Exists(file_name)) File.Delete(file_name);
        FileStream stream = File.Open(file_name, FileMode.OpenOrCreate);
        stream.Write(response.data_body, 0, response.data_body.Length);
        stream.Close();
    }


}