using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
using WPFPageSwitch;

namespace TrainingJournal.Views
{
    /// <summary>
    /// Логика взаимодействия для Workspace.xaml
    /// </summary>
    public partial class Workspace : UserControl, ISwitchable
    {
        private Session _session;
        public List<UserAntropometry> UserAntropometries; 

        public Workspace(Session session)
        {
            InitializeComponent();
            _session = session;
            NameTextBlock.DataContext = _session.LoginedUser;
            FillAntropomertyBlock(_session.GetLastUserAntropometry());
            UserAntropometries = _session.GetUserAntropometry();
            FillUserAntropometryHistoryDg();

            Avatar.DataContext = _session;

            //for (int i = 0; i < 1000; i++)
            //{
            //    Exercise uc = new Exercise();
            //    WrapPanel.Children.Add(uc);
            //}
        }

        private void FillUserAntropometryHistoryDg()
        {
            AntropometryHistoryDatagrid.ItemsSource = null;
            AntropometryHistoryDatagrid.ItemsSource = UserAntropometries;
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
            Exercise uc = new Exercise(_session);
            ExerciseStackPanel.Children.Add(uc);
        }

        private void JournalTabItem_Loaded(object sender, RoutedEventArgs e)
        {
            CurrentDateTextBlock.Text = DateTime.Today.ToString("D");

            _session.TrainJournals = _session.GetTrainJournal();
            foreach (var trainjournal in _session.TrainJournals)
            {
                Exercise uc = new Exercise(_session, trainjournal);
                ExerciseStackPanel.Children.Add(uc);
            }
        }
    }
}
