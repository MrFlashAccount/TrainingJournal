using System;
using System.Windows;
using System.Windows.Controls;
using WPFPageSwitch;

namespace TrainingJournal.Views
{
    /// <summary>
    /// Логика взаимодействия для Registration.xaml
    /// </summary>
    public partial class Registration : UserControl, ISwitchable
    {
        private Session _session;

        public Registration(Session session)
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

        private void RegistrationButton_Click(object sender, RoutedEventArgs e)
        {
            if (PasswordBox.Password != PasswordConfirmBox.Password)
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
                    Switcher.Switch(new MainMenu(_session));
            }
        }
    }
}
