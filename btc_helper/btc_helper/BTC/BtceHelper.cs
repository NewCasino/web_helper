
 
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


public enum BtcePair
{
    btc_usd,
    btc_rur,
    btc_eur,
    ltc_btc,
    ltc_usd,
    ltc_rur,
    nmc_btc,
    nvc_btc,
    usd_rur,
    eur_usd,
    trc_btc,
    ppc_btc,
    ftc_btc,
    Unknown
}

class  BtceHelper
{

    static string key;
    static HMACSHA512 hash_mark;
    static UInt32 nonce;
    static BtceHelper()
    {
        key = "HAB2XCLZ-AVJ9P90L-3FTQCS54-3AYITXUZ-J0I350F5";
        hash_mark = new HMACSHA512(Encoding.ASCII.GetBytes("907d485c3d685dbc64b3cb04342901740b8d38df079be6fd849cc1b44258cabe"));
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
    public static string query(Dictionary<string, string> args)
    {
        args.Add("nonce", get_nonce().ToString());

        var str_data = get_post_string(args);
        var data = Encoding.ASCII.GetBytes(str_data);

        var request = WebRequest.Create(new Uri("https://btc-e.com/tapi")) as HttpWebRequest;
        if (request == null)  throw new Exception("Non HTTP WebRequest");

        request.Method = "POST";
        request.Timeout = 15000;
        request.ContentType = "application/x-www-form-urlencoded";
        request.ContentLength = data.Length;

        request.Headers.Add("Key", key);
        request.Headers.Add("Sign", get_string_from_bytes(hash_mark.ComputeHash(data)).ToLower());
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
  
    static UInt32 get_nonce()
    {
        return nonce++;
    }
}





