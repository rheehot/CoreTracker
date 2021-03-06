﻿using Newtonsoft.Json;
using OpenHardwareMonitor.Hardware;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Runtime.InteropServices;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CoreTracker
{
    public partial class Form1 : Form
    {
        [DllImport("Gdi32.dll", EntryPoint = "CreateRoundRectRgn")]
        private static extern IntPtr CreateRoundRectRgn
        (
            int nLeftRect,     // x-coordinate of upper-left corner
            int nTopRect,      // y-coordinate of upper-left corner
            int nRightRect,    // x-coordinate of lower-right corner
            int nBottomRect,   // y-coordinate of lower-right corner
            int nWidthEllipse, // height of ellipse
            int nHeightEllipse // width of ellipse
        );

        private void InitializeLayout()
        {
            this.SuspendLayout();
            // 
            // Form1
            // 
            this.ClientSize = new System.Drawing.Size(299, 25);
            this.Name = "Form1";
            this.ResumeLayout(false);

        }
    }
    internal class Controller
    {
        #region "Refresh Notification Area Icons"

        [StructLayout(LayoutKind.Sequential)]
        public struct RECT
        {
            public int left;
            public int top;
            public int right;
            public int bottom;
        }

        [DllImport("user32.dll", CharSet = CharSet.Unicode)]
        public static extern IntPtr FindWindow(string lpClassName, string lpWindowName);

        [DllImport("user32.dll", CharSet = CharSet.Unicode)]
        public static extern IntPtr FindWindowEx(IntPtr hwndParent, IntPtr hwndChildAfter, string lpszClass,
            string lpszWindow);

        [DllImport("user32.dll")]
        public static extern bool GetClientRect(IntPtr hWnd, out RECT lpRect);

        [DllImport("user32.dll")]
        public static extern IntPtr SendMessage(IntPtr hWnd, uint msg, int wParam, int lParam);

        public void RefreshTrayArea()
        {
            IntPtr systemTrayContainerHandle = FindWindow("Shell_TrayWnd", null);
            IntPtr systemTrayHandle = FindWindowEx(systemTrayContainerHandle, IntPtr.Zero, "TrayNotifyWnd", null);
            IntPtr sysPagerHandle = FindWindowEx(systemTrayHandle, IntPtr.Zero, "SysPager", null);
            IntPtr notificationAreaHandle = FindWindowEx(sysPagerHandle, IntPtr.Zero, "ToolbarWindow32", "Notification Area");
            if (notificationAreaHandle == IntPtr.Zero) notificationAreaHandle = FindWindowEx(sysPagerHandle, IntPtr.Zero, "ToolbarWindow32", null);
            if (notificationAreaHandle == IntPtr.Zero)
            {
                notificationAreaHandle = FindWindowEx(sysPagerHandle, IntPtr.Zero, "ToolbarWindow32", "User Promoted Notification Area");
                IntPtr notifyIconOverflowWindowHandle = FindWindow("NotifyIconOverflowWindow", null);
                IntPtr overflowNotificationAreaHandle = FindWindowEx(notifyIconOverflowWindowHandle, IntPtr.Zero, "ToolbarWindow32", "Overflow Notification Area");
                RefreshTrayArea(overflowNotificationAreaHandle);
            }
            RefreshTrayArea(notificationAreaHandle);
        }

        private static void RefreshTrayArea(IntPtr windowHandle)
        {
            const uint wmMousemove = 0x0200;
            RECT rect;
            GetClientRect(windowHandle, out rect);
            for (var x = 0; x < rect.right; x += 5)
                for (var y = 0; y < rect.bottom; y += 5)
                    SendMessage(windowHandle, wmMousemove, 0, (y << 16) + x);
        }
        #endregion

        #region "self controller - update, restart"
        string name = System.Diagnostics.Process.GetCurrentProcess().ProcessName;
        
        public static async Task<github_result> CheckVersion()
        {
            
            github_result github_result = new github_result();
            try
            {
                using (HttpClient client = new HttpClient())
                {
                    client.DefaultRequestHeaders.Add("User-Agent", "request");
                    var response = await client.GetAsync(new Uri("https://api.github.com/repos/Fhwang0926/CoreTracker/releases/latest")).ConfigureAwait(false);
                    if (response != null)
                    {
                        dynamic jsonString = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
                        github github = JsonConvert.DeserializeObject<github>(jsonString);

                        // update result info
                        github_result.body = github.body;
                        github_result.tag_name = github.tag_name;
                        if (github.assets.Length == 0) { github_result.is_error = true; }
                        github_result.target = github.assets.FirstOrDefault().browser_download_url;
                    }
                    else { github_result.is_error = true; }

                }
            }
            catch (Exception)
            {
                github_result.is_error = true;
            }
            return github_result;
        }
        // return true when is new
        public bool checkVersion(string appVersion, string recentVersion)
        {
            List<string> av = appVersion.Split('.').ToList<string>();
            List<string> rv = recentVersion.Split('.').ToList<string>();

            // depth 1
            if (Convert.ToInt16(av[0]) < Convert.ToInt16(rv[0])) { return true; }
            if (Convert.ToInt16(av[0]) == Convert.ToInt16(rv[0])) {
                // depth 2
                if (Convert.ToInt16(av[1]) < Convert.ToInt16(rv[1])) { return true; }
                if (Convert.ToInt16(av[1]) == Convert.ToInt16(rv[1]))
                {
                    // depth 3
                    if (Convert.ToInt16(av[2]) < Convert.ToInt16(rv[2])) { return true; }
                }
            }

            return false;
        }

        public static bool downloadAsync(string url, string target)
        {
            try
            {
                using (WebClient client = new WebClient())
                {
                    try { if (File.Exists(target)) { File.Delete(target); } } catch (Exception) {  }
                    Form2 downloadForm = new Form2();
                    downloadForm.Show();
                    client.DownloadProgressChanged += new DownloadProgressChangedEventHandler(downloadForm.ProgressChanged);
                    client.DownloadFileCompleted += new AsyncCompletedEventHandler(downloadForm.Completed);
                    client.DownloadFileAsync(new Uri(url), target);
                    downloadForm.timer.Start();
                    while (!downloadForm.getDownloadStatus())
                    {
                        Application.DoEvents();
                        Thread.Sleep(100);
                    }
                    downloadForm.Close();
                    downloadForm.Dispose();
                }
                return true;
            } catch (Exception)
            {
                return false;
            }
        }

        public bool restart()
        {
            try
            {
                string cmd = Application.StartupPath + $"\\{name}.bat";

                var process = new Process
                {
                    StartInfo = new ProcessStartInfo
                    {
                        WindowStyle = ProcessWindowStyle.Hidden,
                        FileName = cmd
                    }
                };

                process.Start();
                return true;
            } catch (Exception)
            {
                return false;
            }
        }

        private bool setupRestart(string target)
        {
            try
            {
                string[] lines = { "ping 127.0.0.1 -n 2 > nul", "cd %~dp0", "echo off", "cls", "echo start update : " + name, $"taskkill /IM {name}.exe /F", "timeout 2 > NUL",  $"START /B {target}", "del %0" };
                using (var outputFile = new StreamWriter(Path.Combine(Application.StartupPath, $"{name}.bat"))) { foreach (string line in lines) { outputFile.WriteLine(line); } }
            }

            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return false;
            }
            return true;
        }

        public updateFormat startDownload(string url)
        {
            string target = $"{Application.UserAppDataPath}\\" + System.Diagnostics.Process.GetCurrentProcess().ProcessName + "_new_installer.exe";
            // start download
            if (downloadAsync(url, target))
            {
                // setup restart bat file
                if (setupRestart(target)) { return new updateFormat { msg = "download done :D, if click ok button restart program", is_error = false }; }
                else { return new updateFormat { msg = "download done :D, but is failed to setup override new version", is_error = true }; }
            }
            else
            {
                return new updateFormat { msg = "download failed, try later or check internet status", is_error = true };
            }

        }

        public async Task<updateFormat> CompareVersion(string v)
        {
            github_result rs = await CheckVersion();
            if (rs.is_error) { return new updateFormat { msg = "version check failed, try later or check internet status", is_error = true }; }

            string recentVersion = rs.tag_name.Replace("v", string.Empty);
            string appVersion = v.Replace("v", string.Empty);

            if (checkVersion(appVersion, recentVersion))
            {
                return new updateFormat { target = rs.target, msg = "Can you update the latest release version?" };
            }
            else
            {
                return new updateFormat { msg = "Recently version", latest = true };
            }
        }
        #endregion

        #region "hardware"
        public Computer computer = new Computer();
        public scoreBox sb = new scoreBox();
        public UpdateVisitor updateVisitor = new UpdateVisitor();
        public List<ISensor> cpuList;

        public void Dispose()
        {
            computer.Close();
        }

        public void hardwareMoniterInit()
        {
            computer.Open();
        }
        public void hardwareInfo()
        {
            // DEBUG CODE
            // enable or disable setting
            /*computer.CPUEnabled = true;         // default
            computer.RAMEnabled = true;         // optional
            computer.MainboardEnabled = true;   // optional
            computer.GPUEnabled = true;         // optional*/
            
            // update info
            computer.GetReport();
            computer.Accept(updateVisitor);

            foreach (IHardware h in computer.Hardware)
            {
                ISensor info = null;
                switch (h.HardwareType)
                {
                    case HardwareType.CPU:

                        // sub
                        if (!(h.SubHardware is null))
                        {
                            foreach (var s in h.SubHardware)
                            {
                                info = s.Sensors.Where(ss => ss.SensorType == SensorType.Temperature && ss.Name == "CPU Package").FirstOrDefault();
                                if (info is null) { break; }
                                sb.addCpuScore((int)info.Value);

                            }
                        }
                        // standartd
                        info = h.Sensors.Where(ss => ss.SensorType == SensorType.Temperature).FirstOrDefault();
                        if (!(info is null) && !(info.Value is null)) { sb.addCpuScore((int)info.Value); }

                        // usage by core
                        cpuList = h.Sensors.Where(ss => ss.SensorType == SensorType.Load).ToList<ISensor>();
                        //Console.WriteLine("{0} : {1}", h.HardwareType, sb.cpu_temperature);
                        break;
                    case HardwareType.GpuAti:

                        // sub
                        if (!(h.SubHardware is null))
                        {
                            foreach (var s in h.SubHardware)
                            {
                                var sc_sub = s.Sensors.Where(ss => ss.SensorType == SensorType.Temperature && ss.Name == "GPU Core").FirstOrDefault();
                                if (sc_sub is null) { break; }
                                sb.addGpuScore((int)sc_sub.Value);

                            }
                        }
                        // standartd
                        info = h.Sensors.Where(ss => ss.SensorType == SensorType.Temperature && ss.Name == "GPU Core").FirstOrDefault();
                        if (!(info is null) && !(info.Value is null)) { sb.addGpuScore((int)info.Value); }

                        // Console.WriteLine("{0} : {1}", h.HardwareType, sb.gpu_temperature);
                        break;
                    case HardwareType.GpuNvidia:

                        // sub
                        if (!(h.SubHardware is null))
                        {
                            foreach (var s in h.SubHardware)
                            {
                                info = s.Sensors.Where(ss => ss.SensorType == SensorType.Temperature && ss.Name == "GPU Core").FirstOrDefault();
                                if (info is null) { break; }
                                sb.addGpuScore((int)info.Value);

                            }
                        }
                        // standartd
                        info = h.Sensors.Where(ss => ss.SensorType == SensorType.Temperature && ss.Name == "GPU Core").FirstOrDefault();
                        if (!(info is null) && !(info.Value is null)) { sb.addGpuScore((int)info.Value); }

                        // Console.WriteLine("{0} : {1}", h.HardwareType, sb.gpu_temperature);
                        break;
                    case HardwareType.RAM:

                        // sub
                        if (!(h.SubHardware is null))
                        {
                            foreach (var s in h.SubHardware)
                            {
                                info = s.Sensors.Where(ss => ss.SensorType == SensorType.Load && ss.Name == "Memory").FirstOrDefault();
                                if (info is null) { break; }
                                sb.addRamScore((int)info.Value);

                            }
                        }
                        // standartd
                        info = h.Sensors.Where(ss => ss.SensorType == SensorType.Load && ss.Name == "Memory").FirstOrDefault();
                        if (!(info is null) && !(info.Value is null)) { sb.addRamScore((int)info.Value); }

                        // Console.WriteLine("{0} : {1}", h.HardwareType, sb.ram_usage);
                        break;
                    case HardwareType.Mainboard:

                        // sub
                        if (!(h.SubHardware is null))
                        {
                            foreach (var sh in h.SubHardware)
                            {
                                foreach (var ss in sh.Sensors.Where(ss => ss.SensorType == SensorType.Temperature).ToList())
                                {
                                    sb.addboardScore((int)(ss.Value ?? 0));
                                }
                            }
                        }
                        // standartd
                        info = h.Sensors.Where(ss => ss.SensorType == SensorType.Temperature).FirstOrDefault();
                        if (!(info is null) && !(info.Value is null)) { sb.addboardScore((int)info.Value); }

                        // Console.WriteLine("{0} : {1}", h.HardwareType, sb.board_temperature);
                        break;
                }
            }
        }
        #endregion

        public bool RestartExplorer()
        {
            try {
                exeCmd("taskkill /F /IM explorer.exe", true);
                exeCmd("start /B explorer.exe");
                return true;
            } catch (Exception e) { Console.WriteLine(e.ToString()); return false; }
            
        }

        public bool exeCmd(string cmd, bool isAdmin = false)
        {
            try
            {
                Process p = new Process();
                p.StartInfo.FileName = "cmd.exe";
                p.StartInfo.Arguments = "/C " + cmd;
                p.StartInfo.RedirectStandardOutput = false;
                p.StartInfo.UseShellExecute = false;
                if (isAdmin) { p.StartInfo.Verb = "runas"; }
                p.Start();
                p.WaitForExit();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }

    #region extenstion

    public class UpdateVisitor : IVisitor
    {
        public void VisitComputer(IComputer computer)
        {
            computer.Traverse(this);
        }
        public void VisitHardware(IHardware hardware)
        {
            hardware.Update();
            foreach (IHardware subHardware in hardware.SubHardware) subHardware.Accept(this);
        }
        public void VisitSensor(ISensor sensor) { }
        public void VisitParameter(IParameter parameter) { }
    }

    #endregion
}
