using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealmWatcher.Tcp
{
    public class TCPRealmWatcher : IRealmWatcher
    {

        public int UpdateIntervalMs
        {
            get
            {
                throw new NotImplementedException();
            }

            set
            {
                throw new NotImplementedException();
            }
        }

        public void Stop()
        {
            throw new NotImplementedException();
        }

        public Task Watch(Action<RealmInfo> callback)
        {
            throw new NotImplementedException();
        }

        private const int WORLD_SERVER_PORT = 8085;
    }
}
