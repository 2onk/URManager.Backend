using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace URManager.Backend.Model
{
    public class SshCommands
    {
        public static string UpdatePolyscope => "urupdate -u ";
        public static string FilePathPrograms => "/programs/";
        public static string DeleteUpdateFile => "rm /programs/";
        public static string FilePathMedia => "/media/";
        public static string MoveFile => "mv ";
        public static string RemotePowerOn => "echo power on | nc 127.0.0.1 29999 -w 1";
        //public static string RemotePowerOff => "echo \"robotmode\" | nc 127.0.0.1 29999 -w 1 | if grep -q IDLE; then echo \"power off\" | nc 127.0.0.1 29999 -w 1; fi";
        public static string GetConnectedUsb => "find /media/ -iname 'urmount'*";
    }
}
