using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace URManager.Backend.Model
{
    public class SshCommands
    {
        public static string CreateFile => "touch /home/ur/Desktop/text.txt";
        public static string UpdatePolyscope => "urupdate -u ";
        public static string FilePathMedia => "/home/ur/Desktop/";
        public static string RemotePowerOn => "echo power on | nc 127.0.0.1 29999 -w 1";
        public static string RemotePowerOff => "echo \"robotmode\" | nc 127.0.0.1 29999 -w 1 | if grep -q IDLE; then echo \"power off\" | nc 127.0.0.1 29999 -w 1; fi";
    }
}
