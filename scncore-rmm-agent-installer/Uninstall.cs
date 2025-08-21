using Helper;
using MacOS.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace scncore_rmm_agent_installer
{
    internal class Uninstall
    {
        public static void Clean()
        {
            // Stop services
            Console.WriteLine("[" + DateTime.Now + "] - [Main] -> Stopping services.");
            if (OperatingSystem.IsWindows())
            {
                Logging.Handler.Debug("Main", "Stopping services.", "scncore-rmm-agent-comm");
                Helper.Service.Stop("scncore-rmm-agent-comm");
                Logging.Handler.Debug("Main", "Stopping services.", "scncore-rmm-agent-remote");
                Helper.Service.Stop("scncore-rmm-agent-remote");
                Logging.Handler.Debug("Main", "Stopping services.", "scncore-rmm-agent-health");
                Helper.Service.Stop("scncore-rmm-agent-health");

                // For legacy installations (2.0.0.0)
                Logging.Handler.Debug("Main", "Stopping services (legacy).", "scncore-rmm-comm-agent-windows");
                Helper.Service.Stop("scncore-rmm-comm-agent-windows");
                Logging.Handler.Debug("Main", "Stopping services (legacy).", "scncore-rmm-health-agent-windows");
                Helper.Service.Stop("scncore-rmm-health-agent-windows");
                Logging.Handler.Debug("Main", "Stopping services (legacy).", "scncore-rmm-remote-agent-windows");
                Helper.Service.Stop("scncore-rmm-remote-agent-windows");
            }
            else if (OperatingSystem.IsLinux())
            {
                Logging.Handler.Debug("Main", "Stopping services.", "scncore-rmm-agent-comm");
                Bash.Execute_Script("Stopping services", false, "systemctl stop scncore-rmm-agent-comm");
                Logging.Handler.Debug("Main", "Stopping services.", "scncore-rmm-agent-remote");
                Bash.Execute_Script("Stopping services", false, "systemctl stop scncore-rmm-agent-remote");
                Logging.Handler.Debug("Main", "Stopping services.", "scncore-rmm-agent-health");
                Bash.Execute_Script("Stopping services", false, "systemctl stop scncore-rmm-agent-health");
            }
            else if (OperatingSystem.IsMacOS())
            {
                Logging.Handler.Debug("Main", "Stopping services.", Application_Paths.program_files_comm_agent_service_name_osx);
                Zsh.Execute_Script("Stopping services", false, $"launchctl stop {Application_Paths.program_files_comm_agent_service_name_osx}");
                Logging.Handler.Debug("Main", "Stopping services.", Application_Paths.program_files_remote_agent_service_name_osx);
                Zsh.Execute_Script("Stopping services", false, $"launchctl stop {Application_Paths.program_files_remote_agent_service_name_osx}");
                Logging.Handler.Debug("Main", "Stopping services.", Application_Paths.program_files_health_agent_service_name_osx);
                Zsh.Execute_Script("Stopping services", false, $"launchctl stop {Application_Paths.program_files_health_agent_service_name_osx}");
            }
            Console.WriteLine("[" + DateTime.Now + "] - [Main] -> Services stopped.");

            // Kill processes
            Console.WriteLine("[" + DateTime.Now + "] - [Main] -> Terminating processes.");

            if (OperatingSystem.IsWindows())
            {
                Logging.Handler.Debug("Main", "Terminating processes.", "scncore-rmm-agent-comm.exe");
                Helper._Process.Start("cmd.exe", "/c taskkill /F /IM \"scncore-rmm-agent-comm.exe\"");
                Logging.Handler.Debug("Main", "Terminating processes.", "scncore-rmm-agent-remote.exe");
                Helper._Process.Start("cmd.exe", "/c taskkill /F /IM \"scncore-rmm-agent-remote.exe\"");
                Logging.Handler.Debug("Main", "Terminating processes.", "scncore-rmm-agent-health.exe");
                Helper._Process.Start("cmd.exe", "/c taskkill /F /IM \"scncore-rmm-agent-health.exe\"");
                Logging.Handler.Debug("Main", "Terminating processes.", "scncore-rmm-user-process.exe");
                Helper._Process.Start("cmd.exe", "/c taskkill /F /IM \"scncore-rmm-user-process.exe\"");
                Logging.Handler.Debug("Main", "Terminating processes.", "scncore-rmm-user-process-uac.exe");
                Helper._Process.Start("cmd.exe", "/c taskkill /F /IM \"scncore-rmm-user-process-uac.exe\"");
                Logging.Handler.Debug("Main", "Terminating processes.", "scncore-rmm-user-process.exe"); // kill legacy process
                Helper._Process.Start("cmd.exe", "/c taskkill /F /IM \"scncore-rmm-user-process.exe\""); // kill legacy process

                Thread.Sleep(5000); // Wait a little to allow service manager to release handles to prevent service marked for deletion error

                //Helper._Process.Start("cmd.exe", "/c taskkill /F /IM \"yara64.exe\""); // yara64.exe is (currently) not used in the project, its part of a netlock legacy feature
                //Helper._Process.Start("cmd.exe", "/c taskkill /F /IM \"devcon_x64.exe\""); // devcon_x64.exe is (currently) not used in the project, its part of a netlock legacy feature
            }
            else if (OperatingSystem.IsLinux())
            {
                Logging.Handler.Debug("Main", "Terminating processes.", "scncore-rmm-agent-comm");
                Bash.Execute_Script("Terminating processes", false, "pkill -f scncore-rmm-agent-comm");
                Logging.Handler.Debug("Main", "Terminating processes.", "scncore-rmm-agent-remote");
                Bash.Execute_Script("Terminating processes", false, "pkill -f scncore-rmm-agent-remote");
                Logging.Handler.Debug("Main", "Terminating processes.", "scncore-rmm-agent-health");
                Bash.Execute_Script("Terminating processes", false, "pkill -f scncore-rmm-agent-health");
                Logging.Handler.Debug("Main", "Terminating processes.", "scncore-rmm-user-process");
                Bash.Execute_Script("Terminating processes", false, "pkill -f scncore-rmm-user-process");
            }
            else if (OperatingSystem.IsMacOS())
            {
                Logging.Handler.Debug("Main", "Terminating processes.", "scncore-rmm-agent-comm");
                Zsh.Execute_Script("Terminating processes", false, "pkill scncore-rmm-agent-comm");
                Logging.Handler.Debug("Main", "Terminating processes.", "scncore-rmm-agent-remote");
                Zsh.Execute_Script("Terminating processes", false, "pkill scncore-rmm-agent-remote");
                Logging.Handler.Debug("Main", "Terminating processes.", "scncore-rmm-agent-health");
                Zsh.Execute_Script("Terminating processes", false, "pkill scncore-rmm-agent-health");
                Logging.Handler.Debug("Main", "Terminating processes.", "scncore-rmm-user-process");
                Zsh.Execute_Script("Terminating processes", false, "pkill scncore-rmm-user-process");
            }
            Console.WriteLine("[" + DateTime.Now + "] - [Main] -> Terminated processes.");

            // Delete services
            Console.WriteLine("[" + DateTime.Now + "] - [Main] -> Deleting services.");
            if (OperatingSystem.IsWindows())
            {
                Logging.Handler.Debug("Main", "Deleting services.", "scncore-rmm-agent-comm");
                Helper._Process.Start("cmd.exe", "/c sc delete scncore-rmm-agent-comm");
                Logging.Handler.Debug("Main", "Deleting services.", "scncore-rmm-agent-remote");
                Helper._Process.Start("cmd.exe", "/c sc delete scncore-rmm-agent-remote");
                Logging.Handler.Debug("Main", "Deleting services.", "scncore-rmm-agent-health");
                Helper._Process.Start("cmd.exe", "/c sc delete scncore-rmm-agent-health");

                // Unregister user process
                Console.WriteLine("[" + DateTime.Now + "] - [Main] -> Unregistering user process.");
                Logging.Handler.Debug("Main", "Unregistering user process", "scncore-rmm-user-process");
                Windows.Helper.Registry.HKLM_Delete_Value(Application_Paths.hklm_run_directory_reg_path, "scncore-rmm-user-process");
            }
            else if (OperatingSystem.IsLinux())
            {
                Logging.Handler.Debug("Main", "Deleting services.", Application_Paths.program_files_comm_agent_service_name_linux);
                Bash.Execute_Script("Stopping service", false, $"systemctl stop {Application_Paths.program_files_comm_agent_service_name_linux} || true");
                Bash.Execute_Script("Disabling service", false, $"systemctl disable {Application_Paths.program_files_comm_agent_service_name_linux} || true");
                Bash.Execute_Script("Removing service file", false, $"rm -f /etc/systemd/system/{Application_Paths.program_files_comm_agent_service_name_linux}.service");

                Logging.Handler.Debug("Main", "Deleting services.", Application_Paths.program_files_remote_agent_service_name_linux);
                Bash.Execute_Script("Stopping service", false, $"systemctl stop {Application_Paths.program_files_remote_agent_service_name_linux} || true");
                Bash.Execute_Script("Disabling service", false, $"systemctl disable {Application_Paths.program_files_remote_agent_service_name_linux} || true");
                Bash.Execute_Script("Removing service file", false, $"rm -f /etc/systemd/system/{Application_Paths.program_files_remote_agent_service_name_linux}.service");

                Logging.Handler.Debug("Main", "Deleting services.", Application_Paths.program_files_health_agent_service_name_linux);
                Bash.Execute_Script("Stopping service", false, $"systemctl stop {Application_Paths.program_files_health_agent_service_name_linux} || true");
                Bash.Execute_Script("Disabling service", false, $"systemctl disable {Application_Paths.program_files_health_agent_service_name_linux} || true");
                Bash.Execute_Script("Removing service file", false, $"rm -f /etc/systemd/system/{Application_Paths.program_files_health_agent_service_name_linux}.service");

                // Reload Systemd to remove the deleted services
                Bash.Execute_Script("Reloading systemd", false, "systemctl daemon-reload");
            }
            else if (OperatingSystem.IsMacOS())
            {
                // Unload the service
                Logging.Handler.Debug("Main", "Unload service.", Application_Paths.program_files_comm_agent_service_config_path_osx);
                Zsh.Execute_Script("Unload service", false, $"launchctl unload {Application_Paths.program_files_comm_agent_service_config_path_osx}");

                // Delete the service file
                Logging.Handler.Debug("Main", "Deleting service file.", Application_Paths.program_files_comm_agent_service_config_path_osx);
                Zsh.Execute_Script("Deleting service file", false, $"rm -f {Application_Paths.program_files_comm_agent_service_config_path_osx}");

                // Unload the service
                Logging.Handler.Debug("Main", "Unload service.", Application_Paths.program_files_remote_agent_service_config_path_osx);
                Zsh.Execute_Script("Unload service", false, $"launchctl unload {Application_Paths.program_files_remote_agent_service_config_path_osx}");

                // Delete the service file
                Logging.Handler.Debug("Main", "Deleting service file.", Application_Paths.program_files_remote_agent_service_config_path_osx);
                Zsh.Execute_Script("Deleting service file", false, $"rm -f {Application_Paths.program_files_remote_agent_service_config_path_osx}");

                // Unload the service
                Logging.Handler.Debug("Main", "Unload service.", Application_Paths.program_files_health_agent_service_config_path_osx);
                Zsh.Execute_Script("Unload service", false, $"launchctl unload {Application_Paths.program_files_health_agent_service_config_path_osx}");

                // Delete the service file
                Logging.Handler.Debug("Main", "Deleting service file.", Application_Paths.program_files_health_agent_service_config_path_osx);
                Zsh.Execute_Script("Deleting service file", false, $"rm -f {Application_Paths.program_files_health_agent_service_config_path_osx}");
            }
            Console.WriteLine("[" + DateTime.Now + "] - [Main] -> Services deleted.");

            // Delete directories & logs
            Console.WriteLine("[" + DateTime.Now + "] - [Main] -> Deleting directories.");
            Logging.Handler.Debug("Main", "Deleting directories.", Application_Paths.program_files_0x101_cyber_security_dir);
            Helper.IO.Delete_Directory(Application_Paths.program_files_0x101_cyber_security_dir);
            Logging.Handler.Debug("Main", "Deleting directories.", Application_Paths.program_data_0x101_cyber_security_dir);
            Helper.IO.Delete_Directory(Application_Paths.program_data_0x101_cyber_security_dir);

            // Delete logs
            if (OperatingSystem.IsLinux() || OperatingSystem.IsMacOS())
            {
                // Delete comm agent service log
                Logging.Handler.Debug("Main", "Deleting comm agent service log.", Application_Paths.program_files_comm_agent_service_log_path_unix);
                Console.WriteLine("[" + DateTime.Now + "] - [Main] -> Deleting comm agent service log: " + Application_Paths.program_files_comm_agent_service_log_path_unix);
                Bash.Execute_Script("Deleting comm agent service log", false, $"rm -f {Application_Paths.program_files_comm_agent_service_log_path_unix}");

                // Delete remote agent service log
                Logging.Handler.Debug("Main", "Deleting remote agent service log.", Application_Paths.program_files_remote_agent_service_log_path_unix);
                Console.WriteLine("[" + DateTime.Now + "] - [Main] -> Deleting remote agent service log: " + Application_Paths.program_files_remote_agent_service_log_path_unix);
                Bash.Execute_Script("Deleting remote agent service log", false, $"rm -f {Application_Paths.program_files_remote_agent_service_log_path_unix}");

                // Delete health agent service log
                Logging.Handler.Debug("Main", "Deleting health agent service log.", Application_Paths.program_files_health_agent_service_log_path_unix);
                Console.WriteLine("[" + DateTime.Now + "] - [Main] -> Deleting health agent service log: " + Application_Paths.program_files_health_agent_service_log_path_unix);
                Bash.Execute_Script("Deleting health agent service log", false, $"rm -f {Application_Paths.program_files_health_agent_service_log_path_unix}");
            }

            Console.WriteLine("[" + DateTime.Now + "] - [Main] -> Directories deleted.");
        }

        public static void Fix()
        {
            Console.WriteLine("[" + DateTime.Now + "] - [Main] -> Fix mode.");

            // Stop services
            Console.WriteLine("[" + DateTime.Now + "] - [Main] -> Stopping services.");
            if (OperatingSystem.IsWindows())
            {
                Logging.Handler.Debug("Main", "Stopping services.", "scncore-rmm-agent-comm");
                Helper.Service.Stop("scncore-rmm-agent-comm");
                Logging.Handler.Debug("Main", "Stopping services.", "scncore-rmm-agent-remote");
                Helper.Service.Stop("scncore-rmm-agent-remote");

                // For legacy installations (2.0.0.0)
                Logging.Handler.Debug("Main", "Stopping services (legacy).", "scncore-rmm-comm-agent-windows");
                Helper.Service.Stop("scncore-rmm-comm-agent-windows");
                Logging.Handler.Debug("Main", "Stopping services (legacy).", "scncore-rmm-health-agent-windows");
                Helper.Service.Stop("scncore-rmm-health-agent-windows");
                Logging.Handler.Debug("Main", "Stopping services (legacy).", "scncore-rmm-remote-agent-windows");
                Helper.Service.Stop("scncore-rmm-remote-agent-windows");
            }
            else if (OperatingSystem.IsLinux())
            {
                Logging.Handler.Debug("Main", "Stopping services.", "scncore-rmm-agent-comm");
                Bash.Execute_Script("Stopping services", false, "systemctl stop scncore-rmm-agent-comm");
                Logging.Handler.Debug("Main", "Stopping services.", "scncore-rmm-agent-remote");
                Bash.Execute_Script("Stopping services", false, "systemctl stop scncore-rmm-agent-remote");
            }
            else if (OperatingSystem.IsMacOS())
            {
                Logging.Handler.Debug("Main", "Stopping services.", Application_Paths.program_files_comm_agent_service_name_osx);
                Zsh.Execute_Script("Stopping services", false, $"launchctl stop {Application_Paths.program_files_comm_agent_service_name_osx}");
                Logging.Handler.Debug("Main", "Stopping services.", Application_Paths.program_files_remote_agent_service_name_osx);
                Zsh.Execute_Script("Stopping services", false, $"launchctl stop {Application_Paths.program_files_remote_agent_service_name_osx}");
            }

            Console.WriteLine("[" + DateTime.Now + "] - [Main] -> Services stopped.");

            // Wait a little to allow service manager to release handles to prevent service marked for deletion error
            Thread.Sleep(5000);

            // Kill processes
            Console.WriteLine("[" + DateTime.Now + "] - [Main] -> Terminating processes.");
            if (OperatingSystem.IsWindows())
            {
                Logging.Handler.Debug("Main", "Terminating processes.", "scncore-rmm-agent-comm.exe");
                Helper._Process.Start("cmd.exe", "/c taskkill /F /IM \"scncore-rmm-agent-comm.exe\"");
                Logging.Handler.Debug("Main", "Terminating processes.", "scncore-rmm-agent-remote.exe");
                Helper._Process.Start("cmd.exe", "/c taskkill /F /IM \"scncore-rmm-agent-remote.exe\"");
                Logging.Handler.Debug("Main", "Terminating processes.", "scncore-rmm-user-process.exe");
                Helper._Process.Start("cmd.exe", "/c taskkill /F /IM \"scncore-rmm-user-process.exe\"");
                Logging.Handler.Debug("Main", "Terminating processes.", "scncore-rmm-user-process-uac.exe");
                Helper._Process.Start("cmd.exe", "/c taskkill /F /IM \"scncore-rmm-user-process-uac.exe\"");
                Logging.Handler.Debug("Main", "Terminating processes.", "scncore-rmm-user-process.exe"); // kill legacy process
                Helper._Process.Start("cmd.exe", "/c taskkill /F /IM \"scncore-rmm-user-process.exe\""); // kill legacy process
                //Helper._Process.Start("cmd.exe", "/c taskkill /F /IM \"yara64.exe\""); // yara64.exe is (currently) not used in the project, its part of a netlock legacy feature
                //Helper._Process.Start("cmd.exe", "/c taskkill /F /IM \"devcon_x64.exe\""); // devcon_x64.exe is (currently) not used in the project, its part of a netlock legacy feature

                Thread.Sleep(5000); // Wait a little to allow service manager to release handles to prevent service marked for deletion error
            }
            else if (OperatingSystem.IsLinux())
            {
                Logging.Handler.Debug("Main", "Terminating processes.", "scncore-rmm-agent-comm");
                Bash.Execute_Script("Terminating processes", false, "pkill -f scncore-rmm-agent-comm");
                Logging.Handler.Debug("Main", "Terminating processes.", "scncore-rmm-agent-remote");
                Bash.Execute_Script("Terminating processes", false, "pkill -f scncore-rmm-agent-remote");
                Logging.Handler.Debug("Main", "Terminating processes.", "scncore-rmm-user-process");
                Bash.Execute_Script("Terminating processes", false, "pkill -f scncore-rmm-user-process");
            }
            else if (OperatingSystem.IsMacOS())
            {
                Logging.Handler.Debug("Main", "Terminating processes.", "scncore-rmm-agent-comm");
                Zsh.Execute_Script("Terminating processes", false, "pkill -f scncore-rmm-agent-comm");
                Logging.Handler.Debug("Main", "Terminating processes.", "scncore-rmm-agent-remote");
                Zsh.Execute_Script("Terminating processes", false, "pkill -f scncore-rmm-agent-remote");
                Logging.Handler.Debug("Main", "Terminating processes.", "scncore-rmm-agent-health");
                Zsh.Execute_Script("Terminating processes", false, "pkill -f scncore-rmm-agent-health");
            }
            Console.WriteLine("[" + DateTime.Now + "] - [Main] -> Terminated processes.");

            // Delete services
            Console.WriteLine("[" + DateTime.Now + "] - [Main] -> Deleting services.");
            if (OperatingSystem.IsWindows())
            {
                Logging.Handler.Debug("Main", "Deleting services.", "scncore-rmm-agent-comm");
                Helper._Process.Start("cmd.exe", "/c sc delete scncore-rmm-agent-comm");
                Logging.Handler.Debug("Main", "Deleting services.", "scncore-rmm-agent-remote");
                Helper._Process.Start("cmd.exe", "/c sc delete scncore-rmm-agent-remote");
            }
            else if (OperatingSystem.IsLinux())
            {
                Logging.Handler.Debug("Main", "Deleting services.", Application_Paths.program_files_comm_agent_service_name_linux);
                Bash.Execute_Script("Stopping service", false, $"systemctl stop {Application_Paths.program_files_comm_agent_service_name_linux} || true");
                Bash.Execute_Script("Disabling service", false, $"systemctl disable {Application_Paths.program_files_comm_agent_service_name_linux} || true");
                Bash.Execute_Script("Removing service file", false, $"rm -f /etc/systemd/system/{Application_Paths.program_files_comm_agent_service_name_linux}.service");

                Logging.Handler.Debug("Main", "Deleting services.", Application_Paths.program_files_remote_agent_service_name_linux);
                Bash.Execute_Script("Stopping service", false, $"systemctl stop {Application_Paths.program_files_remote_agent_service_name_linux} || true");
                Bash.Execute_Script("Disabling service", false, $"systemctl disable {Application_Paths.program_files_remote_agent_service_name_linux} || true");
                Bash.Execute_Script("Removing service file", false, $"rm -f /etc/systemd/system/{Application_Paths.program_files_remote_agent_service_name_linux}.service");

                // Reload Systemd to remove the deleted services
                Bash.Execute_Script("Reloading systemd", false, "systemctl daemon-reload");
            }
            else if (OperatingSystem.IsMacOS())
            {
                // Unload the service
                Logging.Handler.Debug("Main", "Unload service.", Application_Paths.program_files_comm_agent_service_config_path_osx);
                Zsh.Execute_Script("Unload service", false, $"launchctl unload {Application_Paths.program_files_comm_agent_service_config_path_osx}");

                // Delete the service file
                Logging.Handler.Debug("Main", "Deleting service file.", Application_Paths.program_files_comm_agent_service_config_path_osx);
                Zsh.Execute_Script("Deleting service file", false, $"rm -f {Application_Paths.program_files_comm_agent_service_config_path_osx}");

                // Unload the service
                Logging.Handler.Debug("Main", "Unload service.", Application_Paths.program_files_remote_agent_service_config_path_osx);
                Zsh.Execute_Script("Unload service", false, $"launchctl unload {Application_Paths.program_files_remote_agent_service_config_path_osx}");

                // Delete the service file
                Logging.Handler.Debug("Main", "Deleting service file.", Application_Paths.program_files_remote_agent_service_config_path_osx);
                Zsh.Execute_Script("Deleting service file", false, $"rm -f {Application_Paths.program_files_remote_agent_service_config_path_osx}");
            }
            Console.WriteLine("[" + DateTime.Now + "] - [Main] -> Services deleted.");

            // Delete files
            Console.WriteLine("[" + DateTime.Now + "] - [Main] -> Deleting files.");
            Logging.Handler.Debug("Main", "Deleting files.", Application_Paths.program_data_comm_agent_events_path);
            Helper.IO.Delete_File(Application_Paths.program_data_comm_agent_events_path);
            Logging.Handler.Debug("Main", "Deleting files.", Application_Paths.program_data_comm_agent_policies_path);
            Helper.IO.Delete_File(Application_Paths.program_data_comm_agent_policies_path);
            Logging.Handler.Debug("Main", "Deleting files.", Application_Paths.program_data_comm_agent_server_config);
            Helper.IO.Delete_File(Application_Paths.program_data_comm_agent_version_path);
            Console.WriteLine("[" + DateTime.Now + "] - [Main] -> Deleted files.");

            // Delete directories
            Console.WriteLine("[" + DateTime.Now + "] - [Main] -> Deleting directories.");
            Logging.Handler.Debug("Main", "Deleting directories.", Application_Paths.program_files_comm_agent_dir);
            Helper.IO.Delete_Directory(Application_Paths.program_files_comm_agent_dir);
            Logging.Handler.Debug("Main", "Deleting directories.", Application_Paths.program_files_remote_agent_dir);
            Helper.IO.Delete_Directory(Application_Paths.program_files_remote_agent_dir);
            Logging.Handler.Debug("Main", "Deleting directories.", Application_Paths.program_files_user_agent_dir);
            Helper.IO.Delete_Directory(Application_Paths.program_files_user_agent_dir);
            Logging.Handler.Debug("Main", "Deleting directories.", Application_Paths.program_data_comm_agent_dir);
            Helper.IO.Delete_Directory(Application_Paths.program_data_comm_agent_dir);
            Logging.Handler.Debug("Main", "Deleting directories.", Application_Paths.program_data_remote_agent_dir);
            Helper.IO.Delete_Directory(Application_Paths.program_data_remote_agent_dir);
            Logging.Handler.Debug("Main", "Deleting directories.", Application_Paths.program_data_comm_agent_logs_dir);
            Helper.IO.Delete_Directory(Application_Paths.program_data_comm_agent_logs_dir);
            Logging.Handler.Debug("Main", "Deleting directories.", Application_Paths.program_data_comm_agent_jobs_dir);
            Helper.IO.Delete_Directory(Application_Paths.program_data_comm_agent_jobs_dir);
            Logging.Handler.Debug("Main", "Deleting directories.", Application_Paths.program_data_comm_agent_msdav_dir);
            Helper.IO.Delete_Directory(Application_Paths.program_data_comm_agent_msdav_dir);
            Logging.Handler.Debug("Main", "Deleting directories.", Application_Paths.program_data_comm_agent_scripts_dir);
            Helper.IO.Delete_Directory(Application_Paths.program_data_comm_agent_scripts_dir);
            Logging.Handler.Debug("Main", "Deleting directories.", Application_Paths.program_data_comm_agent_backups_dir);
            Helper.IO.Delete_Directory(Application_Paths.program_data_comm_agent_backups_dir);
            Logging.Handler.Debug("Main", "Deleting directories.", Application_Paths.program_data_comm_agent_sensors_dir);
            Helper.IO.Delete_Directory(Application_Paths.program_data_comm_agent_sensors_dir);
            Logging.Handler.Debug("Main", "Deleting directories.", Application_Paths.program_data_comm_agent_dumps_dir);
            Helper.IO.Delete_Directory(Application_Paths.program_data_comm_agent_dumps_dir);
            Logging.Handler.Debug("Main", "Deleting directories.", Application_Paths.program_data_comm_agent_temp_dir);
            Helper.IO.Delete_Directory(Application_Paths.program_data_comm_agent_temp_dir);
            
            // Delete legacy directories
            Logging.Handler.Debug("Main", "Deleting directories (legacy).", Application_Paths.program_files_user_process_dir);
            Helper.IO.Delete_Directory(Application_Paths.program_files_user_process_dir);
            Logging.Handler.Debug("Main", "Deleting directories (legacy).", Application_Paths.program_data_user_process_dir);
            Helper.IO.Delete_Directory(Application_Paths.program_data_user_process_dir);

            // Delete logs
            if (OperatingSystem.IsLinux() || OperatingSystem.IsMacOS())
            {
                // Delete comm agent service log
                Logging.Handler.Debug("Main", "Deleting comm agent service log.", Application_Paths.program_files_comm_agent_service_log_path_unix);
                Console.WriteLine("[" + DateTime.Now + "] - [Main] -> Deleting comm agent service log: " + Application_Paths.program_files_comm_agent_service_log_path_unix);
                Bash.Execute_Script("Deleting comm agent service log", false, $"rm -f {Application_Paths.program_files_comm_agent_service_log_path_unix}");

                // Delete remote agent service log
                Logging.Handler.Debug("Main", "Deleting remote agent service log.", Application_Paths.program_files_remote_agent_service_log_path_unix);
                Console.WriteLine("[" + DateTime.Now + "] - [Main] -> Deleting remote agent service log: " + Application_Paths.program_files_remote_agent_service_log_path_unix);
                Bash.Execute_Script("Deleting remote agent service log", false, $"rm -f {Application_Paths.program_files_remote_agent_service_log_path_unix}");

                // Delete health agent service log
                Logging.Handler.Debug("Main", "Deleting health agent service log.", Application_Paths.program_files_health_agent_service_log_path_unix);
                Console.WriteLine("[" + DateTime.Now + "] - [Main] -> Deleting health agent service log: " + Application_Paths.program_files_health_agent_service_log_path_unix);
                Bash.Execute_Script("Deleting health agent service log", false, $"rm -f {Application_Paths.program_files_health_agent_service_log_path_unix}");
            }

            Console.WriteLine("[" + DateTime.Now + "] - [Main] -> Deleted directories.");
        }
    }
}
