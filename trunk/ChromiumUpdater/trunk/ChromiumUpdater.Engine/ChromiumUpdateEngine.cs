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
        public IEnumerable<String> GetChromiumVersions()
        {
            Uri uri = new Uri(new ChromiumUrlBuilder().BaseUrl);
            WebClient webClient = new WebClient();
            String content = webClient.DownloadString(uri);
            HtmlDocument doc = new HtmlDocument();
            doc.LoadHtml(content);
            doc.OptionOutputAsXml = true;
            doc.OptionDefaultStreamEncoding = Encoding.UTF8;

            XDocument xdoc = null;
            using (VirtualStream vs = new VirtualStream())
            {
                doc.Save(vs);
                vs.Position = 0;
                
                using (XmlReader xr = XmlReader.Create(vs))
                {
                    xdoc = XDocument.Load(xr);
                }
            }

           

            IEnumerable<String> elements =    xdoc
                                                .Descendants()
                                                .Where(x => x.Name == "a")
                                                .Select<XElement, String>(x => x.Value.Replace("/", String.Empty))    
                                                .Where(y => y.ToCharArray().All(v => Char.IsDigit(v)));

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
            DownloadStringCompletedEventArgs completedEventArgs = null;
            using (AutoResetEvent ev = new AutoResetEvent(false))
            {
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
}
