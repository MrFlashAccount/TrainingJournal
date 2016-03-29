using System;
using System.Windows;
using System.Windows.Controls;
using MahApps.Metro.Controls;
using MahApps.Metro.Controls.Dialogs;
using Microsoft.Win32;
using TrainingJournal.Views;
using WPFPageSwitch;

namespace TrainingJournal
{
    /// <summary>
    /// Interaction logic for Window1.xaml
    /// </summary>
    public partial class PageSwitcher : MetroWindow
    {
        private readonly Session _session;

        public PageSwitcher()
        {
            InitializeComponent();
            _session = new Session();
            Switcher.pageSwitcher = this;
            Switcher.Switch(new MainMenu(_session));
        }

        public void Navigate(UserControl nextPage)
        {
            Content = nextPage;
        }

        public void Navigate(UserControl nextPage, object state)
        {
            Content = nextPage;
            ISwitchable s = nextPage as ISwitchable;

            if (s != null)
                s.UtilizeState(state);
            else
                throw new ArgumentException("NextPage is not ISwitchable! " + nextPage.Name);
        }

        private void SettingsButton_OnClick(object sender, RoutedEventArgs e)
        {
            if (!_session.IsStarted) return;
            NameTextBox.Text = _session.LoginedUser.Name;
            ChangeFlayoutState();
        }

        private async void ChangeAvatarButton_OnClick(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog
            {
                Filter = "Images |*.jpg;*.png"
            };

            if (openFileDialog.ShowDialog() != true) return;

            string fileName = openFileDialog.SafeFileName;
            string sourcePath = openFileDialog.FileName;
            string targetPath = @"Images/";
            string newFileName = _session.LoginedUser.Identificator + ".jpg";
            string sourceFile = System.IO.Path.Combine(sourcePath);
            string destFile = System.IO.Path.Combine(targetPath, newFileName);

            ChangeAvatarButton.Content = fileName;

            if (!System.IO.Directory.Exists(targetPath))
            {
                System.IO.Directory.CreateDirectory(targetPath);
            }

            System.IO.File.Copy(sourceFile, destFile, true);

            if (!_session.TryChangeAvatar(newFileName))
            {
                MessageDialogResult messageResult =
                    await this.ShowMessageAsync("Ошибка", "Возникла ошибка при смене изображения профиля");
            }
            else
            {
                MessageDialogResult messageResult =
                    await this.ShowMessageAsync("Успех", "Аватар успешно изменен");
            }
        }

        private void SettingsButtonSave_OnClick(object sender, RoutedEventArgs e)
        {
            if (NameTextBox.Text != string.Empty)
                _session.ChangeName(NameTextBox.Text);

            if (ChangePasswordBox.Password != string.Empty)
            {
                ShowLoginDialogOnlyPassword(this, e);
                ChangePasswordBox.Password = string.Empty;
            }
        }

        private async void ShowLoginDialogOnlyPassword(object sender, RoutedEventArgs e)
        {
            LoginDialogData result = await this.ShowLoginAsync("Проверка", "Подтвердите смену пароля: ", new LoginDialogSettings { ColorScheme = MetroDialogOptions.ColorScheme, ShouldHideUsername = true });

            if (result == null)
            {
                //User pressed cancel
            }
            else
            {
                if (!_session.TryChangePassword(result.Password, ChangePasswordBox.Password))
                {
                    MessageDialogResult messageResult = await this.ShowMessageAsync("Неверный пароль.", "Повторите ввод.");
                    ChangeFlayoutState();
                }
            }
        }

        private void ChangeFlayoutState()
        {
            var flyout = Flyouts.Items[0] as Flyout;
            if (flyout == null)
            {
                return;
            }

            flyout.IsOpen = !flyout.IsOpen;
        }

        private void MetroWindow_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {

        }
    }
}
