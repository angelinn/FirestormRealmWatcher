using RealmWatcher;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace FirestormRealmWatcher
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private Watcher watcher;
        public MainWindow()
        {
            InitializeComponent();
        }

        protected override void OnActivated(EventArgs e)
        {
            if (watcher == null)
            {
                watcher = new Watcher();

                Task.Run(() => watcher.Watch(UpdateUI));
            }
        }

        private void UpdateUI(RealmInfo legion)
        {   
            Dispatcher.Invoke(() =>
            {
                txtRealm.Text = legion.Name;
                txtStatus.Text = legion.Status;
                txtUpdated.Text = $"Последна проверка: {DateTime.Now.ToString()}";
            }); 
        }
    }
}
