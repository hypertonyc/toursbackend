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
        public OperatorPasrser()
        {

        }

        public void GetXmlFromOperator(String Uri)
        {
            WebRequest wreq = WebRequest.Create(Uri);
            WebResponse wres = (HttpWebResponse)wreq.GetResponse();
            Stream resp = wres.GetResponseStream();
            using (StreamReader reader = new StreamReader(resp))
            {
                Console.WriteLine(reader.ReadToEnd());
            }
                        
            resp.Close();
        }         
    }    
}
