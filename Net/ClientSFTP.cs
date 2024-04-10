using Renci.SshNet;

namespace URManager.Backend.Net
{
    public class ClientSFTP
    {
        private readonly string _ip;
        private readonly SftpClient _sftpClient;
        private static readonly string _sshServerPass = "easybot";
        private static readonly string _sshServerUser = "ur";

        public ClientSFTP(string ip)
        {
            _ip = ip;
            _sftpClient = new SftpClient(_ip, _sshServerUser, _sshServerPass);
        }

        /// <summary>
        /// Connect to robot via sftp
        /// </summary>
        public void ConnectToSftpServer()
        {
            if (_sftpClient is null) return;

            if (_sftpClient.IsConnected) return;

            _sftpClient.Connect();
            //Console.WriteLine(_sftpClient.ConnectionInfo.ServerVersion);
            //var result = _sftpClient.RunCommand("touch /home/ur/Desktop/text.txt");
            //result = _sftpClient.RunCommand("gedit /home/ur/Desktop/text.txt");
            //Console.WriteLine(result.Result);
        }

        /// <summary>
        /// Download supportfile to remotepath
        /// </summary>
        /// <param name="remotePath"></param>
        /// <param name="localPath"></param>
        public void DownloadFile(string remotePath, string localPath)
        {
            var filestream = File.Create(localPath);
            _sftpClient.DownloadFile(remotePath, filestream);
            filestream.Close();
        }

        /// <summary>
        /// Delete supportfile on robot
        /// </summary>
        /// <param name="file"></param>
        public void DeleteFile(string file)
        {
            _sftpClient.Delete(file);
        }

        /// <summary>
        /// Disconnect sftp connection
        /// </summary>
        public void Disconnect()
        {
            _sftpClient.Disconnect();
            _sftpClient?.Dispose();
        }

        /// <summary>
        /// Get status if sftp connected
        /// </summary>
        public bool Connected => _sftpClient.IsConnected;
    }
}
