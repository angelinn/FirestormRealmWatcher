using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace RealmWatcher.Tcp
{
    public class TCPRealmWatcher : SylvanasWatcher
    {
        protected override RealmInfo GetRealmInfo()
        {
            RealmInfo legion = new RealmInfo();

            try
            {
                using (TcpClient tcpClient = new TcpClient())
                {
                    tcpClient.Connect(WORLD_SERVER_URL, WORLD_SERVER_PORT);
                }

                legion.Status = "online";
            }
            catch (SocketException)
            {
                legion.Status = "offline";
            }

            return legion;
        }

        private const string WORLD_SERVER_URL = "legion.logon.firestorm-servers.com";
        private const int WORLD_SERVER_PORT = 8085;
    }
}
