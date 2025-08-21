using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace scncore_rmm_agent_comm
{
    internal class Application_Settings
    {
        public static string version = "2.5.2.5cs";
        public static string Scncore_Data_Database_String = @"Data Source=" + Application_Paths.program_data_scncore_policy_database + ";";
        public static string Scncore_Events_Database_String = @"Data Source=" + Application_Paths.program_data_scncore_events_database + ";";
        public static string Scncore_Local_Encryption_Key = "()TZ%/N)NZTG$/()4i59du4)";
    }
} 