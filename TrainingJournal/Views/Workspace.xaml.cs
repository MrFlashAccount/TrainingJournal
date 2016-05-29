using System;
using System.Collections.Generic;
using System.Globalization;
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
        public List<Weight> UserWeights;

        public Workspace(MetroWindow holder, Session session)
        {
            InitializeComponent();
            _session = session;
            _holder = holder;

            NameTextBlock.DataContext = _session.LoginedUser;
            FillAntropomertyBlock(_session.GetLastUserAntropometry());

            UserAntropometries = _session.GetUserAntropometry();
            FillUserAntropometryHistoryDg();

            FillWeightBlock(_session.GetWeight().LastOrDefault());
            UserWeights = _session.GetWeight();
            FillWeightHistoryDg();

            Avatar.DataContext = _session;
        }

        private void FillUserAntropometryHistoryDg()
        {
            AntropometryHistoryDatagrid.ItemsSource = null;
            AntropometryHistoryDatagrid.ItemsSource = UserAntropometries;
        }

        private void FillWeightHistoryDg()
        {
            WeightHistoryDatagrid.ItemsSource = null;
            WeightHistoryDatagrid.ItemsSource = UserWeights;
        }

        private void FillAntropomertyBlock(UserAntropometry antropometry)
        {
            if (antropometry == null) return;

            NeckBackTextBox.Text = antropometry.Nech.ToString();
            ChestBackTextBox.Text = antropometry.Chest.ToString();
            ArmBackTextBox.Text = antropometry.Arm.ToString();
            WaistBackTextBox.Text = antropometry.Waist.ToString();
            HipBackTextBox.Text = antropometry.Hip.ToString();
            ShinBackTextBox.Text = antropometry.Shin.ToString();
        }

        private void FillWeightBlock(Weight weight)
        {
            if (weight == null) return;

            WeightBackTextBox.Text = weight.Weight1.ToString(CultureInfo.CurrentCulture);
            FatPercentBackTextBox.Text = weight.FatPercent != null ? weight.FatPercent.ToString() : "<пусто>";
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

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            UserAntropometry userAntropometry = new UserAntropometry()
            {
                Login = _session.LoginedUser.Identificator,
                Nech = (float) Convert.ToDouble(NeckTextBox.Text),
                Chest = (float) Convert.ToDouble(ChestTextBox.Text),
                Arm = (float) Convert.ToDouble(ArmTextBox.Text),
                Waist = (float) Convert.ToDouble(WaistTextBox.Text),
                Hip = (float) Convert.ToDouble(HipTextBox.Text),
                Shin = (float) Convert.ToDouble(ShinTextBox.Text),
                Date = DateTime.Today
            };
            UserAntropometries.Add(userAntropometry);
            FillUserAntropometryHistoryDg();
            SuccesLabel.Content = !_session.AddUserAntropometry(userAntropometry)
                ? "Возникла ошибка при сохранении данных"
                : "Сохранено";
        }

        private void AddButton_OnClick(object sender, RoutedEventArgs e)
        {
            TrainJournal trainJournal = new TrainJournal();

            try
            {
                trainJournal.Login = _session.LoginedUser.Identificator;
                //trainJournal.User = _session.LoginedUser;
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
            Weight weight = new Weight()
            {
                Login = _session.LoginedUser.Identificator,
                Date = DateTime.Today,
                Weight1 = float.Parse(WeightTextBox.Text),
                FatPercent = int.Parse(FatPercentTextBox.Text)
            };

            UserWeights.Add(weight);
            FillWeightHistoryDg();
            SuccesWeightLabel.Content = !_session.AddWeight(weight)
                ? "Возникла ошибка при сохранении данных"
                : "Сохранено";
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
    }
}