using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.Threading;

namespace ChromiumUpdater.Engine
{
    public class ChromiumUpdateEngine
    {
        public String GetChromiumLatestVersionString()
        {
            ChromiumUrlBuilder urlBuilder = new ChromiumUrlBuilder();
            Uri versionUri = urlBuilder.GetUrlToLatestChromiumVersionDescription();
            DownloadStringCompletedEventArgs completedEventArgs = null;
            AutoResetEvent ev = new AutoResetEvent(false);
            WebClient webClient = new WebClient();

            webClient.DownloadProgressChanged += (s, e) => 
            {
                DownloadProgressChangedEventArgs ea = e;
            };

            webClient.DownloadStringCompleted += (s, e) =>
            {
                completedEventArgs = e;
                ev.Set();
            };

            webClient.DownloadStringAsync(versionUri);
            ev.WaitOne();

            if (completedEventArgs.Error != null)
                throw new ApplicationException(completedEventArgs.Error.Message, completedEventArgs.Error);

            if (completedEventArgs.Cancelled)
                return String.Empty;

            return completedEventArgs.Result;
        }
    }
}
