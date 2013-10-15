using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace back_end
{
    class Program
    {
        static void Main(string[] args)
        {
            HttpServer MainServer = new HttpServer("http://*:5555/");
            MainServer.ServerCycle();            
        }

        static void ListenerCallback(IAsyncResult result)
        {
            
        }
    }
}
