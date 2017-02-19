using RealmWatcher;
using RealmWatcher.Http;
using RealmWatcher.Tcp;
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
        public RealmStatusViewModel()
        {
            Task.Run(() => watcher.Watch(UpdateUI));
            UpdateSeconds = watcher.UpdateIntervalMs / 1000;
        }

        public void UpdateUI(RealmInfo legion)
        {
            RealmName = legion.Name;
            Status = ParseRealmStatus(legion.Status);
            Updated = $"Последна проверка: {DateTime.Now.ToString("HH:mm:ss")}";

            if (lastStatus == "online" && lastStatus != legion.Status && !String.IsNullOrEmpty(lastStatus) && Status.AfterCrash)
            {
                Log += $"Сървърът стана {Status.Status} в {DateTime.Now.ToString("t")}\n";
                Status.AfterCrash = false;

                Callback?.Invoke($"{RealmName} е {Status.Status}");
            }

            lastStatus = legion.Status;
        }

        public void SetUpdateInterval(string seconds)
        {
            watcher.UpdateIntervalMs = Int32.Parse(seconds) * 1000;
            watcher.Stop();

            Task.Run(() => watcher.Watch(UpdateUI));
        }

        private RealmStatus ParseRealmStatus(string status)
        {
            if (status == "offline" || status == "online")
                return new RealmStatus { Status = status };

            string[] split = status.Split(' ');
            if (split[0] == "online")
            {
                RealmStatus realmStatus = new RealmStatus
                {
                    Status = split[0],
                    Since = $"{split[2]} {(split.Length > 3 ? split[3] : String.Empty)} {(split.Length > 4 ? split[4] : String.Empty)}",
                };

                if (status.Length > 3)
                    realmStatus.AfterCrash = false;

                return realmStatus;
            }

            return null;
        }


        public Action<string> Callback { get; set; }

        private int updateSeconds;
        public int UpdateSeconds
        {
            get
            {
                return updateSeconds;
            }
            set
            {
                updateSeconds = value;
                OnPropertyChanged();
            }
        }

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

        private RealmStatus status;
        public RealmStatus Status
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
                    OnPropertyChanged("StatusString");
                }
            }
        }

        public string StatusString
        {
            get
            {
                if (status?.Status == "online")
                {
                    if (String.IsNullOrEmpty(status.Since))
                        return status.Status;

                    return $"{status.Status} от {status.Since}";
                }

                return $"{status?.Status}";
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

        private IRealmWatcher watcher = new TCPRealmWatcher();
        private string lastStatus;
    }
}
