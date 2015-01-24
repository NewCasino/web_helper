

using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using MongoDB.Bson;

 

class BtctradeHelper
{

    static string api_key;
    static string secret_key;
    static HMACSHA512 hash_mark;
    static UInt64 nonce;
    static BtctradeHelper()
    {
        api_key = SQL.get_value("btctrade_api_key");
        secret_key = SQL.get_value("btctrade_secret_key");
        hash_mark = new HMACSHA512(Encoding.ASCII.GetBytes(SQL.get_value("btctrade_hash_key")));
        nonce = UnixTime.unix_now;
    }
    public static string query(string url)
    {
        var request = WebRequest.Create(url);
        request.Proxy = WebRequest.DefaultWebProxy;
        request.Proxy.Credentials = System.Net.CredentialCache.DefaultCredentials;
        if (request == null)
            throw new Exception("Non HTTP WebRequest");
        return new StreamReader(request.GetResponse().GetResponseStream()).ReadToEnd();
    }
   
    public static string query(string url,Dictionary<string, string> args)
    {
        
        Dictionary<string,string>  list_sign=new  Dictionary<string,string>();
        list_sign.Add("api_key", api_key);
        list_sign.Add("secret_key", secret_key);
        string str_sign_input = get_post_string(list_sign); 
        string sign = System.Web.Security.FormsAuthentication.HashPasswordForStoringInConfigFile(str_sign_input, "MD5").ToUpper();


        args.Add("api_key", api_key);
        args.Add("sign", sign);
        string str_data = get_post_string(args); 

        var data = Encoding.ASCII.GetBytes(str_data);

        var request = WebRequest.Create(new Uri(url)) as HttpWebRequest;
        if (request == null) throw new Exception("Non HTTP WebRequest");

        request.Method = "POST";
        request.Timeout = 15000;
        request.ContentType = "application/x-www-form-urlencoded";
        request.ContentLength = data.Length;
 
        var request_stream = request.GetRequestStream();
        request_stream.Write(data, 0, data.Length);
        request_stream.Close();
        return new StreamReader(request.GetResponse().GetResponseStream()).ReadToEnd();

    }

    static string get_string_from_bytes(byte[] ba)
    {
        return BitConverter.ToString(ba).Replace("-", "");
    }
    static string get_post_string(Dictionary<string, string> list)
    {
        StringBuilder sb = new StringBuilder();
        foreach (var item in list)
        {
            sb.AppendFormat("{0}={1}", item.Key, HttpUtility.UrlEncode(item.Value));
            sb.Append("&");
        }
        if (sb.Length > 0) sb.Remove(sb.Length - 1, 1);
        return sb.ToString();
    }
  
    static UInt64 get_nonce()
    {
        return nonce++;
    }
}





