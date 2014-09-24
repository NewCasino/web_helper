using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using MongoDB.Bson;
using MongoDB.Driver;



class Email100Mix
{
    static bool is_open_mongo = false;

    public static  BsonDocument get_document()
    {
        BsonDocument doc = new BsonDocument();
        doc.Add("start_time", "");
        doc.Add("host", "");
        doc.Add("client", ""); 

        return doc;

    }
    public void compute()
    {
        BsonDocument doc = new BsonDocument();
        if (is_open_mongo) MongoHelper.insert_bson("email", doc); 
    }
}

