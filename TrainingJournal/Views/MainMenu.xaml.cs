using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using WPFPageSwitch;

namespace TrainingJournal.Views
{
    /// <summary>
    /// Логика взаимодействия для MainMenu.xaml
    /// </summary>
    public partial class MainMenu : UserControl, ISwitchable
    {
        private Session _session;

        public MainMenu(Session session)
        {
            InitializeComponent();
            _session = session;
        }

        #region ISwitchable Members

        public void UtilizeState(object state)
        {
            throw new NotImplementedException();
        }

        #endregion

        private void LoginButton_Click(object sender, RoutedEventArgs e)
        {
            Switcher.Switch(new Login(_session));
        }

        private void RegisterButton_Click(object sender, RoutedEventArgs e)
        {
            Switcher.Switch(new Registration(_session));
        }

        private void Grid_Loaded(object sender, RoutedEventArgs e)
        {
            if (_session.IsStarted) Switcher.Switch(new Workspace(_session));
        }
    }
}
