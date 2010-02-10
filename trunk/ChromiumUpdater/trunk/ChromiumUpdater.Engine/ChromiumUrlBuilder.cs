using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ChromiumUpdater.Engine
{
    class ChromiumUrlBuilder
    {
        const String DefaultBaseUrl = "http://build.chromium.org/buildbot/snapshots/chromium-rel-xp/";
        const String MiniInstaller = "mini_installer.exe";
        const String Latest = "LATEST";

        public ChromiumUrlBuilder() : this(ChromiumUrlBuilder.DefaultBaseUrl)
        {}

        public ChromiumUrlBuilder(String baseUrl)
        {
            if (String.IsNullOrEmpty(baseUrl))
                throw new ArgumentException(Resources.Resources.InvalidBaseUrl);

            this.BaseUrl = baseUrl;
        }

        public String BaseUrl { get; set; }

        public Uri GetUrlToVersion(String version)
        {
            UriBuilder urb = new UriBuilder(this.BaseUrl);
            urb.Path = String.Format("{0}{1}/{2}", urb.Path, version, ChromiumUrlBuilder.MiniInstaller);
            return urb.Uri;
        }
             
        public Uri GetUrlToLatestChromiumVersionDescription()
        {
            UriBuilder urb = new UriBuilder(this.BaseUrl);
            urb.Path = String.Format("{0}{1}", urb.Path, ChromiumUrlBuilder.Latest);
            return urb.Uri;
        }
    }
}
