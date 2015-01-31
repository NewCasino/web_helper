using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using MongoDB.Bson;
using WebSocket4Net;
using System.Net;
using System.Threading;
using Newtonsoft.Json;

using QuickFix;
using QuickFix.Fields;
using QuickFix.FIX44;
using System.Xml;



public partial class frm_single_btcchina : Form
{
    public StringBuilder sb = new StringBuilder();


    //---------------------------------------------Web Socket---------------------------------------------------
    public WebSocket socket;
    private static System.Threading.Timer pingIntervalTimer, pingTimeoutTimer;
    private static bool pong;
    public frm_single_btcchina()
    {
        InitializeComponent();
    }
    private void txt_result_TextChanged(object sender, EventArgs e)
    {
        this.txt_result.SelectionStart = this.txt_result.TextLength;
        this.txt_result.ScrollToCaret();
    } 
    //v1.0 message types
    public enum engineioMessageType
    {
        OPEN = 0,//non-ws
        CLOSE = 1,//non-ws
        /*
         * Pings server every "pingInterval" and expects response
         * within "pingTimeout" or closes connection.
         * 
         * client sends ping, waiting for server's pong.
         * socket.io message type is not necessary in ping/pong.
         * 
         * client sends pong after receiving server's ping.
         */
        PING = 2,
        PONG = 3,

