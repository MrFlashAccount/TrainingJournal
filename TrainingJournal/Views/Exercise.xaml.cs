using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;

namespace TrainingJournal.Views
{
    /// <summary>
    /// Логика взаимодействия для Exercise.xaml
    /// </summary>
    public partial class Exercise : INotifyPropertyChanged
    {
        private Session _session;
        private double _weight;
        private int _numApproachs;
        private int _numReps;
        private double _tonnage;
        private string _comment;

        public double Weight
        {
            get { return _weight; }
            set
            {
                _weight = value;
                OnPropertyChanged();
            }
        }

        public int NumApproachs
        {
            get { return _numApproachs; }
            set
            {
                _numApproachs = value;
                OnPropertyChanged();
            }
        }

        public int NumReps
        {
            get { return _numReps; }
            set
            {
                _numReps = value;
                OnPropertyChanged();
            }
        }

        public string Tonnage
        {
            get { return _tonnage.ToString("F2"); }
            set
            {
                _tonnage = Convert.ToDouble(value);
                OnPropertyChanged();
            }
        }

        public string Comment
        {
            get { return _comment; }
            set
            {
                _comment = value;
                OnPropertyChanged();
            }
        }

        public Exercise(Session session, TrainJournal trainJournal = null)
        {
            InitializeComponent();

            if (trainJournal != null)
            {
                TitleTextBlock.Text = trainJournal.Name;
                Weight = trainJournal.Weight;
                NumApproachs = trainJournal.NumOfSets;
                NumReps = trainJournal.NumOfReps;
            }
            _session = session;

            WeightNumericUpDown.DataContext = this;
            NumApproachNumericUpDown.DataContext = this;
            NumRepsNumericUpDown.DataContext = this;
            TonnageTextBlock.DataContext = this;
            CommentTextBlock.DataContext = this;
        }

        #region INotifyPropertyChanged Members

        public event PropertyChangedEventHandler PropertyChanged;

        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
            try
            {
                if (prop != "Tonnage") Tonnage = (Weight * NumReps * NumApproachs).ToString("F2");
            }
            catch
            {
                //ignored
            }

        }

        #endregion

        private void DeleteButton_OnClick(object sender, RoutedEventArgs e)
        {
            //TODO удаление контрола
        }
    }
}