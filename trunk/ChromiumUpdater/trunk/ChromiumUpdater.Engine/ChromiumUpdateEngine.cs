using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.Threading;
using ChromiumUpdater.Engine.Io;
using ChromiumUpdater.Engine.Extensions;
using ChromiumUpdater.Engine.Schemas;
using HtmlAgilityPack;
using System.Xml.Linq;
using System.Xml;

namespace ChromiumUpdater.Engine
{
    public class ChromiumUpdateEngine
    {
        public void DownloadChromiumInstaller(String folder, String version, bool appendVersionToFileName, Func<FileDownloadProgressChangedEventArgs, bool> callback)
        {
            ChromiumUrlBuilder urlBuilder = new ChromiumUrlBuilder();
            Uri uri = urlBuilder.GetUrlToMiniInstaller(version);
            String fileName = Path.Combine(folder, urlBuilder.MiniInstallerFileName);
            using (FileStream fs = File.Create(fileName))
            {
                this.InternalDownloadData(uri, callback, fs);
            }
        }

        public IEnumerable<String> GetChromiumVersions()
        {
            Uri uri = new Uri(new ChromiumUrlBuilder().BaseUrl);
            String content = this.InternalDownloadString(uri, (x) =>
            {
                return true;
            }
            );
            HtmlDocument doc = new HtmlDocument();
            doc.LoadHtml(content);

            IEnumerable<String> elements = doc.DocumentNode.Descendants().Where(x => x.Name == "a")
                                                                         .Select<HtmlNode, String>(x => x.InnerText.Replace("/", String.Empty))
                                                                         .Where(y => y.ToCharArray().All(v => Char.IsDigit(v)))
                                                                         .OrderBy(x => x);
            return elements;
        }

        public Log GetChromiumVersionChangeLogData(String version)
        {
            using (Stream s = this.GetChromiumVersionChangeLogDataStream(version))
            {
                try
                {
                    return Log.Deserialize(s);
                }
                catch
                {
                    return Log.Empty;
                }
            }
        }

        public Stream GetChromiumVersionChangeLogDataStream(String version)
        {
            ChromiumUrlBuilder urlBuilder = new ChromiumUrlBuilder();
            Uri uri = urlBuilder.GetUrlToUpdateXml(version);
            WebClient webClient = new WebClient();
            using (Stream s = webClient.OpenRead(uri))
            {
                VirtualStream vs = new VirtualStream();
                s.CopyContentsTo(vs);
                vs.Position = 0;
                return vs;
            }
        }

        public String GetChromiumLatestVersionString()
        {
            ChromiumUrlBuilder urlBuilder = new ChromiumUrlBuilder();
            Uri versionUri = urlBuilder.GetUrlToLatestChromiumVersionDescription();
            String latestVersion = this.InternalDownloadString(versionUri, (x) => 
                                                                {
                                                                    return true;
                                                                }
                                                                );
            return latestVersion;
        }

        protected String InternalDownloadString(Uri uri,Func<FileDownloadProgressChangedEventArgs, bool> callback)
        {
            DownloadStringCompletedEventArgs completedEventArgs = null;
            using (AutoResetEvent ev = new AutoResetEvent(false))
            {
                using (WebClient webClient = new WebClient())
                {
                    webClient.DownloadProgressChanged += (s, e) =>
                    {
                        if (
                            (callback != null) &&
                            (!callback(new FileDownloadProgressChangedEventArgs() { BytesReceived = e.BytesReceived, ProgressPercentage = e.ProgressPercentage, TotalBytesToReceive = e.TotalBytesToReceive }))
                            )
                        {
                            webClient.CancelAsync();
                        }

                    };

                    webClient.DownloadStringCompleted += (s, e) =>
                    {
                        completedEventArgs = e;
                        ev.Set();
                    };

                    webClient.DownloadStringAsync(uri);
                    ev.WaitOne();

                    if (completedEventArgs.Error != null)
                        throw new ApplicationException(completedEventArgs.Error.Message, completedEventArgs.Error);

                    if (completedEventArgs.Cancelled)
                        return String.Empty;

                    return completedEventArgs.Result;
                }
            }
        }

        protected bool InternalDownloadData(Uri uri, Func<FileDownloadProgressChangedEventArgs, bool> callback, Stream target)
        {
            using (AutoResetEvent ev = new AutoResetEvent(false))
            {
                using (WebClient webClient = new WebClient())
                {
                    webClient.DownloadProgressChanged += (s, e) =>
                    {
                        if (
                            (callback != null) &&
                            (!callback(new FileDownloadProgressChangedEventArgs() { BytesReceived = e.BytesReceived, ProgressPercentage = e.ProgressPercentage, TotalBytesToReceive = e.TotalBytesToReceive}))
                            )
                        {
                            webClient.CancelAsync();
                        }

                    };

                    using (Stream s = webClient.OpenRead(uri))
                    {
                        s.CopyContentsTo(target);
                        target.Position = 0;
                    }
                }
            }

            return true;
        }
    }

    public class FileDownloadProgressChangedEventArgs : EventArgs
    {
        public int ProgressPercentage { get; set; }
        public long BytesReceived { get; set;  }
        public long TotalBytesToReceive { get; set; }
    }
}
