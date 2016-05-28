using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Controls;
using MahApps.Metro.Controls;
using WPFPageSwitch;

namespace TrainingJournal.Views
{
    /// <summary>
    /// Логика взаимодействия для MainMenu.xaml
    /// </summary>
    public partial class MainMenu : ISwitchable, INotifyPropertyChanged
    {
        private readonly Session _session;
        private readonly MetroWindow _holder;
        private string _description;

        public string Description
        {
            get { return _description; }
            set
            {
                _description = value;
                OnPropertyChanged();
            }
            
        }

        public MainMenu(MetroWindow holder, Session session)
        {
            InitializeComponent();
            _session = session;
            _holder = holder;

            WhatIsTheProgramTextBlock.DataContext = this;

            Description =
                "Это дневник тренировок, который позволяет вести учет всех тренировок и видеть результат физического развития. Если вы новичок, то вам тем более нужно вести тренировочный дневник. А вдруг вы занимаетесь с персональным тренером и по каким-либо обстоятельствам вам захочется его сменить? Изучив информацию о вашем опыте, новый тренер поведет вас к цели без пауз и остановок.";
        }

        #region INotifyPropertyChanged Members

        public event PropertyChangedEventHandler PropertyChanged;

        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }

        #endregion

        #region ISwitchable Members

        public void UtilizeState(object state)
        {
            throw new NotImplementedException();
        }

        #endregion

        private void LoginButton_Click(object sender, RoutedEventArgs e)
        {
            Switcher.Switch(new Login(_holder, _session));
        }

        private void RegisterButton_Click(object sender, RoutedEventArgs e)
        {
            Switcher.Switch(new Registration(_holder, _session));
        }

        private void Grid_Loaded(object sender, RoutedEventArgs e)
        {
            if (_session.IsStarted) Switcher.Switch(new Workspace(_holder, _session));
        }

        private void Selector_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var flipview = (FlipView)sender;
            switch (flipview.SelectedIndex)
            {
                case 0:
                    flipview.BannerText = "Ведите статистику. Ставьте цели и фиксируйте результат";
                    break;
                case 1:
                    flipview.BannerText = "Ваша текущая активность в удобном и наглядном виде";
                    break;
                case 2:
                    flipview.BannerText = "Подробная информация о каждом упражнении с описание выполнения";
                    break;
            }
        }
    }
}