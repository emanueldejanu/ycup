using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ChromiumUpdater.Ui.Text.Resources;
using ChromiumUpdater.Engine;
using ChromiumUpdater.Engine.ProcessManagement;
using System.IO;
using ChromiumUpdater.Engine.Schemas;

namespace ChromiumUpdater.Ui.Text
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                Console.WriteLine(AppResources.ApplicationName);

                using (IChromiumUpdateEngine updateEngine = ChromiumUpdateEngineFactory.CreateInstance(new ChromiumUpdateEngineConfiguration() {  WebProxyType = ProxyType.None }))
                {
                    ChromiumRegistryInfo chromiumRegistryInfo = updateEngine.GetChromiumRegistryInfo();
                    String latestVersion = updateEngine.GetChromiumLatestVersionString();
                    changelogs releaseChangeLog = updateEngine.GetChromiumVersionChangeLogData(latestVersion);

                    Console.WriteLine(AppResources.LatestChromiumVersion, latestVersion);
                    Console.WriteLine(AppResources.CurrentInstalledChromiumVersion, chromiumRegistryInfo != null ? chromiumRegistryInfo.ToString() : AppResources.None);

                    Console.WriteLine(AppResources.DownloadingChromium, latestVersion);

                    String targetFileName = null;
                    updateEngine.DownloadChromiumInstaller(Path.GetTempPath(), latestVersion, false, (x) =>
                    {
                        if (x.FileDownloadState == FileDownloadState.Starting)
                            targetFileName = x.FileName;

                        switch (x.FileDownloadState)
                        {
                            case FileDownloadState.Starting:
                                break;

                            case FileDownloadState.Downloading:
                                DrawTextProgressBar(x.BytesReceived, x.TotalBytesToReceive);
                                break;

                            case FileDownloadState.Completed:
                                break;

                            default:
                                break;
                        }

                        return true;
                    });

                    Console.WriteLine();
                    Console.WriteLine(AppResources.LaunchingInstaller);
                    Console.WriteLine();

                    using (ProcessLauncher processLauncher = new ProcessLauncher())
                    {
                        processLauncher.FileName = targetFileName;
                        if (!processLauncher.Start(true, TimeSpan.FromSeconds(60 * 2)))
                        {
                            Console.WriteLine(AppResources.ChildProcessFailure);
                        }
                    }

                    ChromiumRegistryInfo updatedChromiumRegistryInfo = updateEngine.GetChromiumRegistryInfo();

                    if (updatedChromiumRegistryInfo != null)
                    {
                        Console.WriteLine(updatedChromiumRegistryInfo.ToString());
                    }

                    Console.WriteLine(AppResources.Done);


                    if (releaseChangeLog != null)
                    {
                        Console.WriteLine(releaseChangeLog.ConcatenatedText);
                        Console.WriteLine();

                        releaseChangeLog.log.ForEach(x =>
                        {
                            Console.WriteLine("{0}: {1}@{2}", x.revision, x.author.Trim(), x.date);
                            Console.WriteLine("{0}", x.msg.Trim());
                        }
                        );
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(AppResources.Error, ex.Message);
            }
            
        }
        
        private static void DrawTextProgressBar(long progress, long total)
        {
            String v = (progress * 100 / total).ToString();
            String r = String.Format(AppResources.DownloadProgress, v, progress / 1024, total / 1024);
            Console.Write("\r"+ r);
        }
    }
}
