using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace NuKeeperService
{
    public static class ShellExtensions
    {
        public static string Command(this string cmd)
        {
            ProcessStartInfo procStartInfo = new ProcessStartInfo("cmd", "/c " + @cmd) //TODO should be bash when run inside docker
            {
                RedirectStandardOutput = true,
                RedirectStandardError = true,
                UseShellExecute = false,
                CreateNoWindow = true
            };
            
            using (var process = new Process())
            {
                process.StartInfo = procStartInfo;
                process.Start();
                
                process.WaitForExit();
                
                string result = process.StandardOutput.ReadToEnd();
                return result;
            }
        }
    }
}
