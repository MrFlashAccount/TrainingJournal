using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using WPFPageSwitch;
using UserControl = System.Windows.Controls.UserControl;

namespace TrainingJournal.Views
{
    /// <summary>
    /// Логика взаимодействия для Workspace.xaml
    /// </summary>
    public partial class Workspace : UserControl, ISwitchable
    {
        private Session _session;
        public List<UserAntropometry> UserAntropometries;
        public List<Weight> UserWeights;
        public List<TrainJournal> TrainJournals;

        public Workspace(Session session)
        {
            InitializeComponent();
            _session = session;
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

            WeightBackTextBox.Text = weight.Weight1.ToString();
            FatPercentBackTextBox.Text = weight.FatPercent != null ? weight.FatPercent.ToString() : "<пусто>";
        }

        private void FillTrainJournalDg()
        {
            TrainJournalDatagrid.ItemsSource = null;
            TrainJournalDatagrid.ItemsSource = TrainJournals.ToArray().Reverse().ToList();
        }

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
                Nech = (float)Convert.ToDouble(NeckTextBox.Text),
                Chest = (float)Convert.ToDouble(ChestTextBox.Text),
                Arm = (float)Convert.ToDouble(ArmTextBox.Text),
                Waist = (float)Convert.ToDouble(WaistTextBox.Text),
                Hip = (float)Convert.ToDouble(HipTextBox.Text),
                Shin = (float)Convert.ToDouble(ShinTextBox.Text),
                Date = DateTime.Today
            };
            UserAntropometries.Add(userAntropometry);
            FillUserAntropometryHistoryDg();
            SuccesLabel.Content = !_session.AddUserAntropometry(userAntropometry) ? "Возникла ошибка при сохранении данных" : "Сохранено";
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
            }
            catch
            {
                return;
            }
            TrainJournals.Add(trainJournal);
            if (!_session.AddExersice(trainJournal)) MessageBox.Show("Error");
            FillTrainJournalDg();
        }

        private void JournalTabItem_Loaded(object sender, RoutedEventArgs e)
        {
            CurrentDateTextBlock.Text = DateTime.Today.ToString("D");

            TrainJournals = _session.GetTrainJournal();
            FillTrainJournalDg();

            #region Next Iteration

            //foreach (var trainjournal in _session.TrainJournals)
            //{
            //    Exercise uc = new Exercise(_session, trainjournal);
            //    ExerciseStackPanel.Children.Add(uc);
            //}

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
    }
}
