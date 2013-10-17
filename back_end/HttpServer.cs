using System;
using System.IO;
using System.Net;
using System.Collections.Generic;
using System.Runtime.Serialization.Json;
namespace back_end
{    
    class HttpServer
    {
        HttpListener listener;
        Dictionary<String, SearchSession> SessionMap;

        public HttpServer(string url)
        {
            if (!HttpListener.IsSupported)
            {
                Console.WriteLine("Not Supported!");
                return;
            }
            listener = new HttpListener();
            listener.Prefixes.Add(url);
            try
            {
                listener.Start();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return;
            }
            SessionMap = new Dictionary<string, SearchSession>();
            listener.BeginGetContext(new AsyncCallback(HttpRequestCallback), this);
        }

        ~HttpServer()
        {
            if (listener.IsListening)
            {
                listener.Stop();
                listener.Close();
            }
        }

        public void AddSession(SearchSession s)
        {
            SessionMap.Add(s.GetId(), s);
        }

        public bool GetSession(String SessionId, out SearchSession s)
        {
            return SessionMap.TryGetValue(SessionId, out s);
        }
        //DataContractJsonSerializer jsonstr = new DataContractJsonSerializer(

        SearchSession ProcessRequest(HttpListenerContext HttpContext)
        {
            return null;
        }

        static void HttpRequestCallback(IAsyncResult result)
        {
            HttpServer ThisServer = (HttpServer)result.AsyncState;
            HttpListener Listener = ThisServer.listener;
            HttpListenerContext Context = Listener.EndGetContext(result);
            Listener.BeginGetContext(new AsyncCallback(HttpRequestCallback), ThisServer);
            SearchSession CurrentSession = ThisServer.ProcessRequest(Context);
            CurrentSession.DoResponse(Context.Response);
            
            //HttpListenerRequest request = context.Request;
            //String SessionID = request.QueryString["SessionID"];
            //SearchSession Session = null;
            //if (SessionID == null)
            //{
            //    Session = new SearchSession();
            //    Session.Start();                
            //    ThisServer.AddSession(Session);
            //    System.Diagnostics.Debug.WriteLine("NewSession");
            //}
            //else
            //{
            //    if (ThisServer.GetSession(SessionID, out Session))
            //    {
            //        System.Diagnostics.Debug.WriteLine("Session " + SessionID);
            //    }
            //    else
            //    {
            //        response.Close();
            //    }
            //}

            //HttpListenerResponse response = context.Response;
            
            //response.ContentType = "text/javascript";
            //responseString = request.QueryString["callback"] + "({\"SessionID\" : \"" + Session.GetId() + "\", \"Delay\" : " + (int)Session.GetResult().TotalSeconds + "});";            
            //byte[] buffer = System.Text.Encoding.UTF8.GetBytes(responseString);
            //response.ContentLength64 = buffer.Length;
            //Stream output = response.OutputStream;
            //output.Write(buffer, 0, buffer.Length);
            //output.Close();
        }
    }
}
