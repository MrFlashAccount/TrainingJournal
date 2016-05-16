﻿using System;
using System.Diagnostics;
using System.IO;
using System.Windows;
using System.Windows.Controls;

namespace TrainingJournal.HelpPages
{
    /// <summary>
    /// Логика взаимодействия для LegExtension.xaml
    /// </summary>
    public partial class LegExtension : UserControl
    {
        private string _text;

        public LegExtension()
        {
            InitializeComponent();
            ReadData();
            Content.Text = _text;
        }

        private void ReadData()
        {
            string file = @"HtmlPages/LegExtension.html";
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
