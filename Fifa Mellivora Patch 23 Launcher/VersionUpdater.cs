using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Net.Mail;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Fifa_Mellivora_Patch_23_Launcher
{
    internal class VersionUpdater
    {
        string mailmessage = "";
        public string GetVersion()
        {
            WebClient webClient = new WebClient();
            ((NameValueCollection)webClient.Headers).Add("user-agent", "Mozilla/4.0 (compatible; MSIE 6.0; Windows NT 5.1; .NET CLR 1.0.3705;)");
            byte[] newFileData = webClient.DownloadData(new Uri("https://github.com/R-Fatih/MP23/raw/main/version.txt"));
            mailmessage = System.Text.Encoding.UTF8.GetString(newFileData);
            return mailmessage;
        }
    }
}
