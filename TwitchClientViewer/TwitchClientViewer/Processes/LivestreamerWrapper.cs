using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TwitchClientViewer.Helpers;

namespace TwitchClientViewer.Processes
{
    public class LivestreamerWrapper
    {
        private const string livestreamerPath = @"ExternalLibs\livestreamer-v1.12.2";
        private const string livestreamerExePath = @"ExternalLibs\livestreamer-v1.12.2\livestreamer.exe";

        private const string vlcPath = @"ExternalLibs\vlc-2.1.5";
        private const string vlcRelativePath = @"ExternalLibs\vlc-2.1.5";
        private const string vlcExePath = @"ExternalLibs\vlc-2.1.5\vlc.exe";

        private const string httpPortOption = "--player-external-http-port";
        private const int httpPort = 62980;

        Process process;


        public void Start(string channel, LivestreamerOptions options)
        {
            switch (options)
            {
                case LivestreamerOptions.Player:
                    StartPlayerStreaming(channel);
                    break;
                case LivestreamerOptions.File:
                    StartFileStreaming(channel);
                    break;
                case LivestreamerOptions.Http:
                    StartHttpStreaming(channel);
                    break;
                default:
                    throw new ArgumentException("Invalid argument 'options' specified.");
            }
        }

        private void StartPlayerStreaming(string channel)
        {
            if (!File.Exists(livestreamerExePath)) throw new FileNotFoundException(@"Unable to find the required file " + livestreamerExePath);
            if (!File.Exists(vlcExePath)) throw new FileNotFoundException(@"Unable to find the required file " + vlcExePath);

            string[] livestreamerOptions = { LivestreamerOptions.Player.GetStringValue(), vlcExePath.ToString() };
            string[] twitchOptions = { "\"", channel, "\"", "source" };

            string command = string.Concat(livestreamerOptions, twitchOptions);

            this.CreateTempBat(command);
            this.CreateAndStartProcess(command);
        }

        private void StartFileStreaming(string channel)
        {
            throw new NotImplementedException("feature not implemented");
        }

        private void StartHttpStreaming(string channel)
        {
            if (!File.Exists(livestreamerExePath)) throw new FileNotFoundException(@"Unable to find the required file 'ExternalLibs\livestreamer-v1.12.2\livestreamer.exe'");

            string[] livestreamerOptions = { LivestreamerOptions.Http.GetStringValue(), httpPortOption , httpPort.ToString() };
            string[] twitchOptions = { "\""+channel+"\"", "source" };


            string ls = string.Join(" ", livestreamerOptions);
            string to = string.Join(" ", twitchOptions);

            string command = string.Concat(ls, " ", to);
            command = string.Concat(livestreamerExePath, " ", command);

            this.CreateTempBat(command);
            this.CreateAndStartProcess(command);
        }



        private void CreateTempBat(string command)
        {
            if (File.Exists("executor.bat"))
            {
                File.Delete("executor.bat");
            }

            StreamWriter streamWriter = new StreamWriter("executor.bat", false);
            try
            {
                streamWriter.WriteLine("@echo");
                streamWriter.WriteLine(command);
                streamWriter.WriteLine("@echo off");
            }
            finally
            {
                if (streamWriter != null)
                {
                    ((IDisposable)streamWriter).Dispose();
                }
            }
        }

        private void CreateAndStartProcess(string command)
        {
            if (process != null )
            {
                process.Close();
            }

            ProcessStartInfo processStartInfo = new ProcessStartInfo()
            {
                CreateNoWindow = true,
                FileName = "executor.bat",
                UseShellExecute = false
            };

            process = new Process();
            process.StartInfo = processStartInfo;
            process.Start();
            process.EnableRaisingEvents = true;
            process.Exited += new EventHandler((object proc, EventArgs processEa) => AutoClosingMessageBox.Show("Stream Ended or Streamer is offline", "End", 2750));
        }
    }
}