        MESSAGE = 4,//TYPE_EVENT in v0.9.x
        UPGRADE = 5, //new in v1.0
        NOOP = 6
    }
    public enum socketioMessageType
    {
        CONNECT = 0,//right after engine.io UPGRADE
        DISCONNECT = 1,
        EVENT = 2,
        ACK = 3,
        ERROR = 4,
        BINARY_EVENT = 5,
        BINARY_ACK = 6
    } 
    private void btn_start_socket_Click(object sender, EventArgs e)
    {
        string httpScheme = "https://";
        string wsScheme = "wss://";
        string url = "websocket.btcchina.com/socket.io/";


        string polling = string.Empty;
        try
        {
            WebClient wc = new WebClient();
            polling = wc.DownloadString(httpScheme + url + "?transport=polling");
            if (string.IsNullOrEmpty(polling))
            {
                Console.WriteLine("failed to download config");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }
        string config = polling.Substring(polling.IndexOf('{'), polling.IndexOf('}') - polling.IndexOf('{') + 1);
        wsConfigHelper wsc = JsonConvert.DeserializeObject<wsConfigHelper>(config);


        //set timers
        pingTimeoutTimer = new System.Threading.Timer(_ =>
        {
            if (pong)
            {
                pong = false; //waiting for another ping
            }
            else
            {
                Console.WriteLine("Ping Timeout!");
            }
        }, null, Timeout.Infinite, Timeout.Infinite);

        pingIntervalTimer = new System.Threading.Timer(_ =>
        {
            socket.Send(string.Format("{0}", (int)engineioMessageType.PING));
            pingTimeoutTimer.Change(wsc.pingTimeout, Timeout.Infinite);
            pong = false;
        }, null, wsc.pingInterval, wsc.pingInterval);

        socket = new WebSocket(wsScheme + url + "?transport=websocket&sid=" + wsc.sid);
        //socket = new WebSocket("");
        socket.Opened += socket_open;
        socket.Error += socket_error;
        socket.MessageReceived += socket_message_received;
        socket.DataReceived += socket_data_received;
        socket.Closed += socket_closed;
        socket.Open();
    }
    public void socket_open(object sender, EventArgs e)
    {
        //send upgrade message:"52"
        //server responses with message: "40" - message/connect
        //Console.WriteLine("opened.");
        //socket.Send(string.Format("{0}{1}", (int)engineioMessageType.UPGRADE, (int)socketioMessageType.EVENT));
        sb.AppendLine("Opend:");
        socket.Send(string.Format("{0}{1}", (int)engineioMessageType.UPGRADE, (int)socketioMessageType.EVENT));
    }
    public void socket_closed(object sender, EventArgs e)
    {
        Console.WriteLine("Socket Closed!");
    }
    public void socket_data_received(object sender, DataReceivedEventArgs e)
    {
        Console.WriteLine("Receivecd Data:");
        Console.WriteLine(e.Data.ToString());
    }
    public void socket_message_received(object sender, MessageReceivedEventArgs e)
    {
        int eioMessageType = -1;
        if (int.TryParse(e.Message.Substring(0, 1), out eioMessageType))
        {
            switch ((engineioMessageType)eioMessageType)
            {
                case engineioMessageType.PING:
                    //replace incoming PING with PONG in incoming message and resend it.
                    socket.Send(string.Format("{0}{1}", (int)engineioMessageType.PONG, e.Message.Substring(1, e.Message.Length - 1)));
                    break;
                case engineioMessageType.PONG:
                    pong = true;
                    break;

                case engineioMessageType.MESSAGE:
                    int sioMessageType = -1;
                    if (int.TryParse(e.Message.Substring(1, 1), out sioMessageType))
                    {
                        switch ((socketioMessageType)sioMessageType)
                        {
                            case socketioMessageType.CONNECT:
                                //Send "42["subscribe",["marketdata_cnybtc","marketdata_cnyltc","marketdata_btcltc"]]"
                                socket.Send(string.Format("{0}{1}{2}", (int)engineioMessageType.MESSAGE,
                                                                   (int)socketioMessageType.EVENT,
                                                                   "[\"subscribe\",[\"marketdata_cnybtc\",\"marketdata_cnyltc\",\"marketdata_btcltc\"]]"));
                                break;
                            case socketioMessageType.EVENT:
                                if (e.Message.Substring(4, 5) == "trade")//listen on "trade"
                                    Console.WriteLine(e.Message.Substring(e.Message.IndexOf('{'), e.Message.LastIndexOf('}') - e.Message.IndexOf('{') + 1));
                                break;
                            default:
                                Console.WriteLine("error switch socket.io messagetype:" + e.Message);
                                break;
                        }
                    }
                    else
                    {
                        Console.WriteLine("error parse socket.io messagetype!");
                    }
                    break;

                default:
                    Console.WriteLine("error switch engine.io messagetype");
                    break;
            }
        }
        else
        {
            Console.WriteLine("error parsing engine.io messagetype!");
        }
    }
    public void socket_error(object sender, SuperSocket.ClientEngine.ErrorEventArgs e)
    {
        Console.WriteLine("Error:");
        Console.WriteLine(e.Exception.Message);
    }
    //------------------------------------------------------------------------------------------------------------


    //---------------------------------------------Fix Data-------------------------------------------------------
    private void btn_start_fix_Click(object sender, EventArgs e)
    {
        BTCCFIXClientApp app = new BTCCFIXClientApp();
        string sessionFile = Environment.CurrentDirectory+@"\session_client.txt";
        SessionSettings settings = new SessionSettings(sessionFile);
        IMessageStoreFactory storeFactory = new FileStoreFactory(settings);
        ILogFactory logFactory = new FileLogFactory(settings);
        QuickFix.Transport.SocketInitiator initiator = new QuickFix.Transport.SocketInitiator(app, storeFactory, settings, logFactory);
        initiator.Start();

        BTCCMarketDataRequest btccDataRequest = new BTCCMarketDataRequest();

        System.Threading.Thread.Sleep(5000);
        //request full snapshot
        MarketDataRequest dataRequest = btccDataRequest.marketDataFullSnapRequest("BTCCNY");
        bool ret = Session.SendToTarget(dataRequest, app.m_sessionID);
        Console.WriteLine("SendToTarget ret={0}", ret);

        //			dataRequest = btccDataRequest.marketDataFullSnapRequest("LTCCNY");
        //			ret = Session.SendToTarget(dataRequest, app.m_sessionID);
        //			Console.WriteLine("SendToTarget ret={0}", ret);

        //			dataRequest = btccDataRequest.marketDataFullSnapRequest("LTCBTC");
        //			ret = Session.SendToTarget(dataRequest, app.m_sessionID);
        //			Console.WriteLine("SendToTarget ret={0}", ret);

        System.Threading.Thread.Sleep(15000);
        //request incremental request
        dataRequest = btccDataRequest.marketDataIncrementalRequest("BTCCNY");
        ret = Session.SendToTarget(dataRequest, app.m_sessionID);
        Console.WriteLine("SendToTarget ret={0}", ret);

        //			dataRequest = btccDataRequest.marketDataIncrementalRequest("LTCCNY");
        //			ret = Session.SendToTarget(dataRequest, app.m_sessionID);
        //			Console.WriteLine("SendToTarget ret={0}", ret);

        //			dataRequest = btccDataRequest.marketDataIncrementalRequest("LTCBTC");
        //			ret = Session.SendToTarget(dataRequest, app.m_sessionID);
        //			Console.WriteLine("SendToTarget ret={0}", ret);

        System.Threading.Thread.Sleep(40000);
        //unsubscribe incremental request
        dataRequest = btccDataRequest.unsubscribeIncrementalRequest("BTCCNY");
        ret = Session.SendToTarget(dataRequest, app.m_sessionID);
        Console.WriteLine("SendToTarget ret={0}", ret);

        //			dataRequest = btccDataRequest.unsubscribeIncrementalRequest("LTCCNY");
        //			ret = Session.SendToTarget(dataRequest, app.m_sessionID);
        //			Console.WriteLine("SendToTarget ret={0}", ret);

        //			dataRequest = btccDataRequest.unsubscribeIncrementalRequest("LTCBTC");
        //			ret = Session.SendToTarget(dataRequest, app.m_sessionID);
        //			Console.WriteLine("SendToTarget ret={0}", ret);
    }
    
    //------------------------------------------------------------------------------------------------------------


    private void btn_test_Click(object sender, EventArgs e)
    {
        StringBuilder sb = new StringBuilder();
        DateTime dt_start = UnixTime.get_local_time_long(1329104164000);
        DateTime dt_end = dt_start.AddDays(0.5);
        BsonArray list = BtcCompute.get_candle("", dt_start, dt_end, 3600);

        sb.Append(BtcCompute.get_region_info_title());
        for (int i = 0; i < list.Count; i++)
        {
            BsonDocument doc = list[i].AsBsonDocument;
            sb.Append(BtcCompute.get_region_info(doc));
        }
        this.txt_result.Text = sb.ToString();
    }
    private void btn_delete_Click(object sender, EventArgs e)
    {
        delete_repeat_trade();
    }
    private void btn_analyse_depth_Click(object sender, EventArgs e)
    {
        analyse_depth();
    }
 

    public void delete_repeat_trade()
    {
        string sql = "";
        int delete_count = 0;
        for (int i = 0; i < 20000000; i++)
        {
            sql = "select * from trade_btcchina where id={0}";
            sql = string.Format(sql, i);
            DataTable dt_temp = SQLServerHelper.get_table(sql);
            if (dt_temp.Rows.Count > 0)
            {
                string id = dt_temp.Rows[0]["id"].ToString();
                string tid = dt_temp.Rows[0]["tid"].ToString();

                sql = "select * from trade_btcchina where tid={0} and id>{1}";
                sql = string.Format(sql, tid, id);
                DataTable dt_repeat = SQLServerHelper.get_table(sql);
                if (dt_repeat.Rows.Count > 0)
                {
                    sql = "delete from trade_btcchina where  tid={0} and id>{1}";
                    sql = string.Format(sql, tid, id);
                    SQLServerHelper.exe_sql(sql);
                    delete_count = delete_count + 1;
                }
                this.txt_result.Text = delete_count.PR(10) + id.PR(10) + tid.PR(10) + dt_temp.Rows.Count.PR(10);
                System.Windows.Forms.Application.DoEvents();
            }
        }
    }
    public void analyse_depth()
    {
        StringBuilder sb = new StringBuilder();
        string sql = "select distinct time from depth_log where website='btcchina'";
        DataTable dt = SQLServerHelper.get_table(sql);
        foreach (DataRow row in dt.Rows)
        {
            string time = row["time"].ToString();
            BsonArray buys = new BsonArray();
            BsonArray sells = new BsonArray();

            sql = "select * from depth_log where website='btcchina' and time={0} and type='buy'";
            sql = string.Format(sql, time);
            DataTable dt_temp = SQLServerHelper.get_table(sql);
            if (dt_temp.Rows.Count > 0) buys = MongoHelper.get_array_from_str(dt_temp.Rows[0]["text"].ToString());

            sql = "select * from depth_log where website='btcchina' and time={0} and type='sell'";
            sql = string.Format(sql, time);
            dt_temp = SQLServerHelper.get_table(sql);
            if (dt_temp.Rows.Count > 0) sells = MongoHelper.get_array_from_str(dt_temp.Rows[0]["text"].ToString());

            buys = MongoHelper.reserve_array(buys);
            sells = MongoHelper.reserve_array(sells);

            sql = "select top 100  * from trade_btcchina where time>=time";
            DataTable dt_trade = SQLServerHelper.get_table(sql);

            sb.Append(UnixTime.get_local_time_long(Convert.ToUInt64(time)).ToString("yyyy-MM-dd HH:mm:ss").PR(20) + buys.Count.PR(10) + sells.Count.PR(10) + dt_trade.Rows.Count.PR(10) + M.N);
        }

        this.txt_result.Text = sb.ToString();
        System.Windows.Forms.Application.DoEvents();
    }







}
