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

                using (IChromiumUpdateEngine updateEngine = ChromiumUpdateEngineFactory.CreateInstance())
                {
                    ChromiumRegistryInfo chromiumRegistryInfo = updateEngine.GetChromiumRegistryInfo();
                    String latestVersion = updateEngine.GetChromiumLatestVersionString();
                    Log changeLog = updateEngine.GetChromiumVersionChangeLogData(latestVersion);

                    Console.WriteLine(AppResources.LatestChromiumVersion, latestVersion);
                    Console.WriteLine(AppResources.CurrentInstalledChromiumVersion, chromiumRegistryInfo != null ? chromiumRegistryInfo.ToString() : AppResources.None);

                    Console.WriteLine(AppResources.DownloadingChromium, latestVersion);

                    String targetFileName = null;
                    updateEngine.DownloadChromiumInstaller(Path.GetTempPath(), latestVersion, false, (x) =>
                    {
                        if (x.FileDownloadState == FileDownloadState.Starting)
                            targetFileName = x.FileName;


                        if (x.ProgressPercentage % 10 == 0)
                            Console.Write(AppResources.DownloadProgress, x.ProgressPercentage, x.BytesReceived, x.TotalBytesToReceive);

                        return true;
                    });

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
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(AppResources.Error, ex.Message);
            }
        }
    }
}
