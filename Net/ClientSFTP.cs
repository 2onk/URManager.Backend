using Renci.SshNet;
using System.IO;
using System.Net.NetworkInformation;

namespace URManager.Backend.Net
{
    public class ClientSftp
    {
        private readonly string _ip;
        private readonly SftpClient _sftpClient;
        private static readonly string _sftpServerPass = "easybot";
        private static readonly string _sftpServerUser = "root";

        public ClientSftp(string ip)
        {
            _ip = ip;
            _sftpClient = new SftpClient(_ip, _sftpServerUser, _sftpServerPass);
        }

        /// <summary>
        /// Get status if sftp connected
        /// </summary>
        public bool Connected => _sftpClient.IsConnected;

        /// <summary>
        /// Connect to robot via sftp
        /// </summary>
        public async Task<bool> ConnectToSftpServer()
        {
            if (_sftpClient is null) return false;

            var pingSuccess = await PingAsync();
            if (pingSuccess is not true) return false;

            if (_sftpClient.IsConnected) return true;

            await Task.Run
            (() =>
            {
                Parallel.Invoke
                (
                    () => { _sftpClient.Connect(); }
                );
            }
            );
            return true;
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
        /// upload any file to the robot, upload is startet as a task
        /// </summary>
        public async Task UploadFile(string remotePath, string localPath)
        {
            _sftpClient.ChangeDirectory(remotePath);

            await Task.Run
            (() =>
            {
                Parallel.Invoke
                (
                    () => { UploadTask(remotePath, localPath); }
                );
            }
            );
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
        /// Ping desired TCP client
        /// </summary>
        /// <returns>true if succeeded</returns>
        private async Task<bool> PingAsync()
        {
            Ping ping = new Ping();

            PingReply result = await ping.SendPingAsync(_ip);
            return result.Status == IPStatus.Success;
        }

        /// <summary>
        /// upload task async can be awaited
        /// </summary>
        /// <param name="remotePath"></param>
        /// <param name="localPath"></param>
        private void UploadTask(string remotePath, string localPath)
        {
            using (var fileStream = new FileStream(localPath, FileMode.Open))
            {
                _sftpClient.BufferSize = 4 * 1024;
                _sftpClient.UploadFile(fileStream, Path.GetFileName(localPath));
            }
        }
    }
}
