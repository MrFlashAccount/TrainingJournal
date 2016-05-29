using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using MahApps.Metro.Controls;
using MahApps.Metro.Controls.Dialogs;

namespace TrainingJournal.Views
{
    /// <summary>
    /// Логика взаимодействия для Exercise.xaml
    /// </summary>
    public partial class Exercise : INotifyPropertyChanged
    {
        private readonly Session _session;
        private readonly TrainJournal _trainJournal;
        private readonly MetroWindow _holder;

        private string _title;
        private double _tonnage;
        private double _weight;
        private int _numApproachs;
        private int _numReps;
        private string _comment;

        public string Title
        {
            get { return _title; }
            set
            {
                _title = value;
                _trainJournal.Name = value;
                OnPropertyChanged();
            }
        }
        public double Weight
        {
            get { return _weight; }
            set
            {
                _weight = value;
                _trainJournal.Weight = (int)value;
                OnPropertyChanged();
            }
        }
        public int NumOfSets
        {
            get { return _numApproachs; }
            set
            {
                _numApproachs = value;
                _trainJournal.NumOfSets = value;
                OnPropertyChanged();
            }
        }
        public int NumOfReps
        {
            get { return _numReps; }
            set
            {
                _numReps = value;
                _trainJournal.NumOfReps = value;
                OnPropertyChanged();
            }
        }
        public string Comment
        {
            get { return _comment; }
            set
            {
                _comment = value;
                _trainJournal.Comment = value;
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

        public Exercise(MetroWindow holder, Session session, TrainJournal trainJournal = null)
        {
            InitializeComponent();

            if (trainJournal != null)
            {
                _trainJournal = trainJournal;
                Title = _trainJournal.Name;
                Weight = _trainJournal.Weight;
                NumOfSets = _trainJournal.NumOfSets;
                NumOfReps = _trainJournal.NumOfReps;
            }

            _session = session;
            _holder = holder;

            #region Not Today

            if (_trainJournal.Date != DateTime.Today)
            {
                TitleTextBox.IsReadOnly = true;
                WeightNumericUpDown.IsReadOnly = true;
                NumApproachNumericUpDown.IsReadOnly = true;
                NumRepsNumericUpDown.IsReadOnly = true;
                CommentTextBlock.IsReadOnly = true;
            }

            #endregion

            #region Set data context

            TitleTextBox.DataContext = this;
            WeightNumericUpDown.DataContext = this;
            NumApproachNumericUpDown.DataContext = this;
            NumRepsNumericUpDown.DataContext = this;
            CommentTextBlock.DataContext = this;
            TonnageTextBlock.DataContext = this;

            #endregion

        }

        private string CalculateTonnage()
        {
           return (_trainJournal.Weight * _trainJournal.NumOfReps * _trainJournal.NumOfSets).ToString("F2");
        }

        #region INotifyPropertyChanged Members

        public event PropertyChangedEventHandler PropertyChanged;

        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            try
            {
                if (prop != "Tonnage" && prop != "Title") Tonnage = CalculateTonnage();
            }
            catch
            {
                //ignored
            }

            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }

        #endregion

        private void DeleteButton_OnClick(object sender, RoutedEventArgs e)
        {
            if (!_session.DeleteTrainJournalByID(_trainJournal))
            {
                ShowErrorMessage("Возникла ошибка при удалении упражнения", "Сожалеем об этом");
                return;
            }

            Visibility=Visibility.Collapsed;
        }

        private async void ShowErrorMessage(string title, string message)
        {
            await _holder.ShowMessageAsync(title, message);
        }
    }
}