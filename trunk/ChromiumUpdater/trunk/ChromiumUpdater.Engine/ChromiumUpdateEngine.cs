using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.Threading;
using ChromiumUpdater.Engine.Io;
using ChromiumUpdater.Engine.Extensions;

namespace ChromiumUpdater.Engine
{
    public class ChromiumUpdateEngine
    {
        public Stream GetChromiumVersionChangeLogDataStream(String version)
        {
            ChromiumUrlBuilder urlBuilder = new ChromiumUrlBuilder();
            Uri uri = urlBuilder.GetUrlToVersionUpdateXml(version);
            WebClient webClient = new WebClient();
            using (Stream s = webClient.OpenRead(uri))
            {
                VirtualStream vs = new VirtualStream();
                s.CopyContentsTo(vs);
                return vs;
            }
        }

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
