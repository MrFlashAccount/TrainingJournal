using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using MahApps.Metro.Controls;
using MahApps.Metro.Controls.Dialogs;
using TrainingJournal.HelpPages;
using WPFPageSwitch;

namespace TrainingJournal.Views
{
    /// <summary>
    /// Логика взаимодействия для Workspace.xaml
    /// </summary>
    public partial class Workspace : ISwitchable
    {
        private readonly Session _session;
        private readonly MetroWindow _holder;

        public List<UserAntropometry> UserAntropometries;

        public Workspace(MetroWindow holder, Session session)
        {
            InitializeComponent();
            _session = session;
            _holder = holder;

            NameTextBlock.DataContext = _session.LoginedUser;

            FillUserAntropometryHistoryDg();
            FillWeightHistoryDg();

            Avatar.DataContext = _session;
        }

        private void FillUserAntropometryHistoryDg()
        {
            NechDataGrid.ItemsSource = null;
            NechDataGrid.ItemsSource = _session.UserAntropometries;

            ChestDataGrid.ItemsSource = null;
            ChestDataGrid.ItemsSource = _session.UserAntropometries;

            ArmsDataGrid.ItemsSource = null;
            ArmsDataGrid.ItemsSource = _session.UserAntropometries;

            WaistDataGrid.ItemsSource = null;
            WaistDataGrid.ItemsSource = _session.UserAntropometries;

            HipDataGrid.ItemsSource = null;
            HipDataGrid.ItemsSource = _session.UserAntropometries;

            ShinDataGrid.ItemsSource = null;
            ShinDataGrid.ItemsSource = _session.UserAntropometries;

            if (_session.CurrentUserAntropometry == null)
            {
                NeckTextBox.Text = "нет";
                ChestTextBlock.Text = "нет";
                ArmTextBlock.Text = "нет";
                WaistTextBlock.Text = "нет";
                HipTextBlock.Text = "нет";
                ShinTextBlock.Text = "нет";

                return;
            }

            if (_session.CurrentUserAntropometry.Nech != null)
                NeckTextBox.Text = $"{_session.CurrentUserAntropometry.Nech} см.";
            if (_session.CurrentUserAntropometry.Chest != null)
                ChestTextBlock.Text = $"{_session.CurrentUserAntropometry.Chest} см.";
            if (_session.CurrentUserAntropometry.Arm != null)
                ArmTextBlock.Text = $"{_session.CurrentUserAntropometry.Arm} см.";
            if (_session.CurrentUserAntropometry.Waist != null)
                WaistTextBlock.Text = $"{_session.CurrentUserAntropometry.Waist} см.";
            if (_session.CurrentUserAntropometry.Hip != null)
                HipTextBlock.Text = $"{_session.CurrentUserAntropometry.Hip} см.";
            if (_session.CurrentUserAntropometry.Shin != null)
                ShinTextBlock.Text = $"{_session.CurrentUserAntropometry.Shin} см.";
        }

        private void FillWeightHistoryDg()
        {
            WeightDataGrid.ItemsSource = null;
            WeightDataGrid.ItemsSource = _session.UserWeights.ToArray().Reverse().ToList();

            if (_session.CurrentWeight == null)
            {
                WeightTextBox.Text = "нет";
                FatPercentTextBox.Text = "нет";
                return;
            }

            WeightTextBox.Text = $"{_session.CurrentWeight.Weight1} кг.";
            FatPercentTextBox.Text = $"{_session.CurrentWeight.FatPercent} %";
        }

        private void FillTrainJournalContent()
        {
            try
            {
                if (_session.TrainJournals.Count > 0 && _session.TrainJournals.LastOrDefault()?.Date == DateTime.Today)
                    foreach (DateTime date in SelectUniqueDatesInverse())
                    {
                        ExerciseStackPanel?.Children.Add(new DayExpander(_holder, _session, date,
                            SelectTrainJournlasByDate(date)));
                    }
                else
                    foreach (DateTime date in SelectUniqueDatesWithTodayInverse())
                    {
                        ExerciseStackPanel?.Children.Add(new DayExpander(_holder, _session, date,
                            SelectTrainJournlasByDate(date)));
                    }
            }
            catch
            {
                ShowErrorMessage("Возникла ошибка при добавлении упражнений!", "Сожалеем об этом");
            }
        }

        private void AddExerciseUserControl(TrainJournal tJournal)
        {
            DayExpander var = ExerciseStackPanel?.Children[0] as DayExpander;

            if (var?.ControlDateTime != tJournal.Date) AddDayExpander(tJournal);
            else var.AddExercise(tJournal);
        }

        private void AddDayExpander(TrainJournal tJournal)
        {
            DayExpander dx = new DayExpander(_holder, _session, DateTime.Today, new List<TrainJournal> { tJournal });
            ExerciseStackPanel?.Children.Add(dx);
        }

        private List<DateTime> SelectUniqueDatesInverse()
        {
            List<DateTime> listToReturn = _session.TrainJournals.Select(x => x.Date).Distinct().ToList();
            listToReturn.Reverse();

            return listToReturn;
        }

