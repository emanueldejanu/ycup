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

        private static void DrawTextProgressBar(long progress, long total)
        {
            //draw empty progress bar
            Console.CursorLeft = 0;
            Console.Write("["); //start
            Console.CursorLeft = 32;
            Console.Write("]"); //end
            Console.CursorLeft = 1;
            float onechunk = 30.0f / total;

            //draw filled part
            int position = 1;
            for (int i = 0; i < onechunk * progress; i++)
            {
                Console.BackgroundColor = ConsoleColor.Gray;
                Console.CursorLeft = position++;
                Console.Write(" ");
            }

            //draw unfilled part
            for (int i = position; i <= 31; i++)
            {
                Console.BackgroundColor = ConsoleColor.Black;
                Console.CursorLeft = position++;
                Console.Write(" ");
            }

            //draw totals
            Console.CursorLeft = 35;
            Console.BackgroundColor = ConsoleColor.Black;
            Console.Write(progress.ToString() + " of " + total.ToString() + "    "); //blanks at the end remove any excess
        }
    }
}
