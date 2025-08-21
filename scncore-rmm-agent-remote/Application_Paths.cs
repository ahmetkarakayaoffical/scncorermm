using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace scncore_rmm_agent_remote
{
    internal class Application_Paths
    {
        public static string program_data = Path.Combine(GetBasePath_CommonApplicationData(), "scncore", "scncore-rmm", "Remote Agent");
        public static string program_data_logs = Path.Combine(GetBasePath_CommonApplicationData(), "scncore", "scncore-rmm", "Remote Agent", "Logs");
        public static string program_data_debug_txt = Path.Combine(GetBasePath_CommonApplicationData(), "scncore", "scncore-rmm", "Remote Agent", "debug.txt");
        public static string program_data_scripts = Path.Combine(GetBasePath_CommonApplicationData(), "scncore", "scncore-rmm", "Remote Agent", "Scripts");
        public static string scncore_rmm_user_agent_path = Path.Combine(GetBasePath_ProgramFiles(), "scncore", "scncore-rmm", "User Agent", "scncore-rmm-user-process.exe");
        public static string scncore_rmm_user_agent_uac_path = Path.Combine(GetBasePath_ProgramFiles(), "scncore", "scncore-rmm", "User Agent", "scncore-rmm-user-process-uac.exe");

        private static string GetBasePath_CommonApplicationData()
        {
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            {
                return Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData);
            }
            else if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
            {
                return "/var";
            }
            else if (RuntimeInformation.IsOSPlatform(OSPlatform.OSX))
            {
                return "/Library/Application Support";
            }
            else if (OperatingSystem.IsMacOS())
            {
                return "/Library/Application Support";
            }
            else
            {
                throw new NotSupportedException("Unsupported OS");
            }
        }

        private static string GetBasePath_ProgramFiles()
        {
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            {
                return Environment.GetFolderPath(Environment.SpecialFolder.ProgramFiles);
            }
            else if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
            {
                return "/usr";
            }
            else if (RuntimeInformation.IsOSPlatform(OSPlatform.OSX))
            {
                return "/Applications";
            }
            else if (OperatingSystem.IsMacOS())
            {
                return "/Applications";
            }
            else
            {
                throw new NotSupportedException("Unsupported OS");
            }
        }
    }
}
