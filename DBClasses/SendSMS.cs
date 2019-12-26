using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;

namespace Yadi.DBClasses
{
   public  class SendSMS
    {
        public bool SMS(string ApiString)
        {
            WebRequest request = HttpWebRequest.Create(ApiString);
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            Stream s = (Stream)response.GetResponseStream();
            StreamReader readStream = new StreamReader(s);
            if (response.StatusCode == HttpStatusCode.OK)
            {
                response.Close();
                s.Close();
                readStream.Close();
                return true;
            }
            else
            {
                response.Close();
                s.Close();
                readStream.Close();
                return false;
            }                    
        }

    }
}
