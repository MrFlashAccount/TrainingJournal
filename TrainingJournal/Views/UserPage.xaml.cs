using System;
using System.Windows.Controls;
using System.Windows.Media.Imaging;

namespace TrainingJournal.Views
{
    /// <summary>
    /// Логика взаимодействия для UserPage.xaml
    /// </summary>
    public partial class UserPage : UserControl
    {
        //private Session _session;

        public UserPage()
        {
            InitializeComponent();
            //_session = session;
            //Avatar.Source  = new BitmapImage(Properties.Resources.noAvatar);
            //NameTextBlock.Text = _session.LoginedUser.Name;
        }
    }
}
