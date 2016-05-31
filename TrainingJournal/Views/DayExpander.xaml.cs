using System;
using System.Collections.Generic;
using System.Globalization;
using System.Windows;
using MahApps.Metro.Controls;

namespace TrainingJournal.Views
{
    /// <summary>
    /// Логика взаимодействия для DayExpander.xaml
    /// </summary>
    public partial class DayExpander
    {
        private readonly Session _session;
        private readonly MetroWindow _holder;
        private readonly List<TrainJournal> _trainJournals;
        public DateTime ControlDateTime { get; private set; }

        public DayExpander(MetroWindow holder,Session session, DateTime date, List<TrainJournal> trainJournals = null)
        {
            InitializeComponent();
            _session = session;
            _holder = holder;
            ExerciseExpander.Header = date.ToString("D", CultureInfo.CurrentCulture);
            _trainJournals = trainJournals;
            ControlDateTime = date;

            if(_trainJournals == null || _trainJournals.Count == 0) Visibility = Visibility.Collapsed;

            if (date == DateTime.Today) ExerciseExpander.IsExpanded = true;
        }

        private void ExerciseExpander_Expanded(object sender, RoutedEventArgs e)
        {
            if (_trainJournals == null || _trainJournals.Count == 0) return;
            if (ExerciseContainer.Children.Count > 0) return;

            Visibility = Visibility.Visible;

            AddExercises(_trainJournals);
        }

        private void AddExercises(List<TrainJournal> trainJournals)
        {
            foreach (var trainJournal in trainJournals)
            {
                ExerciseContainer.Children.Add(new Exercise(_holder, _session, trainJournal));
            }
        }

        public void AddExercise(TrainJournal trainJournal)
        {
            Visibility = Visibility.Visible;

            ExerciseContainer.Children.Add(new Exercise(_holder, _session, trainJournal));
        }
    }
}
