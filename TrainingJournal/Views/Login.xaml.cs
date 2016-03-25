using System;
using System.Windows;
using System.Windows.Controls;
using WPFPageSwitch;

namespace TrainingJournal.Views
{
    /// <summary>
    /// Логика взаимодействия для Login.xaml
    /// </summary>
    public partial class Login : UserControl, ISwitchable
    {
        private Session _session;

        public Login(Session session)
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

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            Switcher.Switch(new MainMenu(_session));
        }

        private void LoginButton_Click(object sender, RoutedEventArgs e)
        {
            ProgressRing.Visibility = Visibility.Visible;

            User user = new User();
            {
                user.Identificator = LoginTextBox.Text;
                user.Password = PasswordBox.Password;
            }

            if (!_session.TryLogin(user))
            {
                ProgressRing.Visibility = Visibility.Hidden;
                ErrorLabel.Text = "Неверный логин или пароль!";
            }
            else Switcher.Switch(new MainMenu(_session));
        }
    }
}
