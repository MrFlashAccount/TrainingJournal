using System;
using System.Windows;
using WPFPageSwitch;
using System.Collections.Generic;
using MahApps.Metro.Controls;

namespace TrainingJournal.Views
{
    /// <summary>
    /// Логика взаимодействия для Login.xaml
    /// </summary>
    public partial class Login : ISwitchable
    {
        private readonly Session _session;
        private readonly MetroWindow _holder;

        public Login(MetroWindow holder,Session session)
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

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            Switcher.Switch(new MainMenu(_holder, _session));
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            List<User> userList = _session.GetUserList();

            if (userList == null)
            {
                DefaultTextBlock.Visibility = Visibility.Visible;
                RegisterButton.Visibility = Visibility.Visible;
                return;
            }

            foreach (User user in userList)
            {
                Content.Children.Add(new UserEnterController(user, _holder, _session));
            }
        }

        private void RegisterButton_OnClick(object sender, RoutedEventArgs e)
        {
            Switcher.Switch(new Registration(_holder, _session));
        }
    }
}
