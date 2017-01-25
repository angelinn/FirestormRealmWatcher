using FirestormRealmWatcher.ViewModels;
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

using NotifyIcon = System.Windows.Forms.NotifyIcon;

namespace FirestormRealmWatcher
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public RealmStatusViewModel RealmStatusViewModel { get; set; } = new RealmStatusViewModel();

        public MainWindow()
        {
            InitializeComponent();

            icon.Icon = new System.Drawing.Icon("tray_16.ico");
            icon.Visible = true;
            
            DataContext = RealmStatusViewModel;
            Closing += OnMainWindowClosing;

            Task.Run(async () =>
            {
                await Task.Delay(1000);
                icon.ShowBalloonTip(5000, $"{RealmStatusViewModel.RealmName} е онлайн.", " ", System.Windows.Forms.ToolTipIcon.None);
            });
        }

        private void OnMainWindowClosing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            e.Cancel = true;
            Visibility = Visibility.Hidden;
        }

        private void OnExitClick(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private NotifyIcon icon = new NotifyIcon();
    }
}
