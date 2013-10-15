using System;
using System.IO;
using System.Net;
using System.Runtime.Serialization;
namespace back_end
{
    
    class HttpServer
    {
        protected HttpListener listener;
        bool InCycle;

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
            InCycle = true;
        }

        ~HttpServer()
        {
            listener.Stop();
            listener.Close();
        }

        public void ServerCycle()
        {
            while (InCycle)
            {
                IAsyncResult result = listener.BeginGetContext(new AsyncCallback(HttpRequestCallback), listener);                
                Console.WriteLine("Waiting for request to be processed asyncronously.");
                result.AsyncWaitHandle.WaitOne();                
                Console.WriteLine("Request processed asyncronously.");                
            }
        }

        static void HttpRequestCallback(IAsyncResult result)
        {
            HttpListener listener = (HttpListener)result.AsyncState;            
            HttpListenerContext context = listener.EndGetContext(result);
            
            HttpListenerRequest request = context.Request;
            HttpListenerResponse response = context.Response;

            //DataContractJsonSerializer jsonstr = new DataContractJsonSerializer();
            

            

            

           /* StreamReader reader = new StreamReader(request.InputStream);
            String str = reader.ReadToEnd();
            reader.Close();*/

            
            
            string responseString = "<HTML><BODY> Hello world!</BODY></HTML>";
            byte[] buffer = System.Text.Encoding.UTF8.GetBytes(responseString);
            response.ContentLength64 = buffer.Length;
            Stream output = response.OutputStream;
            output.Write(buffer, 0, buffer.Length);
            output.Close();
        }
    }
}
