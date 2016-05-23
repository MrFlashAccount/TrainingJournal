using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using MahApps.Metro.Controls;
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
        public List<TrainJournal> TrainJournals;

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
            ExerciseStackPanel.Children.Clear();

            try
            {
                foreach (DateTime date in SelectUniqueDates())
                {
                    DayExpander dex = new DayExpander(_session, date);
                    dex.AddExercises(SelectTrainJournlasByDate(date));
                    ExerciseStackPanel?.Children.Add(dex);
                }
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
        }

        private List<DateTime> SelectUniqueDates() => TrainJournals.Select(x => x.Date).Distinct().ToList();

        private List<TrainJournal> SelectTrainJournlasByDate(DateTime date) => TrainJournals.Where(x => x.Date == date).ToList();

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

            AddExercise addExercise = new AddExercise();

            if (addExercise.ShowDialog() != true) return;

            try
            {
                trainJournal.Login = _session.LoginedUser.Identificator;
                trainJournal.Name = addExercise.NameTextBox.Text;
                trainJournal.Date = DateTime.Today;
                trainJournal.NumOfSets = byte.Parse(addExercise.NumOfSetsTextBox.Text);
                trainJournal.NumOfReps = byte.Parse(addExercise.NumOfRepsTextBox.Text);
                trainJournal.Weight = int.Parse(addExercise.WeightTextBox.Text);
            }
            catch
            {
                return;
            }
            TrainJournals.Add(trainJournal);
            if (!_session.AddExersice(trainJournal)) MessageBox.Show("Error");
            FillTrainJournalContent();
        }

        private void JournalTabItem_Loaded(object sender, RoutedEventArgs e)
        {
            CurrentDateTextBlock.Text = DateTime.Today.ToString("D");

            TrainJournals = _session.GetTrainJournal();
            FillTrainJournalContent();

            #region Next Iteration

            #endregion

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
    }
}