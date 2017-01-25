using RealmWatcher;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Threading;

namespace FirestormRealmWatcher.ViewModels
{
    public class RealmStatusViewModel : BaseViewModel
    {
        private string realmName;
        public string RealmName
        {
            get
            {
                return realmName;
            }
            set
            {
                if (value != null)
                {
                    realmName = value;
                    OnPropertyChanged();
                }
            }
        }

        private string status;
        public string Status
        {
            get
            {
                return status;
            }
            set
            {
                if (value != null)
                {
                    status = value;
                    OnPropertyChanged();
                }
            }
        }

        private string updated;
        public string Updated
        {
            get
            {
                return updated;
            }
            set
            {
                if (value != null)
                {
                    updated = value;
                    OnPropertyChanged();
                }
            }
        }

        private string log;
        public string Log
        {
            get
            {
                return log;
            }
            set
            {
                if (value != null)
                {
                    log = value;
                    OnPropertyChanged();
                }
            }
        }

        public RealmStatusViewModel()
        {
            Task.Run(() => watcher.Watch(UpdateUI));
        }

        public void UpdateUI(RealmInfo legion)
        {
            RealmName = legion.Name;
            Status = legion.Status;
            Updated = $"Последна проверка: {DateTime.Now.ToString("t")}";

            if (lastStatus != legion.Status && !String.IsNullOrEmpty(lastStatus))
                Log += $"Сървърът стана {legion.Status} на {DateTime.Now.ToString("t")}\n";

            lastStatus = legion.Status;
        }

        private Watcher watcher = new Watcher();
        private string lastStatus;
    }
}
