

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
using System.Collections.Specialized; 
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
 

class OandaHelper
{

    static string api_key;
    static string secret_key;
    static string token;
    static string id;
    static OandaHelper()
    {
        api_key = "313f2379-26c7-4204-86b5-bc66e04ac3e5";
        secret_key = "9f4aa8d2-2f2b-4173-82a2-15719f365727";

        token = "6a50e090edded7218c8f07155a528cb5-fc26c52a44e563b0903c7f12f15b6d9c";
        id = "904796";
    }
    public static string query(string url)
    { 
        var request = WebRequest.Create(url);
        string credentialHeader = String.Format("Bearer {0}", token);
        request.Method = "GET";
        request.ContentType = "application/json";
        //request.UserAgent = "OANDAExchangeRates.C#/0.01";
        request.Headers.Add("Authorization", credentialHeader);

       
        request.Proxy = WebRequest.DefaultWebProxy;
        request.Proxy.Credentials = System.Net.CredentialCache.DefaultCredentials;
        if (request == null)
            throw new Exception("Non HTTP WebRequest");
        return new StreamReader(request.GetResponse().GetResponseStream()).ReadToEnd();
    }

    public static string query(string url, string method,BsonArray param)
    {

        TimeSpan span = DateTime.UtcNow - new DateTime(1970, 1, 1);
        long ms = Convert.ToInt64(span.TotalMilliseconds * 1000);
        string tonce = Convert.ToString(ms);

        Dictionary<string, string> list_sign = new Dictionary<string, string>();
        list_sign.Add("tonce", tonce);
        list_sign.Add("accesskey", api_key);
        list_sign.Add("requestmethod", "post");
        list_sign.Add("id", "1");
        list_sign.Add("method", method);
        list_sign.Add("params", "");
        string str_sign_input = get_post_string(list_sign);


        string hash = get_hm_hash(secret_key, str_sign_input);
        string base64 = Convert.ToBase64String(Encoding.ASCII.GetBytes(api_key + ':' + hash));


        BsonDocument doc = new BsonDocument();
        doc.Add("method", method);
        doc.Add("params", param);
        doc.Add("id", 1); 
        byte[] data = Encoding.ASCII.GetBytes(doc.ToString());

        var request = WebRequest.Create(new Uri(url)) as HttpWebRequest;
        if (request == null) throw new Exception("Non HTTP WebRequest");

      

        request.Method = "POST";
        request.ContentType = "application/json-rpc";
        request.ContentLength = data.Length;
        request.Headers["Authorization"] = "Basic " + base64;
        request.Headers["Json-Rpc-Tonce"] = tonce;

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

    static string get_hm_hash(string secret_key, string input)
    {
        HMACSHA1 hmacsha1 = new HMACSHA1(Encoding.ASCII.GetBytes(secret_key));
        MemoryStream stream = new MemoryStream(Encoding.ASCII.GetBytes(input));
        byte[] hashData = hmacsha1.ComputeHash(stream);

        // Format as hexadecimal string.
        StringBuilder sb = new StringBuilder();
        foreach (byte data in hashData)
        {
            sb.Append(data.ToString("x2"));
        }
        return sb.ToString();
    }
}


