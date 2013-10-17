using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace back_end
{
    class SearchSession
    {
        static readonly Random rndGen = new Random();
        String SessionId;
        DateTime dt;

        public SearchSession()
        {
            SessionId = GetRandomString(30);
        }

        public String GetId()
        {
            return SessionId;
        }

        public void Start()
        {
            dt = DateTime.Now;
        }

        public TimeSpan GetResult()
        {
            return DateTime.Now - dt;
        }

        public void DoResponse(System.Net.HttpListenerResponse response)
        {

        }

        static string GetRandomString(int pwdLength)
        {
            const string rc = "qwertyuiopasdfghjklzxcvbnm";
            char[] sid = new char[pwdLength];
            for (int i = 0; i < sid.Length; i++)
                sid[i] = rc[rndGen.Next(rc.Length)];
            return new string(sid);
        }
    }
}
