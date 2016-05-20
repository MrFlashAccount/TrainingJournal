using System.Windows;
using System.Windows.Controls;

namespace TrainingJournal.Views
{
    /// <summary>
    /// Логика взаимодействия для Exercise.xaml
    /// </summary>
    public partial class Exercise : UserControl
    {
        private Session _session;

        public Exercise(Session session, TrainJournal trainJournal = null)
        {
            InitializeComponent();
            if (trainJournal != null)
            {
                TitleTextBlock.Text = trainJournal.Name;
                WeightTextBlock.Text = trainJournal.Weight.ToString();
                NumApproachTextBlock.Text = trainJournal.NumOfSets.ToString();
                NumRepsTextBlock.Text = trainJournal.NumOfReps.ToString();
            }
            _session = session;
        }

        private void DeleteButton_OnClick(object sender, RoutedEventArgs e)
        {
            //TODO удаление контрола
        }

        private void NumRepsTextBlock_TextChanged(object sender, TextChangedEventArgs e)
        {
            CalculateTonnage();
        }

        private void NumApproachTextBlock_OnTextChanged(object sender, TextChangedEventArgs e)
        {
            CalculateTonnage();
        }

        private void CalculateTonnage()
        {
            try
            {
                int weight = int.Parse(WeightTextBlock.Text);
                int reps = int.Parse(NumRepsTextBlock.Text);
                int approach = int.Parse(NumApproachTextBlock.Text);

                TonnageTextBlock.Text = (weight*reps*approach).ToString();
            }
            catch
            { 
                //ignored
            }
        }

        private void WeightTextBlock_TextChanged(object sender, TextChangedEventArgs e)
        {
            CalculateTonnage();
        }
    }
}
