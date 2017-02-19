using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealmWatcher
{
    public interface IRealmWatcher
    {
        Task Watch(Action<RealmInfo> callback);
        void Stop();
        int UpdateIntervalMs { get; set; }
    }
}
