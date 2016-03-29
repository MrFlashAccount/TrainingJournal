using System;
using System.Windows;
using System.Windows.Controls;
using WPFPageSwitch;

namespace TrainingJournal.Views
{
    /// <summary>
    /// Логика взаимодействия для MainMenu.xaml
    /// </summary>
    public partial class MainMenu : UserControl, ISwitchable
    {
        private Session _session;

        public MainMenu(Session session)
        {
            InitializeComponent();
            _session = session;
        }

        #region ISwitchable Members

        public void UtilizeState(object state)
        {
            throw new NotImplementedException();
        }

        #endregion

        private void LoginButton_Click(object sender, RoutedEventArgs e)
        {
            Switcher.Switch(new Login(_session));
        }

        private void RegisterButton_Click(object sender, RoutedEventArgs e)
        {
            Switcher.Switch(new Registration(_session));
        }

        private void Grid_Loaded(object sender, RoutedEventArgs e)
        {
            if (_session.IsStarted) Switcher.Switch(new Workspace(_session));
        }
    }
}
