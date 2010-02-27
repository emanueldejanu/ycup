using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ChromiumUpdater.Engine.Schemas;
using System.IO;

namespace ChromiumUpdater.Engine
{
    public interface IChromiumUpdateEngine : IDisposable
    {
        void DownloadChromiumInstaller(String folder, String version, bool appendVersionToFileName, Func<FileDownloadProgressChangedEventArgs, bool> callback);
        IEnumerable<String> GetChromiumVersions();
        Log GetChromiumVersionChangeLogData(String version);
        String GetChromiumLatestVersionString();
    }
}
