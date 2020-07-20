﻿using System;
using System.Diagnostics;
using System.IO;
using System.Runtime.InteropServices;

namespace InstallerLib
{
    public static class Platform
    {
        public static RuntimePlatform ActivePlatform
        {
            get
            {
                if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
                    return RuntimePlatform.Windows;
                else if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
                    return RuntimePlatform.Linux;
                else if (RuntimeInformation.IsOSPlatform(OSPlatform.OSX))
                    return RuntimePlatform.MacOS;
                else if (RuntimeInformation.IsOSPlatform(OSPlatform.FreeBSD))
                    return RuntimePlatform.FreeBSD;
                else
                    return 0;
            }
        }

        public static string ExecutableFileExtension
        {
            get
            {
                switch (ActivePlatform)
                {
                    case RuntimePlatform.Windows:
                        return ".exe";
                    case RuntimePlatform.MacOS:
                        return ".app";
                    default:
                        return "";
                }
            }
        }

        public static void Open(FileSystemInfo fsinfo) => Open(fsinfo.FullName);

        public static void Open(string path)
        {
            switch (ActivePlatform)
            {
                case RuntimePlatform.Windows:
                    var startInfo = new ProcessStartInfo("cmd", $"/c start \"\" \"{path.Replace("&", "^&")}\"")
                    {
                        CreateNoWindow = true
                    };
                    Process.Start(startInfo);
                    break;
                case RuntimePlatform.Linux:
                    Process.Start("xdg-open", path);
                    break;
                case RuntimePlatform.MacOS:
                case RuntimePlatform.FreeBSD:
                    Process.Start("open", path);
                    break;
            }
        }
    }
}