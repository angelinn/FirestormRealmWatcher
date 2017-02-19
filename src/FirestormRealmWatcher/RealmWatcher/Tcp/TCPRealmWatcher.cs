using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace RealmWatcher.Tcp
{
    public class TCPRealmWatcher : IRealmWatcher
    {

        private int updateIntervalMs = 60000;
        public int UpdateIntervalMs
        {
            get
            {
                return updateIntervalMs;
            }

            set
            {
                if (value >= 0)
                    updateIntervalMs = value;
            }
        }

        public void Stop()
        {
            waitToken.Cancel();
            waitToken.Dispose();

            waitToken = new CancellationTokenSource();
        }

        public async Task Watch(Action<RealmInfo> callback)
        {

            while (true)
            {
                if (waitToken.Token.IsCancellationRequested)
                    waitToken.Token.ThrowIfCancellationRequested();


                RealmInfo legion = new RealmInfo
                {
                    Name = "Sylvanas",
                };

                try
                {
                    TcpClient tcpClient = new TcpClient();

                    tcpClient.Connect(WORLD_SERVER_URL, WORLD_SERVER_PORT);
                    tcpClient.Close();

                    legion.Status = "online";
                }
                catch (SocketException)
                {
                    legion.Status = "offline";
                }

                callback?.Invoke(legion);

                await Task.Delay(updateIntervalMs, waitToken.Token);
            }
        }

        private const string WORLD_SERVER_URL = "legion.logon.firestorm-servers.com";
        private const int WORLD_SERVER_PORT = 8085;

        private CancellationTokenSource waitToken = new CancellationTokenSource();
    }
}
