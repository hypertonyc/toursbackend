using System;
using System.Collections.Generic;
using System.IO;
//using System.Linq;
//using System.Text;

namespace back_end
{
    class SearchSession
    {
        static readonly Random rndGen = new Random();
        String SessionId;
        DateTime dt;
        String CallbackName;
        bool Finished;

        string GetRandomString(int pwdLength)
        {
            const string rc = "qwertyuiopasdfghjklzxcvbnm";
            char[] sid = new char[pwdLength];
            for (int i = 0; i < sid.Length; i++)
                sid[i] = rc[rndGen.Next(rc.Length)];
            return new string(sid);
        }

        public SearchSession()
        {
            SessionId = GetRandomString(30);
            Finished = false;
        }

        public void Start(System.Collections.Specialized.NameValueCollection Params)
        {
            dt = DateTime.Now;
            try
            {
                CallbackName = Params["callback"];
                foreach (String Op in Params["Operators[]"].Split(','))
                {                    
                    switch (Op.ToUpper().Trim())
                    {
                        case "TEZTOUR":
                            System.Diagnostics.Debug.WriteLine(Op);
                            break;
                        default:
                            System.Diagnostics.Debug.WriteLine("Unknown operator: " + Op);
                            break;
                    }
                }
            }
            catch (Exception e)
            {
                System.Diagnostics.Debug.WriteLine(e.Message);
            }
        }

        public string GetSesID()
        {
            return SessionId;
        }

        public bool IsFinished()
        {
            return Finished;
        }

        public void MakeResponse(System.Net.HttpListenerResponse Response)
        {
            Response.ContentType = "text/javascript";
            String responseString = CallbackName + "({\"SessionID\" : \"" + SessionId + "\", \"Delay\" : \"" + dt.ToString() + "\"});";
            byte[] buffer = System.Text.Encoding.UTF8.GetBytes(responseString);
            Response.ContentLength64 = buffer.Length;
            Stream output = Response.OutputStream;
            output.Write(buffer, 0, buffer.Length);
            output.Close();
        }
    }
}
