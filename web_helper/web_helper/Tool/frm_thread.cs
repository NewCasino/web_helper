using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading;
using MongoDB.Bson;
using System.Runtime.Remoting.Messaging;
namespace web_helper
{
    public partial class frm_thread : Form
    {
        
        Thread _thread = null;//定义线程  
        delegate void D_SetTxt(string str);//定义委托，用来完成线程的赋值   
        public delegate int D_Add(int a, int b);
        StringBuilder sb = new StringBuilder();

        public frm_thread()
        {
            InitializeComponent();
        } 

        private void txt_result_TextChanged(object sender, EventArgs e)
        {
            this.txt_result.SelectionStart = this.txt_result.TextLength;
            this.txt_result.ScrollToCaret();
        }
        private void btn_start_Click(object sender, EventArgs e)
        {
            //线程更新窗体内容
            _thread = new Thread(new ThreadStart(set_txt));
            _thread.Start();


            //线程传递参数
            ParameterizedThreadStart start = new ParameterizedThreadStart(set_txt);
            Thread thread = new Thread(start);
            object obj = "From Thread With Pareameter:Hello ParameterizedThreadStart!!! ";
            thread.Start(obj);

            Thread thread2 = new Thread(() => set_txt("From Thread With Lambda:Hello Lam!!!"));
            thread2.Start();


            ThreadPool.SetMaxThreads(100, 50);
            ThreadPool.QueueUserWorkItem(set_txt, "From Thread With ThreadPool:Hello ThreadPool!!!");


            //传递BsonDocument
            BsonDocument doc_input = new BsonDocument();
            doc_input.Add("sleep", 700);
            doc_input.Add("txt", "From BsonDocument!!!");

            ParameterizedThreadStart start3 = new ParameterizedThreadStart(set_txt_with_doc);
            Thread thread3 = new Thread(start3);
            object obj3 = (object)doc_input;
            thread3.Start(obj3);

            //异步调用
            D_Add handler = new D_Add(f_add);
            sb.AppendLine("Start Add!!");
            IAsyncResult result = handler.BeginInvoke(1, 2, new AsyncCallback(f_complete), "AsycState:OK");
            sb.AppendLine("Start do other work!!");
            this.txt_result.Text = sb.ToString();


        } 
        private void btn_stop_Click(object sender, EventArgs e)
        {
            if (_thread.IsAlive)
            {
                _thread.Abort();
            }
        }




  
        private void set_txt()
        {
            for (int i = 1; i < 10; ++i)
            {
                //这么调用会失败，产生错误“线程间操作无效”  
                //this.txt_result.Text = i.ToString();  

                //需要用委托调用  
                D_SetTxt d_set_txt = new D_SetTxt(f_set_txt);
                //用invoke 方法来达到线程间操作的目的  
                this.Invoke(d_set_txt,"Thread Set Text:"+ i.ToString());
                Thread.Sleep(500);
            }
        } 
        private void set_txt(object obj)
        {
            Thread.Sleep(2000);
            D_SetTxt d_set_txt = new D_SetTxt(f_set_txt); 
            this.Invoke(d_set_txt,"Current Thread ID:"+ Thread.CurrentThread.ManagedThreadId.ToString()+"  "+ obj.ToString());
        } 
        private void set_txt_with_doc(object obj)
        {
            BsonDocument doc = (BsonDocument)obj;
            Thread.Sleep(Convert.ToInt16(doc["sleep"].ToString()));
            D_SetTxt d_set_txt = new D_SetTxt(f_set_txt);
            this.Invoke(d_set_txt, "Current Thread ID:" + Thread.CurrentThread.ManagedThreadId.ToString() + "  " +doc["txt"].ToString());
        } 
        private void f_set_txt(string str)
        {
            sb.AppendLine(str);
            this.txt_result.Text = sb.ToString();
        }



        private int f_add(int a, int b)
        {
            D_SetTxt d_set_txt = new D_SetTxt(f_set_txt); 

            this.Invoke(d_set_txt, "ADD Start");
            Thread.Sleep(3000);
            this.Invoke(d_set_txt, "ADD Complete");
            return a + b;
        }

        private void f_complete(IAsyncResult result)
        {
            D_SetTxt d_set_txt = new D_SetTxt(f_set_txt);


            D_Add handler = (D_Add)((AsyncResult)result).AsyncDelegate;
            string str_result = handler.EndInvoke(result).ToString();
            string str_state = result.AsyncState.ToString();
            this.Invoke(d_set_txt, "Complte Async Result:"+str_result);
            this.Invoke(d_set_txt,"Complte ASync State:"+ str_state);
        }
    } 
}