        private List<DateTime> SelectUniqueDatesWithTodayInverse()
        {
            List<DateTime> listToReturn = _session.TrainJournals.Select(x => x.Date).Distinct().ToList();
            listToReturn.Add(DateTime.Today);
            listToReturn.Reverse();

            return listToReturn;
        }

        private List<TrainJournal> SelectTrainJournlasByDate(DateTime date) => _session.TrainJournals.Where(x => x.Date == date).ToList();

        #region ISwitchable Members

        public void UtilizeState(object state)
        {
            throw new NotImplementedException();
        }

        #endregion

        private void AddButton_OnClick(object sender, RoutedEventArgs e)
        {
            TrainJournal trainJournal = new TrainJournal();

            try
            {
                trainJournal.Login = _session.LoginedUser.Identificator;
                trainJournal.Date = DateTime.Today;
            }
            catch
            {
                return;
            }

            if (!_session.AddExersice(trainJournal)) ShowErrorMessage("Возникла ошибка", "Сожалеем об этом");

            AddExerciseUserControl(trainJournal);
        }

        private void JournalTabItem_Loaded(object sender, RoutedEventArgs e)
        {
            CurrentDateTextBlock.Text = DateTime.Today.ToString("D");

            FillTrainJournalContent();
        }

        private void SaveWeightButton_OnClick(object sender, RoutedEventArgs e)
        {
            if (WeightValueNumericUpDown.Value == null) return;

            Weight weight = new Weight
            {
                Login = _session.LoginedUser.Identificator,
                Date = DateTime.Today,
                Weight1 = (float) WeightValueNumericUpDown.Value,
                FatPercent = Convert.ToInt16(FatPercentValueNumericUpDown.Value ?? 0)
            };

            WeightValueNumericUpDown.Value = null;
            FatPercentValueNumericUpDown.Value = null;
            _session.UserWeights = new List<Weight> {weight};

            FillWeightHistoryDg();
        }

        private void FlipView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var flipview = (FlipView) sender;
            switch (flipview.SelectedIndex)
            {
                case 0:
                    flipview.BannerText = "С чего начать новичку?";
                    break;
                case 1:
                    flipview.BannerText = "Узнай как правильно делать замеры тела!";
                    break;
                case 2:
                    flipview.BannerText = "Стань экспертом в области тренинга!";
                    break;
            }
        }

        private void TreeViewItem_Selected(object sender, RoutedEventArgs e)
        {
            TreeViewItem tvItem = (TreeViewItem)sender;

            switch (tvItem.Name)
            {
                case "UserAntropometry":
                    ContentGrid.Children.Clear();
                    ContentGrid.Children.Add(new HelpPages.UserAntropometry());
                    break;
                case "Squat":
                    ContentGrid.Children.Clear();
                    ContentGrid.Children.Add(new Squat());
                    break;
                case "BenchPress":
                    ContentGrid.Children.Clear();
                    ContentGrid.Children.Add(new BenchPress());
                    break;
                case "Deadlift":
                    ContentGrid.Children.Clear();
                    ContentGrid.Children.Add(new Deadlift());
                    break;
                case "InclineBenchPress":
                    ContentGrid.Children.Clear();
                    ContentGrid.Children.Add(new InclineBenchPress());
                    break;
                case "PullUps":
                    ContentGrid.Children.Clear();
                    ContentGrid.Children.Add(new PullUps());
                    break;
                case "MilitaryPress":
                    ContentGrid.Children.Clear();
                    ContentGrid.Children.Add(new MilitaryPress());
                    break;
                case "GoodMornings":
                    ContentGrid.Children.Clear();
                    ContentGrid.Children.Add(new GoodMornings());
                    break;
                case "BicepsCurl":
                    ContentGrid.Children.Clear();
                    ContentGrid.Children.Add(new BicepsCurl());
                    break;
                case "LyingTricepsExtensions":
                    ContentGrid.Children.Clear();
                    ContentGrid.Children.Add(new LyingTricepsExtension());
                    break;
                case "FlyeMotion":
                    ContentGrid.Children.Clear();
                    ContentGrid.Children.Add(new FlyeMotion());
                    break;
                case "LegExtension":
                    ContentGrid.Children.Clear();
                    ContentGrid.Children.Add(new LegExtension());
                    break;
            }
        }

        private async void ShowErrorMessage(string title, string message)
        {
            await _holder.ShowMessageAsync(title, message);
        }

        private void SaveNechButton_OnClick(object sender, RoutedEventArgs e)
        {
            UserAntropometry userAntropometry = new UserAntropometry()
            {
                Login = _session.LoginedUser.Identificator,
                Nech = (float)Convert.ToDouble(NechNumericUpDown.Value ?? 0),
                Date = DateTime.Today
            };

            _session.UserAntropometries = new List<UserAntropometry> {userAntropometry};
            NechNumericUpDown.Value = 0;

            FillUserAntropometryHistoryDg();
        }
    }
}