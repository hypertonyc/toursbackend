using System;
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
            listener.GetContextAsync();
            //listener.BeginGetContext(new AsyncCallback(HttpRequestCallback), this);
        }

        ~HttpServer()
        {
            if (listener.IsListening)
            {
                listener.Stop();
                listener.Close();
            }
        }

        void CheckFinishedSession(SearchSession CurSession)
        {
            if (CurSession.IsFinished())
                SessionMap.Remove(CurSession.GetSesID());
        }

        SearchSession ProcessRequest(HttpListenerRequest HttpRequest)
        {
            String SesID = HttpRequest.QueryString["SessionID"];//TODO: HardCoded!!!
            SearchSession CurSession = null;
            if (SesID == null)
            {
                CurSession = new SearchSession();
                CurSession.Start(HttpRequest.QueryString);
                SessionMap.Add(CurSession.GetSesID(), CurSession);
            }
            else
            {
                if(!SessionMap.TryGetValue(SesID, out CurSession))
                {
                    System.Diagnostics.Debug.WriteLine("Unknown Session (" + SesID + ")");
                }
            }
            return CurSession;
        }

        static void HttpRequestCallback(IAsyncResult result)
        {
            HttpServer ThisServer = (HttpServer)result.AsyncState;
            HttpListener Listener = ThisServer.listener;
            HttpListenerContext Context = Listener.EndGetContext(result);
            Listener.BeginGetContext(new AsyncCallback(HttpRequestCallback), ThisServer);
            SearchSession CurrentSession = ThisServer.ProcessRequest(Context.Request);
            if (CurrentSession != null)
            {
                CurrentSession.MakeResponse(Context.Response);
                ThisServer.CheckFinishedSession(CurrentSession);                
            }
        }
    }
}
