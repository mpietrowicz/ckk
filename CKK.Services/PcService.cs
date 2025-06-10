using CKK.Abstraction;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CKK.Services
{
    public class PcService : IPcService
    {
        public async Task ShutdownPc()
        {
            using Process process = new Process();
            process.StartInfo.FileName = "shutdown";
            process.StartInfo.Arguments = "/s /t 0"; // Shutdown immediately
            process.StartInfo.UseShellExecute = false;
            process.StartInfo.RedirectStandardOutput = true;
            process.StartInfo.RedirectStandardError = true;
            process.StartInfo.CreateNoWindow = true;
            process.Start();
            await process.WaitForExitAsync(); // Wait for the shutdown command to complete
            if (process.ExitCode != 0)
            {
                string error = await process.StandardError.ReadToEndAsync();
                throw new Exception($"Shutdown failed: {error}");
            }

        }

        public async Task RestartPc()
        {
            using Process process = new Process();
            process.StartInfo.FileName = "shutdown";
            process.StartInfo.Arguments = "/r /t 0"; // Restart immediately
            process.StartInfo.UseShellExecute = false;
            process.StartInfo.RedirectStandardOutput = true;
            process.StartInfo.RedirectStandardError = true;
            process.StartInfo.CreateNoWindow = true;
            process.Start();
            await process.WaitForExitAsync(); // Wait for the restart command to complete
            if (process.ExitCode != 0)
            {
                string error = await process.StandardError.ReadToEndAsync();
                throw new Exception($"Restart failed: {error}");
            }
        }

        public async Task LogOffPc()
        {
            using Process process = new Process();
            process.StartInfo.FileName = "shutdown";
            process.StartInfo.Arguments = "/l"; // Log off
            process.StartInfo.UseShellExecute = false;
            process.StartInfo.RedirectStandardOutput = true;
            process.StartInfo.RedirectStandardError = true;
            process.StartInfo.CreateNoWindow = true;
            process.Start();
            await process.WaitForExitAsync(); // Wait for the logoff command to complete
            if (process.ExitCode != 0)
            {
                string error = await process.StandardError.ReadToEndAsync();
                throw new Exception($"Log off failed: {error}");
            }
        }

        public async Task LockPc()
        {
            using Process process = new Process();
            process.StartInfo.FileName = "rundll32.exe";
            process.StartInfo.Arguments = "user32.dll,LockWorkStation"; // Lock the workstation
            process.StartInfo.UseShellExecute = false;
            process.StartInfo.RedirectStandardOutput = true;
            process.StartInfo.RedirectStandardError = true;
            process.StartInfo.CreateNoWindow = true;
            process.Start();
            await process.WaitForExitAsync(); // Wait for the lock command to complete
            if (process.ExitCode != 0)
            {
                string error = await process.StandardError.ReadToEndAsync();
                throw new Exception($"Lock failed: {error}");
            }
        }

        public async Task SleepPc()
        {
            using Process process = new Process();
            process.StartInfo.FileName = "rundll32.exe";
            process.StartInfo.Arguments = "powrprof.dll,SetSuspendState 0,1,0"; // Put the computer to sleep
            process.StartInfo.UseShellExecute = false;
            process.StartInfo.RedirectStandardOutput = true;
            process.StartInfo.RedirectStandardError = true;
            process.StartInfo.CreateNoWindow = true;
            process.Start();
            await process.WaitForExitAsync(); // Wait for the sleep command to complete
            if (process.ExitCode != 0)
            {
                string error = await process.StandardError.ReadToEndAsync();
                throw new Exception($"Sleep failed: {error}");
            }
        }

        public async Task HibernatePc()
        {
            using Process process = new Process();
            process.StartInfo.FileName = "rundll32.exe";
            process.StartInfo.Arguments = "powrprof.dll,SetSuspendState Hibernate"; // Put the computer into hibernation
            process.StartInfo.UseShellExecute = false;
            process.StartInfo.RedirectStandardOutput = true;
            process.StartInfo.RedirectStandardError = true;
            process.StartInfo.CreateNoWindow = true;
            process.Start();
            await process.WaitForExitAsync(); // Wait for the hibernate command to complete
            if (process.ExitCode != 0)
            {
                string error = await process.StandardError.ReadToEndAsync();
                throw new Exception($"Hibernate failed: {error}");
            }
        }


    }
}
