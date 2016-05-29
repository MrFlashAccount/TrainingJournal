using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
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
    public partial class PageSwitcher
    {
        private readonly Session _session;
        private string _path;

        public PageSwitcher()
        {
            InitializeComponent();
            _session = new Session();

            NameTextBox.DataContext = _session.LoginedUser;

            Switcher.pageSwitcher = this;
            Switcher.Switch(new MainMenu(this, _session));
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
            ChangeSettingsFlayoutState();
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
                await this.ShowMessageAsync("Ошибка", "Возникла ошибка при смене изображения профиля");
            }
            else
            {
                await this.ShowMessageAsync("Успех", "Аватар успешно изменен");
            }
        }

        private void SettingsButtonSave_OnClick(object sender, RoutedEventArgs e)
        {
            if (NameTextBox.Text != string.Empty) _session.ChangeName(NameTextBox.Text);

            if (ChangePasswordBox.Password != string.Empty) ShowLoginDialogOnlyPassword();
        }

        private async void ShowLoginDialogOnlyPassword()
        {
            LoginDialogSettings dialogSettings = new LoginDialogSettings
            {
                ColorScheme = MetroDialogOptions.ColorScheme,
                ShouldHideUsername = true,
                AffirmativeButtonText = "Подтвердить"
            };
            LoginDialogData result = await this.ShowLoginAsync("Проверка", "Подтвердите смену пароля: ", dialogSettings);

            if (result == null)
            {
                //User pressed cancel
            }
            else
            {
                if (_session.TryChangePassword(result.Password, ChangePasswordBox.Password)) return;

                await this.ShowMessageAsync("Неверный пароль.", "Повторите ввод.");
                ChangeSettingsFlayoutState();
                ChangePasswordBox.Password = string.Empty;
            }
        }

        private void ChangeSettingsFlayoutState()
        {
            var flyout = Flyouts.Items[0] as Flyout;
            if (flyout == null)
            {
                return;
            }

            flyout.IsOpen = !flyout.IsOpen;
        }

        private void ChangeUploadFlyoutState()
        {
            var flyout = Flyouts.Items[1] as Flyout;
            if (flyout == null)
            {
                return;
            }

            flyout.IsOpen = !flyout.IsOpen;
        }
        
        private void MetroWindow_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            _session.SaveTrainJournals();
        }

        private void UploadToWord_OnClick(object sender, RoutedEventArgs e)
        {
            if (!_session.IsStarted) return;
            ChangeUploadFlyoutState();
        }

        private void DestinationButton_OnClick(object sender, RoutedEventArgs e)
        {
            var dialog = new System.Windows.Forms.FolderBrowserDialog();
            System.Windows.Forms.DialogResult result = dialog.ShowDialog();
            if (result != System.Windows.Forms.DialogResult.OK) return;

            DestinationButton.Content = dialog.SelectedPath;
            _path = dialog.SelectedPath;
        }

        private void OkButton_OnClick(object sender, RoutedEventArgs e)
        {
            if(FromDatePicker.SelectedDate == null || ToDatePicker.SelectedDate == null)
            {
                ResultTextBlock.Text = "Не выбран период";
                return;
            }

            if (FromDatePicker.SelectedDate > ToDatePicker.SelectedDate)
            {
                ResultTextBlock.Text = "Некорректно выбран период";
                return;
            }

            WordDocument wordDoc = null;
            try
            {
                wordDoc = new WordDocument();
            }
            catch
            {
                wordDoc?.Close();
                throw;
            }
            wordDoc.SetSelectionToBegin();

            wordDoc.Selection.Text =
                $"Отчет по данным {WhatUploadListBox.Text} за период с {FromDatePicker.SelectedDate.Value.Date} по {ToDatePicker.SelectedDate.Value.Date}/n";

            switch (WhatUploadListBox.Text)
            {
                case "Антропометрия":
                {
                    List<UserAntropometry> temp = _session.GetUserAntropometryByPeriod(
                        FromDatePicker.SelectedDate.Value, ToDatePicker.SelectedDate.Value);
                    if (temp == null || temp.Count == 0)
                    {
                        ResultTextBlock.Text = "Не найдено записей за указанный период";
                        return;
                    }

                    #region Шапка таблицы

                    wordDoc.InsertTable(temp.Count + 1, 7);

                    wordDoc.SetSelectionToCell(1, 1);
                    wordDoc.Selection.Text = "Дата";

                    wordDoc.SetSelectionToCell(1, 2);
                    wordDoc.Selection.Text = "Шея";

                    wordDoc.SetSelectionToCell(1, 3);
                    wordDoc.Selection.Text = "Грудь";

                    wordDoc.SetSelectionToCell(1, 4);
                    wordDoc.Selection.Text = "Руки";

                    wordDoc.SetSelectionToCell(1, 5);
                    wordDoc.Selection.Text = "Талия";

                    wordDoc.SetSelectionToCell(1, 6);
                    wordDoc.Selection.Text = "Бедра";

                    wordDoc.SetSelectionToCell(1, 7);
                    wordDoc.Selection.Text = "Голень";

                    #endregion

                    #region заполнение таблицы

                    for (int i = 0; i < temp.Count; i++)
                    {
                        int position = i + 2;

                        wordDoc.SetSelectionToCell(position, 1);
                        wordDoc.Selection.Text = temp[i].Date.ToString();

                        wordDoc.SetSelectionToCell(position, 2);
                        wordDoc.Selection.Text = temp[i].Nech.ToString();

                        wordDoc.SetSelectionToCell(position, 3);
                        wordDoc.Selection.Text = temp[i].Chest.ToString();

                        wordDoc.SetSelectionToCell(position, 4);
                        wordDoc.Selection.Text = temp[i].Arm.ToString();

                        wordDoc.SetSelectionToCell(position, 5);
                        wordDoc.Selection.Text = temp[i].Hip.ToString();

                        wordDoc.SetSelectionToCell(position, 6);
                        wordDoc.Selection.Text = temp[i].Shin.ToString();

                        wordDoc.SetSelectionToCell(position, 7);
                        wordDoc.Selection.Text = temp[i].Waist.ToString();
                    }

                    #endregion

                    break;
                }
                case "Вес":
                {
                    List<Weight> temp = _session.GetWeightByPeriod(
                        FromDatePicker.SelectedDate.Value, ToDatePicker.SelectedDate.Value);
                    if (temp == null || temp.Count == 0)
                    {
                        ResultTextBlock.Text = "Не найдено записей за указанный период";
                        return;
                    }

                    #region Шапка таблицы

                    wordDoc.InsertTable(temp.Count + 1, 3);

                    wordDoc.SetSelectionToCell(1, 1);
                    wordDoc.Selection.Text = "Дата";

                    wordDoc.SetSelectionToCell(1, 2);
                    wordDoc.Selection.Text = "Вес";

                    wordDoc.SetSelectionToCell(1, 3);
                    wordDoc.Selection.Text = "Процент жира";

                    #endregion

                    #region заполнение таблицы

                    for (int i = 0; i < temp.Count; i++)
                    {
                        int position = i + 2;

                        wordDoc.SetSelectionToCell(position, 1);
                        wordDoc.Selection.Text = temp[i].Date.ToString("d");

                        wordDoc.SetSelectionToCell(position, 2);
                        wordDoc.Selection.Text = temp[i].Weight1.ToString("F1");

                        wordDoc.SetSelectionToCell(position, 3);
                        wordDoc.Selection.Text = temp[i].FatPercent.ToString();
                    }

                    #endregion

                    break;
                }
                case "История упражнений":
                {
                    List<TrainJournal> temp = _session.GetTrainJournalsByPeriod(FromDatePicker.SelectedDate.Value,
                        ToDatePicker.SelectedDate.Value);
                    if (temp == null || temp.Count == 0)
                    {
                        ResultTextBlock.Text = "Не найдено записей за указанный период";
                        return;
                    }

                    #region Шапка таблицы

                    wordDoc.InsertTable(temp.Count + 1, 7);

                    wordDoc.SetSelectionToCell(1, 1);
                    wordDoc.Selection.Text = "Дата";

                    wordDoc.SetSelectionToCell(1, 2);
                    wordDoc.Selection.Text = "Название";

                    wordDoc.SetSelectionToCell(1, 3);
                    wordDoc.Selection.Text = "Вес";

                    wordDoc.SetSelectionToCell(1, 4);
                    wordDoc.Selection.Text = "Кол-во подходов";

                    wordDoc.SetSelectionToCell(1, 5);
                    wordDoc.Selection.Text = "Кол-во повторов";

                    wordDoc.SetSelectionToCell(1, 6);
                    wordDoc.Selection.Text = "Тоннаж";

                    wordDoc.SetSelectionToCell(1, 7);
                    wordDoc.Selection.Text = "Комментарий";

                    #endregion

                    #region заполнение таблицы

                    for (int i = 0; i < temp.Count; i++)
                    {
                        int position = i + 2;

                        wordDoc.SetSelectionToCell(position, 1);
                        wordDoc.Selection.Text = temp[i].Date.ToString("d");

                        wordDoc.SetSelectionToCell(position, 2);
                        wordDoc.Selection.Text = temp[i].Name;

                        wordDoc.SetSelectionToCell(position, 3);
                        wordDoc.Selection.Text = temp[i].Weight.ToString();

                        wordDoc.SetSelectionToCell(position, 4);
                        wordDoc.Selection.Text = temp[i].NumOfSets.ToString();

                        wordDoc.SetSelectionToCell(position, 5);
                        wordDoc.Selection.Text = temp[i].NumOfReps.ToString();

                        wordDoc.SetSelectionToCell(position, 6);
                        wordDoc.Selection.Text = (temp[i].Weight*temp[i].NumOfSets*temp[i].NumOfReps).ToString();

                        wordDoc.SetSelectionToCell(position, 7);
                        wordDoc.Selection.Text = temp[i].Comment;
                    }

                    #endregion

                    break;
                }
            }
            ResultTextBlock.Text = "Операция выполнена.";

            wordDoc.Save(_path + @"\\" + @"Отчет " + WhatUploadListBox.Text + @".rtf");
            wordDoc.Close();
            Marshal.CleanupUnusedObjectsInCurrentContext();
            GC.Collect();
        }

        private void ExitButton_OnClick(object sender, RoutedEventArgs e)
        {
            ChangeSettingsFlayoutState();
            _session.Exit();
            Switcher.Switch(new MainMenu(this, _session));
        }
    }
}