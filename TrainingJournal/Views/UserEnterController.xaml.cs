using System.Windows;
using MahApps.Metro.Controls;
using MahApps.Metro.Controls.Dialogs;
using WPFPageSwitch;

namespace TrainingJournal.Views
{
    /// <summary>
    /// Логика взаимодействия для UserEnterController.xaml
    /// </summary>
    public partial class UserEnterController
    {
        private readonly User _user;
        private readonly bool _isNeedToRequestPassword = true;
        private readonly MetroWindow _holder;
        private readonly Session _session;

        public UserEnterController(User user, MetroWindow holder, Session session)
        {
            InitializeComponent();

            UserName.Text = user.Name;
            //UserName.DataContext = user.Name;

            _holder = holder;
            _user = user;
            _session = session;

            if (user.Password == string.Empty) _isNeedToRequestPassword = false;
        }

        private void EnterButton_OnClick(object sender, RoutedEventArgs e)
        {
            if (_isNeedToRequestPassword) ShowLoginDialogOnlyPassword();
            else
            {
                _user.Password = string.Empty;
                Login();
            }
        }

        private async void ShowLoginDialogOnlyPassword()
        {
            LoginDialogSettings dialogSettings = new LoginDialogSettings
            {
                ShouldHideUsername = true,
                AffirmativeButtonText = "Войти",
                PasswordWatermark = "Пароль",
                EnablePasswordPreview = true
            };
            LoginDialogData result = await _holder.ShowLoginAsync("Проверка", "Введите пароль: ", dialogSettings);

            if (result == null)
            {
                //User pressed cancel
            }
            else
            {
                _user.Password = result.Password;
                Login();
            }
        }

        private void Login()
        {
            if (!_session.TryLogin(_user))
            {
                ShowErrorMessage("Неверный пароль", "Повторите ввод");
            }
            else Switcher.Switch(new MainMenu(_holder, _session));
        }

        private async void ShowErrorMessage(string title, string message)
        {
            await _holder.ShowMessageAsync(title, message);
        }
    }
}
