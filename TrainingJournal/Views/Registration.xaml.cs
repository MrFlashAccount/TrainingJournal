using System;
using System.Windows;
using MahApps.Metro.Controls;
using WPFPageSwitch;

namespace TrainingJournal.Views
{
    /// <summary>
    /// Логика взаимодействия для Registration.xaml
    /// </summary>
    public partial class Registration : ISwitchable
    {
        private readonly Session _session;
        private readonly MetroWindow _holder;

        public Registration(MetroWindow holder, Session session)
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

        private void RegistrationButton_Click(object sender, RoutedEventArgs e)
        {
            if (PasswordBox.Password != ConfirmPasswordBox.Password)
                ErrorLabel.Content = "Пароли не совпадают!";
            else
            {
                User user = new User();
                {
                    user.Identificator = LoginTextBox.Text;
                    user.Password = PasswordBox.Password;
                    user.Name = string.Empty;
                    user.Image = "NoAvatar.jpg";
                }

                if (!_session.Registration(user))
                    ErrorLabel.Content = "Возникла непредвиденная ошибка!";
                else
                    Switcher.Switch(new MainMenu(_holder, _session));
            }
        }
    }
}
