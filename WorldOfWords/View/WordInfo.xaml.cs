﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Json;
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
using WorldOfWords.Infrastructure;
using WorldOfWords.Infrastructure.Services;
using WorldOfWords.Model;
using WorldOfWords.ViewModel;

namespace WorldOfWords.View
{
    /// <summary>
    /// Interaction logic for Word.xaml
    /// </summary>
    public partial class WordInfo : Page
    {
        public WordInfoViewModel ViewModel { get; set; }

        public WordInfo(List<WordViewModel> words, IUpdater updater, string namePage, int indexWord = 0)
        {
            InitializeComponent();
            ViewModel = new WordInfoViewModel(words, updater, namePage, indexWord);
            DataContext = ViewModel;
        }

        private void DownOnTranslate(object sender, RoutedEventArgs e)
        {
            (sender as TextBlock).Text = (sender as TextBlock).Tag.ToString();
        }
    }
}
