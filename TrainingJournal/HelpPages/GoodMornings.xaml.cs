using System;
using System.Diagnostics;
using System.IO;
using System.Windows;
using System.Windows.Controls;

namespace TrainingJournal.HelpPages
{
    /// <summary>
    /// Логика взаимодействия для GoodMornings.xaml
    /// </summary>
    public partial class GoodMornings : UserControl
    {
        private string _text;

        public GoodMornings()
        {
            InitializeComponent();
            ReadData();
            Content.Text = _text;
        }

        private void ReadData()
        {
            string file = @"HtmlPages/GoodMornings.html";
            try
            {
                using (StreamReader sr = new StreamReader(file))
                {
                    _text = sr.ReadToEnd();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void WatchVideo_OnClick(object sender, RoutedEventArgs e)
        {
            try
            {
                Process.Start("https://youtu.be/sbB_0N_AfHg");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
