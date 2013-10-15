using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Net;

namespace back_end
{
    class OperatorPasrser
    {
        public void GetXmlFromOperator(String Uri)
        {
            HttpWebRequest wreq = (HttpWebRequest)WebRequest.Create(Uri);
            HttpWebResponse wres = (HttpWebResponse)wreq.GetResponse();
            Stream resp = wres.GetResponseStream();
            using (StreamReader reader = new StreamReader(resp))
            {
                Console.WriteLine(reader.ReadToEnd());
            }
                        
            resp.Close();
        }         
    }    
}
