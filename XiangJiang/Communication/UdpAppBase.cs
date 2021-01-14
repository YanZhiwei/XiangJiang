﻿using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using XiangJiang.Model;

namespace XiangJiang.Communication
{
    /// <summary>
    ///     Udp抽象类
    /// </summary>
    public abstract class UdpAppBase
    {
        /// <summary>
        ///     Udp Client
        /// </summary>
        protected UdpClient AppUpdClient;

        /// <summary>
        ///     构造函数
        /// </summary>
        protected UdpAppBase()
        {
            AppUpdClient = new UdpClient();
        }

        /// <summary>
        ///     接受数据
        /// </summary>
        /// <returns>UdpReceived</returns>
        public async Task<UdpAppReceived> Receive()
        {
            var result = await AppUpdClient.ReceiveAsync();
            return new UdpAppReceived
            {
                Message = Encoding.UTF8.GetString(result.Buffer, 0, result.Buffer.Length),
                Sender = result.RemoteEndPoint
            };
        }
    }
}