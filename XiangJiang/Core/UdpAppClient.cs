using System.Text;
using XiangJiang.Communication;

namespace XiangJiang.Core
{
    /// <summary>
    ///     Upd 终端
    /// </summary>
    public sealed class UdpAppClient : UdpAppBase
    {
        private UdpAppClient()
        {
        }

        /// <summary>
        ///     连接Upd Server
        /// </summary>
        /// <param name="hostname">主机名</param>
        /// <param name="port">端口</param>
        /// <returns>UdpAppClient</returns>
        public static UdpAppClient ConnectTo(string hostname, int port)
        {
            var newUdpClient = new UdpAppClient();
            newUdpClient.AppUpdClient.Connect(hostname, port);
            return newUdpClient;
        }

        /// <summary>
        ///     发送数据报文
        /// </summary>
        /// <param name="message">数据报文</param>
        public void Send(string message)
        {
            var dataGram = Encoding.UTF8.GetBytes(message);
            AppUpdClient.Send(dataGram, dataGram.Length);
        }

        /// <summary>
        ///     发送数据报文
        /// </summary>
        /// <param name="dataGram">数据报文</param>
        public void Send(byte[] dataGram)
        {
            AppUpdClient.Send(dataGram, dataGram.Length);
        }
    }
}