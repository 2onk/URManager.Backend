using Renci.SshNet;

namespace URManager.Backend.Net
{
    public class ClientSsh
    {
        private readonly string _ip;
        private readonly SshClient _sshClient;
        private static readonly string _sshServerPass = "easybot";
        private static readonly string _sshServerUser = "ur";

        public ClientSsh(string ip) 
        {
            _ip = ip;
            _sshClient = new SshClient(_ip, _sshServerUser, _sshServerPass);
        }

        public SshClient SshClient => _sshClient;
        public string Ip => _ip;


        /// <summary>
        /// Connect to a ssh server
        /// </summary>
        /// <returns>true if succeded</returns>
        public bool SshConnect()
        {
            SshClient.Connect();
            if(SshClient.IsConnected) return true;
            return false;
        }

        /// <summary>
        /// Dispose and disconnect actual client
        /// </summary>
        public void SshDisconnect()
        {
            SshClient.Dispose();
            SshClient.Disconnect();
        }

        /// <summary>
        /// Send an terminal command to for execution
        /// </summary>
        /// <param name="command"></param>
        /// <returns>string result</returns>
        public string ExecuteCommand(string command)
        {
            var result = SshClient.RunCommand(command);
            return result.Result;
        }
    }
}
