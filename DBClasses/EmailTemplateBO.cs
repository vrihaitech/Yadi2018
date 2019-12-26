using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Configuration;

namespace Yadi.DBClasses
{
    public class EmailTemplateBO
    {
        public string EmailSubject;
        public string EmailBody;


        public EmailTemplateBO()
        {
        }

        public EmailTemplateBO(string EmailSubject, string EmailBody)
        {
            this.EmailSubject = EmailSubject;
            this.EmailBody = EmailBody;
        }

    }
}