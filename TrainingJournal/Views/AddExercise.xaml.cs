using System.Windows;
using MahApps.Metro.Controls;

namespace TrainingJournal.Views
{
    /// <summary>
    /// Логика взаимодействия для AddExercise.xaml
    /// </summary>
    public partial class AddExercise : MetroWindow
    {
        public AddExercise()
        {
            InitializeComponent();
        }

        private void OkButton_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = true;
            Close();
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
            Close();
        }
    }
}
