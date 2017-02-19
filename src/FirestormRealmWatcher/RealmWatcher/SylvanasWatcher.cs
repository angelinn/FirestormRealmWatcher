using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace RealmWatcher
{
    public abstract class SylvanasWatcher
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
                callback?.Invoke(GetRealmInfo());
                await Task.Delay(updateIntervalMs, waitToken.Token);
            }
        }

        protected abstract RealmInfo GetRealmInfo();

        private CancellationTokenSource waitToken = new CancellationTokenSource();
    }
}
