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

            FillNeckDataGrid();
            FillChestDataGrid();
            FillArmsDataGrid();
            FillWaistDataGrid();
            FillHipDataGrid();
            FillShinDataGrid();
            FillWeightHistoryDg();

            Avatar.DataContext = _session;
        }

        private void FillNeckDataGrid()
        {
            NechDataGrid.ItemsSource = null;
            NechDataGrid.ItemsSource = _session.NeckTables.ToArray().Reverse().ToList();

            if (_session.NeckTables.Count == 0)
            {
                NeckTextBox.Text = "нет";
                return;
            }

            NeckTextBox.Text = $"{_session.NeckTables.Last().Neck} см.";

        }
        private void FillChestDataGrid()
        {
            ChestDataGrid.ItemsSource = null;
            ChestDataGrid.ItemsSource = _session.ChestTables.ToArray().Reverse().ToList();

            if (_session.ChestTables.Count == 0)
            {
                ChestTextBlock.Text = "нет";
                return;
            }

            ChestTextBlock.Text = $"{_session.ChestTables.Last().Chest} см.";
        }
        private void FillArmsDataGrid()
        {
            ArmsDataGrid.ItemsSource = null;
            ArmsDataGrid.ItemsSource = _session.ArmTables.ToArray().Reverse().ToList();

            if (_session.ArmTables.Count == 0)
            {
                ArmTextBlock.Text = "нет";
                return;
            }

            ArmTextBlock.Text = $"{_session.ArmTables.Last().Arm} см.";
        }
        private void FillWaistDataGrid()
        {
            WaistDataGrid.ItemsSource = null;
            WaistDataGrid.ItemsSource = _session.WaistTables.ToArray().Reverse().ToList();

            if (_session.WaistTables.Count == 0)
            {
                WaistTextBlock.Text = "нет";
                return;
            }

            WaistTextBlock.Text = $"{_session.WaistTables.Last().Waist} см.";
        }
        private void FillHipDataGrid()
        {
            HipDataGrid.ItemsSource = null;
            HipDataGrid.ItemsSource = _session.HipTables.ToArray().Reverse().ToList();

            if (_session.HipTables.Count == 0)
            {
                HipTextBlock.Text = "нет";
                return;
            }

            HipTextBlock.Text = $"{_session.HipTables.Last().Hip} см.";
        }
        private void FillShinDataGrid()
        {
            ShinDataGrid.ItemsSource = null;
            ShinDataGrid.ItemsSource = _session.ShinTables.ToArray().Reverse().ToList();

            if (_session.ShinTables.Count == 0)
            {
                ShinTextBlock.Text = "нет";
                return;
            }

            ShinTextBlock.Text = $"{_session.ShinTables.Last().Shin} см.";
        }

        private void FillWeightHistoryDg()
        {
            WeightDataGrid.ItemsSource = null;
            WeightDataGrid.ItemsSource = _session.UserWeights.ToArray().Reverse().ToList();

            if (_session.UserWeights.Count == 0)
            {
                WeightTextBox.Text = "нет";
                FatPercentTextBox.Text = "нет";
                return;
            }

            WeightTextBox.Text = $"{_session.UserWeights.Last().Weight1} кг.";
            FatPercentTextBox.Text = $"{_session.UserWeights.Last().FatPercent} %";
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
                    ContentGrid.Children.Add(new UserAntropometry());
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

        private void SaveNechButton_OnClick(object sender, RoutedEventArgs e)
        {
            NeckTable neckTable = new NeckTable()
            {
                Login = _session.LoginedUser.Identificator,
                Neck = (float) Convert.ToDouble(NechNumericUpDown.Value ?? 0),
                Date = DateTime.Today
            };

            _session.NeckTables = new List<NeckTable> {neckTable};
            NechNumericUpDown.Value = null;

            FillNeckDataGrid();
        }

        private void SaveChestButton_OnClick(object sender, RoutedEventArgs e)
        {
            ChestTable chestTable = new ChestTable()
            {
                Login = _session.LoginedUser.Identificator,
                Chest = (float) Convert.ToDouble(ChestNumericUpDown.Value ?? 0),
                Date = DateTime.Today
            };

            _session.ChestTables = new List<ChestTable> {chestTable};
            ChestNumericUpDown.Value = null;

            FillChestDataGrid();
        }

        private void SaveArmsButton_OnClick(object sender, RoutedEventArgs e)
        {
            ArmTable armTable = new ArmTable()
            {
                Login = _session.LoginedUser.Identificator,
                Arm = (float) Convert.ToDouble(ArmsNumericUpDown.Value ?? 0),
                Date = DateTime.Today
            };

            _session.ArmTables = new List<ArmTable> {armTable};
            ArmsNumericUpDown.Value = null;

            FillArmsDataGrid();
        }

        private void SaveWaistButton_OnClick(object sender, RoutedEventArgs e)
        {
            WaistTable waistTable = new WaistTable()
            {
                Login = _session.LoginedUser.Identificator,
                Waist = (float) Convert.ToDouble(WaistNumericUpDown.Value ?? 0),
                Date = DateTime.Today
            };

            _session.WaistTables = new List<WaistTable> {waistTable};
            WaistNumericUpDown.Value = null;

            FillWaistDataGrid();
        }

        private void SaveHipButton_OnClick(object sender, RoutedEventArgs e)
        {
            HipTable hipTable = new HipTable()
            {
                Login = _session.LoginedUser.Identificator,
                Hip = (float) Convert.ToDouble(HipNumericUpDown.Value ?? 0),
                Date = DateTime.Today
            };

            _session.HipTables = new List<HipTable> {hipTable};
            HipNumericUpDown.Value = null;

            FillHipDataGrid();
        }

        private void SaveShinButton_OnClick(object sender, RoutedEventArgs e)
        {
            ShinTable shinTable = new ShinTable()
            {
                Login = _session.LoginedUser.Identificator,
                Shin = (float) Convert.ToDouble(ShinNumericUpDown.Value ?? 0),
                Date = DateTime.Today
            };

            _session.ShinTables = new List<ShinTable> {shinTable};
            ShinNumericUpDown.Value = null;

            FillShinDataGrid();
        }
    }
}