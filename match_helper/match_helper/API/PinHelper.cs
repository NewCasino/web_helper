

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
 

class PinHelper
{
 

    static string id;
    static string password; 
    static PinHelper()
    { 
        id = "904796";
        password = "password"; 
    } 
 
    public static string query(string url,string post)
    { 
        var request = (HttpWebRequest)WebRequest.Create(url);
        string credentials = String.Format("{0}:{1}", id, password);
        byte[] bytes = Encoding.UTF8.GetBytes(credentials);
        string base64 = Convert.ToBase64String(bytes);
        string authorization = String.Concat("Basic ", base64);

        request.Headers.Add("Authorization", authorization);
        request.Method = "POST";
        request.Accept = "application/json";
        request.ContentType = "application/json; charset=utf-8"; 

        byte[] data = Encoding.UTF8.GetBytes(post);
        Stream request_stream = request.GetRequestStream();
        request_stream.Write(data, 0, data.Length);
        request_stream.Close();

        HttpWebResponse response;
        try
        {
            response = (HttpWebResponse)request.GetResponse();
        }
        catch (WebException error)
        {
            response = (HttpWebResponse)error.Response;
        }

        var response_stream = response.GetResponseStream();
        string body;
        using (var reader = new StreamReader(response_stream))
        {
            body = reader.ReadToEnd();
        } 
        return body;
    } 
}


