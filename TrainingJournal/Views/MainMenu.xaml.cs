using System;
using System.Windows;
using MahApps.Metro.Controls;
using WPFPageSwitch;

namespace TrainingJournal.Views
{
    /// <summary>
    /// Логика взаимодействия для MainMenu.xaml
    /// </summary>
    public partial class MainMenu : ISwitchable
    {
        private readonly Session _session;
        private readonly MetroWindow _holder;

        public MainMenu(MetroWindow holder, Session session)
        {
            InitializeComponent();
            _session = session;
            _holder = holder;
        }

        #region ISwitchable Members

        public void UtilizeState(object state)
        {
            throw new NotImplementedException();
        }

        #endregion

        private void LoginButton_Click(object sender, RoutedEventArgs e)
        {
            Switcher.Switch(new Login(_holder, _session));
        }

        private void RegisterButton_Click(object sender, RoutedEventArgs e)
        {
            Switcher.Switch(new Registration(_holder, _session));
        }

        private void Grid_Loaded(object sender, RoutedEventArgs e)
        {
            if (_session.IsStarted) Switcher.Switch(new Workspace(_holder, _session));
        }
    }
}
