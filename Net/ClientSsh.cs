using Renci.SshNet;

namespace URManager.Backend.Net
{
    public class ClientSsh
    {
        private readonly string _ip;
        private readonly SshClient _sshClient;
        private readonly string _sshServerPass;
        private readonly string _sshServerUser;

        public ClientSsh(string ip, string user = "root", string password = "easybot") 
        {
            _ip = ip;
            _sshServerUser = user;
            _sshServerPass = password;
            _sshClient = new SshClient(_ip, _sshServerUser, _sshServerPass);
        }

        public SshClient SshClient => _sshClient;
        public string Ip => _ip;


        /// <summary>
        /// Connect to a ssh server
        /// </summary>
        /// <returns>true if succeded</returns>
        public async Task<bool> SshConnect()
        {
            try
            {
                await Task.Run
                (() =>
                {
                    Parallel.Invoke
                    (
                        () => { SshClient.Connect(); }
                    );
                }
                );

                if (SshClient.IsConnected) return true;
                return false;
            }
            catch (Exception ex) 
            {
                return false;
            }
        }

        /// <summary>
        /// Dispose and disconnect actual client
        /// </summary>
        public void SshDisconnect()
        {
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
            return result.Result.ToString();
        }
    }
}
