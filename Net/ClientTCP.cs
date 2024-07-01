using System.Net.NetworkInformation;
using System.Net.Sockets;
using System.Net;

namespace URManager.Backend.Net
{
    public class ClientTCP
    {
        private readonly string _ip;
        private readonly TcpClient _clientTCP;
        private NetworkStream? _stream;
        private readonly int _dashboardPort = 29999;


        public ClientTCP(string IP)
        {
            _ip = IP;

            _clientTCP = new TcpClient();
            _clientTCP.ReceiveTimeout = 200;
            _clientTCP.SendTimeout = 500;
        }

        /// <summary>
        /// Connect TCP client
        /// </summary>
        /// <returns>True if connected</returns>
        public async Task<bool> ConnectToServerAsync()
        {
            var available = await PingAsync();

            if (!available) return false;
            try
            {
                await _clientTCP.ConnectAsync(new IPEndPoint(GetIp(_ip), _dashboardPort));
                _stream = _clientTCP.GetStream();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        /// <summary>
        /// Disconnnect TCP client
        /// </summary>
        public void Disconnect()
        {
            if (_clientTCP is null) return;

            _clientTCP.Close();
            _clientTCP.Dispose();
        }

        /// <summary>
        /// Read TCP client stream 
        /// </summary>
        /// <returns>Message as string</returns>
        public async Task<string> ReceiveMessageAsync()
        {
            try
            {
                if (_stream is not null)
                {
                    byte[] data = new Byte[256];

                    Int32 bytes = await _stream.ReadAsync(data, 0, data.Length);
                    return System.Text.Encoding.ASCII.GetString(data, 0, bytes);
                }
                else
                {
                    return ("something went wrong");
                }
            }
            catch
            {
                _stream?.Dispose();
                _clientTCP.Close();
                return ("Lost Connection");
            }

        }

        /// <summary>
        /// Send encoded ASCII string to TCP client
        /// </summary>
        /// <param name="msg"></param>
        public void SendMessage(string msg)
        {
            try
            {
                if (_stream is not null)
                {
                    msg = msg + "\r\n";
                    Byte[] data = System.Text.Encoding.ASCII.GetBytes(msg);
                    _stream.Write(data, 0, data.Length);
                }
            }
            catch
            {
                _stream?.Dispose();
                _clientTCP.Close();
            }
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
        /// Set IP Adress in the TCP client
        /// </summary>
        /// <param name="ip"></param>
        /// <returns></returns>
        private static IPAddress GetIp(string ip)
        {
            return IPAddress.Parse(ip);
        }

    }
}
