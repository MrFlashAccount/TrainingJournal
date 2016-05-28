using System;
using System.Collections.Generic;

namespace TrainingJournal.Views
{
    /// <summary>
    /// Логика взаимодействия для DayExpander.xaml
    /// </summary>
    public partial class DayExpander
    {
        private readonly Session _session;
        public DayExpander(Session session, DateTime date)
        {
            InitializeComponent();
            _session = session;
            ExerciseExpander.Header = date;
            if (date == DateTime.Today) ExerciseExpander.IsExpanded = true;
        }

        public void AddExercises(List<TrainJournal> trainJournals)
        {
            foreach (var trainJournal in trainJournals)
            {
                ExerciseContainer.Children.Add(new Exercise(_session, trainJournal));
            }
        }
    }
}
